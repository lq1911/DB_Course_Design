// src/router/index.ts

import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'

// 分解的各部分路由
import userRoutes from './userRoutes'
import courierRoutes from './courierRoutes'

// 商家页面组件
import MerchantHomeView from '@/views/merchant/MerchantHomeView.vue'
import MerchantOrdersView from '@/views/merchant/MerchantOrdersView.vue'
import MerchantCouponsView from '@/views/merchant/MerchantCouponsView.vue'
import MerchantAftersaleView from '@/views/merchant/MerchantAftersaleView.vue'
import MerchantProfileView from '@/views/merchant/MerchantProfileView.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    // 根据实际需求设置默认重定向
    redirect: '/login' // 默认进入登录页
    // redirect: '/home' // 测试用户首页
    // redirect: '/courier' // 测试骑手页面
    // redirect: '/MerchantHome' // 测试商家主页
    // 或者根据用户角色动态重定向
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginView,
    meta: {
      title: '登录'
    }
  },
  
  // 用户路由
  ...userRoutes,
  
  // 骑手路由  
  ...courierRoutes,
  
  // 商家路由
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