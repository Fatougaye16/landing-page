import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import PhotographersListView from '@/views/PhotographersListView.vue'
import PhotographerDetailsView from '@/views/PhotographerDetailsView.vue'
import Contact from '@/modules/contacts/contact.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/shots',
      name: 'shots',
      component: () => import('@/views/ShotsView.vue'),
    },
    {
      path: '/photographers',
      name: 'photographers',
      component: PhotographersListView,
    },
    {
    path: '/photographers/:id',
    name: 'photographer-details',
    component: PhotographerDetailsView,
    props: true,
  },
    {
      path: '/about',
      name: 'about',
      component: () => import('@/views/AboutView.vue'),
    },
    {
      path: '/contact',
      name: 'contact',
      component: Contact,
    },
  ],
})

export default router
