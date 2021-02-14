<template>
  <form v-on:submit.prevent="submitForm" class="taskForm">
    <div class="status-message error" v-show="errorMsg !== ''">
      {{ errorMsg }}
    </div>
    <div class="form-group">
      <label for="task-name" class="task-label">Task Name:</label>
      <textarea
        id="task-name"
        rows="5"
        cols="50"
        class="form-control"
        v-model="task.taskName"
        autocomplete="off"
        required
        maxlength="255"
      />
      <label for="due-date" class="task-label">Due Date:</label>
      <input
        id="due-date"
        type="date"
        class="form-control"
        v-model="task.dueDate"
      />
      <label for="reminder" class="task-label">Reminder:</label>
      <input
        id="reminder"
        type="date"
        class="form-control"
        v-model="task.reminder"
      />
      <label for="is-complete" class="task-label">Is Complete?:</label>
      <input
        id="is-complete"
        type="checkbox"
        class="form-control"
        v-model="task.isComplete"
      />
      <label for="is-important" class="task-label">Is Important?:</label>
      <input
        id="is-important"
        type="checkbox"
        class="form-control"
        v-model="task.isImportant"
      />
      <label for="recurrence" class="task-label">Recurrence:</label>
      <select id="recurrence" v-model="task.recurrenceName">
        <option value="none">none</option>
        <option value="daily">daily</option>
        <option value="weekdays">weekdays</option>
        <option value="weekly">weekly</option>
        <option value="monthly">monthly</option>
        <option value="annually">annually</option>
      </select>
      <label for="folder-name" class="task-label">Folder Name:</label>
      <select id="folder-name" v-model="task.folderName">
        <option
          v-for="folder in this.$store.state.folders"
          v-bind:key="folder.folderId"
          v-bind:value="folder.folderName"
          v-show="folder.folderId != 2 && folder.folderId != 3"
        >
          {{ folder.folderName }}
        </option>
      </select>
    </div>
    <button class="btn btn-submit">Submit</button>
    <button
      class="btn btn-cancel"
      v-on:click.prevent="cancelForm"
      type="cancel"
    >
      Cancel
    </button>
  </form>
</template>

<script>
import taskService from "../services/TaskService";

export default {
  name: "task-form",
  props: {
    taskID: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      task: {
        //taskId: Number,
        taskName: "",
        dueDate: "",
        reminder: "",
        isComplete: false,
        isImportant: false,
        folderName: "",
        recurrenceName: ""
      },
      errorMsg: "",
    };
  },
  methods: {
    getDateFromJson(dateValue) {
      return new Date(dateValue).toDateString();
    },
    submitForm() {
      const newTask = {
        //folderId: Number(this.$route.params.folderID),
        taskName: this.task.taskName,
        dueDate: this.task.dueDate,
        reminder: this.task.reminder,
        isComplete: this.task.isComplete,
        isImportant: this.task.isImportant,
        folderName: this.task.folderName,
        recurrenceName: this.task.recurrenceName
      };

      if (this.taskID === 0) {
        // add
        alert("Start Adding!");
        taskService
          .addTask(newTask)
          .then((response) => {
            if (response.status === 201) {
              this.$router.push(`/folder/${response.data.folderId}`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "adding");
          });
      } else {
        // update
        newTask.taskId = this.taskID;
        newTask.taskName = this.task.taskName,
        newTask.dueDate = this.task.dueDate,
        newTask.reminder = this.task.reminder,
        newTask.isComplete = this.task.isComplete,
        newTask.isImportant = this.task.isImportant,
        newTask.folderName = this.task.folderName,
        newTask.recurrenceName = this.task.recurrenceName
        taskService
          .updateTask(newTask)
          .then((response) => {
            if (response.status === 200) {
              this.$router.push(`/folder/${this.$route.params.folderID}`);
            }
          })
          .catch((error) => {
            this.handleErrorResponse(error, "updating");
          });
      }
    },
    cancelForm() {
      this.$router.push(`/folder/${this.$route.params.folderID}`);
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
    if (this.taskID != 0) {
      taskService
        .getTask(this.taskID)
        .then((response) => {
          this.task = response.data;
        })
        .catch((error) => {
          if (error.response && error.response.status === 404) {
            alert(
              "Task not available. This task may have been deleted or you have entered an invalid task ID."
            );
            this.$router.push("/");
          }
        });
    }
  },
};
</script>


<style>
.taskForm {
  padding: 10px;
  margin-bottom: 10px;
}
.form-group {
  margin-bottom: 10px;
  margin-top: 10px;
}
label {
  display: inline-block;
  margin-bottom: 0.5rem;
}
.form-control {
  display: block;
  width: 80%;
  height: 30px;
  padding: 0.375rem 0.75rem;
  font-size: 1rem;
  font-weight: 400;
  line-height: 1.5;
  color: #495057;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
}
textarea.form-control {
  height: 75px;
  font-family: Arial, Helvetica, sans-serif;
}
select.form-control {
  width: 20%;
  display: inline-block;
  margin: 10px 20px 10px 10px;
}
.btn-submit {
  color: #fff;
  background-color: #0062cc;
  border-color: #005cbf;
}
.btn-cancel {
  color: #fff;
  background-color: #dc3545;
  border-color: #dc3545;
}
.status-message {
  display: block;
  border-radius: 5px;
  font-weight: bold;
  font-size: 1rem;
  text-align: center;
  padding: 10px;
  margin-bottom: 10px;
}
.status-message.success {
  background-color: #90ee90;
}
.status-message.error {
  background-color: #f08080;
}
</style>
