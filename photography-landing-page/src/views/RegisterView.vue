<template>
  <div class="min-h-screen bg-gray-50 flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
    <div class="max-w-md w-full space-y-8">
      <!-- Header -->
      <div class="text-center">
        <RouterLink to="/" class="text-3xl font-bold text-[#7b1e3a] mb-2 inline-block">
          SweetShots
        </RouterLink>
        <h2 class="text-2xl font-bold text-gray-900 mb-2">Create your account</h2>
        <p class="text-gray-600">Join us to manage your bookings and photos</p>
      </div>

      <!-- Registration Form -->
      <form @submit.prevent="handleRegister" class="mt-8 space-y-6">
        <div class="space-y-4">
          <!-- Full Name Field -->
          <div>
            <label for="fullName" class="block text-sm font-medium text-gray-700 mb-1">
              Full Name
            </label>
            <input
              id="fullName"
              v-model="form.fullName"
              type="text"
              required
              class="appearance-none relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-lg focus:outline-none focus:ring-[#7b1e3a] focus:border-[#7b1e3a] focus:z-10"
              placeholder="Enter your full name"
            />
          </div>

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

          <!-- Phone Field -->
          <div>
            <label for="phone" class="block text-sm font-medium text-gray-700 mb-1">
              Phone Number
            </label>
            <input
              id="phone"
              v-model="form.phone"
              type="tel"
              required
              class="appearance-none relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-lg focus:outline-none focus:ring-[#7b1e3a] focus:border-[#7b1e3a] focus:z-10"
              placeholder="Enter your phone number"
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
                placeholder="Create a password"
              />
              <button
                type="button"
                @click="showPassword = !showPassword"
                class="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-400 hover:text-gray-600"
              >
                <i :class="showPassword ? 'fas fa-eye-slash' : 'fas fa-eye'"></i>
              </button>
            </div>
            <!-- Password Strength Indicator -->
            <div class="mt-2">
              <div class="flex space-x-1">
                <div
                  v-for="i in 4"
                  :key="i"
                  :class="[
                    'h-1 w-full rounded',
                    getPasswordStrength() >= i ? getPasswordStrengthColor() : 'bg-gray-200'
                  ]"
                ></div>
              </div>
              <p class="text-xs text-gray-600 mt-1">
                {{ getPasswordStrengthText() }}
              </p>
            </div>
          </div>

          <!-- Confirm Password Field -->
          <div>
            <label for="confirmPassword" class="block text-sm font-medium text-gray-700 mb-1">
              Confirm Password
            </label>
            <input
              id="confirmPassword"
              v-model="form.confirmPassword"
              type="password"
              required
              class="appearance-none relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-lg focus:outline-none focus:ring-[#7b1e3a] focus:border-[#7b1e3a] focus:z-10"
              placeholder="Confirm your password"
            />
            <p v-if="form.confirmPassword && form.password !== form.confirmPassword" class="text-xs text-red-600 mt-1">
              Passwords do not match
            </p>
          </div>
        </div>

        <!-- Terms and Privacy -->
        <div class="flex items-start">
          <input
            id="terms"
            v-model="form.acceptTerms"
            type="checkbox"
            required
            class="h-4 w-4 text-[#7b1e3a] focus:ring-[#7b1e3a] border-gray-300 rounded mt-0.5"
          />
          <label for="terms" class="ml-2 block text-sm text-gray-700">
            I agree to the
            <RouterLink to="/terms" class="text-[#7b1e3a] hover:text-[#5c162c] font-medium">
              Terms of Service
            </RouterLink>
            and
            <RouterLink to="/privacy" class="text-[#7b1e3a] hover:text-[#5c162c] font-medium">
              Privacy Policy
            </RouterLink>
          </label>
        </div>

        <!-- Submit Button -->
        <button
          type="submit"
          :disabled="isLoading || !isFormValid"
          class="group relative w-full flex justify-center py-3 px-4 border border-transparent text-white bg-[#7b1e3a] hover:bg-[#5c162c] focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-[#7b1e3a] rounded-lg font-medium transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
        >
          <span v-if="!isLoading">Create Account</span>
          <span v-else class="flex items-center">
            <i class="fas fa-spinner fa-spin mr-2"></i>
            Creating account...
          </span>
        </button>
      </form>

      <!-- Social Registration -->
      <div class="mt-6">
        <div class="relative">
          <div class="absolute inset-0 flex items-center">
            <div class="w-full border-t border-gray-300"></div>
          </div>
          <div class="relative flex justify-center text-sm">
            <span class="px-2 bg-gray-50 text-gray-500">Or sign up with</span>
          </div>
        </div>

        <div class="mt-6 grid grid-cols-2 gap-3">
          <button
            @click="registerWithGoogle"
            class="w-full inline-flex justify-center py-2 px-4 border border-gray-300 rounded-lg shadow-sm bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 transition-colors"
          >
            <i class="fab fa-google text-red-500"></i>
            <span class="ml-2">Google</span>
          </button>
          <button
            @click="registerWithFacebook"
            class="w-full inline-flex justify-center py-2 px-4 border border-gray-300 rounded-lg shadow-sm bg-white text-sm font-medium text-gray-500 hover:bg-gray-50 transition-colors"
          >
            <i class="fab fa-facebook text-blue-600"></i>
            <span class="ml-2">Facebook</span>
          </button>
        </div>
      </div>

      <!-- Sign In Link -->
      <div class="text-center">
        <p class="text-sm text-gray-600">
          Already have an account?
          <RouterLink
            to="/login"
            class="font-medium text-[#7b1e3a] hover:text-[#5c162c] ml-1"
          >
            Sign in here
          </RouterLink>
        </p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import { toast } from 'vue3-toastify'
