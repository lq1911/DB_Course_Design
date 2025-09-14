<!-- eslint-disable -->
<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->

<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 顶部导航栏 -->
    <header class="fixed top-0 left-0 right-0 bg-white shadow-sm z-50 h-16">
      <div class="flex items-center justify-between h-full px-6">
        <div class="flex items-center">
          <h1 class="text-xl font-bold text-[#F9771C]">{{ projectName }}</h1>
        </div>
        <div class="flex items-center space-x-4">
          <el-icon class="text-gray-600 text-xl cursor-pointer">
            <Bell />
          </el-icon>
          <div class="flex items-center space-x-2">
            <img
              src="https://readdy.ai/api/search-image?query=professional%20restaurant%20owner%20portrait%20with%20friendly%20smile%20wearing%20chef%20uniform%20against%20clean%20white%20background%20modern%20lighting&width=40&height=40&seq=merchant-avatar-001&orientation=squarish"
              alt="商家头像" class="w-10 h-10 rounded-full object-cover" />
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
            <div v-for="(item, index) in menuItems" :key="index" @click="handleMenuClick(item)" :class="{
                'bg-orange-50 text-[#F9771C] border-r-3 border-[#F9771C]': $route.name === item.routeName,
                'text-gray-700 hover:bg-gray-50': $route.name !== item.routeName
              }"
              class="flex items-center px-4 py-3 rounded-l-lg cursor-pointer transition-colors whitespace-nowrap !rounded-button">
              <el-icon class="mr-3 text-lg">
                <component :is="item.icon" />
              </el-icon>
              <span class="font-medium">{{ item.label }}</span>
            </div>
          </div>
        </nav>
        <div class="p-4 border-t border-gray-100">
          <div @click="handleLogout"
            class="flex items-center px-4 py-3 rounded-lg cursor-pointer transition-colors text-red-500 hover:bg-red-50">
            <el-icon class="mr-3 text-lg">
              <SwitchButton />
            </el-icon>
            <span class="font-medium">退出登录</span>
          </div>
        </div>
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
                  <input v-model="merchantInfo.phone" :disabled="!isEditingPhone" :class="{
                      'w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm': true,
                      'bg-gray-100 cursor-not-allowed': !isEditingPhone,
                      'bg-white': isEditingPhone
                    }" />
                  <button @click="toggleEdit('phone')"
                    class="ml-2 text-[#F9771C] hover:text-[#E16A0E] transition-colors">
                    <el-icon v-if="!isEditingPhone">
                      <Edit />
                    </el-icon>
                    <el-icon v-else>
                      <Check />
                    </el-icon>
                  </button>
                </div>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">电子邮箱</label>
                <div class="flex items-center">
                  <input v-model="merchantInfo.email" type="email" :disabled="!isEditingEmail" :class="{
                      'w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm': true,
                      'bg-gray-100 cursor-not-allowed': !isEditingEmail,
                      'bg-white': isEditingEmail
                    }" />
                  <button @click="toggleEdit('email')"
                    class="ml-2 text-[#F9771C] hover:text-[#E16A0E] transition-colors">
                    <el-icon v-if="!isEditingEmail">
                      <Edit />
                    </el-icon>
                    <el-icon v-else>
                      <Check />
                    </el-icon>
                  </button>
                </div>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">注册时间</label>
                <p class="text-gray-900 py-2">{{ merchantInfo.registerTime }}</p>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">账号状态</label>
                <p :class="{'text-green-600': merchantInfo.status === '正常营业', 'text-red-600': merchantInfo.status === '封禁中'}"
                  class="font-medium py-2">
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
import { getProjectName } from '@/stores/name';

import { ref, onMounted } from 'vue';
// ▼▼▼ 修改点 1: 在图标导入中加入 SwitchButton ▼▼▼
import { Bell, House, List, Ticket, Warning, User, Edit, Check, SwitchButton } from '@element-plus/icons-vue';
// ▼▼▼ 修改点 2: 导入 ElMessageBox 用于确认弹窗 ▼▼▼
import { ElMessage, ElMessageBox } from 'element-plus';
import { useRouter, useRoute } from 'vue-router';
import axios from 'axios';

// --- ▼▼▼ 修改点 3: 添加登出功能所需的导入 ▼▼▼ ---
import loginApi from '@/api/login_api';
import { removeToken } from '@/utils/jwt';
const api = axios.create({
  baseURL: 'http://113.44.82.210:5250/api',
  timeout: 5000,
});

const activeMenu = ref('profile');
const router = useRouter();
const $route = useRoute();

const useProjectName = getProjectName();
const projectName = useProjectName.projectName;

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
    const response = await api.get('/merchant/profile');
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
    const response = await api.put('/merchant/profile', {
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

async function handleLogout() {
  try {
    // 1. 弹出确认框
    await ElMessageBox.confirm(
      '您确定要退出当前商家账号吗？', // 提示信息可以针对商家进行微调
      '退出登录',
      {
        confirmButtonText: '确定退出',
        cancelButtonText: '取消',
        type: 'warning',
      }
    );

    // 2. 调用后端登出接口
    await loginApi.logout();

    // 3. 核心：清除本地登录状态
    removeToken();

    ElMessage.success('您已成功退出登录');

    // 4. 重定向到登录页面
    router.replace('/login'); // 确保 '/login' 是你的登录页路由

  } catch (error: any) {
    if (error === 'cancel') {
      ElMessage.info('已取消退出操作');
    } else {
      console.error('登出时发生错误:', error);
      ElMessage.warning('与服务器通信失败，但已在本地强制退出');
      removeToken();
      router.replace('/login');
    }
  }
}

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('authToken'); // 从本地存储取 token
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
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