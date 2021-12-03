<template>
  <div>
    <custom-events-page
        :root="root"
    >
      <template v-slot:title>
        Invited events :
      </template>
    </custom-events-page>
  </div>
</template>

<script>
import CustomEventsPage from "@/components/CustomEventsPage";
export default {
  name: "MyEventsPage",
  components: {
    CustomEventsPage
  },
  data(){
    return{
      root: 'events',
    }
  },
}
</script>

<style scoped>
</style>