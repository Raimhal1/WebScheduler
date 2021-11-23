<template>
    <form @submit.prevent method="post" class="event form">
      Event name : <my-input
        v-model="event.eventName"
        type="text"
        placeholder="Event name"
      />
      Start : <my-input
        v-model="event.startEventDate"
        type="datetime-local"
      />
      End : <my-input
        v-model="event.endEventDate"
        type="datetime-local"
      />
      Short description : <my-input
        v-model="event.shortDescription"
        type="text"
        placeholder="Short event description"
      />
      Long description : <my-input
        v-model="event.description"
        type="text"
        placeholder="Long event description"
      />
      <my-button
          @click="createEvent"
          class="btn"
      >
        Create
      </my-button>
    </form>
</template>

<script>
import {instance} from "../instance";
import MyInput from "./UI/MyInput";

export default {
  name: "EventForm",
  components: {MyInput},
  data(){
    return{
      event: {
        eventName: "",
        startEventDate: null,
        endEventDate: null,
        shortDescription: "",
        description: ""
      }
    }
  },

  methods: {
    async createEvent() {
      console.log(this.event)
        await instance.post('events', this.event).then(res => this.$emit('create', res))
    },
  }
}
</script>

<style scoped>
.form{
  width: 300px;
  display: flex;
  flex-direction: column;
  border: 2px solid #0c20a1;
  border-radius: 5px;
  padding: 15px;
}
</style>