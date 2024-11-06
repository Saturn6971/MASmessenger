<template>
  <v-app>
    <v-container>
      <v-row v-if="!authenticated">
        <v-col cols="12">
          <v-card class="pa-4 mb-4">
            <v-card-title>Admin Login</v-card-title>
            <v-form @submit.prevent="checkCode">
              <v-text-field v-model="adminEmail" label="Enter Admin Email" type="email" variant="solo-filled" required></v-text-field>
              <v-text-field v-model="adminPassword" label="Enter Admin Password" type="password" variant="solo-filled" required></v-text-field>
              <v-row class="button-row-user">
                <v-btn type="submit" color="primary" class="mt-4" variant="tonal">Login</v-btn>
                <v-btn type="button" color="purple" class="mt-4" variant="tonal" @click="redirectToUrlStartpage()">Startpage</v-btn>
              </v-row>
            </v-form>
          </v-card>
        </v-col>
      </v-row>

      <v-row v-else>
        <!-- Add User Section -->
        <v-col cols="12">
          <v-card class="pa-4 mb-4">
            <v-card-title> User Management: {{ isEditing ? 'Update User' : 'Add User' }}</v-card-title>
            <v-form @submit.prevent="isEditing ? updateUser() : addUser()">
              <v-text-field v-model="name" label="Name" variant="solo-filled" required></v-text-field>
              <v-text-field v-model="surname" label="Surname" variant="solo-filled"></v-text-field>
              <v-text-field v-model="email" label="Email" variant="solo-filled" required></v-text-field>
              <v-text-field v-model="birthday" label="Birthday" type="date" variant="solo-filled"></v-text-field>
              <v-text-field v-model="job" label="Job" variant="solo-filled"></v-text-field>
              <v-autocomplete v-model="gender" label="Gender" :items="['Male', 'Women']" variant="solo-filled"></v-autocomplete>
              <v-text-field v-model="nationality" label="Nationality" variant="solo-filled"></v-text-field>
              <v-autocomplete v-model="role" label="Role" :items="['User', 'Admin']" variant="solo-filled"></v-autocomplete>
              <v-file-input v-model="imageFile" label="Profile Image" variant="solo-filled"></v-file-input>
              <v-text-field v-model="password" label="Password" type="password" variant="solo-filled" required></v-text-field>
              <v-row class="button-row-user">
              <v-btn type="submit" :color="isEditing ? 'orange' : 'primary'" class="mt-4" variant="tonal"> {{ isEditing ? 'Update User' : 'Add User' }}</v-btn>
              <v-btn type="button" color="error" class="mt-4" variant="tonal" @click="clearForm">Cancel</v-btn>
              <v-btn type="button" color="green" class="mt-4" variant="tonal" @click="redirectToUrlLogin()">LogIn</v-btn>
              <v-btn type="button" color="purple" class="mt-4" variant="tonal" @click="redirectToUrlStartpage()">Startpage</v-btn>
            </v-row>
            </v-form>
          </v-card>
        </v-col>

        <!-- Display Users -->
        <v-col cols="12">
          <v-card class="pa-4 mb-4">
            <v-card-title>
            <v-btn @click="toggleUserList" color="primary" class="userbtn" variant="tonal">
            {{ showUsers ? 'Hide Users' : 'Show Users' }}
          </v-btn>
        </v-card-title>
            <v-container v-if="showUsers">
              <v-row>
                <v-col v-for="(user, index) in users" :key="user.oid" cols="12" md="6" lg="4">
                  <v-card :class="{ card: true, expanded: detailsVisible.has(index) }" elevation="16">
                    <v-container class="card-content">
                      <v-col class="text-content">
                        <v-card-subtitle class="textname">Name: {{ user.name }}</v-card-subtitle>
                        <v-card-subtitle class="text">Surname: {{ user.surname }}</v-card-subtitle>
                        <v-card-subtitle class="text">Email: {{ user.email }}</v-card-subtitle>
                        <v-card-subtitle class="text">Birthday: {{ formatDate(user.birthday) }}</v-card-subtitle>
                        <v-card-subtitle class="text">Age: {{ calculateAge(user.birthday) }} years</v-card-subtitle>
                        <v-card-subtitle v-if="detailsVisible.has(index)" class="text">Gender: {{ user.gender }}</v-card-subtitle>
                        <v-card-subtitle v-if="detailsVisible.has(index)" class="text">Job: {{ user.job }}</v-card-subtitle>
                        <v-card-subtitle v-if="detailsVisible.has(index)" class="text">Nationality: {{ user.nationality }}</v-card-subtitle>
                        <v-card-subtitle v-if="detailsVisible.has(index)" class="text">Role: {{ user.role }}</v-card-subtitle>
                        <v-row class="button-row">
                        <v-btn @click="deleteUser(user.oid)" class="deleteuserbtn" color="error" variant="tonal">Delete</v-btn>
                    <v-btn @click="editUser(user)" class="editbtn" color="orange" variant="tonal">Edit</v-btn>
                    <v-btn @click="toggleDetails(index)" class="showmorebtn" color="blue" variant="tonal">
                      {{ detailsVisible.has(index) ? 'Less' : 'More' }}
                    </v-btn>
                    </v-row>
                      </v-col>
                      <v-col class="avatar-container">
                        <v-img :src="'data:image/jpeg;base64,' + user.image" class="avatar" v-if="user.image"></v-img>
                      </v-col>
                    </v-container>
                  </v-card>
                </v-col>
              </v-row>
            </v-container>
          </v-card>
        </v-col>
      
      <v-card>
        <v-row>
            <v-col>
              <v-radio-group inline v-model="view" class="switchbtn" row>
                <v-radio label="Table" value="table"></v-radio>
                <v-radio label="Map" value="map"></v-radio>
                <v-radio label="Chat" value="chat"></v-radio>
              </v-radio-group>
            </v-col>
          </v-row>
        <!-- Display User Login Logs -->
        <v-row v-if="authenticated && view === 'table'" class="tabellelogin">
          <v-col cols="12">
            <v-card class="pa-4 mb-4">
              <v-card-title>User Login Logs</v-card-title>
              <v-data-table-virtual
                :headers="headers"
                :items="loginLogs"
                item-key="oid"
                class="logintabelle"
              >
                <template v-slot:item.loginTime="{ item }">
                  {{ formatDateTime(item.loginTime) }}
                </template>
                <template v-slot:item.name="{ item }">
                  {{ item.name || 'N/A' }}
                </template>
                <template v-slot:item.email="{ item }">
                  {{ item.email || 'N/A' }}
                </template>
                <template v-slot:item.surname="{ item }">
                  {{ item.surname || 'N/A' }}
                </template>
                <template v-slot:item.latitude="{ item }">
                  {{ item.latitude !== null ? item.latitude : 'N/A' }}
                </template>
                <template v-slot:item.longitude="{ item }">
                  {{ item.longitude !== null ? item.longitude : 'N/A' }}
                </template>
              </v-data-table-virtual>
            </v-card>
          </v-col>
        </v-row>

        <!-- Add Map Section -->
        <v-card class="mapcard">
          <v-row v-if="authenticated && view === 'map'">
            <v-col cols="12">
              <v-card class="pa-4">
                <v-card-title>User Login Locations</v-card-title>
                <div id="map" style="height: 400px;"></div>
              </v-card>
            </v-col>
          </v-row>
        </v-card>

      
          <!-- Chat Section, nur sichtbar wenn der Benutzer angemeldet ist -->
          <v-row v-if="authenticated && view === 'chat'">
  <v-col cols="12">
  <v-card class="textchat mt-4">
    <v-card-title class="mt-4">Chat</v-card-title>
    <v-container class="chat-container">
      <!-- Nachrichtenanzeige -->
      <v-btn @click="fetchMessages()" color="primary" class="load-more-btn" variant="tonal">
        Load previous Nachrichten
      </v-btn>
      <v-row class="chat-messages" >
        <v-col cols="12" v-for="message in messages" :key="message.id">
      <!--  <div :class="{'message-from-user': isUserMessage(message), 'message-from-others': !isUserMessage(message)}"> -->  
          <v-card class="nachricht pa-2 mb-2">
            <v-card-title class="user-info-name">{{ message.name }}</v-card-title>
            <v-card-subtitle class="messagetext">{{ message.messageText }}</v-card-subtitle>
            <v-card-subtitle class="user-info-date">{{ formatDateNachricht(message.timestamp) }}</v-card-subtitle>
              <!-- Löschen-Button -->
          <v-btn @click="deleteMessage(message.oid)" icon class="deletebtn">
                <v-icon >mdi-delete</v-icon>
              </v-btn>
          </v-card>
   <!--     </div> -->  
        </v-col>
      </v-row>
