<template>
    <button @click="showUserPanel = !showUserPanel"
        class="w-10 h-10 bg-orange-500 rounded-full flex items-center justify-center text-white hover:bg-orange-600 transition-colors cursor-pointer !rounded-button">
        <i class="fas fa-user"></i>
    </button>

    <div
      v-if="showUserPanel"
      class="fixed inset-0 z-50"
      @click="showUserPanel = false"
    >
      <div class="absolute inset-0 bg-black bg-opacity-50"></div>
      <div
        class="absolute right-0 top-0 h-full w-80 bg-white shadow-xl"
        @click.stop
      >
        <div class="p-6">
          <!-- 用户信息 -->
          <div class="flex items-center space-x-4 mb-8">
            <div
              class="w-16 h-16 bg-orange-500 rounded-full flex items-center justify-center text-white text-xl font-bold"
            >
              {{ userInfo.name.charAt(0) }}
            </div>
            <div>
              <h3 class="font-bold text-lg">{{ userInfo.name }}</h3>
              <p class="text-gray-600 text-sm">{{ userInfo.phone }}</p>
            </div>
          </div>
          <!-- 优惠券信息 -->
          <div class="bg-orange-50 rounded-lg p-4 mb-6">
            <div class="flex items-center justify-between">
              <span class="text-orange-600 font-medium">我的优惠券</span>
              <span
                class="bg-orange-500 text-white px-2 py-1 rounded-full text-xs"
                >{{ userInfo.coupons }}</span
              >
            </div>
          </div>
          <!-- 功能菜单 -->
          <div class="space-y-2">
            <button
              v-for="(menu, index) in userMenus"
              :key="index"
              class="w-full flex items-center justify-between p-3 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer !rounded-button"
            >
              <div class="flex items-center space-x-3">
                <i :class="menu.icon" class="text-gray-600"></i>
                <span>{{ menu.label }}</span>
              </div>
              <i class="fas fa-chevron-right text-gray-400"></i>
            </button>
          </div>
          <!-- 退出登录 -->
          <button
            class="w-full mt-8 bg-red-500 hover:bg-red-600 text-white py-3 rounded-lg font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap"
          >
            退出登录
          </button>
        </div>
      </div>
    </div>

</template>

<script setup lang="ts">
import { ref } from "vue";

const showUserPanel = ref(false);

// 用户信息
const userInfo = {
  name: "张小明",
  phone: "138****5678",
  coupons: 5,
};
// 用户菜单
const userMenus = [
  { icon: "fas fa-map-marker-alt", label: "收货地址管理" },
  { icon: "fas fa-heart", label: "收藏的商家" },
  { icon: "fas fa-ticket-alt", label: "我的优惠券" },
  { icon: "fas fa-cog", label: "账户设置" },
  { icon: "fas fa-headset", label: "客服中心" },
  { icon: "fas fa-question-circle", label: "帮助中心" },
];

</script>

<style scoped>
.\!rounded-button {
  border-radius: 8px;
}

</style>