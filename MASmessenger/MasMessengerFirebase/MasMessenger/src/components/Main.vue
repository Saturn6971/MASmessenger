<template>
  <v-app>
    <v-container>
      <!-- Navbar -->
      <v-app-bar app color="deep-purple darken-2">
        <v-toolbar-title>Masmessenger</v-toolbar-title>
        <v-spacer></v-spacer>
      </v-app-bar>

      <!-- Hero Section -->
      <v-row
        class="text-center hero-section"
        align="center"
        justify="center"
        style="
          background: linear-gradient(to right, #7e57c2, #512da8);
          border-radius: 15px;
          margin-top: 100px;
        "
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
          <h1 class="display-1 white--text">Masmessenger</h1>
          <p class="subtitle-2 white--text">
            Chats made simple, secure, and beautiful.
          </p>
          <v-btn color="purple lighten-1" dark large style="margin: 20px" @click="goToChat"
            >Start Chatting</v-btn
          >
        </v-col>
      </v-row>

      <!-- Features Section -->
      <v-row
        class="text-center"
        align="center"
        justify="center"
        style="margin-top: 20px;"
      >
        <v-col cols="12" md="6">
          <v-tooltip text="View your Account" location="bottom">
            <template v-slot:activator="{ props }">
              <v-btn
                block
                outlined
                color="purple darken-2"
                class="btns"
                v-bind="props"
                @click="goToAccount"
              >
                Account
              </v-btn>
            </template>
          </v-tooltip>
        </v-col>

        <v-col cols="12" md="6">
          <v-tooltip text="View all the Users" location="bottom">
            <template v-slot:activator="{ props }">
              <v-btn
                block
                outlined
                color="purple darken-2"
                class="btns"
                v-bind="props"
                @click="goToUserList"
              >
                User List
              </v-btn>
            </template>
          </v-tooltip>
        </v-col>
      </v-row>

      <!-- Footer -->
    </v-container>
  </v-app>
</template>

<script>
const gradients = [
  ["#222"],
  ["#42b3f4"],
  ["red", "orange", "yellow"],
  ["purple", "violet"],
  ["#00c6ff", "#F0F", "#FF0"],
  ["#4caf50", "#2196f3", "#9c27b0"], // Grün, Blau, Lila
];

export default {
  name: "MasmessengerWebsite",

  data: () => ({
    width: 2,
    radius: 10,
    padding: 8,
    lineCap: "round",
    gradient: gradients[5], // Benutze einen schönen Farbverlauf
    value: [0, 2, 5, 9, 5, 10, 3, 5, 0, 0, 1, 8, 2, 9, 10], // Werte für die Sparkline
    gradientDirection: "top",
    gradients,
    fill: false,
    type: "trend",
    autoLineWidth: false,

    users: [],
  }),

  methods: {
    // Weiterleitung zur Account-Seite
    goToAccount() {
      this.$router.push('/main/account');
    },

    // Weiterleitung zur Chat-Seite
    goToChat() {
      this.$router.push('/chat');
    },

    goToUserList() {
      this.$router.push('/UserList');
    },

    // Methode zum Laden der Benutzerdaten
    async fetchUsers() {
      try {
        const response = await fetch("https://localhost:7267/Users");
        if (response.ok) {
          const data = await response.json();
          console.log("Empfangene Benutzerdaten:", data); // Debugging
          this.users = data;
        } else {
          console.error("Fehler beim Laden der Benutzer:", await response.text());
        }
      } catch (error) {
        console.error("Fehler beim Laden der Benutzer:", error);
      }
    },
  },
};
</script>

<style>
body {
  margin: 0;
  font-family: "Roboto", sans-serif;
  font-size: 18px; /* Allgemeine Schriftgröße vergrößert */
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
  margin-top: 100px;
  font-size: 18px; /* Schriftgröße der Buttons vergrößert */
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

.box {
  margin-top: 100px;
}

/* Add the new font-family */
@import url("https://fonts.googleapis.com/css2?family=Montserrat:wght@400;700&display=swap");

body {
  font-family: "Montserrat", sans-serif; /* Apply the modern font to the entire body */
}

.btn1 {
  margin: 20px;
}

.footer {
  margin-top: 200px;
  border-radius: 20px;
}

.v-tooltip .v-overlay__content {
  font-size: 18px; /* Schriftgröße der Tooltips vergrößert */
  color: white !important; /* Weißer Text */
  background-color: rgb(98, 98, 98) !important; /* Schwarzer Hintergrund */
  padding: 10px 15px; /* Größeres Padding */
  border-radius: 8px; /* Runde Ecken */
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.3); /* Schatteneffekt */
  text-align: center; /* Text zentrieren */
}

/* Anpassungen für den Titel und Untertitel in der Hero Section */
.display-1 {
  font-size: 4rem; /* Titel größer */
}

.subtitle-2 {
  font-size: 1.5rem ; /* Untertitel größer */
}
</style>