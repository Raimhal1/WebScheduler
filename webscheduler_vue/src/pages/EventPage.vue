<template>
  <div class="page">
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
        <div v-for="blob in imageBlobs" :key="blob.id" class="container" >
          <img
              :src="getUrl(blob)"
              alt="image"
              class="image"
              @click="ScaleImage"
          >
          <my-button class="btn" @click="removeFile([event.id, blob.id])" v-if="isCreator">X</my-button>
        </div>
      </div>
      <div class="text__files">
        <div v-for="blob in textBlobs" :key="blob.id">
          <my-button>
            <a :href="getUrl(blob)" download class="text__file">
                {{ getTextFileName(blob.type) }}
            </a>
          </my-button>
          <my-button @click="removeFile([event.id, blob.id])" v-if="isCreator" class="file__btn">X</my-button>
        </div>
      </div>
    </div>
    <div v-else class="center">
      Loading...
    </div>
    <div class="event__btns">
      <my-button @click="$router.back()"> Back </my-button>
      <div class="creator__btns" v-if="isCreator">
        <my-button @click="showAssignDialog"> Invite </my-button>
        <my-button @click="showFileDialog" v-if="fileCount < 5" > Add Files </my-button>
        <my-button @click="showDialog"> Edit </my-button>
      </div>
    </div>

    <my-dialog v-model:show="dialogVisible">
      <event-form :modified="true">
        <template v-slot:submit__name>
          Save
        </template>
      </event-form>
    </my-dialog>
    <my-dialog v-model:show="fileDialogVisible">
      <Form v-slot="{ handleSubmit }" :validation-schema="filesSchema" as="div" class="file__form">
        <my-error-list :errors="errors"></my-error-list>
        <form @submit="handleSubmit($event, UploadFiles)" enctype="multipart/form-data" method="post" id="uploadForm" class="form">
          <my-field type="file" name="files" id="files" @click="this.show()" v-focus multiple/>
          <my-error-message name="files" />
          <my-button type="submit">Submit</my-button>
          <div v-if="isLoading">Loading...</div>
        </form>
      </Form>
    </my-dialog>
    <my-dialog v-model:show="assignDialogVisible">
      <Form v-slot="{ handleSubmit }" :validation-schema="emailsSchema" as="div" class="assign__form">
        <my-error-list :errors="errors"></my-error-list>
        <form method="post" @submit="handleSubmit($event, AssignUsers)" class="form">
          <my-field v-focus v-model="userEmails" name="emailList" placeholder="email@email.com, email2@gmail.com"/>
          <my-error-message name="emailList" />
          <my-button type="submit">Submit</my-button>
          <div v-if="isLoading">Loading...</div>
        </form>
      </Form>
    </my-dialog>
  </div>
</template>

<script>
import EventItem from "@/components/EventItem";
import EventForm from "@/components/EventForm";
import {mapActions, mapMutations, mapState} from "vuex";
import {Form} from 'vee-validate'

import * as yup from 'yup'
import MyField from "@/components/UI/MyField";
import MyErrorMessage from "@/components/UI/MyErrorMessage";
import MyErrorList from "@/components/UI/MyErrorList";

