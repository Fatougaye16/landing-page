<template>
  <div class="user-profile bg-white rounded-lg shadow-md p-6">
    <h2 class="text-2xl font-bold mb-4">User Profile</h2>
    
    <!-- Loading state -->
    <div v-if="loading" class="flex justify-center items-center py-8">
      <div class="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
    </div>
    
    <!-- Error state -->
    <div v-else-if="error" class="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
      {{ error }}
    </div>
    
    <!-- Profile form -->
    <form v-else @submit.prevent="updateProfile" class="space-y-4">
      <div>
        <label for="fullName" class="block text-sm font-medium text-gray-700 mb-1">
          Full Name
        </label>
        <input
          id="fullName"
          v-model="profile.full_name"
          type="text"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Enter your full name"
        />
      </div>
      
      <div>
        <label for="phone" class="block text-sm font-medium text-gray-700 mb-1">
          Phone
        </label>
        <input
          id="phone"
          v-model="profile.phone"
          type="tel"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Enter your phone number"
        />
      </div>
      
      <div v-if="profile.role === 'photographer'">
        <label for="studioName" class="block text-sm font-medium text-gray-700 mb-1">
          Studio Name
        </label>
        <input
          id="studioName"
          v-model="profile.studio_name"
          type="text"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Enter your studio name"
        />
      </div>
      
      <div v-if="profile.role === 'photographer'">
        <label for="description" class="block text-sm font-medium text-gray-700 mb-1">
          Description
        </label>
        <textarea
          id="description"
          v-model="profile.description"
          rows="4"
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Tell us about your photography services..."
        ></textarea>
      </div>
      
      <button
        type="submit"
        :disabled="isUpdating"
        class="bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50"
      >
        <span v-if="isUpdating">Updating...</span>
        <span v-else>Update Profile</span>
      </button>
    </form>
    
    <!-- Logout button -->
    <div class="mt-6 pt-6 border-t">
      <button
        @click="handleLogout"
        :disabled="isLoggingOut"
        class="bg-red-600 text-white py-2 px-4 rounded-md hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 disabled:opacity-50"
      >
        <span v-if="isLoggingOut">Signing out...</span>
        <span v-else>Sign Out</span>
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useAuth } from '@/composables/useAuth'
import { useSupabase } from '@/composables/useSupabase'
import { toast } from 'vue3-toastify'

const router = useRouter()
const { user, signOut, loading: authLoading } = useAuth()
const { loading: dbLoading, error, updateData } = useSupabase()

const profile = ref({
  full_name: '',
  phone: '',
  role: '',
  studio_name: '',
  description: ''
})

const isUpdating = ref(false)
const isLoggingOut = ref(false)
const loading = ref(true)

// Computed property to combine loading states
const isLoading = computed(() => authLoading.value || dbLoading.value || loading.value)

onMounted(async () => {
  if (user.value) {
    // Load user profile data
    profile.value = {
      full_name: user.value.user_metadata?.full_name || '',
      phone: user.value.user_metadata?.phone || '',
      role: user.value.user_metadata?.role || '',
      studio_name: user.value.user_metadata?.studio_name || '',
      description: user.value.user_metadata?.description || ''
    }
  }
  loading.value = false
})

const updateProfile = async () => {
  if (!user.value) return
  
  isUpdating.value = true
  
  try {
    // Update user profile in the profiles table
    const updatedProfile = await updateData('profiles', user.value.id, {
      full_name: profile.value.full_name,
      phone: profile.value.phone,
      studio_name: profile.value.studio_name,
      description: profile.value.description,
      updated_at: new Date().toISOString()
    })
    
    if (updatedProfile) {
      toast.success('Profile updated successfully!')
    } else if (error.value) {
      toast.error(error.value)
    }
  } catch (err) {
    toast.error('Failed to update profile')
  } finally {
    isUpdating.value = false
  }
}

const handleLogout = async () => {
  isLoggingOut.value = true
  
  try {
    const { error: logoutError } = await signOut()
    
    if (logoutError) {
      toast.error(logoutError.message)
    } else {
      toast.success('Successfully signed out')
      router.push('/')
    }
  } catch (err) {
    toast.error('Failed to sign out')
  } finally {
    isLoggingOut.value = false
  }
}
</script>