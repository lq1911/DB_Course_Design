import { RouteRecordRaw } from 'vue-router'

const courierRoutes: Array<RouteRecordRaw> = [
  {
    path: '/courier',
    name: 'Courier',
    component: () => import('@/views/courier/CourierView.vue'),
    meta: { title: '骑手' }
  },
  {
    path: '/courier/account',
    name: 'AccountSettings',
    component: () => import('@/views/courier/AccountSettings.vue'),
    meta: { title: '账户设置' }
  }
]

export default courierRoutes
