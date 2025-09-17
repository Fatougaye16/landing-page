<template>
    <div class="min-h-screen bg-gray-50">
        <!-- Hero Section -->
        <div class="relative h-96 md:h-[500px] overflow-hidden">
            <!-- Cover Image -->
            <div class="absolute inset-0 bg-gradient-to-r from-black/70 to-black/40">
                <img :src="photographer.gallery[0].src" class="w-full h-full object-cover" alt="cover" />
            </div>
            
            <!-- Hero Content -->
            <div class="relative z-10 max-w-6xl mx-auto px-6 h-full flex items-center">
                <div class="flex flex-col md:flex-row items-center md:items-end gap-6 w-full">
                    <!-- Profile Image -->
                    <div class="relative">
                        <img :src="photographer.image" 
                             class="w-32 h-32 md:w-40 md:h-40 rounded-full object-cover border-4 border-white shadow-2xl" 
                             alt="profile" />
                        <div class="absolute -bottom-2 -right-2 bg-green-500 w-8 h-8 rounded-full border-4 border-white flex items-center justify-center">
                            <span class="text-white text-xs">✓</span>
                        </div>
                    </div>
                    
                    <!-- Profile Info -->
                    <div class="flex-1 text-center md:text-left text-white">
                        <h1 class="text-4xl md:text-5xl font-bold mb-2">{{ photographer.name }}</h1>
                        <p class="text-xl md:text-2xl font-light mb-3 text-orange-200">{{ photographer.studio }}</p>
                        
                        <!-- Rating & Stats -->
                        <div class="flex flex-col md:flex-row items-center gap-4 mb-4">
                            <div class="flex items-center gap-1">
                                <span v-for="star in 5" :key="star" class="text-yellow-400 text-lg">⭐</span>
                                <span class="ml-2 font-semibold">4.9 (127 reviews)</span>
                            </div>
                            <div class="hidden md:block w-px h-6 bg-white/30"></div>
                            <div class="flex gap-6 text-sm">
                                <div class="text-center">
                                    <div class="font-bold text-lg">500+</div>
                                    <div class="text-gray-300">Projects</div>
                                </div>
                                <div class="text-center">
                                    <div class="font-bold text-lg">5</div>
                                    <div class="text-gray-300">Years Exp.</div>
                                </div>
                            </div>
                        </div>
                        
                        <!-- Specializations -->
                        <div class="flex flex-wrap gap-2 justify-center md:justify-start">
                            <span class="px-3 py-1 bg-[#7b1e3a] text-white rounded-full text-sm">Wedding</span>
                            <span class="px-3 py-1 bg-[#7b1e3a] text-white rounded-full text-sm">Portrait</span>
                            <span class="px-3 py-1 bg-[#7b1e3a] text-white rounded-full text-sm">Event</span>
                        </div>
                    </div>
                    
                    <!-- Contact Actions -->
                    <div class="flex flex-col gap-3">
                        <button class="btn btn-primary bg-[#7b1e3a] border-[#7b1e3a] hover:bg-[#5c162c] text-white px-6">
                            <span class="text-lg">📞</span> Call Now
                        </button>
                        <button class="btn btn-outline border-white text-white hover:bg-white hover:text-gray-800">
                            <span class="text-lg">💬</span> Message
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Contact Info Bar -->
        <div class="bg-white shadow-md py-4">
            <div class="max-w-6xl mx-auto px-6">
                <div class="flex flex-wrap items-center justify-center md:justify-between gap-4 text-gray-600">
                    <div class="flex items-center gap-2">
                        <span class="text-[#7b1e3a]">📍</span>
                        <span>{{ photographer.address }}</span>
                    </div>
                    <div class="flex items-center gap-2">
                        <span class="text-[#7b1e3a]">📞</span>
                        <span>{{ photographer.phone }}</span>
                    </div>
                    <div class="flex items-center gap-2">
                        <span class="text-[#7b1e3a]">✉️</span>
                        <span>{{ photographer.email }}</span>
                    </div>
                </div>
            </div>
        </div>

        <div class="max-w-6xl mx-auto p-6 space-y-12">

        <!-- About Section -->
        <div class="bg-white rounded-2xl shadow-lg p-8">
            <div class="grid md:grid-cols-2 gap-8 items-center">
                <div>
                    <h2 class="text-3xl font-bold text-gray-800 mb-4">About {{ photographer.name }}</h2>
                    <p class="text-gray-600 leading-relaxed mb-6">
                        With over 5 years of experience capturing life's most precious moments, I specialize in creating 
                        timeless memories through my lens. My passion for photography began during my university years, 
                        and since then, I've had the privilege of documenting hundreds of weddings, portraits, and special events.
                    </p>
                    <div class="grid grid-cols-2 gap-4">
                        <div class="text-center p-4 bg-gray-50 rounded-lg">
                            <div class="text-2xl font-bold text-[#7b1e3a]">500+</div>
                            <div class="text-sm text-gray-600">Happy Clients</div>
                        </div>
                        <div class="text-center p-4 bg-gray-50 rounded-lg">
                            <div class="text-2xl font-bold text-[#7b1e3a]">1000+</div>
                            <div class="text-sm text-gray-600">Photos Taken</div>
                        </div>
                    </div>
                </div>
                <div class="relative">
                    <img :src="photographer.image" class="w-full h-80 object-cover rounded-xl shadow-lg" alt="about" />
                    <div class="absolute inset-0 bg-gradient-to-t from-black/30 to-transparent rounded-xl"></div>
                </div>
            </div>
        </div>

        <!-- Enhanced Booking System -->
        <div id="booking-section" class="bg-white rounded-2xl shadow-lg p-8">
            <div class="text-center mb-8">
                <h2 class="text-3xl font-bold text-gray-800 mb-4">Book Your Session</h2>
                <p class="text-gray-600">Choose your preferred date, time, and package to get started</p>
            </div>

            <!-- Booking Steps Indicator -->
            <div class="flex items-center justify-center mb-8">
                <div class="flex items-center space-x-4">
                    <div class="flex items-center">
                        <div :class="['w-8 h-8 rounded-full flex items-center justify-center text-sm font-semibold transition',
                                     bookingStep >= 1 ? 'bg-[#7b1e3a] text-white' : 'bg-gray-200 text-gray-500']">1</div>
                        <span class="ml-2 text-sm font-medium text-gray-600">Date</span>
                    </div>
                    <div class="w-8 h-px bg-gray-200"></div>
                    <div class="flex items-center">
                        <div :class="['w-8 h-8 rounded-full flex items-center justify-center text-sm font-semibold transition',
                                     bookingStep >= 2 ? 'bg-[#7b1e3a] text-white' : 'bg-gray-200 text-gray-500']">2</div>
                        <span class="ml-2 text-sm font-medium text-gray-600">Time</span>
                    </div>
                    <div class="w-8 h-px bg-gray-200"></div>
                    <div class="flex items-center">
                        <div :class="['w-8 h-8 rounded-full flex items-center justify-center text-sm font-semibold transition',
                                     bookingStep >= 3 ? 'bg-[#7b1e3a] text-white' : 'bg-gray-200 text-gray-500']">3</div>
                        <span class="ml-2 text-sm font-medium text-gray-600">Details</span>
                    </div>
                </div>
            </div>

            <!-- Step 1: Date Selection -->
            <div v-if="bookingStep === 1" class="bg-gray-50 rounded-xl p-6">
                <h3 class="text-xl font-semibold mb-6 text-center text-gray-800">Select Your Preferred Date</h3>
                
                <!-- Calendar Header -->
                <div class="flex items-center justify-between mb-4">
                    <button @click="previousMonth" class="btn btn-circle btn-sm text-gray-700">‹</button>
                    <h4 class="text-lg font-semibold text-gray-800">{{ getMonthYear() }}</h4>
                    <button @click="nextMonth" class="btn btn-circle btn-sm text-gray-700">›</button>
                </div>
                
                <!-- Calendar Grid -->
                <div class="grid grid-cols-7 gap-2 mb-6">
                    <div v-for="day in weekDays" :key="day" class="text-center font-semibold text-gray-700 py-2">
                        {{ day }}
                    </div>
                    <div v-for="blank in getBlankDays()" :key="`blank-${blank}`" class="py-2"></div>
                    <button v-for="day in daysInMonth" :key="day" 
                            @click="selectDate(day)" 
                            :disabled="isDateDisabled(day)"
                            :class="[
                                'py-3 rounded-lg transition font-medium',
                                selectedDate === day ? 'bg-[#7b1e3a] text-white shadow-lg' : 
                                isDateDisabled(day) ? 'text-gray-400 cursor-not-allowed bg-gray-100' :
                                'hover:bg-[#7b1e3a] hover:text-white text-gray-800 bg-white border border-gray-200'
                            ]">
                        {{ day }}
                    </button>
                </div>
                
                <div class="text-center">
                    <button @click="nextStep" :disabled="!selectedDate" 
                            class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white px-8 disabled:opacity-50">
                        Continue to Time Selection
                    </button>
                </div>
            </div>

            <!-- Step 2: Time Selection -->
            <div v-if="bookingStep === 2" class="bg-gray-50 rounded-xl p-6">
                <h3 class="text-xl font-semibold mb-6 text-center text-gray-800">Choose Your Time Slot</h3>
                <p class="text-center text-gray-600 mb-6">
                    Selected Date: <span class="font-semibold text-[#7b1e3a]">{{ formatSelectedDate() }}</span>
                </p>
                
                <!-- Time Slots Grid -->
                <div class="grid grid-cols-2 md:grid-cols-3 gap-4 mb-6">
                    <button v-for="slot in availableTimeSlots" :key="slot.time" 
                            @click="selectTime(slot.time)"
                            :disabled="!slot.available"
                            :class="[
                                'py-4 px-4 rounded-lg border-2 transition text-center bg-white',
                                selectedTime === slot.time ? 
                                'border-[#7b1e3a] bg-[#7b1e3a] text-white shadow-lg' :
                                !slot.available ? 
                                'border-gray-200 bg-gray-100 text-gray-400 cursor-not-allowed' :
                                'border-gray-200 hover:border-[#7b1e3a] hover:text-[#7b1e3a] text-gray-800'
                            ]">
                        <div class="font-semibold">{{ slot.time }}</div>
                        <div class="text-xs mt-1">{{ slot.available ? 'Available' : 'Booked' }}</div>
                    </button>
                </div>
                
                <div class="flex gap-4 justify-center">
                    <button @click="previousStep" class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-100">Back</button>
                    <button @click="nextStep" :disabled="!selectedTime" 
                            class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white px-8 disabled:opacity-50">
                        Continue to Details
                    </button>
                </div>
            </div>

            <!-- Step 3: Booking Details -->
            <div v-if="bookingStep === 3" class="bg-gray-50 rounded-xl p-6">
                <h3 class="text-xl font-semibold mb-6 text-center text-gray-800">Complete Your Booking</h3>
                
                <!-- Booking Summary -->
                <div class="bg-white rounded-lg p-4 mb-6 border-l-4 border-[#7b1e3a]">
                    <h4 class="font-semibold mb-2 text-gray-800">Booking Summary</h4>
                    <div class="text-sm text-gray-600 space-y-1">
                        <p><span class="font-medium text-gray-700">Date:</span> {{ formatSelectedDate() }}</p>
                        <p><span class="font-medium text-gray-700">Time:</span> {{ selectedTime }}</p>
                        <p v-if="selectedPackage"><span class="font-medium text-gray-700">Package:</span> {{ selectedPackage }}</p>
                    </div>
                </div>
                
                <!-- Booking Form -->
                <div class="space-y-4 mb-6">
                    <div class="grid md:grid-cols-2 gap-4">
                        <input v-model="name" type="text" placeholder="Full Name" 
                               class="input input-bordered w-full focus:border-[#7b1e3a] bg-white text-gray-800" />
                        <input v-model="email" type="email" placeholder="Email Address" 
                               class="input input-bordered w-full focus:border-[#7b1e3a] bg-white text-gray-800" />
                    </div>
                    <input v-model="phone" type="tel" placeholder="Phone Number" 
                           class="input input-bordered w-full focus:border-[#7b1e3a] bg-white text-gray-800" />
                    
                    <!-- Package Selection -->
                    <select v-model="selectedPackage" class="select select-bordered w-full focus:border-[#7b1e3a] bg-white text-gray-800">
                        <option disabled value="" class="text-gray-500">Select a Package</option>
                        <option v-for="pkg in photographer.packages" :key="pkg.name" :value="pkg.name" class="text-gray-800">
                            {{ pkg.name }} - {{ pkg.price }}
                        </option>
                    </select>
                    
                    <!-- Special Requests -->
                    <textarea v-model="specialRequests" placeholder="Special requests or additional information..." 
                              class="textarea textarea-bordered w-full h-24 focus:border-[#7b1e3a] bg-white text-gray-800"></textarea>
                </div>
                
                <div class="flex gap-4 justify-center">
                    <button @click="previousStep" class="btn btn-outline border-gray-300 text-gray-700 hover:bg-gray-100">Back</button>
                    <button @click="bookAppointment" 
                            class="btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white px-8">
                        <span class="mr-2">📅</span> Confirm Booking
                    </button>
                </div>
            </div>
        </div>

        <!-- Portfolio Gallery -->
        <div class="bg-white rounded-2xl shadow-lg p-8">
            <div class="flex flex-col md:flex-row justify-between items-start md:items-center mb-8">
                <div>
                    <h2 class="text-3xl font-bold text-gray-800 mb-2">Portfolio Gallery</h2>
                    <p class="text-gray-600">Explore my recent work and creative vision</p>
                </div>
                
                <!-- Gallery Filters -->
                <div class="flex gap-2 mt-4 md:mt-0">
                    <button @click="selectedCategory = 'all'" 
                            :class="['px-4 py-2 rounded-full text-sm font-medium transition',
                                    selectedCategory === 'all' ? 'bg-[#7b1e3a] text-white' : 'bg-gray-100 text-gray-600 hover:bg-gray-200']">
                        All
                    </button>
                    <button @click="selectedCategory = 'wedding'" 
                            :class="['px-4 py-2 rounded-full text-sm font-medium transition',
                                    selectedCategory === 'wedding' ? 'bg-[#7b1e3a] text-white' : 'bg-gray-100 text-gray-600 hover:bg-gray-200']">
                        Wedding
                    </button>
                    <button @click="selectedCategory = 'portrait'" 
                            :class="['px-4 py-2 rounded-full text-sm font-medium transition',
                                    selectedCategory === 'portrait' ? 'bg-[#7b1e3a] text-white' : 'bg-gray-100 text-gray-600 hover:bg-gray-200']">
                        Portrait
                    </button>
                    <button @click="selectedCategory = 'event'" 
                            :class="['px-4 py-2 rounded-full text-sm font-medium transition',
                                    selectedCategory === 'event' ? 'bg-[#7b1e3a] text-white' : 'bg-gray-100 text-gray-600 hover:bg-gray-200']">
                        Events
                    </button>
                </div>
            </div>
            
            <!-- Enhanced Gallery Grid -->
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <div v-for="(img, index) in filteredGallery" :key="index" 
                     class="group relative overflow-hidden rounded-xl shadow-md hover:shadow-xl transition-all duration-300 cursor-pointer"
                     @click="openLightbox(index)">
                    <img :src="img.src" 
                         class="w-full h-64 md:h-72 object-cover group-hover:scale-110 transition-transform duration-500" 
                         :alt="img.category" />
                    
                    <!-- Overlay -->
                    <div class="absolute inset-0 bg-gradient-to-t from-black/70 via-transparent to-transparent opacity-0 group-hover:opacity-100 transition-opacity duration-300">
                        <div class="absolute bottom-4 left-4 text-white">
                            <div class="text-sm font-medium capitalize">{{ img.category }}</div>
                            <div class="text-xs text-gray-300">{{ img.title }}</div>
                        </div>
                        <div class="absolute top-4 right-4">
                            <div class="w-10 h-10 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center">
                                <span class="text-white text-lg">🔍</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- View More Button -->
            <div class="text-center mt-8">
                <button class="btn btn-outline border-[#7b1e3a] text-[#7b1e3a] hover:bg-[#7b1e3a] hover:text-white px-8">
                    View Full Portfolio
                </button>
            </div>
        </div>

        <!-- Lightbox Modal -->
        <div v-if="lightboxOpen" @click="closeLightbox" 
             class="fixed inset-0 z-50 bg-black/90 flex items-center justify-center p-4">
            <div class="relative max-w-4xl max-h-full">
                <img :src="filteredGallery[currentLightboxIndex]?.src" 
                     class="max-w-full max-h-full object-contain rounded-lg" 
                     :alt="filteredGallery[currentLightboxIndex]?.category" />
                
                <!-- Close Button -->
                <button @click="closeLightbox" 
                        class="absolute top-4 right-4 w-10 h-10 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center text-white hover:bg-white/30 transition">
                    ✕
                </button>
                
                <!-- Navigation Arrows -->
                <button v-if="currentLightboxIndex > 0" @click.stop="previousImage" 
                        class="absolute left-4 top-1/2 -translate-y-1/2 w-12 h-12 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center text-white hover:bg-white/30 transition">
                    ←
                </button>
                <button v-if="currentLightboxIndex < filteredGallery.length - 1" @click.stop="nextImage" 
                        class="absolute right-4 top-1/2 -translate-y-1/2 w-12 h-12 bg-white/20 backdrop-blur-sm rounded-full flex items-center justify-center text-white hover:bg-white/30 transition">
                    →
                </button>
            </div>
        </div>

        <!-- Packages Section -->
        <div class="bg-white rounded-2xl shadow-lg p-8">
            <div class="text-center mb-12">
                <h2 class="text-3xl font-bold text-gray-800 mb-4">Photography Packages</h2>
                <p class="text-gray-600 max-w-2xl mx-auto">Choose the perfect package for your special moment. Each package is carefully crafted to deliver exceptional value and unforgettable memories.</p>
            </div>
            
            <div class="grid md:grid-cols-2 lg:grid-cols-4 gap-6">
                <div v-for="(pkg, index) in photographer.packages" :key="index"
                     class="relative group bg-white border-2 border-gray-100 rounded-2xl shadow-lg hover:shadow-2xl hover:border-[#7b1e3a] transition-all duration-300 overflow-hidden">
                    
                    <!-- Popular Badge -->
                    <div v-if="pkg.popular" class="absolute top-4 right-4 z-10">
                        <span class="bg-gradient-to-r from-[#7b1e3a] to-[#a02344] text-white px-3 py-1 rounded-full text-xs font-semibold">
                            Most Popular
                        </span>
                    </div>
                    
                    <!-- Package Image -->
                    <div class="relative h-48 overflow-hidden">
                        <img :src="pkg.image" 
                             class="w-full h-full object-cover group-hover:scale-110 transition-transform duration-500" 
                             :alt="pkg.name" />
                        <div class="absolute inset-0 bg-gradient-to-t from-black/50 to-transparent"></div>
                    </div>
                    
                    <!-- Package Content -->
                    <div class="p-6">
                        <div class="text-center mb-6">
                            <h3 class="text-xl font-bold text-gray-800 mb-2">{{ pkg.name }}</h3>
                            <p class="text-gray-600 text-sm mb-4">{{ pkg.description }}</p>
                            <div class="text-3xl font-bold text-[#7b1e3a] mb-1">{{ pkg.price }}</div>
                            <div class="text-sm text-gray-500">Starting price</div>
                        </div>
                        
                        <!-- Features List -->
                        <div class="space-y-3 mb-6">
                            <div v-for="(feature, i) in pkg.features" :key="i" 
                                 class="flex items-center gap-3 text-sm">
                                <span class="w-5 h-5 bg-green-100 text-green-600 rounded-full flex items-center justify-center text-xs">✓</span>
                                <span class="text-gray-700">{{ feature }}</span>
                            </div>
                        </div>
                        
                        <!-- Action Buttons -->
                        <div class="space-y-2">
                            <button @click="selectPackageForBooking(pkg.name)" 
                                    class="w-full btn bg-[#7b1e3a] hover:bg-[#5c162c] text-white border-none">
                                Book Now
                            </button>
                            <button class="w-full btn btn-outline border-gray-300 text-gray-600 hover:border-[#7b1e3a] hover:text-[#7b1e3a]">
                                Learn More
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Package Comparison Note -->
            <div class="mt-12 text-center">
                <div class="bg-gray-50 rounded-xl p-6 max-w-2xl mx-auto">
                    <h4 class="font-semibold text-gray-800 mb-2">Need a Custom Package?</h4>
                    <p class="text-gray-600 text-sm mb-4">Every event is unique. Let's discuss your specific needs and create a personalized package just for you.</p>
                    <button class="btn btn-outline border-[#7b1e3a] text-[#7b1e3a] hover:bg-[#7b1e3a] hover:text-white">
                        Request Custom Quote
                    </button>
                </div>
            </div>
        </div>

        <!-- Client Reviews & Testimonials -->
        <div class="bg-white rounded-2xl shadow-lg p-8">
            <div class="text-center mb-12">
                <h2 class="text-3xl font-bold text-gray-800 mb-4">What Clients Say</h2>
                <div class="flex items-center justify-center gap-2 mb-4">
                    <div class="flex gap-1">
                        <span v-for="star in 5" :key="star" class="text-yellow-400 text-xl">⭐</span>
                    </div>
                    <span class="text-lg font-semibold text-gray-700">4.9 out of 5</span>
                    <span class="text-gray-500">(127 reviews)</span>
                </div>
            </div>
            
            <div class="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
                <div v-for="(review, index) in reviews" :key="index" 
                     class="bg-gray-50 rounded-xl p-6 hover:shadow-md transition-shadow">
                    <!-- Review Header -->
                    <div class="flex items-center gap-4 mb-4">
                        <img :src="review.avatar" class="w-12 h-12 rounded-full object-cover" :alt="review.name" />
                        <div>
                            <div class="font-semibold text-gray-800">{{ review.name }}</div>
                            <div class="text-sm text-gray-500">{{ review.event }}</div>
                        </div>
                    </div>
                    
                    <!-- Rating -->
                    <div class="flex gap-1 mb-3">
                        <span v-for="star in review.rating" :key="star" class="text-yellow-400">⭐</span>
                    </div>
                    
                    <!-- Review Text -->
                    <p class="text-gray-600 text-sm leading-relaxed mb-3">{{ review.comment }}</p>
                    
                    <!-- Review Date -->
                    <div class="text-xs text-gray-400">{{ review.date }}</div>
                </div>
            </div>
            
            <!-- View More Reviews -->
            <div class="text-center mt-8">
                <button class="btn btn-outline border-[#7b1e3a] text-[#7b1e3a] hover:bg-[#7b1e3a] hover:text-white">
                    View All Reviews
                </button>
            </div>
        </div>

        <!-- Achievements & Social Proof -->
        <div class="grid md:grid-cols-2 gap-6">
            <!-- Achievements -->
            <div class="bg-white rounded-2xl shadow-lg p-8">
                <h3 class="text-2xl font-bold text-gray-800 mb-6">Achievements & Recognition</h3>
                <div class="space-y-4">
                    <div class="flex items-center gap-4 p-4 bg-gray-50 rounded-lg">
                        <div class="w-12 h-12 bg-yellow-100 rounded-full flex items-center justify-center">
                            <span class="text-yellow-600 text-xl">🏆</span>
                        </div>
                        <div>
                            <div class="font-semibold text-gray-800">Wedding Photographer of the Year</div>
                            <div class="text-sm text-gray-600">Gambian Photography Awards 2024</div>
                        </div>
                    </div>
                    
                    <div class="flex items-center gap-4 p-4 bg-gray-50 rounded-lg">
                        <div class="w-12 h-12 bg-blue-100 rounded-full flex items-center justify-center">
                            <span class="text-blue-600 text-xl">📜</span>
                        </div>
                        <div>
                            <div class="font-semibold text-gray-800">Certified Professional Photographer</div>
                            <div class="text-sm text-gray-600">International Photography Institute</div>
                        </div>
                    </div>
                    
                    <div class="flex items-center gap-4 p-4 bg-gray-50 rounded-lg">
                        <div class="w-12 h-12 bg-green-100 rounded-full flex items-center justify-center">
                            <span class="text-green-600 text-xl">⭐</span>
                        </div>
                        <div>
                            <div class="font-semibold text-gray-800">Top Rated on WeddingWire</div>
                            <div class="text-sm text-gray-600">5.0 stars with 50+ reviews</div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Social Media & Contact -->
            <div class="bg-white rounded-2xl shadow-lg p-8">
                <h3 class="text-2xl font-bold text-gray-800 mb-6">Connect With Me</h3>
                
                <!-- Social Media -->
                <div class="space-y-4 mb-6">
                    <div class="flex items-center gap-4">
                        <div class="w-10 h-10 bg-pink-100 rounded-full flex items-center justify-center">
                            <span class="text-pink-600">📷</span>
                        </div>
                        <div class="flex-1">
                            <div class="font-semibold">Instagram</div>
                            <div class="text-sm text-gray-600">@nurutouch_photography</div>
                        </div>
                        <button class="btn btn-sm btn-outline border-pink-500 text-pink-600 hover:bg-pink-500 hover:text-white">
                            Follow
                        </button>
                    </div>
                    
                    <div class="flex items-center gap-4">
                        <div class="w-10 h-10 bg-blue-100 rounded-full flex items-center justify-center">
                            <span class="text-blue-600">👥</span>
                        </div>
                        <div class="flex-1">
                            <div class="font-semibold">Facebook</div>
                            <div class="text-sm text-gray-600">Nuru's Touch Photography</div>
                        </div>
                        <button class="btn btn-sm btn-outline border-blue-500 text-blue-600 hover:bg-blue-500 hover:text-white">
                            Like Page
                        </button>
                    </div>
                </div>
                
                <!-- Quick Stats -->
                <div class="bg-gray-50 rounded-lg p-4">
                    <h4 class="font-semibold mb-3">At a Glance</h4>
                    <div class="grid grid-cols-2 gap-4 text-center">
                        <div>
                            <div class="text-2xl font-bold text-[#7b1e3a]">500+</div>
                            <div class="text-xs text-gray-600">Happy Clients</div>
                        </div>
                        <div>
                            <div class="text-2xl font-bold text-[#7b1e3a]">5</div>
                            <div class="text-xs text-gray-600">Years Experience</div>
                        </div>
                        <div>
                            <div class="text-2xl font-bold text-[#7b1e3a]">4.9</div>
                            <div class="text-xs text-gray-600">Average Rating</div>
                        </div>
                        <div>
                            <div class="text-2xl font-bold text-[#7b1e3a]">48h</div>
                            <div class="text-xs text-gray-600">Response Time</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    </div>
