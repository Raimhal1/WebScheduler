
<template>

  <form>
    <div class="login__page">
      <form method="post" class="user__form" @submit.prevent>
        <h2 class="title">Log in</h2>
        <my-input
            v-model="user.email"
            type="email"
            placeholder="Email"
        >
        </my-input>
        <my-input
            v-model="user.password"
            type="password"
            placeholder="Password"
        >
        </my-input>
        <my-button
            @click="login"
        >
          Log in
        </my-button>
      </form>
    </div>
  </form>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
  name: "LoginPage",
  beforeUnmount() {
    console.log('unmounted')
    if(this.isAuth) {
      localStorage.accessToken = this.accessToken
      localStorage.refreshToken = this.refreshToken
      localStorage.isAuth = this.isAuth
      localStorage.isAdmin = this.isAdmin
    }
  },
  computed: {
    ...mapState({
      accessToken: state => state.accessToken,
      refreshToken: state => state.refreshToken,
      isAuth: state => state.isAuth,
      isAdmin: state => state.isAdmin,
      user: state => state.user.user,
      errors: state => state.errors
    }),
  },
  methods: {
    ...mapActions({
      login: 'user/login'
    }),
  },
}

</script>

<style scoped>
.login__page{
  display: flex;
  justify-content: center;
}
.title{
  margin-bottom: 20px;
}
</style>