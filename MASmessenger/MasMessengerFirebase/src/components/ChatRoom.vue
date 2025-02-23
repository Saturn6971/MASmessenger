<template>
    <v-container>
      <v-list>
        <v-list-item v-for="msg in messages" :key="msg.id">
          <v-list-item-avatar>
            <v-img :src="msg.photoURL"></v-img>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>{{ msg.text }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
      <v-form @submit.prevent="sendMessage">
        <v-text-field v-model="formValue" label="Say something nice"></v-text-field>
        <v-btn type="submit" :disabled="!formValue">Send</v-btn>
      </v-form>
    </v-container>
  </template>
  
  <script>
  import { auth, firestore, collection, query, orderBy, limit, addDoc, serverTimestamp } from '../Firebase';
  import { useCollection } from 'vuefire';
  
  export default {
    data() {
      return {
        formValue: ''
      };
    },
    setup() {
      const messagesRef = collection(firestore, 'messages');
      const q = query(messagesRef, orderBy('createdAt'), limit(25));
      const messages = useCollection(q);
  
      return { messages };
    },
    methods: {
      async sendMessage() {
        const { uid, photoURL } = auth.currentUser;
  
        await addDoc(collection(firestore, 'messages'), {
          text: this.formValue,
          createdAt: serverTimestamp(),
          uid,
          photoURL
        });
  
        this.formValue = '';
      }
    }
  };
  </script>