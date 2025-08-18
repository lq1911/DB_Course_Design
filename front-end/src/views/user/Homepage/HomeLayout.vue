<template>
    <div>
        <TopNav />
        <router-view />
        <Footer />
    </div>
</template>

<script setup lang="ts">

import TopNav from '@/components/user/HomePage/TopNav.vue';
import Footer from '@/components/user/Footer.vue';

</script>