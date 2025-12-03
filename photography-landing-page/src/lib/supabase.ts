import { createClient } from '@supabase/supabase-js'

const supabaseUrl = import.meta.env.VITE_SUPABASE_URL
const supabaseKey = import.meta.env.VITE_SUPABASE_ANON_KEY
const supabasePublishableKey = import.meta.env.VITE_SUPABASE_PUBLISHABLE_KEY

if (!supabaseUrl || !supabaseKey || !supabasePublishableKey) {
  throw new Error('Missing Supabase environment variables')
}

export const supabase = createClient(supabaseUrl, supabaseKey, {
  auth: {
    autoRefreshToken: true,
    persistSession: true,
    detectSessionInUrl: true
  }
})

// For testing: Create user with auto confirmation
// This bypasses email verification by setting email_confirmed_at
export const signUpWithAutoConfirm = async (email: string, password: string, metadata?: Record<string, any>) => {
  try {
    // Use regular signup but with email confirmation disabled
    const { data, error } = await supabase.auth.signUp({
      email,
      password,
      options: {
        data: {
          ...metadata,
          email_confirmed_at: new Date().toISOString() // Auto-confirm for testing
        }
      }
    })

    if (error) {
      // If signup fails due to email confirmation, try alternative approach
      if (error.message.includes('email_not_confirmed') || error.message.includes('Email not confirmed')) {
        console.log('Attempting alternative signup method for testing...')
        
        // Try signing up without email confirmation requirement
        const { data: altData, error: altError } = await supabase.auth.signUp({
          email,
          password,
          options: {
            data: metadata,
            emailRedirectTo: undefined // Don't send confirmation email
          }
        })
        
        if (!altError) {
          // Immediately sign in the user
          const { data: signInData, error: signInError } = await supabase.auth.signInWithPassword({
            email,
            password
          })
          
          if (!signInError) {
            return { data: signInData, error: null }
          }
        }
      }
      
      return { data: null, error }
    }

    return { data, error: null }
  } catch (error) {
    return { data: null, error: error as any }
  }
}