/* eslint-disable */
<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->

<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 顶部导航栏 -->
    <header class="fixed top-0 left-0 right-0 bg-white shadow-sm z-50 h-16">
      <div class="flex items-center justify-between h-full px-6">
        <div class="flex items-center">
          <h1 class="text-xl font-bold text-[#F9771C]">FoodDelivery Pro</h1>
        </div>
        <div class="flex items-center space-x-4">
          <el-icon class="text-gray-600 text-xl cursor-pointer"><Bell /></el-icon>
          <div class="flex items-center space-x-2">
            <img
              src="https://readdy.ai/api/search-image?query=professional%20restaurant%20owner%20portrait%20with%20friendly%20smile%20wearing%20chef%20uniform%20against%20clean%20white%20background%20modern%20lighting&width=40&height=40&seq=merchant-avatar-001&orientation=squarish"
              alt="商家头像"
              class="w-10 h-10 rounded-full object-cover"
            />
            <span class="text-gray-700 font-medium">张老板</span>
          </div>
        </div>
      </div>
    </header>

    <div class="flex pt-16">
      <!-- 左侧导航菜单 -->
      <aside class="fixed left-0 top-16 bottom-0 w-52 bg-white shadow-sm overflow-y-auto">
        <nav class="p-4">
          <div class="space-y-2">
            <div
              v-for="(item, index) in menuItems"
              :key="index"
              @click="handleMenuClick(item)"
              :class="{
                'bg-orange-50 text-[#F9771C] border-r-3 border-[#F9771C]': $route.name === item.routeName,
                'text-gray-700 hover:bg-gray-50': $route.name !== item.routeName
              }"
              class="flex items-center px-4 py-3 rounded-l-lg cursor-pointer transition-colors whitespace-nowrap !rounded-button"
            >
              <el-icon class="mr-3 text-lg">
                <component :is="item.icon" />
              </el-icon>
              <span class="font-medium">{{ item.label }}</span>
            </div>
          </div>
        </nav>
      </aside>
      
      <!-- 主内容区 -->
      <main class="ml-52 flex-1 p-6">
        <!-- 配券中心 -->
        <div v-if="activeMenu === 'coupons'">
          <div class="flex justify-between items-center mb-6">
            <h2 class="text-2xl font-bold text-gray-800">配券中心</h2>
            <button
              @click="showCouponForm = true"
              class="bg-[#F9771C] text-white px-6 py-2 rounded-md hover:bg-[#E16A0E] transition-colors cursor-pointer whitespace-nowrap !rounded-button"
            >
              新建优惠券
            </button>
          </div>
          <!-- 优惠券列表 -->
          <div class="bg-white rounded-lg shadow-sm">
            <el-table :data="coupons" style="width: 100%">
              <el-table-column prop="id" label="优惠券编号" width="180" />
              <el-table-column prop="minAmount" label="起用金额" width="120">
                <template #default="scope">
                  ¥{{ scope.row.minAmount }}
                </template>
              </el-table-column>
              <el-table-column prop="discountAmount" label="优惠金额" width="120">
                <template #default="scope">
                  ¥{{ scope.row.discountAmount }}
                </template>
              </el-table-column>
              <el-table-column prop="startTime" label="开始时间" width="180" />
              <el-table-column prop="endTime" label="结束时间" width="180" />
              <el-table-column label="状态" width="100">
                <template #default="scope">
                  <span
                    :class="{
                      'text-green-500': isValidCoupon(scope.row),
                      'text-gray-500': !isValidCoupon(scope.row)
                    }"
                    class="font-medium"
                  >
                    {{ isValidCoupon(scope.row) ? '有效' : '已过期' }}
                  </span>
                </template>
              </el-table-column>
            </el-table>
          </div>
          <!-- 新建优惠券弹窗 -->
          <div v-if="showCouponForm" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <div class="bg-white rounded-lg p-6 w-96">
              <h3 class="text-lg font-semibold text-gray-800 mb-4">新建优惠券</h3>
              <div class="space-y-4">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">起用金额</label>
                  <input
                    v-model="newCoupon.minAmount"
                    type="number"
                    class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                    placeholder="请输入起用金额"
                  />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">优惠金额</label>
                  <input
                    v-model="newCoupon.discountAmount"
                    type="number"
                    class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                    placeholder="请输入优惠金额"
                  />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">开始时间</label>
                  <input
                    v-model="newCoupon.startTime"
                    type="datetime-local"
                    class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                  />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">结束时间</label>
                  <input
                    v-model="newCoupon.endTime"
                    type="datetime-local"
                    class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                  />
                </div>
              </div>
              <div class="flex justify-end space-x-3 mt-6">
                <button
                  @click="showCouponForm = false"
                  class="px-4 py-2 text-gray-600 border border-gray-300 rounded-md hover:bg-gray-50 transition-colors cursor-pointer whitespace-nowrap !rounded-button"
                >
                  取消
                </button>
                <button
                  @click="createCoupon"
                  class="px-4 py-2 bg-[#F9771C] text-white rounded-md hover:bg-[#E16A0E] transition-colors cursor-pointer whitespace-nowrap !rounded-button"
                >
                  创建
                </button>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import { Bell, House, List, Ticket, Warning, User } from '@element-plus/icons-vue';
