<template>
    <form @submit.prevent method="post" class="event form">
      Event name : <my-input
        v-model="event.eventName"
        type="text"
        placeholder="Event name"
        v-focus
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
          @click="updateEvent(id)"
          class="btn"
          v-if="modified"
      >
        Update
      </my-button>
      <my-button
          @click="createEvent"
          class="btn"
          v-else
      >
        Create
      </my-button>
    </form>
</template>

<script>
import {mapActions, mapState} from "vuex";
import MyButton from "./UI/MyButton";

export default {
  name: "EventForm",
  components: {MyButton},
  props: {
    modified:{
      type: Boolean,
      default: false
    },
    id:{
      type: String,
      default: null
    }
  },
  methods: {
    ...mapActions({
      createEvent: 'event/createEvent',
      updateEvent: 'event/updateEvent'
    }),
  },
  computed: {
    ...mapState({
      event: state => state.event.event,
    }),
  },
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