<template>
  <div>
    <h1>Edit Task</h1>
    <task-form v-bind:taskID="parseInt($route.params.taskID)" />
  </div>
</template>

<script>
import TaskForm from "@/components/TaskForm";

export default {
  components: {
    TaskForm,
  },
};
</script>