</v-container>
<v-form @submit.prevent="sendMessage" class="chat-form">
    <v-textarea v-model="newMessage" label="Nachricht" rows="1" required class="chat-textarea" variant="solo-filled"></v-textarea>
    <v-btn type="submit" color="primary" class="sendbtn mt-2" variant="tonal">Send</v-btn>
  </v-form>
</v-card>
  </v-col>
          </v-row>
        </v-card>
      </v-row>
    </v-container>
  </v-app>
</template>


<script>
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';

export default {
data() {
  return {
    users: [],
    name: '',
    surname: '',
    email: '',
    birthday: '',
    password: '',
    job: '',
    gender: '',
    nationality: '',
    adminEmail:'',
    adminPassword:'',
    imageFile: null,
    isEditing: false,
    authenticated: false,
    detailsVisible: new Set(),
    showUsers: false,
    loginLogs: [], 
    loginLocations: [], 
    headers: [
      { text: 'Name', value: 'name', title:'Name', align: 'start'},
      { text: 'Surname', value: 'surname', title:'Surname' },
      { text: 'Email', value: 'email', title:'Email' },
      { text: 'Latitude', value: 'latitude', title:'Latitude (Breitengrad)' },
      { text: 'Longitude', value: 'longitude', title:'Longitude (Längengrad)'},
      { text: 'Login Time', value: 'loginTime', title:'Uhrzeit' }
    ],
    map: null,
    role:'',
    view: 'table', // Default view is 'table',
    mapInitialized: false,
    messages: [],
    newMessage: ''
  };
},
methods: {
  async fetchUsers() {
    const response = await fetch('http://localhost:5078/Users');
    const data = await response.json();
    this.users = data.map(user => ({
      ...user,
      showDetails: false
    }));
  },

  async fetchLoginLogs() {
    const response = await fetch('http://localhost:5078/Users/loginlogs');
    const data = await response.json();
    this.loginLogs = data;
    this.loginLocations = data
      .filter(log => log.latitude && log.longitude)
      .map(log => ({
        lat: log.latitude,
        lng: log.longitude,
        email: log.email,
        logintime: log.loginTime
      }));
      console.log("Login Locations: ", this.loginLocations); 

      if (this.view === 'map') {
        this.addMarkers();
      }
  },

  async addUser() {
    let imageBase64 = '';
    if (this.imageFile) {
      try {
        imageBase64 = await this.toBase64(this.imageFile);
      } catch (error) {
        console.error('Error converting image to base64', error);
      }
    }
    const response = await fetch('http://localhost:5078/Users', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        name: this.name,
        surname: this.surname,
        email: this.email,
        birthday: this.birthday,
        job: this.job,
        gender: this.gender,
        nationality: this.nationality,
        role: this.role,
        image: imageBase64,
        password: this.password
      }),
    });
    if (response.ok) {
      this.clearForm();
      this.fetchUsers();
    }
  },

  async updateUser() {
  let imageBase64 = this.currentUser.image; 
  if (this.imageFile) {
    try {
      imageBase64 = await this.toBase64(this.imageFile);
    } catch (error) {
      console.error('Error converting image to base64', error);
    }
  }

  const response = await fetch(`http://localhost:5078/Users/${this.currentUser.oid}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      ...this.currentUser,
      name: this.name,
      surname: this.surname,
      email: this.email,
      birthday: this.birthday,
      job: this.job,
      gender: this.gender,
      nationality: this.nationality,
      role: this.role,
      image: imageBase64, 
      password: this.password
    }),
  });

  if (response.ok) {
    this.clearForm();
    this.fetchUsers();
    this.fetchLoginLogs();
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

  editUser(user) {
  this.name = user.name;
  this.surname = user.surname;
  this.email = user.email;
  this.birthday = user.birthday;
  this.job = user.job;
  this.gender = user.gender;
  this.nationality = user.nationality;
  this.role = user.role;
  this.imageFile = null; 
  this.password = ''; 
  this.isEditing = true;
  this.currentUser = { ...user }; 
},

  clearForm() {
    this.name = '';
    this.surname = '';
    this.email = '';
    this.birthday = '';
    this.job = '';
    this.gender = '';
    this.nationality = '';
    this.role = '';
    this.imageFile = null;
    this.password = '';
    this.isEditing = false;
    this.currentUser = null;
  },

  toggleDetails(index) {
    if (this.detailsVisible.has(index)) {
      this.detailsVisible.delete(index);
    } else {
      this.detailsVisible.add(index);
    }
  },
  toggleUserList() {
      this.showUsers = !this.showUsers;
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

    formatDate(date) {
      const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
      return new Date(date).toLocaleDateString(undefined, options);
    },

    formatDateTime(date) {
      const options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', hour12: false};
      return new Date(date).toLocaleDateString(undefined, options);
    },

    toBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result.split(',')[1]);
    reader.onerror = reject;
    reader.readAsDataURL(file);
  });
},

async checkCode() {
      try {
        const response = await fetch('http://localhost:5078/Users/adminLogin', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            email: this.adminEmail,
            password: this.adminPassword,
          }),
        });
        
        if (response.ok) {
          const data = await response.json();
          if (data.role === 'Admin') {
            this.authenticated = true;
            this.fetchUsers();
            this.fetchLoginLogs();
          } else {
            alert('Only Admins are allowed to log in.');
          }
        }
      } catch (error) {
        console.error('Error during authentication:', error);
        alert('An error occurred during authentication. Please try again later.');
      }
    },

  redirectToUrlStartpage() {
    this.$router.push({ path: "/" }); 
  },

  redirectToUrlLogin() {
    this.$router.push({ path: "/StartSeite" }); 
  },
  formatDateNachricht(date) {
    const options = {hour: '2-digit', minute: '2-digit', hour12: false};
    return new Date(date).toLocaleDateString(undefined, options);
  },

  initializeMap() {
      if (this.view === 'map') {
        this.$nextTick(() => {
          if (!this.map) {
            // Erstelle die Karte nur, wenn sie noch nicht existiert
            this.map = L.map('map').setView([47.4167, 9.7500], 13); 
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
              maxZoom: 19,
            }).addTo(this.map);
          }
          this.addMarkers(); // Marker nach der Initialisierung hinzufügen
        });
      }
    },
    addMarkers() {
      if (this.map && this.loginLogs.length > 0) {
        this.loginLogs.forEach(log => {
          if (log.latitude && log.longitude) {
            L.marker([log.latitude, log.longitude]).addTo(this.map)
              .bindPopup(`<b>${log.name || 'N/A'}</b><br>${log.email || 'N/A'}`);
          }
        });
      }
    },
    destroyMap() {
      if (this.map) {
        this.map.remove(); // Entferne die Karte korrekt, wenn auf die Tabellenansicht gewechselt wird
        this.map = null; // Setze die Karte auf null, um sie bei erneutem Aufruf von `initializeMap` neu zu erstellen
      }
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
},

watch: {
    view(newView) {
      if (newView === 'map') {
        this.initializeMap(); // Karte initialisieren, wenn auf die Kartenansicht gewechselt wird
      } else {
        this.destroyMap(); // Karte zerstören, wenn auf die Tabellenansicht gewechselt wird
      }
    },
    loginLogs() {
      if (this.view === 'map') {
        this.addMarkers(); // Marker erneut hinzufügen, wenn sich die loginLogs ändern
      }
    }
  },
  mounted() {
    if (this.view === 'map') {
      this.initializeMap(); // Karte initialisieren, wenn die Seite geladen wird und die Kartenansicht aktiv ist
    }
  }
};
</script>


<style scoped>

.button-row-user{
  gap: 10px;
  margin-bottom: 5px;
  margin-left: 10px;
}
.card {
  display: flex;
  align-items: center; 
  padding: 16px; 
  margin-bottom: 20px; 
  height: 230px; 
  box-sizing: border-box; 
  transition: height 0.3s ease; 
}


.card.expanded {
  height: 400px; 
}


.card-content {
  display: flex;
  align-items: center; 
  width: 100%; 
}


.text-content {
  display: flex;
  flex-direction: column; 
  justify-content: center; 
  flex: 1; 
  margin-right: 20px; 
}


.avatar {
  object-fit: cover;
  width: 150px; 
  height: 150px; 
  border-radius: 15%;
}


.avatar-container {
  display: flex;
  align-items: center; 
  justify-content: center; 
}


.textname {
  margin-bottom: 10px; 
  font-weight: bold;
  font-size: 1.25rem;
}


.text {
  margin-bottom: 5px; 
}


.button-row {
  display: flex;
  justify-content: flex-start; 
  margin-top: 10px; 
  gap: 10px;
}

.deleteuserbtn{
  width: 70px;
}

.editbtn{
  width: 70px;
}

.showmorebtn{
  width: 70px;
}

.logintabelle{
  max-height: 500px;
  overflow-y: auto;
  margin-bottom: 20px;
}

.text-content {
  flex: 1;
}


.mapcard {
  width: 5000px;
}


.userbtn{
  width: 5000px
}


.switchbtn{
  width: 600px;
  margin-left: 20px;
  margin-top: 20px;

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
  width: 5000px;
}


.deletebtn{
  margin-left: 80%;
  margin-top: -100px;
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
</style>
