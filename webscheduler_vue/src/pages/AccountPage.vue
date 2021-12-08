<template>
  <div class="user__page">
    <user-form :modified="modified">
      <template v-slot:header>
        Account
      </template>
      <template v-slot:password>
        New password
      </template>
      <template v-slot:submit__name>
        Save
      </template>
    </user-form>
  </div>
</template>

<script>
import {mapActions, mapMutations, mapState} from "vuex";
import UserForm from "@/components/UserForm";

export default {
  name: "AccountPage",
  components: {UserForm},
  mounted() {
    if(this.$store.state.isAuth)
      this.getCurrentUser()
  },
  beforeUnmount() {
    this.clearErrors()
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
    }),
    ...mapMutations({
      clearErrors: 'clearErrors'
    })
  }
}
</script>

<style scoped>

</style>