<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Dashboard Header -->
    <div class="bg-white shadow-sm border-b border-gray-200">
      <div class="max-w-7xl mx-auto px-6 py-4">
        <div class="flex items-center justify-between">
          <div class="flex items-center space-x-4">
            <img :src="user?.avatar || '/portrait.jpg'" 
                 class="w-12 h-12 rounded-full object-cover border-2 border-[#7b1e3a]" 
                 :alt="user?.name" />
            <div>
              <h1 class="text-2xl font-bold text-gray-900">Welcome back, {{ user?.name }}!</h1>
              <p class="text-gray-600">Manage your bookings and photo gallery</p>
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
            <RouterLink to="/photographers" 
                        class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white px-6">
              Book New Session
            </RouterLink>
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
              <p class="text-2xl font-bold text-gray-900">{{ bookings.length }}</p>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-xl shadow-md p-6">
          <div class="flex items-center">
            <div class="w-12 h-12 bg-green-100 rounded-lg flex items-center justify-center">
              <i class="fas fa-images text-green-600 text-xl"></i>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-600">Photo Albums</p>
              <p class="text-2xl font-bold text-gray-900">{{ photoAlbums.length }}</p>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-xl shadow-md p-6">
          <div class="flex items-center">
            <div class="w-12 h-12 bg-yellow-100 rounded-lg flex items-center justify-center">
              <i class="fas fa-clock text-yellow-600 text-xl"></i>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-600">Upcoming</p>
              <p class="text-2xl font-bold text-gray-900">{{ upcomingBookings.length }}</p>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-xl shadow-md p-6">
          <div class="flex items-center">
            <div class="w-12 h-12 bg-purple-100 rounded-lg flex items-center justify-center">
              <i class="fas fa-star text-purple-600 text-xl"></i>
            </div>
            <div class="ml-4">
              <p class="text-sm font-medium text-gray-600">Reviews Given</p>
              <p class="text-2xl font-bold text-gray-900">{{ completedBookings.length }}</p>
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
          <!-- Bookings Tab -->
          <div v-if="activeTab === 'bookings'" class="space-y-6">
            <div class="flex items-center justify-between">
              <h3 class="text-lg font-semibold text-gray-900">Your Bookings</h3>
              <select v-model="bookingFilter" class="border border-gray-300 rounded-lg px-3 py-2 text-sm">
                <option value="all">All Bookings</option>
                <option value="upcoming">Upcoming</option>
                <option value="completed">Completed</option>
                <option value="cancelled">Cancelled</option>
              </select>
            </div>

            <div class="grid gap-4">
              <div v-for="booking in filteredBookings" :key="booking.id" 
                   class="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow">
                <div class="flex items-center justify-between">
                  <div class="flex items-center space-x-4">
                    <img :src="booking.photographer.image" 
                         class="w-16 h-16 rounded-full object-cover" 
                         :alt="booking.photographer.name" />
                    <div>
                      <h4 class="font-semibold text-gray-900">{{ booking.photographer.name }}</h4>
                      <p class="text-sm text-gray-600">{{ booking.photographer.studio }}</p>
                      <p class="text-sm text-gray-500">{{ booking.package }} Package</p>
                    </div>
                  </div>
                  <div class="text-right">
                    <p class="font-semibold text-gray-900">{{ booking.date }}</p>
                    <p class="text-sm text-gray-600">{{ booking.time }}</p>
                    <span :class="[
                      'inline-block px-2 py-1 rounded-full text-xs font-medium',
                      booking.status === 'confirmed' ? 'bg-green-100 text-green-800' :
                      booking.status === 'pending' ? 'bg-yellow-100 text-yellow-800' :
                      booking.status === 'completed' ? 'bg-blue-100 text-blue-800' :
                      'bg-red-100 text-red-800'
                    ]">
                      {{ booking.status.charAt(0).toUpperCase() + booking.status.slice(1) }}
                    </span>
                  </div>
                </div>
                <div class="mt-4 flex justify-end space-x-2">
                  <button v-if="booking.status === 'upcoming'" 
                          @click="rescheduleBooking(booking)"
                          class="btn btn-sm btn-outline border-gray-300 text-gray-700 hover:bg-gray-50">
                    Reschedule
                  </button>
                  <button v-if="booking.status === 'completed'" 
                          @click="viewPhotos(booking)"
                          class="btn btn-sm bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                    View Photos
                  </button>
                  <button v-if="booking.status === 'completed' && !booking.reviewed" 
                          @click="leaveReview(booking)"
                          class="btn btn-sm btn-outline border-[#7b1e3a] text-[#7b1e3a] hover:bg-[#7b1e3a] hover:text-white">
                    Leave Review
                  </button>
                </div>
              </div>
            </div>
          </div>

          <!-- Photos Tab -->
          <div v-if="activeTab === 'photos'" class="space-y-6">
            <div class="flex items-center justify-between">
              <h3 class="text-lg font-semibold text-gray-900">Your Photo Albums</h3>
              <button class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                <i class="fas fa-download mr-2"></i>
                Download All
              </button>
            </div>

            <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
              <div v-for="album in photoAlbums" :key="album.id" 
                   class="bg-gray-50 rounded-lg overflow-hidden hover:shadow-md transition-shadow cursor-pointer"
                   @click="openAlbum(album)">
                <div class="aspect-w-16 aspect-h-12 relative">
                  <img :src="album.coverPhoto" 
                       class="w-full h-48 object-cover" 
                       :alt="album.title" />
                  <div class="absolute top-2 right-2">
                    <span :class="[
                      'inline-block px-2 py-1 rounded-full text-xs font-medium',
                      album.status === 'edited' 
                        ? 'bg-green-100 text-green-800' 
                        : 'bg-orange-100 text-orange-800'
                    ]">
                      <i :class="album.status === 'edited' ? 'fas fa-check-circle' : 'fas fa-camera'" class="mr-1"></i>
                      {{ album.status === 'edited' ? 'Ready' : 'Raw' }}
                    </span>
                  </div>
                </div>
                <div class="p-4">
                  <h4 class="font-semibold text-gray-900">{{ album.title }}</h4>
                  <p class="text-sm text-gray-600">{{ album.photographer }}</p>
                  <div class="flex items-center justify-between mt-2">
                    <p class="text-sm text-gray-500">{{ album.photos.length }} photos • {{ album.date }}</p>
                    <button v-if="album.status === 'edited'" 
                            @click.stop="downloadAllPhotos(album)"
                            class="text-[#7b1e3a] hover:text-[#5c162c] transition-colors">
                      <i class="fas fa-download"></i>
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Profile Tab -->
          <div v-if="activeTab === 'profile'" class="space-y-6">
            <h3 class="text-lg font-semibold text-gray-900">Profile Settings</h3>
            
            <form @submit.prevent="updateProfile" class="space-y-4 max-w-2xl">
              <div class="grid md:grid-cols-2 gap-4">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Full Name</label>
                  <input v-model="profileForm.name" type="text" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
                  <input v-model="profileForm.email" type="email" required
                         class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
                </div>
              </div>
              
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">Phone Number</label>
                <input v-model="profileForm.phone" type="tel" required
                       class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]" />
              </div>

              <div>
                <label class="block text-sm font-medium text-gray-700 mb-1">Preferred Contact Method</label>
                <select v-model="profileForm.preferredContact" 
                        class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-[#7b1e3a] focus:border-[#7b1e3a]">
                  <option value="email">Email</option>
                  <option value="phone">Phone</option>
                  <option value="both">Both</option>
                </select>
              </div>

              <button type="submit" class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                Update Profile
              </button>
            </form>
          </div>
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
                notification.type === 'photos' ? 'bg-green-500' :
                'bg-yellow-500'
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

    <!-- Album Viewer Modal -->
    <div v-if="showAlbumViewer && selectedAlbum" 
         @click="closeAlbumViewer"
         class="fixed inset-0 z-50 bg-black bg-opacity-75 flex items-center justify-center p-4">
      <div @click.stop 
           class="bg-white rounded-xl max-w-6xl w-full max-h-[90vh] overflow-hidden">
        <!-- Modal Header -->
        <div class="flex items-center justify-between p-6 border-b border-gray-200">
          <div>
            <h2 class="text-2xl font-bold text-gray-900">{{ selectedAlbum.title }}</h2>
            <div class="flex items-center space-x-4 mt-1">
              <p class="text-gray-600">{{ selectedAlbum.photographer }} • {{ selectedAlbum.date }}</p>
              <span :class="[
                'inline-block px-3 py-1 rounded-full text-xs font-medium',
                selectedAlbum.status === 'edited' 
                  ? 'bg-green-100 text-green-800' 
                  : 'bg-orange-100 text-orange-800'
              ]">
                <i :class="selectedAlbum.status === 'edited' ? 'fas fa-check-circle' : 'fas fa-camera'" class="mr-1"></i>
                {{ selectedAlbum.status === 'edited' ? 'Edited & Ready' : 'Unedited Raw Photos' }}
              </span>
            </div>
          </div>
          <button @click="closeAlbumViewer" 
                  class="p-2 text-gray-400 hover:text-gray-600 transition-colors">
            <i class="fas fa-times text-xl"></i>
          </button>
        </div>

        <!-- Album Content -->
        <div class="p-6 max-h-[70vh] overflow-y-auto">
          <!-- Edited Album -->
          <div v-if="selectedAlbum.status === 'edited'">
            <div class="flex items-center justify-between mb-6">
              <div>
                <h3 class="text-lg font-semibold text-gray-900">Ready for Download</h3>
                <p class="text-sm text-gray-600">{{ selectedAlbum.photos.length }} edited photo(s) available</p>
              </div>
              <button @click="downloadAllPhotos" class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
                <i class="fas fa-download mr-2"></i>
                Download All ({{ selectedAlbum.photos.length }})
              </button>
            </div>
            
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
              <div v-for="photo in selectedAlbum.photos" :key="photo.id" 
                   class="group relative aspect-square bg-gray-100 rounded-lg overflow-hidden cursor-pointer hover:shadow-lg transition-shadow">
                <img :src="photo.url" 
                     :alt="photo.filename"
                     class="w-full h-full object-cover group-hover:scale-105 transition-transform duration-300" />
                <div class="absolute inset-0 bg-black bg-opacity-0 group-hover:bg-opacity-20 transition-all duration-300 flex items-center justify-center">
                  <div class="opacity-0 group-hover:opacity-100 transition-opacity">
                    <button @click="downloadPhoto(photo)" 
                            class="bg-white text-gray-900 p-2 rounded-full hover:bg-gray-100 transition-colors mr-2">
                      <i class="fas fa-download"></i>
                    </button>
                    <button @click="viewFullscreen(photo)" 
                            class="bg-white text-gray-900 p-2 rounded-full hover:bg-gray-100 transition-colors">
                      <i class="fas fa-expand"></i>
                    </button>
                  </div>
                </div>
                <div class="absolute bottom-0 left-0 right-0 bg-gradient-to-t from-black to-transparent p-2">
                  <p class="text-white text-xs truncate">{{ photo.filename }}</p>
                </div>
              </div>
            </div>
          </div>

          <!-- Unedited Album -->
          <div v-if="selectedAlbum.status === 'unedited'">
            <div class="flex items-center justify-between mb-6">
              <div>
                <h3 class="text-lg font-semibold text-gray-900">Raw Photos</h3>
                <p class="text-sm text-gray-600">Select photos you'd like the photographer to edit</p>
              </div>
              <div class="flex items-center space-x-4">
                <label class="flex items-center space-x-2 cursor-pointer">
                  <input type="checkbox" 
                         v-model="selectAll" 
                         @change="toggleSelectAll"
                         class="rounded border-gray-300 text-[#7b1e3a] focus:ring-[#7b1e3a]" />
                  <span class="text-sm font-medium text-gray-700">Select All</span>
                </label>
                <button @click="requestEditing" 
                        :disabled="selectedPhotos.length === 0"
                        :class="[
                          'btn text-white',
                          selectedPhotos.length > 0 
                            ? 'bg-[#7b1e3a] hover:bg-[#5c162c]' 
                            : 'bg-gray-400 cursor-not-allowed'
                        ]">
                  <i class="fas fa-edit mr-2"></i>
                  Request Editing ({{ selectedPhotos.length }})
                </button>
              </div>
            </div>
            
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
              <div v-for="photo in selectedAlbum.photos" :key="photo.id" 
                   class="group relative aspect-square bg-gray-100 rounded-lg overflow-hidden">
                <img :src="photo.url" 
                     :alt="photo.filename"
                     class="w-full h-full object-cover transition-transform duration-300"
                     :class="photo.selected ? 'scale-95' : 'group-hover:scale-105'" />
                
                <!-- Selection Overlay -->
                <div :class="[
                  'absolute inset-0 transition-all duration-300 flex items-center justify-center',
                  photo.selected 
                    ? 'bg-[#7b1e3a] bg-opacity-30 border-4 border-[#7b1e3a]' 
                    : 'bg-black bg-opacity-0 group-hover:bg-opacity-20'
                ]">
                  <div class="opacity-0 group-hover:opacity-100 transition-opacity flex items-center space-x-2">
                    <button @click="viewFullscreen(photo)" 
                            class="bg-white text-gray-900 p-2 rounded-full hover:bg-gray-100 transition-colors">
                      <i class="fas fa-expand"></i>
                    </button>
                  </div>
                </div>

                <!-- Selection Checkbox -->
                <div class="absolute top-2 left-2">
                  <label class="flex items-center cursor-pointer">
                    <input type="checkbox" 
                           v-model="photo.selected" 
                           @change="updateSelectedPhotos"
                           class="rounded border-gray-300 text-[#7b1e3a] focus:ring-[#7b1e3a] shadow-lg" />
                  </label>
                </div>

                <!-- Selected Badge -->
                <div v-if="photo.selected" 
                     class="absolute top-2 right-2 bg-[#7b1e3a] text-white p-1 rounded-full">
                  <i class="fas fa-check text-xs"></i>
                </div>

                <div class="absolute bottom-0 left-0 right-0 bg-gradient-to-t from-black to-transparent p-2">
                  <p class="text-white text-xs truncate">{{ photo.filename }}</p>
                </div>
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

