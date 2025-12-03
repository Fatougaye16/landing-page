<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Loading State -->
    <div v-if="isLoadingData || loading" class="flex items-center justify-center min-h-screen">
      <div class="flex flex-col items-center gap-4">
        <div class="loading loading-spinner loading-lg text-[#7b1e3a]"></div>
        <p class="text-gray-600">Loading your dashboard...</p>
      </div>
    </div>
    
    <!-- Main Dashboard Content -->
    <div v-else-if="photographer">
      <!-- Dashboard Header -->
      <div class="bg-white shadow-sm border-b border-gray-200 dashboard-header">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 py-4">
        <div class="flex flex-col sm:flex-row sm:items-center justify-between space-y-4 sm:space-y-0">
          <div class="flex items-center space-x-3 sm:space-x-4">
            <img :src="photographer?.avatar || '/man-lens.jpg'" 
                 class="w-10 h-10 sm:w-12 sm:h-12 rounded-full object-cover border-2 border-[#7b1e3a]" 
                 :alt="photographer?.name" />
            <div>
              <h1 class="text-lg sm:text-2xl font-bold text-gray-900">{{ photographer?.name }}'s Studio</h1>
              <p class="text-gray-600 text-sm sm:text-base">Manage your bookings and client galleries</p>
            </div>
          </div>
          <div class="flex items-center justify-between sm:justify-end space-x-3 sm:space-x-4">
            <button @click="showNotifications = !showNotifications" 
                    class="relative p-2 text-gray-600 hover:text-gray-900 transition-colors rounded-lg hover:bg-gray-100"
                    aria-label="View notifications">
              <i class="fas fa-bell text-lg sm:text-xl"></i>
              <span v-if="notifications.length > 0" 
                    class="absolute -top-1 -right-1 bg-red-500 text-white text-xs rounded-full w-4 h-4 sm:w-5 sm:h-5 flex items-center justify-center">
                {{ notifications.length }}
              </span>
            </button>
            <button @click="showLogoutConfirmation" 
                    class="flex items-center space-x-1 sm:space-x-2 px-3 py-2 text-gray-600 hover:text-red-600 transition-colors rounded-lg hover:bg-red-50"
                    aria-label="Logout">
              <i class="fas fa-sign-out-alt text-sm sm:text-base"></i>
              <span class="font-medium text-sm sm:text-base hidden sm:inline">Logout</span>
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
              <p class="text-2xl font-bold text-gray-900">{{ appointments?.length || 0 }}</p>
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
              <p class="text-2xl font-bold text-gray-900">{{ clientAlbums?.length || 0 }}</p>
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
              <p class="text-2xl font-bold text-gray-900">{{ thisWeekAppointments?.length || 0 }}</p>
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
                          class="btn btn-sm bg-green-600 hover:bg-green-700 text-white"
                          :aria-label="`Confirm appointment with ${appointment.client}`">
                    <i class="fas fa-check mr-1" aria-hidden="true"></i>
                    Confirm
                  </button>
                  <button v-if="appointment.status === 'pending'" 
                          @click="declineAppointment(appointment)"
                          class="btn btn-sm bg-red-600 hover:bg-red-700 text-white"
                          :aria-label="`Decline appointment with ${appointment.client}`">
                    <i class="fas fa-times mr-1" aria-hidden="true"></i>
                    Decline
                  </button>
                  <button v-if="appointment.status === 'confirmed'" 
                          @click="rescheduleAppointment(appointment)"
                          class="btn btn-sm btn-outline border-gray-300 text-gray-700 hover:bg-gray-50"
                          :aria-label="`Reschedule appointment with ${appointment.client}`">
                    <i class="fas fa-calendar-alt mr-1" aria-hidden="true"></i>
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
    </div>
    
    <!-- Error State -->
    <div v-else class="flex items-center justify-center min-h-screen">
      <div class="text-center">
        <div class="text-6xl mb-4">📷</div>
        <h2 class="text-2xl font-bold text-gray-800 mb-2">Access Denied</h2>
        <p class="text-gray-600 mb-4">You need to be logged in as a photographer to access this dashboard.</p>
        <button @click="router.push('/login?redirect=/photographer-dashboard')" 
                class="btn btn-primary">
          Login as Photographer
        </button>
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

    <!-- Logout Confirmation Dialog -->
    <div v-if="showLogoutConfirm" 
         @click="showLogoutConfirm = false"
         class="fixed inset-0 z-50 bg-black bg-opacity-50 flex items-center justify-center p-3 sm:p-4">
      <div @click.stop 
           class="bg-white rounded-xl max-w-md w-full p-4 sm:p-6 shadow-xl">
        <div class="flex items-center mb-4">
          <div class="w-10 h-10 sm:w-12 sm:h-12 bg-yellow-100 rounded-full flex items-center justify-center mr-3 sm:mr-4">
            <i class="fas fa-sign-out-alt text-yellow-600 text-lg sm:text-xl"></i>
          </div>
          <div>
            <h3 class="text-base sm:text-lg font-semibold text-gray-900">Confirm Logout</h3>
            <p class="text-xs sm:text-sm text-gray-600">Are you sure you want to logout?</p>
          </div>
        </div>
        <p class="text-gray-600 mb-4 sm:mb-6 text-sm sm:text-base">
          You will need to login again to access your photographer dashboard.
        </p>
        <div class="flex flex-col sm:flex-row justify-end space-y-2 sm:space-y-0 sm:space-x-4">
          <button @click="showLogoutConfirm = false" 
                  class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50 text-sm sm:text-base">
            Cancel
          </button>
          <button @click="logout" 
                  class="btn bg-red-600 hover:bg-red-700 text-white text-sm sm:text-base">
            <i class="fas fa-sign-out-alt mr-1 sm:mr-2"></i>
            Logout
          </button>
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
         class="fixed inset-0 z-50 bg-black bg-opacity-50 flex items-center justify-center p-3 sm:p-4">
      <div @click.stop 
           class="bg-white rounded-xl max-w-md w-full p-4 sm:p-6 shadow-xl border-l-4 border-red-500">
        <div class="flex items-center mb-4">
          <div class="w-10 h-10 sm:w-12 sm:h-12 bg-red-100 rounded-full flex items-center justify-center mr-3 sm:mr-4">
            <i class="fas fa-exclamation-triangle text-red-600 text-lg sm:text-xl"></i>
          </div>
          <div>
            <h3 class="text-base sm:text-lg font-semibold text-gray-900">Something went wrong</h3>
            <p class="text-xs sm:text-sm text-gray-600">We encountered an error</p>
          </div>
        </div>
        <p class="text-gray-600 mb-4 sm:mb-6 text-sm sm:text-base">
          {{ errorMessage || 'An unexpected error occurred. Please try again or contact support if the problem persists.' }}
        </p>
        <div class="flex flex-col sm:flex-row justify-end space-y-2 sm:space-y-0 sm:space-x-4">
          <button @click="dismissError" 
                  class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-50 text-sm sm:text-base">
            Dismiss
          </button>
          <button v-if="retryAction" 
                  @click="handleRetry" 
                  class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white text-sm sm:text-base">
            Try Again
          </button>
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
import { useBooking } from '@/composables/useBooking'
import 'vue3-toastify/dist/index.css'

