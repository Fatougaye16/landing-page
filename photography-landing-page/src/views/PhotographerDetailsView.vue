<template>
    <div class="max-w-6xl mx-auto p-6 space-y-8">

        <!-- Profile Header -->
        <div class="flex flex-col md:flex-row gap-6 items-center md:items-start bg-white shadow-lg rounded-xl p-6">
            <img :src="photographer.image" class="w-32 h-32 rounded-full object-cover shadow-md" alt="profile" />
            <div class="flex-1 space-y-2">
                <h2 class="text-3xl font-bold text-gray-800">{{ photographer.name }}</h2>
                <p class="text-gray-500 font-semibold uppercase">{{ photographer.studio }}</p>

                <!-- Contact Info -->
                <div class="mt-3 text-gray-600 space-y-1">
                    <p>📍 {{ photographer.address }}</p>
                    <p>📞 {{ photographer.phone }}</p>
                    <p>✉️ {{ photographer.email }}</p>
                </div>
            </div>
        </div>

        <!-- Gallery -->
        <div>
            <h3 class="text-xl font-semibold mb-4">Gallery</h3>
            <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-3">
                <img v-for="(img, index) in photographer.gallery" :key="index" :src="img"
                    class="w-full h-32 object-cover rounded-lg shadow-md hover:scale-105 transition" alt="gallery" />
            </div>
        </div>

        <!-- Packages -->
        <div>
            <h3 class="text-xl font-semibold mb-4">Packages</h3>
            <div class="grid md:grid-cols-2 gap-4">
                <div v-for="(pkg, index) in photographer.packages" :key="index"
                    class="border rounded-lg shadow-sm p-4 hover:shadow-md transition">
                    <div class="card bg-base-100 shadow-sm">
                        <figure class="w-full h-48 overflow-hidden">
                            <img :src="pkg.image" class="w-full h-full object-cover" alt="package" />
                        </figure>
                        <div class="card-body">
                            <h2 class="card-title">{{ pkg.name }}</h2>
                            <p class="text-gray-600">{{ pkg.description }}</p>
                            <p class="mt-2 font-semibold white">{{ pkg.price }}</p>
                            <div class="card-actions justify-end mt-2">
                                <div v-for="(feature, i) in pkg.features" :key="i" class="badge badge-outline">
                                    {{ feature }}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Booking System -->
        <div class=" shadow-md rounded-lg p-6 space-y-4">
            <h3 class="text-xl font-semibold">Book an Appointment</h3>

            <!-- Calendar -->
            <div class="grid grid-cols-7 gap-2 text-center mb-4">
                <div class="font-semibold" v-for="day in weekDays" :key="day">{{ day }}</div>
                <button v-for="day in daysInMonth" :key="day" @click="selectDate(day)" :class="[
                    'py-2 rounded-md transition',
                    selectedDate === day ? 'bg-[#7b1e3a] text-white' : 'hover:bg-gray-400'
                ]">
                    {{ day }}
                </button>
            </div>

            <p v-if="selectedDate" class="text-gray-100">
                Selected Date: <span class="font-semibold">{{ selectedDate }}/{{ currentMonth }}/{{ currentYear
                    }}</span>
            </p>

            <!-- Time Slots -->
            <div v-if="selectedDate" class="mt-4">
                <h4 class="font-semibold mb-2">Select a Time Slot</h4>
                <div class="grid grid-cols-3 gap-2">
                    <button v-for="slot in timeSlots" :key="slot" @click="selectTime(slot)" :class="[
                        'py-2 rounded-md border transition',
                        selectedTime === slot ? 'bg-[#7b1e3a] text-white' : 'hover:bg-gray-200'
                    ]">
                        {{ slot }}
                    </button>
                </div>
            </div>

            <!-- Booking Form -->
            <div v-if="selectedDate && selectedTime" class="mt-6 space-y-2">
                <input v-model="name" type="text" placeholder="Your Name" class="input input-bordered w-full" />
                <input v-model="email" type="email" placeholder="Email" class="input input-bordered w-full" />
                <input v-model="phone" type="tel" placeholder="Phone" class="input input-bordered w-full" />
                <!-- Package selector -->
                <select v-model="selectedPackage" class="select select-bordered w-full">
                    <option disabled value="">Select a Package</option>
                    <option v-for="pkg in photographer.packages" :key="pkg.name" :value="pkg.name">
                        {{ pkg.name }} - {{ pkg.price }}
                    </option>
                </select>

                <button @click="bookAppointment"
                    class="mt-2 px-4 py-2 bg-[#7b1e3a] text-white rounded-lg hover:bg-[#5c162c] transition">
                    Book Appointment
                </button>
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
            "/shot-1.jpg",
            "/shot-2.jpg",
            "/shot-3.jpg",
            "/shot-4.jpg",
        ],
        packages: [
            {
                name: "Basic",
                description: "2 hours, 10 edited photos",
                price: "D100",
                image: "/wedding.jpg",
                features: ["2 hours", "10 edited photos"],
            },
            {
                name: "Standard",
                description: "4 hours, 25 edited photos, 1 album",
                price: "D200",
                image: "/event.jpg",
                features: ["4 hours", "25 edited photos", "1 album"],
            },
            {
                name: "Premium",
                description: "Full day, 50 edited photos, 2 albums, canvas print",
                price: "D400",
                image: "/portrait.jpg",
                features: ["Full day", "50 edited photos", "2 albums", "Canvas print"],
            },
            {
                name: "Videography",
                description: "Full day, 4 edited photos, 2 albums, canvas print",
                price: "D700",
                image: "/shot-4.jpg",
                features: ["Full day", "50 edited photos", "2 albums", "Canvas print"],
            },
        ],
    },
];

const photographer = computed(() =>
    photographers.find((p) => p.id === Number(route.params.id))
);

// Calendar & booking
const currentYear = new Date().getFullYear();
const currentMonth = new Date().getMonth() + 1;
const daysInMonth = Array.from(
    { length: new Date(currentYear, currentMonth, 0).getDate() },
    (_, i) => i + 1
);
const weekDays = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
const selectedDate = ref(null);
const selectedTime = ref(null);
const selectedPackage = ref("");
const timeSlots = ["09:00", "11:00", "13:00", "15:00", "17:00"];
const name = ref("");
const email = ref("");
const phone = ref("");

function selectDate(day) {
    selectedDate.value = day;
}

function selectTime(slot) {
    selectedTime.value = slot;
}

function bookAppointment() {
    if (!name.value || !email.value || !phone.value) {
        toast.error("Please fill in all your details!", {autoClose: 5000});
        return;
    }
    toast.success(`Appointment booked on ${selectedDate.value}/${currentMonth}/${currentYear} at ${selectedTime.value} for ${name.value}`, {autoClose: 5000});
    // TODO: send booking info to backend
    name.value = email.value = phone.value = "";
    selectedDate.value = selectedTime.value = null;
     selectedDate.value = selectedTime.value = selectedPackage.value = null;
}
</script>
