<template>
  <ul class="bg-white rounded-xl shadow-md divide-y divide-gray-100">
    <li class="p-4 text-sm font-semibold text-gray-500 tracking-wide">
      Available Photographers
    </li>

    <li
      v-for="photographer in photographers"
      :key="photographer.id"
      class="flex items-center justify-between px-4 py-3 hover:bg-gray-50 transition"
    >
      <!-- Profile -->
      <div class="flex items-center gap-3">
        <img
          :src="photographer.image"
          class="w-12 h-12 rounded-full object-cover"
          alt="profile"
        />
        <div>
          <div class="font-medium text-gray-800">{{ photographer.name }}</div>
          <div class="text-xs uppercase font-semibold text-gray-500">
            {{ photographer.studio }}
          </div>
        </div>
      </div>

      <!-- View Button -->
      <router-link
        :to="{ name: 'photographer-details', params: { id: photographer.id } }"
        class="btn btn-sm bg-[#7b1e3a] text-white hover:bg-[#5c162c] rounded-lg"
      >
        View
      </router-link>
    </li>
  </ul>
</template>

<script setup>
const photographers = [
  {
    id: 1,
    name: "Nuru",
    studio: "Nuru's Touch",
    image: "/man-lens.jpg",
  },
  {
    id: 2,
    name: "Ellie Beilish",
    studio: "Dutcher's Shot",
    image: "https://img.daisyui.com/images/profile/demo/4@94.webp",
  },
  {
    id: 3,
    name: "Adu",
    studio: "Cappuccino",
    image: "https://img.daisyui.com/images/profile/demo/3@94.webp",
  },
];
</script>
