<template>
    <!-- 购物车按钮 -->
    <div class="fixed right-10 bottom-10 z-10">
      <button
        @click="toggleCart"
        class="bg-[#F9771C] text-white w-14 h-14 rounded-xl shadow-lg hover:bg-orange-600 transition-colors flex items-center justify-center cursor-pointer !rounded-button whitespace-nowrap"
      >
        <i class="fas fa-shopping-cart text-lg"></i>
        <span
          v-if="totalItems > 0"
          class="absolute -top-2 -right-2 bg-red-500 text-white text-xs w-6 h-6 rounded-full flex items-center justify-center"
        >
          {{ totalItems }}
        </span>
      </button>
    </div>

    <!-- 购物车侧边栏 -->
    <div
      v-if="showCart"
      class="fixed inset-0 z-50 overflow-hidden"
      @click="showCart = false"
    >
      <div
        class="absolute right-0 top-0 h-full w-96 bg-white shadow-xl"
        @click.stop
      >
        <div class="flex flex-col h-full">
          <div class="flex items-center justify-between p-4 border-b">
            <h3 class="text-lg font-semibold text-gray-900">购物车</h3>
            <button
              @click="showCart = false"
              class="text-gray-500 hover:text-gray-700 cursor-pointer"
            >
              <i class="fas fa-times text-lg"></i>
            </button>
          </div>
          <div class="flex-1 overflow-y-auto p-4">
            <div
              v-if="cartItems.length === 0"
              class="text-center text-gray-500 mt-8"
            >
              <i class="fas fa-shopping-cart text-4xl mb-4"></i>
              <p>购物车是空的</p>
            </div>
            <div v-else class="space-y-4">
              <div
                v-for="item in cartItems"
                :key="item.id"
                class="flex items-center space-x-3 bg-gray-50 p-3 rounded-lg"
              >
                <img
                  :src="item.image"
                  class="w-12 h-12 object-cover rounded object-top"
                />
                <div class="flex-1">
                  <h4 class="font-medium text-gray-900 text-sm">
                    {{ item.name }}
                  </h4>
                  <p class="text-[#F9771C] font-semibold">¥{{ item.price }}</p>
                </div>
                <div class="flex items-center space-x-2">
                  <button
                    @click="emit('decrease', item.id)"
                    class="w-6 h-6 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 cursor-pointer !rounded-button whitespace-nowrap"
                  >
                    <i class="fas fa-minus text-xs"></i>
                  </button>
                  <span class="w-6 text-center text-sm">{{ item.quantity }}</span
                  >
                  <button
                    @click="emit('increase', item.id)"
                    class="w-6 h-6 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer !rounded-button whitespace-nowrap"
                  >
                    <i class="fas fa-plus text-xs"></i>
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div v-if="cartItems.length > 0" class="border-t p-4">
            <div class="flex items-center justify-between mb-4">
              <span class="text-lg font-semibold text-gray-900">总计</span>
              <span class="text-xl font-bold text-[#F9771C]">¥{{ totalPrice.toFixed(2) }}</span>
            </div>
            <button
              class="w-full bg-[#F9771C] text-white py-3 rounded-lg font-semibold hover:bg-orange-600 transition-colors cursor-pointer !rounded-button whitespace-nowrap"
            >
              去结算
            </button>
          </div>
        </div>
      </div>
    </div>
</template>

<script setup lang="ts">
import { ref, computed, defineProps, defineEmits } from 'vue'

import type { MenuItem } from '@/api/store_info'

const props = defineProps<{
  cart: Record<number, number>;
  menuItems: MenuItem | null;
}>();

const emit = defineEmits<{
  (e: 'increase', itemId: number): void;
  (e: 'decrease', itemId: number): void;
}>();

const showCart = ref(false);

const cartItems = computed(() => {
  return Object.entries(props.cart)
    .filter(([, quantity]) => quantity > 0)
    .map(([itemId, quantity]) => {
      const item = menuItems.find((item) => item.id === parseInt(itemId));
      return item ? { ...item, quantity } : null;
    })
    .filter((item): item is Exclude<typeof item, null> => item !== null);
});

const toggleCart = () => {
  showCart.value = !showCart.value;
};

const totalPrice = computed(() => {
  return cartItems.value.reduce((sum, item) => {
    return sum + (item?.price || 0) * (item?.quantity || 0);
  }, 0);
});

const totalItems = computed(() => {
  return Object.values(props.cart).reduce((sum, quantity) => sum + quantity, 0);
});

// 测试代码
import { menuItems } from '@/api/store_info';
</script>