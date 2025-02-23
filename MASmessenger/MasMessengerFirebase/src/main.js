import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import vuetify from './plugins/vuetify';
import { VueFire, VueFireFirestoreOptionsAPI } from 'vuefire';
import { firestore } from './Firebase';

const app = createApp(App);
app.use(router);
app.use(vuetify);
app.use(VueFire, {
  modules: [
    VueFireFirestoreOptionsAPI
  ],
  firestore
});
app.mount('#app');