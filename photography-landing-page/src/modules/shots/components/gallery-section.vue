<script setup>
import { ref, onMounted } from 'vue'
import { useSupabase } from '@/composables/useSupabase'

const { fetchData } = useSupabase()
const galleryPhotos = ref([])
const isLoading = ref(false)

// Load featured photos from database
const loadFeaturedPhotos = async () => {
  isLoading.value = true
  try {
    // Fetch portfolio items with photos
    const portfolioItems = await fetchData('portfolio_items', { is_featured: true })
    
    if (portfolioItems && portfolioItems.length > 0) {
      // Transform data to match gallery structure
      galleryPhotos.value = portfolioItems.slice(0, 6).map((item, index) => {
        const categories = ['Wedding Photography', 'Portrait Sessions', 'Events', 'Nature', 'Fashion & Lifestyle', 'Commercial']
        const descriptions = ['Romantic & Elegant', 'Professional & Personal', 'Corporate & Social', 'Landscapes & Wildlife', 'Creative & Artistic', 'Products & Brands']
        
        return {
          id: item.id,
          src: item.image_url || `/shot-${index + 1}.jpg`,
          alt: item.title || categories[index % categories.length],
          category: item.category || categories[index % categories.length],
          description: descriptions[index % descriptions.length],
          gridClass: getGridClass(index)
        }
      })
    } else {
      // Fallback to default images
      loadDefaultPhotos()
    }
  } catch (err) {
    console.error('Error loading gallery photos:', err)
    loadDefaultPhotos()
  } finally {
    isLoading.value = false
  }
}

const getGridClass = (index) => {
  const classes = [
    'col-span-2 row-span-1', // Large horizontal
    'col-span-1 row-span-2', // Tall vertical
    'col-span-1 row-span-1', // Regular
    'col-span-1 row-span-1', // Regular
    'col-span-2 row-span-1', // Large horizontal
    'col-span-1 row-span-1'  // Regular
  ]
  return classes[index % classes.length]
}

const loadDefaultPhotos = () => {
  galleryPhotos.value = [
    {
      id: 1,
      src: '/shot-1.jpg',
      alt: 'Wedding Photography',
      category: 'Wedding Photography',
      description: 'Romantic & Elegant',
      gridClass: 'col-span-2 row-span-1'
    },
    {
      id: 2,
      src: '/shot-2.jpg',
      alt: 'Portrait Session',
      category: 'Portrait Sessions',
      description: 'Professional & Personal',
      gridClass: 'col-span-1 row-span-2'
    },
    {
      id: 3,
      src: '/shot-3.jpg',
      alt: 'Event Photography',
      category: 'Events',
      description: 'Corporate & Social',
      gridClass: 'col-span-1 row-span-1'
    },
    {
      id: 4,
      src: '/nature-shot.jpg',
      alt: 'Nature Photography',
      category: 'Nature',
      description: 'Landscapes & Wildlife',
      gridClass: 'col-span-1 row-span-1'
    },
    {
      id: 5,
      src: '/shot-4.jpg',
      alt: 'Fashion Photography',
      category: 'Fashion & Lifestyle',
      description: 'Creative & Artistic',
      gridClass: 'col-span-2 row-span-1'
    },
    {
      id: 6,
      src: '/shot-5.jpg',
      alt: 'Commercial Photography',
      category: 'Commercial',
      description: 'Products & Brands',
      gridClass: 'col-span-1 row-span-1'
    }
  ]
}

onMounted(() => {
  loadFeaturedPhotos()
})
</script>

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
        
        <!-- Loading State -->
        <div v-if="isLoading" class="flex justify-center items-center py-12">
          <div class="flex flex-col items-center gap-4">
            <div class="loading loading-spinner loading-lg text-[#7b1e3a]"></div>
            <p class="text-gray-600">Loading gallery...</p>
          </div>
        </div>
        
        <!-- Gallery Grid -->
        <div v-else class="grid grid-cols-2 md:grid-cols-3 gap-4 auto-rows-[200px] md:auto-rows-[300px]">
          <div 
            v-for="photo in galleryPhotos" 
            :key="photo.id" 
            :class="photo.gridClass + ' group'"
          >
            <div class="relative h-full overflow-hidden rounded-2xl shadow-lg">
              <img 
                :src="photo.src" 
                :alt="photo.alt" 
                class="bento-img"
              >
              <div class="absolute inset-0 bg-black/40 opacity-0 group-hover:opacity-100 transition-opacity duration-300 flex items-center justify-center">
                <div class="text-white text-center">
                  <h3 class="text-lg md:text-xl font-semibold mb-1 md:mb-2">{{ photo.category }}</h3>
                  <p class="text-xs md:text-sm">{{ photo.description }}</p>
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
  