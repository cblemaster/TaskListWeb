<template>
  <div class="folder">
    <h2>{{ folderName }}</h2>
    <div class="tasks">
      <div
        class="task"
        v-for="task in tasks"
        v-bind:key="task.taskId"
        v-on:click="viewTaskDetails(task.taskId)"
      >
        <div class="header">
          <h3>{{ task.taskName }}</h3>
        </div>
        <div class="footer">
          <p>Due Date: {{ task.dueDate }}</p>
          <p>Is Important?: {{ task.isImportant }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "folder-column",
  props: ["taskName", "tasks", "folderID"],
  methods: {
    viewTaskDetails(taskID) {
      this.$router.push(`/folder/${this.folderID}/task/${taskID}`);
    },
  },
};
</script>

<style scoped>
.folder {
  background-color: #f7fafc;
  border-radius: 10px;
  padding: 0 20px 20px 20px;
}
.task {
  background: #fff;
  border-radius: 0.25rem;
  padding: 10px;
  border: 1px;
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06);
  margin-bottom: 10px;
  cursor: pointer;
}
.task:last-child {
  margin-bottom: 0px;
}
.task h3 {
  margin-top: 0px;
  font-size: 0.875rem;
}
.task .header {
  display: flex;
  justify-content: space-between;
}
.task .header img {
  border-radius: 9999px;
  width: 32px;
  align-self: flex-start;
}
.task .footer {
 
  margin: 20px 0 10px 0;
  font-size: 0.75rem;
}
</style>
