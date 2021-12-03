<template>
  <div class="register__page">
    <form @submit.prevent="register" method="post" class="user__form">
      <h2>Register</h2>
      <my-input
          v-model="user.FirstName"
          type="text"
          placeholder="Firstname"
          v-focus
      >
      </my-input>
      <my-input
          v-model="user.LastName"
          type="text"
          placeholder="Lastname"
      >
      </my-input>
      <my-input
          v-model="user.UserName"
          type="text"
          placeholder="Username"
      >
      </my-input>
      <my-input
          v-model="user.Email"
          type="email"
          placeholder="Email"
      >
      </my-input>
      <my-input
          v-model="user.Password"
          type="password"
          placeholder="Password"
      >
      </my-input>
      <my-button
          type="submit"
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
  beforeUnmount() {
  },
  methods: {
    ...mapActions({
      register: 'user/register'
    }),
  },
  computed: {
    ...mapState({
      user: state => state.user.user,
      invalid: state => state.user.invalid
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