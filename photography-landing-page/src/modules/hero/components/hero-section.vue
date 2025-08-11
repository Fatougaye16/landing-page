<template>
  <div class="relative h-screen bg-cover bg-center overflow-hidden" style="background-image: url('/bw-portrait.jpg');">
    <!-- Cinematic Gradient Overlay -->
    <div class="absolute inset-0 bg-gradient-to-t from-black/90 via-black/40 to-transparent"></div>

    <div class="absolute inset-0 pointer-events-none hidden sm:block">
      <div v-for="n in 20" :key="n" class="absolute w-2 h-2 bg-white rounded-full opacity-30 animate-float" :style="{
        top: `${Math.random() * 100}%`,
        left: `${Math.random() * 100}%`,
        animationDelay: `${Math.random() * 5}s`,
      }"></div>
    </div>

    <NavBar />

    <div
      class="relative z-20 flex flex-col items-center justify-center h-full text-center text-white px-4 sm:px-8 lg:px-16">
      <h1
        class="font-extrabold font-serif text-3xl sm:text-5xl md:text-6xl lg:text-7xl xl:text-8xl tracking-tight animate-fadeInUp leading-tight">
        Capturing <span class="text-[#7b1e3a]">Stories</span> <br />
        Beyond Frames
      </h1>
      <p class="mt-4 max-w-xl text-sm sm:text-base md:text-lg text-gray-200 animate-fadeInUp delay-200 px-2 sm:px-0">
        Photography is the art of freezing emotions, preserving moments, and
        telling timeless tales without words.
      </p>
      <button @click="scrollToContact" class="mt-8 px-8 py-3 rounded-full border border-[#7b1e3a] bg-[#7b1e3a] text-white
               hover:bg-white hover:text-black transition-all duration-300 shadow-lg animate-fadeInUp delay-300
               w-full sm:w-auto max-w-xs">
        Contact Us
      </button>
    </div>

    <!-- Right bottom photo frames: stack vertically on small screens -->
    <div
      class="absolute right-4 bottom-4 flex flex-col items-end space-y-4 z-20 animate-slideIn sm:right-16 sm:bottom-12 sm:flex-row sm:space-x-4 sm:space-y-0">
      <div class="photo-frame z-20 rotate-6 w-28 sm:w-64" style="--rotate: 6deg; --delay: 0s">
        <img src="/lens.jpg" alt="Sample Work" />
      </div>
      <div class="photo-frame z-10 -rotate-6 w-28 sm:w-64" style="--rotate: -6deg; --delay: 0.5s">
        <img src="/lens-2.jpg" alt="Sample Work" />
      </div>
    </div>

    <!-- Left bottom photo frames: stack vertically on small screens -->
    <div
      class="absolute left-4 bottom-4 flex flex-col space-y-4 z-20 animate-slideIn sm:left-16 sm:bottom-12 sm:flex-row sm:space-x-4 sm:space-y-0">
      <div class="photo-frame z-20 rotate-6 w-28 sm:w-64" style="--rotate: 6deg; --delay: 1s">
        <img src="/man-lens.jpg" alt="Sample Work" />
      </div>
      <div class="photo-frame z-10 -rotate-6 w-28 sm:w-64" style="--rotate: -6deg; --delay: 1.5s">
        <img src="/nature-shot.jpg" alt="Sample Work" />
      </div>
    </div>

    <!-- Social Media Links -->
    <div class="absolute bottom-4 left-1/2 -translate-x-1/2 flex gap-6 text-white z-20 text-xl sm:text-2xl">
      <a v-for="link in socialLinks" :key="link.icon" :href="link.url" target="_blank"
        class="hover:text-[#7b1e3a] transition duration-300">
        <i :class="link.icon"></i>
      </a>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";
import NavBar from "./nav-bar.vue";
const socialLinks = [
  { icon: "fab fa-facebook", url: "https://facebook.com" },
  { icon: "fab fa-instagram", url: "https://instagram.com" },
  { icon: "fab fa-twitter", url: "https://twitter.com" },
  { icon: "fab fa-youtube", url: "https://youtube.com" },
];

const router = useRouter();
const scrollToContact = () => {
  router.push("/contact");
};
</script>

<style>
@import url('https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css');

@keyframes float {
  0% {
    transform: translateY(0) scale(1);
    opacity: 0.3;
  }

  50% {
    transform: translateY(-20px) scale(1.2);
    opacity: 0.5;
  }

  100% {
    transform: translateY(0) scale(1);
    opacity: 0.3;
  }
}

.animate-float {
  animation: float 6s ease-in-out infinite;
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.animate-fadeInUp {
  animation: fadeInUp 1s ease forwards;
}

.animate-fadeInUp.delay-200 {
  animation-delay: 0.2s;
}

.animate-fadeInUp.delay-300 {
  animation-delay: 0.3s;
}

@keyframes photoFloat {
  0% {
    transform: translateY(0) rotate(var(--rotate));
    opacity: 0;
  }

  50% {
    transform: translateY(-8px) rotate(var(--rotate));
    opacity: 1;
  }

  100% {
    transform: translateY(0) rotate(var(--rotate));
    opacity: 1;
  }
}

.photo-frame {
  animation: photoFloat 4s ease-in-out infinite;
  animation-delay: var(--delay);
}

@keyframes fadeSlideUp {
  0% {
    opacity: 0;
    transform: translateY(40px) rotate(6deg);
  }

  100% {
    opacity: 1;
    transform: translateY(0) rotate(var(--rotate));
  }
}

.animate-photo {
  animation: fadeSlideUp 0.8s ease forwards;
}

.photo-frame {
  width: 16rem;
  /* default */
  border: 8px solid white;
  border-radius: 0.5rem;
  overflow: hidden;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.3);
  transition: transform 0.5s ease, box-shadow 0.5s ease;
}

.photo-frame img {
  display: block;
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.photo-frame:hover {
  transform: scale(1.05);
  box-shadow: 0 15px 35px rgba(0, 0, 0, 0.4);
}

/* Add media queries for responsiveness */
@media (max-width: 640px) {
  .photo-frame {
    width: 7rem !important;
    border-width: 4px !important;
  }

}
</style>
