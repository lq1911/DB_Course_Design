// src/router/index.ts


import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'
import UserHomeView from '@/views/user/Homepage/UserHome.vue'
import UserRecommendView from '@/views/user/Homepage/UserRecommend.vue'
import UserRestaurantsView from '@/views/user/Homepage/UserRestaurants.vue'
import UserOrderView from '@/views/user/Homepage/UserOrders.vue'

import MerchantHomeView from '@/views/merchant/MerchantHomeView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    // redirect: '/login'
    // redirect: '/home'
    redirect: '/MerchantHome'
  },

  {
    path: '/MerchantHome',
    name: 'MerchantHome',
    component: MerchantHomeView,
    meta: {
      title: '商家'
    }
  },

  {
    path: '/login',
    name: 'Login',
    component: LoginView,
    meta: {
      title: '登录'
    }
  },
  {
    path: '/home',
    name: 'Home',
    component: UserHomeView,
    meta: {
      title: '首页'
    }
  },
  {
    path: '/recommend',
    name: 'Recommend',
    component: UserRecommendView,
    meta: {
      title: '推荐'
    }
  },
  {
    path: '/restaurants',
    name: 'Restaurant',
    component: UserRestaurantsView,
    meta: {
      title: '商家'
    }
  },
  {
    path: '/orders',
    name: 'Order',
    component: UserOrderView,
    meta: {
      title: '订单'
    }
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL), 
  routes,
})

router.afterEach((to) => {
  const name = getProjectName().projectName;
  const title = to.meta.title as string;

  document.title = (`${name} - ${title}`);
});

export default router