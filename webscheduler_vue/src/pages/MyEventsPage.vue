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
      <my-button @click="showDialog"> Create event </my-button>
    </div>
    <event-list
        :events="sortedAndSearchedEvents"
        :is-creator="true"
        @remove="removeEvent"
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
    <my-event-dialog v-model:show="dialogVisible">
      <event-form
          :modified="false"
      />
    </my-event-dialog>
  </div>
</template>

<script>
import EventForm from "@/components/EventForm";
import EventList from "@/components/EventList";
import {mapState, mapActions, mapGetters, mapMutations} from 'vuex'
export default {
  name: "MyEventsPage",
  components: {
    EventList,
    EventForm
  },
  mounted() {
    if(this.$store.state.isAuth)
      this.getEventList(window.location.pathname)
  },
  beforeUnmount() {
    this.$store.commit('event/clearEventStore')
  },
  data() {
    return {
      dialogVisible: false,
    }
  },
  methods: {
    ...mapMutations({
        setSearchQuery: 'event/setSearchQuery',
        setSelectedSort: 'event/setSelectedSort'
    }),
    ...mapActions({
      loadMoreEvents: 'event/loadMoreEvents',
      getEventList: 'event/getEventList',
      removeEvent: 'event/removeEvent',
    }),
    async showDialog() {
      this.dialogVisible = true;
    },

  },
  computed: {
      ...mapState({
        events: state => state.event.events,
        allEvents: state => state.event.allEvents,
        event: state => state.event.event,
        isEventListLoading: state => state.event.isEventListLoading,
        selectedSort: state => state.event.selectedSort,
        searchQuery: state => state.event.searchQuery,
        limit: state => state.event.limit,
        sortOptions: state => state.event.sortOptions,
      }),
      ...mapGetters({
        sortedAndSearchedEvents: 'event/sortedAndSearchedEvents',
      })
  },

}
</script>

<style scoped>

</style>