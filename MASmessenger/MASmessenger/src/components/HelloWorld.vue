<template>
  <v-app class="app-container">
    <!-- Logo -->

    <!-- Titel -->
    <v-container class="text-left">
      <h1 class="app-title">MasMessenger</h1>
      <p class="app-subtitle">von Ali, Max, Matej</p>
    </v-container>

    <!-- Login-Formular -->
    <v-container class="login-container">
      <v-card class="login-card elevation-10">
        <v-card-text>
          <h2 class="login-title">Willkommen zurück</h2>
          <p class="login-subtitle">
            Bitte logge dich in dein Konto ein, um fortzufahren.
          </p>

          <v-form>
            <v-text-field
              label="E-Mail"
              outlined
              dense
              color="blue"
              autocomplete="off"
              class="login-input"
            />
            <v-text-field
              label="Passwort"
              outlined
              dense
              color="blue"
              type="password"
              autocomplete="off"
              class="login-input"
            />
            <v-row justify="space-between" class="login-options">
              <v-checkbox
                label="Angemeldet bleiben"
                class="login-checkbox"
                color="blue"
              />
              <span class="forgot-password">Passwort vergessen?</span>
            </v-row>

            <v-btn color="blue darken-2" block large tile class="login-btn">
              Einloggen
            </v-btn>
            <p class="login-subtitle">
            Noch kein Konto?
          </p>
            <v-btn @click="showRegisterForm = true" color="blue darken-2" block large tile class="login-btn2">
              Registrieren
            </v-btn>
          </v-form>
        </v-card-text>
      </v-card>
    </v-container>

    <!-- Registrierungsformular (nur sichtbar, wenn showRegisterForm true ist) -->
    <v-container v-if="showRegisterForm" class="login-container2">
      <v-card class="login-card elevation-10">
        <v-card-text>
          <h2 class="login-title">Registriere dich</h2>
          <p class="login-subtitle">
            Erstelle ein neues Konto, um fortzufahren.
          </p>

          <v-form>
            <v-text-field
              label="Nickname"
              outlined
              dense
              color="blue"
              autocomplete="off"
              class="login-input"
            />
            <v-text-field
              label="E-Mail"
              outlined
              dense
              color="blue"
              autocomplete="off"
              class="login-input"
            />
            <v-text-field
              label="Passwort"
              outlined
              dense
              color="blue"
              type="password"
              autocomplete="off"
              class="login-input"
            />
            <v-btn color="blue darken-2" block large tile class="login-btn">
              Weiter
            </v-btn>
            <p class="login-subtitle">
           
            </p>
          </v-form>
        </v-card-text>
      </v-card>
    </v-container>

    <!-- Footer-Buttons -->
    <v-container class="footer-buttons text-left">
      <v-btn class="footer-btn" color="purple darken-3" outlined>About</v-btn>
      <v-btn class="footer-btn" color="purple darken-3" outlined>Contact</v-btn>
    </v-container>

    <!-- Sparkline -->
    <v-container class="sparkline-container">
      <v-sparkline
        :value="sparklineData"
        :gradient="['#3a0ca3', '#7209b7', '#b5179e']"
        gradient-direction="top"
        line-width="3"
        auto-draw
        auto-draw-duration="2000"
      ></v-sparkline>
    </v-container>
  </v-app>
</template>

<v-divider class="divider"></v-divider>
<script>
export default {
  data: () => ({
    step: 1,
    sparklineData: [0, 2, 5, 9, 5, 10, 3, 5, 0, 0, 1, 8, 2, 9, 0],
    showRegisterForm: false,  // Neue Variable für die Steuerung der Anzeige des Formulars
  }),
  methods: {
    hoverEffect() {
      // Effekt bei Hover auf Haupt-Button
    },
    resetEffect() {
      // Effekt zurücksetzen
    },
  },
};
</script>


