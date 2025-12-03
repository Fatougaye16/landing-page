<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Dashboard Header -->
    <div class="bg-white shadow-sm border-b border-gray-200 dashboard-header">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 py-4">
        <div class="flex flex-col sm:flex-row sm:items-center justify-between space-y-4 sm:space-y-0">
          <div class="flex items-center space-x-3 sm:space-x-4">
            <img :src="user?.avatar || '/portrait.jpg'" 
                 class="w-10 h-10 sm:w-12 sm:h-12 rounded-full object-cover border-2 border-[#7b1e3a]" 
                 :alt="user?.name" />
            <div>
              <h1 class="text-lg sm:text-2xl font-bold text-gray-900">Welcome back, {{ user?.name }}!</h1>
              <p class="text-gray-600 text-sm sm:text-base">Manage your bookings and photo gallery</p>
            </div>
          </div>
          <div class="flex items-center justify-between sm:justify-end space-x-3 sm:space-x-4">
            <button @click="showNotifications = !showNotifications" 
                    class="relative p-2 text-gray-600 hover:text-gray-900 transition-colors rounded-lg hover:bg-gray-100">
              <i class="fas fa-bell text-lg sm:text-xl"></i>
              <span v-if="notifications.length > 0" 
                    class="absolute -top-1 -right-1 bg-red-500 text-white text-xs rounded-full w-4 h-4 sm:w-5 sm:h-5 flex items-center justify-center">
                {{ notifications.length }}
              </span>
            </button>
            <button @click="showLogoutConfirmation" 
                    class="flex items-center space-x-1 sm:space-x-2 px-3 py-2 text-gray-600 hover:text-red-600 transition-colors rounded-lg hover:bg-red-50">
              <i class="fas fa-sign-out-alt text-sm sm:text-base"></i>
              <span class="font-medium text-sm sm:text-base hidden sm:inline">Logout</span>
            </button>
            <RouterLink to="/photographers" 
                        class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white px-3 sm:px-6 text-sm sm:text-base">
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
          <nav class="flex overflow-x-auto scrollbar-hide px-4 sm:px-6" style="scrollbar-width: none; -ms-overflow-style: none;">
            <button
              v-for="tab in tabs"
              :key="tab.id"
              @click="activeTab = tab.id"
              :data-tab="tab.id"
              :class="[
                'py-3 sm:py-4 px-2 sm:px-1 border-b-2 font-medium text-xs sm:text-sm transition-colors whitespace-nowrap flex-shrink-0 mr-4 sm:mr-8',
                activeTab === tab.id
                  ? 'border-[#7b1e3a] text-[#7b1e3a]'
                  : 'border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300'
              ]"
            >
              <i :class="tab.icon" class="mr-1 sm:mr-2"></i>
              <span class="hidden sm:inline">{{ tab.name }}</span>
              <span class="sm:hidden">{{ tab.name.split(' ')[0] }}</span>
              <span v-if="tab.count" class="ml-1 sm:ml-2 bg-gray-100 text-gray-600 px-1.5 sm:px-2 py-0.5 sm:py-1 rounded-full text-xs">
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
              <select v-model="bookingFilter" 
                      class="border border-gray-300 rounded-lg px-3 py-2 text-sm bg-white text-gray-900 focus:border-[#7b1e3a] focus:ring-1 focus:ring-[#7b1e3a]">
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
           class="absolute top-16 right-2 sm:right-6 w-80 sm:w-96 max-w-[calc(100vw-1rem)] bg-white rounded-lg shadow-xl border border-gray-200 max-h-96 overflow-y-auto">
        <div class="p-3 sm:p-4 border-b border-gray-200">
          <h3 class="font-semibold text-gray-900">Notifications</h3>
        </div>
        <div v-if="notifications.length === 0" class="p-4 text-center text-gray-500">
          No new notifications
        </div>
        <div v-else>
          <div v-for="notification in notifications" :key="notification.id" 
               class="p-3 sm:p-4 border-b border-gray-100 hover:bg-gray-50">
            <div class="flex items-start space-x-2 sm:space-x-3">
              <div :class="[
                'w-2 h-2 rounded-full mt-2 flex-shrink-0',
                notification.type === 'booking' ? 'bg-blue-500' :
                notification.type === 'photos' ? 'bg-green-500' :
                'bg-yellow-500'
              ]"></div>
              <div class="flex-1 min-w-0">
                <p class="text-sm font-medium text-gray-900 truncate">{{ notification.title }}</p>
                <p class="text-sm text-gray-600 line-clamp-2">{{ notification.message }}</p>
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
         class="fixed inset-0 z-50 bg-black bg-opacity-75 flex items-center justify-center p-2 sm:p-4">
      <div @click.stop 
           class="bg-white rounded-xl max-w-6xl w-full max-h-[95vh] sm:max-h-[90vh] overflow-hidden">
        <!-- Modal Header -->
        <div class="flex flex-col sm:flex-row sm:items-center justify-between p-4 sm:p-6 border-b border-gray-200">
          <div class="mb-3 sm:mb-0">
            <h2 class="text-lg sm:text-2xl font-bold text-gray-900">{{ selectedAlbum.title }}</h2>
            <div class="flex flex-col sm:flex-row sm:items-center space-y-2 sm:space-y-0 sm:space-x-4 mt-1">
              <p class="text-gray-600 text-sm sm:text-base">{{ selectedAlbum.photographer }} • {{ selectedAlbum.date }}</p>
              <span :class="[
                'inline-block px-2 sm:px-3 py-1 rounded-full text-xs font-medium self-start',
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
                  class="absolute top-4 right-4 sm:relative sm:top-auto sm:right-auto p-2 text-gray-400 hover:text-gray-600 transition-colors">
            <i class="fas fa-times text-lg sm:text-xl"></i>
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
              <button @click="downloadAllPhotos" 
                      :disabled="isLoading"
                      class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white disabled:opacity-50 disabled:cursor-not-allowed">
                <i v-if="isLoading && loadingMessage === 'downloading'" class="fas fa-spinner fa-spin mr-2"></i>
                <i v-else class="fas fa-download mr-2"></i>
                {{ isLoading && loadingMessage === 'downloading' ? 'Downloading...' : `Download All (${selectedAlbum.photos.length})` }}
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
                        :disabled="selectedPhotos.length === 0 || isLoading"
                        :class="[
                          'btn text-white disabled:opacity-50 disabled:cursor-not-allowed',
                          selectedPhotos.length > 0 && !isLoading
                            ? 'bg-[#7b1e3a] hover:bg-[#5c162c]' 
                            : 'bg-gray-400 cursor-not-allowed'
                        ]">
                  <i v-if="isLoading && loadingMessage === 'requesting'" class="fas fa-spinner fa-spin mr-2"></i>
                  <i v-else class="fas fa-edit mr-2"></i>
                  {{ isLoading && loadingMessage === 'requesting' ? 'Sending Request...' : `Request Editing (${selectedPhotos.length})` }}
                </button>
                <button v-if="selectedPhotos.length > 0" 
                        @click="showDeleteConfirmation"
                        class="btn btn-outline border-red-300 text-red-700 hover:bg-red-50">
                  <i class="fas fa-trash mr-2"></i>
                  Remove Selected
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
                  <label class="flex items-center cursor-pointer" :for="`photo-${photo.id}`">
                    <input :id="`photo-${photo.id}`"
                           type="checkbox" 
                           v-model="photo.selected" 
                           @change="updateSelectedPhotos"
                           :aria-label="`Select ${photo.filename} for editing`"
                           class="rounded border-gray-300 text-[#7b1e3a] focus:ring-[#7b1e3a] shadow-lg" />
                    <span class="sr-only">Select {{ photo.filename }} for editing</span>
                  </label>
                </div>

                <!-- Selected Badge -->
                <div v-if="photo.selected" 
                     class="absolute top-2 right-2 bg-[#7b1e3a] text-white p-1 rounded-full"
                     aria-label="Photo selected">
                  <i class="fas fa-check text-xs" aria-hidden="true"></i>
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

    <!-- Onboarding Overlay -->
    <div v-if="showOnboarding" 
         class="fixed inset-0 z-50 bg-black bg-opacity-50">
      <div class="absolute inset-0 flex items-center justify-center p-3 sm:p-4">
        <div class="bg-white rounded-xl max-w-lg w-full p-4 sm:p-6 shadow-xl max-h-[90vh] overflow-y-auto">
          <div class="flex flex-col sm:flex-row sm:items-center justify-between mb-4">
            <div class="flex items-center mb-3 sm:mb-0">
              <div class="w-8 h-8 bg-[#7b1e3a] rounded-full flex items-center justify-center text-white font-bold mr-3 flex-shrink-0">
                {{ onboardingStep + 1 }}
              </div>
              <div class="min-w-0">
                <h3 class="text-base sm:text-lg font-semibold text-gray-900">{{ onboardingSteps[onboardingStep].title }}</h3>
                <p class="text-xs sm:text-sm text-gray-600">Step {{ onboardingStep + 1 }} of {{ onboardingSteps.length }}</p>
              </div>
            </div>
            <button @click="skipOnboarding" 
                    class="absolute top-4 right-4 sm:relative sm:top-auto sm:right-auto text-gray-400 hover:text-gray-600 transition-colors"
                    aria-label="Skip tour">
              <i class="fas fa-times text-lg"></i>
            </button>
          </div>
          
          <p class="text-gray-600 mb-4 sm:mb-6 text-sm sm:text-base">
            {{ onboardingSteps[onboardingStep].description }}
          </p>
          
          <!-- Progress Bar -->
          <div class="w-full bg-gray-200 rounded-full h-2 mb-4 sm:mb-6">
            <div class="bg-[#7b1e3a] h-2 rounded-full transition-all duration-300" 
                 :style="`width: ${((onboardingStep + 1) / onboardingSteps.length) * 100}%`"></div>
          </div>
          
          <div class="flex flex-col sm:flex-row justify-between space-y-3 sm:space-y-0">
            <button v-if="onboardingStep > 0" 
                    @click="previousStep" 
                    class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50 text-sm sm:text-base order-2 sm:order-1">
              <i class="fas fa-arrow-left mr-1 sm:mr-2"></i>
              Previous
            </button>
            <div v-else class="hidden sm:block"></div>
            
            <div class="flex flex-col sm:flex-row space-y-2 sm:space-y-0 sm:space-x-3 order-1 sm:order-2">
              <button @click="skipOnboarding" 
                      class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50 text-sm sm:text-base">
                Skip Tour
              </button>
              <button @click="nextStep" 
                      class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white text-sm sm:text-base">
                {{ onboardingStep === onboardingSteps.length - 1 ? 'Get Started' : 'Next' }}
                <i v-if="onboardingStep < onboardingSteps.length - 1" class="fas fa-arrow-right ml-1 sm:ml-2"></i>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Error Dialog -->
    <div v-if="hasError" 
         @click="dismissError"
         class="fixed inset-0 z-50 bg-black bg-opacity-50 flex items-center justify-center p-4">
      <div @click.stop 
           class="bg-white rounded-xl max-w-md w-full p-6 shadow-xl border-l-4 border-red-500">
        <div class="flex items-center mb-4">
          <div class="w-12 h-12 bg-red-100 rounded-full flex items-center justify-center mr-4">
            <i class="fas fa-exclamation-triangle text-red-600 text-xl"></i>
          </div>
          <div>
            <h3 class="text-lg font-semibold text-gray-900">Something went wrong</h3>
            <p class="text-sm text-gray-600">We encountered an error</p>
          </div>
        </div>
        <p class="text-gray-600 mb-6">
          {{ errorMessage || 'An unexpected error occurred. Please try again or contact support if the problem persists.' }}
        </p>
        <div class="flex justify-end space-x-4">
          <button @click="dismissError" 
                  class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50">
            Dismiss
          </button>
          <button v-if="retryAction" 
                  @click="handleRetry" 
                  class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
            Try Again
          </button>
        </div>
      </div>
    </div>

    <!-- Confirmation Dialog -->
    <div v-if="showDeleteConfirm || showLogoutConfirm" 
         @click="cancelConfirmation"
         class="fixed inset-0 z-50 bg-black bg-opacity-50 flex items-center justify-center p-4">
      <div @click.stop 
           class="bg-white rounded-xl max-w-md w-full p-6 shadow-xl">
        <!-- Delete Confirmation -->
        <div v-if="showDeleteConfirm">
          <div class="flex items-center mb-4">
            <div class="w-12 h-12 bg-red-100 rounded-full flex items-center justify-center mr-4">
              <i class="fas fa-trash text-red-600 text-xl"></i>
            </div>
            <div>
              <h3 class="text-lg font-semibold text-gray-900">Delete Selected Photos?</h3>
              <p class="text-sm text-gray-600">You have {{ selectedPhotos.length }} photo(s) selected</p>
            </div>
          </div>
          <p class="text-gray-600 mb-6">
            This will remove the selected photos from your edit request. This action cannot be undone.
          </p>
          <div class="flex justify-end space-x-4">
            <button @click="cancelConfirmation" 
                    class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50">
              Cancel
            </button>
            <button @click="confirmDeleteAction" 
                    :disabled="isLoading"
                    class="btn bg-red-600 hover:bg-red-700 text-white disabled:opacity-50">
              <i v-if="isLoading" class="fas fa-spinner fa-spin mr-2"></i>
              Delete Photos
            </button>
          </div>
        </div>

        <!-- Logout Confirmation -->
        <div v-if="showLogoutConfirm">
          <div class="flex items-center mb-4">
            <div class="w-12 h-12 bg-yellow-100 rounded-full flex items-center justify-center mr-4">
              <i class="fas fa-sign-out-alt text-yellow-600 text-xl"></i>
            </div>
            <div>
              <h3 class="text-lg font-semibold text-gray-900">Sign Out?</h3>
              <p class="text-sm text-gray-600">You will be redirected to the home page</p>
            </div>
          </div>
          <p class="text-gray-600 mb-6">
            Are you sure you want to sign out? Any unsaved changes will be lost.
          </p>
          <div class="flex justify-end space-x-4">
            <button @click="cancelConfirmation" 
                    class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50">
              Cancel
            </button>
            <button @click="confirmLogoutAction" 
                    class="btn bg-yellow-600 hover:bg-yellow-700 text-white">
              Sign Out
            </button>
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
import { useAuth } from '@/composables/useAuth'
import { useSupabase } from '@/composables/useSupabase'
import 'vue3-toastify/dist/index.css'

