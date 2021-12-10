<template>
  <div class="navbar">
    <div class="logo" @click="$router.push('/')">Web Scheduler</div>
    <div class="navbar__btns">
      <div v-if="!isAuth">
        <router-link to="/login"><my-button>Log in</my-button></router-link>
        <router-link to="/register"><my-button>Register</my-button></router-link>
      </div>
      <div v-else>
        <router-link to="/events"><my-button>Invited events</my-button></router-link>
        <router-link to="/my/events"><my-button>Your events</my-button></router-link>
        <router-link to="/account"><my-button>Account</my-button></router-link>
        <router-link to="/">
          <my-button
            @click="this.$store.dispatch('logout')"
          >
            Log out
          </my-button>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Navbar",
  computed: {
    isAuth(){
      return JSON.parse(this.$store.state.isAuth)
    }
  }
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
}
.navbar__btns, .navbar__btns>div{
  display: flex;
  gap: 10px;
}

.logo{
  color: #0c20a1;
  font-size: 20px;
  font-weight: 600;
  cursor: pointer;
}
</style>