<template>
    <div>
        <StoreNav :storeInfo="storeInfo" :storeID="storeID" />
        <router-view />
    </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { computed, ref, onMounted, watch } from 'vue'

import type { StoreInfo } from '@/api/store_info'
import { getStoreInfo } from '@/api/store_info'

import StoreNav from '@/components/user/StoreDetail/StoreNav.vue';

const route = useRoute();
const storeID = computed(() => route.params.id as string);
const storeInfo = ref<StoreInfo | null>(null);

onMounted(async () => {
    storeInfo.value = await getStoreInfo(storeID.value);
})

watch(storeID, async (newID, oldID) => {
    if (newID != oldID) {
        storeInfo.value = await getStoreInfo(storeID.value);
    }
})

</script>