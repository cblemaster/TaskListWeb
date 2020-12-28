import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    folders: [],
    tasks: [],
    task: {}
  },
  mutations: {
    SET_FOLDERS(state, data) {
      state.folders = data;
    },
    SET_TASKS(state, data) {
      state.tasks = data;
    },    
    SET_TASK(state, data) {
      state.task = data;
    }
  },
  actions: {
  },
  modules: {
  }
})