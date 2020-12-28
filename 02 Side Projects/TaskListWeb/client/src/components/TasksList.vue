<template>
  <div id="sideNav">
    <p>My Tasks in Selected Folder</p>
    <div class="tasks">
      <div class="status-message error" v-show="errorMsg !== ''">
        {{ errorMsg }}
      </div>
      <div class="loading" v-if="isLoading">
        <p>No folder selected...</p>
      </div>
      <button>Sort</button>
      <p
        v-for="task in this.$store.state.tasks"
        v-bind:key="task.taskId"
      >
        <a href="#" v-on:click="setSelectedTaskId(task.taskId)">{{ task.taskName }}</a>
        &nbsp;<button v-if="selectedTaskId===task.taskId">Edit</button> <button v-if="selectedTaskId===task.taskId">Delete</button>
      </p>
      <button>Add task</button>
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
      selectedTaskId: Number
    };
  },
  created() {
  },
  methods: {
    setSelectedTaskId(id) {
      this.selectedTaskId = id; 
      taskService.getTask(this.selectedTaskId).then((response) =>{
        this.$store.commit("SET_TASK", response.data);
      });
    }
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
