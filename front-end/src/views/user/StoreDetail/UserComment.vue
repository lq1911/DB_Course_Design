<template>
  <div v-if="activeTab === 'reviews'" class="bg-white rounded-lg shadow-sm p-6">
    <div class="mb-6">
      <h3 class="text-xl font-bold text-gray-900 mb-4">用户评价</h3>
      <div class="flex items-center space-x-6 mb-6">
        <div class="text-center">
          <div class="text-3xl font-bold text-[#F9771C] mb-1">
            {{ storeInfo.rating }}
          </div>
          <div class="text-sm text-gray-600">综合评分</div>
        </div>
        <div class="flex-1">
          <div class="space-y-2">
            <div v-for="(score, index) in [5, 4, 3, 2, 1]" :key="score" class="flex items-center space-x-2">
              <span class="text-sm text-gray-600 w-8">{{ score }}星</span>
              <div class="flex-1 bg-gray-200 rounded-full h-2">
                <div class="bg-[#F9771C] h-2 rounded-full" :style="{ width: `${reviewStats[index]}%` }"></div>
              </div>
              <span class="text-sm text-gray-600 w-12">{{ reviewStats[index] }}%</span>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="space-y-6">
      <div v-for="review in reviews" :key="review.id" class="border-b border-gray-200 pb-6 last:border-b-0">
        <div class="flex items-start space-x-4">
          <img :src="review.avatar" alt="用户头像" class="w-12 h-12 rounded-full object-cover" />
          <div class="flex-1">
            <div class="flex items-center space-x-2 mb-2">
              <span class="font-semibold text-gray-900">{{ review.username }}</span>
              <div class="flex items-center">
                <i v-for="star in 5" :key="star" :class="star <= review.rating ? 'text-yellow-400' : 'text-gray-300'"
                  class="fas fa-star text-sm"></i>
              </div>
              <span class="text-sm text-gray-500">{{ review.date }}</span>
            </div>
            <p class="text-gray-700 mb-3">{{ review.content }}</p>
            <div v-if="review.images.length" class="flex space-x-2">
              <img v-for="(image, index) in review.images" :key="index" :src="image" alt="评价图片"
                class="w-20 h-20 object-cover rounded-lg cursor-pointer" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>

</script>