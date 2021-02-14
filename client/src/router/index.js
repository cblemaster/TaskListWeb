import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Folder from '../views/Folder.vue'
import Task from '../views/Task.vue'
import AddTask from '../views/AddTask.vue'
import EditTask from '../views/EditTask.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/folder/:id',
    name: 'Folder',
    component: Folder
  },
  {
    path: '/folder/:folderID/task/:taskID',
    name: 'Task',
    component: Task
  },
  {
    path: '/folder/:folderID/task/create',
    name: 'AddTask',
    component: AddTask
  },
  {
    path: '/folder/:folderID/task/:taskID/edit',
    name: 'EditTask',
    component: EditTask
  }
  // May need to add a route (and view, and component) for renaming (editing) folder
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router