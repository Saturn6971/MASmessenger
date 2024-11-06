<template>
    <v-app>
    <v-container>
      <v-row>
        <!-- Add User Section -->
        <v-col cols="12">
          <v-card class="pa-4 mb-4">
            <v-card-title class="title">UserManagment </v-card-title>
            <v-card-subtitle class="subtitle">Welcome to your personal UserManager!</v-card-subtitle>
              <v-btn type="button" color="primary" class="mt-4" variant="tonal" @click="redirectToUrlAdmin()">Admin View</v-btn>
              <v-btn type="button" color="green" class="mt-4" variant="tonal" @click="redirectToUrlLogin()">LogIn</v-btn>
          </v-card>
        </v-col>

        <v-col cols="12">
          <v-card>
          <v-text-field
            v-model="searchQuery"
            label="Search Users"
            prepend-icon="mdi-magnify"
            clearable
            class="searchbar"
          ></v-text-field>
        </v-card>
        </v-col>

          <!-- Display Users -->
          <v-col cols="12">
            <v-card class="pa-4">
              <v-card-title>
                Current Users
              </v-card-title>
              <v-container>
                <v-row>
                  <v-col v-for="(user, index) in filteredUsers" :key="user.oid" cols="12" md="6" lg="4">
                    <v-card :class="{ card: true, expanded: detailsVisible.has(index) }" elevation="16">
                      <v-img :src="'data:image/jpeg;base64,' + user.image" height="200px" cover v-if="user.image" class="avatar"></v-img>
                      <v-card-title class="textname">
                        Name: {{ user.name }}
                      </v-card-title>
                      <v-card-subtitle class="text">
                        Surname: {{ user.surname }}
                      </v-card-subtitle>
                      <v-card-subtitle class="text">
                        Email: {{ user.email }}
                      </v-card-subtitle>
                      <v-card-subtitle class="text">
                        Birthday: {{ formatDate(user.birthday) }}
                      </v-card-subtitle>
                      <v-card-subtitle class="text">
                        Age: {{ calculateAge(user.birthday) }} years
                      </v-card-subtitle>
  
                      <!-- Toggleable Details Section -->
                      <v-card-subtitle v-if="detailsVisible.has(index)" class="text">
                        Gender: {{ user.gender }}
                      </v-card-subtitle>
                      <v-card-subtitle v-if="detailsVisible.has(index)" class="text">
                        Job: {{ user.job }}
                      </v-card-subtitle>
                      <v-card-subtitle v-if="detailsVisible.has(index)" class="text">
                        Nationality: {{ user.nationality }}
                      </v-card-subtitle>
                      <v-card-subtitle v-if="detailsVisible.has(index)" class="text">
                        Role: {{ user.role }}
                      </v-card-subtitle>
                      <v-btn @click="toggleDetails(index)" class="showmorebtn" color="blue" variant="tonal">
                        {{ detailsVisible.has(index) ? 'Less' : 'More' }}
                      </v-btn>
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
      users: [],
        name: '',
        surname: '',
        email: '',
        birthday: '',
        password: '',
        job: '',
        gender: '',
        nationality: '',
        imageFile: null,
        isEditing: false,
        adminCode: '',
        authenticated: false,
        detailsVisible: new Set(),
        searchQuery: '',
        role: '',
      };
  },
  computed: {
    filteredUsers() {
      return this.users.filter(user => {
        const query = this.searchQuery.toLowerCase();
        return (
          user.name.toLowerCase().includes(query) ||
          user.surname.toLowerCase().includes(query) ||
          user.email.toLowerCase().includes(query) ||
          user.job.toLowerCase().includes(query) ||
          user.gender.toLowerCase().includes(query) ||
          user.nationality.toLowerCase().includes(query)
        );
      });
    },
  },
  methods: {
    async fetchUsers() {
      const response = await fetch('http://localhost:5078/Users');
      const data = await response.json();
      this.users = data;
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

  redirectToUrlLogin() {
      this.$router.push({path: "/StartSeite"}); 
    },
  redirectToUrlAdmin() {
      this.$router.push({path: "/AdminSeite"}); 
    },
    toggleDetails(index) {
    if (this.detailsVisible.has(index)) {
      this.detailsVisible.delete(index); 
    } else {
      this.detailsVisible.add(index); 
    }
  },
  },

  created() {
    this.fetchUsers();
  }
};
</script>

<style scoped>
.title{
  margin-left: 10px;
}

.subtitle{
  margin-left: 10px;
}

.card {
  height: 230px;
}

.v-btn {
  width: 100%;
}
.user-row {
  display: flex;
  align-items: center;
}
.avatar {
  width: 180px;
  height: 110px;
  margin-left: 350px;
  border-radius: 15%;
  margin-top: 10px;
}
.textname {
  margin-top: -200px;
  font-weight: bold;
  font-size: 1.25rem;
  margin-left: 20px;
  margin-top: -170px;
}

.text{
  margin-left: 20px;
}


.showmorebtn {
    width: 70px;
    bottom: -10px;
    left: 30px;
  }


.card.expanded {
  height: 300px; /* Erhöhte Höhe bei ausgeklappter Ansicht */
}

.searchbar{
justify-content: center;
width:auto;
margin-left: 30px;
margin-right: 30px;
margin-top: 10px;
margin-bottom: -10px;
}

</style>

