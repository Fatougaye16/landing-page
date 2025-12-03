// Database Types for Supabase Tables

export interface Profile {
  id: string
  email: string
  full_name: string | null
  phone: string | null
  role: 'client' | 'photographer'
  avatar_url: string | null
  
  // Photographer-specific fields
  studio_name: string | null
  specialization: 'portrait' | 'wedding' | 'event' | 'commercial' | 'nature' | null
  experience: number | null
  description: string | null
  base_price: number | null
  location: string | null
  portfolio_images: string[] | null
  rating: number
  total_reviews: number
  
  // Client-specific fields
  preferred_location: string | null
  booking_history: string[] | null
  
  // Common fields
  is_active: boolean
  created_at: string
  updated_at: string
}

export interface PhotoSession {
  id: string
  photographer_id: string
  client_id: string
  session_type: 'portrait' | 'wedding' | 'event' | 'commercial' | 'nature'
  session_date: string
  session_time: string
  duration_hours: number
  location: string
  price: number
  deposit_amount: number | null
  status: 'pending' | 'confirmed' | 'in_progress' | 'completed' | 'cancelled' | 'refunded'
  client_notes: string | null
  photographer_notes: string | null
  cancellation_reason: string | null
  created_at: string
  updated_at: string
}

export interface Photo {
  id: string
  session_id: string
  photographer_id: string
  client_id: string
  file_url: string
  thumbnail_url: string | null
  title: string | null
  description: string | null
  file_size: number | null
  dimensions: string | null
  is_favorite: boolean
  is_public: boolean
  download_count: number
  taken_at: string | null
  uploaded_at: string
  created_at: string
}

export interface Review {
  id: string
  photographer_id: string
  client_id: string
  session_id: string
  rating: number
  title: string | null
  comment: string | null
  is_public: boolean
  created_at: string
  updated_at: string
}

export interface PortfolioItem {
  id: string
  photographer_id: string
  title: string
  description: string | null
  image_url: string
  thumbnail_url: string | null
  category: 'portrait' | 'wedding' | 'event' | 'commercial' | 'nature' | null
  is_featured: boolean
  sort_order: number
  created_at: string
  updated_at: string
}

export interface Notification {
  id: string
  user_id: string
  title: string
  message: string
  type: 'booking' | 'payment' | 'review' | 'system' | 'message'
  is_read: boolean
  action_url: string | null
  created_at: string
  read_at: string | null
}

// Booking-related types with relationships
export interface BookingWithPhotographer extends PhotoSession {
  photographer: {
    id: string
    full_name: string
    studio_name: string
    avatar_url: string
    specialization: string
  } | null
}

// Form data types
export interface BookingFormData {
  photographerId: string
  sessionType: PhotoSession['session_type']
  sessionDate: string
  sessionTime: string
  durationHours: number
  location: string
  price: number
  clientNotes?: string
}

// Example Supabase database schema SQL (for reference):
/*
-- Users table (extends Supabase auth.users)
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

-- Photo sessions table
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

-- Photos table
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

-- Reviews table
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

-- Policies (examples)
CREATE POLICY "Users can view their own profile" ON profiles
  FOR SELECT USING (auth.uid() = id);

CREATE POLICY "Users can update their own profile" ON profiles
  FOR UPDATE USING (auth.uid() = id);
*/