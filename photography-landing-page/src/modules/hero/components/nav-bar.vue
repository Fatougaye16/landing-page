<template>
  <nav v-if="!isDashboardPage" class="fixed top-0 left-0 right-0 flex items-center justify-between w-full px-6 py-4 bg-white text-[#7b1e3a] shadow-md z-50">
    <!-- Logo -->
    <div class="flex-1">
      <RouterLink to="/" class="text-2xl md:text-3xl font-bold tracking-wide hover:text-black transition-colors">
        SweetShots
      </RouterLink>
    </div>

    <!-- Desktop Links -->
    <div class="hidden md:flex flex-1 justify-center">
      <ul class="flex gap-8 text-lg font-medium">
        <li>
          <RouterLink 
            to="/" 
            class="hover:text-black cursor-pointer transition-colors relative group"
            active-class="text-black font-semibold"
          >
            Home
            <span class="absolute -bottom-1 left-0 w-0 h-0.5 bg-[#7b1e3a] transition-all duration-300 group-hover:w-full"></span>
          </RouterLink>
        </li>
        <li>
          <RouterLink 
            to="/shots" 
            class="hover:text-black cursor-pointer transition-colors relative group"
            active-class="text-black font-semibold"
          >
            Shots
            <span class="absolute -bottom-1 left-0 w-0 h-0.5 bg-[#7b1e3a] transition-all duration-300 group-hover:w-full"></span>
          </RouterLink>
        </li>
        <li>
          <RouterLink 
            to="/about" 
            class="hover:text-black cursor-pointer transition-colors relative group"
            active-class="text-black font-semibold"
          >
            Know Us
            <span class="absolute -bottom-1 left-0 w-0 h-0.5 bg-[#7b1e3a] transition-all duration-300 group-hover:w-full"></span>
          </RouterLink>
        </li>
        <li>
          <RouterLink 
            to="/photographers" 
            class="hover:text-black cursor-pointer transition-colors relative group"
            active-class="text-black font-semibold"
          >
            Photographers
            <span class="absolute -bottom-1 left-0 w-0 h-0.5 bg-[#7b1e3a] transition-all duration-300 group-hover:w-full"></span>
          </RouterLink>
        </li>
        <li>
          <RouterLink 
            to="/contact" 
            class="hover:text-black cursor-pointer transition-colors relative group"
            active-class="text-black font-semibold"
          >
            Contact
            <span class="absolute -bottom-1 left-0 w-0 h-0.5 bg-[#7b1e3a] transition-all duration-300 group-hover:w-full"></span>
          </RouterLink>
        </li>
      </ul>
    </div>

    <div class="hidden md:flex flex-1 justify-end">
      <!-- User Authentication Section -->
      <div v-if="user" class="flex items-center space-x-4">
        <!-- User Dropdown -->
        <div class="relative">
          <button @click="showUserMenu = !showUserMenu" 
                  class="flex items-center space-x-2 text-gray-700 hover:text-[#7b1e3a] transition-colors">
            <img :src="user.avatar || '/portrait.jpg'" 
                 class="w-8 h-8 rounded-full object-cover border-2 border-[#7b1e3a]" 
                 :alt="user.name" />
            <span class="font-medium">{{ user.name }}</span>
            <i class="fas fa-chevron-down text-xs"></i>
          </button>
          
          <!-- Dropdown Menu -->
          <div v-if="showUserMenu" 
               @click="showUserMenu = false"
               class="absolute right-0 mt-2 w-48 bg-white rounded-lg shadow-lg border border-gray-200 py-2 z-50">
            <RouterLink to="/dashboard" 
                        class="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-50 hover:text-[#7b1e3a]">
              <i class="fas fa-tachometer-alt mr-3"></i>
              Dashboard
            </RouterLink>
            <RouterLink to="/profile" 
                        class="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-50 hover:text-[#7b1e3a]">
              <i class="fas fa-user mr-3"></i>
              Profile
            </RouterLink>
            <RouterLink to="/my-bookings" 
                        class="flex items-center px-4 py-2 text-gray-700 hover:bg-gray-50 hover:text-[#7b1e3a]">
              <i class="fas fa-calendar-alt mr-3"></i>
              My Bookings
            </RouterLink>
            <hr class="my-2">
            <button @click="logout" 
                    class="flex items-center w-full px-4 py-2 text-gray-700 hover:bg-gray-50 hover:text-red-600">
              <i class="fas fa-sign-out-alt mr-3"></i>
              Logout
            </button>
          </div>
        </div>
        
        <RouterLink to="/photographers" 
                    class="px-6 py-2 rounded-full border border-[#7b1e3a] bg-[#7b1e3a] text-white hover:bg-white hover:text-[#7b1e3a] transition-all duration-300 shadow-lg">
          Book Session
        </RouterLink>
      </div>
      
      <!-- Guest Authentication Links -->
      <div v-else class="flex items-center space-x-4">
        <RouterLink to="/login" 
                    class="text-[#7b1e3a] hover:text-black font-medium transition-colors">
          Sign In
        </RouterLink>
        <RouterLink to="/register" 
                    class="px-6 py-2 rounded-full border border-[#7b1e3a] bg-[#7b1e3a] text-white hover:bg-white hover:text-[#7b1e3a] transition-all duration-300 shadow-lg">
          Sign Up
        </RouterLink>
      </div>
    </div>


    <!-- Mobile Menu Button -->
    <button class="md:hidden text-2xl" @click="isMenuOpen = !isMenuOpen">
      <i :class="isMenuOpen ? 'fas fa-times' : 'fas fa-bars'"></i>
    </button>

    <!-- Mobile Dropdown Menu -->
    <transition name="fade">
      <div v-if="isMenuOpen" class="fixed top-16 left-0 w-full bg-white shadow-lg p-6 z-50 md:hidden">
        <ul class="flex flex-col gap-4 text-lg font-medium">
          <li>
            <RouterLink 
              to="/" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              Home
            </RouterLink>
          </li>
          <li>
            <RouterLink 
              to="/shots" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              Shots
            </RouterLink>
          </li>
          <li>
            <RouterLink 
              to="/about" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              Know Us
            </RouterLink>
          </li>
          <li>
            <RouterLink 
              to="/photographers" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              Photographers
            </RouterLink>
          </li>
          <li>
            <RouterLink 
              to="/contact" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              Contact
            </RouterLink>
          </li>
          <li v-if="user">
            <RouterLink 
              to="/dashboard" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              <i class="fas fa-tachometer-alt mr-2"></i>
              Dashboard
            </RouterLink>
          </li>
          <li v-if="user">
            <button @click="logout; isMenuOpen = false" 
                    class="hover:text-red-600 cursor-pointer transition-colors block py-2 w-full text-left">
              <i class="fas fa-sign-out-alt mr-2"></i>
              Logout
            </button>
          </li>
          <li v-if="!user">
            <RouterLink 
              to="/login" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              <i class="fas fa-sign-in-alt mr-2"></i>
              Sign In
            </RouterLink>
          </li>
          <li v-if="!user">
            <RouterLink 
              to="/register" 
              @click="isMenuOpen = false" 
              class="hover:text-black cursor-pointer transition-colors block py-2"
              active-class="text-black font-semibold"
            >
              <i class="fas fa-user-plus mr-2"></i>
              Sign Up
            </RouterLink>
          </li>
          <li v-if="user">
            <RouterLink to="/photographers" @click="isMenuOpen = false"
                        class="mt-4 w-full border-2 border-[#7b1e3a] bg-[#7b1e3a] text-white px-5 py-2 rounded-lg hover:bg-white hover:text-[#7b1e3a] transition-colors block text-center">
              Book Session
            </RouterLink>
          </li>
          <li v-else>
            <button
            @click="bookNow"
              class="mt-4 w-full border-2 border-[#7b1e3a] bg-[#7b1e3a] text-white px-5 py-2 rounded-lg hover:bg-white hover:text-[#7b1e3a] transition-colors">
              Book Now
            </button>
          </li>
        </ul>
      </div>
    </transition>
  </nav>
