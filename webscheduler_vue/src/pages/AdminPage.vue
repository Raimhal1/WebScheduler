<template>
  <div class="page">
    <div class="users">
      <user-list :users="users">
      </user-list>
    </div>
    <div class="types">
      <allowed-extension-list :file-types="fileTypes"></allowed-extension-list>
    </div>
    <div class="btns">
      <my-button @click="$router.back()"> Back </my-button>
      <div class="main">
        <report
            :length="length"
            :root="root"
            :reportType="reportType"
        >
          Next month report
        </report>
        <my-button @click="showFileTypeDialog">
          Add file type
        </my-button>
      </div>
    </div>
    <my-dialog v-model:show="fileTypeDialogVisible">
      <file-type-form>
      </file-type-form>
    </my-dialog>
  </div>
</template>
<script>
import Report from "@/components/Report";
import UserList from "../components/UserList";
import {mapActions, mapMutations, mapState} from "vuex";
import AllowedExtensionList from "@/components/AllowedExtensionList";
import FileTypeForm from "../components/FileTypeForm";
import MyButton from "../components/UI/MyButton";

export default {
  name: "AdminPage",
  components: {MyButton, FileTypeForm, AllowedExtensionList, UserList, Report},
  data(){
    return{
      fileTypeDialogVisible: false,
      fileTypeUpdateDialogVisible: false,
      modified: false,
      length: 1,
      id: null,
      reportType: 'next-month-report',
      root: 'events'
    }
  },
  mounted() {
    this.getFileTypes()
    this.getUsers()
  },
  beforeUnmount() {
    this.clearErrors()
  },
  methods: {
    ...mapMutations({
      clearFileType: 'file/clearFileType',
      clearErrors: 'clearErrors'
    }),
    ...mapActions({
      getUsers: 'user/getUsers',
      getFileTypes: 'file/getAllowedFileTypes',
      addFileType: 'file/addFileType',

    }),
    async showFileTypeDialog(){
      this.clearFileType()
      this.fileTypeDialogVisible = true
    }

  },
  computed: {
    ...mapState({
      fileTypes: state => state.file.fileTypes,
      file: state => state.file.file,
      users: state => state.user.users,

    }),
  },
}
</script>

<style scoped>
.users, types{
  height: fit-content;
  max-height: 80vh;
  overflow-y: auto;
}

.btns{
  display: flex;
  justify-content: space-between;
}

.main {
  display: flex;
  white-space: nowrap;
  gap: 20px;
}
.page{
  display: flex;
  flex-direction: column;
  gap: 30px
}
</style>