<template>
  <main class="pt-20 min-h-screen max-w-screen-xl mx-auto px-6 flex gap-8">
    <SieveStore class="sticky top-20 w-64" />

    <!-- 商家列表 -->
    <section class="flex-1">
      <!-- 占位 / 缓冲图标 -->
      <div v-if="showLoading" class="flex justify-center items-center h-64">
        <i class="fas fa-spinner fa-spin text-3xl text-[#F9771C]"></i>
      </div>

      <!-- 商家列表 -->
      <div v-else class="grid grid-cols-2 gap-6">
        <div v-for="(restaurant, index) in pagedRestaurants" :key="index" class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow">
          <img :src="restaurant.image" :alt="restaurant.name" class="w-full h-48 object-cover object-top" />
          <div class="p-6">
            <div class="flex justify-between items-start mb-3">
              <h3 class="font-bold text-xl">{{ restaurant.name }}</h3>
            </div>
            <div class="flex justify-between items-start mt-2">
              <div class="flex flex-col text-gray-600 text-sm">
                <span class="flex items-center mb-1">
                  <i class="fas fa-star text-yellow-400 mr-1"></i>
                  {{ restaurant.averageRating  }}
                </span>
                <span>月售 {{ restaurant.monthlySales }}</span>
              </div>
              <!-- 右侧：按钮 -->
              <button
                class="bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-lg text-sm transition-colors cursor-pointer whitespace-nowrap"
                @click="goToPage(`/store/${restaurant.id}`)">
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
const query = route.query.q as string | undefined;
const showLoading = ref(true);

const restaurantList = computed<showStore[]>(() => {
  if (!allRestaurants.value) return [];
  if ('allStores' in allRestaurants.value) return allRestaurants.value.allStores;
  if ('searchStore' in allRestaurants.value) return allRestaurants.value.searchStore;
  return [];
});

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

    showLoading.value = false;
    console.log(allRestaurants.value)
  } catch (err) {
    alert('获取商家失败');
    console.error('获取商家失败', err);
  }
});

function goToPage(path: string) {
  router.push(path);
}

// 分页逻辑
const currentPage = ref(1);
const pageSize = 12;

// 总页数基于 restaurantList
const totalPages = computed(() => Math.ceil(restaurantList.value.length / pageSize));

// 当前页商家列表
const pagedRestaurants = computed(() => {
  const start = (currentPage.value - 1) * pageSize;
  const end = start + pageSize;
  return restaurantList.value.slice(start, end);
});

function goPage(page: number) {
  if (page >= 1 && page <= totalPages.value) currentPage.value = page;
}
</script>
