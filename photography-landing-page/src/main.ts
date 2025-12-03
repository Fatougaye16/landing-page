import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './style.css'

// Initialize Supabase (imported for side effects)
import './lib/supabase'

const app = createApp(App)
app.use(router)
app.mount('#app')
