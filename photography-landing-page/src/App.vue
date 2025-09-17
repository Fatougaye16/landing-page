<script setup lang="ts">
import { RouterView, useRoute } from 'vue-router'
import NavBar from '@/modules/hero/components/nav-bar.vue'

const route = useRoute()
</script>

<template>
  <NavBar />
  <div :class="{ 'pt-20': route.name !== 'home' }">
    <RouterView />
  </div>
</template>

