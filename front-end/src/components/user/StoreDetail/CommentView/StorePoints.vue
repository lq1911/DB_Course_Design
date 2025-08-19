<template>
    <div class="mt-6 mb-6 border-b-2">
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
                <div class="bg-[#F9771C] h-2 rounded-full" :style="{ width: `${commentStatus[index]}%` }"></div>
              </div>
              <span class="text-sm text-gray-600 w-12">{{ commentStatus[index] }}%</span>
            </div>
          </div>
        </div>
      </div>
    </div>
</template>

<script setup lang="ts">
import { defineProps } from 'vue'

import type { CommentStatus } from '@/api/user_store_info'
import type { StoreInfo } from '@/api/user_store_info'

const props = defineProps<{
    commentStatus: CommentStatus | null;
    storeInfo: StoreInfo | null;
}>();

// 测试使用代码，完成后删除
import { storeInfo } from '@/api/user_store_info'
import { commentStatus } from '@/api/user_store_info'
</script>