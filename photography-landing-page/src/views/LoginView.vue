<template>
  <div class="min-h-screen bg-gray-50 flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <!-- Header -->
      <div class="text-center">
        <RouterLink to="/" class="text-3xl font-bold text-[#7b1e3a] mb-2 inline-block">
          SweetShots
        </RouterLink>
        <h2 class="text-2xl font-bold text-gray-900 mb-2">Sign in to your account</h2>
        <p class="text-gray-600">Access your bookings and photo gallery</p>
      </div>

      <!-- Login Form -->
      <form @submit.prevent="handleLogin" class="mt-8 space-y-6">
        <div class="space-y-4">
          <!-- Email Field -->
          <div>
            <label for="email" class="block text-sm font-medium text-gray-700 mb-1">
              Email address
            </label>
            <input
              id="email"
              v-model="form.email"
              type="email"
              required
              class="appearance-none relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-lg focus:outline-none focus:ring-[#7b1e3a] focus:border-[#7b1e3a] focus:z-10"
              placeholder="Enter your email"
            />
          </div>

          <!-- Password Field -->
          <div>
            <label for="password" class="block text-sm font-medium text-gray-700 mb-1">
              Password
            </label>
            <div class="relative">
              <input
                id="password"
                v-model="form.password"
                :type="showPassword ? 'text' : 'password'"
                required
                class="appearance-none relative block w-full px-3 py-3 pr-12 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-lg focus:outline-none focus:ring-[#7b1e3a] focus:border-[#7b1e3a] focus:z-10"
                placeholder="Enter your password"
              />
              <button
                type="button"
                @click="showPassword = !showPassword"
                class="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-400 hover:text-gray-600"
              >
                <i :class="showPassword ? 'fas fa-eye-slash' : 'fas fa-eye'"></i>
              </button>
            </div>
          </div>
        </div>

        <!-- Remember Me & Forgot Password -->
        <div class="flex items-center justify-between">
          <div class="flex items-center">
            <input
              id="remember-me"
              v-model="form.rememberMe"
              type="checkbox"
              class="h-4 w-4 text-[#7b1e3a] focus:ring-[#7b1e3a] border-gray-300 rounded"
            />
            <label for="remember-me" class="ml-2 block text-sm text-gray-700">
              Remember me
            </label>
          </div>
          <RouterLink
            to="/forgot-password"
            class="text-sm text-[#7b1e3a] hover:text-[#5c162c] font-medium"
          >
            Forgot your password?
          </RouterLink>
        </div>

        <!-- Submit Button -->
        <button
          type="submit"
          :disabled="isLoading"
          class="group relative w-full flex justify-center py-3 px-4 border border-transparent text-white bg-[#7b1e3a] hover:bg-[#5c162c] focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-[#7b1e3a] rounded-lg font-medium transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <span v-if="!isLoading">Sign in</span>
          <span v-else class="flex items-center">
            <i class="fas fa-spinner fa-spin mr-2"></i>
            Signing in...
          </span>
        </button>
      </form>

      <!-- Social Login -->
      <div class="mt-6">
        <div class="relative">
          <div class="absolute inset-0 flex items-center">
            <div class="w-full border-t border-gray-300"></div>
          </div>
          <div class="relative flex justify-center text-sm">
            <span class="px-2 bg-gray-50 text-gray-500">Or continue with</span>
          </div>
        </div>

        <div class="mt-6 grid grid-cols-2 gap-3">
          <button
            @click="loginWithGoogle"
            class="w-full inline-flex justify-center py-2 px-4 border border-gray-300 rounded-lg shadow-sm bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 transition-colors"
          >
            <i class="fab fa-google text-red-500"></i>
            <span class="ml-2">Google</span>
          </button>
          <button
            @click="loginWithFacebook"
            class="w-full inline-flex justify-center py-2 px-4 border border-gray-300 rounded-lg shadow-sm bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 transition-colors"
          >
            <i class="fab fa-facebook text-blue-600"></i>
            <span class="ml-2">Facebook</span>
          </button>
        </div>
      </div>

      <!-- Sign Up Link -->
      <div class="text-center">
        <p class="text-sm text-gray-600">
          Don't have an account?
          <RouterLink
            to="/register"
            class="font-medium text-[#7b1e3a] hover:text-[#5c162c] ml-1"
          >
            Sign up for free
          </RouterLink>
        </p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

const router = useRouter()

const form = ref({
  email: '',
  password: '',
  rememberMe: false
})

const showPassword = ref(false)
const isLoading = ref(false)

const handleLogin = async () => {
  isLoading.value = true
  
  try {
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 1000))
    
    // TODO: Replace with actual authentication logic
    if (form.value.email && form.value.password) {
      // Store user session (this would normally be handled by your auth system)
      const userData = {
        id: 1,
        email: form.value.email,
        name: form.value.email.split('@')[0],
        avatar: '/portrait.jpg'
      };
      
      localStorage.setItem('user', JSON.stringify(userData));
      
      // Emit custom event to notify navbar of login
      window.dispatchEvent(new CustomEvent('userLogin', { detail: userData }));
      
      toast.success('Welcome back! Login successful.');
      
      // Redirect to dashboard or previous page
      const redirect = router.currentRoute.value.query.redirect || '/dashboard'
      router.push(redirect)
    } else {
      toast.error('Please fill in all fields.')
    }
  } catch (error) {
    toast.error('Login failed. Please try again.')
  } finally {
    isLoading.value = false
  }
}

const loginWithGoogle = () => {
  toast.info('Google login integration coming soon!')
}

const loginWithFacebook = () => {
  toast.info('Facebook login integration coming soon!')
}
</script>