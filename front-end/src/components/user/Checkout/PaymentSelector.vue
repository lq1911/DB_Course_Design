<template>
  <div class="bg-white shadow-lg rounded-lg border-0">
    <div class="px-4 py-3 border-b">
      <h3 class="text-base font-semibold">支付方式</h3>
    </div>
    <div class="p-4 space-y-2">
      <div
        v-for="method in paymentMethods"
        :key="method.id"
        class="flex items-center gap-3 p-3 rounded-lg cursor-pointer transition-all duration-200"
        :class="props.selectedMethod === method.id 
          ? 'bg-[#F9771C]/10 border-2 border-[#F9771C]' 
          : 'border-2 border-gray-100 hover:border-gray-200 hover:bg-gray-50'"
        @click="onMethodChange(method.id)"
      >
        <i :class="['text-xl', method.icon, method.color]"></i>
        <span class="font-medium text-gray-900">{{ method.name }}</span>
        <div
          v-if="props.selectedMethod === method.id"
          class="ml-auto w-4 h-4 bg-[#F9771C] rounded-full flex items-center justify-center"
        >
          <div class="w-2 h-2 bg-white rounded-full"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

interface PaymentMethod {
  id: string;
  name: string;
  icon: string; // 用 FontAwesome 类名
  color: string; // Tailwind 色类
}

// 支付方式
const paymentMethods: PaymentMethod[] = [
  { id: 'wechat', name: '微信支付', icon: 'fab fa-weixin', color: 'text-green-600' },
  { id: 'alipay', name: '支付宝', icon: 'fab fa-alipay', color: 'text-blue-600' },
  { id: 'card', name: '银行卡', icon: 'fas fa-credit-card', color: 'text-black' },
];

const props = defineProps<{
  selectedMethod: string;
}>();

const emit = defineEmits<{
  (e: 'update:selectedMethod', id: string): void;
}>();

function onMethodChange(id: string) {
  emit('update:selectedMethod', id);
}
</script>