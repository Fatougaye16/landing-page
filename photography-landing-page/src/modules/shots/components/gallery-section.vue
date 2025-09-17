<template>
    <section class="py-20 bg-gradient-to-br from-gray-50 to-white">
      <div class="max-w-6xl mx-auto px-6">
        <div class="text-center mb-16">
          <span class="inline-block px-4 py-2 bg-[#7b1e3a]/10 text-[#7b1e3a] rounded-full text-sm font-semibold mb-4">
            Our Portfolio
          </span>
          <h2 class="text-4xl font-bold text-gray-800 mb-4">Featured Work</h2>
          <p class="text-xl text-gray-600 max-w-3xl mx-auto">
            Explore our stunning collection of photography work across different styles and occasions
          </p>
        </div>
        
        <!-- Gallery Grid -->
        <div class="grid grid-cols-2 md:grid-cols-3 gap-4 auto-rows-[200px] md:auto-rows-[300px]">
          <div class="col-span-2 row-span-1 group">
            <div class="relative h-full overflow-hidden rounded-2xl shadow-lg">
              <img src="/shot-1.jpg" alt="Wedding Photography" class="bento-img">
              <div class="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                <div class="text-white text-center">
                  <h3 class="text-xl font-semibold mb-2">Wedding Photography</h3>
                  <p class="text-sm">Romantic & Elegant</p>
                </div>
              </div>
            </div>
          </div>
          
          <div class="col-span-1 row-span-2 group">
            <div class="relative h-full overflow-hidden rounded-2xl shadow-lg">
              <img src="/shot-2.jpg" alt="Portrait Session" class="bento-img">
              <div class="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                <div class="text-white text-center">
                  <h3 class="text-xl font-semibold mb-2">Portrait Sessions</h3>
                  <p class="text-sm">Professional & Personal</p>
                </div>
              </div>
            </div>
          </div>
          
          <div class="col-span-1 row-span-1 group">
            <div class="relative h-full overflow-hidden rounded-2xl shadow-lg">
              <img src="/shot-3.jpg" alt="Event Photography" class="bento-img">
              <div class="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                <div class="text-white text-center">
                  <h3 class="text-lg font-semibold mb-1">Events</h3>
                  <p class="text-xs">Corporate & Social</p>
                </div>
              </div>
            </div>
          </div>
          
          <div class="col-span-1 row-span-1 group">
            <div class="relative h-full overflow-hidden rounded-2xl shadow-lg">
              <img src="/nature-shot.jpg" alt="Nature Photography" class="bento-img">
              <div class="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                <div class="text-white text-center">
                  <h3 class="text-lg font-semibold mb-1">Nature</h3>
                  <p class="text-xs">Landscapes & Wildlife</p>
                </div>
              </div>
            </div>
          </div>
          
          <div class="col-span-2 row-span-1 group">
            <div class="relative h-full overflow-hidden rounded-2xl shadow-lg">
              <img src="/shot-4.jpg" alt="Fashion Photography" class="bento-img">
              <div class="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                <div class="text-white text-center">
                  <h3 class="text-xl font-semibold mb-2">Fashion & Lifestyle</h3>
                  <p class="text-sm">Creative & Artistic</p>
                </div>
              </div>
            </div>
          </div>
          
          <div class="col-span-1 row-span-1 group">
            <div class="relative h-full overflow-hidden rounded-2xl shadow-lg">
              <img src="/shot-5.jpg" alt="Commercial Photography" class="bento-img">
              <div class="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                <div class="text-white text-center">
                  <h3 class="text-lg font-semibold mb-1">Commercial</h3>
                  <p class="text-xs">Products & Brands</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        
        <!-- View More Button -->
        <div class="text-center mt-12">
          <router-link to="/photographers" 
                       class="inline-block bg-[#7b1e3a] hover:bg-[#5c162c] text-white px-8 py-3 rounded-lg font-semibold transition-colors shadow-lg">
            Explore Our Photographers
          </router-link>
        </div>
      </div>
    </section>
  </template>
  
  <style scoped>
  .bento-img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    transition: transform 0.5s ease;
  }
  
  .group:hover .bento-img {
    transform: scale(1.1);
  }
  </style>
  