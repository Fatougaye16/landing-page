import { ref, type Ref } from 'vue'
import { useSupabase } from './useSupabase'
import { useAuth } from './useAuth'
import { toast } from 'vue3-toastify'

interface Profile {
  id: string
  full_name?: string
  studio_name?: string
  avatar_url?: string
  specialization?: string
  phone?: string
  email?: string
  role?: string
}

export interface BookingData {
  photographerId: string
  sessionType: 'portrait' | 'wedding' | 'event' | 'commercial' | 'nature'
  sessionDate: string
  sessionTime: string
  durationHours: number
  location: string
  price: number
  clientNotes?: string
}

export interface Booking {
  id: string
  photographer_id: string
  client_id: string
  session_type: string
  session_date: string
  session_time: string
  duration_hours: number
  location: string
  price: number
  deposit_amount?: number
  status: 'pending' | 'confirmed' | 'in_progress' | 'completed' | 'cancelled' | 'refunded'
  client_notes?: string
  photographer_notes?: string
  cancellation_reason?: string
  created_at: string
  updated_at: string
  photographer?: {
    id: string
    full_name: string
    studio_name: string
    avatar_url: string
    specialization: string
  }
}

export function useBooking() {
  const { fetchData, insertData, updateData, loading, error } = useSupabase()
  const { user } = useAuth()
  
  const bookings: Ref<Booking[]> = ref([])
  const isLoading = ref(false)

  // Create a new booking
  const createBooking = async (bookingData: BookingData): Promise<Booking | null> => {
    if (!user.value) {
      toast.error('Please log in to create a booking')
      return null
    }

    isLoading.value = true
    try {
      const newBooking = await insertData('photo_sessions', {
        photographer_id: bookingData.photographerId,
        client_id: user.value.id,
        session_type: bookingData.sessionType,
        session_date: bookingData.sessionDate,
        session_time: bookingData.sessionTime,
        duration_hours: bookingData.durationHours,
        location: bookingData.location,
        price: bookingData.price,
        client_notes: bookingData.clientNotes,
        status: 'pending'
      })

      if (newBooking) {
        toast.success('Booking request sent successfully!')
        await loadUserBookings() // Refresh bookings list
        return newBooking as Booking
      } else {
        toast.error(error.value || 'Failed to create booking')
        return null
      }
    } catch (err) {
      console.error('Error creating booking:', err)
      toast.error('Failed to create booking')
      return null
    } finally {
      isLoading.value = false
    }
  }

  // Load user's bookings
  const loadUserBookings = async (): Promise<void> => {
    if (!user.value) return

    isLoading.value = true
    try {
      const sessionsData = await fetchData('photo_sessions', { client_id: user.value.id })
      
      if (sessionsData) {
        // Fetch photographer details for each booking
        const bookingsWithPhotographers = await Promise.all(
          sessionsData.map(async (session: any) => {
            const photographerData = await fetchData('profiles', { id: session.photographer_id })
            const photographer = photographerData?.[0] as Profile | undefined
            
            return {
              ...session,
              photographer: photographer ? {
                id: photographer.id,
                full_name: photographer.full_name || '',
                studio_name: photographer.studio_name || '',
                avatar_url: photographer.avatar_url || '',
                specialization: photographer.specialization || ''
              } : null
            }
          })
        )
        
        bookings.value = bookingsWithPhotographers
      }
    } catch (err) {
      console.error('Error loading bookings:', err)
      toast.error('Failed to load bookings')
    } finally {
      isLoading.value = false
    }
  }

  // Update booking status
  const updateBookingStatus = async (
    bookingId: string, 
    status: Booking['status'],
    notes?: string
  ): Promise<boolean> => {
    isLoading.value = true
    try {
      const updateData: any = { status }
      
      if (notes && user.value) {
        // Determine if user is photographer or client
        const booking = bookings.value.find(b => b.id === bookingId)
        if (booking) {
          if (booking.photographer_id === user.value.id) {
            updateData.photographer_notes = notes
          } else if (booking.client_id === user.value.id) {
            updateData.client_notes = notes
          }
        }
      }

      const updatedBooking = await updateData('photo_sessions', bookingId, updateData)
      
      if (updatedBooking) {
        toast.success('Booking updated successfully!')
        await loadUserBookings() // Refresh bookings
        return true
      } else {
        toast.error(error.value || 'Failed to update booking')
        return false
      }
    } catch (err) {
      console.error('Error updating booking:', err)
      toast.error('Failed to update booking')
      return false
    } finally {
      isLoading.value = false
    }
  }

  // Cancel booking
  const cancelBooking = async (bookingId: string, reason?: string): Promise<boolean> => {
    return updateBookingStatus(bookingId, 'cancelled', reason)
  }

  // Confirm booking (for photographers)
  const confirmBooking = async (bookingId: string, notes?: string): Promise<boolean> => {
    return updateBookingStatus(bookingId, 'confirmed', notes)
  }

  // Complete booking
  const completeBooking = async (bookingId: string, notes?: string): Promise<boolean> => {
    return updateBookingStatus(bookingId, 'completed', notes)
  }

  // Load photographer's bookings (for photographer dashboard)
  const loadPhotographerBookings = async (photographerId?: string): Promise<Booking[]> => {
    const targetId = photographerId || user.value?.id
    if (!targetId) return []

    isLoading.value = true
    try {
      const sessionsData = await fetchData('photo_sessions', { photographer_id: targetId })
      
      if (sessionsData) {
        // Fetch client details for each booking
        const bookingsWithClients = await Promise.all(
          sessionsData.map(async (session: any) => {
            const clientData = await fetchData('profiles', { id: session.client_id })
            const client = clientData?.[0] as Profile | undefined
            
            return {
              ...session,
              client: client ? {
                id: client.id,
                full_name: client.full_name || '',
                avatar_url: client.avatar_url || '',
                phone: client.phone || ''
              } : null
            }
          })
        )
        
        bookings.value = bookingsWithClients
        return bookingsWithClients
      }
      return []
    } catch (err) {
      console.error('Error loading photographer bookings:', err)
      toast.error('Failed to load bookings')
      return []
    } finally {
      isLoading.value = false
    }
  }

  // Get available time slots for a photographer on a specific date
  const getAvailableSlots = async (photographerId: string, date: string): Promise<string[]> => {
    try {
      // Fetch photographer's availability and existing bookings for the date
      const [availabilityData, bookingsData] = await Promise.all([
        fetchData('availability', { photographer_id: photographerId, date }),
        fetchData('photo_sessions', { 
          photographer_id: photographerId, 
          session_date: date,
          status: ['confirmed', 'pending', 'in_progress']
        })
      ])

      // Default available slots (9 AM to 6 PM)
      const defaultSlots = [
        '09:00', '10:00', '11:00', '12:00', 
        '13:00', '14:00', '15:00', '16:00', '17:00', '18:00'
      ]

      let availableSlots = defaultSlots
      
      // If photographer has specific availability set, use that
      if (availabilityData && availabilityData.length > 0) {
        availableSlots = availabilityData
          .filter((slot: any) => slot.is_available)
          .map((slot: any) => slot.start_time)
      }

      // Remove booked slots
      if (bookingsData && bookingsData.length > 0) {
        const bookedSlots = bookingsData.map((booking: any) => booking.session_time)
        availableSlots = availableSlots.filter(slot => !bookedSlots.includes(slot))
      }

      return availableSlots
    } catch (err) {
      console.error('Error fetching available slots:', err)
      return []
    }
  }

  return {
    bookings,
    isLoading: isLoading,
    createBooking,
    loadUserBookings,
    loadPhotographerBookings,
    updateBookingStatus,
    cancelBooking,
    confirmBooking,
    completeBooking,
    getAvailableSlots
  }
}