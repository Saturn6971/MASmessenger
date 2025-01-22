<template>
    <head>
    <title>MasMessenger</title>
    </head>
    
    
    <body>
        <div class="container">
        <header>
            <v-img class="logo" src="../assets/logo.png" @click="redirectToUrlPG"></v-img>
            <h1 class="headline">MasMessenger</h1> 
        </header>
    <br>
    <br>
    <br>
        <h2>Willkommen bei MasMessenger</h2>
    <div class="about-us">
                <p>– der Messenger, der Kommunikation auf ein neues Level bringt!</p>
                <p>Unser Ziel ist es, Menschen auf der ganzen Welt zu verbinden und eine Plattform zu bieten, die sowohl intuitiv als auch sicher ist. Bei MasMessenger stehen Benutzerfreundlichkeit, Datenschutz und innovative Funktionen im Mittelpunkt.</p>
                <p>Wir glauben daran, dass Kommunikation mehr ist als nur das Versenden von Nachrichten. Es geht darum, echte Verbindungen zu schaffen, Ideen auszutauschen und gemeinsam zu wachsen. Mit MasMessenger geben wir dir die Werkzeuge an die Hand, um deine Gespräche lebendig, produktiv und stressfrei zu gestalten.</p>
            </div>
    <br>
    <br>
    <br>
    <br>       
            <h1 class="team">Unser Team</h1>
            <br>
            <br>
            <div class="team-member">
                <v-img class="team-img" src="../assets/Image_Teammember.png" ></v-img>
                <p>Maximilian Gmeinder -- </p>
                <a href="https://www.instagram.com/max.gmeinder/" target="_blank">
                    <button class="kontakt" type="button">Kontakt</button>
                  </a>
            </div>
    <br>
    <br>
    <br>
    <br>
    <br>
            <div class="team-member">
                <img class="team-img" src="../assets/Image_Teammember.png" alt="Teammitglied">
                <p>Ali Dadak -- </p>
                <a href="" target="_blank">
                    <button class="kontakt" type="button">Kontakt</button>
                  </a>
            </div>
    <br>
    <br>
    <br>
    <br>
    <br>
         <div class="team-member">
                <v-img class="team-img" src="../assets/Image_Teammember.png" ></v-img>
                <p>Matej Grkovic -- </p>
                <a href="https://www.instagram.com/matejgvc/" target="_blank">
                    <button class="kontakt" type="button">Kontakt</button>
                  </a>
            </div>

        </div>
    </body>
    </template>
    
    <script>
    export default {
    methods: {
            redirectToUrlPG() {
            this.$router.push({path: "/personalinformation"}); // Assuming '/Cool' is the correct route
            },
        },
    };
    </script>
    
    
    <style scoped>
    body, html {
        height: 100%;
        margin: 0;
        font-family: Arial, sans-serif;
        background-color: #000;
        color: white;
    }
    
    .container {
        padding: 20px;
    }
    
    header {
        display: flex; /* Deklariert den Header als Flex-Container */
        align-items: center; /* Zentriert die Flex-Items vertikal */
        justify-content: space-between; /* Verteilt den vorhandenen Platz zwischen den Flex-Items */
    }
    
    .headline {
        color: #bb45ff;
        text-align: center;
        font-family: 'Arial', sans-serif;
        font-size: 48px;
        text-shadow: 2px 2px 4px rgba(0,0,0,0.5);
        display: block;
        margin: 0 auto;
        padding-bottom: 5px;
    }
    
    .logo {
        position: absolute;
        width:50px;
        border-radius: 5px;
        height:50px;
        left:2%;
        right:0%;
        
    }
    
    
    h2 {
        color: white;
        font-size: bold;
        display: flex;
        justify-content: center;
        text-align: center;
    }
    .about-us{
        text-align: center;
        color: white;
        font-family: Arial, sans-serif;
        max-width: 800px; /* passt die maximale Breite des Textes an */
        margin: 0 auto; /* zentriert den Textblock */
        padding: 20px;
        line-height: 1.6; /* verbessert die Lesbarkeit */
    }
    /* Stil für den 'Über uns'-Button */
    .homepage { /* Fügt Padding hinzu, damit der Button größer erscheint */
        padding: 10px; /* Fügt Padding hinzu, damit der Button größer erscheint */
        font-size: 1em; /* Setzt die Schriftgröße entsprechend der Schriftgröße des Headers */
        cursor: pointer; /* Ändert den Cursor, wenn der Benutzer über den Button fährt */
        border: none; /* Entfernt die Standard-Rand */
        background-color: #706e6e; /* Setzt eine Hintergrundfarbe für den Button */
        color: #fff; /* Setzt die Textfarbe auf Weiß */
        border-radius: 5px; /* Fügt abgerundete Ecken hinzu */
        width: fit-content;
    }
    
    .homepage:hover {
        background-color: #434343;
    }
    
    
    
    h1:hover {
      cursor: pointer;
    }
    
    .team{
        text-align: center;
        color: white;
        font-size: 40px;
    }
    
    
    .team-member {
        text-align: center;
        margin-bottom: 20px;
        color: white;
    }
    
    .team-img {
        display: block;
        margin: 0 auto;
        width: 200px; /* oder eine andere Grße */
        height: auto;
        border-radius: 50%; /* Fr runde Bilder */
        align-items: center;
    }
    
    .team-member p {
        margin-top: 10px;
    }
    
    .kontakt { /* Fügt Padding hinzu, damit der Button größer erscheint */
        padding: 10px; /* Fügt Padding hinzu, damit der Button größer erscheint */
        font-size: 1em; /* Setzt die Schriftgröße entsprechend der Schriftgröße des Headers */
        cursor: pointer; /* Ändert den Cursor, wenn der Benutzer über den Button fährt */
        border: none; /* Entfernt die Standard-Rand */
        background-color: #706e6e; /* Setzt eine Hintergrundfarbe für den Button */
        color: #fff; /* Setzt die Textfarbe auf Weiß */
        border-radius: 5px; /* Fügt abgerundete Ecken hinzu */
        width: fit-content;
    }
    </style>