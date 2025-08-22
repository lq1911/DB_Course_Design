<template>
  <div class="space-y-6">
    <!-- 评论列表 -->
    <div v-for="comment in pagedComments" :key="comment.id" class="border-b border-gray-200 pb-6 last:border-b-0">
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
              class="w-24 h-24 object-cover rounded-lg cursor-pointer" @click="openPcomment(image)" />
          </div>
        </div>
      </div>
    </div>
    
    <!-- 分页按钮 -->
    <div class="flex justify-center mt-6 space-x-2">
      <button class="px-3 py-1 border shadow-lg rounded hover:bg-gray-100" :disabled="currentPage === 1"
        @click="goPage(currentPage - 1)">
        上一页
      </button>

      <button v-for="page in totalPages" :key="page" class="px-3 py-1 border shadow-lg rounded"
        :class="{ 'bg-orange-500 text-white': page === currentPage, 'bg-white hover:bg-gray-100': page !== currentPage }"
        @click="goPage(page)">
        {{ page }}
      </button>

      <button class="px-3 py-1 border shadow-lg rounded hover:bg-gray-100" :disabled="currentPage === totalPages"
        @click="goPage(currentPage + 1)">
        下一页
      </button>
    </div>

    <!-- 图片放大弹窗 -->
    <div v-if="pcommentUrl" class="fixed inset-0 bg-black bg-opacity-70 flex items-center justify-center z-10"
      @click.self="closePcomment">
      <div class="relative max-w-3xl max-h-full">
        <img :src="pcommentUrl" class="max-w-full max-h-full rounded-lg shadow-lg" />
        <button class="absolute top-2 right-2 text-white text-xl font-bold" @click="closePcomment">&times;</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, defineProps, watch } from 'vue'
import type { CommentList } from '@/api/user_store_info'

const props = defineProps<{
  commentList: CommentList
}>()

// 分页控制
const currentPage = ref(1);
const pageSize = 12;
const totalPages = computed(() => Math.ceil(props.commentList.comments.length / pageSize));

const pagedComments = computed(() => {
  const start = (currentPage.value - 1) * pageSize
  return props.commentList.comments.slice(start, start + pageSize)
})

function goPage(page: number) {
  if (page < 1 || page > totalPages.value) return
  currentPage.value = page
}

// 图片放大
const pcommentUrl = ref<string | null>(null)
function openPcomment(url: string) { pcommentUrl.value = url }
function closePcomment() { pcommentUrl.value = null }

</script>
