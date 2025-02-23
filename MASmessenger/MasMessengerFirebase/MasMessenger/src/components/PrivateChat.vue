<template>
  <div class="private-chat">
    <!-- Benutzerliste -->
    <v-card class="user-list-card" elevation="2">
      <v-card-title class="user-list-title">Benutzer</v-card-title>
      <v-list>
        <v-list-item
          v-for="user in users"
          :key="user.uid"
          @click="openChat(user.uid)"
          :class="{ 'current-user': user.uid === currentUserUid }"
        >
          <v-avatar size="40" class="mr-3">
            <v-img :src="getAvatarUrl(user.photoURL)" />
          </v-avatar>
          <v-list-item-content>
            <v-list-item-title>{{ user.displayName }}</v-list-item-title>
            <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-card>

    <!-- Chat-Bereich -->
    <div class="chat-area">
      <!-- Header mit Empfängername und Zurück-Button -->
      <v-card class="header-card" elevation="2">
        <v-card-title class="header-title">
          <v-btn icon @click="$router.push('/main')" class="back-btn">
            <v-icon>mdi-arrow-left</v-icon>
          </v-btn>

          <!-- Empfängername und Avatar -->
          <v-avatar size="40" class="mr-3 avatar">
            <v-img :src="getAvatarUrl(receiver.photoURL)" />
          </v-avatar>
          <span>{{ receiver.displayName || "Unbekannter Benutzer" }}</span>
        </v-card-title>
      </v-card>

      <!-- Nachrichtenbereich -->
      <div ref="chatContainer" class="chat-container">
        <!-- Ladeanzeige -->
        <v-alert v-if="isLoading" type="info">
          Nachrichten werden geladen...
        </v-alert>

        <!-- Fehlermeldung, wenn Empfänger nicht gefunden wird -->
        <v-alert v-if="receiverError" type="error" class="mb-4">
          Empfänger wurde nicht gefunden!
        </v-alert>

        <!-- Nachrichten anzeigen -->
        <div
          v-for="msg in messages"
          :key="msg.id"
          class="message"
          :class="{'sent': msg.senderID === user?.uid, 'received': msg.senderID !== user?.uid}"
        >
          <v-avatar size="36" class="mr-2">
            <v-img :src="getAvatarUrl(msg.senderPhotoURL)" />
          </v-avatar>
          <div class="message-content">
            <p>{{ msg.Text }}</p>
            <span class="sender-name">{{ msg.senderDisplayName }}</span>
          </div>
        </div>
      </div>

      <!-- Nachrichten-Eingabebereich -->
      <v-card class="input-card" elevation="2">
        <v-text-field
          v-model="formValue"
          placeholder="Schreibe eine Nachricht..."
          outlined
          dense
          hide-details
          @keyup.enter="sendMessage"
        >
          <template v-slot:append>
            <v-btn icon @click="sendMessage" :disabled="!formValue">
              <v-icon color="primary">mdi-send</v-icon>
            </v-btn>
          </template>
        </v-text-field>
      </v-card>
    </div>
  </div>
</template>

<script>
import { ref, watch, nextTick, onMounted } from 'vue';
import { getAuth, onAuthStateChanged } from 'firebase/auth';
import { getFirestore, collection, query, where, orderBy, onSnapshot, addDoc, serverTimestamp, getDocs, limit } from 'firebase/firestore';
import { useRoute, useRouter } from 'vue-router';
import defaultAvatar from '../assets/default-avatar.png'; // Importiere den Standard-Avatar

