<template>
  <div class="user__page">
    <user-form>
      <template v-slot:header>
        Register
      </template>
      <template v-slot:password>
        Password
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
  methods: {
    ...mapMutations({
      clearUser: 'user/clearUser'
    }),
  },
}
</script>

<style scoped>

</style>