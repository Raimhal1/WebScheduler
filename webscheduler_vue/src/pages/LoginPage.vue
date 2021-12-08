<template>
  <div class="user__page">
    <Form v-slot="{ handleSubmit }" :validation-schema="schema" as="div" class="user__form">
      <h2 class="title">Log in</h2>
      <my-error-list :errors="errors"></my-error-list>
      <form @submit="handleSubmit($event, login)" class="form">
        <MyField
            v-model="user.email"
            name="email"
            placeholder="email@gmail.com"
        />
        <MyErrorMessage name="email" />
        <MyField
            v-model="user.password"
            name="password"
            type="password"
            placeholder="password"
        />
        <MyErrorMessage name="password" />
        <my-button
            type="submit"
        >
          Log in
        </my-button>
      </form>
    </Form>
  </div>
</template>

<script>
import {mapActions, mapMutations, mapState} from "vuex";
import {Form} from 'vee-validate'

import * as yup from 'yup'
import MyField from "@/components/UI/MyField";
import MyErrorMessage from "@/components/UI/MyErrorMessage";
import MyErrorList from "@/components/UI/MyErrorList";

export default {
  name: "LoginPage",
  components:{
    MyErrorList,
    MyField,
    Form, MyErrorMessage
  },
  beforeUnmount() {
    if(this.isAuth) {
      localStorage.accessToken = this.accessToken
      localStorage.refreshToken = this.refreshToken
      localStorage.isAuth = this.isAuth
      localStorage.isAdmin = this.isAdmin
    }
    this.clearErrors()
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
    schema() {
      return yup.object().shape({
        email: yup.string().email().max(50).required().label('Email'),
        password: yup.string().min(5).required().label('Password'),
      })
    }
  },
  methods: {
    ...mapActions({
      login: 'user/login'
    }),
    ...mapMutations({
      clearErrors: 'clearErrors'
    })

  }
}

</script>

<style scoped>
.title{
  margin-bottom: 20px;
}
</style>