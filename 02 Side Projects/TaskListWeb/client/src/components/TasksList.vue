<template>
  <div>
    <div class="header">
      <h1>Tasks</h1>
      <router-link
        tag="button"
        class="btn addNewTask"
        :to="{ name: 'AddTask', params: { folderID: this.folderId } }"
        v-if="!isLoading && this.folderId != 2 && this.folderId != 3"
        >Add New Task</router-link
      >
      <button
        class="btn btn-cancel deleteFolder"
        v-if="
          !isLoading &&
          this.folderId != 1 &&
          this.folderId != 2 &&
          this.folderId != 3
        "
        v-on:click="deleteFolder"
      >
        Delete Folder
      </button>
      <button
        class="btn renameFolder"
        v-if="
          !isLoading &&
          !showRenameFolder &&
          this.folderId != 1 &&
          this.folderId != 2 &&
          this.folderId != 3
        "
        v-on:click="showRenameFolder = !showRenameFolder"
      >
        Rename Folder
      </button>
    </div>
    <div class="renaming">
      <form v-if="showRenameFolder">
        Folder Name:
        <input
          type="text"
          class="form-control"
          maxlength="50"
          required
          v-model="folder.folderName"
        />
        <button class="btn btn-submit" v-on:click="renameFolder">Save</button>
        <button class="btn btn-cancel" v-on:click="resetRenameFolder">
          Cancel
        </button>
      </form>
    </div>
    <div class="loading" v-if="isLoading">
      <img src="../assets/loading-gif.gif" />
    </div>
    <div v-else>
      <div class="status-message error" v-show="errorMsg !== ''">
        {{ errorMsg }}
      </div>
      <div v-if="!showRenameFolder" class="folders">
        <span
          ><strong>Show completed? </strong
          ><input type="checkbox" id="show-completed" v-model="showCompleted"
        /></span>
        <div><hr /></div>
        <folder-column
          taskName="this.folderName"
          :tasks="this.filteredTasks"
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
      showRenameFolder: false,
      showCompleted: true,
      errorMsg: "",
      folder: {
        folderId: Number,
        folderName: "",
      },
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
          "Are you sure that you want to delete this folder? There is no undo."
        )
      ) {
        if (this.$store.state.folderTasks.length > 0) {
          alert(
            "This folder contains tasks and cannot be deleted. Either move these tasks to another folder or delete these tasks before deleting this folder."
          );
        } else {
          taskService
            .deleteFolder(this.folderId)
            .then((response) => {
              if (response.status === 204) {
                alert("Folder successfully deleted.");
                this.$router.push("/");  //TODO: fix this route - after folder delete refresh the folders list
              }
            })
            .catch((error) => {
              if (error.response) {
                this.errorMsg =
                  "Error deleting folder. Response from server was " +
                  error.response.statusText +
                  ".";
              } else if (error.request) {
                this.errorMsg =
                  "Error deleting folder. Could not reach server.";
              } else {
                this.errorMsg =
                  "Error deleting new folder. Request could not be created.";
              }
            });
        }
      }
    },
    resetRenameFolder() {
      this.folder = {
        folderId: Number,
        folderName: "",
      };
      this.showRenameFolder = false;
    },
    renameFolder() {
      if (this.folder.folderName.length === 0) {
        alert("Folder Name is required.");
      } else if (this.folder.folderName.length > 50) {
        alert("Max length for Folder Name is 50 characters.");
      } else {
        this.folder.folderId = parseInt(this.folderId);
        taskService
          .updateFolder(this.folder)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/folder/${this.folderId}`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "updating");
          });
      }
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " task. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg = "Error " + verb + " task. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " task. Request could not be created.";
      }
    },
  },
  created() {
    this.folderId = this.$route.params.id;
    this.retrieveTasks();
  },
  computed: {
    filteredTasks() {
      if (this.showCompleted) {
        return this.$store.state.folderTasks;
      } else {
        return this.$store.state.folderTasks.filter((task) => {
          return task.isComplete === false;
        });
      }
    },
  },
  /*  important() {
      return this.$store.state.folderTasks.filter(
        (task) => task.isImportant === true
      );
    },
    recurring() {
      return this.$store.state.folderTasks.filter(
        (task) => task.recurrenceName != "none"
      );
    },
  }, */
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
.btn.renameFolder {
  color: #fff;
  background-color: #294857;
  border-color: #294857;
}
.header {
  display: flex;
  align-items: center;
}
.header h1 {
  flex-grow: 1;
}
</style>
