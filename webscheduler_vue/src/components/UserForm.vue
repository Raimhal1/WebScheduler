<template>
  <Form v-slot="{ handleSubmit }" :validation-schema="schema" as="div" class="user__form">
    <h2 class="title"><slot name="header"></slot></h2>
    <my-error-list :errors="errors"></my-error-list>
    <form @submit="handleSubmit($event, action)" method="post" class="form">
      Email : <my-field
        v-model="user.email"
        name="email"
        placeholder="email@gmail.com"
      />
      <my-error-message name="email" />
      Username : <my-field
        v-model="user.userName"
        name="username"
        placeholder="Username"
      />
      <my-error-message name="username" />
      First name : <my-field
        v-model="user.firstName"
        name="firstname"
        placeholder="Firstname"
      />
      <my-error-message name="firstname" />
      Last name : <my-field
        v-model="user.lastName"
        name="lastname"
        placeholder="Lastname"
      />
      <my-error-message name="lastname" />
      <slot name="password"></slot> :
      <my-field
        v-model="user.password"
        name="password"
        type="password"
        placeholder="Password"
      />
      <my-error-message name="password" />
      <my-button
          type="submit"
      >
        <slot name="submit__name"></slot>
      </my-button>
    </form>
  </Form>
</template>

<script>
import {mapActions, mapState} from "vuex";
import {Form} from 'vee-validate'

import * as yup from 'yup'
import MyField from "@/components/UI/MyField";
import MyErrorMessage from "@/components/UI/MyErrorMessage";
import MyErrorList from "./UI/MyErrorList";

export default {
  name: "UserForm",
  components: {
    MyErrorList,
    Form, MyField, MyErrorMessage
  },
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
    async action(){
      if(this.modified)
        this.updateUser()
      else
        this.register()
    }

  },
  computed: {
    ...mapState({
      user: state => state.user.user,
      errors: state => state.errors
    }),
    schema() {
      let schema = yup.object().shape({
        email: yup.string().email().max(50).required().label('Email'),
        username: yup.string().max(50).required().label('Username'),
        firstname: yup.string().max(50).label('First name'),
        lastname: yup.string().max(50).label('Last name'),
      })

      let password
      if(!this.modified)
        password = {password: yup.string().min(5).required().label('Password')}
      else
        password = {password: yup.string().min(5).label('Password')}
      return schema.shape(password)
    },
  },
}
</script>

<style scoped>

.title{
  margin-bottom: 20px;
}
</style>