<template>
  <div class="account-page">
    <v-container>
      <!-- Zurück-Button -->
      <v-btn @click="$router.push('/main')" class="mb-6 back-btn" dark>Zurück</v-btn>
      <v-btn @click="signOut" class="mb-6 back-btn2" dark>SignOut</v-btn>

      <!-- Benutzerinformationen -->
      <v-card v-if="user" class="pa-6 mb-6 user-card" dark>
        <v-card-title class="text-h4">Account Information</v-card-title>
        <v-divider class="my-4"></v-divider>
        <v-card-text>
          <v-avatar size="120" class="mb-6">
            <img :src="getAvatarUrl(user.photoURL)" alt="Profilbild" />
          </v-avatar>
          <v-list>
            <v-list-item>
              <v-list-item-title>Name:</v-list-item-title>
              <v-list-item-subtitle>{{ user.displayName }}</v-list-item-subtitle>
            </v-list-item>
            <v-list-item>
              <v-list-item-title>E-Mail:</v-list-item-title>
              <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
            </v-list-item>
            <v-list-item>
              <v-list-item-title>Benutzer-ID:</v-list-item-title>
              <v-list-item-subtitle>{{ user.uid }}</v-list-item-subtitle>
            </v-list-item>
          </v-list>
        </v-card-text>
      </v-card>

      <!-- Ladeanzeige, falls Benutzer noch geladen wird -->
      <v-card v-else class="pa-6 mb-6" dark>
        <v-card-title class="text-h4">Lade Benutzerdaten...</v-card-title>
      </v-card>
    </v-container>
  </div>
</template>

<script>
import { getAuth, onAuthStateChanged, signOut } from 'firebase/auth';
import { getFirestore, doc, getDoc } from 'firebase/firestore';
import defaultAvatar from '../assets/default-avatar.png'; // Importiere den Standard-Avatar

export default {
  data() {
    return {
      user: null, // Benutzer aus der users-Sammlung
      defaultAvatar: defaultAvatar, // Verwende das importierte Standard-Avatar
    };
  },
  created() {
    this.fetchUserData();
  },
  methods: {
    async fetchUserData() {
      const auth = getAuth();
      const db = getFirestore();

      onAuthStateChanged(auth, async (firebaseUser) => {
        if (firebaseUser) {
          try {
            const userDoc = await getDoc(doc(db, 'users', firebaseUser.uid));
            if (userDoc.exists()) {
              this.user = {
                displayName: userDoc.data().displayName,
                email: userDoc.data().email,
                uid: firebaseUser.uid,
                photoURL: userDoc.data().photoURL,
              };
            } else {
              console.error('Benutzer nicht in der users-Sammlung gefunden');
              this.$router.push('/');
            }
          } catch (error) {
            console.error('Fehler beim Abrufen der Benutzerdaten:', error);
          }
        } else {
          this.$router.push('/');
        }
      });
    },
    async signOut() {
      const auth = getAuth();
      try {
        await signOut(auth);
        this.$router.push('/');
      } catch (error) {
        console.error('Fehler beim Abmelden:', error);
      }
    },
    getAvatarUrl(photoURL) {
      // Wenn die photoURL 'https://example.com/avatar.jpg' ist oder nicht vorhanden, verwende das Standard-Avatar
      return photoURL === 'https://example.com/avatar.jpg' || !photoURL ? this.defaultAvatar : photoURL;
    },
  },
};
</script>

<style scoped>
.account-page {
  padding: 20px;
  background-color: #121212; /* Dunkler Hintergrund */
  min-height: 100vh;
  color: #fff; /* Helle Schriftfarbe */
}

.user-card {
  background-color: #1e1e1e; /* Dunkle Kartenfarbe */
}

.back-btn {
  background-color: #7e57c2; /* Lila Button */
  color: white;
}

.back-btn2 {
  background-color: #e65151; /* Roter Button für SignOut */
  color: white;
  margin-left: 20px;
}

.back-btn:hover {
  background-color: #9575cd; /* Hellere Lila-Farbe beim Hover */
}
</style>