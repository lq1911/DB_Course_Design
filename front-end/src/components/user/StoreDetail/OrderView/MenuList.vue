<template>
    <div class="w-1/6 bg-white rounded-lg shadow-sm ml-[5%] mt-5 border-2" style="box-shadow: 2px 0 4px rgba(0,0,0,0.05)">
        <div class="py-4">
            <div v-for="(category, index) in props.categories" @click="selectCategory(category.id)"
                :class="{
                    'bg-white text-[#F9771C] border-l-4 border-[#F9771C]': props.activeCategory === category.id,
                    'bg-[#F8F8F8] text-gray-700 hover:bg-[#F2F2F2]': props.activeCategory !== category.id
                }"
                class="h-16 px-2 flex items-center justify-between cursor-pointer transition-all duration-200 relative"
                :style="index < props.categories.length - 1 ? 'border-bottom: 1px solid #EEEEEE' : ''">
                <span class="text-base font-medium ml-2">{{ category.name }}</span>
                <i v-if="props.activeCategory === category.id" class="fas fa-chevron-right text-gray-400 text-xs mr-4"></i>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from "vue"

const props = defineProps < {
    categories: Array <{ id: number; name: string }>
    activeCategory: number;
}>();

const emit = defineEmits(['update:activeCategory'])

function selectCategory(id: number) {
    emit('update:activeCategory', id);
}

</script> 