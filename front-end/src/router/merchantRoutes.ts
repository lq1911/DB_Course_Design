import { RouteRecordRaw } from 'vue-router'
import MerchantHomeView from '@/views/merchant/MerchantHomeView.vue'
import MerchantOrdersView from '@/views/merchant/MerchantOrdersView.vue'
import MerchantCouponsView from '@/views/merchant/MerchantCouponsView.vue'
import MerchantAftersaleView from '@/views/merchant/MerchantAftersaleView.vue'
import MerchantProfileView from '@/views/merchant/MerchantProfileView.vue'

const merchantRoutes: Array<RouteRecordRaw> = [
    {
        path: '/MerchantHome',
        name: 'MerchantHome',
        component: MerchantHomeView,
        meta: { title: '商家主页'}
    },
    {
        path: '/MerchantOrders',
        name: 'MerchantOrders',
        component: MerchantOrdersView,
        meta: { title: '商家订单页'}
    },
    {
        path: '/MerchantCoupons',
        name: 'MerchantCoupons',
        component: MerchantCouponsView,
        meta: { title: '商家配券页'}
    },
    {
        path: '/MerchantAftersale',
        name: 'MerchantAftersale',
        component: MerchantAftersaleView,
        meta: { title: '商家售后页'}
    },
    {
        path: '/MerchantProfile',
        name: 'MerchantProfile',
        component: MerchantProfileView,
        meta: { title: '商家个人页'}
    }
];

export default merchantRoutes;