const user = ref(null)
const activeTab = ref('bookings')
const bookingFilter = ref('all')
const showNotifications = ref(false)
const showAlbumViewer = ref(false)
const selectedAlbum = ref(null)
const albumViewerTab = ref('edited')
const selectedPhotos = ref([])
const selectAll = ref(false)

const profileForm = ref({
  name: '',
  email: '',
  phone: '',
  preferredContact: 'email'
})

const tabs = computed(() => [
  { id: 'bookings', name: 'Bookings', icon: 'fas fa-calendar-alt', count: bookings.value.length },
  { id: 'photos', name: 'Photo Albums', icon: 'fas fa-images', count: photoAlbums.value.length },
  { id: 'profile', name: 'Profile', icon: 'fas fa-user', count: null }
])

const bookings = ref([
  {
    id: 1,
    photographer: { name: 'Nuru Ahmed', studio: "Nuru's Touch", image: '/man-lens.jpg' },
    package: 'Professional',
    date: '2025-09-25',
    time: '10:00 AM',
    status: 'confirmed'
  },
  {
    id: 2,
    photographer: { name: 'Fatou Ceesay', studio: 'Fatou Studios', image: '/shot-2.jpg' },
    package: 'Essential',
    date: '2025-08-15',
    time: '2:00 PM',
    status: 'completed',
    reviewed: true
  },
  {
    id: 3,
    photographer: { name: 'Omar Bah', studio: 'Bah Photography', image: '/shot-5.jpg' },
    package: 'Premium',
    date: '2025-10-05',
    time: '11:30 AM',
    status: 'pending'
  }
])

