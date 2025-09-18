<template>
  <div class="min-h-screen bg-white">
    <!-- Hero Section -->
    <section class="py-20 px-6 bg-gradient-to-br from-gray-50 to-gray-100">
      <div class="max-w-6xl mx-auto text-center">
        <span class="inline-block px-4 py-2 bg-[#7b1e3a]/10 text-[#7b1e3a] rounded-full text-sm font-semibold mb-4">
          Meet Our Team
        </span>
        <h1 class="text-4xl lg:text-5xl font-bold text-gray-900 mb-6">
          Our Photographers
        </h1>
        <p class="text-xl text-gray-600 max-w-3xl mx-auto leading-relaxed">
          Meet our talented team of professional photographers who bring passion, creativity, and 
          expertise to every shoot. Each photographer has their unique style and specialization.
        </p>
      </div>
    </section>

    <!-- Photographers Grid -->
    <section class="py-16 px-6">
      <div class="max-w-6xl mx-auto">
        <div class="grid md:grid-cols-2 lg:grid-cols-4 gap-6">
          <div 
            v-for="photographer in photographers"
            :key="photographer.id"
            class="group bg-white rounded-xl shadow-md overflow-hidden hover:shadow-lg transition-all duration-300 transform hover:-translate-y-1"
          >
            <!-- Profile Image -->
            <div class="relative h-48 overflow-hidden">
              <img
                :src="photographer.image"
                :alt="photographer.name"
                class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500"
              />
              <!-- Overlay -->
              <div class="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                <div class="absolute bottom-3 left-3 right-3 text-white">
                  <p class="text-xs font-medium opacity-90">{{ photographer.experience }} experience</p>
                </div>
              </div>
              
              <!-- Status Badge -->
              <div class="absolute top-3 left-3">
                <span class="px-2 py-1 bg-green-500 text-white text-xs font-medium rounded-full">
                  Available
                </span>
              </div>
            </div>

            <!-- Content -->
            <div class="p-4">
              <div class="mb-3">
                <h3 class="text-lg font-bold text-gray-900 mb-1">{{ photographer.name }}</h3>
                <p class="text-[#7b1e3a] font-medium text-sm mb-2">{{ photographer.studio }}</p>
                <p class="text-gray-600 text-xs leading-relaxed line-clamp-2">{{ photographer.bio }}</p>
              </div>

              <!-- Specializations -->
              <div class="mb-3">
                <h4 class="text-xs font-semibold text-gray-700 mb-1">Specializations:</h4>
                <div class="flex flex-wrap gap-1">
                  <span 
                    v-for="specialty in photographer.specializations.slice(0, 2)" 
                    :key="specialty"
                    class="px-2 py-1 bg-gray-100 text-gray-700 text-xs rounded-full"
                  >
                    {{ specialty }}
                  </span>
                  <span v-if="photographer.specializations.length > 2" class="text-xs text-gray-500">
                    +{{ photographer.specializations.length - 2 }}
                  </span>
                </div>
              </div>

              <!-- Rating & Price -->
              <div class="flex items-center justify-between mb-3">
                <div class="flex items-center gap-1">
                  <div class="flex text-yellow-400 text-sm">
                    <i v-for="n in 5" :key="n" :class="n <= photographer.rating ? 'fas fa-star' : 'far fa-star'"></i>
                  </div>
                  <span class="text-xs text-gray-600">({{ photographer.rating }}.0)</span>
                </div>
                <div>
                  <span class="text-lg font-bold text-gray-900">${{ photographer.price }}</span>
                  <span class="text-gray-600 text-xs">/hr</span>
                </div>
              </div>

              <!-- Action Buttons -->
              <div class="flex gap-2">
                <RouterLink
                  :to="{ name: 'photographer-details', params: { id: photographer.id } }"
                  class="flex-1 bg-[#7b1e3a] text-white text-center py-2 px-3 rounded-lg text-sm font-semibold hover:bg-[#5c162c] transition-colors"
                >
                  View Profile
                </RouterLink>
                <button 
                  @click="bookPhotographer(photographer)"
                  class="flex-1 border border-[#7b1e3a] text-[#7b1e3a] text-center py-2 px-3 rounded-lg text-sm font-semibold hover:bg-[#7b1e3a] hover:text-white transition-colors"
                >
                  Book
                </button>
              </div>
            </div>
          </div>
        </div>

        <!-- No photographers message -->
        <div v-if="photographers.length === 0" class="text-center py-16">
          <div class="w-24 h-24 bg-gray-100 rounded-full flex items-center justify-center mx-auto mb-6">
            <i class="fas fa-camera text-gray-400 text-2xl"></i>
          </div>
          <h3 class="text-xl font-semibold text-gray-900 mb-2">No photographers available</h3>
          <p class="text-gray-600">Please check back later for available photographers.</p>
        </div>
      </div>
    </section>

    <!-- Features Section -->
    <section class="py-16 px-6 bg-gray-50">
      <div class="max-w-6xl mx-auto">
        <div class="text-center mb-12">
          <h3 class="text-3xl font-bold text-gray-900 mb-4">Why Choose Our Photographers?</h3>
          <p class="text-lg text-gray-600 max-w-2xl mx-auto">
            Our carefully selected photographers bring expertise, creativity, and professionalism to every project.
          </p>
        </div>

        <div class="grid md:grid-cols-3 gap-8">
          <div class="text-center p-6">
            <div class="w-16 h-16 bg-[#7b1e3a] rounded-full flex items-center justify-center mx-auto mb-4">
              <i class="fas fa-award text-white text-2xl"></i>
            </div>
            <h4 class="text-xl font-semibold text-gray-900 mb-2">Professional Quality</h4>
            <p class="text-gray-600">Years of experience and training ensure exceptional results every time.</p>
          </div>
          
          <div class="text-center p-6">
            <div class="w-16 h-16 bg-[#7b1e3a] rounded-full flex items-center justify-center mx-auto mb-4">
              <i class="fas fa-palette text-white text-2xl"></i>
            </div>
            <h4 class="text-xl font-semibold text-gray-900 mb-2">Creative Vision</h4>
            <p class="text-gray-600">Unique artistic perspectives that bring your vision to life in stunning ways.</p>
          </div>
          
          <div class="text-center p-6">
            <div class="w-16 h-16 bg-[#7b1e3a] rounded-full flex items-center justify-center mx-auto mb-4">
              <i class="fas fa-clock text-white text-2xl"></i>
            </div>
            <h4 class="text-xl font-semibold text-gray-900 mb-2">Reliable Service</h4>
            <p class="text-gray-600">Punctual, professional, and committed to delivering on time, every time.</p>
          </div>
        </div>
      </div>
    </section>

    <!-- Call to Action -->
    <section class="py-20 px-6 bg-[#7b1e3a] text-white">
      <div class="max-w-4xl mx-auto text-center">
        <h3 class="text-3xl lg:text-4xl font-bold mb-6">Ready to Book Your Session?</h3>
        <p class="text-xl text-pink-100 mb-8 leading-relaxed">
          Get in touch with us to discuss your photography needs and find the perfect photographer for your project.
        </p>
        <div class="flex flex-col sm:flex-row gap-4 justify-center">
          <RouterLink to="/contact" 
            class="inline-flex items-center justify-center px-8 py-4 bg-white text-[#7b1e3a] rounded-lg font-semibold hover:bg-gray-100 transition-colors shadow-lg">
            Contact Us
          </RouterLink>
          <RouterLink to="/shots" 
            class="inline-flex items-center justify-center px-8 py-4 border-2 border-white text-white rounded-lg font-semibold hover:bg-white hover:text-[#7b1e3a] transition-colors">
            View Our Work
          </RouterLink>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router'

