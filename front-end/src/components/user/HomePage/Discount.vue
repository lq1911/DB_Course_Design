<template>
  <section class="max-w-screen-xl mx-auto px-6">
    <h2 class="text-2xl font-bold text-gray-800 mb-8 text-left">限时优惠</h2>
    <div class="grid grid-cols-3 gap-6">
      <div
        v-for="(promo, index) in promotions"
        :key="index"
        class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow cursor-pointer text-left"
      >
        <img :src="promo.image" :alt="promo.title" class="w-full h-48 object-cover object-top" />
        <div class="p-4">
          <h3 class="font-bold text-xl mb-2 font-black font-yahei">{{ promo.title }}</h3>
          <p class="text-gray-600 text-sm">{{ promo.description }}</p>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
const promotions = [
  {
    title: "新用户专享",
    description: "首单立减 20 元，满 30 元可用",
    image:
      "https://readdy.ai/api/search-image?query=new%20user%20promotion%20banner%20with%20discount%20coupon%20and%20fresh%20food%20delivery%20on%20clean%20white%20background%20commercial%20design%20style%20high%20quality&width=400&height=192&seq=promo001&orientation=landscape",
  },
  {
    title: "周末特惠",
    description: "精选商家 8 折起，限时抢购",
    image:
      "https://readdy.ai/api/search-image?query=weekend%20special%20offer%20banner%20with%20discounted%20meals%20and%20promotional%20design%20on%20clean%20white%20background%20commercial%20style%20high%20quality&width=400&height=192&seq=promo002&orientation=landscape",
  },
  {
    title: "满减活动",
    description: "满 50 减 10，满 100 减 25",
    image:
      "https://readdy.ai/api/search-image?query=discount%20promotion%20banner%20with%20money%20saving%20offers%20and%20delicious%20food%20on%20clean%20white%20background%20commercial%20design%20style%20high%20quality&width=400&height=192&seq=promo003&orientation=landscape",
  },
];
</script>
