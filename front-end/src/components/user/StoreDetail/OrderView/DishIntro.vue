<template>
    <div class="mt-5 mr-10 ml-5 w-full bg-white border-2 rounded-lg shadow-sm p-2">
        <div v-if="getCurrentMenuItems().length > 0" class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            <div v-for="item in getCurrentMenuItems()" :key="item.id">
                <div v-if="item" class="bg-white border border-gray-200 rounded-lg overflow-hidden hover:shadow-md">
                    <div class="w-full h-56 overflow-hidden">
                        <img :src="item.image || defaultImage" class="w-full h-84 object-cover object-top" />
                    </div>
                    <div class="p-4">
                        <h4 class="font-semibold text-gray-900 mb-2">{{ item.name }}</h4>
                        <p class="text-sm text-gray-600 mb-3 line-clamp-2 h-10">{{ item.description }}</p>
                        <div class="flex items-center justify-between">
                            <div class="flex items-center space-x-2">
                                <span class="text-lg font-bold text-[#F9771C]">¥{{ item.price }}</span>
                            </div>
                            <div v-if="!isItemSoldOut(item.isSoldOut)" class="flex items-center space-x-2">
                                <button @click="emit('decrease', item)" :disabled="!getItemQuantity(item.id)"
                                    class="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 disabled:opacity-50 cursor-pointer">
                                    <i class="fas fa-minus text-xs"></i>
                                </button>
                                <span class="w-8 text-center">{{ getItemQuantity(item.id) || 0 }}</span>
                                <button @click="emit('increase', item)"
                                    class="w-8 h-8 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer">
                                    <i class="fas fa-plus text-xs"></i>
                                </button>
                            </div>
                            <div v-else>
                                <span>已售罄</span>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div v-else class="flex justify-center items-center py-10 text-gray-500">
            抱歉，该菜单下暂时没有菜品~
        </div>
    </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue'
import type { MenuItem, ShoppingCart } from '@/api/user_checkout';

const props = defineProps<{
    categories: Array<{ id: number, name: string }>;
    activeCategory: number;
    cart: ShoppingCart;
    menuItems: MenuItem[];
}>();

const defaultImage = "https://media.istockphoto.com/id/520410807/zh/%E7%85%A7%E7%89%87/cheeseburger.jpg?s=612x612&w=0&k=20&c=U5V_0yjY1KGcqKLmNZjuDtNZJpYl3QPc-3_fAOQGKgI=";


const isItemSoldOut = (isSoldOut?: number) => isSoldOut == 0; // 等于0返回False

const getCurrentMenuItems = () => props.menuItems.filter(item => item.categoryId === props.activeCategory);

const getItemQuantity = (dishId: number) => {
    const item = props.cart.items.find(i => i.dishId === dishId);
    return item ? item.quantity : 0;
}

const emit = defineEmits<{
    (e: 'increase', item: MenuItem): void;
    (e: 'decrease', item: MenuItem): void;
}>();
</script>