const photoAlbums = ref([
  {
    id: 1,
    title: 'Family Portrait Session',
    photographer: 'Fatou Ceesay',
    coverPhoto: '/portrait.jpg',
    photoCount: 25,
    date: 'Aug 15, 2025',
    status: 'edited', // 'edited' or 'unedited'
    photos: [
      { id: 'p1', url: '/portrait.jpg', filename: 'family_portrait_001.jpg' },
      { id: 'p2', url: '/shot-2.jpg', filename: 'family_portrait_002.jpg' },
      { id: 'p3', url: '/shot-3.jpg', filename: 'family_portrait_003.jpg' },
      { id: 'p4', url: '/shot-1.jpg', filename: 'family_portrait_004.jpg' }
    ]
  },
  {
    id: 2,
    title: 'Wedding Photography',
    photographer: 'Nuru Ahmed',
    coverPhoto: '/wedding.jpg',
    photoCount: 150,
    date: 'Jul 20, 2025',
    status: 'edited',
    photos: [
      { id: 'p5', url: '/wedding.jpg', filename: 'wedding_001.jpg' },
      { id: 'p6', url: '/shot-5.jpg', filename: 'wedding_002.jpg' },
      { id: 'p7', url: '/event.jpg', filename: 'wedding_003.jpg' }
    ]
  },
  {
    id: 3,
    title: 'Corporate Headshots',
    photographer: 'Omar Bah',
    coverPhoto: '/man-lens.jpg',
    photoCount: 45,
    date: 'Sep 10, 2025',
    status: 'unedited',
    photos: [
      { id: 'p8', url: '/man-lens.jpg', filename: 'corporate_001_raw.jpg', selected: false },
      { id: 'p9', url: '/pexels-amar-30670956.jpg', filename: 'corporate_002_raw.jpg', selected: false },
      { id: 'p10', url: '/lens.jpg', filename: 'corporate_003_raw.jpg', selected: false },
      { id: 'p11', url: '/lens-2.jpg', filename: 'corporate_004_raw.jpg', selected: false },
      { id: 'p12', url: '/nature-shot.jpg', filename: 'corporate_005_raw.jpg', selected: false }
    ]
  }
])

