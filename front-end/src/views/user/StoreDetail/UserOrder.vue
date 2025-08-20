<template>
  <div>
    <div class="flex">
      <MenuList :categories="categories" v-model:activeCategory="activeCategory" />
      <DishIntro 
        :categories="categories" 
        :activeCategory="activeCategory" 
        :cart="cart" 
        :menuItems="menuItems"
        @increase="increaseQuantity"
        @decrease="decreaseQuantity" 
      />
    </div>
    <ItemCart 
      :cart="cart" 
      :menuItems="menuItems"
      @increase="increaseQuantity"
      @decrease="decreaseQuantity"
      />
  </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { ref, computed, onMounted, watch } from 'vue'

import type { StoreInfo, MenuItem } from '@/api/user_store_info'
import { getStoreInfo, getMenuItem } from '@/api/user_store_info'

import MenuList from '@/components/user/StoreDetail/OrderView/MenuList.vue'
import DishIntro from '@/components/user/StoreDetail/OrderView/DishIntro.vue';
import ItemCart from '@/components/user/StoreDetail/OrderView/ItemCart.vue';

const route = useRoute();
const storeID = computed(() => route.params.id as string);
const storeInfo = ref<StoreInfo | null>(null);
const menuItems = ref<MenuItem | null>(null);

const categories = [
  { id: 1, name: "招牌推荐" },
  { id: 2, name: "荤菜类" },
  { id: 3, name: "素菜类" },
  { id: 4, name: "丸子类" },
  { id: 5, name: "豆制品" },
  { id: 6, name: "主食类" },
  { id: 7, name: "饮品" },
];

const activeCategory = ref(1);
const cart = ref<Record<number, number>>({});

function increaseQuantity(itemId: number) {
  cart.value[itemId] = (cart.value[itemId] || 0) + 1;
  return cart.value[itemId];
}

function decreaseQuantity(itemId: number) {
  if (cart.value[itemId] && cart.value[itemId] > 0) {
    cart.value[itemId]--;
    if (cart.value[itemId] === 0) delete cart.value[itemId];
  }
}

onMounted(async () => {
  storeInfo.value = await getStoreInfo(storeID.value);
  menuItems.value = await getMenuItem(storeID.value);
})

watch(storeID, async (newID, oldID) => {
    if (newID != oldID) {
      storeInfo.value = await getStoreInfo(storeID.value);
      menuItems.value = await getMenuItem(storeID.value);
    }
})

</script>
