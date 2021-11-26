<template>
  <form>
    <div class="login__page">
      <form @submit="this.$router.push('/')" method="post" class="user__form" @submit.prevent>
        <h2>Log in</h2>
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
            @click="login"
        >
          Log in
        </my-button>
      </form>
    </div>
  </form>
</template>

<script>
import {mapActions, mapGetters, mapState} from "vuex";

export default {
  name: "LoginPage",
  computed: {
    ...mapState({
      user: state => state.user.user
    }),
    ...mapGetters({

    })
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
</style>