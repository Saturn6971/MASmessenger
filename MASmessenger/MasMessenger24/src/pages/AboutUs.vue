<template>
    <v-container>
      <!-- Header -->
      <v-app-bar app color="deep-purple darken-2" dark>
        <v-toolbar-title>Masmessenger</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn text href="/main">Home</v-btn>
      </v-app-bar>
  
      <!-- Welcome Section -->
      <v-row class="mt-10 text-center">
        <v-col cols="12">
          <h1 class="display-1 purple--text">Willkommen bei MasMessenger</h1>
          <p class="subtitle-1 white--text">
            Der Messenger, der Kommunikation auf ein neues Level bringt!
          </p>
        </v-col>
      </v-row>
  
      <!-- About Us Text -->
       <div class="bg">
      <v-row class="text-center my-10">
        <v-col cols="12" md="8" offset-md="2">
          <p class="about-us">
            Unser Ziel ist es, Menschen auf der ganzen Welt zu verbinden und eine
            Plattform zu bieten, die sowohl intuitiv als auch sicher ist. Bei
            MasMessenger stehen Benutzerfreundlichkeit, Datenschutz und
            innovative Funktionen im Mittelpunkt.
          </p>
          <p class="about-us">
            Wir glauben daran, dass Kommunikation mehr ist als nur das Versenden
            von Nachrichten. Es geht darum, echte Verbindungen zu schaffen,
            Ideen auszutauschen und gemeinsam zu wachsen.
          </p>
        </v-col>
   
      </v-row>
    </div>
  
      <!-- Team Section -->
      <v-row class="text-center">
        <v-col cols="12">
          <h2 class="display-2 white--text">Unser Team</h2>
        </v-col>
  
        <!-- Team Member 1 -->
        <v-col cols="12" md="4" class="text-center">
          <v-img
            class="team-img"
            src="../assets/Image_Teammember.png"
            alt="Maximilian Gmeinder"
          ></v-img>
          <h3 class="white--text mt-2">Maximilian Gmeinder</h3>
          <v-btn
            class="kontakt-btn"
            color="purple lighten-1"
            href="https://www.instagram.com/max.gmeinder/"
            target="_blank"
          >
            Kontakt
          </v-btn>
        </v-col>
  
        <!-- Team Member 2 -->
        <v-col cols="12" md="4" class="text-center">
          <v-img
            class="team-img"
            src="../assets/Image_Teammember.png"
            alt="Ali Dadak"
          ></v-img>
          <h3 class="white--text mt-2">Ali Dadak</h3>
          <v-btn class="kontakt-btn" color="purple lighten-1" href="#" target="_blank">
            Kontakt
          </v-btn>
        </v-col>
  
        <!-- Team Member 3 -->
        <v-col cols="12" md="4" class="text-center">
          <v-img
            class="team-img"
            src="../assets/Image_Teammember.png"
            alt="Matej Grkovic"
          ></v-img>
          <h3 class="white--text mt-2">Matej Grkovic</h3>
          <v-btn
            class="kontakt-btn"
            color="purple lighten-1"
            href="https://www.instagram.com/matejgvc/"
            target="_blank"
          >
            Kontakt
          </v-btn>
        </v-col>
      </v-row>
    </v-container>
  </template>
  
  <script>
  export default {
    name: "AboutUs",
  };
  </script>
  
  <style scoped>
  /* Allgemeiner Hintergrund */
  body {
    background-color: black;
    color: white;
    font-family: "Montserrat", sans-serif;
  }
  
  /* Header */

  /* Überschriften */
  h1,
  h2 {
    font-family: "Montserrat", sans-serif;
  }
  
  /* About-Us Text */
  .about-us {
    font-size: 18px;
    line-height: 1.6;
    color: white;
    text-align: center;
  }
  
  /* Team-Image Styling */
  .team-img {
    width: 150px;
    height: 150px;
    object-fit: cover;
    border-radius: 50%;
    border: 3px solid white;
    margin-left: 110px;
  }
  
  /* Kontakt Button */
  .kontakt-btn {
    margin-top: 10px;
    color: white;
    font-weight: bold;
    border-radius: 25px;
    text-transform: none;
  }
  
  .kontakt-btn:hover {
    background-color: #5e35b1 !important;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
  }
  .bg{
  background: linear-gradient(to right, #7e57c2, #512da8);
  border-radius: 15px;
  }
  </style>
  