<template>
  <section class="max-w-screen-xl mx-auto px-6">
    <h2 class="text-2xl font-bold text-gray-800 mb-8 text-left">热门分类</h2>
    <div class="grid grid-cols-8 gap-6">
      <div v-for="(category, index) in categories" :key="index" class="text-center group cursor-pointer">
        <div
          class="w-16 h-16 mx-auto mb-3 rounded-full overflow-hidden group-hover:scale-110 transition-transform"
        >
          <img :src="category.image" :alt="category.name" class="w-full h-full object-cover object-top" />
        </div>
        <span class="text-sm text-gray-600 group-hover:text-orange-500">{{ category.name }}</span>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
const categories = [
  {
    name: "中式菜品",
    image:
      "https://readdy.ai/api/search-image?query=traditional%20chinese%20cuisine%20dishes%20with%20rice%20noodles%20and%20vegetables%20on%20clean%20white%20background%20food%20photography%20style%20commercial%20quality&width=64&height=64&seq=cat001&orientation=squarish",
  },
  {
    name: "西式快餐",
    image:
      "https://readdy.ai/api/search-image?query=western%20fast%20food%20burger%20fries%20and%20drinks%20on%20clean%20white%20background%20commercial%20food%20photography%20style%20high%20quality&width=64&height=64&seq=cat002&orientation=squarish",
  },
  {
    name: "日韩料理",
    image:
      "https://readdy.ai/api/search-image?query=japanese%20korean%20cuisine%20sushi%20ramen%20and%20kimchi%20on%20clean%20white%20background%20food%20photography%20style%20commercial%20quality&width=64&height=64&seq=cat003&orientation=squarish",
  },
  {
    name: "甜品饮品",
    image:
      "https://readdy.ai/api/search-image?query=desserts%20and%20beverages%20cakes%20coffee%20and%20bubble%20tea%20on%20clean%20white%20background%20food%20photography%20style%20commercial%20quality&width=64&height=64&seq=cat004&orientation=squarish",
  },
  {
    name: "火锅烧烤",
    image:
      "https://readdy.ai/api/search-image?query=hotpot%20and%20barbecue%20grilled%20meat%20and%20vegetables%20on%20clean%20white%20background%20food%20photography%20style%20commercial%20quality&width=64&height=64&seq=cat005&orientation=squarish",
  },
  {
    name: "小食零食",
    image:
      "https://readdy.ai/api/search-image?query=snacks%20and%20light%20meals%20chips%20nuts%20and%20crackers%20on%20clean%20white%20background%20food%20photography%20style%20commercial%20quality&width=64&height=64&seq=cat006&orientation=squarish",
  },
  {
    name: "健康轻食",
    image:
      "https://readdy.ai/api/search-image?query=healthy%20light%20meals%20salads%20and%20fresh%20vegetables%20on%20clean%20white%20background%20food%20photography%20style%20commercial%20quality&width=64&height=64&seq=cat007&orientation=squarish",
  },
  {
    name: "地方特色",
    image:
      "https://readdy.ai/api/search-image?query=local%20specialty%20dishes%20traditional%20regional%20cuisine%20on%20clean%20white%20background%20food%20photography%20style%20commercial%20quality&width=64&height=64&seq=cat008&orientation=squarish",
  },
];
</script>
