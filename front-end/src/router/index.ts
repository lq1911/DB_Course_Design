// src/router/index.ts


import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'
import UserHomeView from '@/views/user/Homepage/UserHome.vue'
import UserRecommendView from '@/views/user/Homepage/UserRecommend.vue'
import UserRestaurantsView from '@/views/user/Homepage/UserRestaurants.vue'
import UserOrderView from '@/views/user/Homepage/UserOrders.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    // redirect: '/login'
    redirect: '/home'
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
  },
  {
    path: '/courier',
    name: 'Courier',
    component: () => import('@/views/courier/CourierView.vue'),
    meta: {
      title: '骑手'
    }
  },
    {
    path: '/courier/account',
    name: 'AccountSettings',
    // 使用路由懒加载，优化性能
    component: () => import('../views/courier/AccountSettings.vue'), 
    meta: {
      title: '账户设置'
    }
  },
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