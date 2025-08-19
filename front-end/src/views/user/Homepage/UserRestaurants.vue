<template>
  <main class="pt-20 min-h-screen max-w-screen-xl mx-auto px-6 flex gap-8">
    <SieveStore class="sticky top-20 w-64" />
    
    <!-- 商家列表 -->
    <section class="flex-1">
      <div class="grid grid-cols-2 gap-6">
        <div v-for="(restaurant, index) in pagedRestaurants" :key="index"
          class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow">
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
                class="bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-lg text-sm transition-colors cursor-pointer whitespace-nowrap"
                @click="goToPage(`/store/${restaurant.id}`)"
                >
                进入店铺
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- 分页组件 -->
      <div class="flex justify-center mt-6 space-x-2">
        <button
          class="px-3 py-1 border shadow-lg rounded hover:bg-gray-100"
          :disabled="currentPage === 1"
          @click="goPage(currentPage - 1)">
          上一页
        </button>

        <button
          v-for="page in totalPages" :key="page"
          class="px-3 py-1 border shadow-lg rounded"
          :class="{'bg-orange-500 text-white': page === currentPage, 'bg-white hover:bg-gray-100': page !== currentPage}"
          @click="goPage(page)">
          {{ page }}
        </button>

        <button
          class="px-3 py-1 border shadow-lg rounded hover:bg-gray-100"
          :disabled="currentPage === totalPages"
          @click="goPage(currentPage + 1)">
          下一页
        </button>
      </div>
    </section>
  </main>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'

import SieveStore from '@/components/user/HomePage/Home/SieveStore.vue';

const router = useRouter();

function goToPage(path: string) {
  router.push(path);
}

// 所有商家
const allRestaurants = [
  {
    id: 1,
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
    id: 2,
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
    id: 3,
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
    id: 4,
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
    id: 5,
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
    id: 6,
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
  {
    id: 6,
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
  {
    id: 6,
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
  {
    id: 6,
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
  {
    id: 6,
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
  {
    id: 6,
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
  {
    id: 6,
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
  {
    id: 6,
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

// 分页逻辑
const currentPage = ref(1);
const pageSize = 12;

const totalPages = computed(() => Math.ceil(allRestaurants.length / pageSize));

const pagedRestaurants = computed(() => {
  const start = (currentPage.value - 1) * pageSize;
  const end = start + pageSize;
  return allRestaurants.slice(start, end);
});

function goPage(page: number) {
  if (page >= 1 && page <= totalPages.value) {
    currentPage.value = page;
  }
}
</script>
