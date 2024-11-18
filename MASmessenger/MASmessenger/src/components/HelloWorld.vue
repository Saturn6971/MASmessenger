<template>
  <v-app class="app-container">
    <!-- Logo -->
    <v-container class="logo-container">
     
    </v-container>

    <!-- Titel -->
    <v-container class="text-left">
      <h1 class="app-title">MasMessenger</h1>
      <p class="app-subtitle">von Ali, Max, Matej</p>
    </v-container>

    <!-- Haupt-Button -->
    <v-container class="text-left btn-container">
      <v-btn 
        class="main-button" 
        color="purple darken-3" 
        large
        depressed
        @mouseover="hoverEffect"
        @mouseleave="resetEffect"
      >
        Weiter
      </v-btn>
    </v-container>

    <!-- Footer-Buttons -->
    <v-container class="footer-buttons text-left">
      <v-btn class="footer-btn" color="purple darken-3" outlined>
        About
      </v-btn>
      <v-btn class="footer-btn" color="purple darken-3" outlined>
        Contact
      </v-btn>
    </v-container>
  </v-app>
</template>

<script>
export default {
  methods: {
    hoverEffect(event) {
      event.target.style.transform = "scale(1.1)";
    },
    resetEffect(event) {
      event.target.style.transform = "scale(1)";
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
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  font-family: Arial, sans-serif;
  color: #ffffff;
  padding: 20px;
  position: relative;
}

/* Logo-Styles */
.logo-container {
  position: absolute;
  top: 50%;
  right: 20px; /* Positioniert rechts */
  transform: translateY(-50%);
}

.logo {
  display: flex;
  width: 500px; /* Größe des Logos angepasst */
  height: auto;
  justify-content: center;
  margin-left: 500px;
  
  
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
  bottom: 70px;
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
</style>
