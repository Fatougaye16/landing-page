<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Hero Section -->
    <section class="relative py-20 px-6 bg-gradient-to-r from-[#7b1e3a] to-[#9c2d52] text-white">
      <div class="max-w-6xl mx-auto text-center">
        <h1 class="text-4xl lg:text-6xl font-bold mb-6">Our Shots</h1>
        <p class="text-xl lg:text-2xl text-pink-100 mb-8 max-w-3xl mx-auto">
          Explore our collection of captured moments - from intimate portraits to grand celebrations, 
          each image tells a unique story.
        </p>
        
        <!-- Category Filter -->
        <div class="flex flex-wrap justify-center gap-4 mb-8">
          <button 
            v-for="category in categories" 
            :key="category"
            @click="activeCategory = category"
            :class="[
              'px-6 py-2 rounded-full font-semibold transition-all duration-300',
              activeCategory === category 
                ? 'bg-white text-[#7b1e3a] shadow-lg' 
                : 'bg-white/20 text-white hover:bg-white/30'
            ]"
          >
            {{ category }}
          </button>
        </div>
      </div>
    </section>

    <!-- Gallery Section -->
    <section class="py-16 px-6">
      <div class="max-w-7xl mx-auto">
        <!-- Loading State -->
        <div v-if="isLoading" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          <div v-for="n in 12" :key="n" class="bg-gray-200 aspect-square rounded-xl animate-pulse"></div>
        </div>

        <!-- Gallery Grid -->
        <div v-else class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          <div 
            v-for="(photo, index) in filteredPhotos" 
            :key="index"
            @click="openLightbox(index)"
            class="group cursor-pointer relative overflow-hidden rounded-xl bg-gray-200 aspect-square hover:shadow-2xl transition-all duration-500"
          >
            <img 
              :src="photo.src" 
              :alt="photo.alt" 
              class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-700"
              loading="lazy"
            >
            <!-- Overlay -->
            <div class="absolute inset-0 bg-gradient-to-t from-black/60 via-transparent to-transparent opacity-0 group-hover:opacity-100 transition-opacity duration-300">
              <div class="absolute bottom-4 left-4 text-white">
                <p class="font-semibold">{{ photo.title }}</p>
                <p class="text-sm text-gray-300">{{ photo.category }}</p>
              </div>
              <div class="absolute top-4 right-4">
                <i class="fas fa-expand-alt text-white text-lg"></i>
              </div>
            </div>
          </div>
        </div>

        <!-- Load More Button -->
        <div class="text-center mt-12" v-if="hasMorePhotos">
          <button 
            @click="loadMorePhotos"
            class="px-8 py-3 bg-[#7b1e3a] text-white rounded-full font-semibold hover:bg-[#5c162c] transition-colors shadow-lg"
          >
            Load More Photos
          </button>
        </div>
      </div>
    </section>

    <!-- Lightbox Modal -->
    <div 
      v-if="lightboxOpen" 
      @click="closeLightbox"
      class="fixed inset-0 bg-black/90 z-50 flex items-center justify-center p-4"
    >
      <div class="relative max-w-4xl max-h-full">
        <!-- Close Button -->
        <button 
          @click="closeLightbox"
          class="absolute top-4 right-4 z-10 w-10 h-10 bg-white/20 hover:bg-white/30 rounded-full flex items-center justify-center text-white transition-colors"
        >
          <i class="fas fa-times text-lg"></i>
        </button>

        <!-- Navigation Arrows -->
        <button 
          @click="previousPhoto"
          class="absolute left-4 top-1/2 transform -translate-y-1/2 w-12 h-12 bg-white/20 hover:bg-white/30 rounded-full flex items-center justify-center text-white transition-colors"
        >
          <i class="fas fa-chevron-left text-lg"></i>
        </button>
        
        <button 
          @click="nextPhoto"
          class="absolute right-4 top-1/2 transform -translate-y-1/2 w-12 h-12 bg-white/20 hover:bg-white/30 rounded-full flex items-center justify-center text-white transition-colors"
        >
          <i class="fas fa-chevron-right text-lg"></i>
        </button>

        <!-- Main Image -->
        <img 
          :src="currentPhoto?.src" 
          :alt="currentPhoto?.alt"
          class="max-w-full max-h-full object-contain rounded-lg"
          @click.stop
        >

        <!-- Photo Info -->
        <div class="absolute bottom-4 left-4 right-4 text-center text-white">
          <h3 class="text-xl font-semibold mb-2">{{ currentPhoto?.title }}</h3>
          <p class="text-gray-300">{{ currentPhoto?.description }}</p>
          <div class="mt-2 text-sm text-gray-400">
            {{ currentLightboxIndex + 1 }} / {{ filteredPhotos.length }}
          </div>
        </div>
      </div>
    </div>

    <!-- Call to Action -->
    <section class="py-20 px-6 bg-white">
      <div class="max-w-4xl mx-auto text-center">
        <h3 class="text-3xl font-bold text-gray-900 mb-6">Love What You See?</h3>
        <p class="text-lg text-gray-600 mb-8 max-w-2xl mx-auto">
          Ready to create your own collection of beautiful memories? Let's work together to capture your special moments.
        </p>
        <div class="flex flex-col sm:flex-row gap-4 justify-center">
          <RouterLink to="/contact" 
            class="px-8 py-3 bg-[#7b1e3a] text-white rounded-full font-semibold hover:bg-[#5c162c] transition-colors shadow-lg">
            Book a Session
          </RouterLink>
          <RouterLink to="/about" 
            class="px-8 py-3 border-2 border-[#7b1e3a] text-[#7b1e3a] rounded-full font-semibold hover:bg-[#7b1e3a] hover:text-white transition-colors">
            Learn More About Us
          </RouterLink>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onBeforeUnmount } from 'vue'
