<template>
  <div class="user-list">
    <!-- Titel -->
    <v-card class="header-card" elevation="2">
      <v-card-title class="header-title">
        <v-icon large class="mr-2">mdi-account-group</v-icon>
        Benutzerliste
      </v-card-title>
    </v-card>

    <!-- Suchfeld -->
    <v-text-field
      v-model="searchQuery"
      label="Benutzer suchen"
      class="search-input"
      clearable
      outlined
      dense
    ></v-text-field>

    <!-- Benutzerliste -->
    <v-card class="user-card" elevation="2">
      <v-list>
        <v-list-item
          v-for="user in filteredUsers"
          :key="user.uid"
          @click="startChat(user)"
          class="user-item"
          :class="{ 'current-user': user.uid === currentUserUid }"
        >
          <!-- Benutzeravatar -->
          <v-avatar size="56" class="mr-4">
            <v-img :src="getAvatarUrl(user.photoURL)" />
          </v-avatar>

          <!-- Benutzerdetails -->
          <v-list-item-content>
            <v-list-item-title class="user-name">
              {{ user.displayName || "Unbekannter Benutzer" }}
              <v-icon v-if="user.uid === currentUserUid" color="white" class="ml-2">mdi-account-check</v-icon>
            </v-list-item-title>
            <v-list-item-subtitle class="user-email">
              {{ user.email || "Keine E-Mail angegeben" }}
            </v-list-item-subtitle>
          </v-list-item-content>

          <!-- Chat-Icon -->
          <v-list-item-action>
            <v-icon color="white">mdi-message-text</v-icon>
          </v-list-item-action>
        </v-list-item>
      </v-list>

      <!-- Keine Benutzer gefunden -->
      <v-alert v-if="filteredUsers.length === 0" type="info" class="no-users-alert">
        Keine Benutzer gefunden.
      </v-alert>
    </v-card>
  </div>
</template>

<script>
import { getFirestore, collection, onSnapshot } from 'firebase/firestore';
import { getAuth, onAuthStateChanged } from 'firebase/auth';
import defaultAvatar from '@/assets/default-avatar.png'; // Importiere das Standard-Avatar-Bild

export default {
  data() {
    return {
      users: [], // Liste aller Benutzer aus der users-Sammlung
      searchQuery: '', // Suchbegriff
      currentUserUid: null, // UID des aktuell angemeldeten Benutzers
    };
  },
  computed: {
    // Gefilterte Benutzerliste basierend auf dem Suchbegriff
    filteredUsers() {
      // Filtere die Benutzerliste basierend auf dem Suchbegriff
      const filtered = this.users.filter(user =>
        user.displayName.toLowerCase().includes(this.searchQuery.toLowerCase())
      );

      // Sortiere die Liste, sodass der aktuelle Benutzer immer an erster Stelle steht
      return filtered.sort((a, b) => {
        if (a.uid === this.currentUserUid) return -1; // Aktueller Benutzer nach oben
        if (b.uid === this.currentUserUid) return 1; // Aktueller Benutzer nach oben
        return 0; // Andere Benutzer bleiben unverändert
      });
    },
  },
  created() {
    this.loadUsers();
    this.getCurrentUser();
  },
  methods: {
    async loadUsers() {
      const db = getFirestore();
      const usersRef = collection(db, 'users'); // Benutzer aus der users-Sammlung

      // Benutzer aus Firestore abrufen
      onSnapshot(usersRef, (snapshot) => {
        this.users = snapshot.docs.map(doc => ({
          uid: doc.id,
          displayName: doc.data().displayName || "Unbekannter Benutzer",
          email: doc.data().email || "Keine E-Mail angegeben",
          photoURL: doc.data().photoURL || defaultAvatar, // Verwende das importierte Standard-Avatar
        }));
      }, (error) => {
        console.error("Fehler beim Laden der Benutzer:", error);
      });
    },
    startChat(user) {
      if (user.uid !== this.currentUserUid) {
        this.$router.push(`/chat/${user.uid}`); // Weiterleitung zum Chat mit dem ausgewählten Benutzer
      }
    },
    getCurrentUser() {
      const auth = getAuth();
      onAuthStateChanged(auth, (user) => {
        if (user) {
          this.currentUserUid = user.uid; // UID des aktuell angemeldeten Benutzers speichern
        }
      });
    },
    getAvatarUrl(photoURL) {
      // Wenn die photoURL 'https://example.com/avatar.jpg' ist oder nicht vorhanden, verwende das Standard-Avatar
      return photoURL === 'https://example.com/avatar.jpg' || !photoURL ? defaultAvatar : photoURL;
    },
  },
};
</script>

<style scoped>
.user-list {
  padding: 20px;
  max-width: 800px;
  margin: 0 auto;
}

.header-card {
  margin-bottom: 20px;
  background: linear-gradient(135deg, #fb6eef, #512da8);
  color: white;
}

.header-title {
  font-size: 1.5rem;
  font-weight: bold;
}

.search-input {
  margin-bottom: 20px;
}

.user-card {
  padding: 20px;
  border-radius: 12px;
}

.user-item {
  margin-bottom: 10px;
  border-radius: 8px;
  transition: background-color 0.3s ease, transform 0.3s ease;
}

.user-item:hover {
  background-color: #bbbbbb;
  transform: translateY(-2px);
}

.user-name {
  font-size: 1.1rem;
  font-weight: bold;
}

.user-email {
  font-size: 0.9rem;
  color: #666;
}

.no-users-alert {
  margin-top: 20px;
}

/* Stil für den aktuellen Benutzer */
.current-user {
  background-color: #f4d7fc; /* Hellblauer Hintergrund */
  border-left: 4px solid #512da8; /* Blaue linke Randlinie */
}

.current-user .user-name {
  color: #512da8; /* Blaue Schriftfarbe */
}
</style>