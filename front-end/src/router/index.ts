// src/router/index.ts


import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'

//分解的各部分路由
import userRoutes from './userRoutes'
import courierRoutes from './courierRoutes'

import MerchantHomeView from '@/views/merchant/MerchantHomeView.vue'
import MerchantOrdersView from '@/views/merchant/MerchantOrdersView.vue'
import MerchantCouponsView from '@/views/merchant/MerchantCouponsView.vue'
import MerchantAftersaleView from '@/views/merchant/MerchantAftersaleView.vue'
import MerchantProfileView from '@/views/merchant/MerchantProfileView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    //修改用来测试页面
    // redirect: '/login'
    // redirect: '/home'
     redirect: '/MerchantHome'
    // redirect: '/MerchantOrders'
    // redirect: '/MerchantCoupons'
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginView,
    meta: {
      title: '登录'
    }
  },
  ...userRoutes,
  ...courierRoutes
  
  // -----商家页面start----- 
  {
    path: '/MerchantHome',
    name: 'MerchantHome',
    component: MerchantHomeView,
    meta: {
      title: '商家主页'
    }
  },
  {
    path: '/MerchantOrders',
    name: 'MerchantOrders',
    component: MerchantOrdersView,
    meta: {
      title: '商家订单页'
    }
  },
  {
    path: '/MerchantCoupons',
    name: 'MerchantCoupons',
    component: MerchantCouponsView,
    meta: {
      title: '商家配券页'
    }
  },
  {
    path: '/MerchantAftersale',
    name: 'MerchantAftersale',
    component: MerchantAftersaleView,
    meta: {
      title: '商家售后页'
    }
  },
  {
    path: '/MerchantProfile',
    name: 'MerchantProfile',
    component: MerchantProfileView,
    meta: {
      title: '商家个人页'
    }
  }
  // -----商家页面end-----
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