const router = useRouter()
const { user, loading: authLoading } = useAuth()
const { fetchData, insertData, updateData, loading: dbLoading, error } = useSupabase()
const activeTab = ref('bookings')
const bookingFilter = ref('all')
const showNotifications = ref(false)
const showAlbumViewer = ref(false)
const selectedAlbum = ref(null)
const albumViewerTab = ref('edited')
const selectedPhotos = ref([])
const selectAll = ref(false)
const isLoading = ref(false)
const isUploading = ref(false)
const uploadProgress = ref(0)
const loadingMessage = ref('')
const showDeleteConfirm = ref(false)
const showLogoutConfirm = ref(false)
const confirmAction = ref(null)
const confirmData = ref(null)
const hasError = ref(false)
const errorMessage = ref('')
const retryAction = ref(null)
const showOnboarding = ref(false)
const onboardingStep = ref(0)
const onboardingSteps = ref([
  {
    title: 'Welcome to Your Dashboard!',
    description: 'This is your personal space to manage all your photography bookings and photos.',
    target: '.dashboard-header',
    position: 'bottom'
  },
  {
    title: 'View Your Bookings',
    description: 'Here you can see all your upcoming and past photography sessions.',
    target: '[data-tab="bookings"]',
    position: 'bottom'
  },
  {
    title: 'Access Your Photos',
    description: 'Browse and download your photos, or request edits from photographers.',
    target: '[data-tab="photos"]',
    position: 'bottom'
  },
  {
    title: 'Manage Your Profile',
    description: 'Update your personal information and preferences.',
    target: '[data-tab="profile"]',
    position: 'bottom'
  }
])

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

