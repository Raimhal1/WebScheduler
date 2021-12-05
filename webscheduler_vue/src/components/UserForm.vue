<template>
  <form @submit.prevent method="post" class="user__form">
    <h2 class="title"><slot name="header"></slot></h2>
    Email : <my-input
      v-model="user.email"
      type="email"
      placeholder="Email@email.com"
    />
    Username : <my-input
      v-model="user.userName"
      type="text"
      placeholder="Username"
    />
    First name :<my-input
      v-model="user.firstName"
      type="text"
      placeholder="Firstname"
      v-focus
    />
    Last name : <my-input
      v-model="user.lastName"
      type="text"
      placeholder="Lastname"
    />
    <slot name="password"></slot> : <my-input
      v-model="user.password"
      type="password"
      placeholder="Password"
    />
    <my-button
        @click="updateUser"
        v-if="modified"
    >
      Save
    </my-button>
    <my-button
        @click="register"
        v-else
    >
      Register
    </my-button>
  </form>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
  name: "UserForm",
  props: {
    modified:{
      type: Boolean,
      default: false
    }
  },
  methods: {
    ...mapActions({
      register: 'user/register',
      updateUser: 'user/updateUser'
    }),
  },
  computed: {
    ...mapState({
      user: state => state.user.user,
    }),
  },
}
</script>

<style scoped>

.title{
  margin-bottom: 20px;
}
</style>