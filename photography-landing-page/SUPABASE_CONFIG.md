# Environment variables for local development
# Copy this file to .env and fill in your actual values

VITE_SUPABASE_URL=your_supabase_project_url
VITE_SUPABASE_ANON_KEY=your_supabase_anon_key

# IMPORTANT FOR TESTING: 
# To disable email confirmation in Supabase Dashboard:
# 1. Go to Authentication > Settings in your Supabase project
# 2. Scroll to "User Signups" section
# 3. Turn OFF "Enable email confirmations"
# 4. Save settings
# 
# This allows users to sign up and immediately access the app without email verification.