import { useSupabase } from '@/composables/useSupabase'
import { toast } from 'vue3-toastify'

const { fetchData, loading, error } = useSupabase()

const activeCategory = ref('All')
const lightboxOpen = ref(false)
const currentLightboxIndex = ref(0)
const photosLoaded = ref(12) // Initially load 12 photos
const allPhotos = ref([])
const isLoading = ref(false)

const categories = ['All', 'Wedding', 'Portrait', 'Event', 'Nature', 'Commercial']

// Load photos from Supabase
const loadPhotos = async () => {
  isLoading.value = true
  try {
    // Fetch public photos from portfolio_items and photos tables
    const [portfolioData, publicPhotos] = await Promise.all([
      fetchData('portfolio_items'),
      fetchData('photos', { is_public: true })
    ])
    
    const photos = []
    
    // Add portfolio items
    if (portfolioData) {
      portfolioData.forEach(item => {
        photos.push({
          src: item.image_url,
          alt: item.title || 'Portfolio image',
          title: item.title || 'Untitled',
          category: item.category ? item.category.charAt(0).toUpperCase() + item.category.slice(1) : 'Commercial',
          description: item.description || 'Professional photography work',
          photographer: item.photographer_id
        })
      })
    }
    
    // Add public photos
    if (publicPhotos) {
      publicPhotos.forEach(photo => {
        photos.push({
          src: photo.file_url,
          alt: photo.title || 'Photo',
          title: photo.title || 'Untitled Photo',
          category: 'Portrait', // Default category, could be enhanced with session data
          description: photo.description || 'Beautiful captured moment',
          photographer: photo.photographer_id
        })
      })
    }
    
    // If no photos from database, use fallback images
    if (photos.length === 0) {
      photos.push(
        { src: '/wedding.jpg', alt: 'Beautiful wedding ceremony', title: 'Wedding Bliss', category: 'Wedding', description: 'A magical moment captured during the ceremony' },
        { src: '/portrait.jpg', alt: 'Professional portrait', title: 'Portrait Session', category: 'Portrait', description: 'Stunning professional headshot' },
        { src: '/event.jpg', alt: 'Corporate event', title: 'Corporate Gathering', category: 'Event', description: 'Professional corporate event photography' },
        { src: '/nature-shot.jpg', alt: 'Nature photography', title: 'Natural Beauty', category: 'Nature', description: 'Breathtaking landscape capture' },
        { src: '/shot-1.jpg', alt: 'Creative shot', title: 'Artistic Vision', category: 'Commercial', description: 'Creative commercial photography' },
        { src: '/shot-2.jpg', alt: 'Another creative shot', title: 'Dynamic Composition', category: 'Portrait', description: 'Bold and dynamic portrait work' }
      )
    }
    
    allPhotos.value = photos
  } catch (err) {
    console.error('Error loading photos:', err)
    toast.error('Failed to load gallery photos')
    
    // Use fallback images on error
    allPhotos.value = [
      { src: '/wedding.jpg', alt: 'Beautiful wedding ceremony', title: 'Wedding Bliss', category: 'Wedding', description: 'A magical moment captured during the ceremony' },
      { src: '/portrait.jpg', alt: 'Professional portrait', title: 'Portrait Session', category: 'Portrait', description: 'Stunning professional headshot' },
      { src: '/event.jpg', alt: 'Corporate event', title: 'Corporate Gathering', category: 'Event', description: 'Professional corporate event photography' }
    ]
  } finally {
    isLoading.value = false
  }
}

