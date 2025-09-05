<template>
  <div>
    <!-- 地址卡片 -->
    <div class="bg-white shadow-lg rounded-lg border-0">
      <div class="flex items-center justify-between px-4 py-3 border-b">
        <div class="flex items-center gap-2 text-[#F9771C]">
          <i class="fas fa-map-pin"></i>
          收货地址
        </div>
        <button
          class="text-[#F9771C] text-sm flex items-center gap-1 hover:bg-[#F9771C]/10 px-2 py-1 rounded"
          @click="showForm = true"
        >
          编辑 <i class="fas fa-chevron-right text-xs"></i>
        </button>
      </div>

      <div class="p-4">
        <div v-if="address">
          <div class="flex items-center gap-2 mb-1">
            <span class="font-semibold text-gray-900">{{ address.name }}</span>
            <span class="text-sm text-gray-600">{{ address.phoneNumber }}</span>
          </div>
          <p class="text-sm text-gray-600 leading-relaxed">{{ address.address }}</p>
        </div>
      </div>
    </div>

    <!-- 弹窗表单 -->
    <transition name="fade">
      <div v-if="showForm" class="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
        <div class="bg-white w-full max-w-md rounded-lg shadow-xl p-6">
          <div class="flex justify-between items-center mb-4">
            <h3 class="font-medium text-gray-900">编辑地址</h3>
            <button class="text-gray-500 hover:text-gray-700" @click="closeForm">
              <i class="fas fa-times"></i>
            </button>
          </div>

          <form @submit.prevent="saveAddress" class="space-y-4">
            <div class="grid grid-cols-2 gap-3">
              <div>
                <label class="block text-sm text-gray-700 mb-1">收货人</label>
                <input v-model="formData.name" type="text" class="w-full border rounded px-2 py-1" required />
              </div>
              <div>
                <label class="block text-sm text-gray-700 mb-1">手机号</label>
                <input v-model="formData.phoneNumber" type="text" class="w-full border rounded px-2 py-1" required />
              </div>
            </div>

            <div>
              <label class="block text-sm text-gray-700 mb-1">详细地址</label>
              <input v-model="formData.address" type="text" class="w-full border rounded px-2 py-1" required />
            </div>

            <div class="flex gap-3 pt-2">
              <button type="button" class="flex-1 border rounded px-4 py-2 hover:bg-gray-50" @click="closeForm">
                取消
              </button>
              <button type="submit" class="flex-1 bg-[#F9771C] text-white rounded px-4 py-2 hover:bg-[#F9771C]/90">
                保存
              </button>
            </div>
          </form>
        </div>
      </div>
    </transition>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, defineEmits } from 'vue'

import type { Address } from '@/api/user_address'
import { getAddress } from '@/api/user_address'


const emit = defineEmits<{
  (e: 'onAddressChange', addr: Address): void
}>()

// 等待添加用户ID
const userID = 30;
const address = ref<Address>()
const showForm = ref(false)

const formData = reactive({
  name: '',
  phoneNumber: 123,
  address: '',
})

onMounted(async() => {
  address.value = await getAddress(userID);
  Object.assign(formData, address.value)
})

// 关闭表单
function closeForm() {
  showForm.value = false
}

// 保存修改
function saveAddress() {
  address.value = { ...formData }
  emit('onAddressChange', address.value)
  closeForm()
}
</script>