const notifications = ref([
  {
    id: 1,
    type: 'photos',
    title: 'Photos Ready!',
    message: 'Your family portrait photos are now available for download.',
    time: '2 hours ago'
  },
  {
    id: 2,
    type: 'booking',
    title: 'Booking Confirmed',
    message: 'Your session with Nuru Ahmed has been confirmed for Sep 25.',
    time: '1 day ago'
  }
])

const filteredBookings = computed(() => {
  if (bookingFilter.value === 'all') return bookings.value
  return bookings.value.filter(booking => {
    if (bookingFilter.value === 'upcoming') {
      return ['confirmed', 'pending'].includes(booking.status)
    }
    return booking.status === bookingFilter.value
  })
})

const upcomingBookings = computed(() => 
  bookings.value.filter(b => ['confirmed', 'pending'].includes(b.status))
)

const completedBookings = computed(() => 
  bookings.value.filter(b => b.status === 'completed')
)

const rescheduleBooking = (booking) => {
  toast.info('Reschedule feature coming soon!')
}

const viewPhotos = (booking) => {
  toast.info('Photo gallery feature coming soon!')
}

const leaveReview = (booking) => {
  toast.info('Review system coming soon!')
}

const openAlbum = (album) => {
  selectedAlbum.value = album
  showAlbumViewer.value = true
  updateSelectedPhotos()
}