const filteredPhotos = computed(() => {
  const filtered = activeCategory.value === 'All' 
    ? allPhotos.value 
    : allPhotos.value.filter(photo => photo.category === activeCategory.value)
  
  return filtered.slice(0, photosLoaded.value)
})

const hasMorePhotos = computed(() => {
  const totalFiltered = activeCategory.value === 'All' 
    ? allPhotos.value.length 
    : allPhotos.value.filter(photo => photo.category === activeCategory.value).length
  
  return photosLoaded.value < totalFiltered
})

const currentPhoto = computed(() => {
  return filteredPhotos.value[currentLightboxIndex.value]
})

const openLightbox = (index: number) => {
  currentLightboxIndex.value = index
  lightboxOpen.value = true
  document.body.style.overflow = 'hidden'
}

const closeLightbox = () => {
  lightboxOpen.value = false
  document.body.style.overflow = 'auto'
}

const nextPhoto = () => {
  if (currentLightboxIndex.value < filteredPhotos.value.length - 1) {
    currentLightboxIndex.value++
  } else {
    currentLightboxIndex.value = 0 // Loop to first photo
  }
}

const previousPhoto = () => {
  if (currentLightboxIndex.value > 0) {
    currentLightboxIndex.value--
  } else {
    currentLightboxIndex.value = filteredPhotos.value.length - 1 // Loop to last photo
  }
}

const loadMorePhotos = () => {
  photosLoaded.value += 8 // Load 8 more photos
}

// Keyboard navigation for lightbox
const handleKeydown = (event: KeyboardEvent) => {
  if (!lightboxOpen.value) return
  
  switch (event.key) {
    case 'Escape':
      closeLightbox()
      break
    case 'ArrowLeft':
      previousPhoto()
      break
    case 'ArrowRight':
      nextPhoto()
      break
  }
}

onMounted(() => {
  document.addEventListener('keydown', handleKeydown)
  loadPhotos()
})

onBeforeUnmount(() => {
  document.removeEventListener('keydown', handleKeydown)
  document.body.style.overflow = 'auto' // Ensure scroll is restored
})
</script>

<style scoped>
/* Custom scrollbar for better aesthetics */
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

/* Smooth transitions */
.transition-all {
  transition-property: all;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
}

/* Grid responsive adjustments */
@media (max-width: 640px) {
  .grid-cols-1 {
    grid-template-columns: repeat(1, minmax(0, 1fr));
  }
}

@media (min-width: 641px) and (max-width: 1024px) {
  .sm\:grid-cols-2 {
    grid-template-columns: repeat(2, minmax(0, 1fr));
  }
}
</style>