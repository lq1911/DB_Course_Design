<template>
  <div v-if="visible">
    <div class="overflow-hidden bg-white shadow-lg hover:shadow-xl transition-all duration-300 rounded-lg">
      <!-- 图片 -->
      <div class="">
        <img
          :src="item.image_url || 'https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape'"
          class="w-full h-full object-cover" />
      </div>

      <!-- 内容 -->
      <div class="p-4">
        <!-- 名称 -->
        <div class="text-left mb-2">
          <h3 class="font-semibold text-gray-900 text-lg">
            {{ item.name }}
          </h3>
        </div>

        <!-- 价格-->
        <div class="flex items-center justify-between">
          <span class="text-[#F9771C] font-bold text-xl">
            ¥{{ item.price.toFixed(2) }}
          </span>

          <div class="flex items-center space-x-2">
            <!-- 数量调整器 -->
            <button
              class="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 cursor-pointer"
              @click="handleQuantity(item.quantity - 1)">
              <i class="fas fa-minus text-xs"></i>
            </button>

            <span class="w-8 text-center">
              {{ item.quantity }}
            </span>

            <button
              class="w-8 h-8 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer"
              @click="handleQuantity(item.quantity + 1)">
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
  emit('remove', props.item.id)
}
</script>