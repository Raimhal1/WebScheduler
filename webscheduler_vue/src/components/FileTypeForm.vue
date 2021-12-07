<template>
  <Form v-slot="{ handleSubmit }" :validation-schema="schema" as="div" class="type__form">
    <my-error-list :errors="errors"></my-error-list>
    <form @submit="handleSubmit($event, action)" method="post"  class="form" >
      File type : <my-field v-model="file.fileType" name="type"/>
      <my-error-message name="type" />
      File max size : <my-field v-model="file.fileSize" name="size"/>
      <my-error-message name="size" />
      <my-button type="submit">
        <slot name="submit__name"></slot>
      </my-button>
    </form>
  </Form>
</template>

<script>
import {mapActions, mapState} from "vuex";
import {Form} from 'vee-validate'

import * as yup from 'yup'
import MyField from "@/components/UI/MyField";
import MyErrorMessage from "@/components/UI/MyErrorMessage";
import MyErrorList from "./UI/MyErrorList";
export default {
  name: "FileTypeForm",
  components: {Form, MyField, MyErrorList, MyErrorMessage},
  props: {
    modified:{
      type: Boolean,
      default: false
    },
  },
  computed: {
    ...mapState({
      file: state => state.file.file,
      errors: state => state.errors
    }),
    schema() {
      return  yup.object().shape({
        type: yup.string().max(8).required().label('File type'),
        size: yup.number().typeError("File max size is a number field").min(1).max(50).required().label('File max size'),
      })
    },
  },
  methods: {
    ...mapActions({
      createFileType: 'file/addFileType',
      updateFileType: 'file/updateFileType'
    }),
    async action(){
      if(this.modified)
        this.updateFileType()
      else
        this.createFileType()
    }
  },

}
</script>

<style scoped>
.type__form{
  padding: 10px;
  border: 2px solid #0c20a1;
  border-radius: 5px;
}
</style>