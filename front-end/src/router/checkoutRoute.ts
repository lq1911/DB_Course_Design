import { RouteRecordRaw } from 'vue-router'
import UserCheckout from '@/views/user/Checkout/UserCheckout.vue'


const checkoutRoutes: Array<RouteRecordRaw> = [
  {
    path: '/checkout/:id',
    name: 'Checkout',
    component: UserCheckout,
    meta: { title: '结算' }
  }
]
export default checkoutRoutes
