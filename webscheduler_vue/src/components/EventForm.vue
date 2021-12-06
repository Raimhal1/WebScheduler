<template>
  <Form v-slot="{ handleSubmit }" :validation-schema="schema" as="div" class="event event__form">
    <my-error-list :errors="errors"></my-error-list>
    <form @submit="handleSubmit($event, action)" method="post" class="form">
      Event name : <my-field
        v-model="event.eventName"
        placeholder="Event name"
        name="name"
      />
      <my-error-message name="name" />
      Start : <my-field
        v-model="event.startEventDate"
        type="datetime-local"
        name="start"
        v-focus
      />
      <my-error-message name="start" />
      End : <my-field
        v-model="event.endEventDate"
        type="datetime-local"
        name="end"
      />
      <my-error-message name="end" />
      Short description : <my-field
        v-model="event.shortDescription"
        placeholder="Short event description"
        name="short"
      />
      <my-error-message name="short" />
      Long description : <my-field
        v-model="event.description"
        placeholder="Long event description"
        name="long"
      />
      <my-error-message name="long" />
      <my-button
          type="submit"
          class="btn"
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
  name: "EventForm",
  components: {Form, MyField, MyErrorList, MyErrorMessage},
  props: {
    modified:{
      type: Boolean,
      default: false
    },
  },
  methods: {
    ...mapActions({
      createEvent: 'event/createEvent',
      updateEvent: 'event/updateEvent'
    }),
    async action(){
      if(this.modified)
        this.updateEvent(this.event.id)
      else
        this.createEvent()
    }
  },
  computed: {
    ...mapState({
      event: state => state.event.event,
      errors: state => state.errors
    }),
    schema() {
      return  yup.object().shape({
        name: yup.string().max(50).required().label('Event name'),
        start: yup.date().min(new Date(1), 'Start date is a required field').required().label('Start date'),
        end: yup.date().min(yup.ref("start"), "End date can't be before than start date").required().label('End date'),
        short: yup.string().max(50).label('Short description'),
        long: yup.string().max(2000).label('Long description'),
      })
    },
  },
}
</script>

<style scoped>
.event__form{
  width: 300px;
  border: 2px solid #0c20a1;
  border-radius: 5px;
  padding: 15px;
}
</style>