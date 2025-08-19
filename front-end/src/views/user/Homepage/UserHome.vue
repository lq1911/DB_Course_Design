<template>
    <main class="pt-20 min-h-screen space-y-12">
        <HeroBanner />
        <FoodCategory />
        <Discount />
        <PopularStore />
    </main>
</template>

<script lang="ts" setup>
import HeroBanner from '@/components/user/HomePage/Home/HeroBanner.vue';
import FoodCategory from '@/components/user/HomePage/Home/FoodCategory.vue';
import Discount from '@/components/user/HomePage/Home/Discount.vue';
import PopularStore from '@/components/user/HomePage/Home/PopularStore.vue';

</script>