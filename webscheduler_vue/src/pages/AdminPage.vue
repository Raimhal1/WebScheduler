<template>
  <user-list :users="users">
  </user-list>
  <report
      :length="length"
      :root="root"
      :reportType="reportType"
  >
    Next month report
  </report>
</template>

<script>
import Report from "@/components/Report";
import UserList from "../components/UserList";
import {mapActions, mapMutations, mapState} from "vuex";

export default {
  name: "AdminPage",
  components: {UserList, Report},
  data(){
    return{
      length: 1,
      reportType: 'next-month-report',
      root: 'events'
    }
  },
  mounted() {
    this.getUsers()
  },
  methods: {
    ...mapMutations({

    }),
    ...mapActions({
      getUsers: 'user/getUsers',
    }),

  },
  computed: {
    ...mapState({
      users: state => state.user.users
    }),
  },
}
</script>

<style scoped>

</style>