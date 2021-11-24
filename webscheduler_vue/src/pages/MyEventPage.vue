<template>
  <div>
    <my-title>Events :</my-title>
    <my-search
        v-model="searchQuery"
    />
    <div class="app__btns">
      <my-button @click="showDialog" style="margin: 15px 0"> Create event </my-button>
      <my-select
          v-model="selectedSort"
          :options="sortOptions"
      />
    </div>
    <event-list
        :events="sortedAndSearchedEvents"
        :creator="true"
        @remove="removeEvent"
        v-if="!isEventListLoading"
    />
    <div v-else class="center">
      Loading...
    </div>
    <div
        ref="observer"
        class="observer"
    ></div>
    <my-event-dialog v-model:show="dialogVisible">
      <event-form
          @create="createEvent"
      />
    </my-event-dialog>
  </div>
</template>

<script>
import EventForm from "@/components/EventForm";
import EventList from "@/components/EventList";
import {instance} from '@/instance'
export default {
  name: "MyEventPage",
  components: {
    EventList,
    EventForm
  },
  mounted() {
    this.getEventList(window.location.pathname)
    const options = {
      rootMargin: '0px',
      threshold: 1.0
    }

    const callback = (entries) =>{
      if(entries[0].isIntersecting && this.allEvents !== []){
        this.loadMoreEvents()
      }
    }

    const observer =  new IntersectionObserver(callback, options)
    observer.observe(this.$refs.observer)

  },
  computed: {
    sortedEvents(){
      return [...this.events].sort((event_a, event_b) =>
          event_a[this.selectedSort]?.localeCompare(event_b[this.selectedSort]))
    },
    sortedAndSearchedEvents(){
      return this.sortedEvents.filter(e => e.eventName.toLowerCase().includes(this.searchQuery.toLowerCase()))
    }
  },
  data() {
    return {
      events: [],
      allEvents: [],
      event: {
        eventName: "",
        startEventDate: "",
        endEventDate: "",
        shortDescription: "",
        description: "",
      },
      dialogVisible: false,
      isEventListLoading: false,
      selectedSort: '',
      searchQuery: '',
      sortOptions: [
        {value: 'eventName', name: 'By name'},
        {value: 'startEventDate', name: 'By date'}
      ]
    }

  },
  methods: {
    async createEvent(info){
      const event = await this.getEvent(info.data)
      this.events.push(event)
    },
    async getEventList(path) {
      try {
        this.isEventListLoading = true
        const result = await instance.get(path)
        this.allEvents = result.data
        await this.loadMoreEvents(40)
      }
      catch (ex){
        console.log(ex)
      }
      finally {
        this.isEventListLoading = false;
      }
    },
    async loadMoreEvents(len=25){
      this.events = [...this.events, ...(await this.getMoreEvents(len))]
    },
    async getMoreEvents(len){
      if(this.allEvents.length >= len)
        return this.allEvents.splice(0, len)
      else
        return this.allEvents.splice(0, this.allEvents.length)
    },
    async getEvent(event_id){
      const path = `events/${event_id}`
      const result = await instance.get(path)
      return result.data
    },
    async removeEvent(event_id){
      const path = `events/${event_id}/delete`
      await instance.delete(path)
      this.events = this.events.filter(x => x.id !== event_id )
    },
    async showDialog(){
      this.dialogVisible = true;
    },
  }
}
</script>

<style scoped>

</style>