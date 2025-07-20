<template>
  <aside class="w-64 bg-white rounded-lg shadow-md p-6 h-fit">
    <h3 class="font-bold text-lg mb-6 border-b pb-2">筛选条件</h3>

    <!-- 商家分类 -->
    <section class="mb-6">
      <h4 class="font-medium text-gray-800 mb-4">商家分类</h4>
      <div class="space-y-3">
        <label
          v-for="(cat, index) in restaurantCategories"
          :key="index"
          class="flex items-center cursor-pointer select-none"
        >
          <input
            type="checkbox"
            class="mr-3 text-orange-500 focus:ring-orange-500"
          />
          <span class="text-sm">{{ cat }}</span>
        </label>
      </div>
    </section>

    <!-- 配送范围 -->
    <section class="mb-6">
      <h4 class="font-medium text-gray-800 mb-4">配送范围</h4>
      <div class="space-y-3">
        <label
          v-for="(range, index) in deliveryRanges"
          :key="index"
          class="flex items-center cursor-pointer select-none"
        >
          <input
            type="radio"
            name="delivery"
            class="mr-3 text-orange-500 focus:ring-orange-500"
          />
          <span class="text-sm">{{ range }}</span>
        </label>
      </div>
    </section>

    <!-- 商家评分 -->
    <section>
      <h4 class="font-medium text-gray-800 mb-4">商家评分</h4>
      <div class="space-y-3">
        <label
          v-for="(rating, index) in ratingFilters"
          :key="index"
          class="flex items-center cursor-pointer select-none"
        >
          <input
            type="checkbox"
            class="mr-3 text-orange-500 focus:ring-orange-500"
          />
          <span class="text-sm flex items-center">
            <i class="fas fa-star text-yellow-400 mr-1"></i>
            {{ rating }}
          </span>
        </label>
      </div>
    </section>
  </aside>
</template>

<script lang="ts" setup>
// 商家分类筛选
const restaurantCategories = [
  "中餐",
  "西餐",
  "日韩料理",
  "东南亚菜",
  "快餐",
  "甜品饮品",
  "火锅烧烤",
];

const deliveryRanges = [
  "1km 以内",
  "3km 以内",
  "5km 以内",
  "不限距离",
];

const ratingFilters = ["4.5 分以上", "4.0 分以上", "3.5 分以上"];
</script>
