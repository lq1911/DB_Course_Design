<!-- eslint-disable -->
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
        <!-- 订单售后 -->
        <div v-if="activeMenu === 'aftersale'">
          <h2 class="text-2xl font-bold text-gray-800 mb-6">订单售后</h2>
          
          <!-- 切换标签 -->
          <div class="bg-white rounded-lg shadow-sm p-4 mb-6">
            <div class="flex space-x-4">
              <button
                v-for="tab in aftersaleTabs"
                :key="tab.value"
                @click="activeAftersaleTab = tab.value"
                :class="{
                  'bg-[#F9771C] text-white': activeAftersaleTab === tab.value,
                  'bg-gray-100 text-gray-700 hover:bg-gray-200': activeAftersaleTab !== tab.value
                }"
                class="px-4 py-2 rounded-md transition-colors cursor-pointer whitespace-nowrap !rounded-button"
              >
                {{ tab.label }}
              </button>
            </div>
          </div>

          <!-- 处罚记录 -->
          <div v-if="activeAftersaleTab === 'penalties'">
            <div class="bg-white rounded-lg shadow-sm">
              <el-table :data="penalties" style="width: 100%">
                <el-table-column prop="id" label="处罚编号" width="150" />
                <el-table-column prop="reason" label="处罚原因" />
                <el-table-column prop="time" label="处罚时间" width="180" />
                <el-table-column prop="merchantAction" label="商家处罚措施" />
                <el-table-column prop="platformAction" label="店铺处罚措施" />
              </el-table>
            </div>
          </div>

          <!-- 评论查看 -->
          <div v-if="activeAftersaleTab === 'reviews'">
            <div class="space-y-4">
              <div v-for="review in reviews" :key="review.id" class="bg-white rounded-lg shadow-sm p-6">
                <div class="flex items-start space-x-4">
                  <img
                    :src="review.avatar"
                    alt="用户头像"
                    class="w-12 h-12 rounded-full object-cover"
                  />
                  <div class="flex-1">
                    <div class="flex items-center space-x-2 mb-2">
                      <span class="font-medium text-gray-800">{{ review.username }}</span>
                      <div class="flex text-yellow-400">
                        <el-icon v-for="i in review.rating" :key="i"><Star /></el-icon>
                      </div>
                      <span class="text-sm text-gray-500">{{ review.time }}</span>
                    </div>
                    <p class="text-gray-700 mb-3">{{ review.content }}</p>
                    <div v-if="review.reply" class="bg-gray-50 rounded-lg p-3">
                      <p class="text-sm text-gray-600"><strong>商家回复：</strong>{{ review.reply }}</p>
                    </div>
                  </div>
                </div>
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
import { Bell, Star, House, List, Ticket, Warning, User } from '@element-plus/icons-vue';
import { useRouter, useRoute } from 'vue-router';

const activeMenu = ref('aftersale');
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

const penalties = ref([
  {
    id: 'PEN20241201001',
    reason: '食品安全问题',
    time: '2024-11-15 16:30:00',
    merchantAction: '整改厨房卫生',
    platformAction: '警告处理'
  }
]);

const reviews = ref([
  {
    id: 1,
    username: '美食爱好者',
    avatar: 'https://readdy.ai/api/search-image?query=happy%20customer%20portrait%20smiling%20face%20with%20clean%20background%20modern%20lighting%20professional%20photo&width=48&height=48&seq=customer-avatar-001&orientation=squarish',
    rating: 5,
    time: '2024-12-01 14:30:00',
    content: '菜品很棒，味道正宗，配送也很及时！',
    reply: ''
  },
  {
    id: 2,
    username: '吃货小王',
    avatar: 'https://readdy.ai/api/search-image?query=young%20customer%20portrait%20friendly%20smile%20with%20clean%20background%20modern%20lighting%20professional%20photo&width=48&height=48&seq=customer-avatar-002&orientation=squarish',
    rating: 4,
    time: '2024-11-30 19:45:00',
    content: '川菜做得不错，就是稍微有点咸了。',
    reply: '感谢您的反馈，我们会注意调整口味的。'
  }
]);

const aftersaleTabs = [
  { value: 'penalties', label: '处罚记录' },
  { value: 'reviews', label: '评论查看' }
];

const activeAftersaleTab = ref('penalties');


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