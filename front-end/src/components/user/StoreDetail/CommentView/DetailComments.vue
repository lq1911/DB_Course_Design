<template>
  <div class="space-y-6">
    <div v-for="comment in commentList" :key="comment.id" class="border-b border-gray-200 pb-6 last:border-b-0">
      <div class="flex items-start space-x-4">
        <img :src="comment.avatar" class="w-12 h-12 rounded-full object-cover" />
        <div class="flex-1">
          <div class="flex items-center space-x-2 mb-2">
            <span class="font-semibold text-gray-900">{{ comment.username }}</span>
            <div class="flex items-center">
              <i v-for="star in 5" :key="star" :class="star <= comment.rating ? 'text-yellow-400' : 'text-gray-300'"
                class="fas fa-star text-sm"></i>
            </div>
            <span class="text-sm text-gray-500">{{ comment.date }}</span>
          </div>
          <p class="text-left text-gray-700 mb-3">{{ comment.content }}</p>
          <div v-if="comment.images.length" class="flex space-x-2">
            <img v-for="(image, index) in comment.images" :key="index" :src="image"
              class="w-24 h-24 object-cover rounded-lg cursor-pointer"
              @click="openPcomment(image)" />
          </div>
        </div>
      </div>
    </div>

    <!-- 图片放大弹窗 -->
    <div v-if="pcommentUrl" class="fixed inset-0 bg-black bg-opacity-70 flex items-center justify-center z-10" @click.self="closePcomment">
      <div class="relative max-w-3xl max-h-[80vh]">
        <img :src="pcommentUrl" class="max-w-full max-h-full rounded-lg shadow-lg" />
        <button class="absolute top-2 right-2 text-white text-xl font-bold" @click="closePcomment">&times;</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

import type { CommentList } from '@/api/store_info'

const props = defineProps<{
  commentList: CommentList | null;
}>()

// 当前预览的图片 URL
const pcommentUrl = ref<string | null>(null)

// 打开图片预览
function openPcomment(url: string) {
  pcommentUrl.value = url
}

// 关闭图片预览
function closePcomment() {
  pcommentUrl.value = null
}

// 测试代码
import { commentList } from '@/api/store_info'

</script>
