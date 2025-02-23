import { initializeApp } from 'firebase/app';
import { getAuth, GoogleAuthProvider, signInWithPopup, signOut } from 'firebase/auth';
import { getFirestore, collection, query, orderBy, limit, addDoc, serverTimestamp } from 'firebase/firestore';

const firebaseConfig = {
  apiKey: "AIzaSyBghnuMMbBgTcdlONhWjVk1SHhIqtCvGfA",
  authDomain: "masmessenger-7dd69.firebaseapp.com",
  projectId: "masmessenger-7dd69",
  storageBucket: "masmessenger-7dd69.firebasestorage.app",
  messagingSenderId: "252011870032",
  appId: "1:252011870032:web:5636ddb7cec9629bd39e55",
  measurementId: "G-07X91Q5W0P"
};

const app = initializeApp(firebaseConfig);
const auth = getAuth(app);
const firestore = getFirestore(app);

export { auth, firestore, GoogleAuthProvider, signInWithPopup, signOut, collection, query, orderBy, limit, addDoc, serverTimestamp };