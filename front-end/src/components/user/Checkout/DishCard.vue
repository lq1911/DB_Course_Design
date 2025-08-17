<template>
  <transition
    name="fade-scale"
    appear
  >
    <div v-if="visible" class="relative group">
      <div class="overflow-hidden bg-white shadow-sm hover:shadow-lg transition-all duration-300 border border-gray-100 rounded-lg">
        <!-- 图片 -->
        <div class="aspect-square overflow-hidden bg-gray-50">
          <img
            :src="item.image_url || 'https://images.unsplash.com/photo-1546833999-b9f581a1996d?w=300&h=300&fit=crop'"
            :alt="item.name"
            class="w-full h-full object-cover transition-transform duration-300 group-hover:scale-105"
          />
        </div>

        <!-- 内容 -->
        <div class="p-4">
          <!-- 名称 & 删除按钮 -->
          <div class="flex items-start justify-between mb-2">
            <h3 class="font-semibold text-gray-900 text-sm leading-tight line-clamp-2">
              {{ item.name }}
            </h3>
            <button
              class="opacity-0 group-hover:opacity-100 transition-opacity h-6 w-6 -mt-1 -mr-1"
              @click="handleRemove"
            >
              <i class="fas fa-times text-xs"></i>
            </button>
          </div>

          <!-- 价格 & 小计 -->
          <div class="flex items-center justify-between mb-3">
            <span class="text-[#F9771C] font-bold text-lg">
              ¥{{ item.price.toFixed(2) }}
            </span>
            <span class="text-xs text-gray-500">
              小计: ¥{{ (item.price * item.quantity).toFixed(2) }}
            </span>
          </div>

          <!-- 数量调整器 -->
          <div class="flex items-center justify-center bg-gray-50 rounded-full p-1">
            <button
              class="h-8 w-8 rounded-full hover:bg-white"
              @click="handleQuantity(item.quantity - 1)"
            >
              <i class="fas fa-minus text-xs"></i>
            </button>

            <span class="mx-3 font-semibold text-gray-900 min-w-[20px] text-center">
              {{ item.quantity }}
            </span>

            <button
              class="h-8 w-8 rounded-full hover:bg-white"
              @click="handleQuantity(item.quantity + 1)"
            >
              <i class="fas fa-plus text-xs"></i>
            </button>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits } from 'vue'

const props = defineProps({
  item: { type: Object, required: true },
})

const emit = defineEmits(['updateQuantity', 'remove'])

// 控制删除动画
const visible = ref(true)

const handleQuantity = (newQuantity: number) => {
  if (newQuantity <= 0) {
    handleRemove()
  } else {
    emit('updateQuantity', props.item.id, newQuantity)
  }
}

const handleRemove = () => {
  visible.value = false
  setTimeout(() => {
    emit('remove', props.item.id)
  }, 300) // 动画时间需和 transition duration 一致
}
</script>

<style scoped>
/* 简单 fade + scale 动画 */
.fade-scale-enter-active,
.fade-scale-leave-active {
  transition: all 0.3s ease;
}
.fade-scale-enter-from,
.fade-scale-leave-to {
  opacity: 0;
  transform: scale(0.95);
}
.fade-scale-enter-to,
.fade-scale-leave-from {
  opacity: 1;
  transform: scale(1);
}
</style>