</template>

<script setup>
import { ref, onMounted, onUnmounted, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { RouterLink } from "vue-router";
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const router = useRouter();
const route = useRoute();
const isMenuOpen = ref(false);
const showUserMenu = ref(false);
const user = ref(null);

// Check if current page is dashboard
const isDashboardPage = computed(() => route.name === 'dashboard')

const bookNow = () => {
  console.log('clicked')
  router.push('/photographers')
  isMenuOpen.value = false; // Close mobile menu after navigation
}

const logout = () => {
  localStorage.removeItem('user');
  user.value = null;
  showUserMenu.value = false;
  
  // Emit custom logout event
  window.dispatchEvent(new CustomEvent('userLogout'));
  
  toast.success('Logged out successfully!');
  router.push('/');
}

const handleStorageChange = (e) => {
  if (e.key === 'user') {
    if (e.newValue) {
      user.value = JSON.parse(e.newValue);
    } else {
      user.value = null;
    }
  }
};

const handleUserLogin = (e) => {
  user.value = e.detail;
};

const handleUserLogout = () => {
  user.value = null;
};

const handleClickOutside = (e) => {
  if (!e.target.closest('.relative')) {
    showUserMenu.value = false;
  }
};

// Check for user session on component mount
onMounted(() => {
  const userData = localStorage.getItem('user');
  if (userData) {
    user.value = JSON.parse(userData);
  }
  
  // Add event listeners
  window.addEventListener('storage', handleStorageChange);
  window.addEventListener('userLogin', handleUserLogin);
  window.addEventListener('userLogout', handleUserLogout);
  document.addEventListener('click', handleClickOutside);
});

// Cleanup event listeners on component unmount
onUnmounted(() => {
  window.removeEventListener('storage', handleStorageChange);
  window.removeEventListener('userLogin', handleUserLogin);
  window.removeEventListener('userLogout', handleUserLogout);
  document.removeEventListener('click', handleClickOutside);
});
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
