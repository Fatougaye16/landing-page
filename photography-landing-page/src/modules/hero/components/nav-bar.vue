<template>
  <nav class="flex items-center justify-between w-full px-6 py-4 bg-white text-[#7b1e3a] shadow-md">
    <!-- Logo -->
    <div class="flex-1">
      <h1 class="text-2xl md:text-3xl font-bold tracking-wide">SweetShots</h1>
    </div>

    <!-- Desktop Links -->
    <div class="hidden md:flex flex-1 justify-center">
      <ul class="flex gap-8 text-lg font-medium">
        <li class="hover:text-black cursor-pointer transition-colors">Shots</li>
        <li class="hover:text-black cursor-pointer transition-colors">Know Us</li>
        <li class="hover:text-black cursor-pointer transition-colors">Contact</li>
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
      <div v-if="isMenuOpen" class="absolute top-16 left-0 w-full bg-white shadow-lg p-6 z-50 md:hidden">
        <ul class="flex flex-col gap-4 text-lg font-medium">
          <li class="hover:text-black cursor-pointer transition-colors">Shots</li>
          <li class="hover:text-black cursor-pointer transition-colors">Know Us</li>
          <li class="hover:text-black cursor-pointer transition-colors">Contact</li>
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

const router = useRouter();
const bookNow = () => {
  console.log('clicked')
  router.push('/photographers')
}
const isMenuOpen = ref(false);
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
