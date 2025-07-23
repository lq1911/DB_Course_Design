<template>
  <TopNav />

  <main class="pt-20 min-h-screen max-w-screen-xl mx-auto px-6 flex gap-8">
    <SieveStore class="w-64" />
    <!-- 商家列表 -->
    <section class="flex-1">
      <div class="grid grid-cols-2 gap-6">
        <div v-for="(restaurant, index) in allRestaurants" :key="index"
          class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow cursor-pointer">
          <img :src="restaurant.image" :alt="restaurant.name" class="w-full h-48 object-cover object-top" />
          <div class="p-6">
            <div class="flex justify-between items-start mb-3">
              <h3 class="font-bold text-xl">{{ restaurant.name }}</h3>
              <span class="bg-orange-100 text-orange-600 px-2 py-1 rounded text-xs">
                {{ restaurant.category }}
              </span>
            </div>
            <div class="flex items-center justify-between text-sm text-gray-600 mb-3">
              <span class="flex items-center">
                <i class="fas fa-star text-yellow-400 mr-1"></i>
                {{ restaurant.rating }}
              </span>
              <span>月售 {{ restaurant.monthlySales }}</span>
            </div>
            <div class="flex items-center justify-between text-sm text-gray-600 mb-4">
              <span>{{ restaurant.deliveryTime }}</span>
              <span>起送 ¥{{ restaurant.minOrder }}</span>
            </div>
            <div class="flex items-center justify-between">
              <span class="text-orange-500 font-medium">{{ restaurant.promotion }}</span>
              <button
                class="bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-lg text-sm transition-colors cursor-pointer !rounded-button whitespace-nowrap">
                进入店铺
              </button>
            </div>
          </div>
        </div>
      </div>
    </section>
  </main>

  <Footer />
</template>

<script lang="ts" setup>
import TopNav from '@/components/user/HomePage/TopNav.vue';
import SieveStore from '@/components/user/HomePage/SieveStore.vue';
import Footer from '@/components/user/HomePage/Footer.vue';

// 所有商家
const allRestaurants = [
  {
    name: "蜀香麻辣烫",
    category: "中餐",
    rating: "4.8",
    monthlySales: "1200",
    deliveryTime: "25-35 分钟",
    minOrder: "20",
    promotion: "满 30 减 5",
    image:
      "https://readdy.ai/api/search-image?query=sichuan%20restaurant%20storefront%20with%20spicy%20hot%20pot%20and%20traditional%20chinese%20decor%20on%20clean%20background%20commercial%20photography%20style&width=400&height=192&seq=shop001&orientation=landscape",
  },
  {
    name: "港记茶餐厅",
    category: "港式",
    rating: "4.6",
    monthlySales: "850",
    deliveryTime: "30-40 分钟",
    minOrder: "25",
    promotion: "新店开业 8 折",
    image:
      "https://readdy.ai/api/search-image?query=hong%20kong%20tea%20restaurant%20with%20dim%20sum%20and%20traditional%20dishes%20display%20on%20clean%20background%20commercial%20photography%20style&width=400&height=192&seq=shop002&orientation=landscape",
  },
  {
    name: "一兰拉面",
    category: "日料",
    rating: "4.7",
    monthlySales: "960",
    deliveryTime: "20-30 分钟",
    minOrder: "30",
    promotion: "买二送一",
    image:
      "https://readdy.ai/api/search-image?query=japanese%20ramen%20shop%20with%20authentic%20noodle%20bowls%20and%20modern%20interior%20design%20on%20clean%20background%20commercial%20photography&width=400&height=192&seq=shop003&orientation=landscape",
  },
  {
    name: "必胜客",
    category: "西餐",
    rating: "4.5",
    monthlySales: "720",
    deliveryTime: "35-45 分钟",
    minOrder: "40",
    promotion: "第二份半价",
    image:
      "https://readdy.ai/api/search-image?query=pizza%20restaurant%20with%20wood%20fired%20oven%20and%20fresh%20italian%20pizzas%20on%20clean%20background%20commercial%20photography%20style&width=400&height=192&seq=shop004&orientation=landscape",
  },
  {
    name: "韩膳宫",
    category: "韩料",
    rating: "4.4",
    monthlySales: "680",
    deliveryTime: "25-35 分钟",
    minOrder: "35",
    promotion: "满 50 减 8",
    image:
      "https://readdy.ai/api/search-image?query=korean%20restaurant%20with%20traditional%20korean%20dishes%20and%20modern%20interior%20on%20clean%20background%20commercial%20photography%20style&width=400&height=192&seq=shop005&orientation=landscape",
  },
  {
    name: "泰式风情",
    category: "东南亚",
    rating: "4.6",
    monthlySales: "540",
    deliveryTime: "30-40 分钟",
    minOrder: "28",
    promotion: "限时 7 折",
    image:
      "https://readdy.ai/api/search-image?query=thai%20restaurant%20with%20authentic%20curry%20dishes%20and%20tropical%20decor%20on%20clean%20background%20commercial%20photography%20style&width=400&height=192&seq=shop006&orientation=landscape",
  },
];
</script>