const router = useRouter()

// Authentication and API
const { user, signOut } = useAuth()
const { fetchData, insertData, updateData, deleteData, loading, error } = useSupabase()
const { loadPhotographerBookings, updateBookingStatus } = useBooking()

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

// Loading states and UI improvements
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
    title: 'Welcome to Your Photography Studio!',
    description: 'This is your professional dashboard to manage clients, appointments, and photo galleries.',
    target: '.dashboard-header',
    position: 'bottom'
  },
  {
    title: 'Manage Your Appointments',
    description: 'View and manage all your photography sessions and client bookings.',
    target: '[data-tab="appointments"]',
    position: 'bottom'
  },
  {
    title: 'Client Photo Albums',
    description: 'Upload and organize client photos, create galleries for client review.',
    target: '[data-tab="albums"]',
    position: 'bottom'
  },
  {
    title: 'Your Studio Profile',
    description: 'Update your studio information, pricing, and portfolio details.',
    target: '[data-tab="profile"]',
    position: 'bottom'
  }
])

const tabs = computed(() => [
  { id: 'appointments', name: 'Appointments', icon: 'fas fa-calendar-alt', count: appointments.value?.length || 0 },
  { id: 'albums', name: 'Client Albums', icon: 'fas fa-images', count: clientAlbums.value?.length || 0 },
  { id: 'profile', name: 'Studio Profile', icon: 'fas fa-user-circle', count: null }
])

// Reactive data
const appointments = ref([])
const isLoadingData = ref(false)

// Load appointments from Supabase
const loadAppointments = async () => {
  if (!photographer.value?.id) return
  
  isLoadingData.value = true
  try {
    const bookings = await loadPhotographerBookings(photographer.value.id)
    
    // Transform bookings to match component structure
    appointments.value = bookings.map(booking => {
      const clientProfile = booking.client || {}
      return {
        id: booking.id,
        client: {
          name: clientProfile.full_name || clientProfile.email?.split('@')[0] || 'Client',
          email: clientProfile.email || 'No email',
          avatar: clientProfile.avatar_url || '/portrait.jpg'
        },
        package: booking.session_type || 'Standard',
        date: booking.session_date,
        time: booking.session_time || 'TBD',
        status: booking.status,
        location: booking.location,
        price: booking.price,
        notes: booking.client_notes
      }
    })
  } catch (err) {
    console.error('Error loading appointments:', err)
    showError('Failed to load appointments', loadAppointments)
  } finally {
    isLoadingData.value = false
  }
}

