<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 返回按钮 -->
    <button @click="goBack"
      class="fixed left-4 top-6 flex items-center bg-white shadow-lg px-3 py-2 rounded-xl z-10 hover:bg-gray-100">
      <i class="fas fa-arrow-left mr-2"></i>
      返回
    </button>

    <div class="max-w-8xl mx-auto p-6">
      <div class="flex gap-6 ml-20">
        <!-- 左侧菜品列表 -->
        <div class="w-[125vh] h-[95vh] bg-white rounded-2xl shadow-lg p-6 overflow-y-auto">
          <div class="flex items-center justify-between mb-6 border-b">
            <h1 class="text-2xl font-bold text-gray-900">已选菜品</h1>
          </div>

          <!-- 菜品列表 -->
          <div v-if="cartItems.length > 0" class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            <DishCard
              v-for="item in cartItems"
              :key="item.id"
              :item="item"
              @updateQuantity="updateQuantity"
              @onRemove="removeItem"
            />
          </div>
          <div v-else class="text-center py-12">
            <h3 class="text-lg font-semibold text-gray-500 mb-2">购物车是空的</h3>
            <p class="text-gray-400">快去选择您喜欢的菜品吧~</p>
          </div>
        </div>

        <!-- 右侧订单/支付区 -->
        <div class="space-y-4 w-[45vh]">
          <AddressSelector
            :selectedAddress="selectedAddress"
            @onAddressChange="selectedAddress = $event"
          />
          <CouponSelector
            :totalAmount="subtotal"
            v-model:selectedCoupon="selectedCoupon"
            @onCouponChange="selectedCoupon = $event"
          />
          <PaymentSelector v-model:selectedMethod="paymentMethod" />
          <OrderSummary
            v-model:subtotal="subtotal"
            :selectedCoupon="selectedCoupon"
            v-model:deliveryFee="deliveryFee"
            @checkout="checkout"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useUserStore } from '@/stores/user';

import type { Address } from '@/api/user_address';
import type { MenuItem, ShoppingCart } from '@/api/user_checkout';
import type { CouponInfo } from '@/api/user_coupon';
import { getAddress } from '@/api/user_address';
import { getMenuItem, getShoppingCart, addOrUpdateCartItem, removeCartItem } from '@/api/user_checkout';
import { submitOrder, useCoupon } from '@/api/user_checkout';
import { getDeliveryTasks } from '@/api/user_store_info';

import DishCard from '@/components/user/Checkout/DishCard.vue';
import AddressSelector from '@/components/user/Checkout/AddressSelector.vue';
import CouponSelector from '@/components/user/Checkout/CouponSelector.vue';
import PaymentSelector from '@/components/user/Checkout/PaymentSelector.vue';
import OrderSummary from '@/components/user/Checkout/OrderSummary.vue';

// 路由参数
const route = useRoute();
const router = useRouter();
const userStore = useUserStore();
const userID = userStore.getUserID();
const storeID = computed(() => route.params.id as string);
const deliveryFee = getDeliveryTasks().deliveryFee;

// 数据
const menuItems = ref<MenuItem[]>([]);
const cart = ref<ShoppingCart>({
  cartId: 0,
  totalPrice: 0,
  items: [],
});

const selectedAddress = ref<Address>();
const selectedCoupon = ref < CouponInfo | null>(null);
const paymentMethod = ref('wechat');

// 计算已选菜品
const cartItems = computed(() => {
  return cart.value.items
    .map(ci => {
      const dish = menuItems.value.find(d => d.id === ci.dishId);
      return dish ? { ...dish, quantity: ci.quantity } : null;
    })
    .filter((item): item is MenuItem & { quantity: number } => item !== null);
});

const subtotal = computed(() =>
  cartItems.value.reduce((sum, item) => sum + item.price * item.quantity, 0)
);

watch(
  storeID,
  (newID) => {
    // 只有当 newID 有一个有效值时才加载数据
    if (newID) {
      loadData(newID);
    }
  },
  { immediate: true } // 立即执行以处理首次加载
);

async function loadData(currentStoreID: string) {
  try {
    // 并行加载，速度更快
    const [addressData, menuItemsData, cartData] = await Promise.all([
      getAddress(userID),
      getMenuItem(currentStoreID),
      getShoppingCart(currentStoreID, userID)
    ]);
    
    selectedAddress.value = addressData;
    menuItems.value = menuItemsData;
    cart.value = cartData;

  } catch (error) {
    console.error("加载结算页面数据失败:", error);
  }
}

// 增减菜品数量
async function updateQuantity(dish: MenuItem, quantity: number) {
  if (!cart.value.cartId) return;
  if (quantity > 0) {
    await addOrUpdateCartItem(cart.value.cartId, dish.id, quantity);
  } else {
    await removeCartItem(cart.value.cartId, dish.id);
  }
  // 重新加载数据
  if (storeID.value) {
    await loadData(storeID.value);
  }
}

// 移除菜品
async function removeItem(dish: MenuItem) {
  if (!cart.value.cartId) return;
  await removeCartItem(cart.value.cartId, dish.id);
  // 重新加载数据
  if (storeID.value) {
    await loadData(storeID.value);
  }
}

// 支付结算
async function checkout() {
  if (!selectedAddress.value) {
    alert('请先选择收货地址');
    return;
  }
  if (cartItems.value.length === 0) {
    alert('购物车为空');
    return;
  }

  try {
    await useCoupon(selectedCoupon.value?.couponID ?? null) // 未使用时返回空值
    await submitOrder(userID, cart.value.cartId, Number(storeID.value), deliveryFee);
    cart.value.items = [];
    goBack();
  } catch (error) {
    alert('下单失败，请重试');
  }
}

console.log(deliveryFee);

function goBack() {
  router.back();
}

</script>
