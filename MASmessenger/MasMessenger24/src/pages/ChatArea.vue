<template>
<v-app>
  <v-card>
    <v-layout>
      <v-navigation-drawer
        expand-on-hover
        rail
        class="Drawer"
      >
        <v-list>
          <v-list-item
            prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg"
            subtitle="sandra_a88@gmailcom"
            title="Sandra Adams"
            
          ></v-list-item>
         
            <v-list-item class="btns">
                <v-btn>
                    Home
                </v-btn>
             
            </v-list-item>
      
          
        </v-list>
        <v-list-item  title="My Chat" value="myfiles"></v-list-item>
        <v-divider></v-divider>

        <v-list density="compact" nav>
          <v-list-item prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg" title="Matej" value="myfiles"></v-list-item>
          <v-list-item prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg" title="Ali" value="myfiles"></v-list-item>
          <v-list-item prepend-avatar="https://randomuser.me/api/portraits/women/85.jpg" title="Max" value="myfiles"></v-list-item>
        </v-list>


        <v-btn>Abmelden</v-btn>
     </v-navigation-drawer>

      <v-main style="height: 2000px"></v-main>
    </v-layout>
  </v-card>

</v-app>

</template>

<style scoped>
.drawer{

    max-width: 2000px;
    min-width: 200px;
}


</style>