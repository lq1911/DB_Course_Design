import { defineStore } from 'pinia'

interface UserInfo {
    userID: number;
}

export const useUserStore = defineStore('user', {
    state: (): UserInfo => ({
        userID: -1
    }),
    actions: {
        login(id: number) {
            this.userID = id
        },
        logout() {
            this.userID = -1
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