</template>

<script setup>
import { computed, ref } from "vue";
import { useRoute } from "vue-router";
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
const route = useRoute();

// Mock photographer data
const photographers = [
    {
        id: 1,
        name: "Nuru",
        studio: "Nuru's Touch",
        phone: "+220 123 4567",
        address: "Kairaba Avenue, Banjul",
        email: "dio@nurutouch.com",
        image: "/man-lens.jpg",
        gallery: [
            { src: "/shot-1.jpg", category: "wedding", title: "Beautiful Wedding Ceremony" },
            { src: "/shot-2.jpg", category: "portrait", title: "Professional Portrait" },
            { src: "/shot-3.jpg", category: "event", title: "Corporate Event" },
            { src: "/shot-4.jpg", category: "wedding", title: "Wedding Reception" },
            { src: "/wedding.jpg", category: "wedding", title: "Romantic Couple" },
            { src: "/portrait.jpg", category: "portrait", title: "Studio Portrait" },
            { src: "/event.jpg", category: "event", title: "Conference Photography" },
            { src: "/shot-5.jpg", category: "portrait", title: "Outdoor Portrait" },
        ],
        packages: [
            {
                name: "Essential",
                description: "Perfect for intimate gatherings and small events",
                price: "D100",
                image: "/wedding.jpg",
                features: ["2 hours coverage", "10 edited photos", "Online gallery", "Email delivery"],
                popular: false,
            },
            {
                name: "Professional",
                description: "Most popular choice for weddings and events",
                price: "D200",
                image: "/event.jpg",
                features: ["4 hours coverage", "25 edited photos", "Premium album", "USB drive", "Print release"],
                popular: true,
            },
            {
                name: "Premium",
                description: "Ultimate package with full-day coverage",
                price: "D400",
                image: "/portrait.jpg",
                features: ["Full day coverage", "50 edited photos", "2 premium albums", "Canvas prints", "Engagement session"],
                popular: false,
            },
            {
                name: "Cinematic",
                description: "Video + photography combination package",
                price: "D700",
                image: "/shot-4.jpg",
                features: ["Full day coverage", "Video highlights", "50 edited photos", "4K video quality", "Drone footage"],
                popular: false,
            },
        ],
    },
];

