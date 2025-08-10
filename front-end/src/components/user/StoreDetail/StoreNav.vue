<template>
    <div class="relative bg-gradient-to-r from-orange-50 to-orange-100 shadow-sm overflow-hidden">
        <!--返回按钮-->
        <button @click="goBack"
            class="absolute left-4 top-4 flex items-center bg-white shadow-lg px-4 py-2 rounded-2xl z-10 hover:bg-gray-100">
            <i class="fas fa-arrow-left mr-2"></i>
            返回
        </button>

        <!--背景图标-->
        <div>
            <div class="absolute top-0 right-0 w-1/3 h-full opacity-10">
                <i
                    class="fas fa-utensils text-orange-500 text-[300px] transform rotate-12 translate-x-1/4 -translate-y-1/4"></i>
            </div>
            <div class="absolute bottom-0 left-0 w-1/4 h-full opacity-10">
                <i
                    class="fas fa-pepper-hot text-orange-500 text-[200px] transform -rotate-12 -translate-x-1/4 translate-y-1/4"></i>
            </div>
        </div>

        <!--简要介绍-->
        <div class="max-w-7xl mx-auto px-4 py-8 relative">
            <div class="flex items-center space-x-8">
                <div class="relative">
                    <img :src="storeInfo.image" alt="商家头像" class="w-28 h-28 rounded-2xl object-cover shadow-lg" />
                    <div
                        class="absolute -bottom-2 -right-2 bg-orange-500 text-white px-3 py-1 rounded-full text-sm font-medium">
                        <i class="fas fa-check-circle mr-1"></i>认证商家
                    </div>
                </div>
                <div class="flex-1">
                    <div class="flex items-center space-x-4 mb-3">
                        <h1 class="text-3xl font-bold text-gray-900">
                            {{ storeInfo.name }}
                        </h1>
                        <div class="flex items-center px-3 py-1 bg-orange-500 bg-opacity-10 rounded-full">
                            <i class="fas fa-crown text-orange-500 mr-2"></i>
                            <span class="text-orange-600 font-medium">优质商家</span>
                        </div>
                    </div>
                    <div class="flex items-center space-x-6 text-sm mb-3">
                        <div class="flex items-center bg-white px-3 py-1.5 rounded-full shadow-sm">
                            <i class="fas fa-star text-yellow-400 mr-2"></i>
                            <span class="font-medium text-gray-900">{{ storeInfo.rating }}</span>
                        </div>
                        <div class="flex items-center">
                            <i class="fas fa-shopping-bag text-gray-500 mr-2"></i>
                            <span>月售 {{ storeInfo.monthlySales }} 单</span>
                        </div>
                        <div class="flex items-center">
                            <i class="fas fa-truck text-gray-500 mr-2"></i>
                            <span>配送费 ¥ {{ deliveryTask.deliveryFee }}</span>
                        </div>
                        <div class="flex items-center">
                            <i class="fas fa-clock text-gray-500 mr-2"></i>
                            <span>配送时间 {{ deliveryTask.deliveryTime }} 分钟</span>
                        </div>
                    </div>
                    <p class="text-gray-700 max-w-2xl">{{ storeInfo.description }}</p>
                </div>
            </div>
        </div>

        <!-- Tab导航栏 -->
        <div class="bg-white border-b">
            <div class="max-w-7xl mx-auto px-4">
                <div class="flex space-x-8">
                    <button v-for="tab in tabs"
                    @click="goToPage(tab.path)" 
                    :class="{
                        'border-b-2 border-[#F9771C] text-[#F9771C]': route.path === tab.path,
                        'text-gray-600 hover:text-gray-900': route.path !== tab.path
                    }" class="py-4 px-2 font-medium transition-colors cursor-pointer z-10">
                    {{ tab.label }}
                    </button>
                </div>
            </div>
        </div>
    </div>

</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute();
const router = useRouter();

const storeID = computed(() => route.params.id);

const tabs = computed( () => [
    { path: `/store/${storeID.value}/order`, label: "点餐界面" },
    { path: `/store/${storeID.value}/comment`, label: "评价" },
    { path: `/store/${storeID.value}/info`, label: "商家" },
]);

//测试使用
import { storeInfo } from '@/api/store_info'
import { deliveryTask } from '@/api/store_info'

function goBack() {
    router.push(`/home`);
}

function goToPage(path: string) {
    router.push(path);
}

</script>