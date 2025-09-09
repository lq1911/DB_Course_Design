import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router' 
import { getProjectName } from '@/stores/name'
import LoginView from '@/views/login/LoginView.vue'

// 分解的各部分路由
import userRoutes from './userRoutes'
import courierRoutes from './courierRoutes'
import inStoreRoutes from './inStoreRoutes'
import merchantRoutes from './merchantRoutes'
import checkoutRoutes from './checkoutRoute'
import managerRoutes from './adminRoute'

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
  ...courierRoutes,
  ...inStoreRoutes,
  ...merchantRoutes,
  ...checkoutRoutes,
  ...managerRoutes,
  ...courierRoutes,
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