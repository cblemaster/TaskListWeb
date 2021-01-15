<template>
  <div>
    <div class="header">
      <h1>Tasks in folder</h1>
      <router-link
        tag="button"
        class="btn addNewTask"
        :to="{ name: 'AddTask', params: { folderID: this.folderId } }"
        v-if="!isLoading"
        >Add New Task</router-link
      >
      <button
        class="btn btn-cancel deleteFolder"
        v-if="!isLoading"
        v-on:click="deleteFolder"
      >
        Delete Folder
      </button>
    </div>
    <div class="loading" v-if="isLoading">
      <img src="../assets/ping_pong_loader.gif" />
    </div>
    <div v-else>
      <div class="status-message error" v-show="errorMsg !== ''">
        {{ errorMsg }}
      </div>
      <div class="folders">
        <folder-column
          taskName="this.folderName"
          :tasks="this.$store.state.folderTasks"
          :folderID="this.folderId"
        />
      </div>
    </div>
    <div class="folder-actions" v-if="!isLoading">
      <router-link to="/">Back to Folders</router-link>
    </div>
  </div>
</template>

<script>
import taskService from "../services/TaskService";
import FolderColumn from "@/components/FolderColumn";

export default {
  name: "tasks-list",
  components: {
    FolderColumn,
  },
  data() {
    return {
      taskName: "",
      folderId: 0,
      isLoading: true,
      errorMsg: "",
    };
  },
  methods: {
    retrieveTasks() {
      if (this.folderId === 2) {
        // recurring tasks
        taskService
          .getTasksRecurring(this.folderId)
          .then((response) => {
            this.taskName = response.data.taskName;
            this.$store.commit("SET_FOLDER_TASKS", response.data);
            this.isLoading = false;
          })
          .catch((error) => {
            if (error.response && error.response.status === 404) {
              alert(
                "Folder tasks not available. This folder may have been deleted or you have entered an invalid folder ID."
              );
              this.$router.push("/");
            }
          });
      } else if (this.folderId === 3) {
        // important tasks
        taskService
          .getTasksImportant()
          .then((response) => {
            this.taskName = response.data.taskName;
            this.$store.commit("SET_FOLDER_TASKS", response.data);
            this.isLoading = false;
          })
          .catch((error) => {
            if (error.response && error.response.status === 404) {
              alert(
                "Folder tasks not available. This folder may have been deleted or you have entered an invalid folder ID."
              );
              this.$router.push("/");
            }
          });
      } else {
        taskService
          .getTasksByFolder(this.folderId)
          .then((response) => {
            this.taskName = response.data.taskName;
            this.$store.commit("SET_FOLDER_TASKS", response.data);
            this.isLoading = false;
          })
          .catch((error) => {
            if (error.response && error.response.status === 404) {
              alert(
                "Folder tasks not available. This folder may have been deleted or you have entered an invalid folder ID."
              );
              this.$router.push("/");
            }
          });
      }
    },
    deleteFolder() {
      if (
        confirm(
          "Are you sure that you want to delete this folder and all tasks? There is no undo."
        )
      ) {
        taskService
          .deleteFolder(this.folderId)
          .then((response) => {
            if (response.status === 200) {
              alert("Folder successfully deleted.");
              //this.$store.commit("DELETE_FOLDER", this.folderId);
              this.$router.push("/");
            }
          })
          .catch((error) => {
            if (error.response) {
              this.errorMsg =
                "Error deleting folder. Response from server was " +
                error.response.statusText +
                ".";
            } else if (error.request) {
              this.errorMsg = "Error deleting folder. Could not reach server.";
            } else {
              this.errorMsg =
                "Error deleting new folder. Request could not be created.";
            }
          });
      }
    },
  },
  created() {
    this.folderId = this.$route.params.id;
    this.retrieveTasks();
  },
  computed: {
    important() {
      return this.$store.state.folderTasks.filter(
        (task) => task.isImportant === true
      );
    },
    recurring() {
      return this.$store.state.folderTasks.filter(
        (task) => task.recurrenceName != "none"
      );
    },
  },
};
</script>

<style>
.folders {
  display: grid;
  grid-template-columns: ifr;
  grid-gap: 20px;
}
.folder-actions {
  text-align: center;
  padding: 20px 0;
}
.folder-actions a:link,
.folder-actions a:visited {
  color: blue;
  text-decoration: none;
}
.folder-actions a:hover {
  text-decoration: underline;
}
.btn.addNewTask {
  color: #fff;
  background-color: #508ca8;
  border-color: #508ca8;
}
.header {
  display: flex;
  align-items: center;
}
.header h1 {
  flex-grow: 1;
}
</style>
