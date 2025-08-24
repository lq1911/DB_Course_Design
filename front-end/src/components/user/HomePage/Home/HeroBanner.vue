<template>
  <section class="relative h-96 overflow-hidden bg-gradient-to-r from-orange-400 to-red-500">
    <div class="absolute inset-0">
      <img
        src="https://readdy.ai/api/search-image?query=delicious%20food%20delivery%20banner%20with%20fresh%20ingredients%20colorful%20vegetables%20fruits%20and%20prepared%20meals%20on%20clean%20white%20background%20modern%20food%20photography%20style%20high%20quality%20commercial%20food%20styling&width=1440&height=384&seq=hero001&orientation=landscape"
        alt="美食外卖横幅"
        class="w-full h-full object-cover object-top"
      />
      <div class="absolute inset-0 bg-black bg-opacity-30"></div>
    </div>
    <div class="relative z-10 h-full flex items-center justify-center">
      <div class="text-center text-white">
        <h1 class="text-5xl font-bold mb-4">新鲜美食 快速送达</h1>
        <p class="text-xl mb-8">精选优质商家，30 分钟内送达您的美食</p>
        <button
          class="bg-orange-500 hover:bg-orange-600 text-white px-8 py-3 rounded-lg text-lg font-medium transition-colors cursor-pointer whitespace-nowrap"
          @click="goToPage('restaurants')"
          >
          立即点餐
        </button>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'

const router = useRouter();

function goToPage(path: string) {
  router.push(path);
}

</script>