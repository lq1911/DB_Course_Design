<template>
    <div>
        <StoreNav :storeInfo="storeInfo" :storeID="storeID" :deliveryTask="deliveryTask" />
        <router-view />
        <Footer />
    </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { computed, ref, onMounted, watch } from 'vue'

import type { StoreInfo } from '@/api/user_store_info'
import { getDeliveryTasks, getStoreInfo } from '@/api/user_store_info'

import Footer from '@/components/user/Footer.vue'
import StoreNav from '@/components/user/StoreDetail/StoreNav.vue';

const route = useRoute();
const storeID = computed(() => route.params.id as string);
const storeInfo = ref<StoreInfo>({
    id: 0,
    name: '加载中...',
    image: '',
    address: '',
    businessHours: '',
    rating: 0,
    monthlySales: 0,
    description: '',
    createTime: ''
});
const deliveryTask = getDeliveryTasks();

onMounted(async () => {
    storeInfo.value = await getStoreInfo(storeID.value);
})

watch(storeID, async (newID, oldID) => {
    if (newID != oldID) {
        storeInfo.value = await getStoreInfo(storeID.value);
    }
})

</script>