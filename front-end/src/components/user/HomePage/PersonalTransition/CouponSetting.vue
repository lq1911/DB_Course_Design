<template>
  <transition name="fade">
    <div v-if="props.showCouponForm" class="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
      <div class="bg-white w-full max-w-md rounded-lg shadow-xl p-6 overflow-y-auto max-h-[80vh]">
        <div class="flex justify-between items-center mb-4">
          <h3 class="font-medium text-gray-900">我的优惠券</h3>
          <button class="text-gray-500 hover:text-gray-700" @click="closeForm">
            <i class="fas fa-times"></i>
          </button>
        </div>

        <div v-if="coupons.length === 0" class="text-gray-500 text-center py-4">
          暂无可用优惠券
        </div>

        <div v-else class="space-y-3">
          <div
            v-for="coupon in coupons"
            :key="coupon.couponID"
            class="p-3 border rounded-lg transition-colors"
            :class="'border-gray-200 hover:bg-gray-50'"
          >
            <div class="flex items-center justify-between">
              <div class="flex-1">
                <div class="flex items-center gap-2 mb-1">
                  <span class="text-[#F9771C] font-bold text-lg">¥{{ coupon.discountAmount.toFixed(2) }}</span>
                  <span class="text-xs px-1 border rounded">满¥{{ coupon.minimumSpend.toFixed(2) }}可用</span>
                </div>
                <p class="text-xs text-gray-500">有效期至 {{ coupon.validTo }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script setup lang="ts">
import { defineProps, defineEmits, ref, reactive, onMounted } from 'vue'

import type { CouponInfo } from '@/api/user_coupon';
import { useUserStore } from '@/stores/user';
import { getCouponInfo } from '@/api/user_coupon';

const userStore = useUserStore();
const userID = userStore.getUserID();
const coupons = ref<CouponInfo[]>([]);

const props = defineProps<{
    showCouponForm: Boolean;
}>();

const emit = defineEmits<{
    (e: "update:showCouponForm", value: Boolean): void;
}>();

// 关闭弹窗
function closeForm() {
    emit("update:showCouponForm", false);
    console.log('已关闭');
}

onMounted(async () => {
  coupons.value = await getCouponInfo(userID);
})

</script>