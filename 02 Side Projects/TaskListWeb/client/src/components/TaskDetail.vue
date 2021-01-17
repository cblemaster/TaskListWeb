<template>
  <div>
    <div class="loading" v-if="isLoading">
      <img src="../assets/ping_pong_loader.gif" />
    </div>
    <div v-else>
      <h1>{{ task.taskName }}</h1>
      <p class="task-label">Due Date: <span class="task-display">{{ getDateFromJson(task.dueDate) }}</span></p>
      <p class="task-label">Reminder: <span class="task-display">{{ getDateFromJson(task.reminder) }}</span></p>
      <p class="task-label">Is Complete?: <span class="task-display">{{ task.isComplete }}</span></p>
      <p class="task-label">Is Important?: <span class="task-display">{{ task.isImportant }}</span></p>
      <p class="task-label">Recurrence: <span class="task-display">{{ task.recurrenceName }}</span></p>
      <p class="task-label">Created Date: <span class="task-display">{{ getDateFromJson(task.createdDate) }}</span></p>
      <p class="task-label">Folder Name: <span class="task-display">{{ task.folderName }}</span></p>
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

export default {
  name: "task-detail",
  data() {
    return {
      isLoading: true,
      errorMsg: "",
    };
  },
  methods: {
    getDateFromJson(dateValue) {
      return new Date(dateValue).toDateString();
    },
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
          .deleteTask(this.task.taskId)
          .then((response) => {
            if (response.status === 204) {
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
.task-label {
  font-weight: bold;
}
.task-display {
  color: blue;
}
</style>
