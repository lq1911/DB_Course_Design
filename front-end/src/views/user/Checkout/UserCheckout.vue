<template>
  <div class="min-h-screen bg-gray-50">
    <button @click="goBack"
      class="fixed left-4 top-6 flex items-center bg-white shadow-lg px-3 py-2 rounded-xl z-10 hover:bg-gray-100">
      <i class="fas fa-arrow-left mr-2"></i>
      返回
    </button>

    <div class="max-w-8xl mx-auto p-6">
      <div class="flex gap-6 ml-20">
        <!-- 左侧菜品列表 -->
        <div class="w-[120vh] h-[95vh] bg-white rounded-2xl shadow-lg p-6 overflow-y-auto">
          <div class="flex items-center justify-between mb-6 border-b">
            <div class="flex items-center gap-3">
              <h1 class="text-2xl font-bold text-gray-900">已选菜品</h1>
            </div>
          </div>

          <!-- 菜品列表 -->
          <div v-if="items.length > 0" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            <DishCard v-for="item in items" :key="item.id" :item="item" 
              @updateQuantity="updateQuantity"
              @onRemove="removeItem" />
          </div>
          <div v-else class="text-center py-12">
            <h3 class="text-lg font-semibold text-gray-500 mb-2">购物车是空的</h3>
            <p class="text-gray-400">快去选择您喜欢的菜品吧~</p>
          </div>
        </div>

        <!-- 右侧订单/支付区 -->
        <div class="space-y-4 w-[40vh]">
          <AddressSelector :selectedAddress="selectedAddress" @onAddressChange="selectedAddress = $event" />
          <CouponSelector :selectedCoupon="selectedCoupon" :totalAmount="subtotal"
            @onCouponChange="selectedCoupon = $event" />
          <PaymentSelector v-model:selectedMethod="paymentMethod" />
          <OrderSummary :items="items" :deliveryFee="deliveryFee" :isProcessing="isProcessing" @checkout="checkout" />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router'

import DishCard from '@/components/user/Checkout/DishCard.vue';
import AddressSelector from '@/components/user/Checkout/AddressSelector.vue';
import CouponSelector from '@/components/user/Checkout/CouponSelector.vue';
import PaymentSelector from '@/components/user/Checkout/PaymentSelector.vue';
import OrderSummary from '@/components/user/Checkout/OrderSummary.vue';

interface Item { id: number; name: string; price: number; quantity: number; image_url?: string }
interface Coupon { id: number; discount_amount: number; min_amount: number }
interface Address { id: number; name: string; phone: string; address: string; label?: string; is_default?: boolean }

const items = ref<Item[]>([]);
const selectedAddress = ref<Address | null>(null);
const selectedCoupon = ref<Coupon | null>(null);
const paymentMethod = ref('wechat');
const isProcessing = ref(false);
const deliveryFee = ref(5);

const router = useRouter();

const subtotal = computed(() =>
  items.value.reduce((sum, item) => sum + item.price * item.quantity, 0)
);

// 模拟加载购物车
onMounted(() => {
  // TODO: 替换为接口请求
  items.value = [
    { id: 1, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 2, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 3, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 4, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 5, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 6, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 7, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 8, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 9, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 10, name: '番茄炒蛋', price: 12, quantity: 1 }
  ];
});

// --- 方法 ---
function updateQuantity(itemId: number, newQuantity: number) {
  const item = items.value.find(i => i.id === itemId);
  if (item) item.quantity = newQuantity;
}

function removeItem(itemId: number) {
  items.value = items.value.filter(i => i.id !== itemId);
}

async function checkout() {
  if (!selectedAddress.value) {
    alert('请先选择收货地址');
    return;
  }
  isProcessing.value = true;
  await new Promise(resolve => setTimeout(resolve, 2000)); // 模拟支付
  items.value = [];
  selectedCoupon.value = null;
  isProcessing.value = false;
  alert('支付成功！订单已提交。');
}

function goBack() {
    router.back();
}

</script>
