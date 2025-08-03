// src/router/index.ts


import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'

//分解的各部分路由
import userRoutes from './userRoutes'
import courierRoutes from './courierRoutes'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    //修改用来测试页面
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
  ...userRoutes,
  ...courierRoutes
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