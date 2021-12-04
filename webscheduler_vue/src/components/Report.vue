<template>
  <my-button @click="showDialog" v-if="length !== 0"> <slot></slot> </my-button>
  <my-dialog v-model:show="dialogVisible">
      <div class="report">
        <my-button @click="generateReport('xml')"> XML </my-button>
        <my-button @click="generateReport('csv')"> CSV </my-button>
      </div>
  </my-dialog>
</template>

<script>
import {instance} from "@/router/instance";

export default {
  name: "Report",
  data(){
    return {
      dialogVisible: false,
    }
  },
  props: {
    length : {
      type: Number,
      default: 0
    },
    root: {
      type: String,
      required: true
    },
    reportType: {
      type: String,
      default: 'report'
    }

  },
  methods: {
    async showDialog() {
      this.dialogVisible = true
    },
    async generateReport(extension){
      const path = `${this.root}/${this.reportType}/${extension}`
      await instance
          .get(path, {
            responseType: 'blob',
            headers: {
              Authorization: `Bearer ${this.$store.state.accessToken}`,
            },
          })
          .then(response => {
            console.log(response.data)
            const link = document.createElement('a')
            link.href= window.URL.createObjectURL(new Blob([response.data]))
            if(extension === 'csv')
              extension = 'xls'
            link.setAttribute('download', `report.${extension}`)
            document.body.appendChild(link)
            link.click()
            document.body.removeChild(link)
          })
          .catch( error => console.log(error))
    }
  }

}
</script>

<style scoped>
.report{
  width: 20vw;
  display: flex;
  flex-direction: column;
  border: 2px solid #0c20a1;
  border-radius: 5px;
  padding: 15px;
}
</style>