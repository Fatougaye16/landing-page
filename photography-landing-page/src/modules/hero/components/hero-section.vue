<template>
    <div class="relative h-screen bg-[url('/camera-bg.jpg')] bg-cover bg-center">
      <div class="absolute inset-0 bg-black/50"></div>
  
      <NavBar class="relative z-10"/>
  
      <div class="relative z-10 flex flex-col items-center justify-center h-full text-white text-center px-4">
        <h1 class="font-extrabold text-5xl md:text-7xl lg:text-8xl">
          Capturing Your Best Moments
        </h1>
        <button class="border-2 text-white px-12 py-3 rounded-lg mt-8 transition duration-300 hover:bg-sky-100 hover:text-black">
          Contact Us
        </button>
      </div>
  
      <div class="absolute bottom-8 left-1/2 transform -translate-x-1/2 flex gap-6 text-white">
        <a href="#" class="text-2xl hover:text-sky-400 transition duration-300"><i class="fab fa-facebook"></i></a>
        <a href="#" class="text-2xl hover:text-pink-500 transition duration-300"><i class="fab fa-instagram"></i></a>
        <a href="#" class="text-2xl hover:text-blue-500 transition duration-300"><i class="fab fa-twitter"></i></a>
        <a href="#" class="text-2xl hover:text-red-500 transition duration-300"><i class="fab fa-youtube"></i></a>
      </div>
    </div>
  </template>
  
  <script setup lang="ts">
  import NavBar from './nav-bar.vue';
  </script>
  
  <style>
  @import url('https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css');
  </style>
  