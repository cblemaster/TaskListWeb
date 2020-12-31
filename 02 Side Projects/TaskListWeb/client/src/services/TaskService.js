import axios from 'axios';

const http = axios.create({
  baseURL: "https://localhost:44312"
});

export default {

  getFolders() {
    return http.get(`/folder`);  
  },
  getTasks(folderId) {
    return http.get(`/task/folder/${folderId}`);
  },
  getTask(taskId) {
    return http.get(`/task/${taskId}`);
  },
  getTasksImportant() {
    return http.get(`/task/important`);
  },
  getTasksRecurring() {
    return http.get(`/task/recurring`);
  },
  createFolder(folderToCreate) {
    return http.post(`/folder`, folderToCreate);
  },
  getFoldersSortAsc() {
    return http.get(`/folder/asc`);
  },
  getFoldersSortDesc() {
    return http.get(`/folder/desc`);
  },
  deleteFolder(folderId) {
    return http.delete(`/folder/${folderId}`);
  }
}