import { useRouter, useRoute } from 'vue-router';

const activeMenu = ref('coupons');
const router = useRouter();
const $route = useRoute();

const menuItems = [
  { key: 'overview', label: '店铺概况', icon: House, routeName: 'MerchantHome' },
  { key: 'orders', label: '订单中心', icon: List, routeName: 'MerchantOrders' },
  { key: 'coupons', label: '配券中心', icon: Ticket, routeName: 'MerchantCoupons' },
  { key: 'aftersale', label: '订单售后', icon: Warning, routeName: 'MerchantAftersale' },
  { key: 'profile', label: '商家信息', icon: User, routeName: 'MerchantProfile' }
] as const;

const handleMenuClick = (menuItem: typeof menuItems[number]) => {
  router.push({ name: menuItem.routeName });
};

// const merchantInfo = ref({
//   username: 'zhanglaosan',
// });


const showCouponForm = ref(false);
const newCoupon = ref({
  minAmount: '',
  discountAmount: '',
  startTime: '',
  endTime: ''
});

const coupons = ref([
  {
    id: '20241201120000',
    minAmount: 50,
    discountAmount: 10,
    startTime: '2024-12-01 00:00:00',
    endTime: '2024-12-31 23:59:59'
  },
  {
    id: '20241201130000',
    minAmount: 100,
    discountAmount: 20,
    startTime: '2024-12-01 00:00:00',
    endTime: '2024-11-30 23:59:59'
  }
]);

const isValidCoupon = (coupon: any) => {
  const now = new Date();
  const endTime = new Date(coupon.endTime);
  return endTime > now;
};

const createCoupon = () => {
  const now = new Date();
  const couponId = now.getFullYear().toString() +
    (now.getMonth() + 1).toString().padStart(2, '0') +
    now.getDate().toString().padStart(2, '0') +
    now.getHours().toString().padStart(2, '0') +
    now.getMinutes().toString().padStart(2, '0') +
    now.getSeconds().toString().padStart(2, '0');
  coupons.value.push({
    id: couponId,
    minAmount: Number(newCoupon.value.minAmount),
    discountAmount: Number(newCoupon.value.discountAmount),
    startTime: newCoupon.value.startTime.replace('T', ' ') + ':00',
    endTime: newCoupon.value.endTime.replace('T', ' ') + ':00'
  });
  showCouponForm.value = false;
  newCoupon.value = {
    minAmount: '',
    discountAmount: '',
    startTime: '',
    endTime: ''
  };
};
</script>

<style scoped>
.\!rounded-button {
  border-radius: 8px;
}
input[type="number"]::-webkit-outer-spin-button,
input[type="number"]::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
input[type="number"] {
  -moz-appearance: textfield;
}
</style>