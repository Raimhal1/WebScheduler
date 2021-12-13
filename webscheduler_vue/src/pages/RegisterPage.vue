<template>
  <div class="user__page">
    <user-form>
      <template v-slot:header>
        Register
      </template>
      <template v-slot:password>
        Password
      </template>
      <template v-slot:submit__name>
        Register
      </template>
      <template v-slot:link>
        <p>
          I already have an account :
          <router-link to="/login" class="link">Log in</router-link>
        </p>
      </template>
    </user-form>
  </div>
</template>

<script>
import {mapMutations} from "vuex";
import UserForm from "@/components/UserForm";

export default {
  name: "RegisterPage",
  components: {UserForm},
  mounted() {
    this.clearUser()
  },
  beforeUnmount() {
    this.clearErrors()
  },
  methods: {
    ...mapMutations({
      clearUser: 'user/clearUser',
      clearErrors: 'clearErrors'
    }),
  },
}
</script>

<style scoped>

</style>