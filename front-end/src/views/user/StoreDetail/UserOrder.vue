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
import { ref } from 'vue'

import MenuList from '@/components/user/StoreDetail/OrderView/MenuList.vue'
import DishIntro from '@/components/user/StoreDetail/OrderView/DishIntro.vue';
import ItemCart from '@/components/user/StoreDetail/OrderView/ItemCart.vue';

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

const menuItems = [
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
    image:
      "https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape",
  },
  {
    id: 2,
    categoryId: 1,
    name: "川香牛肉面",
    description: "手工拉面配嫩滑牛肉片，汤底鲜美，麻辣适中",
    price: 22,
    image:
      "https://readdy.ai/api/search-image?query=traditional%20chinese%20beef%20noodle%20soup%20with%20tender%20beef%20slices%20and%20hand%20pulled%20noodles%20in%20clear%20savory%20broth%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food002&orientation=landscape",
  },
  {
    id: 3,
    categoryId: 1,
    name: "特色酸辣粉",
    description: "正宗重庆酸辣粉，酸辣开胃，配花生米和榨菜",
    price: 18,
    image:
      "https://readdy.ai/api/search-image?query=authentic%20chinese%20sour%20and%20spicy%20glass%20noodles%20with%20peanuts%20and%20pickled%20vegetables%20in%20tangy%20red%20sauce%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food003&orientation=landscape",
  },
  {
    id: 4,
    categoryId: 2,
    name: "精品牛肉片",
    description: "新鲜牛肉切片，肉质鲜嫩，是麻辣烫的经典搭配",
    price: 12,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20sliced%20raw%20beef%20for%20hot%20pot%20cooking%2C%20thin%20marbled%20meat%20slices%20arranged%20neatly%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food004&orientation=landscape",
  },
  {
    id: 5,
    categoryId: 2,
    name: "嫩滑羊肉卷",
    description: "优质羊肉卷，肥瘦相间，涮煮后鲜美可口",
    price: 15,
    image:
      "https://readdy.ai/api/search-image?query=premium%20lamb%20meat%20rolls%20sliced%20thin%20for%20hot%20pot%2C%20marbled%20texture%20with%20fat%20and%20lean%20meat%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food005&orientation=landscape",
  },
  {
    id: 6,
    categoryId: 2,
    name: "午餐肉",
    description: "经典午餐肉片，方便快捷，老少皆宜",
    price: 8,
    image:
      "https://readdy.ai/api/search-image?query=sliced%20luncheon%20meat%20spam%20for%20hot%20pot%20cooking%2C%20pink%20rectangular%20meat%20slices%20arranged%20on%20plate%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food006&orientation=landscape",
  },
  {
    id: 7,
    categoryId: 3,
    name: "新鲜白菜",
    description: "当季新鲜白菜，清脆爽口，营养丰富",
    price: 4,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20chinese%20cabbage%20leaves%20cut%20for%20hot%20pot%2C%20green%20and%20white%20vegetable%20pieces%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food007&orientation=landscape",
  },
  {
    id: 8,
    categoryId: 3,
    name: "土豆片",
    description: "精选土豆切片，口感软糯，吸汤入味",
    price: 5,
    image:
      "https://readdy.ai/api/search-image?query=sliced%20potatoes%20for%20hot%20pot%20cooking%2C%20thin%20round%20potato%20slices%20arranged%20neatly%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food008&orientation=landscape",
  },
  {
    id: 9,
    categoryId: 3,
    name: "菠菜",
    description: "新鲜菠菜叶，富含维生素，健康美味",
    price: 6,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20spinach%20leaves%20for%20hot%20pot%20cooking%2C%20green%20leafy%20vegetables%20clean%20and%20vibrant%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food009&orientation=landscape",
  },
  {
    id: 10,
    categoryId: 4,
    name: "鱼豆腐",
    description: "Q弹鱼豆腐，口感丰富，老少皆宜",
    price: 8,
    image:
      "https://readdy.ai/api/search-image?query=fish%20tofu%20balls%20for%20hot%20pot%2C%20white%20and%20golden%20colored%20processed%20fish%20balls%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food010&orientation=landscape",
  },
  {
    id: 11,
    categoryId: 4,
    name: "牛肉丸",
    description: "手工制作牛肉丸，弹性十足，肉香浓郁",
    price: 10,
    image:
      "https://readdy.ai/api/search-image?query=handmade%20beef%20meatballs%20for%20hot%20pot%2C%20round%20brown%20meat%20balls%20with%20smooth%20texture%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food011&orientation=landscape",
  },
  {
    id: 12,
    categoryId: 4,
    name: "鱼丸",
    description: "新鲜鱼肉制作，口感爽滑，鲜味十足",
    price: 9,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20fish%20balls%20for%20hot%20pot%20cooking%2C%20white%20round%20fish%20meatballs%20with%20smooth%20surface%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food012&orientation=landscape",
  },
];

function increaseQuantity(itemId: number) {
  cart.value[itemId] = (cart.value[itemId] || 0) + 1;
  return cart.value[itemId];
};
function decreaseQuantity(itemId: number) {
  if (cart.value[itemId] && cart.value[itemId] > 0) {
    cart.value[itemId]--;
    if (cart.value[itemId] === 0) delete cart.value[itemId];
  }
}
</script>
