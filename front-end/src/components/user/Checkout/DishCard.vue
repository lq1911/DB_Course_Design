<template>
  <div v-if="visible">
    <div class="overflow-hidden bg-white shadow-lg hover:shadow-xl transition-all duration-300 rounded-lg">
      <!-- 图片 -->
      <div class="">
        <img
          :src="item.image"
          class="w-full h-full object-cover"
        />
      </div>

      <!-- 内容 -->
      <div class="p-4">
        <!-- 名称 -->
        <div class="text-left mb-2">
          <h3 class="font-semibold text-gray-900 text-lg">{{ item.name }}</h3>
        </div>

        <!-- 价格 -->
        <div class="flex items-center justify-between">
          <span class="text-[#F9771C] font-bold text-xl">
            ¥{{ item.price.toFixed(2) }}
          </span>

          <div class="flex items-center space-x-2">
            <!-- 数量调整器 -->
            <button
              class="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 cursor-pointer"
              @click="decreaseQuantity"
            >
              <i class="fas fa-minus text-xs"></i>
            </button>

            <span class="w-8 text-center">{{ item.quantity }}</span>

            <button
              class="w-8 h-8 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer"
              @click="increaseQuantity"
            >
              <i class="fas fa-plus text-xs"></i>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits } from 'vue'
import type { MenuItem } from '@/api/user_checkout'

interface DishWithQty extends MenuItem {
  quantity: number
}

const props = defineProps<{
  item: DishWithQty
}>()

const emit = defineEmits<{
  (e: 'updateQuantity', dish: MenuItem, quantity: number): void
  (e: 'remove', dish: MenuItem): void
}>()

const visible = ref(true)

// 增加数量
const increaseQuantity = () => {
  emit('updateQuantity', props.item, props.item.quantity + 1)
}

// 减少数量
const decreaseQuantity = () => {
  const newQty = props.item.quantity - 1
  if (newQty <= 0) {
    handleRemove()
  } else {
    emit('updateQuantity', props.item, newQty)
  }
}

// 删除
const handleRemove = () => {
  visible.value = false
  emit('remove', props.item)
}
</script>