const bookings = ref([])
const isLoadingBookings = ref(false)

const photoAlbums = ref([])
const isLoadingPhotos = ref(false)

const notifications = ref([])
const isLoadingNotifications = ref(false)

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

const requestEditing = async () => {
  if (selectedPhotos.value.length === 0) return
  
  isLoading.value = true
  loadingMessage.value = 'requesting'
  
  try {
    // Simulate API call
    await new Promise((resolve, reject) => {
      setTimeout(() => {
        // Simulate random failure for demo
        if (Math.random() > 0.8) {
          reject(new Error('Network connection failed'))
        } else {
          resolve()
        }
      }, 2000)
    })
    
    const photoNames = selectedPhotos.value.map(photo => photo.filename).join(', ')
    toast.success(`Edit request sent for ${selectedPhotos.value.length} photo(s) to ${selectedAlbum.value.photographer}`)
    
    // Reset selections
    selectedAlbum.value.photos.forEach(photo => {
      photo.selected = false
    })
    updateSelectedPhotos()
  } catch (error) {
    console.error('Edit request failed:', error)
    showError(
      'Failed to send edit request. Please check your internet connection and try again.',
      () => requestEditing()
    )
  } finally {
    isLoading.value = false
    loadingMessage.value = ''
  }
}

const downloadPhoto = async (photo) => {
  isLoading.value = true
  loadingMessage.value = 'downloading'
  
  try {
    // Simulate download
    await new Promise(resolve => setTimeout(resolve, 1500))
    toast.success(`Downloaded ${photo.filename}`)
  } catch (error) {
    toast.error('Download failed. Please try again.')
  } finally {
    isLoading.value = false
    loadingMessage.value = ''
  }
}

