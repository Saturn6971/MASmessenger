import { createRouter, createWebHistory } from 'vue-router';

import SignIn from '../components/SignIn.vue';
import SignOut from '../components/SignOut.vue';
import ChatRoom from '../components/ChatRoom.vue';
import main from '../components/Main.vue';
import account from '../components/Account.vue';
import UserList from '../components/UserList.vue';
import PrivateChat from '../components/PrivateChat.vue'; 

const routes = [
  
  { path: '/', component: SignIn },
  { path: '/signout', component: SignOut },
  { path: '/chat', component: ChatRoom },
  { path: '/main', component: main },
  { path: '/main/Account', component: account },
  { path: '/UserList', component: UserList },


  {
    path: '/chat/:uid', // Dynamischer Parameter für die Benutzer-ID
    component: PrivateChat,
    meta: { requiresAuth: true }, // Nur für authentifizierte Benutzer
  },

];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;