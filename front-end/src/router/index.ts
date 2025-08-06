// src/router/index.ts
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'

//分解的各部分路由
import userRoutes from './userRoutes'
import courierRoutes from './courierRoutes'
import inStoreRoutes from './inStoreRoutes'
import merchantRoutes from './merchantRoutes'


const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/login'
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginView,
    meta: { title: '登录'}
  },
  ...userRoutes,
  ...courierRoutes,
  ...inStoreRoutes,
  ...merchantRoutes
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL), 
  routes,
})

router.afterEach((to) => {
  const name = getProjectName().projectName;
  let title = to.meta.title as string;

  if (!title) {
    title = "热爱每一餐"
  }

  document.title = (`${name} - ${title}`);
});

export default router