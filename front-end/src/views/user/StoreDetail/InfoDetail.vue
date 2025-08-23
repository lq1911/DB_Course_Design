<template>
    <StoreIntro v-if="storeInfo" :storeInfo="storeInfo" />

    <!-- 加载中 -->
    <div v-else class="flex justify-center items-center h-64">
        <i class="fas fa-spinner fa-spin text-3xl text-[#F9771C]"></i>
    </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'

import type { StoreInfo } from '@/api/user_store_info'
import { getStoreInfo } from '@/api/user_store_info'

import StoreIntro from '@/components/user/StoreDetail/InfoView/StoreInfo.vue'

const route = useRoute();
const storeID = computed(() => route.params.id as string);
const storeInfo = ref<StoreInfo>();

onMounted(async () => {
    storeInfo.value = await getStoreInfo(storeID.value);
})

watch(storeID, async (newID, oldID) => {
    if (newID != oldID) {
        storeInfo.value = await getStoreInfo(storeID.value);
    }
})

</script>