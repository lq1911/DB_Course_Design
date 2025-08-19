<template>
  <section class="max-w-screen-xl mx-auto px-6 pb-12">
    <h2 class="text-2xl font-bold text-gray-800 mb-8 text-left">人气商家</h2>
    <div class="grid grid-cols-4 gap-6">
      <div
        v-for="(restaurant, index) in popularRestaurants"
        :key="index"
        class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow cursor-pointer text-left"
      >
        <img :src="restaurant.image" :alt="restaurant.name" class="w-full h-40 object-cover object-top" />
        <div class="p-4">
          <h3 class="font-bold text-lg mb-2 font-bold">{{ restaurant.name }}</h3>
          <div class="flex items-center justify-between text-sm text-gray-600">
            <span class="flex items-center">
              <i class="fas fa-star text-yellow-400 mr-1"></i>
              {{ restaurant.rating }}
            </span>
            <span>{{ restaurant.deliveryTime }}</span>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
const popularRestaurants = [
  {
    name: "川香麻辣烫",
    rating: "4.8",
    deliveryTime: "25-35 分钟",
    image:
      "https://readdy.ai/api/search-image?query=sichuan%20spicy%20hot%20pot%20restaurant%20storefront%20with%20traditional%20chinese%20decoration%20and%20food%20display%20on%20clean%20background%20commercial%20photography%20style&width=300&height=160&seq=rest001&orientation=landscape",
  },
  {
    name: "港式茶餐厅",
    rating: "4.6",
    deliveryTime: "30-40 分钟",
    image:
      "https://readdy.ai/api/search-image?query=hong%20kong%20style%20tea%20restaurant%20with%20dim%20sum%20and%20traditional%20dishes%20on%20clean%20background%20commercial%20photography%20style%20high%20quality&width=300&height=160&seq=rest002&orientation=landscape",
  },
  {
    name: "日式拉面馆",
    rating: "4.7",
    deliveryTime: "20-30 分钟",
    image:
      "https://readdy.ai/api/search-image?query=japanese%20ramen%20restaurant%20with%20authentic%20noodle%20bowls%20and%20traditional%20interior%20on%20clean%20background%20commercial%20photography%20style&width=300&height=160&seq=rest003&orientation=landscape",
  },
  {
    name: "意式披萨屋",
    rating: "4.5",
    deliveryTime: "35-45 分钟",
    image:
      "https://readdy.ai/api/search-image?query=italian%20pizza%20restaurant%20with%20wood%20fired%20oven%20and%20fresh%20pizzas%20on%20clean%20background%20commercial%20photography%20style%20high%20quality&width=300&height=160&seq=rest004&orientation=landscape",
  },
];
</script>