const closeAlbumViewer = () => {
  showAlbumViewer.value = false
  selectedAlbum.value = null
  selectedPhotos.value = []
  selectAll.value = false
}

const updateSelectedPhotos = () => {
  if (selectedAlbum.value && selectedAlbum.value.status === 'unedited') {
    selectedPhotos.value = selectedAlbum.value.photos?.filter(photo => photo.selected) || []
    selectAll.value = selectedPhotos.value.length === selectedAlbum.value.photos?.length && selectedPhotos.value.length > 0
  }
}

const toggleSelectAll = () => {
  if (selectedAlbum.value?.photos && selectedAlbum.value.status === 'unedited') {
    selectedAlbum.value.photos.forEach(photo => {
      photo.selected = selectAll.value
    })
    updateSelectedPhotos()
  }
}

const requestEditing = () => {
  if (selectedPhotos.value.length === 0) return
  
  const photoNames = selectedPhotos.value.map(photo => photo.filename).join(', ')
  toast.success(`Edit request sent for ${selectedPhotos.value.length} photo(s) to ${selectedAlbum.value.photographer}`)
  
  // Reset selections
  selectedAlbum.value.photos.forEach(photo => {
    photo.selected = false
  })
  updateSelectedPhotos()
}

const downloadPhoto = (photo) => {
  toast.info(`Downloading ${photo.filename}...`)
  // In a real app, this would trigger an actual download
}

const downloadAllPhotos = (album = null) => {
  const targetAlbum = album || selectedAlbum.value
  if (targetAlbum && targetAlbum.status === 'edited') {
    toast.success(`Downloading all ${targetAlbum.photos.length} photos from ${targetAlbum.title}`)
    // In a real app, this would trigger a zip download
  }
}

const viewFullscreen = (photo) => {
  toast.info('Fullscreen viewer coming soon!')
}

const updateProfile = () => {
  // Update user data
  user.value = { ...user.value, ...profileForm.value }
  localStorage.setItem('user', JSON.stringify(user.value))
  toast.success('Profile updated successfully!')
}

onMounted(() => {
  // Check if user is logged in
  const userData = localStorage.getItem('user')
  if (userData) {
    user.value = JSON.parse(userData)
    profileForm.value = {
      name: user.value.name || '',
      email: user.value.email || '',
      phone: user.value.phone || '',
      preferredContact: 'email'
    }
  } else {
    router.push('/login?redirect=/dashboard')
  }
})
</script>