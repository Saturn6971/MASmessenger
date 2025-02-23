import { createRouter, createWebHistory } from 'vue-router';
import Main from '../components/Main.vue';
import SignIn from '../components/SignIn.vue';
import SignOut from '../components/SignOut.vue';
import ChatRoom from '../components/ChatRoom.vue';

const routes = [
  { path: '/', component: Main },
  { path: '/signin', component: SignIn },
  { path: '/signout', component: SignOut },
  { path: '/chat', component: ChatRoom }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;