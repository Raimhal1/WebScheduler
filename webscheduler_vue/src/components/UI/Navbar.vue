<template>
  <div class="navbar">
    <div class="logo" @click="$router.push('/')">Web Scheduler</div>
    <div class="navbar__btns">
      <router-link to="/events"><my-button>Events</my-button></router-link>
      <router-link to="/my/events"><my-button>MyEvents</my-button></router-link>
      <router-link to="/about"><my-button>About</my-button></router-link>
      <router-link to="/login"><my-button>Login</my-button></router-link>
      <router-link to="/register"><my-button>Register</my-button></router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "Navbar",
}
</script>

<style scoped>
.navbar{
  height: 50px;
  background-color: rgba(175, 175, 175, 0.42);
  box-shadow: 2px 2px 4px rgba(168, 168, 168, 0.76);
  display: flex;
  align-items: center;
  padding: 0 15px;
}

.navbar__btns{
  margin-left: auto;
  display: flex;
  gap: 10px;
}

.logo{
  color: #0c20a1;
  font-size: 20px;
  font-weight: 600;
}
</style>