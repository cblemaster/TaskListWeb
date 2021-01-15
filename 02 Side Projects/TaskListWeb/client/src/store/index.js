import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    folders: [],
    folderTasks: [],
    task: {
      taskId: '',
      taskName: '',
      dueDate: '',
      reminder: '',
      createdDate: '',
      isComplete: '',
      isImportant: '',
      folderName: '',
      recurrenceName: '',
      folderId: '',
      recurrenceId: ''
    }
  },
  mutations: {
    SET_FOLDERS(state, data) {
      state.folders = data;
    },
    SET_FOLDER_TASKS(state, data) {
      state.folderTasks = data;
    },
    SET_CURRENT_TASK(state, data) {
      state.task = data;
    }/* ,
    DELETE_BOARD(state, boardIdToDelete) {
      state.boards = state.boards.filter((board) => {
        return board.id !== boardIdToDelete;
      });
    } */
  },
  actions: {
  },
  modules: {
  }
})
