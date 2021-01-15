import axios from 'axios';

const http = axios.create({
  baseURL: "https://localhost:44312"
});

export default {

  getFolders() {
    return http.get('/folder');
  },

  getTask(taskID) {
    return http.get(`/task/${taskID}`);
  },

  addTask(task) {
    return http.post('/task', task);
  },

  updateTask(task) {
    return http.put(`/task/${task.taskId}`, task);
  },

  deleteTask(taskID) {
    return http.delete(`/task/${taskID}`);
  },

  addFolder(folder) {
    return http.post('/folder', folder);
  },

  deleteFolder(folderID) {
    return http.delete(`/folder/${folderID}`);
  },

  updateFolder(folder) {
    return http.put(`/folder/${folder.folderId}`, folder);
  },

  getTasksByFolder(folderID) {
    return http.get(`/task/folder/${folderID}`);
  },

  getTasksImportant() {
    return http.get(`/task/important`);
  },

  getTasksRecurring() {
    return http.get(`/task/recurring`);
  }

}