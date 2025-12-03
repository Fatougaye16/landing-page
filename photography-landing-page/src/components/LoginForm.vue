<template>
  <div class="login-form bg-white p-8 rounded-lg shadow-md max-w-md mx-auto">
    <h2 class="text-2xl font-bold text-center mb-6">Sign In</h2>
    
    <form @submit.prevent="handleSignIn" class="space-y-4">
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
          placeholder="Enter your password"
        />
      </div>
      
      <button
        type="submit"
        :disabled="loading"
        class="w-full bg-blue-600 text-white py-2 px-4 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        <span v-if="loading">Signing in...</span>
        <span v-else>Sign In</span>
      </button>
    </form>
    
    <div v-if="errorMessage" class="mt-4 p-3 bg-red-100 border border-red-400 text-red-700 rounded">
      {{ errorMessage }}
    </div>
    
    <div class="mt-6 text-center">
      <p class="text-sm text-gray-600">
        Don't have an account?
        <router-link to="/register" class="text-blue-600 hover:text-blue-500">
          Sign up
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
const { signIn, loading } = useAuth()

const email = ref('')
const password = ref('')
const errorMessage = ref('')

const handleSignIn = async () => {
  errorMessage.value = ''
  
  const { error } = await signIn(email.value, password.value)
  
  if (error) {
    errorMessage.value = error.message
  } else {
    // Redirect to dashboard or home page after successful login
    router.push('/dashboard')
  }
}
</script>