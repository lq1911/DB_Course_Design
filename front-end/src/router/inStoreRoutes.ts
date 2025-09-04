import { RouteRecordRaw } from 'vue-router'
import StoreOrder from '@/views/user/StoreDetail/UserOrder.vue'
import StoreComment from '@/views/user/StoreDetail/UserComment.vue'
import StoreInfo from '@/views/user/StoreDetail/InfoDetail.vue'
import StoreLayout from '@/views/user/StoreDetail/StoreLayout.vue'

const inStoreRoutes: Array<RouteRecordRaw> = [
    {
        path: '/store/:id',
        name: 'InStore',
        redirect: (to) => `/store/${to.params.id}/order`,
    },
    {
        path: '/store/:id',
        component: StoreLayout,
        children: [
            {
                path: 'order',
                name: 'StoreOrder',
                component: StoreOrder,
                meta: {title: '点单'}
            },
            {
                path: 'comment',
                name: 'StoreComment',
                component: StoreComment,
                meta: {title: '商家评价'}
            },
            {
                path: 'info',
                name: 'StoreInfo',
                component: StoreInfo,
                meta: {title: '商家信息'}
            },
        ]
    },
];

export default inStoreRoutes;