<template>
  <section class="max-w-screen-xl mx-auto px-6 pb-12">
    <h2 class="text-2xl font-bold text-gray-800 mb-8 text-left">人气商家</h2>

    <!-- 占位 / 缓冲图标 -->
    <div v-if="showLoading" class="flex justify-center items-center h-64">
      <i class="fas fa-spinner fa-spin text-3xl text-[#F9771C]"></i>
    </div>

    <!-- 商家列表 -->
    <div v-else class="grid grid-cols-4 gap-6">
      <div v-for="(restaurant, index) in popularRestaurants?.recomStore" :key="index"
        class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow cursor-pointer text-left">
        <img :src="restaurant.image" :alt="restaurant.name" class="w-full h-40 object-cover object-top" />
        <div class="p-4 flex justify-between items-center">
          <div>
            <h3 class="font-bold text-lg mb-2">{{ restaurant.name }}</h3>
            <div class="flex items-center justify-between text-sm text-gray-600">
              <span class="flex items-center">
                <i class="fas fa-star text-yellow-400 mr-1"></i>
                {{ restaurant.averageRating }}
              </span>
            </div>
          </div>
          <!-- 右侧：按钮 -->
          <button
            class="w-30 h-12 bg-orange-500 hover:bg-orange-600 text-white px-4 py-2 rounded-lg text-sm transition-colors cursor-pointer whitespace-nowrap"
            @click="goToPage(`/store/${restaurant.id}`)">
            进入店铺
          </button>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router'

import type { RecomStore } from '@/api/user_home';
import { getRecomStore } from '@/api/user_home';

const router = useRouter();

const popularRestaurants = ref<RecomStore>({ recomStore: [] });
const showLoading = ref(true); // 控制缓冲图标显示

onMounted(async () => {
  try {
    popularRestaurants.value = (await getRecomStore());
    showLoading.value = false;
  } catch (error) {
    alert('获取推荐商家失败');
    console.error('获取推荐商家失败', error);
  }
});

function goToPage(path: string) {
  router.push(path);
}
</script>