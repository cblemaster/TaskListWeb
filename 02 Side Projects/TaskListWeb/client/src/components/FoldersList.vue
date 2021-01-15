<template>
  <div id="sideNav">
    <h1>Task Folders</h1>
    <div class="folders">
      <div class="status-message error" v-show="errorMsg !== ''">
        {{ errorMsg }}
      </div>
      <div class="loading" v-if="isLoading">
        <img src="../assets/ping_pong_loader.gif" />
      </div>
      <router-link
        :to="{ name: 'Folder', params: { id: folder.folderId } }"
        class="folder"
        v-for="folder in this.$store.state.folders"
        v-bind:key="folder.folderId"
        v-else
        tag="div"
      >
        {{ folder.folderName }}
      </router-link>
      <button
        class="btn addFolder"
        v-if="!isLoading && !showAddFolder"
        v-on:click="showAddFolder = !showAddFolder"
      >
        Add Folder
      </button>
      <form v-if="showAddFolder">
        Folder Name:
        <input
          type="text"
          class="form-control"
          v-model="newFolder.folderName"
        />
        Background Color:
        <input
          type="text"
          class="form-control"
          v-model="newFolder.backgroundColor"
        />
        <button class="btn btn-submit" v-on:click="saveNewFolder">Save</button>
        <button class="btn btn-cancel" v-on:click="resetAddFolder">
          Cancel
        </button>
      </form>
    </div>
  </div>
</template>

<script>
import taskService from "../services/TaskService";

export default {
  data() {
    return {
      isLoading: true,
      showAddFolder: false,
      newFolder: {
        folderName: "",
        backgroundColor: this.randomBackgroundColor(),
      },
      errorMsg: "",
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

        if (
          this.$route.name == "Home" &&
          response.status === 200 &&
          response.data.length > 0
        ) {
          this.$router.push(`/folder/${response.data[0].folderId}`);
        }
      });
    },
    saveNewFolder() {
      alert("begin save new folder");
      this.isLoading = true; //show the ping pong
      taskService
        .addFolder(this.newFolder)
        .then((response) => {
          if (response.status === 201) {
            //refresh the list of all of the folders
            this.retrieveFolders();
            //stop showing the form
            //reset the new folder back to blank
            this.resetAddFolder();
            // this.isLoading = false;
          }
        })
        .catch((error) => {
          if (error.response) {
            this.errorMsg =
              "Error creating folder. Response from server was " +
              error.response.statusText +
              ".";
          } else if (error.request) {
            this.errorMsg = "Error creating folder. Could not reach server.";
          } else {
            this.errorMsg =
              "Error creating new folder. Request could not be created.";
          }
          // this.isLoading = false;
        })
        .finally(() => {
          this.isLoading = false;
        });

      alert("at the end of save new folder");
    },
    resetAddFolder() {
      this.newFolder = {
        folderName: "",
        backgroundColor: this.randomBackgroundColor(),
      };
      this.showAddFolder = false;
    },
    randomBackgroundColor() {
      return "#" + this.generateHexCode();
    },
    generateHexCode() {
      var bg = Math.floor(Math.random() * 16777215).toString(16);
      if (bg.length !== 6) bg = this.generateHexCode();
      return bg;
    },
  },
};
</script>

<style scoped>
div#sideNav {
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
.folder {
  color: #f7fafc;
  background-color: blue;
  border-radius: 10px;
  padding: 40px;
  flex: 1;
  margin: 10px;
  text-align: center;
  cursor: pointer;
  width: 60%;
}
.addFolder {
  color: #f7fafc;
  border-radius: 10px;
  background-color: #28a745;
  font-size: 16px;
  width: 60%;
  margin: 10px;
  padding: 20px;
  cursor: pointer;
}
.form-control {
  margin-bottom: 10px;
}
.btn {
  margin-bottom: 35px;
}
.loading {
  flex: 3;
}
.folder:hover:not(.router-link-active),
.addFolder:hover {
  font-weight: bold;
}
.router-link-active {
  font-weight: bold;
  border: solid blue 5px;
  background-color: green;
}
</style>
