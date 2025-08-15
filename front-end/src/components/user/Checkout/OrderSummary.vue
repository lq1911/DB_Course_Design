<template>
  <div class="bg-white shadow-lg rounded-lg border-0">
    <div class="px-4 py-3 border-b">
      <h3 class="text-base font-semibold">费用明细</h3>
    </div>
    <div class="p-4 space-y-4">
      <div class="space-y-2">
        <div class="flex justify-between text-sm">
          <span class="text-gray-600">商品总价</span>
          <span>¥{{ subtotal.toFixed(2) }}</span>
        </div>
        <div class="flex justify-between text-sm">
          <span class="text-gray-600">配送费</span>
          <span>¥{{ deliveryFee.toFixed(2) }}</span>
        </div>
        <div v-if="discount > 0" class="flex justify-between text-sm">
          <span class="text-gray-600">优惠金额</span>
          <span class="text-[#F9771C] font-semibold">-¥{{ discount.toFixed(2) }}</span>
        </div>
      </div>

      <div class="border-t border-gray-200"></div>

      <div class="flex justify-between items-center">
        <span class="font-semibold text-lg">实付金额</span>
        <span class="font-bold text-xl text-[#F9771C]">¥{{ total.toFixed(2) }}</span>
      </div>

      <button
        class="w-full bg-[#F9771C] hover:bg-[#F9771C]/90 text-white font-semibold py-3 text-lg rounded"
        @click="onCheckout"
        :disabled="isProcessing || items.length === 0"
      >
        {{ isProcessing ? '处理中...' : `立即支付 ¥${total.toFixed(2)}` }}
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, defineProps, defineEmits } from 'vue';

interface Item {
  id: string | number;
  name: string;
  price: number;
  quantity: number;
}

interface Coupon {
  id: string | number;
  name: string;
  discount_amount: number;
}

const props = defineProps<{
  items: Item[];
  selectedCoupon?: Coupon | null;
  deliveryFee?: number;
  isProcessing?: boolean;
}>();

const emit = defineEmits<{
  (e: 'checkout'): void;
}>();

const deliveryFee = props.deliveryFee ?? 5;

const subtotal = computed(() =>
  props.items.reduce((sum, item) => sum + item.price * item.quantity, 0)
);

const discount = computed(() => props.selectedCoupon?.discount_amount ?? 0);

const total = computed(() => Math.max(0, subtotal.value + deliveryFee - discount.value));

function onCheckout() {
  emit('checkout');
}
</script>

<style scoped></style>
