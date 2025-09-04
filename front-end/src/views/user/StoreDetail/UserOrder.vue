<template>
  <div>
    <div class="flex">
      <!-- 菜单分类 -->
      <MenuList 
        :categories="categories" 
        v-model:activeCategory="activeCategory" />

      <!-- 菜品展示 -->
      <DishIntro 
        :categories="categories" 
        :activeCategory="activeCategory" 
        :cart="cart" 
        :menuItems="menuItems" 
        @increase="increaseQuantity"
        @decrease="decreaseQuantity"
      />
    </div>

    <!-- 购物车 -->
    <ItemCart 
      :cart="cart" 
      :storeID="storeID"
      :menuItems="menuItems" 
      @increase="increaseQuantity"
      @decrease="decreaseQuantity"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'

import type { StoreInfo } from '@/api/user_store_info'
import type { MenuItem, ShoppingCart, ShoppingCartItem } from '@/api/user_checkout'
import { getStoreInfo } from '@/api/user_store_info'
import { getMenuItem, getShoppingCart, addOrUpdateCartItem, removeCartItem } from '@/api/user_checkout'

import MenuList from '@/components/user/StoreDetail/OrderView/MenuList.vue'
import DishIntro from '@/components/user/StoreDetail/OrderView/DishIntro.vue'
import ItemCart from '@/components/user/StoreDetail/OrderView/ItemCart.vue'

// 路由
const route = useRoute()
const storeID = computed(() => route.params.id as string)
const userID = 30;  // 待添加用户编号

// 数据
const storeInfo = ref<StoreInfo>()
const menuItems = ref<MenuItem[]>([])
const cart = ref<ShoppingCart>({
  cartId: 3,
  totalPrice: 0,
  items: []
});  // 防止未定义

// 固定分类（可根据需要改成动态生成）
const categories = [
  { id: 0, name: "招牌推荐" },
  { id: 2, name: "荤菜类" },
  { id: 3, name: "素菜类" },
  { id: 4, name: "丸子类" },
  { id: 5, name: "豆制品" },
  { id: 6, name: "主食类" },
  { id: 7, name: "饮品" },
]

const activeCategory = ref(1)

// 增加数量
async function increaseQuantity(dish: MenuItem) {
  const item = cart.value.items.find(i => i.dishId === dish.id)
  const newQty = (item?.quantity ?? 0) + 1

  await addOrUpdateCartItem(cart.value.cartId, dish.id, newQty)
  await loadCart()
}

// 减少数量
async function decreaseQuantity(dish: MenuItem) {
  const item = cart.value.items.find(i => i.dishId === dish.id)
  if (!item) return

  const newQty = item.quantity - 1
  if (newQty > 0) {
    await addOrUpdateCartItem(cart.value.cartId, dish.id, newQty)
  } else {
    await removeCartItem(cart.value.cartId, dish.id)
  }
  await loadCart()
}

// 读取购物车
async function loadCart() {
  const data = await getShoppingCart(storeID.value, userID)
  cart.value = data ?? { cartId: 0, totalPrice: 0, items: [] }
}

// 获取数据
async function loadData(storeID: string) {
  storeInfo.value = await getStoreInfo(storeID)
  menuItems.value = await getMenuItem(storeID)
  console.log(menuItems)
  await loadCart()
}

// 生命周期
onMounted(() => loadData(storeID.value))
watch(storeID, (newID, oldID) => {
  if (newID !== oldID) loadData(newID)
})

</script>
