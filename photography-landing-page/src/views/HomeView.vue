
<script setup lang="ts">
import { ref } from 'vue'
import HeroSection from '@/modules/hero/components/hero-section.vue';
import GallerySection from '@/modules/shots/components/gallery-section.vue';
import AboutSection from '@/modules/about/about-section.vue';
import ContactSection from '@/modules/contacts/contact.vue';

const photographers = ref([
	{
		id: 1,
		name: "Nuru",
		studio: "Nuru's Touch",
		description: "Specializing in romantic wedding photography and intimate portrait sessions with a focus on natural lighting and candid moments.",
		coverImage: "/wedding.jpg",
		logo: "/man-lens.jpg",
		rating: 4.9,
		specializations: ["Wedding", "Portrait", "Event", "Fashion"],
		projectsCompleted: 500,
		experience: 5,
		location: "Banjul"
	},
	{
		id: 2,
		name: "Fatou Ceesay",
		studio: "Fatou Studios",
		description: "Creative portrait photographer known for vibrant family sessions and professional corporate headshots with artistic flair.",
		coverImage: "/portrait.jpg",
		logo: "/shot-2.jpg",
		rating: 4.8,
		specializations: ["Portrait", "Family", "Corporate", "Fashion"],
		projectsCompleted: 350,
		experience: 4,
		location: "Serrekunda"
	},
	{
		id: 3,
		name: "Omar Bah",
		studio: "Bah Photography",
		description: "Event specialist capturing corporate functions, cultural celebrations, and commercial photography with dynamic storytelling.",
		coverImage: "/event.jpg",
		logo: "/shot-5.jpg",
		rating: 4.7,
		specializations: ["Event", "Corporate", "Commercial", "Documentary"],
		projectsCompleted: 400,
		experience: 6,
		location: "Brikama"
	},
	{
		id: 4,
		name: "Aminata Jallow",
		studio: "Jallow Lens",
		description: "Fine art photographer specializing in creative portraits, fashion photography, and unique conceptual shoots.",
		coverImage: "/shot-1.jpg",
		logo: "/shot-3.jpg",
		rating: 4.9,
		specializations: ["Fashion", "Portrait", "Art", "Creative"],
		projectsCompleted: 280,
		experience: 3,
		location: "Bakau"
	},
	{
		id: 5,
		name: "Lamin Sanyang",
		studio: "Sanyang Studios",
		description: "Wedding and event photographer with expertise in traditional ceremonies and modern celebrations throughout Gambia.",
		coverImage: "/shot-4.jpg",
		logo: "/nature-shot.jpg",
		rating: 4.8,
		specializations: ["Wedding", "Traditional", "Event", "Cultural"],
		projectsCompleted: 450,
		experience: 7,
		location: "Gunjur"
	},
	{
		id: 6,
		name: "Isatou Darboe",
		studio: "Darboe Creative",
		description: "Contemporary photographer focusing on lifestyle shoots, brand photography, and social media content creation.",
		coverImage: "/shot-2.jpg",
		logo: "/lens.jpg",
		rating: 4.6,
		specializations: ["Lifestyle", "Brand", "Social Media", "Product"],
		projectsCompleted: 200,
		experience: 2,
		location: "Kanifing"
	}
])
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
			
			<div class="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
				<div v-for="photographer in photographers" :key="photographer.id" 
					 class="bg-white rounded-2xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow duration-300 group">
					<!-- Photographer Image -->
					<div class="relative h-64 overflow-hidden">
						<img :src="photographer.coverImage" 
							 class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" 
							 :alt="photographer.name" />
						<div class="absolute inset-0 bg-gradient-to-t from-black/60 to-transparent"></div>
						
						<!-- Logo/Avatar -->
						<div class="absolute bottom-4 left-4">
							<img :src="photographer.logo" 
								 class="w-16 h-16 rounded-full border-4 border-white shadow-lg object-cover" 
								 :alt="photographer.studio" />
						</div>
						
						<!-- Rating -->
						<div class="absolute top-4 right-4 bg-white/90 backdrop-blur-sm rounded-full px-3 py-1">
							<div class="flex items-center gap-1">
								<span class="text-yellow-500">⭐</span>
								<span class="text-sm font-semibold text-gray-800">{{ photographer.rating }}</span>
							</div>
						</div>
					</div>
					
					<!-- Content -->
					<div class="p-6">
						<div class="flex items-center justify-between mb-3">
							<h3 class="text-xl font-bold text-gray-800">{{ photographer.name }}</h3>
							<span class="text-sm text-[#7b1e3a] font-semibold">{{ photographer.studio }}</span>
						</div>
						
						<p class="text-gray-600 text-sm mb-4 line-clamp-2">{{ photographer.description }}</p>
						
						<!-- Specializations -->
						<div class="flex flex-wrap gap-2 mb-4">
							<span v-for="spec in photographer.specializations.slice(0, 3)" :key="spec" 
								  class="px-2 py-1 bg-gray-100 text-gray-700 rounded-full text-xs">
								{{ spec }}
							</span>
						</div>
						
						<!-- Stats -->
						<div class="flex justify-between items-center mb-4 text-sm text-gray-500">
							<span>{{ photographer.projectsCompleted }}+ Projects</span>
							<span>{{ photographer.experience }} Years</span>
							<span>{{ photographer.location }}</span>
						</div>
						
						<!-- Action Button -->
						<router-link :to="`/photographer/${photographer.id}`" 
									 class="block w-full bg-[#7b1e3a] hover:bg-[#5c162c] text-white text-center py-3 rounded-lg font-semibold transition-colors">
							View Portfolio
						</router-link>
					</div>
				</div>
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
