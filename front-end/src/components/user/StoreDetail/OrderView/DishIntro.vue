<template>
    <div class="flex-1 pl-6">
        <div class="bg-white rounded-lg shadow-sm p-6">
            <h3 class="text-xl font-bold text-gray-900 mb-6">
                {{ getCurrentCategory()?.name }}
            </h3>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <div v-for="item in getCurrentMenuItems()" :key="item.id"
                    class="bg-white border border-gray-200 rounded-lg overflow-hidden hover:shadow-md transition-shadow">
                    <img :src="item.image" alt="商品图片" class="w-full h-48 object-cover object-top" />
                    <div class="p-4">
                        <h4 class="font-semibold text-gray-900 mb-2">
                            {{ item.name }}
                        </h4>
                        <p class="text-sm text-gray-600 mb-3 line-clamp-2">
                            {{ item.description }}
                        </p>
                        <div class="flex items-center justify-between">
                            <div class="flex items-center space-x-2">
                                <span class="text-lg font-bold text-[#F9771C]">¥{{ item.price }}</span>
                                <span v-if="item.originalPrice" class="text-sm text-gray-500 line-through">¥{{
                                    item.originalPrice }}</span>
                            </div>
                            <div class="flex items-center space-x-2">
                                <button @click="decreaseQuantity(item.id)" :disabled="!getItemQuantity(item.id)"
                                    class="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 disabled:opacity-50 cursor-pointer !rounded-button whitespace-nowrap">
                                    <i class="fas fa-minus text-xs"></i>
                                </button>
                                <span class="w-8 text-center">{{ getItemQuantity(item.id) || 0 }}</span>
                                <button @click="increaseQuantity(item.id)"
                                    class="w-8 h-8 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer !rounded-button whitespace-nowrap">
                                    <i class="fas fa-plus text-xs"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">

const getCurrentCategory = () => {
  return categories.find((cat) => cat.id === activeCategory.value);
};

const getCurrentMenuItems = () => {
  return menuItems.filter((item) => item.categoryId === activeCategory.value);
};
const getItemQuantity = (itemId: number) => {
  return cart.value[itemId] || 0;
};
const increaseQuantity = (itemId: number) => {
  cart.value[itemId] = (cart.value[itemId] || 0) + 1;
};
const decreaseQuantity = (itemId: number) => {
  if (cart.value[itemId] && cart.value[itemId] > 0) {
    cart.value[itemId]--;
    if (cart.value[itemId] === 0) {
      delete cart.value[itemId];
    }
  }
};

</script>