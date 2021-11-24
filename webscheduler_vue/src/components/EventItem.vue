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
    <div class="event__btns" v-if="creator">
      <my-button
        @click="$emit('remove', event.id)"
      >
        Delete
      </my-button>
      <my-button>
        Edit
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
    creator:{
      type: Boolean,
      default: false
    }
  },
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