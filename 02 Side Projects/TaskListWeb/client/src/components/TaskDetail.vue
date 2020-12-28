<template>
  <div id="sideNav">
    <p>My Tasks Detail</p>
    <div class="task">
      <div class="status-message error" v-show="errorMsg !== ''">
        {{ errorMsg }}
      </div>
      <div class="loading" v-if="isLoading">
        <p>No task selected...</p>
      </div>
      <p>Task Name: {{this.$store.state.task.taskName}}</p>
      <p>Due Date: {{this.$store.state.task.dueDate}}</p>
      <p>Reminder: {{this.$store.state.task.reminder}}</p>
      <p>Recurrence: {{this.$store.state.task.recurrenceName}}</p>
      <p>Is Complete: {{this.$store.state.task.isComplete}}</p>
      <p>Is Important: {{this.$store.state.task.isImportant}}</p>
      <p>Folder: {{this.$store.state.task.folderName}}</p>
      <p>Created Date: {{this.$store.state.task.createdDate}}</p>
      <button>Edit</button> <button>Delete</button>
    </div>
  </div>
</template>

<script>

export default {
  data() {
    return {
      isLoading: true,
      errorMsg: "",
    };
  },
  created() {
  },
  methods: {
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
