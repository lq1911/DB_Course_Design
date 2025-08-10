<template>
    <MenuList  />
    <!--<DishIntro />
    <ItemCart /> -->
    
</template>

<script setup>
import MenuList from '@/components/user/StoreDetail/OrderView/MenuList.vue';
// import DishIntro from '@/components/user/StoreDetail/OrderView/DishIntro.vue';
// import ItemCart from '@/components/user/StoreDetail/OrderView/ItemCart.vue';

import { ref } from "vue";

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


</script>

