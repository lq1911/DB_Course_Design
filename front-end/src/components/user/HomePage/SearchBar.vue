<template>
    <div class="relative">
        <input type="text" placeholder="输入商家名称~"
            class="w-64 pl-10 pr-4 py-2 bg-gray-100 border-none rounded-full text-sm focus:outline-none focus:ring-2 focus:ring-orange-500"
            v-model="searchQuery" @keyup.enter="handleSearch" />
        <i class="fas fa-search absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-500 text-sm"
            @click="handleSearch"></i>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from 'vue-router';

const router = useRouter();
const searchQuery = ref("");

// 等待添加获取用信息接口
const userID = 30;
const address = "沈阳大街";

function handleSearch() {
    const query = searchQuery.value.trim();
    if (!query) {
        return;
    }
    router.push({
        path: '/home/restaurants',
        query: { keyword: query, userID, address }
    });
    searchQuery.value = ''; // 跳转后清空输入框
}

</script>