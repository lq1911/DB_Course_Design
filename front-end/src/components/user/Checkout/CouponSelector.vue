<template>
  <div>
    <!-- 优惠券卡片 -->
    <div class="bg-white shadow-lg rounded-lg border-0">
      <div class="flex items-center justify-between px-4 py-3 border-b">
        <div class="flex items-center gap-2 text-[#F9771C]">
          <i class="fas fa-ticket-alt"></i> 优惠券
        </div>
        <button
          class="text-[#F9771C] text-sm flex items-center gap-1 hover:bg-[#F9771C]/10 px-2 py-1 rounded"
          @click="showSelector = true"
        >
          选择 <i class="fas fa-chevron-right text-xs"></i>
        </button>
      </div>
      <div class="p-4">
        <div v-if="selectedCoupon" class="flex items-center justify-between">
          <div class="flex-1">
            <p class="text-[#F9771C] font-bold text-lg mb-1">
              ¥{{ selectedCoupon.discountAmount.toFixed(2) }}
            </p>
            <p class="text-xs text-gray-500">
              满¥{{ selectedCoupon.minimumSpend.toFixed(2) }}可用 · 有效期至 {{ formatDate(selectedCoupon.validTo) }}
            </p>
          </div>
          <span class="px-2 py-0.5 text-[#F9771C] border border-[#F9771C] rounded text-xs">
            已选用
          </span>
        </div>
        <div v-else>
          <span class="text-gray-600">
            {{ availableCoupons.length }} 张可用
          </span>
        </div>
      </div>
    </div>

    <!-- 弹窗 -->
    <transition name="fade">
      <div
        v-if="showSelector"
        class="fixed inset-0 bg-black/50 z-50 flex items-center justify-center"
      >
        <div
          class="w-full max-w-md bg-white rounded-2xl max-h-[80vh] overflow-hidden"
        >
          <div class="p-4 border-b flex justify-between items-center">
            <h3 class="text-lg font-semibold">选择优惠券</h3>
            <button class="text-[#F9771C]" @click="showSelector = false">完成</button>
          </div>

          <div class="p-4 space-y-3 overflow-y-auto max-h-[60vh]">
            <!-- 不使用优惠券 -->
            <div
              class="p-3 border rounded-lg cursor-pointer transition-colors"
              :class="!selectedCoupon ? 'border-[#F9771C] bg-[#F9771C]/5' : 'border-gray-200 hover:bg-gray-50'"
              @click="selectCoupon(null)"
            >
              <div class="flex items-center justify-between">
                <span class="font-medium">不使用优惠券</span>
                <i v-if="!selectedCoupon" class="fas fa-check text-[#F9771C]"></i>
              </div>
            </div>

            <!-- 可用优惠券 -->
            <div
              v-for="coupon in availableCoupons"
              :key="coupon.couponID"
              class="p-3 border rounded-lg cursor-pointer transition-colors"
              :class="selectedCoupon?.couponID === coupon.couponID ? 'border-[#F9771C] bg-[#F9771C]/5' : 'border-gray-200 hover:bg-gray-50'"
              @click="selectCoupon(coupon)"
            >
              <div class="flex items-center justify-between">
                <div class="flex-1">
                  <p class="text-[#F9771C] font-bold text-lg mb-1">
                    ¥{{ coupon.discountAmount.toFixed(2) }}
                  </p>
                  <p class="text-xs text-gray-500">
                    满¥{{ coupon.minimumSpend.toFixed(2) }}可用 · 有效期至 {{ formatDate(coupon.validTo) }}
                  </p>
                </div>
                <i v-if="selectedCoupon?.couponID === coupon.couponID" class="fas fa-check text-[#F9771C]"></i>
              </div>
            </div>

            <!-- 不可用优惠券 -->
            <div
              v-for="coupon in unavailableCoupons"
              :key="coupon.couponID"
              class="p-3 border border-gray-200 rounded-lg bg-gray-50 opacity-60"
            >
              <div class="flex items-center justify-between">
                <div class="flex-1">
                  <p class="text-gray-500 font-bold text-lg mb-1">
                    ¥{{ coupon.discountAmount.toFixed(2) }}
                  </p>
                  <p class="text-xs text-gray-400">
                    还差¥{{ (coupon.minimumSpend - totalAmount).toFixed(2) }}可使用
                  </p>
                </div>
              </div>
            </div>

          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, defineProps, defineEmits } from 'vue';

import { CouponInfo, getCouponInfo } from '@/api/user_coupon';
import { useUserStore } from '@/stores/user';

const showSelector = ref(false);
const userStore = useUserStore();
const userID = userStore.getUserID();

const props = defineProps<{
  totalAmount: number;
  selectedCoupon: CouponInfo | null;
}>();

const emit = defineEmits<{
  (e: 'update:selectedCoupon', coupon: CouponInfo | null): void;
}>();

const coupons = ref<CouponInfo[]>([]);

onMounted(async () => {
  coupons.value = await getCouponInfo(userID);
});

// 可用优惠券，选中的放最前面
const availableCoupons = computed(() => {
  const list = coupons.value.filter(c => props.totalAmount >= c.minimumSpend);
  if (!props.selectedCoupon) return list;
  return [
    props.selectedCoupon,
    ...list.filter(c => c.couponID !== props.selectedCoupon?.couponID)
  ];
});

// 不可用优惠券
const unavailableCoupons = computed(() =>
  coupons.value.filter(c => props.totalAmount < c.minimumSpend)
);

function selectCoupon(coupon: CouponInfo | null) {
  emit('update:selectedCoupon', coupon);
  showSelector.value = false;
}

function formatDate(dateStr: string) {
  const date = new Date(dateStr);
  return `${date.getMonth() + 1}月${date.getDate()}日`;
}
</script>
