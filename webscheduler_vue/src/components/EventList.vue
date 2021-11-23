<template>
  <div>
    <my-title>Events :</my-title>
    <div class="events">
      <event-item
          v-for="event in events"
          :event="event"
          :key="event.id"
          @remove="$emit('remove', event.id)"
      />
    </div>
  </div>
</template>

<script>
import EventItem from "./EventItem";
import MyTitle from "./UI/MyTitle";
export default {
  name: "EventList",
  components: {MyTitle, EventItem},
  props: {
    events: {
      type: Array,
      required: true
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

</style>