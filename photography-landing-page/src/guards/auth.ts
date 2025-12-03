import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'
import { supabase } from '@/lib/supabase'

export async function authGuard(
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
) {
  const { data: { session } } = await supabase.auth.getSession()
  
  if (!session) {
    // Redirect to login with the intended destination
    next({
      name: 'login',
      query: { redirect: to.fullPath }
    })
  } else {
    next()
  }
}

export async function guestGuard(
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
) {
  const { data: { session } } = await supabase.auth.getSession()
  
  if (session) {
    // Redirect authenticated users away from guest-only pages
    next({ name: 'dashboard' })
  } else {
    next()
  }
}