export default {
  setup() {
    const formValue = ref('');
    const messages = ref([]);
    const chatContainer = ref(null);
    const route = useRoute();
    const router = useRouter();
    const receiverId = ref(route.params.uid); // ID des Empfängers aus der URL
    const receiver = ref({}); // Empfängerinformationen
    const auth = getAuth();
    const user = ref(null); // Aktueller Benutzer
    const users = ref([]); // Liste aller Benutzer
    const isLoading = ref(true); // Ladezustand
    const receiverError = ref(false); // Fehlerzustand für Empfänger
    const currentUserUid = ref(null); // UID des aktuell angemeldeten Benutzers

    // Benutzerstatus überwachen
    onMounted(() => {
      if (!receiverId.value) {
        console.error("Empfänger-ID fehlt.");
        router.push('/main');
        return;
      }

      onAuthStateChanged(auth, (currentUser) => {
        if (currentUser) {
          user.value = currentUser;
          currentUserUid.value = currentUser.uid; // UID des aktuellen Benutzers speichern
          loadMessages(currentUser.uid);
          loadReceiverInfo();
          loadUsers();
        } else {
          router.push('/');
        }
      });
    });

    // Nachrichten aus der privatemasseges-Sammlung laden
    const loadMessages = async (userId) => {
      try {
        const db = getFirestore();
        const messagesRef = collection(db, 'privatemasseges'); // Sammlung "privatemasseges"
        const q = query(
          messagesRef,
          where('senderID', 'in', [userId, receiverId.value]),
          where('receiverID', 'in', [userId, receiverId.value]),
          orderBy('timeStamp', 'asc')
        );

        onSnapshot(q, (snapshot) => {
          messages.value = snapshot.docs.map((doc) => ({
            id: doc.id,
            ...doc.data(),
          }));
          isLoading.value = false;

          // Automatisch nach unten scrollen, wenn neue Nachrichten geladen werden
          nextTick(() => {
            if (chatContainer.value) {
              chatContainer.value.scrollTop = chatContainer.value.scrollHeight;
            }
          });
        });
      } catch (error) {
        console.error("Fehler beim Laden der Nachrichten:", error);
        alert("Fehler beim Laden der Nachrichten.");
        isLoading.value = false;
      }
    };

    // Empfängerinformationen aus der users-Sammlung laden
    const loadReceiverInfo = async () => {
      try {
        const db = getFirestore();
        const usersRef = collection(db, 'users'); // Sammlung "users"
        const q = query(usersRef, where('uid', '==', receiverId.value));
        const querySnapshot = await getDocs(q);

        if (!querySnapshot.empty) {
          const userDoc = querySnapshot.docs[0];
          receiver.value = {
            uid: userDoc.data().uid,
            displayName: userDoc.data().displayName,
            photoURL: userDoc.data().photoURL,
          };
          receiverError.value = false;
        } else {
          console.error("Empfänger nicht gefunden.");
          receiverError.value = true;
        }
      } catch (error) {
        console.error("Fehler beim Laden der Empfängerinformationen:", error);
        receiverError.value = true;
      }
    };

    // Alle Benutzer aus der users-Sammlung laden und nach neuester Nachricht sortieren
    const loadUsers = async () => {
      try {
        const db = getFirestore();
        const usersRef = collection(db, 'users'); // Sammlung "users"
        const querySnapshot = await getDocs(usersRef);

        // Alle Benutzer laden
        const allUsers = querySnapshot.docs.map((doc) => ({
          uid: doc.data().uid,
          displayName: doc.data().displayName,
          email: doc.data().email,
          photoURL: doc.data().photoURL,
        }));

        // Aktuellen Benutzer aus der Liste entfernen
        const otherUsers = allUsers.filter((u) => u.uid !== user.value.uid);

        // Zeitstempel der letzten Nachricht für jeden Benutzer abrufen
        const usersWithLastMessage = await Promise.all(
          otherUsers.map(async (u) => {
            const messagesRef = collection(db, 'privatemasseges');
            const q = query(
              messagesRef,
              where('senderID', 'in', [user.value.uid, u.uid]),
              where('receiverID', 'in', [user.value.uid, u.uid]),
              orderBy('timeStamp', 'desc'),
              limit(1)
            );

            const snapshot = await getDocs(q);
            const lastMessage = snapshot.docs[0]?.data()?.timeStamp || null;

            return {
              ...u,
              lastMessageTimestamp: lastMessage,
            };
          })
        );

        // Benutzer nach dem Zeitstempel der letzten Nachricht sortieren
        usersWithLastMessage.sort((a, b) => {
          if (!a.lastMessageTimestamp && !b.lastMessageTimestamp) return 0;
          if (!a.lastMessageTimestamp) return 1;
          if (!b.lastMessageTimestamp) return -1;
          return b.lastMessageTimestamp.toMillis() - a.lastMessageTimestamp.toMillis();
        });

        // Eigenen Benutzer an den Anfang der Liste setzen
        const currentUser = allUsers.find((u) => u.uid === user.value.uid);
        users.value = [currentUser, ...usersWithLastMessage];
      } catch (error) {
        console.error("Fehler beim Laden der Benutzerliste:", error);
      }
    };

    // Chat mit einem Benutzer öffnen
    const openChat = async (uid) => {
      // Aktualisiere die Empfänger-ID
      receiverId.value = uid;

      // Lade die Nachrichten für den neuen Empfänger
      await loadMessages(user.value.uid);

      // Lade die Empfängerinformationen
      await loadReceiverInfo();

      // Scroll zum unteren Ende des Chat-Containers
      nextTick(() => {
        if (chatContainer.value) {
          chatContainer.value.scrollTop = chatContainer.value.scrollHeight;
        }
      });
    };

    // Nachricht senden
    const sendMessage = async () => {
      if (!user.value) {
        console.error('Kein Benutzer angemeldet.');
        return;
      }

      if (!formValue.value.trim()) {
        alert("Bitte gib eine Nachricht ein.");
        return;
      }

      const { uid, photoURL, displayName } = user.value;

      try {
        await addDoc(collection(getFirestore(), 'privatemasseges'), {
          senderID: uid,
          receiverID: receiverId.value,
          Text: formValue.value.trim(),
          timeStamp: serverTimestamp(),
          senderPhotoURL: photoURL || defaultAvatar, // Profilbild des Senders
          senderDisplayName: displayName || "Unbekannter Benutzer", // Name des Senders
        });

        formValue.value = ''; // Eingabefeld zurücksetzen
      } catch (error) {
        console.error('Fehler beim Senden der Nachricht:', error);
        alert('Nachricht konnte nicht gesendet werden. Bitte versuchen Sie es erneut.');
      }
    };

    // Automatisch nach unten scrollen, wenn neue Nachrichten geladen werden
    watch(messages, () => {
      nextTick(() => {
        if (chatContainer.value) {
          chatContainer.value.scrollTop = chatContainer.value.scrollHeight;
        }
      });
    }, { deep: true });

    // Methode zur Bestimmung der Avatar-URL
    const getAvatarUrl = (photoURL) => {
      return photoURL === 'https://example.com/avatar.jpg' || !photoURL ? defaultAvatar : photoURL;
    };

    return { messages, formValue, sendMessage, user, chatContainer, receiver, users, openChat, defaultAvatar, isLoading, receiverError, currentUserUid, getAvatarUrl };
  },
};
</script>



