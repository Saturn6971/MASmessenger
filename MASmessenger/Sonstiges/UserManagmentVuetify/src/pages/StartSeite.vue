<template>
  <v-app>
    <v-container class="no-scroll">
      <v-row justify="center">
        <v-col cols="12" md="8" lg="6">
          <v-card class="pa-4">
            <v-card-title v-if="!loggedIn">Login</v-card-title>
            <v-form v-if="!loggedIn" @submit.prevent="login">
              <v-text-field v-model="email" label="Email" type="email" variant="solo-filled" required></v-text-field>
              <v-text-field v-model="password" label="Password" type="password" variant="solo-filled" required></v-text-field>
              <v-btn type="submit" color="primary" class="logintbn" variant="tonal">Login</v-btn>
              <v-btn type="button" color="green" class="adminviewbtn" variant="tonal" @click="redirectToUrlAdmin()">Admin View</v-btn>
              <v-btn type="button" color="purple" class="startseitebtn" variant="tonal" @click="redirectToUrlStartpage()">Startpage</v-btn>
            </v-form>

            <div v-else>
              <v-card-title v-if="!isEditing">Profil</v-card-title>
              <v-card-title v-else>edit User</v-card-title>
              <div class="avatar-container">
                <v-img :src="'data:image/jpeg;base64,' + userData.image" class="avatar" v-if="userData.image"></v-img>
              </div>

              <v-form v-if="isEditing" @submit.prevent="updateUser" class="editfeld">
                <v-text-field v-model="userData.name" label="name" variant="solo-filled" required></v-text-field>
                <v-text-field v-model="userData.surname" label="surname" variant="solo-filled"></v-text-field>
                <v-text-field v-model="userData.email" label="email" variant="solo-filled" required></v-text-field>
                <v-text-field v-model="userData.birthday" label="birthday" type="date" variant="solo-filled"></v-text-field>
                <v-text-field v-model="userData.job" label="job" variant="solo-filled"></v-text-field>
                <v-text-field v-model="userData.gender" label="gender" variant="solo-filled"></v-text-field>
                <v-text-field v-model="userData.nationality" label="nationality" variant="solo-filled"></v-text-field>
                <v-text-field v-model="userData.role" label="role" variant="solo-filled" readonly ></v-text-field>
                <v-file-input v-model="newImageFile" label="image" variant="solo-filled"></v-file-input>
                <v-row>
                <v-text-field v-model="newPassword" label="password" type="password" variant="solo-filled" class="passwordtext"></v-text-field>
                <v-btn class="info" v-tooltip="'Bitte gebe nur etwas ein wenn du dein Passwort ändern möchtest!'" v-bind="attrs" v-on="on" icon><v-icon>mdi-information</v-icon></v-btn>
              </v-row>
                <v-btn type="submit" color="primary" class="editbtn" variant="tonal">Save</v-btn>
                <v-btn type="button" @click="isEditing = false" color="error" class="logout" variant="tonal">Cancel</v-btn>
              </v-form>

              <div v-else>
                <v-card-subtitle class="user-name">{{ userData.name }}</v-card-subtitle>
                <v-card-subtitle class="user-info">Surname: {{ userData.surname }}</v-card-subtitle>
                <v-card-subtitle class="user-info">Email: {{ userData.email }}</v-card-subtitle>
                <v-card-subtitle class="user-info">Birthday: {{ formatDate(userData.birthday) }}</v-card-subtitle>
                <v-card-subtitle class="user-info">Age: {{ calculateAge(userData.birthday) }} years</v-card-subtitle>
                <v-btn @click="isEditing = true" color="orange" class="editbtn" variant="tonal">Edit</v-btn>
                <v-btn @click="logout" color="error" class="logout" variant="tonal">Logout</v-btn>
              </div>
            </div>
          </v-card>
          <!-- Chat Section, nur sichtbar wenn der Benutzer angemeldet ist -->
  <v-card v-if="loggedIn" class="textchat mt-4">
    <v-card-title class="mt-4">Chat</v-card-title>
    <v-container class="chat-container">
      <!-- Nachrichtenanzeige -->
      <v-btn @click="fetchMessages()" color="primary" class="load-more-btn" variant="tonal">
        Load previous Nachrichten
      </v-btn>

      <v-row class="chat-messages" >
        <v-col cols="12" v-for="message in messages" :key="message.id">
          <div :class="{'message-from-user': isUserMessage(message), 'message-from-others': !isUserMessage(message)}">
          <v-card class="nachricht pa-2 mb-2">
            <v-card-title class="user-info-name">{{ message.name }}</v-card-title>
            <v-card-subtitle class="messagetext">{{ message.messageText }}</v-card-subtitle>
            <v-card-subtitle class="user-info-date">{{ formatDateNachricht(message.timestamp) }}</v-card-subtitle>
              <!-- Löschen-Button -->
          <v-btn v-if="isUserMessage(message)" @click="deleteMessage(message.oid)" icon class="delete-button">
                <v-icon>mdi-delete</v-icon>
              </v-btn>
          </v-card>
          </div>
        </v-col>
      </v-row>
