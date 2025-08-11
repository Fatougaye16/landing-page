<template>
  <div class="min-h-screen bg-white flex flex-col items-center px-6 py-12 text-[#7b1e3a]">
    <h1 class="text-4xl font-bold mb-8">Get in Touch</h1>

    <div class="bg-[#fef6f8] shadow-lg rounded-2xl p-8 max-w-xl w-full">
      <form class="flex flex-col gap-6">
        <div>
          <label class="block mb-2 font-medium">Name</label>
          <input
            type="text"
            class="w-full border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:border-[#7b1e3a]"
          />
        </div>

        <div>
          <label class="block mb-2 font-medium">Email</label>
          <input
            type="email"
            class="w-full border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:border-[#7b1e3a]"
          />
        </div>

        <div>
          <label class="block mb-2 font-medium">Message</label>
          <textarea
            rows="5"
            class="w-full border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:border-[#7b1e3a]"
          ></textarea>
        </div>

        <button
          type="submit"
          class="bg-[#7b1e3a] text-white px-6 py-3 rounded-lg hover:bg-white hover:text-[#7b1e3a] border-2 border-[#7b1e3a] transition-colors"
        >
          Send Message
        </button>
      </form>
    </div>

    <div class="mt-10 text-center text-gray-600">
      <p>Email: <span class="text-[#7b1e3a]">info@sweetshots.com</span></p>
      <p>Phone: <span class="text-[#7b1e3a]">+220 123 4567</span></p>
      <p>Location: <span class="text-[#7b1e3a]">Banjul, The Gambia</span></p>
    </div>
  </div>
</template>
