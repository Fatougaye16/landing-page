<template>
    <div class="flex flex-col items-center p-8 bg-linear-to-r from-stone-50 to-black">
      <h1 class="text-center text-3xl font-bold mb-6">Our Shots</h1>
      <div class="grid grid-cols-3 gap-4 p-8 auto-rows-[200px] md:auto-rows-[300px]">
        <div class="col-span-2 row-span-1">
          <img src="/shot-1.jpg" alt="Shot 1" class="bento-img">
        </div>
        <div class="col-span-1 row-span-2">
          <img src="/shot-2.jpg" alt="Shot 2" class="bento-img">
        </div>
        <div class="col-span-1 row-span-1">
          <img src="/shot-3.jpg" alt="Shot 3" class="bento-img">
        </div>
        <div class="col-span-1 row-span-1">
          <img src="/shot-3.jpg" alt="Shot 3" class="bento-img">
        </div>
        <div class="col-span-2 row-span-1">
          <img src="/shot-4.jpg" alt="Shot 4" class="bento-img">
        </div>
        <div class="col-span-1 row-span-1">
          <img src="/shot-5.jpg" alt="Shot 3" class="bento-img">
        </div>
      </div>
    </div>
  </template>
  
  <style scoped>
  .bento-img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    border-radius: 12px;
  }
  </style>
  