/**
 * main.js
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'

import 'leaflet/dist/leaflet.css'

import '@mdi/font/css/materialdesignicons.css';

import 'vuetify/dist/vuetify.min.css';


const app = createApp(App)

registerPlugins(app)

app.mount('#app')

