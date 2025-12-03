
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useSupabase } from '@/composables/useSupabase'
import HeroSection from '@/modules/hero/components/hero-section.vue';
import GallerySection from '@/modules/shots/components/gallery-section.vue';
import AboutSection from '@/modules/about/about-section.vue';
import ContactSection from '@/modules/contacts/contact.vue';

interface Profile {
	id: string
	full_name?: string
	studio_name?: string
	description?: string
	cover_image_url?: string
	avatar_url?: string
	rating?: number
	specialization?: string
	total_projects?: number
	experience?: number
	location?: string
	services_offered?: string
	role?: string
}

interface Photographer {
	id: string
	name: string
	studio: string
	description: string
	coverImage: string
	logo: string
	rating: number
	specializations: string[]
	projectsCompleted: number
	experience: number
	location: string
	type: string
}

const { fetchData } = useSupabase()
const isLoading = ref(false)

const photographers = ref<Photographer[]>([])

// Load photographers from Supabase
const loadFeaturedPhotographers = async () => {
	isLoading.value = true
	try {
		// Fetch photographer profiles from database
		const profiles = await fetchData('profiles', {
			role: 'photographer'
		})
		
		if (profiles && profiles.length > 0) {
			// Transform data to match component structure and limit to 6 for homepage
			photographers.value = (profiles as Profile[]).slice(0, 6).map((profile: Profile): Photographer => ({
				id: profile.id,
				name: profile.full_name || 'Photographer',
				studio: profile.studio_name || 'Photography Studio',
				description: profile.description || 'Professional photographer specializing in various styles.',
				coverImage: profile.cover_image_url || '/wedding.jpg',
				logo: profile.avatar_url || '/man-lens.jpg',
				rating: profile.rating || 4.8,
				specializations: profile.specialization ? [profile.specialization] : ['Portrait'],
				projectsCompleted: profile.total_projects || 100,
				experience: profile.experience || 3,
				location: profile.location || 'Gambia',
				type: profile.services_offered || 'Photography'
			}))
		} 
	} catch (err) {
		console.error('Error loading photographers:', err)
		// Use fallback data on error
	} finally {
		isLoading.value = false
	}
}

// Load data on mount
onMounted(() => {
	loadFeaturedPhotographers()
})
</script>

