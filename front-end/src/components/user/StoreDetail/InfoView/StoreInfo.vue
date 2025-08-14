<template>
  <div class="bg-white rounded-lg shadow-sm p-6">
    <h3 class="text-xl font-bold text-gray-900 mb-6">商家信息</h3>
    <div class="ml-80 space-y-6">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div class="space-y-4">
          <div class="flex items-center space-x-3">
            <i class="fas fa-store text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">商家名称</div>
              <div class="text-gray-600">{{ storeInfo.name }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-map-marker-alt text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">商家地址</div>
              <div class="text-gray-600">{{ storeInfo.address }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-phone text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">联系电话</div>
              <div class="text-gray-600">{{ storeInfo.phone }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-clock text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">营业时间</div>
              <div class="text-gray-600">{{ storeInfo.businessHours }}</div>
            </div>
          </div>
        </div>
        <div class="space-y-4">
          <div class="flex items-center space-x-3">
            <i class="fas fa-certificate text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">营业执照</div>
              <div class="text-gray-600">{{ storeInfo.license }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-shield-alt text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">食品经营许可证</div>
              <div class="text-gray-600">{{ storeInfo.foodLicense }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-truck text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">配送范围</div>
              <div class="text-gray-600">{{ storeInfo.deliveryRange }}</div>
            </div>
          </div>
          <div class="flex items-center space-x-3">
            <i class="fas fa-money-bill text-[#F9771C] w-5"></i>
            <div>
              <div class="font-semibold text-gray-900">起送金额</div>
              <div class="text-gray-600">¥{{ storeInfo.minOrder }}</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps } from 'vue'

import type { StoreInfo } from '@/api/store_info'

// 测试使用代码，完成后删除
import { storeInfo } from '@/api/store_info'

const props = defineProps<{
    storeInfo: StoreInfo | null
}>();

</script>