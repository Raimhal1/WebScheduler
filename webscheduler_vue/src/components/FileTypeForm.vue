<template>
    <form method="post" @submit.prevent class="type__form" >
      File type : <my-input type="text" v-model="file.fileType"/>
      File max size : <my-input type="number" min="1" max="50" placeholder="1" v-model="file.fileSize"/>
      <my-button
          @click="updateFileType"
          v-if="modified"
      >
        Update
      </my-button>
      <my-button
          @click="createFileType"
          v-else
      >
        Create
      </my-button>
    </form>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
  name: "FileTypeForm",
  props: {
    modified:{
      type: Boolean,
      default: false
    },
  },
  computed: {
    ...mapState({
      file: state => state.file.file,
    }),
  },
  methods: {
    ...mapActions({
      createFileType: 'file/addFileType',
      updateFileType: 'file/updateFileType'
    }),
  },

}
</script>

<style scoped>
.type__form{
  display: flex;
  flex-direction: column;
  padding: 10px;
  border: 2px solid #0c20a1;
  border-radius: 5px;
}
</style>