<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-toolbar-title>Chat App</v-toolbar-title>
      <v-spacer></v-spacer>
      <router-link to="/signin" v-if="!user">Sign In</router-link>
      <router-link to="/signout" v-if="user">Sign Out</router-link>
    </v-app-bar>

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