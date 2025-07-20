<template>
    <div class="relative">
        <input type="text" placeholder="搜索美食、商家"
            class="w-64 pl-10 pr-4 py-2 bg-gray-100 border-none rounded-full text-sm focus:outline-none focus:ring-2 focus:ring-orange-500"
            v-model="searchQuery" />
        <i class="fas fa-search absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-500 text-sm"></i>
    </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

const searchQuery = ref("");

</script>