<template>
	<HeroSection/>
	
	<!-- About/Features Section -->
	<section class="py-20 bg-white">
		<div class="max-w-6xl mx-auto px-6">
			<div class="text-center mb-16">
				<h2 class="text-4xl font-bold text-gray-800 mb-4">Why Choose Our Photography Platform?</h2>
				<p class="text-xl text-gray-600 max-w-3xl mx-auto">Connect with Gambia's most talented photographers and capture your most precious moments with professional excellence.</p>
			</div>
			
			<div class="grid md:grid-cols-3 gap-8 text-black">
				<div class="text-center p-6">
					<div class="w-16 h-16 bg-[#7b1e3a] rounded-full flex items-center justify-center mx-auto mb-4">
						<span class="text-white text-2xl">📸</span>
					</div>
					<h3 class="text-xl font-semibold mb-3">Professional Quality</h3>
					<p class="text-gray-600">Work with certified photographers who deliver exceptional quality and attention to detail in every shot.</p>
				</div>
				
				<div class="text-center p-6">
					<div class="w-16 h-16 bg-[#7b1e3a] rounded-full flex items-center justify-center mx-auto mb-4">
						<span class="text-white text-2xl">⚡</span>
					</div>
					<h3 class="text-xl font-semibold mb-3">Easy Booking</h3>
					<p class="text-gray-600">Simple, streamlined booking process with real-time availability and instant confirmation for your convenience.</p>
				</div>
				
				<div class="text-center p-6">
					<div class="w-16 h-16 bg-[#7b1e3a] rounded-full flex items-center justify-center mx-auto mb-4">
						<span class="text-white text-2xl">💎</span>
					</div>
					<h3 class="text-xl font-semibold mb-3">Diverse Styles</h3>
					<p class="text-gray-600">From wedding photography to corporate events, find specialists for every occasion and photography style.</p>
				</div>
			</div>
		</div>
	</section>

	<!-- Photographers Showcase Section -->
	<section class="py-20 bg-gray-50">
		<div class="max-w-6xl mx-auto px-6">
			<div class="text-center mb-16">
				<h2 class="text-4xl font-bold text-gray-800 mb-4">Our Featured Photographers</h2>
				<p class="text-xl text-gray-600">Discover talented photographers ready to capture your special moments</p>
			</div>
			
			<!-- Loading State -->
			<div v-if="isLoading" class="flex justify-center items-center py-12">
				<div class="flex flex-col items-center gap-4">
					<div class="loading loading-spinner loading-lg text-[#7b1e3a]"></div>
					<p class="text-gray-600">Loading photographers...</p>
				</div>
			</div>
			
			<!-- Empty State -->
			<div v-else-if="photographers.length === 0" class="text-center py-12">
				<div class="text-6xl mb-4">📷</div>
				<h3 class="text-xl font-semibold text-gray-800 mb-2">No Photographers Available</h3>
				<p class="text-gray-600 mb-6">We're working on adding talented photographers to our platform.</p>
				<router-link to="/register" class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white">
					Join as Photographer
				</router-link>
			</div>
			
			<!-- Photographers Grid -->
			<div v-else class="grid grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-4">
				<router-link 
					v-for="photographer in photographers" 
					:key="photographer.id" 
					:to="`/photographers/${photographer.id}`"
					 class="text-center group cursor-pointer block">
					<!-- Logo -->
					<div class="mb-2">
						<img :src="photographer.logo" 
							 class="w-14 h-14 md:w-16 md:h-16 rounded-full mx-auto object-cover shadow-md group-hover:shadow-lg transition-shadow duration-300" 
							 :alt="photographer.name" />
					</div>
					
					<!-- Name -->
					<h3 class="text-sm md:text-base font-semibold text-gray-800 group-hover:text-[#7b1e3a] transition-colors">
						{{ photographer.name }}
					</h3>
					
					<!-- Type -->
					<p class="text-xs text-gray-600 mt-1">
						{{ photographer.type }}
					</p>
				</router-link>
			</div>
			
			<!-- View All Button -->
			<div class="text-center mt-12">
				<router-link to="/photographers" 
							 class="inline-block bg-white border-2 border-[#7b1e3a] text-[#7b1e3a] hover:bg-[#7b1e3a] hover:text-white px-8 py-3 rounded-lg font-semibold transition-colors">
					View All Photographers
				</router-link>
			</div>
		</div>
	</section>

	<!-- Services Overview -->
	<section class="py-20 bg-white text-black">
		<div class="max-w-6xl mx-auto px-6">
			<div class="text-center mb-16">
				<h2 class="text-4xl font-bold text-gray-800 mb-4">Photography Services</h2>
				<p class="text-xl text-gray-600">Professional photography services for every occasion</p>
			</div>
			
			<div class="grid md:grid-cols-2 lg:grid-cols-4 gap-6">
				<div class="text-center p-6 rounded-xl hover:shadow-lg transition-shadow">
					<div class="w-20 h-20 mx-auto mb-4 rounded-full bg-gradient-to-br from-[#7b1e3a] to-[#a02344] flex items-center justify-center">
						<span class="text-white text-3xl">💒</span>
					</div>
					<h4 class="text-lg font-semibold mb-2">Wedding Photography</h4>
					<p class="text-gray-600 text-sm">Capture your special day with romantic and elegant wedding photography.</p>
				</div>
				
				<div class="text-center p-6 rounded-xl hover:shadow-lg transition-shadow">
					<div class="w-20 h-20 mx-auto mb-4 rounded-full bg-gradient-to-br from-[#7b1e3a] to-[#a02344] flex items-center justify-center">
						<span class="text-white text-3xl">👨‍👩‍👧‍👦</span>
					</div>
					<h4 class="text-lg font-semibold mb-2">Portrait Sessions</h4>
					<p class="text-gray-600 text-sm">Professional portraits for individuals, families, and corporate headshots.</p>
				</div>
				
				<div class="text-center p-6 rounded-xl hover:shadow-lg transition-shadow">
					<div class="w-20 h-20 mx-auto mb-4 rounded-full bg-gradient-to-br from-[#7b1e3a] to-[#a02344] flex items-center justify-center">
						<span class="text-white text-3xl">🎉</span>
					</div>
					<h4 class="text-lg font-semibold mb-2">Event Photography</h4>
					<p class="text-gray-600 text-sm">Document your corporate events, parties, and special celebrations.</p>
				</div>
				
				<div class="text-center p-6 rounded-xl hover:shadow-lg transition-shadow">
					<div class="w-20 h-20 mx-auto mb-4 rounded-full bg-gradient-to-br from-[#7b1e3a] to-[#a02344] flex items-center justify-center">
						<span class="text-white text-3xl">🏢</span>
					</div>
					<h4 class="text-lg font-semibold mb-2">Commercial</h4>
					<p class="text-gray-600 text-sm">Product photography, brand shoots, and commercial photography services.</p>
				</div>
			</div>
		</div>
	</section>

	<GallerySection/>
	
	<!-- Call to Action Section -->
	<section class="py-20 bg-[#7b1e3a] text-white">
		<div class="max-w-4xl mx-auto px-6 text-center">
			<h2 class="text-4xl font-bold mb-4">Ready to Capture Your Story?</h2>
			<p class="text-xl mb-8 text-pink-100">Browse our talented photographers and book your perfect session today.</p>
			<div class="flex flex-col sm:flex-row gap-4 justify-center">
				<router-link to="/photographers" 
							 class="bg-white text-[#7b1e3a] hover:bg-gray-100 px-8 py-3 rounded-lg font-semibold transition-colors">
					Browse Photographers
				</router-link>
				<router-link to="/contact" 
							 class="border-2 border-white text-white hover:bg-white hover:text-[#7b1e3a] px-8 py-3 rounded-lg font-semibold transition-colors">
					Get in Touch
				</router-link>
			</div>
		</div>
	</section>
</template>

<style scoped>
.line-clamp-2 {
	display: -webkit-box;
	-webkit-line-clamp: 2;
	line-clamp: 2;
	-webkit-box-orient: vertical;
	overflow: hidden;
}
</style>
