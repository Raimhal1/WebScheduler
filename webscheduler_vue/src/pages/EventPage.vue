<template>
  <event-item
      :event="event"
      :showUsers="true"
      :showFullInfo="true"
      class="custom"
      :is-not-hidden-delete="false"
  ></event-item>
  <my-event-dialog v-model:show="dialogVisible">
    <event-form
        :modified="true"
        :id="this.id"
    />
  </my-event-dialog>
  <div class="event__btns">
    <my-button @click="this.$router.back()"> Back </my-button>
    <my-button @click="showDialog" v-if="this.isCreator"> Update </my-button>
  </div>
</template>

<script>
import EventItem from "@/components/EventItem";
import EventForm from "@/components/EventForm";
import {mapActions, mapMutations, mapState} from "vuex";
export default {
  name: "EventPage",
  components: {EventItem, EventForm},
  props: {
  },
  beforeUnmount() {
    this.clearEvent()
  },
  async mounted() {
    await this.getEvent(this.id)
  },
  data(){
    return{
      dialogVisible: false,
      isCreator: (window.history.state.back === '/my/events') || (this.isAdmin === true),
      id: this.$route.params.id
    }
  },
  computed: {
    ...mapState({
      event: state => state.event.event,
      event_id: state => state.event.event_id,
      isAdmin: state => state.isAdmin
    }),

  },
  methods: {
    ...mapActions({
      getEvent: 'event/getEvent',
      removeEvent: 'event/removeEvent'
    }),
    ...mapMutations({
      clearEvent: 'event/clearEvent'
    }),
    async showDialog() {
      this.dialogVisible = true;
    },
  },
}

</script>

<style scoped>
.custom{
  font-size: 26px;
}
.event__btns{
  margin: 15px 10px;
  display: flex;
  justify-content: space-between;
}
</style>