const clientAlbums = ref([])

// Load photo albums from Supabase
const loadClientAlbums = async () => {
  if (!photographer.value?.id) return
  
  try {
    // Fetch photo albums for this photographer
    const albums = await fetchData('photo_albums', {
      photographer_id: photographer.value.id
    })
    
    if (albums) {
      // Transform albums data
      clientAlbums.value = await Promise.all(albums.map(async (album) => {
        // Get photos for this album
        const photos = await fetchData('photos', {
          album_id: album.id
        }) || []
        
        return {
          id: album.id,
          title: album.title || `${album.client_name || 'Client'} Session`,
          client: album.client_name || 'Client',
          coverPhoto: photos[0]?.image_url || '/shot-1.jpg',
          status: album.status || 'unedited',
          editRequests: album.edit_requests || 0,
          date: new Date(album.created_at).toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short', 
            day: 'numeric'
          }),
          photos: photos.map(photo => ({
            id: photo.id,
            url: photo.image_url,
            filename: photo.filename || 'photo.jpg',
            selected: photo.selected || false
          }))
        }
      }))
    }
  } catch (err) {
    console.error('Error loading client albums:', err)
    showError('Failed to load photo albums', loadClientAlbums)
  }
}

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
  if (!appointments.value || appointments.value.length === 0) return []
  if (appointmentFilter.value === 'all') return appointments.value
  return appointments.value.filter(appointment => appointment.status === appointmentFilter.value)
})

const thisWeekAppointments = computed(() => {
  if (!appointments.value || appointments.value.length === 0) return []
  
  const now = new Date()
  const startOfWeek = new Date(now.setDate(now.getDate() - now.getDay()))
  const endOfWeek = new Date(now.setDate(startOfWeek.getDate() + 6))
  
  return appointments.value.filter(appointment => {
    const appointmentDate = new Date(appointment.date)
    return appointmentDate >= startOfWeek && appointmentDate <= endOfWeek
  })
})

const confirmAppointment = async (appointment) => {
  try {
    await updateBookingStatus(appointment.id, 'confirmed')
    appointment.status = 'confirmed'
    toast.success(`Appointment with ${appointment.client.name} confirmed!`)
  } catch (err) {
    console.error('Error confirming appointment:', err)
    toast.error('Failed to confirm appointment')
  }
}

const declineAppointment = async (appointment) => {
  try {
    await updateBookingStatus(appointment.id, 'cancelled')
    appointment.status = 'cancelled'
    toast.info(`Appointment with ${appointment.client.name} declined.`)
  } catch (err) {
    console.error('Error declining appointment:', err)
    toast.error('Failed to decline appointment')
  }
}

