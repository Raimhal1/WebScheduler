<template>
  <div class="register__page">
    <form @submit="this.$router.push('/login')" method="post" class="user__form">
      <h2>Register</h2>
      <my-input
          v-model="user.FirstName"
          type="text"
          placeholder="Firstname"
          v-focus
          required
      >
      </my-input>
      <my-input
          v-model="user.LastName"
          type="text"
          placeholder="Lastname"
          required
      >
      </my-input>
      <my-input
          v-model="user.UserName"
          type="text"
          placeholder="Username"
          required
      >
      </my-input>
      <my-input
          v-model="user.Email"
          type="email"
          placeholder="Email"
          required
      >
      </my-input>
      <my-input
          v-model="user.Password"
          type="password"
          placeholder="Password"
          required
      >
      </my-input>
      <my-button
          type="submit"
          @click="register"
      >
        Register
      </my-button>
    </form>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
  name: "RegisterPage",

  methods: {
    ...mapActions({
      register: 'user/register'
    }),
  },
  computed: {
    ...mapState({
      user: state => state.user.user
    }),
  },
}
</script>

<style scoped>
.register__page{
  display: flex;
  justify-content: center;
}
</style>