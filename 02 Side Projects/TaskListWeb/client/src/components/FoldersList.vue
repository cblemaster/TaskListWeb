<template>
  <div id="sideNav">
    <p>My Task Folders</p>
    <div class="folders">
      <div class="status-message error" v-show="errorMsg !== ''">
        {{ errorMsg }}
      </div>
      <div class="loading" v-if="isLoading">
        <img src="../assets/ping_pong_loader.gif" />
      </div>
      <button>Sort</button>
      <p
        v-for="folder in this.$store.state.folders"
        v-bind:key="folder.folderId"
      >
        <a href="#" v-on:click="setSelectedFolderId(folder.folderId)">{{
          folder.folderName
        }}</a>
        &nbsp;<button v-if="selectedFolderId === folder.folderId">
          Rename
        </button>
        <button v-if="selectedFolderId === folder.folderId">Delete</button>
      </p>
      <button>Add folder</button>
    </div>
  </div>
</template>

<script>
import taskService from "../services/TaskService.js";

export default {
  data() {
    return {
      isLoading: true,
      errorMsg: "",
      selectedFolderId: Number
    };
  },
  created() {
    this.retrieveFolders();
  },
  methods: {
    retrieveFolders() {
      taskService.getFolders().then((response) => {
        this.$store.commit("SET_FOLDERS", response.data);
        this.isLoading = false;
      });
    },
    setSelectedFolderId(id) {
      this.selectedFolderId = id; 
      if (id === 2) {
        taskService.getTasksRecurring().then((response) =>{
        this.$store.commit("SET_TASKS", response.data);
      });
      }
      else if (id === 3) {
        taskService.getTasksImportant().then((response) =>{
        this.$store.commit("SET_TASKS", response.data);
      });
      }
      else {
          taskService.getTasks(this.selectedFolderId).then((response) =>{
        this.$store.commit("SET_TASKS", response.data);
      });
           }
    },
}
}
</script>

<style scoped>
/* div#sideNav {
  height: 100%;
  width: 20%;
  position: fixed;
  z-index: 1;
  left: 0;
  top: 0;
  padding-top: 20px;
  padding-bottom: 20px;
  overflow-x: hidden;
  border-right: solid lightgrey 1px;
}

h1 {
  text-align: center;
}

.folders {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}

.loading {
  flex: 3;
} */
</style>