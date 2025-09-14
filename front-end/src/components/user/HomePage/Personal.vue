<template>
    <button @click="showUserPanel = !showUserPanel"
        class="w-10 h-10 bg-orange-500 rounded-lg flex items-center justify-center text-white hover:bg-orange-600 transition-colors cursor-pointer">
        <i class="fas fa-user"></i>
    </button>

    <div
      v-if="showUserPanel"
      class="fixed inset-0"
      @click="showUserPanel = false"
    >
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
              <p class="text-gray-600 text-sm">{{ userInfo.phoneNumber }}</p>
            </div>
          </div>
          <!-- 功能菜单 -->
          <div class="space-y-2">
            <button
              v-for="(menu, index) in userMenus"
              :key="index"
              class="w-full flex items-center justify-between p-3 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer"
              @click="menu.action"
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
            class="w-full mt-8 bg-red-500 hover:bg-red-600 text-white py-3 rounded-lg font-medium transition-colors cursor-pointer whitespace-nowrap"
            @click="showExitForm=true"
          >
            退出登录
          </button>

          <AddrSetting v-model:showAddressForm="showAddressForm" />
          <CouponSetting v-model:showCouponForm="showCouponForm" />
          <AccountSetting v-model:showAccountForm="showAccountForm" />
          <ExitAccount v-model:showExitForm="showExitForm" />
        </div>
      </div>
    </div>

</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from "vue";

import { useUserStore } from "@/stores/user";
import { getUserInfo } from "@/api/user_home";

import AddrSetting from "./PersonalTransition/AddrSetting.vue";
import CouponSetting from "./PersonalTransition/CouponSetting.vue";
import AccountSetting from "./PersonalTransition/AccountSetting.vue";
import ExitAccount from "./PersonalTransition/ExitAccount.vue";

const userStore = useUserStore();
const userID = userStore.getUserID();

const userInfo = ref({
  name: "张小明",
  phoneNumber: 1234556,
  image: '',
  defaultAddress: "同济大学"
});

onMounted(async () => {
  userInfo.value = await getUserInfo(userID);
});

const showUserPanel = ref(false);
const showAddressForm = ref(false);
const showCouponForm = ref(false);
const showAccountForm = ref(false);
const showExitForm = ref(false);

// 用户菜单
const userMenus = [
  { icon: "fas fa-map-marker-alt", label: "默认地址管理", action: ()=>openForm(showAddressForm) },
  { icon: "fas fa-ticket-alt", label: "我的优惠券", action: ()=>openForm(showCouponForm) },
  { icon: "fas fa-cog", label: "账户设置", action: ()=>openForm(showAccountForm) }
];

function openForm(f: { value: Boolean }) {
  f.value = true;
}

</script>