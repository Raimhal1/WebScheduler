<template>
  <div class="event">
    <div>
      <h3 class="header">{{ event.eventName }}</h3>
      <div class="status">
        {{ this.getStatus(event.status)}}
      </div>
      <div>
        Start : {{ new Date(event.startEventDate).toLocaleString().slice(0, -3) }}
      </div>
      <div>
        End : {{ new Date(event.endEventDate).toLocaleString().slice(0, -3) }}
      </div>

      <div v-if="event.shortDescription">
        Short description : {{ event.shortDescription }}
      </div>
      <div v-if="showFullInfo && event.description">
        Long description : {{ event.description }}
      </div>
      <div v-if="showUsers">
        <div class="users">
          <div v-for="user in event.users" :key="user.email" class="user">
            {{user.userName}} ({{user.email}})
          </div>
        </div>
      </div>
    </div>
    <div class="event__btns">
      <my-button
        @click="$emit('remove', event.id)"
        v-if="isCreator && isNotHiddenDelete"
      >
        Delete
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
    },
    isNotHiddenDelete:{
      type: Boolean,
      default: true
    }
  },
  methods: {
    getStatus(s){
      switch (s){
        case 0: return "Expected"
        case 1: return "In process"
        case 2: return "Ended"
      }
    }
  }
}
</script>

<style scoped>

.header{
  display: flex;
  justify-content: center;
}

.event{
  padding: 5px;
  margin: 5px;
  border: 2px solid #0c20a1;
  border-radius: 5px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  word-wrap: break-word;
}

.event__btns{
  display: flex;
  justify-content: center;
  gap: 30px;
}

.users{
  display: flex;
  flex-flow: wrap;
  width: fit-content;
  font-size: 14px;
}
.user{
  white-space: nowrap;
  border-radius: 18px;
  padding: 0 10px;
  margin: 2px;
  background-color: rgba(2, 106, 248, 0.21);
  overflow: hidden;
}


</style>