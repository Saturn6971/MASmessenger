<template>
    <v-container fluid class="d-flex fill-height" dark>
      <!-- Sidebar Navigation -->
      <v-navigation-drawer app permanent dark color="black">
        <v-list>
          <v-list-item>
            <v-icon>mdi-home</v-icon>
            <v-list-item-title>Home</v-list-item-title>
          </v-list-item>
  
          <v-list-item>
            <v-icon>mdi-message</v-icon>
            <v-list-item-title>Chats</v-list-item-title>
          </v-list-item>
  
          <v-list-item>
            <v-icon>mdi-account</v-icon>
            <v-list-item-title>Profile</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
  
      <!-- Contacts List -->
      <v-col cols="3" class="pa-4 grey darken-3">
        <v-list dense>
          <v-list-item v-for="contact in contacts" :key="contact.name">
            <v-list-item-avatar>
              <v-icon>mdi-account-circle</v-icon>
            </v-list-item-avatar>
  
            <v-list-item-content>
              <v-list-item-title>{{ contact.name }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-col>
  
      <!-- Chat Section -->
      <v-col cols="9" class="pa-4">
        <div class="chat-container">
          <v-row v-for="(message, index) in messages" :key="index" :class="{'justify-end': message.sent}">
            <v-col cols="auto">
              <div class="message-bubble" :class="{'sent': message.sent}">
                {{ message.text }}
              </div>
            </v-col>
          </v-row>
        </div>
      </v-col>
    </v-container>
  </template>
  
  <script>
  export default {
    data() {
      return {
        contacts: [
          { name: 'Alice' },
          { name: 'Bob' },
          { name: 'Charlie' },
          { name: 'Diana' }
        ],
        messages: [
          { text: 'Hey there!', sent: true },
          { text: 'Hi! How are you?', sent: false },
       +
        ]
      };
    }
  };
  </script>
  
  <style scoped>
  .chat-container {
    background-color: #212121;
    border-radius: 8px;
    padding: 16px;
    height: 100%;
    overflow-y: auto;
  }
  
  .message-bubble {
    background-color: #7e57c2;
    color: white;
    padding: 12px;
    border-radius: 16px;
    max-width: 60%;
    word-wrap: break-word;
  }
  
  .message-bubble.sent {
    background-color: #6200ea;
  }
  
  .v-navigation-drawer {
    width: 80px;
  }
  </style>
  