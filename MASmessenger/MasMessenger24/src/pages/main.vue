<template>
  <v-app>
    <v-container>
      <!-- Navbar -->
      <v-app-bar app color="deep-purple darken-2" dark>
        <v-toolbar-title>Masmessenger</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn text to="#chats">Chats</v-btn>
        <v-btn text to="#account">Account</v-btn>
        <v-btn text to="#about-us">About Us</v-btn>
      </v-app-bar>

      <!-- Hero Section -->
      <v-row class="text-center" align="center" justify="center" style="height: 50vh; background: linear-gradient(to right, #7e57c2, #512da8); border-radius: 15px; margin-top: 100px;">
        <v-col cols="12">
          <h1 class="display-2 white--text">Masmessenger</h1>
          <p class="subtitle-1 white--text">Chats made simple, secure, and beautiful.</p>
          <v-btn color="purple lighten-1" dark large>Start Chatting</v-btn>
        </v-col>
      </v-row>

      <!-- Features Section -->
      <v-row class="my-10">
        <v-col cols="12" md="4">
          <v-btn block outlined color="purple darken-2" class="rounded-button" href="#chats">
            Chats
          </v-btn>
        </v-col>
        <v-col cols="12" md="4">
          <v-btn block outlined color="purple darken-2" class="rounded-button" href="#account">
            Account
          </v-btn>
        </v-col>
        <v-col cols="12" md="4">
          <v-btn block outlined color="purple darken-2" class="rounded-button" href="#about-us">
            About Us
          </v-btn>
        </v-col>
      </v-row>

      <!-- Footer -->
      <v-footer color="deep-purple darken-2" dark>
        <v-col class="text-center white--text">© 2025 Masmessenger. All Rights Reserved.</v-col>
      </v-footer>
    </v-container>
  </v-app>
</template>

<script>
export default {
  name: "MasmessengerWebsite",
};
</script>

<style>
body {
  margin: 0;
  font-family: 'Roboto', sans-serif;
}

.rounded-button {
  border-radius: 15px;
  transition: transform 0.2s, background-color 0.2s;
}

.rounded-button:hover {
  background-color: #673ab7;
  transform: scale(1.05);
}
</style>
