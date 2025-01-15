import { createRouter, createWebHistory } from 'vue-router'

import index from '../pages/index.vue'
import main from '../pages/main.vue'

const routes = [
  {
    path: '/',
    name: 'index',
    component: index
  },
  {
    path: '/main',
    name: 'main',
    component: main
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
