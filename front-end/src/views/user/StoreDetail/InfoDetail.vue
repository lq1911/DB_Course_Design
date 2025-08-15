<template>
  <StoreIntro :storeInfo="storeInfo" />
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue'
import { useRoute } from 'vue-router'

import type { StoreInfo } from '@/api/store_info'
import { getStoreInfo } from '@/api/store_info'

import StoreIntro from '@/components/user/StoreDetail/InfoView/StoreInfo.vue'

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