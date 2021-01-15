<template>
  <div>
    <div class="loading" v-if="isLoading">
      <img src="../assets/ping_pong_loader.gif" />
    </div>
    <div v-else>
      <h1>{{ task.taskName }}</h1>
      <p>Due Date: {{ task.dueDate }}</p>
      <p>Reminder: {{ task.reminder }}</p>
      <p>Is Complete?: {{ task.isComplete }}</p>
      <p>Is Important?: {{ task.isImportant }}</p>
      <p>Recurrence Name: {{ task.recurrenceName }}</p>
      <p>Task Id: {{ task.taskId }}</p>
      <p>Created Date: {{ task.createdDate }}</p>
      <p>Folder Name: {{ task.folderName }}</p>
      <p>Folder Id: {{ task.folderId }}</p>
      <p>Recurrence Id: {{ task.recurrenceId }}</p>
      <router-link
        tag="button"
        :to="{ name: 'EditTask', params: { taskID: $route.params.taskID } }"
        class="btn editTask"
        >Edit Task</router-link
      >
      <button class="btn deleteTask" v-on:click="deleteTask">
        Delete Task
      </button>
      <div class="status-message error" v-show="errorMsg !== ''">
        {{ errorMsg }}
      </div>
    </div>

    <div class="folder-actions" v-if="!isLoading">
      <router-link
        :to="{ name: 'Folder', params: { id: $route.params.folderID } }"
        >Back to Folder</router-link
      >
    </div>
  </div>
</template>

<script>
import taskService from "../services/TaskService";
//import CommentsList from "@/components/CommentsList";

export default {
  name: "task-detail",
  /* components: {
    CommentsList
  }, */
  data() {
    return {
      isLoading: true,
      errorMsg: "",
    };
  },
  methods: {
    retrieveTask() {
      taskService
        .getTask(this.$route.params.taskID)
        .then((response) => {
          this.$store.commit("SET_CURRENT_TASK", response.data);
          this.isLoading = false;
        })
        .catch((error) => {
          if (error.response && error.response.status === 404) {
            alert(
              "Task not available. This task may have been deleted or you have entered an invalid task ID."
            );
            this.$router.push("/");
          }
        });
    },
    deleteTask() {
      if (
        confirm(
          "Are you sure you want to delete this task? This action cannot be undone."
        )
      ) {
        taskService
          .deleteTask(this.task.id)
          .then((response) => {
            if (response.status === 200) {
              alert("Task successfully deleted");
              this.$router.push(`/folder/${this.task.folderId}`);
            }
          })
          .catch((error) => {
            if (error.response) {
              this.errorMsg =
                "Error deleting task. Response received was '" +
                error.response.statusText +
                "'.";
            } else if (error.request) {
              this.errorMsg =
                "Error deleting task. Server could not be reached.";
            } else {
              this.errorMsg =
                "Error deleting task. Request could not be created.";
            }
          });
      }
    },
  },
  created() {
    this.retrieveTask();
  },
  computed: {
    task() {
      return this.$store.state.task;
    },
  },
};
</script>

<style>
.btn.editTask {
  color: #fff;
  background-color: #508ca8;
  border-color: #508ca8;
  margin-bottom: 10px;
}
.btn.deleteTask {
  color: #fff;
  background-color: #ef031a;
  border-color: #ef031a;
  margin-bottom: 10px;
}
</style>
