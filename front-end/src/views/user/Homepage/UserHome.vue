<template>
    <main class="pt-8 min-h-screen space-y-12">
        <TopNav />
        <HeroBanner />
        <FoodCategory />
        <Discount />
        <PopularStore />
        <Footer />
    </main>
</template>

<script lang="ts" setup>
import TopNav from '@/components/user/HomePage/TopNav.vue';
import HeroBanner from '@/components/user/HomePage/HeroBanner.vue';
import FoodCategory from '@/components/user/HomePage/FoodCategory.vue';
import Discount from '@/components/user/HomePage/Discount.vue';
import PopularStore from '@/components/user/HomePage/PopularStore.vue';
import Footer from '@/components/user/Footer.vue';

</script>