const downloadAllPhotos = async (album = null) => {
  const targetAlbum = album || selectedAlbum.value
  if (targetAlbum && targetAlbum.status === 'edited') {
    isLoading.value = true
    loadingMessage.value = 'downloading'
    
    try {
      // Simulate zip download
      await new Promise(resolve => setTimeout(resolve, 3000))
      toast.success(`Downloaded all ${targetAlbum.photos.length} photos from ${targetAlbum.title}`)
    } catch (error) {
      toast.error('Download failed. Please try again.')
    } finally {
      isLoading.value = false
      loadingMessage.value = ''
    }
  }
}

const viewFullscreen = (photo) => {
  toast.info('Fullscreen viewer coming soon!')
}

// Onboarding functions
const skipOnboarding = () => {
  showOnboarding.value = false
  localStorage.setItem('dashboard-onboarding-complete', 'true')
}

const nextStep = () => {
  if (onboardingStep.value < onboardingSteps.value.length - 1) {
    onboardingStep.value++
  } else {
    completeOnboarding()
  }
}

const previousStep = () => {
  if (onboardingStep.value > 0) {
    onboardingStep.value--
  }
}

const completeOnboarding = () => {
  showOnboarding.value = false
  localStorage.setItem('dashboard-onboarding-complete', 'true')
  toast.success('Welcome! You\'re all set to start managing your albums.')
}

