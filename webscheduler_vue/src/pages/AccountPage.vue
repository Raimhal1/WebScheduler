<template>
  <div class="user__page">
    <user-form :modified="modified">
      <template v-slot:header>
        Account
      </template>
      <template v-slot:password>
        New password
      </template>
    </user-form>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import UserForm from "@/components/UserForm";

export default {
  name: "AccountPage",
  components: {UserForm},
  mounted() {
    this.getCurrentUser()
  },
  data(){
    return{
      modified: true
    }
  },
  computed: {
    ...mapState({
      user: state => state.user.user,
    }),
  },
  methods: {
    ...mapActions({
      getCurrentUser: 'user/GetCurrentUser',
      updateUser: 'user/updateUser',
    })
  }
}
</script>

<style scoped>

</style>