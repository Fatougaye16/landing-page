<template>
  <nav class="fixed top-0 left-0 right-0 flex items-center justify-between w-full px-6 py-4 bg-white text-[#7b1e3a] shadow-md z-50">
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
       <button @click="bookNow" class="mt-8 px-8 py-3 rounded-full border border-[#7b1e3a] bg-[#7b1e3a] text-white
               hover:bg-white hover:text-black transition-all duration-300 shadow-lg animate-fadeInUp delay-300
               w-full sm:w-auto max-w-xs">
        Book Now
      </button>
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
          <li>
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
import { ref } from "vue";
import { useRouter } from "vue-router";
import { RouterLink } from "vue-router";

const router = useRouter();
const isMenuOpen = ref(false);

const bookNow = () => {
  console.log('clicked')
  router.push('/photographers')
  isMenuOpen.value = false; // Close mobile menu after navigation
}
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
