<template>
        <div class="mt-5 bg-white border-2 rounded-lg shadow-sm p-6">
            <div class="w-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <div v-for="item in getCurrentMenuItems()" :key="item.id"
                    class="bg-white border border-gray-200 rounded-lg overflow-hidden hover:shadow-md transition-shadow">
                    <img :src="item.image" class="w-full h-48 object-cover object-top" />
                    <div class="p-4">
                        <h4 class="font-semibold text-gray-900 mb-2">{{ item.name }}</h4>
                        <p class="text-sm text-gray-600 mb-3 line-clamp-2">{{ item.description }}</p>
                        <div class="flex items-center justify-between">
                            <div class="flex items-center space-x-2">
                                <span class="text-lg font-bold text-[#F9771C]">¥{{ item.price }}</span>
                            </div>
                            <div class="flex items-center space-x-2">
                                <button @click="emit('decrease', item.id)" :disabled="!getItemQuantity(item.id)"
                                    class="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 disabled:opacity-50 cursor-pointer !rounded-button whitespace-nowrap">
                                    <i class="fas fa-minus text-xs"></i>
                                </button>
                                <span class="w-8 text-center">{{ getItemQuantity(item.id) || 0 }}</span>
                                <button @click="emit('increase', item.id)"
                                    class="w-8 h-8 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer !rounded-button whitespace-nowrap">
                                    <i class="fas fa-plus text-xs"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue'

const props = defineProps<{
    categories: Array<{ id: number, name: string }>;
    activeCategory: number;
    cart: Record<number, number>;
    menuItems: Array<{
        id: number,
        categoryId: number,
        name: string,
        description: string,
        price: number,
        image: string
    }>;
}>();

const getCurrentCategory = () => props.categories.find(cat => cat.id === props.activeCategory);
const getCurrentMenuItems = () => props.menuItems.filter(item => item.categoryId === props.activeCategory);
const getItemQuantity = (itemId: number) => props.cart[itemId] || 0;

const emit = defineEmits<{
    (e: 'increase', itemId: number): void;
    (e: 'decrease', itemId: number): void;
}>();

</script>