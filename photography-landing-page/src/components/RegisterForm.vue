<template>
  <div class="register-form bg-white p-8 rounded-lg shadow-md max-w-md mx-auto">
    <h2 class="text-2xl font-bold text-center mb-6">Create Account</h2>
    
    <form @submit.prevent="handleSignUp" class="space-y-4">
      <div>
        <label for="name" class="block text-sm font-medium text-gray-700 mb-1">
          Full Name
        </label>
        <input
          id="name"
          v-model="name"
          type="text"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Enter your full name"
        />
      </div>
      
      <div>
        <label for="email" class="block text-sm font-medium text-gray-700 mb-1">
          Email
        </label>
        <input
          id="email"
          v-model="email"
          type="email"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Enter your email"
        />
      </div>
      
      <div>
        <label for="password" class="block text-sm font-medium text-gray-700 mb-1">
          Password
        </label>
        <input
          id="password"
          v-model="password"
          type="password"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          placeholder="Enter your password (min. 6 characters)"
        />
      </div>
      
      <div>
        <label for="userType" class="block text-sm font-medium text-gray-700 mb-1">
          I am a
        </label>
        <select
          id="userType"
          v-model="userType"
          required
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        >
          <option value="">Select user type</option>
          <option value="client">Client (Looking for photographers)</option>
          <option value="photographer">Photographer (Offering services)</option>
        </select>
      </div>
      
      <button
        type="submit"
        :disabled="loading"
        class="w-full bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        <span v-if="loading">Creating account...</span>
        <span v-else>Create Account</span>
      </button>
    </form>
    
    <div v-if="errorMessage" class="mt-4 p-3 bg-red-100 border border-red-400 text-red-700 rounded">
      {{ errorMessage }}
    </div>
    
    <div v-if="successMessage" class="mt-4 p-3 bg-green-100 border border-green-400 text-green-700 rounded">
      {{ successMessage }}
    </div>
    
    <div class="mt-6 text-center">
      <p class="text-sm text-gray-600">
        Already have an account?
        <router-link to="/login" class="text-blue-600 hover:text-blue-500">
          Sign in
        </router-link>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuth } from '@/composables/useAuth'

const router = useRouter()
const { signUp, loading } = useAuth()

const name = ref('')
const email = ref('')
const password = ref('')
const userType = ref('')
const errorMessage = ref('')
const successMessage = ref('')

const handleSignUp = async () => {
  errorMessage.value = ''
  successMessage.value = ''
  
  if (password.value.length < 6) {
    errorMessage.value = 'Password must be at least 6 characters long'
    return
  }
  
  const { error } = await signUp(email.value, password.value, {
    full_name: name.value,
    user_type: userType.value
  })
  
  if (error) {
    errorMessage.value = error.message
  } else {
    successMessage.value = 'Account created successfully! Please check your email to verify your account.'
    // Optionally redirect after a delay
    setTimeout(() => {
      router.push('/login')
    }, 3000)
  }
}
</script>