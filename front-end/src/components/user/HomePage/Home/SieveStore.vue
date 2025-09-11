<template>
  <aside class="w-64 bg-white rounded-lg shadow-md p-6 h-fit">
    <h3 class="font-bold text-xl mb-6 border-b pb-2 text-left">筛选条件</h3>

    <!-- 商家评分 -->
    <section>
      <h4 class="font-medium text-gray-800 mb-4 text-left">商家评分</h4>
      <div class="space-y-3">
        <label v-for="rating in ratingOptions" :key="rating.value" class="flex items-center cursor-pointer select-none">
          <input
            type="radio"
            name="rating"
            class="mr-3 text-orange-500"
            :value="rating.value"
            v-model.number="selectedRating"
          />
          <span class="text-sm flex items-center">
            <i class="fas fa-star text-yellow-400 mr-1"></i>{{ rating.label }}
          </span>
        </label>
      </div>
    </section>

    <!-- 排序 -->
    <section class="mt-6">
      <h3 class="font-bold text-xl mb-6 border-b pb-2 text-left">排序方式</h3>
      <div class="space-y-3">
        <label v-for="sort in sortOptions" :key="sort" class="flex items-center cursor-pointer select-none">
          <input
            type="radio"
            name="sort"
            class="mr-3 text-orange-500"
            :value="sort"
            v-model="selectedSort"
          />
          <span class="text-sm">{{ sort }}</span>
        </label>
      </div>
    </section>
  </aside>
</template>

<script lang="ts" setup>
import { ref, watch, defineProps, defineEmits } from 'vue';

const props = defineProps({
  defaultSort: String,
  defaultRating: Number
});

const emit = defineEmits(['update-sort', 'update-rating']);

const sortOptions = ["综合排序", "评分最高", "月售最高", "距离最近"];
const ratingOptions = [
  { label: "所有商家", value: 0},
  { label: "4.5 分以上", value: 4.5 },
  { label: "4.0 分以上", value: 4.0 },
  { label: "3.5 分以上", value: 3.5 },
  { label: "3.0 分以上", value: 3.0 }
];

const selectedSort = ref(props.defaultSort || "综合排序");
const selectedRating = ref<number>(props.defaultRating || 0);

// 监听变化，传给父组件
watch(selectedSort, val => emit('update-sort', val), { immediate: true });
watch(selectedRating, val => emit('update-rating', val), { immediate: true });
</script>
