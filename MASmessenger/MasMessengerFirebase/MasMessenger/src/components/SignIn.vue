<template>
  <div class="app">
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
            v-model="firstName"
            ref="firstName"
            density="compact"
            placeholder="Vorname"
            prepend-inner-icon="mdi-account"
            variant="outlined"
            :rules="nameRules"
          ></v-text-field>

          <v-text-field
            v-if="isSignUp"
            v-model="lastName"
            ref="lastName"
            density="compact"
            placeholder="Nachname"
            prepend-inner-icon="mdi-account"
            variant="outlined"
            :rules="nameRules"
          ></v-text-field>

          <v-text-field
            v-model="email"
            ref="email"
            density="compact"
            placeholder="Email address"
            prepend-inner-icon="mdi-email-outline"
            variant="outlined"
            :rules="emailRules"
          ></v-text-field>

          <div class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between mt-4 mb-2">
            Password
          </div>

          <v-text-field
            v-model="password"
            ref="password"
            :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
            :type="visible ? 'text' : 'password'"
            density="compact"
            placeholder="Enter your password"
            prepend-inner-icon="mdi-lock-outline"
            variant="outlined"
            @click:append-inner="visible = !visible"
            :rules="passwordRules"
          ></v-text-field>

          <v-btn 
            class="login"
            size="large"
            variant="tonal"
            block
            @click="submitForm"
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

          <!-- Google-Anmeldung -->
          <v-btn
            class="google-signin mt-4"
            size="large"
            variant="tonal"
            block
            @click="signInWithGoogle"
          >
            <v-icon left>mdi-google</v-icon>
            Sign in with Google
          </v-btn>

        </v-card>
      </div>
      <v-img class="Handy" src="../assets/Bild.png"></v-img>
    </div>
  </div>

</template>

<script>
import { getAuth, createUserWithEmailAndPassword, signInWithEmailAndPassword, GoogleAuthProvider, signInWithPopup } from 'firebase/auth';
import { getFirestore, doc, setDoc } from 'firebase/firestore';

export default {
  data: () => ({
    visible: false,
    isSignUp: false,
    firstName: "",
    lastName: "",
    email: "",
    password: "",
  }),
  methods: {
    toggleSignUp() {
      this.isSignUp = !this.isSignUp;
    },

    async submitForm() {
      const auth = getAuth();
      const db = getFirestore();

      try {
        if (this.isSignUp) {
          // Benutzer registrieren
          const userCredential = await createUserWithEmailAndPassword(auth, this.email, this.password);
          const user = userCredential.user;

          // Zufällige receiverId und senderId generieren
          const receiverId = `rec_${Math.random().toString(36).substr(2, 9)}`;
          const senderId = `send_${Math.random().toString(36).substr(2, 9)}`;

          // Benutzer in Firestore speichern
          await setDoc(doc(db, "users", user.uid), {
            displayName: `${this.firstName} ${this.lastName}`,
            email: this.email,
            photoURL: "https://example.com/avatar.jpg",
            uid: user.uid,
            receiverId: receiverId,
            senderId: senderId,
          });

          console.log("Benutzer erfolgreich registriert und gespeichert!");
        } else {
          // Benutzer anmelden
          await signInWithEmailAndPassword(auth, this.email, this.password);
          console.log("Benutzer erfolgreich angemeldet!");
        }

        this.$router.push('/main');
      } catch (error) {
        console.error("Fehler bei der Authentifizierung:", error.message);
        alert(error.message);
      }
    },

    async signInWithGoogle() {
      try {
        const auth = getAuth();
        const db = getFirestore();
        const provider = new GoogleAuthProvider();
        const result = await signInWithPopup(auth, provider);
        const user = result.user;

        // Zufällige receiverId und senderId generieren
        const receiverId = `rec_${Math.random().toString(36).substr(2, 9)}`;
        const senderId = `send_${Math.random().toString(36).substr(2, 9)}`;

        // Benutzer in Firestore speichern
        await setDoc(doc(db, "users", user.uid), {
          displayName: user.displayName,
          email: user.email,
          photoURL: user.photoURL || "https://example.com/avatar.jpg",
          uid: user.uid,
          receiverId: receiverId,
          senderId: senderId,
        }, { merge: true });

        console.log("Erfolgreich mit Google angemeldet und gespeichert!");
        this.$router.push('/main');
      } catch (error) {
        console.error("Fehler bei der Google-Anmeldung:", error.message);
        alert(error.message);
      }
    }
  }
};
</script>

<style scoped>
/* Container für beide Elemente */

.Title{
  font-size: 24px;
  font-weight: bolder;
}

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
  flex-direction: row; /* Elemente nebeneinander anordnen */
  justify-content: center; /* Zentriert den Inhalt horizontal */
  align-items: center; /* Zentriert den Inhalt vertikal */
  height: calc(100vh - 120px); /* Platz für den Footer freilassen */
  background-color: #000; /* Schwarzer Hintergrund */
  gap: 100px; /* Abstand zwischen Anmeldefenster und Bild */
  padding-left: 100px; /* Verschiebt das Anmeldefenster nach links */
}

/* Footer */
.footer {
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #000; /* Schwarzer Hintergrund */
  color: #bb45ff; /* Lila Text */
  padding: 45px;
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
  margin-right: 0; /* Kein zusätzlicher rechter Rand */
}

.anmeldemaske {
  width: 448px; /* Breite des Anmeldefensters */
  margin-left: 0; /* Kein zusätzlicher linker Rand */
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
  color: rgba(255, 255, 255, 0.7)
}

/* Google-Anmeldebutton */
.google-signin {
  background-color: #4285f4; /* Google-Blau */
  color: white;
  border-radius: 25px;
}

/* Bild */
.Handy {
  width: 900px; /* Passe die Breite nach Bedarf an */
  height: auto; /* Behält das Seitenverhältnis bei */
  margin-right: 70px; /* Abstand vom rechten Rand */
  margin-left: 150px;
}
</style>