// Mock reviews data
const reviews = [
    {
        name: "Fatou Jallow",
        event: "Wedding Photography",
        rating: 5,
        comment: "Nuru captured our special day perfectly! The photos are absolutely stunning and we couldn't be happier with the quality and professionalism.",
        avatar: "/portrait.jpg",
        date: "2 weeks ago"
    },
    {
        name: "Amie Ceesay",
        event: "Portrait Session",
        rating: 5,
        comment: "Amazing experience! Nuru made me feel comfortable throughout the session and delivered breathtaking portraits that exceeded my expectations.",
        avatar: "/shot-2.jpg",
        date: "1 month ago"
    },
    {
        name: "Omar Bah",
        event: "Corporate Event",
        rating: 5,
        comment: "Professional, punctual, and incredibly talented. Nuru documented our company event beautifully and delivered the photos quickly.",
        avatar: "/man-lens.jpg",
        date: "3 weeks ago"
    },
    {
        name: "Kaddy Touray",
        event: "Family Portrait",
        rating: 4,
        comment: "Great photographer with an eye for detail. Our family photos turned out wonderful and the whole experience was enjoyable.",
        avatar: "/shot-5.jpg",
        date: "1 week ago"
    },
    {
        name: "Lamin Sanyang",
        event: "Wedding Photography",
        rating: 5,
        comment: "Exceptional work! Nuru has a unique ability to capture candid moments and emotions. Our wedding album is a treasure.",
        avatar: "/wedding.jpg",
        date: "2 months ago"
    },
    {
        name: "Isatou Darboe",
        event: "Event Photography",
        rating: 5,
        comment: "Highly recommend! Professional service, beautiful photos, and great communication throughout the entire process.",
        avatar: "/event.jpg",
        date: "3 weeks ago"
    }
];

