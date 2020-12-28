<template>
  <div id="main" class="container">
    <div id="header">
      <header-component />
    </div>
    <div id="folders-list">
      <folders-list />
    </div>
    <div id="tasks-list">
      <tasks-list />
    </div>
    <div id="task-details">
      <task-detail />
    </div>
    <div id="footer">
      <footer-component />
    </div>
  </div>
</template>

<script>
import FoldersList from "./components/FoldersList.vue";
import TasksList from "./components/TasksList.vue";
import TaskDetail from "./components/TaskDetail.vue";
import HeaderComponent from "./components/HeaderComponent.vue";
import FooterComponent from "./components/FooterComponent.vue";

export default {
  components: {
    FoldersList,
    TasksList,
    TaskDetail,
    HeaderComponent,
    FooterComponent,
  },
};
</script>

<style>
#main {
  display: grid;
  grid-template-columns: 2fr 2fr 3fr;
  grid-template-areas:
    "header header header"
    "folders-list tasks-list task-details"
    "footer footer footer";
}

#header {
  grid-area: header;
  height: 25vh;
}

#folders-list {
  grid-area: folders-list;
  height: 70vh;
}

#tasks-list {
  grid-area: tasks-list;
}

#task-details {
  grid-area: task-details;
}

#footer {
  grid-area: footer;
  height: 5vh;
}

/* body {
  font-family: system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto,
    'Helvetica Neue', Arial, 'Noto Sans', sans-serif, 'Apple Color Emoji',
    'Segoe UI Emoji', 'Segoe UI Symbol', 'Noto Color Emoji';
}
.container {
  width: 100%;
  max-width: 1100px;
  margin: 0 auto;
}
.loading {
  display: flex;
  justify-content: center;
  align-items: center;
}
.routerView {
  width: 79%;
  position: absolute;
  right: 0;
}
.btn {
  display: inline-block;
  font-weight: 400;
  color: #212529;
  text-align: center;
  vertical-align: middle;
  border: 1px solid transparent;
  padding: .375rem .75rem;
  margin-right: 10px;
  margin-bottom: 5px;
  font-size: 1rem;
  line-height: 1.5;
  border-radius: .25rem;
  cursor: pointer;
}  */
</style>
