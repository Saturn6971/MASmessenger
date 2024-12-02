<template>
  <div class="wrapper">
    <v-card
      class="anmeldemaske pa-12 pb-8"
      elevation="8"
      max-width="448"
      rounded="lg"
    >
      <div class="text-subtitle-1 text-medium-emphasis">Account</div>

      <v-text-field
        density="compact"
        placeholder="Email address"
        prepend-inner-icon="mdi-email-outline"
        variant="outlined"
      ></v-text-field>

      <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between">
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
        Log In
      </v-btn>

      <v-divider></v-divider>   

      <div class="text-subtitle-1 text-medium-emphasis">
        Du hast noch kein Konto?
      </div>

      <v-btn
        class="signup"
        size="large"
        variant="tonal"
        block
      >
        Sign Up
      </v-btn>
    </v-card>
  </div>
</template>

<script>
export default {
  data: () => ({
    visible: false,
  }),
};
</script>

<style scoped>
.wrapper {
  display: flex;
  justify-content: center; /* Zunächst zentriert */
  align-items: center; /* Vertikale Zentrierung */
  height: 100vh; /* Volle Bildschirmhöhe */
  background-color: #000; /* Schwarzer Hintergrund */
}

.anmeldemaske {
  transform: translateX(-20%); /* Verschiebt das Anmeldefenster leicht nach links */
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
  margin-bottom: 20px;
  margin-top: 10px;
}
</style>