<style scoped>
/* Container-Styles */
.app-container {
  background: linear-gradient(135deg, #000000, #3a0ca3, #ffffff);
  height: 100vh;
  overflow: hidden;
  display: flex;
  font-family: Arial, sans-serif;
  color: #ffffff;
  padding: 20px;
  position: relative;
}

/* Titel-Styles */
.app-title {
  font-size: 3rem;
  font-weight: bold;
  margin-top: 50px;
  color: #ffffff;
}

.app-subtitle {
  font-size: 1.2rem;
  color: #e0e0e0;
}

/* Button-Styles */
.btn-container {
  margin-top: 30px;
}

.main-button {
  background-color: #3a0ca3 !important;
  color: white !important;
  font-weight: bold !important;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  box-shadow: 0px 4px 10px rgba(58, 12, 163, 0.5);
}

.main-button:hover {
  box-shadow: 0px 6px 15px rgba(58, 12, 163, 0.8);
}

/* Footer-Buttons */
.footer-buttons {
  position: absolute;
  bottom: 50px;
  left: 20px;
  display: flex;
  gap: 10px;
}

.footer-btn {
  background-color: #3a0ca3 !important;
  color: white !important;
  font-weight: bold !important;
  border-color: #3a0ca3 !important;
  transition: background-color 0.3s ease, color 0.3s ease;
}

.footer-btn:hover {
  background-color: #5c2dc5 !important;
  color: white !important;
}

/* Sparkline-Styles */
.sparkline-container {
  display: flex;
}

/* Login-Container */
.login-container {
  display: flex;
  justify-content: flex-start; /* Verschiebt den Container ganz nach rechts */
  align-items: flex-start;    /* Positioniert ihn höher */
          /* Etwas mehr Platz nach oben */
  margin-right: 200px;         /* Verschiebt den Container nach ganz rechts */
  height: 100%;
}

.login-container2 {
  display: flex;
  justify-content: center; /* Zentriert den Container horizontal */
  align-items: center;     /* Zentriert den Container vertikal */
  position: absolute;      /* Damit er relativ zum Container positioniert ist */
  top: 50%;                /* Positioniert den Container auf der vertikalen Mitte */
  left: 50%;               /* Positioniert den Container auf der horizontalen Mitte */
  transform: translate(-50%, -50%); /* Verschiebt den Container um 50% seiner Größe nach oben und links, um ihn exakt zu zentrieren */
  width: 100%;             /* Der Container kann die gesamte Breite einnehmen */
  height: 100%;            /* Der Container nimmt die gesamte Höhe ein */
  transition: 1s;          /* Übergangszeit für Animationen */
}

/* Login-Card */
.login-card {
  background: #1e1e2f;
  border-radius: 12px;
  padding: 20px;
  max-width: 400px;
  width: 100%;
  box-shadow: 0px 8px 20px rgba(0, 0, 0, 0.3);
  color: white;
 /* Höhere Positionierung */
}

/* Titel und Untertitel */
.login-title {
  text-align: center;
  font-size: 1.8rem;
  font-weight: bold;
  margin-bottom: 10px;
  color: #e0e0e0;
}

.login-subtitle {
  text-align: center;
  font-size: 1rem;
  color: #a0a0a0;
  margin-bottom: 20px;
}

/* Eingabefelder */
.login-input {
  margin-bottom: 20px;
  background-color: #282846;
  border-radius: 8px;
}

/* Optionen (Checkbox und Passwort vergessen) */
.login-options {
  margin-bottom: 20px;
}

.login-checkbox {
  font-size: 0.9rem;
  color: #e0e0e0;
}

.forgot-password {
  font-size: 0.9rem;
  color: #4a90e2;
  cursor: pointer;
}

.forgot-password:hover {
  text-decoration: underline;
}

/* Login-Button */
.login-btn {
  background-color: #3a0ca3 !important;
  color: white !important;
  font-weight: bold !important;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  box-shadow: 0px 4px 10px rgba(58, 12, 163, 0.5);
  margin-bottom: 20px;
}

.login-btn2 {
  background-color: #3a0ca3 !important;
  color: white !important;
  font-weight: bold !important;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  box-shadow: 0px 4px 10px rgba(58, 12, 163, 0.5);
}

.login-btn:hover {
  background-color: #5c2dc5;
  box-shadow: 0px 6px 15px rgba(58, 12, 163, 0.8);
}


</style>
