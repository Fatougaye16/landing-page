<template>
    <div class="bg-[url('/camera-bg.jpg')] bg-cover bg-center h-screen">
        <NavBar/>
        <div class=" text-white text-center mt-64">
            <h1 class="text-bold text-8xl ">Capturing your best moments</h1>
            <button class="border-2 text-white px-16 py-2 rounded-lg hover:bg-sky-100 hover:text-black mt-12">Conatct us</button>
        </div>
    </div>
</template>

<script setup lang="ts">
import NavBar from './nav-bar.vue';

</script>