</v-container>
<v-form @submit.prevent="sendMessage" class="chat-form">
    <v-textarea v-model="newMessage" label="Nachricht" rows="1" required class="chat-textarea" variant="solo-filled"></v-textarea>
    <v-btn type="submit" color="primary" class="sendbtn mt-2" variant="tonal">Send</v-btn>
  </v-form>
</v-card>
        </v-col>
 <!-- Users List Section -->
 <v-col cols="12" md="6">
          <v-card class="pa-4 users-list-card">
            <v-card-title>Users List</v-card-title>
            <v-container class="user-list-container">
              <v-row>
                <v-col v-for="user in users" :key="user.oid" cols="12">
                  <v-card class="card" elevation="16">
                    <v-img :src="'data:image/jpeg;base64,' + user.image"  cover v-if="user.image" class="avatar"></v-img>
                    <v-card-title class="textname">Name: {{ user.name }}</v-card-title>
                    <v-card-subtitle class="text">Surname: {{ user.surname }}</v-card-subtitle>
                    <v-card-subtitle class="text">Email: {{ user.email }}</v-card-subtitle>
                    <v-card-subtitle class="text">Birthday: {{ formatDate(user.birthday) }}</v-card-subtitle>
                    <v-card-subtitle class="text">Age: {{ calculateAge(user.birthday) }} years</v-card-subtitle>
                    <v-card-subtitle class="text">Gender: {{ user.gender }}</v-card-subtitle>
                    <v-card-subtitle class="text">Job: {{ user.job }}</v-card-subtitle>
                    <v-card-subtitle class="text">Nationality: {{ user.nationality }}</v-card-subtitle>
                    <v-card-subtitle class="text">Role: {{ user.role }}</v-card-subtitle>
                  </v-card>
                </v-col>
              </v-row>
            </v-container>
          </v-card>
      </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>


