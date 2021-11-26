<template>
  <event-item
      :event="event"
      :showUsers="true"
      :showFullInfo="true"
      class="custom"
  ></event-item>
</template>

<script>
import EventItem from "@/components/EventItem";
import {instance} from "@/instance";
export default {
  name: "EventPage",
  components: {EventItem},
  props: {

  },
  async mounted() {
    console.log(this.$route.params)
    this.event = await this.getEvent(this.$route.params.id)
  },
  data() {
    return {
      event: {
        eventName: "",
        startEventDate: "",
        endEventDate: "",
        shortDescription: "",
        description: "",
      }
    }
  },
  methods: {
    async getEvent(event_id){
      const path = `events/${event_id}`
      const result = await instance.get(path)
      return result.data
    },
  }
}

</script>

<style scoped>
.custom{
  font-size: 28px;
}
</style>