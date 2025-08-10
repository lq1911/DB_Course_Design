<template>
<!-- 左侧商品分类导航 -->
    <div class="w-1/6 bg-white rounded-lg shadow-sm ml-[5%] mt-5 border-2" style="box-shadow: 2px 0 4px rgba(0,0,0,0.05)">
        <div class="py-4">
            <div v-for="(category, index) in categories" :key="category.id" @click="activeCategory = category.id"
                :class="{
                    'bg-white text-[#F9771C] border-l-4 border-[#F9771C]': activeCategory === category.id,
                    'bg-[#F8F8F8] text-gray-700 hover:bg-[#F2F2F2]': activeCategory !== category.id
                }"
                class="h-15 px-2 flex items-center justify-between cursor-pointer transition-all duration-200 relative"
                :style="index < categories.length - 1 ? 'border-bottom: 1px solid #EEEEEE' : ''">
                <span class="text-base font-medium ml-2">{{ category.name }}</span>
                <i v-if="activeCategory === category.id" class="fas fa-chevron-right text-gray-400 text-xs mr-4"></i>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

const categories = [
  { id: 1, name: "招牌推荐" },
  { id: 2, name: "荤菜类" },
  { id: 3, name: "素菜类" },
  { id: 4, name: "丸子类" },
  { id: 5, name: "豆制品" },
  { id: 6, name: "主食类" },
  { id: 7, name: "饮品" },
];

/* 实际代码
const props = defineProps<{
    activeCategory: number;
}>();

const activeCategory = props.activeCategory;
*/

// 测试使用，最后删除
const activeCategory = ref(1);

</script> 

<style scoped>
.h-15 {
  height: 60px;
}
</style>