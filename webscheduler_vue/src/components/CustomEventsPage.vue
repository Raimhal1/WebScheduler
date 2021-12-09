<template>
  <div>
    <my-title><slot name="title"></slot></my-title>
    <my-search
        v-focus
        :model-value="searchQuery"
        @update:model-value="setSearchQuery"
    />
    <div class="app__btns">
      <div class="__creation">
        <slot name="create"></slot>
        <report :length="events.length" :root="root"> Report </report>
      </div>
      <my-select
          :model-value="selectedSort"
          @update:model-value="setSelectedSort"
          :options="sortOptions"
      />
    </div>
    <event-list
        :events="sortedAndSearchedEvents"
        v-if="!isEventListLoading"
        @remove="removeEvent"
    />
    <div v-else class="center">
      Loading...
    </div>
    <div
        v-intersection:[root]="getEventList"
        class="observer"
    >
    </div>
  </div>
</template>

<script>
import EventList from "@/components/EventList";
import Report from "@/components/Report";
import {mapActions, mapGetters, mapMutations, mapState} from "vuex";
export default {
  name: "MyEventsPage",
  components: {
    EventList,
    Report
  },
  props: {
    root: {
      type: String,
      required: true
    },
  },
  mounted() {
    if(this.$store.state.isAuth)
      this.getEventList(this.root)
  },
  beforeUnmount() {
    this.clearEventStore()
    this.clearErrors()
  },
  methods: {
    ...mapMutations({
      setSearchQuery: 'event/setSearchQuery',
      setSelectedSort: 'event/setSelectedSort',
      clearEventStore: 'event/clearEventStore',
      clearErrors: 'clearErrors'
    }),
    ...mapActions({
      loadMoreEvents: 'event/loadMoreEvents',
      getEventList: 'event/getEventList',
      removeEvent: 'event/removeEvent'
    }),

  },
  computed: {
    ...mapState({
      events: state => state.event.events,
      isEventListLoading: state => state.event.isLoading,
      selectedSort: state => state.event.selectedSort,
      searchQuery: state => state.event.searchQuery,
      sortOptions: state => state.event.sortOptions
    }),
    ...mapGetters({
      sortedAndSearchedEvents: 'event/sortedAndSearchedEvents'
    }),
  },
}
</script>

<style scoped>
.__creation{
  display: flex;
  justify-content: flex-start;
  gap: 10px;
}
</style>