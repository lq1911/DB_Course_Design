import { defineStore } from 'pinia'

interface UserInfo {
    userID: number | null;
}

export const useUserStore = defineStore('user', {
    state: (): UserInfo => ({
        userID: null
    }),
    actions: {
        login(id: number) {
            this.userID = id
        },
        logout() {
            this.userID = null
        },
        getUserID() {
            return this.userID
        }
    },
    persist: {
        storage: localStorage,
        key: 'user'   // 持久化时的 key
    },
})