<script>
export default {
  data() {
    return {
      email: '',
      password: '',
      newPassword: '', 
      userData: null,
      newImageFile: null,
      loggedIn: false,
      isEditing: false,
      users: [],
      messages: [],
      newMessage: ''
    };
  },
  methods: {
  async fetchUsers() {
    try {
      const response = await fetch('http://localhost:5078/Users');
      if (response.ok) {
        this.users = await response.json();
      } else {
        console.error('Fehler beim Laden der Benutzer:', await response.text());
      }
    } catch (error) {
      console.error('Fehler beim Laden der Benutzer:', error);
    }
  },
  async login() {
try {
  const response = await fetch('http://localhost:5078/Users/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      email: this.email,
      password: this.password,  // Passwort wird beim Login gesendet
    }),
  });

  if (response.ok) {
    const user = await response.json();
    this.userData = {
      ...user,
      password: this.password,  // Passwort speichern
    };
    this.loggedIn = true;

    navigator.geolocation.getCurrentPosition(async (position) => {
      const latitude = position.coords.latitude;
      const longitude = position.coords.longitude;

      const logData = {
        user: {
          Oid: this.userData.oid,
          email: this.userData.email || '',
          name: this.userData.name || '',
          surname: this.userData.surname || '',
          job: this.userData.job || null,
          gender: this.userData.gender || null,
          nationality: this.userData.nationality || null,
          password: this.password  // Passwort verwenden
        },
        Latitude: latitude,
        Longitude: longitude,
        LoginTime: new Date().toISOString(),
      };

      const logResponse = await fetch('http://localhost:5078/Users/loginLog', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(logData),
      });

      if (!logResponse.ok) {
        const errorText = await logResponse.text();
        console.error('Fehler beim Speichern des Login-Logs:', errorText);
      }
    }, (error) => {
      console.error('Fehler beim Erfassen des Standorts:', error);
      alert('Standort konnte nicht ermittelt werden.');
    });
  } else {
    const errorText = await response.text();
    console.error('Login fehlgeschlagen:', errorText);
    alert('Login fehlgeschlagen');
  }
} catch (error) {
  console.error('Fehler beim Login:', error);
  alert('Es ist ein Fehler aufgetreten. Bitte versuchen Sie es erneut.');
}
},
  async updateUser() {
    let imageBase64 = this.userData.image;
    if (this.newImageFile) {
        try {
            imageBase64 = await this.toBase64(this.newImageFile);
        } catch (error) {
            console.error('Fehler beim Konvertieren des Bildes zu Base64', error);
            return;
        }
    }

    const updatedUser = {
        oid: this.userData.oid,
        name: this.userData.name,
        surname: this.userData.surname,
        email: this.userData.email,
        birthday: this.userData.birthday,
        job: this.userData.job,
        gender: this.userData.gender,
        nationality: this.userData.nationality,
        role: this.userData.role,
        image: imageBase64,
        password: this.newPassword || this.userData.password
    };

    try {
        const response = await fetch(`http://localhost:5078/Users/${this.userData.oid}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(updatedUser),
        });

        if (response.ok) {
            this.isEditing = false;
            this.fetchUsers();
        } else {
            const errorText = await response.text();
            console.error('Fehler beim Aktualisieren des Benutzers:', errorText);
            alert('Fehler beim Aktualisieren des Benutzers');
        }
    } catch (error) {
        console.error('Fehler beim Aktualisieren des Benutzers:', error);
        alert('Es ist ein Fehler aufgetreten. Bitte versuchen Sie es erneut.');
    }
},

  async deleteUser(oid) {
    const response = await fetch(`http://localhost:5078/Users/${oid}`, {
      method: 'DELETE',
    });
    if (response.ok) {
      this.fetchUsers();
    }
  },
  toBase64(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        const base64String = reader.result.split(',')[1];
        resolve(base64String);
      };
      reader.onerror = (error) => reject(error);
    });
  },
  logout() {
    this.userData = null;
    this.loggedIn = false;
    this.email = '';
    this.password = '';
    this.newPassword = '';
    this.isEditing = false;
  },
  async fetchMessages() {
  try {
    const response = await fetch('http://localhost:5078/Message');
    if (response.ok) {
      const messages = await response.json(); 
      this.messages = messages.map(message => ({
        // Sicherstellen, dass alle benötigten Felder korrekt zugewiesen werden
        oid: message.oid,
        name: message.name,  // Hier wird der Name des Absenders erwartet
        surname: message.surname,  // Optional, falls benötigt
        email: message.email,  // Optional, falls benötigt
        messageText: message.messageText,  // Hier ist der eigentliche Nachrichtentext
        timestamp: message.timestamp,
        isRead: message.isRead
      }));
    } else {
      console.error('Fehler beim Abrufen der Nachrichten:', await response.text());
    }
  } catch (error) {
    console.error('Fehler beim Abrufen der Nachrichten:', error);
  }
},
  
  async sendMessage() {
if (this.newMessage.trim() === "") return;

// Überprüfen, ob das Passwort vorhanden ist
if (!this.userData.password) {
  console.error("Das Passwortfeld ist erforderlich, aber es wurde nicht bereitgestellt.");
  return;
}

const message = {
  user: {
    Oid: this.userData.oid,
    name: this.userData.name,
    surname: this.userData.surname,
    email: this.userData.email,
    job: this.userData.job,
    gender: this.userData.gender,
    nationality: this.userData.nationality,
    password: this.userData.password,  // Passwort-Feld hinzugefügt
  },
  MessageText: this.newMessage,
};

try {
  const response = await fetch("http://localhost:5078/Message", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(message),
  });

  if (response.ok) {
    this.newMessage = ""; // Leert das Textfeld
    this.fetchMessages(); // Nachrichten erneut laden
  } else {
    const errorText = await response.json();
    console.error("Fehler beim Senden der Nachricht:", errorText);
  }
} catch (error) {
  console.error("Fehler beim Senden der Nachricht:", error);
}
},
async deleteMessage(oid) {
  try {
    const response = await fetch(`http://localhost:5078/Message/${oid}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        // Hier kannst du ggf. Authentifizierungs-Token oder andere Header hinzufügen
      },
    });

    if (response.ok) {
      // Nachricht erfolgreich gelöscht
      this.messages = this.messages.filter(message => message.id !== oid); // Nachrichten-Array korrekt aktualisieren
      console.log('Message deleted successfully');
      this.fetchMessages();
    } else {
      // Fehler bei der Anfrage
      const error = await response.text();
      console.error(`Failed to delete message: ${error}`);
    }
  } catch (error) {
    console.error('Error deleting message:', error);
    alert('An error occurred while deleting the message');
  }
},
  formatDate(date) {
    const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Date(date).toLocaleDateString(undefined, options);
  },
  formatDateNachricht(date) {
    const options = {hour: '2-digit', minute: '2-digit', hour12: false};
    return new Date(date).toLocaleDateString(undefined, options);
  },
  calculateAge(birthday) {
    const birthDate = new Date(birthday);
    const today = new Date();
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDiff = today.getMonth() - birthDate.getMonth();
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }
    return age;
  },
  isUserMessage(message) {
  return message.email === this.userData.email;
},
  redirectToUrlAdmin() {
    this.$router.push({ path: '/AdminSeite' });
  },
  redirectToUrlStartpage() {
    this.$router.push({ path: '/' });
  },
},
created() {
  this.fetchUsers();
},
};

</script>


<style scoped>
.no-scroll{
  overflow: hidden;
}

.adminviewbtn{
  margin-bottom: 5px;
  margin-left: 10px;
}

.startseitebtn{
  margin-bottom: 5px;
  margin-left: 10px;
}

.logintbn{
  margin-bottom: 5px;
}

.user-profile-card {
  margin-right: auto; 
  max-height: 980px;
}

.users-list-card {
  margin-left: auto; 
  height: auto;
}

.user-list-container {
  height: 100%;
}

.editfeld{
 margin-top: 150px;


}

.avatar-container {
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}

.avatar {
  border-radius: 5%;
  max-width: 350px;
  max-height: 350px;
  object-fit: cover;
}

.user-name {
  font-size: 1.5rem;
  font-weight: bold;
  text-align: center;
  margin-bottom: 10px;
  margin-top: 150px;
}

.user-info-name{
  font-size: 1rem; /* Vergrößert die Schriftgröße des Namens */
font-weight: bold; /* Macht den Namen fett */
color: #8cbcf0; /* Farbe des Namens (z.B. Blau) */
text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2); /* Schatten für besseren Kontrast */
}

.messagetext{
  font-size: 1rem;
  margin-top: -5px;
  margin-left: 15px;
}


.user-info-date{
  font-size: 0.75rem;
  margin-right: 5%;
  margin-top: 20px;
}

.user-info {
  font-size: 1rem;
  color: #f3f3f3;
  text-align: center;
  margin-bottom: 5px;
}

.editbtn{
  margin-bottom: 15px;

}
.logout{
  margin-left: 10px;
  margin-bottom: 15px;
}

.avatar{
  margin-left: 470px;
  margin-top: 30px;
  height: 160px;
  width: 120px;
}

.textname {
  margin-top: -180px;
  font-weight: bold;
  font-size: 1.25rem;
  margin-left: 60px;
}

.text{
  margin-top: -20;
  margin-left: 60px;
}

.card{
  height: 230px;
  margin-bottom: 10px;
}

.passwordtext{
  width: 100px;
  margin-left: 15px;
}

.info{
    margin-left: 10px;
    margin-right: 10px;
  }


.textchat {
display: flex;
flex-direction: column;
max-height: 600px;
overflow-y: hidden;
}

.message-from-user {
justify-content: flex-end;
}

.message-from-others {
justify-content: flex-start;
}

.nachricht {
max-width: 80%;
min-width: 20%;
width: max-content;
word-wrap: break-word; /* Bricht lange Wörter um */
overflow-wrap: break-word; /* Bricht lange Wörter um */
white-space: pre-wrap; /* Erhält Zeilenumbrüche in langen Nachrichten */
padding: 10px; 
overflow-x: auto; /* Horizontales Scrollen aktivieren, falls notwendig */
}

.message-from-user .nachricht {
background-color: #272626;
margin-left: auto; /* Nachrichten des Benutzers nach rechts verschieben */
}

.message-from-others .nachricht {
background-color: #6a6969;
margin-right: auto; /* Nachrichten anderer Benutzer nach links verschieben */
}


.sendbtn{
  width: 100px;
  height: 100%; /* Button füllt die Höhe des Containers */
  min-height: 56px; /* Mindesthöhe anpassen, falls erforderlich */
  white-space: nowrap; /* Verhindert das Umbruch von Button-Text */
}

.chat-form {
display: flex; /* Flexbox aktivieren */
align-items: stretch; /* Vertikal zentrieren */
margin-top: 20px; /* Abstand zu den Nachrichten oben */
margin-top: auto; /* Fixiert das Formular am unteren Rand */
  display: flex;
  align-items: center;
  padding: 10px;
}

.chat-textarea {
flex: 1; /* Textarea nimmt den verbleibenden Raum ein */
margin-right: 10px; /* Abstand zwischen Textarea und Button */
height: 56px; /* Höhe des Textfelds, anpassen falls nötig */

}


.chat-input-container {
display: flex;
align-items: center; /* Zentriert den Inhalt vertikal */
}

.chat-container {
  flex: 1; /* Nimmt den verfügbaren Raum ein */
  display: flex;
  flex-direction: column;
  overflow: hidden; /* Verhindert, dass der Container selbst scrollt */
}

.chat-messages {
  flex: 1;
  overflow-y: auto; /* Ermöglicht das Scrollen der Nachrichten */
}


.load-more-btn{
  margin-bottom: 25px;
  margin-top: -10px;
  width: 1000px;
}

.delete-button{
  margin-left: auto;
}
</style>
