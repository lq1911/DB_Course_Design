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
            <span class="text-gray-700 font-medium">{{ merchantInfo.name || '加载中...' }}</span>
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
        <!-- 商家信息 -->
        <div v-if="activeMenu === 'profile'">
          <h2 class="text-2xl font-bold text-gray-800 mb-6">个人信息</h2>
          <div class="bg-white rounded-lg shadow-sm p-6">
            <h3 class="text-lg font-semibold text-gray-800 mb-4">基本信息</h3>
            <div class="grid grid-cols-2 gap-6">
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">ID</label>
                <p class="text-gray-900 py-2">{{ merchantInfo.id }}</p>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">姓名</label>
                <p class="text-gray-900 py-2">{{ merchantInfo.name }}</p>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">手机号</label>
                <div class="flex items-center">
                  <input
                    v-model="merchantInfo.phone"
                    :disabled="!isEditingPhone"
                    :class="{
                      'w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm': true,
                      'bg-gray-100 cursor-not-allowed': !isEditingPhone,
                      'bg-white': isEditingPhone
                    }"
                  />
                  <button
                    @click="toggleEdit('phone')"
                    class="ml-2 text-[#F9771C] hover:text-[#E16A0E] transition-colors"
                  >
                    <el-icon v-if="!isEditingPhone"><Edit /></el-icon>
                    <el-icon v-else><Check /></el-icon>
                  </button>
                </div>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">电子邮箱</label>
                <div class="flex items-center">
                  <input
                    v-model="merchantInfo.email"
                    type="email"
                    :disabled="!isEditingEmail"
                    :class="{
                      'w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm': true,
                      'bg-gray-100 cursor-not-allowed': !isEditingEmail,
                      'bg-white': isEditingEmail
                    }"
                  />
                  <button
                    @click="toggleEdit('email')"
                    class="ml-2 text-[#F9771C] hover:text-[#E16A0E] transition-colors"
                  >
                    <el-icon v-if="!isEditingEmail"><Edit /></el-icon>
                    <el-icon v-else><Check /></el-icon>
                  </button>
                </div>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">注册时间</label>
                <p class="text-gray-900 py-2">{{ merchantInfo.registerTime }}</p>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">账号状态</label>
                <p :class="{'text-green-600': merchantInfo.status === '正常营业', 'text-red-600': merchantInfo.status === '封禁中'}" class="font-medium py-2">
                  {{ merchantInfo.status }}
                </p>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue';
import { Bell, House, List, Ticket, Warning, User, Edit, Check } from '@element-plus/icons-vue';
import { ElMessage } from 'element-plus';
import { useRouter, useRoute } from 'vue-router';
import axios from 'axios';

const activeMenu = ref('profile');
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

const isSaving = ref(false);
const isEditingPhone = ref(false);
const isEditingEmail = ref(false);

const merchantInfo = ref({
  id: '加载中...',
  name: '加载中...',
  phone: '加载中...',
  email: '加载中...',
  registerTime: '加载中...',
  status: '正常营业'
});

const toggleEdit = (field: 'phone' | 'email') => {
  if (field === 'phone') {
    isEditingPhone.value = !isEditingPhone.value;
    if (!isEditingPhone.value) {
      saveShopInfo();
    }
  } else if (field === 'email') {
    isEditingEmail.value = !isEditingEmail.value;
    if (!isEditingEmail.value) {
      saveShopInfo();
    }
  }
};

// 获取商家信息
const fetchMerchantInfo = async () => {
  try {
    const response = await axios.get('/api/merchant/profile');
    merchantInfo.value = response.data.data;
  } catch (error) {
    ElMessage.error('获取商家信息失败');
    console.error('Error fetching merchant info:', error);
  }
};

// 保存商家信息
const saveShopInfo = async () => {
  if (isSaving.value) return;
  isSaving.value = true;
  
  try {
    const { phone, email } = merchantInfo.value;
    const response = await axios.put('/api/merchant/profile', {
      phone,
      email
    });
    
    if (response.data.success) {
      ElMessage.success('商家信息更新成功');
      fetchMerchantInfo(); // 重新获取最新数据
    } else {
      ElMessage.error(response.data.message || '更新失败');
    }
  } catch (error) {
    ElMessage.error('保存失败，请重试');
    console.error('Error saving merchant info:', error);
  } finally {
    isSaving.value = false;
  }
};

// 组件挂载时获取商家信息
onMounted(() => {
  fetchMerchantInfo();
});
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