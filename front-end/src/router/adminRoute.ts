import { RouteRecordRaw } from 'vue-router'

const managerRoutes: Array<RouteRecordRaw> = [
  {
    path: '/admin',
    name: 'Admin',
    component: () => import('@/views/administrator/AdminView.vue'),
    meta: { title: '管理员' }
  },
];

export default managerRoutes;