import 'vue3-toastify/dist/index.css'

const router = useRouter()

const form = ref({
  fullName: '',
  email: '',
  phone: '',
  password: '',
  confirmPassword: '',
  acceptTerms: false
})

const showPassword = ref(false)
const isLoading = ref(false)

const isFormValid = computed(() => {
  return form.value.fullName &&
         form.value.email &&
         form.value.phone &&
         form.value.password &&
         form.value.confirmPassword &&
         form.value.password === form.value.confirmPassword &&
         form.value.acceptTerms &&
         getPasswordStrength() >= 2
})

const getPasswordStrength = () => {
  const password = form.value.password
  let strength = 0
  
  if (password.length >= 8) strength++
  if (/[A-Z]/.test(password)) strength++
  if (/[0-9]/.test(password)) strength++
  if (/[^A-Za-z0-9]/.test(password)) strength++
  
  return strength
}

const getPasswordStrengthColor = () => {
  const strength = getPasswordStrength()
  if (strength <= 1) return 'bg-red-500'
  if (strength === 2) return 'bg-yellow-500'
  if (strength === 3) return 'bg-blue-500'
  return 'bg-green-500'
}

const getPasswordStrengthText = () => {
  const strength = getPasswordStrength()
  if (strength === 0) return 'Enter a password'
  if (strength === 1) return 'Weak password'
  if (strength === 2) return 'Fair password'
  if (strength === 3) return 'Good password'
  return 'Strong password'
}

const handleRegister = async () => {
  if (!isFormValid.value) {
    toast.error('Please fill in all fields correctly.')
    return
  }

  isLoading.value = true
  
  try {
    // Simulate API call
    await new Promise(resolve => setTimeout(resolve, 1500))
    
    // TODO: Replace with actual registration logic
    // Store user session (this would normally be handled by your auth system)
    const userData = {
      id: Date.now(),
      email: form.value.email,
      name: form.value.fullName,
      phone: form.value.phone,
      avatar: '/portrait.jpg'
    };
    
    localStorage.setItem('user', JSON.stringify(userData));
    
    // Emit custom event to notify navbar of login
    window.dispatchEvent(new CustomEvent('userLogin', { detail: userData }));
    
    toast.success('Account created successfully! Welcome to SweetShots.');
    
    // Redirect to dashboard
    router.push('/dashboard')
  } catch (error) {
    toast.error('Registration failed. Please try again.')
  } finally {
    isLoading.value = false
  }
}

const registerWithGoogle = () => {
  toast.info('Google registration integration coming soon!')
}

const registerWithFacebook = () => {
  toast.info('Facebook registration integration coming soon!')
}
</script>