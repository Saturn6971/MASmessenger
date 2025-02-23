<template>
  <v-app>
  

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import { auth } from './Firebase';
import { onAuthStateChanged } from 'firebase/auth';

export default {
  name: 'App',
  data() {
    return {
      user: null
    };
  },
  created() {
    onAuthStateChanged(auth, (user) => {
      this.user = user;
    });
  }
};
</script>