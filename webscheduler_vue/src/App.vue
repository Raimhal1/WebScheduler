<template>
  <div class="app">
  <event-list
      :events="events"
      @remove="removeEvent"
  />
  <event-form
      @create="createEvent"
  />
  </div>
</template>

<script>
import EventForm from "./components/EventForm";
import EventList from "./components/EventList";
import {instance} from './instance'
export default {
  name: "App",
  components: {
    EventList,
    EventForm
  },
  mounted() {
      instance.get('my/events').then(res => (this.events = res.data))
  },
  data() {
    return {
        events: [],
        event: {
        eventName: "",
        startDate: "",
        endDate: "",
        shortDesc: "",
        longDesc: ""
      }
    }

  },
  methods: {
    async createEvent(event){
      console.log(event.data)
      await this.getEventList()
    },
    async getEventList() {
      await instance.get('my/events').then(res => (this.events = res.data))
    },
    async getEvent(event_id){
      const path = `events/${event_id}`
      console.log(path)
      const result = await instance.get(path)
      return result.data
    },
    async removeEvent(event_id){
      const path = `events/${event_id}/delete`
      await instance.delete(path)
          await this.getEventList()
    }
  }
}
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

.app{
  padding: 20px;
}

</style>