<style scoped>
body {
  background-color: #282c34;
  margin: 0;
  font-family: 'Inter', sans-serif;
}

.private-chat {
  display: flex;
  height: 100vh;
  max-width: 1200px;
  margin: 0 auto;
  background-color: #282c34;
}

.user-list-card {
  width: 300px;
  background-color: #181717;
  color: white;
  border-radius: 12px; /* Abgerundete Ecken */
  overflow-y: auto;
  margin-right: 20px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
  margin-left: 20px;
}

.user-list-title {
  background-color: #181717;
  color: white;
  padding: 20px;
  font-size: 1.2rem;
  font-weight: bold;
  border-top-left-radius: 12px; /* Abgerundete Ecken oben */
  border-top-right-radius: 12px;
}

.chat-area {
  flex: 1;
  display: flex;
  flex-direction: column;
  background-color: #282c34;
}

.header-card {
  background-color: #181717;
  height: 80px;
  color: white;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 20px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
  border-radius: 12px; /* Abgerundete Ecken */
}

.header-title {
  display: flex;
  align-items: center;
}

.back-btn {
  margin-right: 10px;
}

.chat-container {
  flex: 1;
  padding: 20px;
  overflow-y: scroll;
  display: flex;
  flex-direction: column;
}

.chat-container::-webkit-scrollbar {
  width: 8px;
}

.chat-container::-webkit-scrollbar-track {
  background: #1e1e24;
  border-radius: 4px;
}

.chat-container::-webkit-scrollbar-thumb {
  background: #6649b8;
  border-radius: 4px;
}

.message {
  display: flex;
  align-items: flex-start;
  margin-bottom: 20px;
  margin: 10px;
}

.message.sent {
  flex-direction: row-reverse;
}

.message-content {
  max-width: 70%;
  padding: 12px 16px;
  border-radius: 12px;
  background-color: #e5e5ea;
}

.message.sent .message-content {
  background-color: #512da8;
  color: white;
  border-radius: 12px 12px 0 12px;
}

.message.received .message-content {
  background-color: #e5e5ea;
  color: black;
  border-radius: 12px 12px 12px 0;
}

.sender-name {
  font-size: 0.8rem;
  color: #666;
  margin-top: 4px;
  display: block;
}

.input-card {
  height: 80px;
  background-color: rgb(24, 23, 23);
  display: flex;
  align-items: center;
  padding: 0 20px;
  box-shadow: 0 -4px 12px rgba(0, 0, 0, 0.3);
  border-radius: 12px; /* Abgerundete Ecken */
}

.input-card input {
  flex: 1;
  height: 50px;
  font-size: 1.2rem;
  background: rgb(58, 58, 58);
  color: white;
  outline: none;
  border: none;
  border-radius: 12px;
  padding: 0 15px;
  margin-right: 10px;
}

.input-card button {
  width: 100px;
  height: 50px;
  background-color: #98aff8;
  border: none;
  color: white;
  border-radius: 12px;
  font-size: 1.2rem;
  cursor: pointer;
  transition: background-color 0.2s;
}

.input-card button:hover {
  background-color: #512da8;
}

.input-card button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

/* Stil für den aktuellen Benutzer */
.current-user {
  background-color: #512da8;
  color: white;
  border-radius: 12px; /* Abgerundete Ecken */
  padding: 8px;
  margin: 4px 0; /* Etwas Abstand */
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.2); /* Leichter Schatten */
}


</style>