<template>
  <v-card>
    <v-card-title>Sign In</v-card-title>
    <v-card-actions>
      <v-btn color="primary" @click="signInWithGoogle">Sign in with Google</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { auth, GoogleAuthProvider, signInWithPopup } from '../Firebase';

export default {
  methods: {
    async signInWithGoogle() {
      const provider = new GoogleAuthProvider();
      await signInWithPopup(auth, provider);
    }
  }
};
</script>