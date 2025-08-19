<template>
  <div class="min-h-screen bg-gray-50">
    <div class="max-w-7xl mx-auto p-4 lg:p-6">
      <!-- 移动端标题栏 -->
      <div class="lg:hidden mb-6 flex items-center justify-between">
        <h1 class="text-2xl font-bold text-gray-900 flex items-center gap-2">
          <ShoppingCartIcon class="h-6 w-6 text-[#F9771C]" />
          购物车
          <Badge v-if="totalItems > 0" class="bg-[#F9771C]">{{ totalItems }}</Badge>
        </h1>
        <Button
          v-if="items.length > 0"
          variant="ghost"
          size="sm"
          class="text-gray-500 hover:text-red-500"
          @click="clearCart"
        >
          <Trash2Icon class="h-4 w-4 mr-1" /> 清空
        </Button>
      </div>

      <div class="grid lg:grid-cols-10 gap-6">
        <!-- 左侧菜品列表 -->
        <div class="lg:col-span-7">
          <div class="bg-white rounded-2xl shadow-lg p-6">
            <!-- 桌面端标题栏 -->
            <div class="hidden lg:flex items-center justify-between mb-6">
              <div class="flex items-center gap-3">
                <h1 class="text-2xl font-bold text-gray-900">已选菜品</h1>
                <Badge v-if="totalItems > 0" class="bg-[#F9771C] text-lg px-3 py-1">
                  {{ totalItems }}件
                </Badge>
              </div>
              <Button
                v-if="items.length > 0"
                variant="ghost"
                class="text-gray-500 hover:text-red-500"
                @click="clearCart"
              >
                <Trash2Icon class="h-4 w-4 mr-2" /> 清空购物车
              </Button>
            </div>

            <!-- 菜品列表 -->
            <div v-if="items.length > 0" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
              <DishCard
                v-for="item in items"
                :key="item.id"
                :item="item"
                @onQuantityChange="updateQuantity"
                @onRemove="removeItem"
              />
            </div>

            <div v-else class="text-center py-12">
              <ShoppingCartIcon class="h-16 w-16 text-gray-300 mx-auto mb-4" />
              <h3 class="text-lg font-semibold text-gray-500 mb-2">购物车是空的</h3>
              <p class="text-gray-400">快去选择您喜欢的菜品吧~</p>
            </div>
          </div>
        </div>

        <!-- 右侧订单/支付区 -->
        <div class="lg:col-span-3 space-y-4 lg:sticky lg:top-6">
          <AddressSelector
            :selectedAddress="selectedAddress"
            @onAddressChange="selectedAddress = $event"
          />

          <CouponSelector
            :selectedCoupon="selectedCoupon"
            :totalAmount="subtotal"
            @onCouponChange="selectedCoupon = $event"
          />

          <PaymentSelector
            :selectedMethod="paymentMethod"
            @onMethodChange="paymentMethod = $event"
          />

          <!-- <OrderSummary
            :items="items"
            :selectedCoupon="selectedCoupon"
            :deliveryFee="deliveryFee"
            :isProcessing="isProcessing"
            @checkout="checkout"
          /> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';

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

const subtotal = computed(() =>
  items.value.reduce((sum, item) => sum + item.price * item.quantity, 0)
);
const totalItems = computed(() =>
  items.value.reduce((sum, item) => sum + item.quantity, 0)
);

// 模拟加载购物车
onMounted(() => {
  // TODO: 替换为接口请求
  items.value = [
    { id: 1, name: '番茄炒蛋', price: 12, quantity: 1 },
    { id: 2, name: '宫保鸡丁', price: 18, quantity: 2 }
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

function clearCart() {
  if (confirm('确定要清空购物车吗？')) items.value = [];
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
</script>
