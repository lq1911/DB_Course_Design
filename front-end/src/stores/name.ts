import { defineStore } from 'pinia'
import { ref } from 'vue'

export const getProjectName = defineStore('name', () => {
  const projectName = ref('名字暂定')

  return {
    projectName
  }
})
