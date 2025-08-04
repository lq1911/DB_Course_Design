// src/router/userRoutes.ts
import { RouteRecordRaw } from 'vue-router';
import UserHomeView from '@/views/user/Homepage/UserHome.vue'
import UserRecommendView from '@/views/user/Homepage/UserRecommend.vue'
import UserRestaurantsView from '@/views/user/Homepage/UserRestaurants.vue'
import UserOrderView from '@/views/user/Homepage/UserOrders.vue'

const userRoutes: Array<RouteRecordRaw> = [
  {
    path: '/home',
    name: 'Home',
    component: UserHomeView,
    meta: { title: '首页' }
  },
  {
    path: '/recommend',
    name: 'Recommend',
    component: UserRecommendView,
    meta: { title: '推荐' }
  },
  {
    path: '/restaurants',
    name: 'Restaurant',
    component: UserRestaurantsView,
    meta: { title: '商家' }
  },
  {
    path: '/orders',
    name: 'Order',
    component: UserOrderView,
    meta: { title: '订单' }
  }
]

export default userRoutes;
