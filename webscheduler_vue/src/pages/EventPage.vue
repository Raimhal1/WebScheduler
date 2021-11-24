<template>
  <div>
    <my-title>Events :</my-title>
    <my-search
        v-model="searchQuery"
    />
    <div class="app__btns">
      <my-select
          v-model="selectedSort"
          :options="sortOptions"
      />
    </div>
    <event-list
        :events="sortedAndSearchedEvents"
        v-if="!isEventListLoading"
    />
    <div v-else class="center">
      Loading...
    </div>
    <div
        ref="observer"
        class="observer"
    ></div>
  </div>
</template>

<script>
import EventList from "@/components/EventList";
import {instance} from '@/instance'
export default {
  name: "MyEventPage",
  components: {
    EventList,
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
      else {
        return this.allEvents.splice(0, this.allEvents.length)
      }
    },
    async getEvent(event_id){
      const path = `events/${event_id}`
      const result = await instance.get(path)
      return result.data
    },

  }
}
</script>

<style scoped>
</style>