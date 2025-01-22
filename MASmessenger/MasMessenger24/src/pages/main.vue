<template>
  <v-app>
    <v-container>
      <!-- Navbar -->
      <v-app-bar app color="deep-purple darken-2" dark>
        <v-toolbar-title>Masmessenger</v-toolbar-title>
        <v-spacer></v-spacer>
 
  <div class="text-center">
    <v-menu
      v-model="menu"
      :close-on-content-click="false"
      location="end"
    >
      <template v-slot:activator="{ props }">
        <v-btn
          color="white"
          v-bind="props"
        >
          Account 
        </v-btn>
      </template>

      <v-card min-width="300">
        <v-list>
          <v-list-item
            prepend-avatar="https://cdn.vuetifyjs.com/images/john.jpg"
            subtitle="Ur Account"
            title="Mas Girkovic"
          >
            <template v-slot:append>
           
            </template>
          </v-list-item>
        </v-list>

        <v-divider></v-divider>

        <v-list>
          <v-list-item>
            <v-btn
              v-model="message"
              color="purple"
              label="Enable messages"
              hide-details
            >Details</v-btn>
          </v-list-item>

          <v-list-item>
            <v-btn
              v-model="hints"
              color="purple"
              label="Enable hints"
              hide-details
              style="width: 100px;"
              href="/"
            > Abmelden </v-btn>
          </v-list-item>
        </v-list>

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            variant="text"
            @click="menu = false"
          >
            Cancel
          </v-btn>
        
        </v-card-actions>
      </v-card>
    </v-menu>
  </div>


        <v-btn text to="/main/aboutus">About Us</v-btn>
      </v-app-bar>

      <!-- Hero Section -->
      <v-row 
        class="text-center hero-section"
         align="center" 
         justify="center" 
         style="
         background: linear-gradient(to right, #7e57c2, #512da8); 
         border-radius: 15px; 
         margin-top: 100px;"
      >
        <!-- Sparkline as Background -->
        <div class="sparkline-container">
          <v-sparkline
            :auto-line-width="autoLineWidth"
            :fill="fill"
            :gradient="gradient"
            :gradient-direction="gradientDirection"
            :line-width="width"
            :model-value="value"
            :padding="padding"
            :smooth="radius || false"
            :stroke-linecap="lineCap"
            :type="type"
            auto-draw
          ></v-sparkline>
        </div>

        <!-- Hero Content -->
        <v-col cols="12" class="box">
          <h1 class="display-2 white--text">Masmessenger</h1>
          <p class="subtitle-1 white--text">Chats made simple, secure, and beautiful.</p>
          <v-btn color="purple lighten-1" dark large style="margin: 20px;">Start Chatting</v-btn>
        </v-col>
      </v-row>

      <!-- Features Section -->
      <v-row class="my-10">
  <v-col cols="12" md="4">
    <v-tooltip text="Start and manage your chats" location="bottom">
      <template v-slot:activator="{ props }">
        <v-btn
          block
          outlined
          color="purple darken-2"
          class="btns"
          href="#chats"
          v-bind="props"
        >
          Chats
        </v-btn>
      </template>
    </v-tooltip>
  </v-col>
  <v-col cols="12" md="4">
    <v-tooltip text="Manage your account settings" location="bottom">
      <template v-slot:activator="{ props }">
        <v-btn
          block
          outlined
          color="purple darken-2"
          class="btns"
          href="#account"
          v-bind="props"
        >
          Account
        </v-btn>
      </template>
    </v-tooltip>
  </v-col>
  <v-col cols="12" md="4">
    <v-tooltip text="Learn more about us" location="bottom">
      <template v-slot:activator="{ props }">
        <v-btn
          block
          outlined
          color="purple darken-2"
          class="btns"
          href="#about-us"
          v-bind="props"
        >
          About Us
        </v-btn>
      </template>
    </v-tooltip>
  </v-col>
</v-row>
      <!-- Footer -->
      <v-footer class="footer">
        <v-col class="text-center white--text">© 2025 Masmessenger. All Rights Reserved.</v-col>
      </v-footer>
    </v-container>
  </v-app>
</template>

<script>
const gradients = [
  ['#222'],
  ['#42b3f4'],
  ['red', 'orange', 'yellow'],
  ['purple', 'violet'],
  ['#00c6ff', '#F0F', '#FF0'],
  ['#4caf50', '#2196f3', '#9c27b0'], // Grün, Blau, Lila
];

export default {
  name: "MasmessengerWebsite",
  data: () => ({
    width: 2,
    radius: 10,
    padding: 8,
    lineCap: 'round',
    gradient: gradients[5], // Benutze einen schönen Farbverlauf
    value: [0, 2, 5, 9, 5, 10, 3, 5, 0, 0, 1, 8, 2, 9, 10], // Werte für die Sparkline
    gradientDirection: 'top',
    gradients,
    fill: false,
    type: 'trend',
    autoLineWidth: false,
    fav: true,
      menu: false,
      message: false,
      hints: true,
  }),
};
</script>

<style>
body {
  margin: 0;
  font-family: 'Roboto', sans-serif;
}

.hero-section {
 
  position: relative;
  height: 50vh;
  background: linear-gradient(to right, #7e57c2, #512da8);
  border-radius: 15px;
  margin-top: 300px; /* Erhöht den Abstand nach unten */
  overflow: hidden;
}

.sparkline-container {
  position: absolute;
  bottom: 150px; /* Platzierung der Sparkline etwas angehoben */
  left: 0;
  width: 100%;
  height: 80px; /* Höhe der Sparkline */
  z-index: 0;
}

.sparkline-container .v-sparkline {
  width: 100%;
  height: 100%; /* Sparkline wird vollständig gestreckt */
}


.hero-section .v-col {
  position: relative;
  z-index: 1; /* Inhalt wird über der Sparkline angezeigt */
}

.btns {


  height: 50px; /* Optional: Anpassen der Buttonhöhe */
  padding: 25px;
  border-radius: 20px;
}



.v-btns {
  border-radius: 30px; /* Abgerundete Ecken */
  transition: transform 0.3s, background-color 0.3s, box-shadow 0.3s; /* Sanfte Übergänge */
}

.v-btns:hover {
  transform: scale(1.1); /* Leichte Vergrößerung bei Hover */
  background-color: #4caf50; /* Ändert die Hintergrundfarbe auf grün */
  box-shadow: 0px 8px 15px rgba(0, 0, 0, 0.2); /* Leichter Schatteneffekt */
}

.v-btns:focus {
  outline: none; /* Entfernt den Standard-Button-Rand */
  box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.2); /* Fokus-Effekt */
}

.v-btns.block {
  display: block;
  width: 100%; /* Stellt sicher, dass die Buttons die volle Breite einnehmen */
}

.box{
  margin-top: 100px;
}

/* Add the new font-family */
@import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap');

body {
  font-family: 'Montserrat', sans-serif; /* Apply the modern font to the entire body */
}

.btn1{
  margin: 20px;
}


.footer{
  margin-top: 200px;
  color: black;
  border-radius: 20px;
  
}

.v-tooltip .v-overlay__content {
  font-size: 16px; /* Größerer Tooltip-Text */
  color: white !important; /* Weißer Text */
  background-color: rgb(98, 98, 98) !important; /* Schwarzer Hintergrund */
  padding: 10px 15px; /* Größeres Padding */
  border-radius: 8px; /* Runde Ecken */
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.3); /* Schatteneffekt */
  text-align: center; /* Text zentrieren */
}

.v-app-bar {
    border-radius: 0 0 15px 15px;
  }
  
</style>
