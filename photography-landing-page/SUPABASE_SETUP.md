# Photography Platform - Supabase Integration

This project has been configured to work with Supabase for authentication and database operations.

## Setup Instructions

### 1. Supabase Setup

1. Go to [Supabase](https://supabase.com) and create a new project
2. Once your project is created, go to Settings → API
3. Copy your project URL and anon key

### 2. Environment Configuration

1. Copy the `.env.example` file to `.env`:
   ```bash
   cp .env.example .env
   ```

2. Update the `.env` file with your Supabase credentials:
   ```
   VITE_SUPABASE_URL=your_supabase_project_url_here
   VITE_SUPABASE_ANON_KEY=your_supabase_anon_key_here
   ```

### 3. Database Schema Setup

Run the following SQL in your Supabase SQL editor to set up the database schema:

```sql
-- Create profiles table
CREATE TABLE profiles (
  id UUID PRIMARY KEY REFERENCES auth.users(id) ON DELETE CASCADE,
  email TEXT UNIQUE NOT NULL,
  full_name TEXT,
  phone TEXT,
  role TEXT CHECK (role IN ('client', 'photographer')),
  avatar TEXT,
  studio_name TEXT,
  specialization TEXT,
  experience INTEGER,
  description TEXT,
  base_price DECIMAL(10,2),
  location TEXT,
  portfolio_images TEXT[],
  rating DECIMAL(2,1) DEFAULT 0,
  total_bookings INTEGER DEFAULT 0,
  preferred_location TEXT,
  booking_history UUID[],
  created_at TIMESTAMPTZ DEFAULT NOW(),
  updated_at TIMESTAMPTZ DEFAULT NOW()
);

-- Create photo_sessions table
CREATE TABLE photo_sessions (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  photographer_id UUID REFERENCES profiles(id) ON DELETE CASCADE,
  client_id UUID REFERENCES profiles(id) ON DELETE CASCADE,
  session_type TEXT CHECK (session_type IN ('portrait', 'wedding', 'event', 'commercial', 'nature')),
  session_date DATE NOT NULL,
  session_time TIME NOT NULL,
  duration_hours INTEGER NOT NULL,
  location TEXT NOT NULL,
  price DECIMAL(10,2) NOT NULL,
  status TEXT CHECK (status IN ('pending', 'confirmed', 'completed', 'cancelled')) DEFAULT 'pending',
  notes TEXT,
  created_at TIMESTAMPTZ DEFAULT NOW(),
  updated_at TIMESTAMPTZ DEFAULT NOW()
);

-- Create photos table
CREATE TABLE photos (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  session_id UUID REFERENCES photo_sessions(id) ON DELETE CASCADE,
  photographer_id UUID REFERENCES profiles(id) ON DELETE CASCADE,
  client_id UUID REFERENCES profiles(id) ON DELETE CASCADE,
  file_url TEXT NOT NULL,
  thumbnail_url TEXT,
  title TEXT,
  description TEXT,
  is_favorite BOOLEAN DEFAULT FALSE,
  taken_at TIMESTAMPTZ,
  created_at TIMESTAMPTZ DEFAULT NOW()
);

-- Create reviews table
CREATE TABLE reviews (
  id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
  photographer_id UUID REFERENCES profiles(id) ON DELETE CASCADE,
  client_id UUID REFERENCES profiles(id) ON DELETE CASCADE,
  session_id UUID REFERENCES photo_sessions(id) ON DELETE CASCADE,
  rating INTEGER CHECK (rating >= 1 AND rating <= 5),
  comment TEXT,
  created_at TIMESTAMPTZ DEFAULT NOW(),
  UNIQUE(client_id, session_id)
);

-- Enable Row Level Security
ALTER TABLE profiles ENABLE ROW LEVEL SECURITY;
ALTER TABLE photo_sessions ENABLE ROW LEVEL SECURITY;
ALTER TABLE photos ENABLE ROW LEVEL SECURITY;
ALTER TABLE reviews ENABLE ROW LEVEL SECURITY;

-- Create policies
CREATE POLICY "Users can view their own profile" ON profiles
  FOR SELECT USING (auth.uid() = id);

CREATE POLICY "Users can update their own profile" ON profiles
  FOR UPDATE USING (auth.uid() = id);

CREATE POLICY "Anyone can view photographer profiles" ON profiles
  FOR SELECT USING (role = 'photographer');

-- Function to automatically create a profile when a user signs up
CREATE OR REPLACE FUNCTION public.handle_new_user()
RETURNS TRIGGER AS $$
BEGIN
  INSERT INTO public.profiles (id, email, full_name, phone, role, avatar, studio_name, specialization, experience, description, base_price)
  VALUES (
    NEW.id,
    NEW.email,
    NEW.raw_user_meta_data->>'full_name',
    NEW.raw_user_meta_data->>'phone',
    NEW.raw_user_meta_data->>'role',
    NEW.raw_user_meta_data->>'avatar',
    NEW.raw_user_meta_data->>'studio_name',
    NEW.raw_user_meta_data->>'specialization',
    (NEW.raw_user_meta_data->>'experience')::INTEGER,
    NEW.raw_user_meta_data->>'description',
    (NEW.raw_user_meta_data->>'base_price')::DECIMAL(10,2)
  );
  RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

-- Trigger to call the function when a new user is created
CREATE TRIGGER on_auth_user_created
  AFTER INSERT ON auth.users
  FOR EACH ROW EXECUTE PROCEDURE public.handle_new_user();
```

### 4. Optional: Enable Social Authentication

If you want to enable Google or Facebook authentication:

1. Go to Authentication → Providers in your Supabase dashboard
2. Enable the providers you want
3. Configure the OAuth settings with your app credentials

## Features Included

### Authentication
- ✅ Email/Password signup and login
- ✅ Social authentication (Google, Facebook) - requires setup
- ✅ Password reset functionality
- ✅ Protected routes with authentication guards
- ✅ Automatic session management

### Database Operations
- ✅ User profile management
- ✅ CRUD operations with the useSupabase composable
- ✅ Type-safe database operations
- ✅ Row Level Security (RLS) policies

### Components
- ✅ Login and Register forms with Supabase integration
- ✅ User profile component
- ✅ Authentication composable (`useAuth`)
- ✅ Database operations composable (`useSupabase`)

## Usage Examples

### Authentication in Components

```vue
<script setup>
import { useAuth } from '@/composables/useAuth'

const { user, signIn, signUp, signOut, loading } = useAuth()

// Sign in
const login = async () => {
  const { error } = await signIn('email@example.com', 'password')
  if (error) console.error(error.message)
}

// Sign up
const register = async () => {
  const { error } = await signUp('email@example.com', 'password', {
    full_name: 'John Doe',
    role: 'client'
  })
  if (error) console.error(error.message)
}
</script>
```

### Database Operations

```vue
<script setup>
import { useSupabase } from '@/composables/useSupabase'

const { fetchData, insertData, updateData, loading, error } = useSupabase()

// Fetch photographers
const photographers = await fetchData('profiles', { role: 'photographer' })

// Create a booking
const booking = await insertData('photo_sessions', {
  photographer_id: 'uuid',
  client_id: 'uuid',
  session_date: '2023-12-01',
  // ... other fields
})
</script>
```

## Development

```bash
# Install dependencies
npm install

# Start development server
npm run dev

# Build for production
npm run build
```

## Important Notes

- The `.env` file is ignored by git for security
- Always use environment variables for sensitive data
- RLS policies ensure data security at the database level
- User metadata is automatically synced to the profiles table on signup