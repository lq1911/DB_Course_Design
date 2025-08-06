import { defineStore } from 'pinia'
import type { StoreInfo } from '@/api/store_info'
import { getStoreInfo } from '@/api/store_info'

interface State{
    storeInfo: StoreInfo | null
}

export const loadStoreInfo = defineStore('storeInfo', {
    state: (): State => ({
        storeInfo: null
    }),

    actions: {
        async fetchStoreInfo(id: string) {
            try {
                const data = await getStoreInfo(id)
                this.storeInfo = data
            } catch (error) {
                console.error('获取店铺信息失败', error)
            }
        }
    }
})
    