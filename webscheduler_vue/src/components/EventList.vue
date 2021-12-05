<template>
  <div v-if="events.length > 0">
    <div class="events">
      <transition-group name="event-list">
        <event-item
            v-for="event in events"
            :event="event"
            :isCreator="isCreator"
            :is-list-component="true"
            :key="event.id"
            @remove="$emit('remove', event.id)"
        />
      </transition-group>
    </div>
  </div>
  <div  v-else class="empty__list">
    <h3 style="color: rgba(109, 165, 252, 0.9)">
      List is empty
    </h3>
  </div>
</template>

<script>
import EventItem from "./EventItem";

export default {
  name: "EventList",
  components: {EventItem},
  props: {
    events: {
      type: Array,
      required: true
    },
  },
  computed: {
    isCreator(){
      return JSON.parse(localStorage.getItem('isAdmin')) || window.location.pathname.includes('my/')
    }
  }
}
</script>

<style scoped>
.events{
  padding: 15px;
  display: flex;
  flex-flow: row wrap;
  justify-content: center;
  align-items: stretch;
}

.empty__list{
  display: flex;
  justify-content: center;
  align-items: center;
}

.event-list-item{
  display: inline-block;
  margin-right: 10px;
}
.event-list-enter-active,
.event-list-leave-active{
  transition: all 0.7s ease;
}

.event-list-enter-from,
.event-list-leave-to{
  opacity: 0;
  transform: translateX(130px);
}

.event-list-move{
  transition: transform 0.4s ease;
}
</style>