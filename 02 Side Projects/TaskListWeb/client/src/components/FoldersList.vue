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
      <button type="button" name="sortFoldersAsc" v-on:click="sortFoldersAsc()">
        Sort A-Z
      </button>
      <button
        type="button"
        name="sortFoldersDesc"
        v-on:click="sortFoldersDesc()"
      >
        Sort Z-A
      </button>
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
        <button
          type="button"
          name="deleteFolder"
          v-if="selectedFolderId === folder.folderId"
          v-on:click.prevent="deleteFolder(folder.folderId, folder.folderName)"
        >
          Delete
        </button>
      </p>
      <button
        type="button"
        name="addFolder"
        v-if="!isAdding"
        v-on:click="showAddFolderForm()"
      >
        Add folder
      </button>
      <form v-on:submit.prevent="submitAddFolderForm()" v-if="isAdding">
        <label for="folderName">Folder Name:</label>
        <input
          type="text"
          autocomplete="off"
          v-model="folderToAdd.folderName"
        />
        <button type="submit">Submit</button>
        <button type="cancel" v-on:click.prevent="cancelForm()">Cancel</button>
      </form>
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
      selectedFolderId: Number,
      isAdding: false,
      folderToAdd: {
        folderName: "",
      },
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
    sortFoldersAsc() {
      this.selectedFolderId = null;
      taskService.getFoldersSortAsc().then((response) => {
        this.$store.commit("SET_FOLDERS", response.data);
      });
    },
    sortFoldersDesc() {
      this.selectedFolderId = null;
      taskService.getFoldersSortDesc().then((response) => {
        this.$store.commit("SET_FOLDERS", response.data);
      });
    },
    setSelectedFolderId(id) {
      this.selectedFolderId = id;
      if (id === 2) {
        // selected folder = recurring
        taskService.getTasksRecurring().then((response) => {
          this.$store.commit("SET_TASKS", response.data);
        });
      } else if (id === 3) {
        // selected folder = important
        taskService.getTasksImportant().then((response) => {
          this.$store.commit("SET_TASKS", response.data);
        });
      } else {
        taskService.getTasks(this.selectedFolderId).then((response) => {
          this.$store.commit("SET_TASKS", response.data);
        });
      }
    },
    showAddFolderForm() {
      this.isAdding = true;
    },
    cancelForm() {
      this.folderToAdd.folderName = "";
      this.isAdding = false;
    },
    submitAddFolderForm() {
      // handle error if new folder name is blank or null
      if (
        this.folderToAdd.folderName === "" ||
        this.folderToAdd.folderName === null
      ) {
        alert("Folder name cannot be blank!");
        return;
      }
      // handle error if new folder name exceeds max length
      else if (this.folderToAdd.folderName.length > 50) {
        alert("Max length for folder name is 50 characters!");
        this.folderToAdd.folderName = "";
        return;
      }
      // handle error if new folder name already exists
      else {
        this.$store.state.folders.forEach((folder) => {
          if (folder.folderName === this.folderToAdd.folderName) {
            alert("Folder name already exists!");
            this.folderToAdd.folderName = "";
            return;
          }
        });
      }
      taskService
        .createFolder(this.folderToAdd)
        .then((response) => {
          if (response.status === 201) {
            location.reload();
          }
        })
        .catch(() => {
          alert("Error adding new folder...");
        });
    },
    deleteFolder(id, name) {
      // Tasks, Important, and Recurring folders cannot be deleted
      if (id === 1 || id === 2 || id === 3) {
        alert("This folder cannot be deleted!");
        id = 0;
        return;
      }
      // Folders that contain tasks cannot be deleted
      else {
        this.$store.state.tasks.forEach((task) => {
          if (task.folderName === name) {
            alert("This folder contains tasks and cannot be deleted!");
            id = 0;
            return;
          }
        });
      }
      if (id != 0) {
        if (
          confirm(
            "Are you sure you want to delete this folder? This action cannot be undone."
          )
        ) {
          taskService
            .deleteFolder(id)
            .then((response) => {
              if (response.status === 204) {
                alert("Folder deleted successfully!");
                location.reload();
              }
            })
            .catch(() => {
              alert("Error deleting folder...");
            });
        }
      }
    },
  },
};
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