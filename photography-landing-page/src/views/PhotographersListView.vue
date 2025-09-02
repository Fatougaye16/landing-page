<template>
  <ul class="bg-white rounded-xl shadow-md divide-y divide-gray-100">
    <!-- Header -->
    <li class="p-4 text-sm font-semibold text-gray-500 tracking-wide">
      Available Photographers
    </li>

    <!-- Photographer Row -->
    <li
      v-for="(photographer, index) in photographers"
      :key="index"
      class="flex items-center justify-between px-4 py-4 hover:bg-gray-50 transition"
    >
      <!-- Profile -->
      <div class="flex items-center gap-4">
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

      <!-- Actions -->
      <button
        class="btn btn-sm bg-[#7b1e3a] text-white hover:bg-[#5c162c] rounded-lg"
      >
        View
      </button>
    </li>
  </ul>
</template>

<script setup>
const photographers = [
  {
    name: "Dio Lupa",
    studio: "Nuru's Touch",
    image: "https://img.daisyui.com/images/profile/demo/1@94.webp",
  },
  {
    name: "Ellie Beilish",
    studio: "Dutcher's Shot",
    image: "https://img.daisyui.com/images/profile/demo/4@94.webp",
  },
  {
    name: "Adu",
    studio: "Cappuccino",
    image: "https://img.daisyui.com/images/profile/demo/3@94.webp",
  },
   {
    name: "Dio Lupa",
    studio: "Nuru's Touch",
    image: "https://img.daisyui.com/images/profile/demo/1@94.webp",
  },
  {
    name: "Ellie Beilish",
    studio: "Dutcher's Shot",
    image: "https://img.daisyui.com/images/profile/demo/4@94.webp",
  },
  {
    name: "Adu",
    studio: "Cappuccino",
    image: "https://img.daisyui.com/images/profile/demo/3@94.webp",
  },
   {
    name: "Dio Lupa",
    studio: "Nuru's Touch",
    image: "https://img.daisyui.com/images/profile/demo/1@94.webp",
  },
  {
    name: "Ellie Beilish",
    studio: "Dutcher's Shot",
    image: "https://img.daisyui.com/images/profile/demo/4@94.webp",
  },
  {
    name: "Adu",
    studio: "Cappuccino",
    image: "https://img.daisyui.com/images/profile/demo/3@94.webp",
  },
];
</script>