const photographer = computed(() =>
    photographers.find((p) => p.id === Number(route.params.id))
);

// Gallery functionality
const selectedCategory = ref('all');
const lightboxOpen = ref(false);
const currentLightboxIndex = ref(0);

const filteredGallery = computed(() => {
    if (!photographer.value?.gallery) return [];
    if (selectedCategory.value === 'all') return photographer.value.gallery;
    return photographer.value.gallery.filter(img => img.category === selectedCategory.value);
});

function openLightbox(index) {
    currentLightboxIndex.value = index;
    lightboxOpen.value = true;
    document.body.style.overflow = 'hidden';
}

function closeLightbox() {
    lightboxOpen.value = false;
    document.body.style.overflow = 'auto';
}

function nextImage() {
    if (currentLightboxIndex.value < filteredGallery.value.length - 1) {
        currentLightboxIndex.value++;
    }
}

function previousImage() {
    if (currentLightboxIndex.value > 0) {
        currentLightboxIndex.value--;
    }
}

function selectPackageForBooking(packageName) {
    selectedPackage.value = packageName;
    // Scroll to booking section
    const bookingSection = document.querySelector('#booking-section');
    if (bookingSection) {
        bookingSection.scrollIntoView({ behavior: 'smooth' });
    }
}

// Calendar & booking
const currentYear = ref(new Date().getFullYear());
const currentMonth = ref(new Date().getMonth() + 1);
const bookingStep = ref(1);
const specialRequests = ref("");

