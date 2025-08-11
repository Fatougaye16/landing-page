<template>
  <nav class="flex items-center justify-between w-full px-10 py-6 bg-white text-[#7b1e3a] shadow-md">
    <!-- Logo -->
    <div class="flex-1">
      <h1 class="text-3xl font-bold tracking-wide">SweetShots</h1>
    </div>

    <!-- Links -->
    <div class="flex-1 flex justify-center">
      <ul class="flex gap-8 text-lg font-medium">
        <li class="hover:text-black cursor-pointer transition-colors">Shots</li>
        <li class="hover:text-black cursor-pointer transition-colors">Know Us</li>
        <li class="hover:text-black cursor-pointer transition-colors">Contact</li>
      </ul>
    </div>

    <!-- Button -->
    <div class="flex-1 flex justify-end">
      <button class="border-2 border-[#7b1e3a] bg-[#7b1e3a] text-white px-5 py-2 rounded-lg hover:bg-white hover:text-[#7b1e3a] transition-colors">
        Book Now
      </button>
    </div>
  </nav>
</template>
