<template>
    <nav class="fixed top-0 left-0 right-0 bg-white shadow-md z-50">
        <div class="w-full max-w-screen-xl mx-auto px-6 py-4 flex items-center justify-between">
            <!-- Logo -->
            <div class="flex items-center space-x-2">
                <i class="fas fa-utensils text-orange-500 text-2xl"></i>
                <span class="text-xl font-bold text-gray-800">{{ name }}</span>
            </div>
            <!-- 主导航 -->
            <div class="flex items-center space-x-8">
                <button v-for="nav in navItems" 
                @click="goToPage(nav.path)" 
                :class="{
                    'text-orange-500 border-b-2 border-orange-500': route.path === nav.path,
                    'text-gray-600 hover:text-orange-500': route.path !==  nav.path
                }" class="px-4 py-2 font-medium transition-colors duration-200 cursor-pointer whitespace-nowrap !rounded-button">
                    {{ nav.label }}
                </button>
            </div>
            <!-- 右侧功能区 -->
            <div class="flex items-center space-x-4">
                <SearchBar />
                <Personal />
            </div>
        </div>
    </nav>
</template>

<script lang="ts" setup>
import { useRoute } from "vue-router";
import { getProjectName } from '@/stores/name'
import SearchBar from "@/components/user/HomePage/SearchBar.vue";
import Personal from "@/components/user/HomePage/Personal.vue";
import router from "@/router";

const name = getProjectName().projectName;
const route = useRoute();

// 导航菜单
const navItems = [
    { path: "/home/intro", label: "首页" },
    { path: "/home/restaurants", label: "商家" },
    { path: "/home/orders", label: "订单" },
];


const goToPage = (path: string) => {
    router.push(path)
};

</script>
