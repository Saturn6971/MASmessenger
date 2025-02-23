<template>
    <v-btn color="error" @click="signOut">Sign Out</v-btn>
  </template>
  
  <script>
  import { auth, signOut } from '../Firebase';
  
  export default {
    methods: {
      async signOut() {
        await signOut(auth);
      }
    }
  };
  </script>