const router = useRouter()

const photographers = [
  {
    id: 1,
    name: "Nuru Ahmed",
    studio: "Nuru's Touch",
    image: "/man-lens.jpg",
    bio: "Specializing in portrait and lifestyle photography with over 8 years of experience capturing authentic moments.",
    experience: "8+ years",
    specializations: ["Portrait", "Lifestyle", "Family"],
    rating: 5,
    price: 150,
  },
  {
    id: 2,
    name: "Ellie Beilish",
    studio: "Dutcher's Shot",
    image: "https://img.daisyui.com/images/profile/demo/4@94.webp",
    bio: "Award-winning wedding and event photographer known for capturing emotional storytelling through creative compositions.",
    experience: "6+ years",
    specializations: ["Wedding", "Events", "Corporate"],
    rating: 5,
    price: 200,
  },
  {
    id: 3,
    name: "Adu Mensah",
    studio: "Cappuccino",
    image: "https://img.daisyui.com/images/profile/demo/3@94.webp",
    bio: "Creative commercial photographer with expertise in product, fashion, and architectural photography.",
    experience: "10+ years",
    specializations: ["Commercial", "Fashion", "Architecture"],
    rating: 4,
    price: 180,
  },
];

const bookPhotographer = (photographer) => {
  // You can implement booking logic here
  console.log('Booking photographer:', photographer.name)
  router.push({ name: 'photographer-details', params: { id: photographer.id } })
}
</script>

<style scoped>
/* Custom scrollbar */
::-webkit-scrollbar {
  width: 8px;
}

::-webkit-scrollbar-track {
  background: #f1f1f1;
}

::-webkit-scrollbar-thumb {
  background: #7b1e3a;
  border-radius: 4px;
}

::-webkit-scrollbar-thumb:hover {
  background: #5c162c;
}

/* Line clamp for bio text */
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