const logout = () => {
  localStorage.removeItem('user')
  user.value = null
  
  // Emit custom event to notify navbar of logout
  window.dispatchEvent(new CustomEvent('userLogout'))
  
  toast.success('Logged out successfully!')
  router.push('/')
}

const showLogoutConfirmation = () => {
  showLogoutConfirm.value = true
}

const confirmLogoutAction = () => {
  showLogoutConfirm.value = false
  logout()
}

const showDeleteConfirmation = () => {
  if (selectedPhotos.value.length === 0) {
    toast.error('No photos selected')
    return
  }
  showDeleteConfirm.value = true
}

const confirmDeleteAction = async () => {
  isLoading.value = true
  
  try {
    // Simulate deletion
    await new Promise(resolve => setTimeout(resolve, 1500))
    
    selectedAlbum.value.photos.forEach(photo => {
      photo.selected = false
    })
    updateSelectedPhotos()
    
    toast.success('Selected photos removed from edit request')
  } catch (error) {
    toast.error('Failed to remove photos. Please try again.')
  } finally {
    isLoading.value = false
    showDeleteConfirm.value = false
  }
}

const cancelConfirmation = () => {
  showDeleteConfirm.value = false
  showLogoutConfirm.value = false
  confirmAction.value = null
  confirmData.value = null
}

