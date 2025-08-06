import { RouteRecordRaw } from 'vue-router'
import StoreOrder from '@/views/user/StoreDetail/UserOrder.vue'
import StoreComment from '@/views/user/StoreDetail/UserComment.vue'
import StoreInfo from '@/views/user/StoreDetail/InfoDetail.vue'

const inStoreRoutes: Array<RouteRecordRaw> = [
    {
        path: '/store',
        component: StoreOrder,
    },
    {
        path: '/store/comment',
        component: StoreComment
    },
    {
        path: '/store/info',
        component: StoreInfo
    }

];

export default inStoreRoutes;