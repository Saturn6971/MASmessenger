<template>
  <button @click="signOut" class="sign-out-btn">
    Sign Out
  </button>
</template>

<script>
export default {
  methods: {
    signOut() {
      this.$emit('sign-out'); // Event auslösen
    }
  }
};
</script>

<style scoped>
.sign-out-btn {
  background: linear-gradient(135deg, #ff6b6b, #ff4757);
  border: none;
  color: white;
  padding: 10px 20px;
  border-radius: 8px;
  cursor: pointer;
  transition: background 0.3s ease;
}

.sign-out-btn:hover {
  background: linear-gradient(135deg, #ff4757, #ff6b6b);
}
</style>