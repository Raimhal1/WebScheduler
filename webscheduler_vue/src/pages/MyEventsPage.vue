<template>
  <div>
    <custom-events-page
        :root="root"
    >
      <template v-slot:title>
        Your events :
      </template>
      <template v-slot:create>
        <my-button @click="showDialog"> Create event </my-button>
        <my-dialog v-model:show="dialogVisible">
          <event-form>
            <template v-slot:submit__name>
              Create
            </template>
          </event-form>
        </my-dialog>
      </template>
    </custom-events-page>
  </div>
</template>

<script>
import EventForm from "@/components/EventForm";
import CustomEventsPage from "@/components/CustomEventsPage";

export default {
  name: "MyEventsPage",
  components: {
    EventForm,
    CustomEventsPage
  },
  data(){
    return{
      root: 'my/events',
      dialogVisible: false
    }
  },
  methods: {
    async showDialog() {
      this.dialogVisible = true
    },
  },
}
</script>

<style scoped>
</style>