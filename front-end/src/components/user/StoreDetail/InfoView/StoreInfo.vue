<template>
  <div v-if="props.storeInfo" class="bg-white rounded-lg shadow-sm p-6">
    <h3 class="text-xl font-bold text-gray-900 mb-6">商家信息</h3>
    <div class="space-y-6">
      <div class="flex justify-center gap-[50vh]">
        <!-- 左侧基础信息 -->
        <div class="space-y-4 text-left">
          <div class="flex items-center space-x-3">
            <i class="fas fa-store text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">商家名称</div>
              <div class="text-gray-600">{{ props.storeInfo.name }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-map-marker-alt text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">商家地址</div>
              <div class="text-gray-600">{{ props.storeInfo.address }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-clock text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">营业时间</div>
              <div class="text-gray-600">{{ props.storeInfo.businessHours }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-star text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">商店评分</div>
              <div class="text-gray-600">{{ props.storeInfo.rating }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-chart-line text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">月销量</div>
              <div class="text-gray-600">{{ props.storeInfo.monthlySales }}</div>
            </div>
          </div>
        </div>

        <!-- 右侧描述信息 -->
        <div class="space-y-4 text-left">
          <div class="flex items-start space-x-3">
            <i class="fas fa-info-circle text-[#F9771C] w-5 mt-1"></i>
            <div>
              <div class="font-semibold text-gray-900">商店描述</div>
              <div class="text-gray-600 break-words max-w-full">{{ props.storeInfo.description }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-calendar-alt text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">创建时间</div>
              <div class="text-gray-600">{{ props.storeInfo.createTime }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps } from 'vue'
import type { StoreInfo } from '@/api/user_store_info'

const props = defineProps<{
  storeInfo: StoreInfo
}>()
</script>
