<template>
    <div v-if="activeTab === 'home'" class="space-y-12">
        <!-- Hero 轮播区域 -->
        <section class="relative h-96 overflow-hidden bg-gradient-to-r from-orange-400 to-red-500">
            <div class="absolute inset-0">
                <img src="https://readdy.ai/api/search-image?query=delicious%20food%20delivery%20banner%20with%20fresh%20ingredients%20colorful%20vegetables%20fruits%20and%20prepared%20meals%20on%20clean%20white%20background%20modern%20food%20photography%20style%20high%20quality%20commercial%20food%20styling&width=1440&height=384&seq=hero001&orientation=landscape"
                    alt="美食外卖横幅" class="w-full h-full object-cover object-top" />
                <div class="absolute inset-0 bg-black bg-opacity-30"></div>
            </div>
            <div class="relative z-10 h-full flex items-center justify-center">
                <div class="text-center text-white">
                    <h1 class="text-5xl font-bold mb-4">新鲜美食 快速送达</h1>
                    <p class="text-xl mb-8">精选优质商家，30 分钟内送达您的美食</p>
                    <button
                        class="bg-orange-500 hover:bg-orange-600 text-white px-8 py-3 rounded-full text-lg font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap">
                        立即点餐
                    </button>
                </div>
            </div>
        </section>
        <!-- 美食分类 -->
        <section class="max-w-screen-xl mx-auto px-6">
            <h2 class="text-2xl font-bold text-gray-800 mb-8">热门分类</h2>
            <div class="grid grid-cols-8 gap-6">
                <div v-for="(category, index) in categories" :key="index" class="text-center group cursor-pointer">
                    <div
                        class="w-16 h-16 mx-auto mb-3 rounded-full overflow-hidden group-hover:scale-110 transition-transform">
                        <img :src="category.image" :alt="category.name" class="w-full h-full object-cover object-top" />
                    </div>
                    <span class="text-sm text-gray-600 group-hover:text-orange-500">{{ category.name }}</span>
                </div>
            </div>
        </section>
        <!-- 优惠活动 -->
        <section class="max-w-screen-xl mx-auto px-6">
            <h2 class="text-2xl font-bold text-gray-800 mb-8">限时优惠</h2>
            <div class="grid grid-cols-3 gap-6">
                <div v-for="(promo, index) in promotions" :key="index"
                    class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow cursor-pointer">
                    <img :src="promo.image" :alt="promo.title" class="w-full h-48 object-cover object-top" />
                    <div class="p-4">
                        <h3 class="font-bold text-lg mb-2">{{ promo.title }}</h3>
                        <p class="text-gray-600 text-sm">{{ promo.description }}</p>
                    </div>
                </div>
            </div>
        </section>
        <!-- 人气商家 -->
        <section class="max-w-screen-xl mx-auto px-6 pb-12">
            <h2 class="text-2xl font-bold text-gray-800 mb-8">人气商家</h2>
            <div class="grid grid-cols-4 gap-6">
                <div v-for="(restaurant, index) in popularRestaurants" :key="index"
                    class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow cursor-pointer">
                    <img :src="restaurant.image" :alt="restaurant.name" class="w-full h-40 object-cover object-top" />
                    <div class="p-4">
                        <h3 class="font-bold text-lg mb-2">{{ restaurant.name }}</h3>
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
    </div>

</template>

<script lang="ts" setup>
import { ref } from "vue";

const activeTab = ref("home");

// 美食分类
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

// 优惠活动
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

// 人气商家
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

<style scoped>
.\!rounded-button {
  border-radius: 8px;
}

</style>