export default {
  name: "EventPage",
  components: {EventItem, EventForm, Form, MyField, MyErrorMessage, MyErrorList},
  props: {
  },
  beforeUnmount() {
    this.clearEvent()
    this.clearBlobs()
    this.clearErrors()
  },
  async mounted() {
    if(this.isAuth) {
      await this.getEvent(this.id)
      await this.getEventFiles(this.event.id)
      await this.getAllowedFileExtensions()
    }
  },
  data(){
    return{
      dialogVisible: false,
      fileDialogVisible: false,
      assignDialogVisible: false,
      id: this.$route.params.id,
      urlCreator: window.URL || window.webkitURL,
      userEmails: "",

    }
  },
  computed: {
    ...mapState({
      isAuth: state => state.isAuth,
      isAdmin: state => state.isAdmin,
      event: state => state.event.event,
      isLoading: state => state.file.isLoading,
      imageBlobs: state => state.file.imageBlobs,
      textBlobs: state => state.file.textBlobs,
      fileCount: state => state.file.file_ids.length,
      fileTypes: state => state.file.fileTypes,
      errors: state => state.errors
    }),
    isCreator(){
      return (window.history.state.back === '/my/events') || (Boolean(this.isAdmin))
    },
    emailsSchema(){
      const schema = yup.object().shape({
        emailList: yup.array()
            .transform(function(value, originalValue) {
              if (this.isType(value) && value !== null) {
                return value;
              }
              return originalValue ? originalValue.split(/[\s,]+/) : [];
            })
            .required("At least one email is required")
            .of(yup.string().email(({ value }) => `${value} is an invalid email.`))
      });
      console.log(schema)
      return  schema
    },
    filesSchema(){
      console.log(this.fileTypes)
      const fileTypes = this.fileTypes
      const isCorrectFileType = (files) =>{
        let valid = true
        if(files){
          files.map(file => {
            const allowedFile =
                fileTypes.filter(t =>
                    t.fileType === file
                        .name.split(".").pop())[0]
            if (allowedFile?.fileSize === undefined)
              valid = false
          })
        return valid
        }
      }

      const isCorrectFileSize = (files) => {
        let valid = true
        if (files) {
          files.map(file => {
            const size = file.size / Math.pow(10, 6)
            const allowedFile =
                fileTypes.filter(t =>
                    t.fileType === file
                        .name.split(".").pop())[0]
            if (allowedFile?.fileSize === undefined)
              valid = false
            else {
              console.log(size)
              console.log(allowedFile.fileSize)
              if (size > allowedFile.fileSize) {
                valid = false
              }
            }

          })
        }
        return valid
      }
      return yup.object().shape({
        files: yup.array()
            .required()
            .test('is-correct-type', 'Files of an invalid type', isCorrectFileType)
            .test('is-correct-file', 'Files of an invalid type or too large', isCorrectFileSize)})
    }
  },

  methods: {
    ...mapActions({
      getEvent: 'event/getEvent',
      assignUser: 'event/assignToEvent',
      getEventFiles: 'file/getEventFiles',
      removeFile: 'file/removeFile',
      uploadFiles: 'file/uploadFiles',
      getAllowedFileExtensions: 'file/getAllowedFileTypes'
    }),
    ...mapMutations({
      clearEvent: 'event/clearEvent',
      clearBlobs: 'file/clearBlobs',
      clearErrors: 'clearErrors'
    }),
    async UploadFiles(){
      this.uploadFiles(this.event.id)
    },
    async getAllowedExtensions(){
      console.log(this.fileTypes)
      const allowedExtensions = [...(await this.fileTypes.map(t => t.fileType))].join(",.")
      console.log(allowedExtensions)
      return `.${allowedExtensions}`
    },
    async showDialog() {
      this.dialogVisible = true
    },
    async show(){
      const files = document.querySelector("#files")
      console.log(files)
      const accept = await this.getAllowedExtensions()
      files.setAttribute('accept', accept)
    },
    async showFileDialog(){
      this.fileDialogVisible = true
    },
    async showAssignDialog(){
      this.assignDialogVisible = true
    },
    async ScaleImage(e){
      let el = e.target

      if(el.width > el.height)
        el.classList.toggle('max__width')
      else
        el.classList.toggle('max__height')

      el.classList.toggle('image__max')
      el.parentNode.classList.toggle('dialog')
      el.parentNode.classList.toggle('absolute')
      el.nextElementSibling.classList.toggle('close')
    },
    async AssignUsers(){
      console.log(this.userEmails)
      var emails = this.userEmails.split(', ')
      await emails.forEach(async email => {
        await this.assignUser([email, this.event.id])
      })
      this.userEmails = ''
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
      else
        return 'file'
    },
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
}

.max__width{
  width: 75%;
  height: auto;
}

.max__height{
  height: 75%;
  width: auto;
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
  font-size: 14px;
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

.file__form, .assign__form{
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