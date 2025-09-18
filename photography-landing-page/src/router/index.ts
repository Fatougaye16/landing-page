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
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue'),
    },
    {
      path: '/register',
      name: 'register',
      component: () => import('@/views/RegisterView.vue'),
    },
    {
      path: '/dashboard',
      name: 'dashboard',
      component: () => import('@/views/DashboardView.vue'),
      meta: { requiresAuth: true },
    },
  ],
})

// Navigation guard for protected routes
router.beforeEach((to, from, next) => {
  const user = localStorage.getItem('user')
  
  if (to.meta.requiresAuth && !user) {
    // Redirect to login if route requires auth and user is not logged in
    next({ name: 'login', query: { redirect: to.fullPath } })
  } else if ((to.name === 'login' || to.name === 'register') && user) {
    // Redirect to dashboard if user is already logged in and trying to access auth pages
    next({ name: 'dashboard' })
  } else {
    next()
  }
})

export default router
