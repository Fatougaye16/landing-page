<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Dashboard Header -->
    <div class="bg-white shadow-sm border-b border-gray-200">
      <div class="max-w-7xl mx-auto px-6 py-4">
        <div class="flex items-center justify-between">
          <div class="flex items-center space-x-4">
            <img :src="photographer?.avatar || '/man-lens.jpg'" 
                 class="w-12 h-12 rounded-full object-cover border-2 border-[#7b1e3a]" 
                 :alt="photographer?.name" />
            <div>
              <h1 class="text-2xl font-bold text-gray-900">{{ photographer?.name }}'s Studio</h1>
              <p class="text-gray-600">Manage your bookings and client galleries</p>
            </div>
          </div>
          <div class="flex items-center space-x-4">
            <button @click="showNotifications = !showNotifications" 
                    class="relative p-2 text-gray-600 hover:text-gray-900 transition-colors">
              <i class="fas fa-bell text-xl"></i>
              <span v-if="notifications.length > 0" 
                    class="absolute -top-1 -right-1 bg-red-500 text-white text-xs rounded-full w-5 h-5 flex items-center justify-center">
                {{ notifications.length }}
              </span>
            </button>
            <button @click="logout" 
                    class="flex items-center space-x-2 px-4 py-2 text-gray-600 hover:text-red-600 transition-colors rounded-lg hover:bg-red-50">
              <i class="fas fa-sign-out-alt"></i>
              <span class="font-medium">Logout</span>
            </button>
          </div>
        </div>
      </div>
    </div>

    <div class="max-w-7xl mx-auto px-6 py-8">
      <!-- Quick Stats -->
      <div class="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
        <div class="bg-white rounded-xl shadow-md p-6">
          <div class="flex items-center">
            <div class="w-12 h-12 bg-blue-100 rounded-lg flex items-center justify-center">
              <i class="fas fa-calendar-check text-blue-600 text-xl"></i>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-600">Total Bookings</p>
              <p class="text-2xl font-bold text-gray-900">{{ appointments.length }}</p>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-xl shadow-md p-6">
          <div class="flex items-center">
            <div class="w-12 h-12 bg-green-100 rounded-lg flex items-center justify-center">
              <i class="fas fa-images text-green-600 text-xl"></i>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-600">Client Albums</p>
              <p class="text-2xl font-bold text-gray-900">{{ clientAlbums.length }}</p>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-xl shadow-md p-6">
          <div class="flex items-center">
            <div class="w-12 h-12 bg-yellow-100 rounded-lg flex items-center justify-center">
              <i class="fas fa-clock text-yellow-600 text-xl"></i>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-600">This Week</p>
              <p class="text-2xl font-bold text-gray-900">{{ thisWeekAppointments.length }}</p>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-xl shadow-md p-6">
          <div class="flex items-center">
            <div class="w-12 h-12 bg-purple-100 rounded-lg flex items-center justify-center">
              <i class="fas fa-star text-purple-600 text-xl"></i>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-600">Average Rating</p>
              <p class="text-2xl font-bold text-gray-900">4.8</p>
            </div>
          </div>
        </div>
      </div>

      <!-- Main Content Tabs -->
      <div class="bg-white rounded-xl shadow-md">
        <!-- Tab Navigation -->
        <div class="border-b border-gray-200">
          <nav class="flex space-x-8 px-6">
            <button
              v-for="tab in tabs"
              :key="tab.id"
              @click="activeTab = tab.id"
              :class="[
                'py-4 px-1 border-b-2 font-medium text-sm transition-colors',
                activeTab === tab.id
                  ? 'border-[#7b1e3a] text-[#7b1e3a]'
                  : 'border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300'
              ]"
            >
              <i :class="tab.icon" class="mr-2"></i>
              {{ tab.name }}
              <span v-if="tab.count" class="ml-2 bg-gray-100 text-gray-600 px-2 py-1 rounded-full text-xs">
                {{ tab.count }}
              </span>
            </button>
          </nav>
        </div>

        <!-- Tab Content -->
        <div class="p-6">
          <!-- Appointments Tab -->
          <div v-if="activeTab === 'appointments'" class="space-y-6">
            <div class="flex items-center justify-between">
              <h3 class="text-lg font-semibold text-gray-900">Client Appointments</h3>
              <select v-model="appointmentFilter" class="border border-gray-300 rounded-lg px-3 py-2 text-sm">
                <option value="all">All Appointments</option>
                <option value="upcoming">Upcoming</option>
                <option value="completed">Completed</option>
                <option value="pending">Pending Confirmation</option>
              </select>
            </div>

            <div class="grid gap-4">
              <div v-for="appointment in filteredAppointments" :key="appointment.id" 
                   class="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow">
                <div class="flex items-center justify-between">
                  <div class="flex items-center space-x-4">
                    <img :src="appointment.client.avatar || '/portrait.jpg'" 
                         class="w-16 h-16 rounded-full object-cover" 
                         :alt="appointment.client.name" />
                    <div>
                      <h4 class="font-semibold text-gray-900">{{ appointment.client.name }}</h4>
                      <p class="text-sm text-gray-600">{{ appointment.client.email }}</p>
                      <p class="text-sm text-gray-500">{{ appointment.package }} Package</p>
                    </div>
                  </div>
                  <div class="text-right">
                    <p class="font-semibold text-gray-900">{{ appointment.date }}</p>
                    <p class="text-sm text-gray-600">{{ appointment.time }}</p>
                    <span :class="[
                      'inline-block px-2 py-1 rounded-full text-xs font-medium',
                      appointment.status === 'confirmed' ? 'bg-green-100 text-green-800' :
                      appointment.status === 'pending' ? 'bg-yellow-100 text-yellow-800' :
                      appointment.status === 'completed' ? 'bg-blue-100 text-blue-800' :
                      'bg-red-100 text-red-800'
                    ]">
                      {{ appointment.status.charAt(0).toUpperCase() + appointment.status.slice(1) }}
                    </span>
                  </div>
                </div>
                <div class="mt-4 flex justify-end space-x-2">
                  <button v-if="appointment.status === 'pending'" 
                          @click="confirmAppointment(appointment)"
                          class="btn btn-sm bg-green-600 hover:bg-green-700 text-white">
                    Confirm
                  </button>
                  <button v-if="appointment.status === 'pending'" 
                          @click="declineAppointment(appointment)"
                          class="btn btn-sm btn-outline border-red-300 text-red-700 hover:bg-red-50">
                    Decline
                  </button>
                  <button v-if="appointment.status === 'confirmed'" 
                          @click="rescheduleAppointment(appointment)"
                          class="btn btn-sm btn-outline border-gray-300 text-gray-700 hover:bg-gray-50">
                    Reschedule
                  </button>
                  <button v-if="appointment.status === 'confirmed'" 
                          @click="markAsCompleted(appointment)"
                          class="btn btn-sm bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                    Mark Complete
                  </button>
                  <button v-if="appointment.status === 'completed'" 
                          @click="managePhotos(appointment)"
                          class="btn btn-sm bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                    Manage Photos
                  </button>
                </div>
              </div>
            </div>
          </div>

          <!-- Client Albums Tab -->
          <div v-if="activeTab === 'albums'" class="space-y-6">
            <div class="flex items-center justify-between">
              <h3 class="text-lg font-semibold text-gray-900">Client Photo Albums</h3>
              <button @click="showUploadModal = true" class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                <i class="fas fa-plus mr-2"></i>
                Create New Album
              </button>
            </div>

            <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
              <div v-for="album in clientAlbums" :key="album.id" 
                   class="bg-gray-50 rounded-lg overflow-hidden hover:shadow-md transition-shadow cursor-pointer"
                   @click="openAlbumManager(album)">
                <div class="aspect-w-16 aspect-h-12 relative">
                  <img :src="album.coverPhoto" 
                       class="w-full h-48 object-cover" 
                       :alt="album.title" />
                  <div class="absolute top-2 right-2">
                    <span :class="[
                      'inline-block px-2 py-1 rounded-full text-xs font-medium',
                      album.status === 'edited' 
                        ? 'bg-green-100 text-green-800' 
                        : album.editRequests > 0
                          ? 'bg-yellow-100 text-yellow-800'
                          : 'bg-gray-100 text-gray-800'
                    ]">
                      <i :class="
                        album.status === 'edited' ? 'fas fa-check-circle' : 
                        album.editRequests > 0 ? 'fas fa-edit' : 
                        'fas fa-camera'
                      " class="mr-1"></i>
                      {{ 
                        album.status === 'edited' ? 'Ready' : 
                        album.editRequests > 0 ? `${album.editRequests} Requests` : 
                        'Raw' 
                      }}
                    </span>
                  </div>
                </div>
                <div class="p-4">
                  <h4 class="font-semibold text-gray-900">{{ album.title }}</h4>
                  <p class="text-sm text-gray-600">{{ album.client }}</p>
                  <div class="flex items-center justify-between mt-2">
                    <p class="text-sm text-gray-500">{{ album.photos.length }} photos • {{ album.date }}</p>
                    <div class="flex items-center space-x-2">
                      <button @click.stop="uploadPhotos(album)" 
                              class="text-[#7b1e3a] hover:text-[#5c162c] transition-colors" 
                              title="Upload Photos">
                        <i class="fas fa-upload"></i>
                      </button>
                      <button @click.stop="shareAlbum(album)" 
                              class="text-blue-600 hover:text-blue-800 transition-colors" 
                              title="Share Album">
                        <i class="fas fa-share"></i>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Profile Tab -->
          <div v-if="activeTab === 'profile'" class="space-y-6">
            <h3 class="text-lg font-semibold text-gray-900">Studio Profile</h3>
            
            <form @submit.prevent="updateProfile" class="space-y-6 max-w-4xl">
              <div class="grid md:grid-cols-2 gap-6">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Studio Name</label>
                  <input v-model="profileForm.studioName" type="text" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Photographer Name</label>
                  <input v-model="profileForm.name" type="text" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
              </div>

              <div class="grid md:grid-cols-2 gap-6">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
                  <input v-model="profileForm.email" type="email" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Phone Number</label>
                  <input v-model="profileForm.phone" type="tel" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
              </div>

              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">Studio Description</label>
                <textarea v-model="profileForm.description" rows="4" required
                          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]"
                          placeholder="Tell clients about your photography style and services..."></textarea>
              </div>

              <div class="grid md:grid-cols-3 gap-6">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Years of Experience</label>
                  <input v-model.number="profileForm.experience" type="number" min="0" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Base Price</label>
                  <input v-model.number="profileForm.basePrice" type="number" min="0" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Specialization</label>
                  <select v-model="profileForm.specialization" 
                          class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]">
                    <option value="portrait">Portrait</option>
                    <option value="wedding">Wedding</option>
                    <option value="event">Event</option>
                    <option value="commercial">Commercial</option>
                    <option value="nature">Nature</option>
                  </select>
                </div>
              </div>

              <button type="submit" class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                Update Profile
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>

    <!-- Upload Photos Modal -->
    <div v-if="showUploadModal" 
         @click="showUploadModal = false"
         class="fixed inset-0 z-50 bg-black bg-opacity-50 flex items-center justify-center p-4">
      <div @click.stop 
           class="bg-white rounded-xl max-w-2xl w-full max-h-[80vh] overflow-hidden">
        <div class="flex items-center justify-between p-6 border-b border-gray-200">
          <h2 class="text-xl font-bold text-gray-900">Create New Album</h2>
          <button @click="showUploadModal = false" 
                  class="p-2 text-gray-400 hover:text-gray-600 transition-colors">
            <i class="fas fa-times text-xl"></i>
          </button>
        </div>
        
        <div class="p-6">
          <form @submit.prevent="createAlbum" class="space-y-4">
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Album Title</label>
              <input v-model="newAlbum.title" type="text" required
                     class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]"
                     placeholder="e.g., Sarah's Wedding Photos" />
            </div>
            
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Client Name</label>
              <input v-model="newAlbum.client" type="text" required
                     class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]"
                     placeholder="Client's full name" />
            </div>
            
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Album Type</label>
              <select v-model="newAlbum.status" 
                      class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]">
                <option value="unedited">Raw Photos (Unedited)</option>
                <option value="edited">Edited Photos (Ready)</option>
              </select>
            </div>
            
            <div class="flex justify-end space-x-4 pt-4">
              <button type="button" @click="showUploadModal = false" 
                      class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50">
                Cancel
              </button>
              <button type="submit" class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                Create Album
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>

    <!-- Notifications Dropdown -->
    <div v-if="showNotifications" 
         @click="showNotifications = false"
         class="fixed inset-0 z-50 bg-black bg-opacity-25">
      <div @click.stop 
           class="absolute top-16 right-6 w-80 bg-white rounded-lg shadow-xl border border-gray-200 max-h-96 overflow-y-auto">
        <div class="p-4 border-b border-gray-200">
          <h3 class="font-semibold text-gray-900">Notifications</h3>
        </div>
        <div v-if="notifications.length === 0" class="p-4 text-center text-gray-500">
          No new notifications
        </div>
        <div v-else>
          <div v-for="notification in notifications" :key="notification.id" 
               class="p-4 border-b border-gray-100 hover:bg-gray-50">
            <div class="flex items-start space-x-3">
              <div :class="[
                'w-2 h-2 rounded-full mt-2',
                notification.type === 'booking' ? 'bg-blue-500' :
                notification.type === 'edit-request' ? 'bg-orange-500' :
                'bg-green-500'
              ]"></div>
              <div class="flex-1">
                <p class="text-sm font-medium text-gray-900">{{ notification.title }}</p>
                <p class="text-sm text-gray-600">{{ notification.message }}</p>
                <p class="text-xs text-gray-400 mt-1">{{ notification.time }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

const router = useRouter()

const photographer = ref(null)
const activeTab = ref('appointments')
const appointmentFilter = ref('all')
const showNotifications = ref(false)
const showUploadModal = ref(false)

const profileForm = ref({
  studioName: '',
  name: '',
  email: '',
  phone: '',
  description: '',
  experience: 0,
  basePrice: 0,
  specialization: 'portrait'
})

const newAlbum = ref({
  title: '',
  client: '',
  status: 'unedited'
})

const tabs = computed(() => [
  { id: 'appointments', name: 'Appointments', icon: 'fas fa-calendar-alt', count: appointments.value.length },
  { id: 'albums', name: 'Client Albums', icon: 'fas fa-images', count: clientAlbums.value.length },
  { id: 'profile', name: 'Studio Profile', icon: 'fas fa-user-circle', count: null }
])

const appointments = ref([
  {
    id: 1,
    client: { name: 'Sarah Johnson', email: 'sarah@email.com', avatar: '/portrait.jpg' },
    package: 'Wedding',
    date: '2025-09-25',
    time: '10:00 AM',
    status: 'pending'
  },
  {
    id: 2,
    client: { name: 'Mike Thompson', email: 'mike@email.com', avatar: '/man-lens.jpg' },
    package: 'Portrait',
    date: '2025-09-20',
    time: '2:00 PM',
    status: 'confirmed'
  },
  {
    id: 3,
    client: { name: 'Emma Davis', email: 'emma@email.com', avatar: '/shot-2.jpg' },
    package: 'Event',
    date: '2025-08-15',
    time: '11:30 AM',
    status: 'completed'
  }
])

const clientAlbums = ref([
  {
    id: 1,
    title: 'Emma\'s Corporate Event',
    client: 'Emma Davis',
    coverPhoto: '/event.jpg',
    status: 'edited',
    editRequests: 0,
    date: 'Aug 15, 2025',
    photos: [
      { id: 'p1', url: '/event.jpg', filename: 'event_001.jpg' },
      { id: 'p2', url: '/shot-3.jpg', filename: 'event_002.jpg' }
    ]
  },
  {
    id: 2,
    title: 'Mike\'s Portrait Session',
    client: 'Mike Thompson',
    coverPhoto: '/man-lens.jpg',
    status: 'unedited',
    editRequests: 3,
    date: 'Sep 20, 2025',
    photos: [
      { id: 'p3', url: '/man-lens.jpg', filename: 'portrait_001_raw.jpg', selected: false },
      { id: 'p4', url: '/portrait.jpg', filename: 'portrait_002_raw.jpg', selected: false }
    ]
  }
])

const notifications = ref([
  {
    id: 1,
    type: 'booking',
    title: 'New Booking Request',
    message: 'Sarah Johnson requested a wedding photography session.',
    time: '2 hours ago'
  },
  {
    id: 2,
    type: 'edit-request',
    title: 'Photo Edit Request',
    message: 'Mike Thompson selected 3 photos for editing.',
    time: '1 day ago'
  }
])

const filteredAppointments = computed(() => {
  if (appointmentFilter.value === 'all') return appointments.value
  return appointments.value.filter(appointment => appointment.status === appointmentFilter.value)
})

const thisWeekAppointments = computed(() => {
  const now = new Date()
  const startOfWeek = new Date(now.setDate(now.getDate() - now.getDay()))
  const endOfWeek = new Date(now.setDate(startOfWeek.getDate() + 6))
  
  return appointments.value.filter(appointment => {
    const appointmentDate = new Date(appointment.date)
    return appointmentDate >= startOfWeek && appointmentDate <= endOfWeek
  })
})

const confirmAppointment = (appointment) => {
  appointment.status = 'confirmed'
  toast.success(`Appointment with ${appointment.client.name} confirmed!`)
}

const declineAppointment = (appointment) => {
  appointment.status = 'cancelled'
  toast.info(`Appointment with ${appointment.client.name} declined.`)
}

const markAsCompleted = (appointment) => {
  appointment.status = 'completed'
  toast.success(`Session with ${appointment.client.name} marked as completed!`)
}

const rescheduleAppointment = (appointment) => {
  toast.info('Reschedule feature coming soon!')
}

const managePhotos = (appointment) => {
  // Find the corresponding album for this appointment
  const album = clientAlbums.value.find(album => 
    album.client === appointment.client.name
  )
  if (album) {
    openAlbumManager(album)
  } else {
    toast.info('No album found for this client.')
  }
}

const openAlbumManager = (album) => {
  toast.info('Album management interface coming soon!')
}

const uploadPhotos = (album) => {
  toast.info('Photo upload feature coming soon!')
}

const shareAlbum = (album) => {
  toast.success(`Sharing link generated for ${album.title}`)
}

const createAlbum = () => {
  const album = {
    id: Date.now(),
    title: newAlbum.value.title,
    client: newAlbum.value.client,
    status: newAlbum.value.status,
    editRequests: 0,
    date: new Date().toLocaleDateString('en-US', { 
      year: 'numeric', 
      month: 'short', 
      day: 'numeric' 
    }),
    coverPhoto: '/shot-1.jpg',
    photos: []
  }
  
  clientAlbums.value.push(album)
  toast.success(`Album "${album.title}" created successfully!`)
  
  // Reset form and close modal
  newAlbum.value = { title: '', client: '', status: 'unedited' }
  showUploadModal.value = false
}

const logout = () => {
  localStorage.removeItem('user')
  localStorage.removeItem('photographer')
  photographer.value = null
  
  // Emit custom event to notify navbar of logout
  window.dispatchEvent(new CustomEvent('userLogout'))
  
  toast.success('Logged out successfully!')
  router.push('/')
}

const updateProfile = () => {
  // Update photographer data
  photographer.value = { ...photographer.value, ...profileForm.value }
  localStorage.setItem('photographer', JSON.stringify(photographer.value))
  toast.success('Studio profile updated successfully!')
}

onMounted(() => {
  // Check if photographer is logged in
  const photographerData = localStorage.getItem('photographer')
  if (photographerData) {
    photographer.value = JSON.parse(photographerData)
    profileForm.value = {
      studioName: photographer.value.studioName || '',
      name: photographer.value.name || '',
      email: photographer.value.email || '',
      phone: photographer.value.phone || '',
      description: photographer.value.description || '',
      experience: photographer.value.experience || 0,
      basePrice: photographer.value.basePrice || 0,
      specialization: photographer.value.specialization || 'portrait'
    }
  } else {
    // Check if user is logged in and is a photographer
    const userData = localStorage.getItem('user')
    if (userData) {
      const user = JSON.parse(userData)
      if (user.role === 'photographer') {
        photographer.value = user
        profileForm.value = {
          studioName: user.studioName || '',
          name: user.name || '',
          email: user.email || '',
          phone: user.phone || '',
          description: user.description || '',
          experience: user.experience || 0,
          basePrice: user.basePrice || 0,
          specialization: user.specialization || 'portrait'
        }
      } else {
        router.push('/dashboard') // Redirect clients to their dashboard
      }
    } else {
      router.push('/login?redirect=/photographer-dashboard')
    }
  }
})
</script>