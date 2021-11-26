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
        class="list"
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
    document.querySelector('.list').innerHTML = ''
    this.getEventList(window.location.pathname)
  },
  methods: {
    ...mapMutations({
      setSearchQuery: 'my_event/setSearchQuery',
      setSelectedSort: 'my_event/setSelectedSort'
    }),
    ...mapActions({
      loadMoreEvents: 'my_event/loadMoreEvents',
      getEventList: 'my_event/getEventList',
    }),

  },
  computed: {
    ...mapState({
      events: state => state.my_event.events,
      allEvents: state => state.my_event.allEvents,
      isEventListLoading: state => state.my_event.isEventListLoading,
      selectedSort: state => state.my_event.selectedSort,
      searchQuery: state => state.my_event.searchQuery,
      limit: state => state.my_event.limit,
      sortOptions: state => state.my_event.sortOptions
    }),
    ...mapGetters({
      sortedAndSearchedEvents: 'my_event/sortedAndSearchedEvents'
    })
  },
}
</script>

<style scoped>
</style>