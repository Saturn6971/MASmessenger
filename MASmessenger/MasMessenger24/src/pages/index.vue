<template class="app">
  <div class="container">
    <!-- Anmeldefenster -->
    <div class="wrapper">
      <v-card
        class="anmeldemaske pa-12 pb-8"
        elevation="8"
        max-width="448"
        rounded="lg"
      >
        <v-text class="Title">MASmessenger</v-text>

        <v-divider class="my-4"></v-divider>

        <!-- Dynamischer Titel -->
        <div class="text-subtitle-1 text-medium-emphasis mb-4">
          {{ isSignUp ? "Create an Account" : "Account" }}
        </div>

        <!-- Zusätzlicher Benutzername für Sign-up -->
        <v-text-field
          v-if="isSignUp"
          density="compact"
          placeholder="Username"
          prepend-inner-icon="mdi-account"
          variant="outlined"
        ></v-text-field>

        <v-text-field
          density="compact"
          placeholder="Email address"
          prepend-inner-icon="mdi-email-outline"
          variant="outlined"
        ></v-text-field>

        <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between mt-4 mb-2">
          Password
        </div>

        <v-text-field
          :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
          :type="visible ? 'text' : 'password'"
          density="compact"
          placeholder="Enter your password"
          prepend-inner-icon="mdi-lock-outline"
          variant="outlined"
          @click:append-inner="visible = !visible"
        ></v-text-field>

        <v-btn
          class="login"
          size="large"
          variant="tonal"
          block
        >
          {{ isSignUp ? "Sign Up" : "Log In" }}
        </v-btn>

        <v-divider class="my-4"></v-divider>

        <!-- Umschalter für Sign-up/Login -->
        <div class="text-subtitle-1 text-medium-emphasis mb-2">
          {{ isSignUp ? "Already have an account?" : "Don't have an account yet?" }}
        </div>

        <v-btn
          class="signup"
          size="large"
          variant="tonal"
          block
          @click="toggleSignUp"
        >
          {{ isSignUp ? "Log In" : "Sign Up" }}
        </v-btn>
      </v-card>
    </div>

    <!-- Bild -->
    <v-img class="Handy" src="../assets/Bild.png"></v-img>
  </div>

  <!-- Footer -->
  <div class="footer">
    <span class="follow-us">FOLLOW US</span>
    <div class="divider"></div>
    <div class="social-icons">
      <v-icon>mdi-facebook</v-icon>
      <v-icon>mdi-twitter</v-icon>
      <v-icon>mdi-instagram</v-icon>
    </div>
  </div>
</template>

<script>
export default {
  data: () => ({
    visible: false, // Passwort-Anzeige umschalten
    isSignUp: false, // Wechsel zwischen Login und Sign-up
  }),
  methods: {
    toggleSignUp() {
      this.isSignUp = !this.isSignUp; // Umschalten zwischen den Modi
    },
  },
};
</script>

<style scoped>
/* Container für beide Elemente */

html, body {
  background-color: #000; /* Schwarzer Hintergrund */
  margin: 0; /* Entfernt Standardränder des Browsers */
  padding: 0; /* Entfernt Standardabstände des Browsers */
  height: 100%; /* Höhe auf gesamten Bildschirm setzen */
  overflow: hidden;
}

.app {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  height: 100%; /* Füllt die gesamte Bildschirmhöhe */
  background-color: #000; /* Hintergrundfarbe bleibt schwarz */
  overflow: hidden;
}

.container {
  display: flex;
  justify-content: center; /* Zentriert den Inhalt horizontal */
  align-items: center; /* Zentriert den Inhalt vertikal */
  height: calc(100vh - 120px); /* Platz für den Footer freilassen */
  background-color: #000; /* Schwarzer Hintergrund */
  gap: 20px; /* Abstand zwischen Anmeldefenster und Bild */
}

/* Footer */
.footer {
  display: flex;
  justify-content: center;
  align-items: center;

  background-color: #000; /* Schwarzer Hintergrund */
  color: #bb45ff; /* Lila Text */
 padding: 50px;
  position: relative;
  
}

.follow-us {
  font-size: 16px;
  font-weight: bold;
  text-transform: uppercase;
  background-color: #000; /* Schwarzer Hintergrund */
}

.divider {
  height: 2px;
  background-color: #bb45ff;
  width: 100px;
  margin: 0 10px;
 
}

.social-icons v-icon {
  font-size: 24px;
  color: #bb45ff;
  margin: 0 8px;
  cursor: pointer;
  background-color: #000; /* Schwarzer Hintergrund */
}

.social-icons v-icon:hover {
  color: white; /* Hover-Effekt */
  background-color: #000; /* Schwarzer Hintergrund */
}

/* Anmeldefenster */
.wrapper {
  display: flex;
  flex-direction: column; /* Stellt sicher, dass Kinder vertikal gestapelt sind */

}

.anmeldemaske {
  width: 100%;
  width: 448px;
  margin-left: 100px;
}

.login {
  background-color: #bb45ff;
  color: white;
  border-radius: 25px;
  margin-bottom: 20px;
}

.signup {
  background-color: #bb45ff;
  color: white;
  border-radius: 25px;
  margin-top: 10px;
}

.text-medium-emphasis {
  color: rgba(255, 255, 255, 0.7); /* Weiche Textfarbe */
}

/* Bild */
.Handy {
  height: 800px;
  width: 800px;
  object-fit: cover; /* Skaliert das Bild proportional */
  border-radius: 10px; /* Optional: abgerundete Ecken */
}

.Title {
  margin-bottom: 10px;
  font-size: 40px;
  font-weight: bolder;
}
</style>