const showError = (message, retry = null) => {
  hasError.value = true
  errorMessage.value = message
  retryAction.value = retry
}

const dismissError = () => {
  hasError.value = false
  errorMessage.value = ''
  retryAction.value = null
}

const handleRetry = () => {
  if (retryAction.value) {
    dismissError()
    retryAction.value()
  }
}

// Load user's bookings from database
const loadBookings = async () => {
  if (!user.value) return
  
  isLoadingBookings.value = true
  try {
    const sessionsData = await fetchData('photo_sessions', { client_id: user.value.id })
    if (sessionsData) {
      // Fetch photographer details for each session
      const bookingsWithDetails = await Promise.all(
        sessionsData.map(async (session) => {
          const photographerData = await fetchData('profiles', { id: session.photographer_id })
          const photographer = photographerData?.[0]
          
          return {
            id: session.id,
            photographer: {
              name: photographer?.full_name || 'Unknown',
              studio: photographer?.studio_name || 'Studio',
              image: photographer?.avatar_url || '/portrait.jpg'
            },
            package: session.session_type,
            date: new Date(session.session_date).toLocaleDateString(),
            time: session.session_time,
            status: session.status,
            location: session.location,
            price: session.price,
            notes: session.client_notes
          }
        })
      )
      bookings.value = bookingsWithDetails
    }
  } catch (err) {
    console.error('Error loading bookings:', err)
    toast.error('Failed to load bookings')
  } finally {
    isLoadingBookings.value = false
  }
}

// Load user's photo albums from database
const loadPhotoAlbums = async () => {
  if (!user.value) return
  
  isLoadingPhotos.value = true
  try {
    const photosData = await fetchData('photos', { client_id: user.value.id })
    if (photosData) {
      // Group photos by session
      const albumsMap = new Map()
      
      for (const photo of photosData) {
        if (!albumsMap.has(photo.session_id)) {
          // Get session and photographer details
          const sessionData = await fetchData('photo_sessions', { id: photo.session_id })
          const session = sessionData?.[0]
          const photographerData = await fetchData('profiles', { id: photo.photographer_id })
          const photographer = photographerData?.[0]
          
          albumsMap.set(photo.session_id, {
            id: photo.session_id,
            title: `${session?.session_type || 'Photo'} Session`,
            photographer: photographer?.full_name || 'Unknown',
            coverPhoto: photo.thumbnail_url || photo.file_url,
            date: new Date(session?.session_date || photo.created_at).toLocaleDateString(),
            status: photo.is_public ? 'edited' : 'unedited',
            photos: []
          })
        }
        
        albumsMap.get(photo.session_id).photos.push({
          id: photo.id,
          url: photo.file_url,
          filename: photo.title || `photo_${photo.id}.jpg`,
          selected: false
        })
      }
      
      photoAlbums.value = Array.from(albumsMap.values())
    }
  } catch (err) {
    console.error('Error loading photo albums:', err)
    toast.error('Failed to load photo albums')
  } finally {
    isLoadingPhotos.value = false
  }
}

