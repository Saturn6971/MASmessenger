<template>
  <div class="App">
    <header>
      <h1>GroupChat</h1>
      <v-btn @click="$router.push('/main')" class="mb-6 back-btn" dark>Zurück</v-btn>
    </header>

    <section>
      <main ref="chatContainer">
        <div v-for="msg in messages" :key="msg.id" class="message" :class="{'sent': msg.uid === auth.currentUser?.uid, 'received': msg.uid !== auth.currentUser?.uid}">
          <img :src="msg.photoURL || defaultAvatar" alt="Avatar" />
          <div class="message-content">
            <strong>{{ msg.displayName }}</strong>
            <p>{{ msg.text }}</p>
          </div>
        </div>
      </main>

      <form @submit.prevent="sendMessage">
        <input v-model="formValue" placeholder="Sag etwas Nettes" />
        <button type="submit" :disabled="!formValue">
          <v-icon>mdi-send</v-icon>
        </button>
      </form>
    </section>
  </div>
</template>

<script>
import { ref, watch, nextTick } from 'vue';
import { auth, firestore, collection, query, orderBy, limit, addDoc, serverTimestamp } from '../Firebase';
import { useCollection } from 'vuefire';
import { useRouter } from 'vue-router';
import defaultAvatar from '../assets/default-avatar.png'; // Importiere den Standard-Avatar

export default {
  setup() {
    const formValue = ref('');
    const messagesRef = collection(firestore, 'messages');
    const q = query(messagesRef, orderBy('createdAt', 'asc'), limit(50));
    const messages = useCollection(q);
    const chatContainer = ref(null);
    const router = useRouter();

    watch(messages, () => {
      nextTick(() => {
        if (chatContainer.value) {
          chatContainer.value.scrollTop = chatContainer.value.scrollHeight;
        }
      });
    }, { deep: true });

    const sendMessage = async () => {
      if (!auth.currentUser) {
        console.error('Kein Benutzer angemeldet.');
        return;
      }

      const { uid, photoURL, displayName } = auth.currentUser;

      try {
        await addDoc(messagesRef, {
          text: formValue.value.trim(),
          createdAt: serverTimestamp(),
          uid,
          photoURL: photoURL || defaultAvatar, // Verwende das importierte Standard-Avatar
          displayName: displayName || 'Unbekannt'
        });

        formValue.value = '';
      } catch (error) {
        console.error('Fehler beim Senden der Nachricht:', error);
        alert('Nachricht konnte nicht gesendet werden. Bitte versuche es erneut.');
      }
    };

    const handleSignOut = async () => {
      try {
        await auth.signOut();
        router.push('/');
      } catch (error) {
        console.error('Fehler beim Abmelden:', error);
      }
    };

    return { messages, formValue, sendMessage, auth, chatContainer, handleSignOut, defaultAvatar };
  }
};
</script>

<style scoped>
body {
  background-color: #282c34;
  margin: 0;
  font-family: 'Inter', sans-serif;
}

.App {
  text-align: center;
  max-width: 1200px; /* Breiter gemacht */
  margin: 0 auto;
}

.App header {
  background-color: #181717;
  height: 80px;
  color: white;
  position: fixed;
  width: 100%;
  max-width: 1200px; /* Breiter gemacht */
  top: 0;
  display: flex;
  align-items: center;
  justify-content: space-between;
  z-index: 99;
  padding: 0 20px;
  box-sizing: border-box;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.3);
}

.App header h1 {
  font-size: 28px;
  font-weight: bold;
  margin: 0;
}

.App section {
  display: flex;
  flex-direction: column;
  justify-content: center;
  min-height: 100vh;
  background-color: rgb(40, 37, 53);
  padding-top: 80px; /* Platz für den Header */
}

main {
  padding: 20px;
  height: calc(100vh - 160px); /* Angepasst für Header und Formular */
  overflow-y: scroll;
  display: flex;
  flex-direction: column;
}

main::-webkit-scrollbar {
  width: 8px; /* Dickere Scrollbar */
}

main::-webkit-scrollbar-track {
  background: #1e1e24;
}

main::-webkit-scrollbar-thumb {
  background: #6649b8;
  border-radius: 4px; /* Abgerundete Scrollbar */
}

form {
  height: 80px;
  position: fixed;
  bottom: 0;
  background-color: rgb(24, 23, 23);
  width: 100%;
  max-width: 1200px; /* Breiter gemacht */
  display: flex;
  align-items: center;
  padding: 0 20px;
  box-sizing: border-box;
}

form input {
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

form button {
  width: 100px;
  height: 50px;
  background-color: #6e8efb;
  border: none;
  color: white;
  border-radius: 12px;
  font-size: 1.2rem;
  cursor: pointer;
  transition: background-color 0.2s;
}

form button:hover {
  background-color: #4a6cf7;
}

form button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.message {
  display: flex;
  align-items: flex-start;
  margin-bottom: 20px;
}

.message img {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  margin-right: 10px;
}

.message-content {
  max-width: 70%;
}

.message-content strong {
  font-size: 16px;
  font-weight: bold;
  color: #ffffff;
  display: block;
  margin-bottom: 4px;
}

.message-content p {
  font-size: 14px;
  margin: 0;
  padding: 12px 16px;
  border-radius: 12px;
  line-height: 1.5;
}

.sent {
  flex-direction: row-reverse;
}

.sent .message-content {
  align-items: flex-end;
}

.sent .message-content p {
  background: #0b93f6;
  color: white;
  border-radius: 12px 12px 0 12px;
}

.received .message-content p {
  background: #e5e5ea;
  color: black;
  border-radius: 12px 12px 12px 0;
}
</style>