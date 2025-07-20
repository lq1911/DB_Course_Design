// src/router/index.ts


import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import LoginView from '../views/login/LoginView.vue'
import UserHomeView from '../views/user/UserHome.vue'
import UserRecommendView from '@/views/user/UserRecommend.vue'
import UserRestaurantsView from '@/views/user/UserRestaurants.vue'
import UserOrderView from '@/views/user/UserOrders.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    // redirect: '/login'
    redirect: '/home'
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginView
  },
  {
    path: '/home',
    name: 'Home',
    component: UserHomeView
  },
  {
    path: '/recommend',
    name: 'Recommend',
    component: UserRecommendView
  },
  {
    path: '/restaurants',
    name: 'Restaurant',
    component: UserRestaurantsView
  },
  {
    path: '/orders',
    name: 'Order',
    component: UserOrderView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL), 
  routes,
})

export default router