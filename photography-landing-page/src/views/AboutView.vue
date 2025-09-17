<template>
  <div class="min-h-screen bg-gradient-to-br from-gray-50 to-white">
    <!-- Hero Section -->
    <section class="relative py-20 px-6 overflow-hidden">
      <div class="max-w-6xl mx-auto">
        <!-- Hero Content -->
        <div class="grid lg:grid-cols-2 gap-12 items-center">
          <div class="space-y-8">
            <div>
              <span class="inline-block px-4 py-2 bg-[#7b1e3a]/10 text-[#7b1e3a] rounded-full text-sm font-semibold mb-4">
                Our Story
              </span>
              <h1 class="text-4xl lg:text-6xl font-bold text-gray-900 leading-tight">
                Know Us
              </h1>
              <h2 class="text-xl lg:text-2xl text-gray-600 mt-4">
                Capturing life's most precious moments with passion and artistry
              </h2>
            </div>
            
            <p class="text-lg text-gray-700 leading-relaxed">
              At SweetShots, we believe that every moment tells a story. Founded with a passion for 
              capturing the beauty in everyday life, we've dedicated ourselves to preserving memories 
              that will last a lifetime.
            </p>

            <div class="flex flex-wrap gap-4">
              <RouterLink to="/contact" 
                class="px-8 py-3 bg-[#7b1e3a] text-white rounded-full font-semibold hover:bg-[#5c162c] transition-colors shadow-lg">
                Work With Us
              </RouterLink>
              <RouterLink to="/shots" 
                class="px-8 py-3 border-2 border-[#7b1e3a] text-[#7b1e3a] rounded-full font-semibold hover:bg-[#7b1e3a] hover:text-white transition-colors">
                View Our Work
              </RouterLink>
            </div>
          </div>

          <div class="relative">
            <div class="relative z-10">
              <img src="/man-lens.jpg" alt="Our photographer at work" 
                class="w-full h-96 object-cover rounded-2xl shadow-2xl">
            </div>
            <!-- Decorative elements -->
            <div class="absolute -top-4 -right-4 w-32 h-32 bg-[#7b1e3a]/20 rounded-full blur-xl"></div>
            <div class="absolute -bottom-4 -left-4 w-24 h-24 bg-pink-300/30 rounded-full blur-lg"></div>
          </div>
        </div>
      </div>
    </section>

    <!-- Stats Section -->
    <section class="py-16 px-6 bg-white">
      <div class="max-w-6xl mx-auto">
        <div class="grid grid-cols-2 lg:grid-cols-4 gap-8">
          <div class="text-center">
            <div class="text-4xl font-bold text-[#7b1e3a] mb-2">500+</div>
            <div class="text-gray-600 font-medium">Happy Clients</div>
          </div>
          <div class="text-center">
            <div class="text-4xl font-bold text-[#7b1e3a] mb-2">2000+</div>
            <div class="text-gray-600 font-medium">Photos Captured</div>
          </div>
          <div class="text-center">
            <div class="text-4xl font-bold text-[#7b1e3a] mb-2">5+</div>
            <div class="text-gray-600 font-medium">Years Experience</div>
          </div>
          <div class="text-center">
            <div class="text-4xl font-bold text-[#7b1e3a] mb-2">50+</div>
            <div class="text-gray-600 font-medium">Events Covered</div>
          </div>
        </div>
      </div>
    </section>

    <!-- Mission & Values -->
    <section class="py-20 px-6">
      <div class="max-w-6xl mx-auto">
        <div class="grid lg:grid-cols-2 gap-16 items-center">
          <div class="space-y-8">
            <div>
              <h3 class="text-3xl font-bold text-gray-900 mb-6">Our Mission</h3>
              <p class="text-lg text-gray-700 leading-relaxed mb-6">
                To create timeless photographs that capture the essence of every moment, 
                preserving emotions and memories for generations to come.
              </p>
            </div>

            <div class="space-y-6">
              <h4 class="text-2xl font-semibold text-gray-900">Our Values</h4>
              <div class="space-y-4">
                <div class="flex items-start gap-4">
                  <div class="w-8 h-8 bg-[#7b1e3a] rounded-full flex items-center justify-center flex-shrink-0 mt-1">
                    <i class="fas fa-heart text-white text-sm"></i>
                  </div>
                  <div>
                    <h5 class="font-semibold text-gray-900 mb-1">Passion</h5>
                    <p class="text-gray-600">We pour our heart into every shot, ensuring each image tells a unique story.</p>
                  </div>
                </div>
                <div class="flex items-start gap-4">
                  <div class="w-8 h-8 bg-[#7b1e3a] rounded-full flex items-center justify-center flex-shrink-0 mt-1">
                    <i class="fas fa-eye text-white text-sm"></i>
                  </div>
                  <div>
                    <h5 class="font-semibold text-gray-900 mb-1">Vision</h5>
                    <p class="text-gray-600">We see beauty in every moment and capture it with artistic excellence.</p>
                  </div>
                </div>
                <div class="flex items-start gap-4">
                  <div class="w-8 h-8 bg-[#7b1e3a] rounded-full flex items-center justify-center flex-shrink-0 mt-1">
                    <i class="fas fa-handshake text-white text-sm"></i>
                  </div>
                  <div>
                    <h5 class="font-semibold text-gray-900 mb-1">Trust</h5>
                    <p class="text-gray-600">Building lasting relationships with our clients through reliability and quality.</p>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="relative">
            <img src="/portrait.jpg" alt="Our mission in action" 
              class="w-full h-96 object-cover rounded-2xl shadow-xl">
            <div class="absolute inset-0 bg-gradient-to-tr from-[#7b1e3a]/20 to-transparent rounded-2xl"></div>
          </div>
        </div>
      </div>
    </section>

    <!-- Team Section -->
    <section class="py-20 px-6 bg-gray-50">
      <div class="max-w-6xl mx-auto text-center">
        <h3 class="text-3xl font-bold text-gray-900 mb-4">Meet Our Team</h3>
        <p class="text-lg text-gray-600 mb-12 max-w-2xl mx-auto">
          Our passionate team of photographers brings years of experience and a keen eye for detail to every project.
        </p>

        <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-8">
          <div class="bg-white rounded-2xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow">
            <img src="/shot-1.jpg" alt="Team member" class="w-full h-64 object-cover">
            <div class="p-6">
              <h4 class="text-xl font-semibold text-gray-900 mb-2">Sarah Johnson</h4>
              <p class="text-[#7b1e3a] font-medium mb-3">Lead Photographer</p>
              <p class="text-gray-600 text-sm">Specializes in wedding and portrait photography with 8+ years of experience.</p>
            </div>
          </div>

          <div class="bg-white rounded-2xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow">
            <img src="/shot-2.jpg" alt="Team member" class="w-full h-64 object-cover">
            <div class="p-6">
              <h4 class="text-xl font-semibold text-gray-900 mb-2">Michael Chen</h4>
              <p class="text-[#7b1e3a] font-medium mb-3">Event Photographer</p>
              <p class="text-gray-600 text-sm">Expert in capturing candid moments and the energy of special events.</p>
            </div>
          </div>

          <div class="bg-white rounded-2xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow">
            <img src="/shot-3.jpg" alt="Team member" class="w-full h-64 object-cover">
            <div class="p-6">
              <h4 class="text-xl font-semibold text-gray-900 mb-2">Emma Davis</h4>
              <p class="text-[#7b1e3a] font-medium mb-3">Creative Director</p>
              <p class="text-gray-600 text-sm">Brings artistic vision and ensures every project meets our high standards.</p>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- CTA Section -->
    <section class="py-20 px-6 bg-[#7b1e3a] text-white">
      <div class="max-w-4xl mx-auto text-center">
        <h3 class="text-3xl lg:text-4xl font-bold mb-6">Ready to Create Beautiful Memories?</h3>
        <p class="text-xl text-pink-100 mb-8 leading-relaxed">
          Let us capture your special moments with the care and artistry they deserve.
        </p>
        <div class="flex flex-col sm:flex-row gap-4 justify-center">
          <RouterLink to="/contact" 
            class="px-8 py-4 bg-white text-[#7b1e3a] rounded-full font-semibold hover:bg-gray-100 transition-colors shadow-lg">
            Get in Touch
          </RouterLink>
          <RouterLink to="/photographers" 
            class="px-8 py-4 border-2 border-white text-white rounded-full font-semibold hover:bg-white hover:text-[#7b1e3a] transition-colors">
            View Our Photographers
          </RouterLink>
        </div>
      </div>
    </section>
  </div>
</template>

<style scoped>
/* Add any custom animations or effects here */
.fade-in {
  animation: fadeIn 0.6s ease-out;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>