const daysInMonth = computed(() => 
    Array.from(
        { length: new Date(currentYear.value, currentMonth.value, 0).getDate() },
        (_, i) => i + 1
    )
);

const weekDays = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
const selectedDate = ref(null);
const selectedTime = ref(null);
const selectedPackage = ref("");
const name = ref("");
const email = ref("");
const phone = ref("");

const availableTimeSlots = [
    { time: "09:00", available: true },
    { time: "10:30", available: true },
    { time: "12:00", available: false },
    { time: "13:30", available: true },
    { time: "15:00", available: true },
    { time: "16:30", available: false },
];

function getMonthYear() {
    const months = ['January', 'February', 'March', 'April', 'May', 'June',
                   'July', 'August', 'September', 'October', 'November', 'December'];
    return `${months[currentMonth.value - 1]} ${currentYear.value}`;
}

function getBlankDays() {
    const firstDay = new Date(currentYear.value, currentMonth.value - 1, 1).getDay();
    return Array.from({ length: firstDay }, (_, i) => i);
}

function isDateDisabled(day) {
    const today = new Date();
    const selectedDay = new Date(currentYear.value, currentMonth.value - 1, day);
    return selectedDay < today;
}

function formatSelectedDate() {
    if (!selectedDate.value) return '';
    const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                   'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return `${selectedDate.value} ${months[currentMonth.value - 1]} ${currentYear.value}`;
}

