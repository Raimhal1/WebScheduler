<template>
  <div>
    <my-title>Events :</my-title>
    <my-search
        v-focus
        :model-value="searchQuery"
        @update:model-value="setSearchQuery"

    />
    <div class="app__btns">
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
        v-intersection="loadMoreEvents"
        class="observer"
    ></div>
  </div>
</template>

<script>
import EventList from "@/components/EventList";
import {mapActions, mapGetters, mapMutations, mapState} from "vuex";
export default {
  name: "MyEventsPage",
  components: {
    EventList,
  },
  mounted() {
    this.getEventList(window.location.pathname)
  },
  beforeUnmount() {
    this.clearEventStore()
  },
  methods: {
    ...mapMutations({
      setSearchQuery: 'event/setSearchQuery',
      setSelectedSort: 'event/setSelectedSort',
      clearEventStore: 'event/clearEventStore'
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
      allEvents: state => state.event.allEvents,
      isEventListLoading: state => state.event.isLoading,
      selectedSort: state => state.event.selectedSort,
      searchQuery: state => state.event.searchQuery,
      limit: state => state.event.limit,
      sortOptions: state => state.event.sortOptions
    }),
    ...mapGetters({
      sortedAndSearchedEvents: 'event/sortedAndSearchedEvents'
    }),
  },
}
</script>

<style scoped>
</style>