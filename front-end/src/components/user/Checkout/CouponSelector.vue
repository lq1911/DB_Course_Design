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
          <div>
            <span class="font-semibold text-gray-900">{{ selectedCoupon.name }}</span>
            <p class="text-sm text-[#F9771C] font-semibold">
              -¥{{ selectedCoupon.discount_amount.toFixed(2) }}
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
          <p v-if="bestCoupon" class="text-sm text-[#F9771C]">
            最高可省 ¥{{ bestCoupon.discount_amount.toFixed(2) }}
          </p>
        </div>
      </div>
    </div>

    <!-- 弹窗 -->
    <transition name="fade">
      <div
        v-if="showSelector"
        class="fixed inset-0 bg-black/50 z-50 flex items-end justify-center"
      >
        <div
          class="w-full max-w-md bg-white rounded-t-2xl max-h-[80vh] overflow-hidden"
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
              :key="coupon.id"
              class="p-3 border rounded-lg cursor-pointer transition-colors"
              :class="selectedCoupon?.id === coupon.id ? 'border-[#F9771C] bg-[#F9771C]/5' : 'border-gray-200 hover:bg-gray-50'"
              @click="selectCoupon(coupon)"
            >
              <div class="flex items-center justify-between">
                <div class="flex-1">
                  <div class="flex items-center gap-2 mb-1">
                    <span class="font-semibold">{{ coupon.name }}</span>
                    <span class="text-xs px-1 border rounded">{{ coupon.type }}</span>
                  </div>
                  <p class="text-[#F9771C] font-bold text-lg mb-1">¥{{ coupon.discount_amount.toFixed(2) }}</p>
                  <p class="text-xs text-gray-500">
                    满¥{{ coupon.min_amount.toFixed(2) }}可用 · 有效期至 {{ formatDate(coupon.expiry_date) }}
                  </p>
                </div>
                <i v-if="selectedCoupon?.id === coupon.id" class="fas fa-check text-[#F9771C]"></i>
              </div>
            </div>

            <!-- 不可用优惠券 -->
            <div
              v-for="coupon in unavailableCoupons"
              :key="coupon.id"
              class="p-3 border border-gray-200 rounded-lg bg-gray-50 opacity-60"
            >
              <div class="flex items-center justify-between">
                <div class="flex-1">
                  <div class="flex items-center gap-2 mb-1">
                    <span class="font-semibold">{{ coupon.name }}</span>
                    <span class="text-xs px-1 border rounded">{{ coupon.type }}</span>
                  </div>
                  <p class="text-gray-500 font-bold text-lg mb-1">¥{{ coupon.discount_amount.toFixed(2) }}</p>
                  <p class="text-xs text-gray-400">
                    还差¥{{ (coupon.min_amount - totalAmount).toFixed(2) }}可使用
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

interface Coupon {
  id: number;
  name: string;
  type: string;
  discount_amount: number;
  min_amount: number;
  expiry_date: string;
  is_used: boolean;
}

const selectedCoupon = ref({
  id: 1,
  name: '新人满减券',
  type: '满减',
  discount_amount: 5,
  min_amount: 20,
  expiry_date: '2025-12-31',
  is_used: false
});

const props = defineProps<{
//   selectedCoupon: Coupon | null;
  totalAmount: number;
}>();

const emit = defineEmits<{
  (e: 'update:selectedCoupon', coupon: Coupon | null): void;
}>();

const coupons = ref<Coupon[]>([]);
const showSelector = ref(false);

// 模拟获取优惠券
onMounted(() => {
  coupons.value = [
    { id: 1, name: '满100减20', type: '满减', discount_amount: 20, min_amount: 100, expiry_date: '2025-12-31', is_used: false },
    { id: 2, name: '满200减50', type: '满减', discount_amount: 50, min_amount: 200, expiry_date: '2025-12-31', is_used: false },
  ];
});

const availableCoupons = computed(() =>
  coupons.value.filter(c => !c.is_used && props.totalAmount >= c.min_amount)
);

const unavailableCoupons = computed(() =>
  coupons.value.filter(c => props.totalAmount < c.min_amount)
);

const bestCoupon = computed(() => {
  return availableCoupons.value.reduce((best, current) =>
    !best || current.discount_amount > best.discount_amount ? current : best, null as Coupon | null
  );
});

function selectCoupon(coupon: Coupon | null) {
  emit('update:selectedCoupon', coupon);
  showSelector.value = false;
}

function formatDate(dateStr: string) {
  const date = new Date(dateStr);
  return `${date.getMonth() + 1}月${date.getDate()}日`;
}
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
