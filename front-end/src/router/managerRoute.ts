import { RouteRecordRaw } from 'vue-router'

const managerRoutes: Array<RouteRecordRaw> = [
    {
        path: '/manager',
        name: 'Manager',
        component: () => import('@/views/manager/ManagerView.vue'),
        meta: { title: '管理中心' }
    },
];

export default managerRoutes;