const markAsCompleted = async (appointment) => {
  try {
    await updateBookingStatus(appointment.id, 'completed')
    appointment.status = 'completed'
    toast.success(`Session with ${appointment.client.name} marked as completed!`)
  } catch (err) {
    console.error('Error marking appointment as completed:', err)
    toast.error('Failed to update appointment status')
  }
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

const createAlbum = async () => {
  if (!photographer.value?.id) {
    toast.error('Photographer not found')
    return
  }
  
  try {
    const albumData = {
      title: newAlbum.value.title,
      client_name: newAlbum.value.client,
      photographer_id: photographer.value.id,
      status: newAlbum.value.status,
      edit_requests: 0
    }
    
    const createdAlbum = await insertData('photo_albums', albumData)
    
    if (createdAlbum) {
      // Add to local state
      const album = {
        id: createdAlbum.id,
        title: createdAlbum.title,
        client: createdAlbum.client_name,
        status: createdAlbum.status,
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
  } catch (err) {
    console.error('Error creating album:', err)
    toast.error('Failed to create album')
  }
}

// UI improvement functions
const showConfirmation = (message, action, data = null) => {
  confirmAction.value = action
  confirmData.value = data
  showDeleteConfirm.value = true
}

const confirmActionHandler = () => {
  if (confirmAction.value) {
    confirmAction.value(confirmData.value)
  }
  showDeleteConfirm.value = false
  confirmAction.value = null
  confirmData.value = null
}

const cancelConfirmation = () => {
  showDeleteConfirm.value = false
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

// Onboarding functions
const skipOnboarding = () => {
  showOnboarding.value = false
  localStorage.setItem('photographer-onboarding-complete', 'true')
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
  localStorage.setItem('photographer-onboarding-complete', 'true')
  toast.success('Welcome to your photography studio! You\'re ready to manage your business.')
}

const showLogoutConfirmation = () => {
  showLogoutConfirm.value = true
}

const logout = async () => {
  showLogoutConfirm.value = false
  
  try {
    await signOut()
    photographer.value = null
    
    // Emit custom event to notify navbar of logout
    window.dispatchEvent(new CustomEvent('userLogout'))
    
    toast.success('Logged out successfully!')
    router.push('/')
  } catch (err) {
    console.error('Error during logout:', err)
    toast.error('Logout failed, please try again')
  }
}

const updateProfile = async () => {
  if (!photographer.value?.id) {
    toast.error('Photographer profile not found')
    return
  }
  
  try {
    const profileData = {
      full_name: profileForm.value.name,
      studio_name: profileForm.value.studioName,
      phone: profileForm.value.phone,
      description: profileForm.value.description,
      experience: profileForm.value.experience,
      base_price: profileForm.value.basePrice,
      specialization: profileForm.value.specialization
    }
    
    // Update in database
    const updatedProfile = await updateData('profiles', photographer.value.id, profileData)
    
    if (updatedProfile) {
      // Update local photographer data
      photographer.value = { 
        ...photographer.value, 
        name: profileData.full_name,
        studioName: profileData.studio_name,
        phone: profileData.phone,
        description: profileData.description,
        experience: profileData.experience,
        basePrice: profileData.base_price,
        specialization: profileData.specialization
      }
      
      // Also update localStorage session data
      try {
        const sessionKey = 'sb-ywlujqnutcqlghucyfwx-auth-token'
        const sessionData = localStorage.getItem(sessionKey)
        if (sessionData) {
          const parsedSession = JSON.parse(sessionData)
          if (parsedSession.user && parsedSession.user.user_metadata) {
            parsedSession.user.user_metadata = {
              ...parsedSession.user.user_metadata,
              full_name: profileForm.value.name,
              studio_name: profileForm.value.studioName,
              phone: profileForm.value.phone,
              description: profileForm.value.description,
              experience: profileForm.value.experience,
              base_price: profileForm.value.basePrice,
              specialization: profileForm.value.specialization
            }
            localStorage.setItem(sessionKey, JSON.stringify(parsedSession))
          }
        }
      } catch (localStorageErr) {
        console.warn('Could not update localStorage session data:', localStorageErr)
      }
      
      toast.success('Studio profile updated successfully!')
    }
  } catch (err) {
    console.error('Error updating profile:', err)
    toast.error('Failed to update profile: ' + (err.message || 'Please try again.'))
  }
}

onMounted(async () => {
  initializeDashboard()
})

const initializeDashboard = async () => {
  isLoadingData.value = true
  
  try {
    // Get user data from localStorage or Supabase auth
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
    
    if (!currentUser) {
      console.warn('User not authenticated, redirecting to login')
      router.push('/login?redirect=/photographer-dashboard')
      return
    }
    
    // Check if user has photographer role
    const userRole = currentUser.user_metadata?.role
    if (userRole !== 'photographer') {
      toast.error('Access denied. You need photographer privileges to access this dashboard.')
      router.push('/dashboard')
      return
    }
    
    // Set up photographer data from user metadata
    photographer.value = {
      id: currentUser.id,
      name: currentUser.user_metadata?.full_name || 'Photographer',
      studioName: currentUser.user_metadata?.studio_name || 'Photography Studio',
      email: currentUser.email,
      phone: currentUser.user_metadata?.phone || '',
      description: currentUser.user_metadata?.description || '',
      experience: currentUser.user_metadata?.experience || 0,
      basePrice: currentUser.user_metadata?.base_price || 150,
      specialization: currentUser.user_metadata?.specialization || 'portrait',
      avatar: currentUser.user_metadata?.avatar || '/man-lens.jpg'
    }
    
    // Initialize profile form
    profileForm.value = {
      studioName: photographer.value.studioName,
      name: photographer.value.name,
      email: photographer.value.email,
      phone: photographer.value.phone,
      description: photographer.value.description,
      experience: photographer.value.experience,
      basePrice: photographer.value.basePrice,
      specialization: photographer.value.specialization
    }
    
    // Load dashboard data
    await Promise.all([
      loadAppointments(),
      loadClientAlbums()
    ])
    
    // Check if onboarding should be shown
    const onboardingComplete = localStorage.getItem('photographer-onboarding-complete')
    if (!onboardingComplete && photographer.value) {
      setTimeout(() => {
        showOnboarding.value = true
      }, 1000)
    }
  } catch (err) {
    console.error('Error loading photographer dashboard:', err)
    showError('Failed to load dashboard data. ' + (err.message || 'Please try again.'), async () => {
      await initializeDashboard()
    })
  } finally {
    isLoadingData.value = false
  }
}
</script>