function previousMonth() {
    if (currentMonth.value === 1) {
        currentMonth.value = 12;
        currentYear.value--;
    } else {
        currentMonth.value--;
    }
    selectedDate.value = null;
}

function nextMonth() {
    if (currentMonth.value === 12) {
        currentMonth.value = 1;
        currentYear.value++;
    } else {
        currentMonth.value++;
    }
    selectedDate.value = null;
}

function nextStep() {
    if (bookingStep.value < 3) {
        bookingStep.value++;
    }
}

function previousStep() {
    if (bookingStep.value > 1) {
        bookingStep.value--;
    }
}

function selectDate(day) {
    selectedDate.value = day;
    bookingStep.value = 2;
}

function selectTime(slot) {
    selectedTime.value = slot;
    bookingStep.value = 3;
}

function bookAppointment() {
    if (!name.value || !email.value || !phone.value || !selectedPackage.value) {
        toast.error("Please fill in all required details!", {autoClose: 5000});
        return;
    }
    
    const bookingDetails = {
        name: name.value,
        email: email.value,
        phone: phone.value,
        date: formatSelectedDate(),
        time: selectedTime.value,
        package: selectedPackage.value,
        specialRequests: specialRequests.value
    };
    
    toast.success(`Booking confirmed for ${name.value} on ${formatSelectedDate()} at ${selectedTime.value}!`, {autoClose: 5000});
    
    // Reset form
    name.value = email.value = phone.value = specialRequests.value = "";
    selectedDate.value = selectedTime.value = selectedPackage.value = null;
    bookingStep.value = 1;
    
    // TODO: Send booking details to backend
    console.log('Booking Details:', bookingDetails);
}
</script>
