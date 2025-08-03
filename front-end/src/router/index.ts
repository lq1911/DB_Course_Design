// src/router/index.ts


import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'

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