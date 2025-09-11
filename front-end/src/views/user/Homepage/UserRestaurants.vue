<template>
  <main class="pt-20 min-h-screen max-w-screen-xl mx-auto px-6 flex gap-8">
    <!-- 筛选组件 -->
    <SieveStore
      class="sticky top-20 w-64"
      :default-rating="selectedRating"
      :default-sort="selectedSort"
      @update-sort="onSortChange"
      @update-rating="onRatingChange"
    />

    <!-- 商家列表 -->
    <section class="flex-1">
      <!-- 占位 / 缓冲图标 -->
      <div v-if="showLoading" class="flex justify-center items-center h-64">
        <i class="fas fa-spinner fa-spin text-3xl text-[#F9771C]"></i>
      </div>

      <!-- 商家列表 -->
      <div v-else class="grid grid-cols-2 gap-6">
        <div v-for="restaurant in pagedRestaurants" :key="restaurant.id" class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow">
          <img :src="restaurant.image" :alt="restaurant.name" class="w-full h-48 object-cover object-top" />
          <div class="p-6">
            <div class="flex justify-between items-start mb-3">
              <h3 class="font-bold text-xl">{{ restaurant.name }}</h3>
            </div>
            <div class="flex justify-between items-start mt-2">
              <div class="flex flex-col text-gray-600 text-sm">
                <span class="flex items-center mb-1">
                  <i class="fas fa-star text-yellow-400 mr-1"></i>
                  {{ restaurant.averageRating > 0 ? restaurant.averageRating.toFixed(1) : '暂无评分' }}
                </span>
                <span>月售 {{ restaurant.monthlySales }}</span>
              </div>
              <button
                class="bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-lg text-sm transition-colors cursor-pointer whitespace-nowrap"
                @click="goToPage(restaurant.id)">
                进入店铺
              </button>
            </div>
          </div>
        </div>
      </div>

      <!-- 分页组件 -->
      <div v-if="!showLoading" class="flex justify-center mt-6 space-x-2">
        <button class="px-3 py-1 border shadow-lg rounded hover:bg-gray-100" :disabled="currentPage === 1" @click="goPage(currentPage - 1)">
          上一页
        </button>

        <button v-for="page in totalPages" :key="page" class="px-3 py-1 border shadow-lg rounded"
          :class="{ 'bg-orange-500 text-white': page === currentPage, 'bg-white hover:bg-gray-100': page !== currentPage }"
          @click="goPage(page)">
          {{ page }}
        </button>

        <button class="px-3 py-1 border shadow-lg rounded hover:bg-gray-100" :disabled="currentPage === totalPages" @click="goPage(currentPage + 1)">
          下一页
        </button>
      </div>
    </section>
  </main>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'

import type { AllStore, SearchStore, showStore } from '@/api/user_home';
import { getSearchStore, getAllStore } from '@/api/user_home';

import SieveStore from '@/components/user/HomePage/Home/SieveStore.vue';

const route = useRoute();
const router = useRouter();

const allRestaurants = ref<AllStore | SearchStore>();
const showLoading = ref(true);

// 父组件选项
const selectedSort = ref('综合排序');
const selectedRating = ref(4.5);

// 原始列表
const restaurantList = computed<showStore[]>(() => {
  const data = allRestaurants.value;
  if (!data) return [];
  if (Array.isArray((data as any).allStores)) return (data as any).allStores;
  if (Array.isArray((data as any).searchStores)) return (data as any).searchStores;
  return [];
});

// 过滤 + 排序后的列表
const filteredRestaurants = computed(() => {
  return restaurantList.value
    .filter(r => r.averageRating >= selectedRating.value)
    .sort((a, b) => {
      switch (selectedSort.value) {
        case '评分最高': return b.averageRating - a.averageRating;
        case '月售最高': return b.monthlySales - a.monthlySales;
        default: return 0;
      }
    });
});

// 分页
const currentPage = ref(1);
const pageSize = 12;
const totalPages = computed(() => Math.ceil(filteredRestaurants.value.length / pageSize));
const pagedRestaurants = computed(() => {
  const start = (currentPage.value - 1) * pageSize;
  const end = start + pageSize;
  return filteredRestaurants.value.slice(start, end);
});

function goPage(page: number) {
  if (page >= 1 && page <= totalPages.value) currentPage.value = page;
}

function goToPage(id: number | string) {
  router.push({ name: 'InStore', params: { id } });
}

// 子组件事件
function onSortChange(val: string) {
  selectedSort.value = val;
  currentPage.value = 1; // 切换排序重置到第一页
}

function onRatingChange(val: number) {
  selectedRating.value = val;
  currentPage.value = 1; // 切换评分重置到第一页
}

// 获取数据
onMounted(async () => {
  try {
    if (route.query.keyword) {
      const keyword = route.query.keyword as string;
      const userID = Number(route.query.userID);
      const address = route.query.address as string;
      allRestaurants.value = await getSearchStore(userID, address, keyword);
    } else {
      allRestaurants.value = await getAllStore();
    }
  } catch (err) {
    alert('获取商家失败');
    console.error(err);
  } finally {
    showLoading.value = false;
  }
});
</script>
