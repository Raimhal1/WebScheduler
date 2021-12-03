<template>
  <event-item
      :event="event"
      :showUsers="true"
      :showFullInfo="true"
      class="custom"
      :is-not-hidden-delete="false"
      :is-creator="isCreator"
  ></event-item>
  <div v-if="!isLoading">
    <div class="images">
      <div v-for="blob in imageBlobs" :key="blob.id" class="container">
        <img
            :src="getUrl(blob)"
            alt="image"
            class="image"
            @click="ScaleImage"
        >
        <my-button class="btn" @click="removeFile(blob.id)" v-if="isCreator">X</my-button>
      </div>
    </div>
    <div class="text__files">
      <div v-for="blob in textBlobs" :key="blob.id">
        <my-button>
          <a :href="getUrl(blob)" download class="text__file">
              {{ getTextFileName(blob.type) }}
          </a>
        </my-button>
        <my-button @click="removeFile(blob.id)" v-if="isCreator" class="file__btn">X</my-button>
      </div>
    </div>
  </div>
  <div v-else class="center">
    Loading...
  </div>
  <div class="event__btns">
    <my-button @click="$router.back()"> Back </my-button>
    <div class="creator__btns" v-if="isCreator">
      <my-button @click="showFileDialog" v-if="[...imageBlobs, ...textBlobs].length < 5" > Add Files </my-button>
      <my-button @click="showDialog"> Update </my-button>
    </div>
  </div>

  <my-event-dialog v-model:show="dialogVisible">
    <event-form
        :modified="true"
        :id="id"
    />
  </my-event-dialog>
  <my-event-dialog v-model:show="fileDialogVisible">
    <form enctype="multipart/form-data" method="post" id="uploadForm"  @submit.prevent class="file__form">
      <my-input type="file" id="files" multiple/>
      <my-button @click="uploadFiles">Submit</my-button>
      <div v-if="isLoading">Loading...</div>
    </form>
  </my-event-dialog>
</template>

<script>
import EventItem from "@/components/EventItem";
import EventForm from "@/components/EventForm";
import {mapActions, mapMutations, mapState} from "vuex";
import {instance} from "@/router/instance";

export default {
  name: "EventPage",
  components: {EventItem, EventForm},
  props: {
  },
  beforeUnmount() {
    this.clearEvent()
    this.clearBlobs()
  },
  async mounted() {
    if(this.isAuth) {
      await this.getEvent(this.id)
      await this.getEventFiles()
    }
  },
  data(){
    return{
      dialogVisible: false,
      fileDialogVisible: false,
      id: this.$route.params.id,
      urlCreator: window.URL || window.webkitURL,
      types: ['word', 'pdf', 'text']
    }
  },
  computed: {
    ...mapState({
      isAuth: state => state.isAuth,
      isAdmin: state => state.isAdmin,
      event: state => state.event.event,
      isLoading: state => state.event.isLoading,
      imageBlobs: state => state.event.imageBlobs,
      textBlobs: state => state.event.textBlobs
    }),
    isCreator(){
      return (window.history.state.back === '/my/events') || (Boolean(this.isAdmin))
    },

  },
  methods: {
    ...mapActions({
      getEvent: 'event/getEvent',
      getEventFiles: 'event/getEventFiles',
      removeFile: 'event/removeFile'
    }),
    ...mapMutations({
      clearEvent: 'event/clearEvent',
      clearBlobs: 'event/clearBlobs'
    }),
    async showDialog() {
      this.dialogVisible = true
    },
    async showFileDialog(){
      this.fileDialogVisible = true
    },
    async ScaleImage(e){
      let el = e.target
      el.classList.toggle('image__max')
      el.parentNode.classList.toggle('dialog')
      el.parentNode.classList.toggle('absolute')
      el.nextElementSibling.classList.toggle('close')
    },
    getUrl(blob){
      return this.urlCreator.createObjectURL(blob)
    },
    getTextFileName(type){
      if(type.includes('word'))
        return 'doc'
      else if(type.includes('pdf'))
        return 'pdf'
      else if(type.includes('text'))
        return  'txt'
    },
    async uploadFiles(){
      this.$store.commit('event/setLoading', true)
      const form = new FormData(document.querySelector('#uploadForm'))
      var data = document.querySelector('#files')
      console.log(data)
      console.log(data.files)
      console.log(data.files[0])
      for(let i = 0; i < data.files.length; i++)
        form.append('files', data.files[i])
      console.log(form)
      const path = `events/${this.event.id}/files/add-files`
      await instance
          .post(path, form, {
            headers: {
              Authorization: `Bearer ${this.$store.state.accessToken}`,
              'Content-Type': 'multipart/form-data',
          }})
          .then(response => {
            console.log(response)
            console.log('ok')
          })
          .catch(error => console.log(error))
          .then(() => this.$store.dispatch('event/getEventFiles'))
      this.$store.commit('event/setLoading', false)
    }
  },
}

</script>

<style scoped>

.custom{
  font-size: 18px;
}

.event__btns{
  margin: 15px 10px;
  display: flex;
  justify-content: space-between;
}

.image{
  height: 30vh;
  width: auto;
  cursor: pointer;
  margin: 5px 5px;
}

.image__max{
  right: 0;
  top: 0;
  margin: auto;
  width: auto;
  height: 85vh;
}

.images{
  display: flex;
  flex-flow: row wrap;
  align-items: center;
  justify-content: center;
}

.dialog{
  position: fixed;
  top: 0;
  right: 0;
  left: 0;
  bottom: 0;
  display: flex;
}

.container{
  position: relative;
}

.absolute{
  position: absolute;
  z-index: 2;
}

.container .btn {
  position: absolute;
  right: 1px;
  top: 30px;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  background-color: #555;
  color: white;
  font-size: 16px;
  border: none;
  cursor: pointer;
  text-align: center;
}

.container .btn:hover {
  background-color: #282626;
}

.close{
  display: none;
}

.text__files{
  display: flex;
  justify-content: center;
  gap: 20px;
}

.text__files>div{
  display: flex;
  flex-wrap: nowrap;
}

.text__file{
  text-decoration: none;
  font-size: 14px;
}
.text__file:hover{
  color: #ff8010;
}

.file__btn{
  background-color: #555;
  border: none;
  color: white;
  font-size: 15px;
  cursor: pointer;
  text-align: center;
}

.file__btn:hover{
  background-color: #282626;
}

.file__form{
  display: flex;
  flex-direction: column;
  border: 2px solid #0c20a1;
  border-radius: 5px;
  padding: 15px;
}

.creator__btns{
  display: flex;
  justify-content: flex-end;
  gap: 20px;
}


</style>