// Load user's notifications from database
const loadNotifications = async () => {
  if (!user.value) return
  
  isLoadingNotifications.value = true
  try {
    const notificationsData = await fetchData('notifications', { 
      user_id: user.value.id,
      is_read: false 
    })
    if (notificationsData) {
      notifications.value = notificationsData.map(notif => ({
        id: notif.id,
        type: notif.type,
        title: notif.title,
        message: notif.message,
        time: new Date(notif.created_at).toLocaleString()
      }))
    }
  } catch (err) {
    console.error('Error loading notifications:', err)
  } finally {
    isLoadingNotifications.value = false
  }
}

// Create a new booking
const createBooking = async (bookingData) => {
  try {
    const newBooking = await insertData('photo_sessions', {
      photographer_id: bookingData.photographerId,
      client_id: user.value.id,
      session_type: bookingData.sessionType,
      session_date: bookingData.date,
      session_time: bookingData.time,
      duration_hours: bookingData.duration || 2,
      location: bookingData.location,
      price: bookingData.price,
      client_notes: bookingData.notes
    })
    
    if (newBooking) {
      toast.success('Booking created successfully!')
      await loadBookings() // Refresh bookings
      return newBooking
    }
  } catch (err) {
    console.error('Error creating booking:', err)
    toast.error('Failed to create booking')
    return null
  }
}

// Update user profile
const updateProfile = async () => {
  if (!user.value) return
  
  try {
    const updatedProfile = await updateData('profiles', user.value.id, {
      full_name: profileForm.value.name,
      phone: profileForm.value.phone,
      preferred_location: profileForm.value.preferredContact
    })
    
    if (updatedProfile) {
      toast.success('Profile updated successfully!')
    } else if (error.value) {
      toast.error(error.value)
    }
  } catch (err) {
    console.error('Error updating profile:', err)
    toast.error('Failed to update profile')
  }
}

onMounted(async () => {
  // Wait for auth to load
  if (authLoading.value) {
    // Wait a bit for auth to resolve
    await new Promise(resolve => setTimeout(resolve, 1000))
  }
  
  if (!user.value) {
    router.push('/login?redirect=/dashboard')
    return
  }
  
  // Check if user is a photographer and redirect accordingly
  let currentUser = user.value
  
  // If no user from composable, try localStorage
  if (!currentUser) {
    const supabaseSession = localStorage.getItem('sb-ywlujqnutcqlghucyfwx-auth-token')
    if (supabaseSession) {
      try {
        const sessionData = JSON.parse(supabaseSession)
        currentUser = sessionData.user
      } catch (e) {
        console.warn('Could not parse localStorage session data')
      }
    }
  }
  
  // Check role from user metadata
  if (currentUser?.user_metadata?.role === 'photographer') {
    // Redirect photographers to their dashboard
    router.push('/photographer-dashboard')
    return
  }
  
  // Set up profile form with user data
  profileForm.value = {
    name: user.value.user_metadata?.full_name || user.value.email?.split('@')[0] || '',
    email: user.value.email || '',
    phone: user.value.user_metadata?.phone || '',
    preferredContact: 'email'
  }
  
  // Load all dashboard data
  await Promise.all([
    loadBookings(),
    loadPhotoAlbums(),
    loadNotifications()
  ])
  
  // Check if onboarding should be shown
  const onboardingComplete = localStorage.getItem('dashboard-onboarding-complete')
  if (!onboardingComplete && bookings.value.length === 0) {
    setTimeout(() => {
      showOnboarding.value = true
    }, 2000) // Show after data loads
  }
})
</script>