<template>
  <div class="event">
    <h4>{{ event.eventName }}</h4><br/>
    <div>
      Start : {{ new Date(event.startEventDate).toLocaleString() }}<br/>
    </div>
    <div>
      End : {{ new Date(event.endEventDate).toLocaleString() }}<br/>
    </div>
    <div>
      Short info : {{ event.shortDescription }}<br/>
    </div>
    <div v-if="showFullInfo">
      Info : {{ event.description }}<br/>
    </div>
    <div v-if="showUsers">
      Users:
      <div v-for="user in event.users" :key="user.email">
        {{user.userName}} ({{user.email}})
      </div>
    </div>
    <div class="event__btns">
      <my-button
        @click="$emit('remove', event.id)"
        v-if="isCreator"
      >
        Delete
      </my-button>
      <my-button
          v-if="isCreator"
          @click="showUpdateDialog"
      >
        Edit
      </my-button>
      <my-button
          v-if="isListComponent"
        @click="$router.push(`/events/${event.id}`)"
      >
        Details
      </my-button>
    </div>
  </div>
</template>

<script>
export default {
  name: "EventItem",
  props:{
    event: {
      type: Object,
      required: true,
    },
    showFullInfo: {
      type: Boolean,
      default: false
    },
    showUsers:{
      type: Boolean,
      default: false
    },
    isCreator:{
      type: Boolean,
      default: false
    },
    isListComponent:{
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      updateDialogVisible: false
    }
  },
  methods: {
    async showUpdateDialog() {
      this.updateDialogVisible = true;
    }
  }

}
</script>

<style scoped>
.event{
  padding: 5px;
  margin: 5px;
  border: 2px solid #0c20a1;
  border-radius: 5px;
}
.event__btns{
  display: flex;
  justify-content: space-evenly;
}
</style>