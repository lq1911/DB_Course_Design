<template>
    <main class="pt-20 min-h-screen">
        <TopNav />

        <div v-if="route.path === '/recommend'" class="max-w-screen-xl mx-auto px-6 py-8">
            <div class="flex justify-between items-center mb-8">
                <h1 class="text-3xl font-bold text-gray-800">为您推荐</h1>
                <div class="flex items-center space-x-4">
                    <select
                        class="px-4 py-2 border border-gray-300 rounded-lg text-sm focus:outline-none focus:ring-2 focus:ring-orange-500">
                        <option>综合排序</option>
                        <option>销量优先</option>
                        <option>评分优先</option>
                        <option>价格优先</option>
                    </select>
                </div>
            </div>
            <div class="grid grid-cols-4 gap-6">
                <div v-for="(item, index) in recommendedItems" :key="index"
                    class="bg-white rounded-lg shadow-md overflow-hidden hover:shadow-lg transition-shadow cursor-pointer text-left">
                    <img :src="item.image" :alt="item.name" class="w-full h-48 object-cover object-top" />
                    <div class="p-4">
                        <h3 class="font-bold text-lg mb-2 font-black font-yahei">{{ item.name }}</h3>
                        <p class="text-gray-600 text-sm mb-3">{{ item.description }}</p>
                        <div class="flex items-center justify-between">
                            <span class="text-orange-500 font-bold text-lg">¥{{ item.price }}</span>
                            <div class="flex items-center text-sm text-gray-600">
                                <i class="fas fa-star text-yellow-400 mr-1"></i>
                                <span>{{ item.rating }}</span>
                                <span class="ml-2">月售{{ item.sales }}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</template>

<script lang="ts" setup>
import TopNav from '@/components/user/TopNav.vue';
import { useRoute } from 'vue-router';

const route = useRoute();

// 推荐商品
const recommendedItems = [
    {
        name: "招牌麻辣烫",
        description: "精选食材，香辣可口",
        price: "28",
        rating: "4.8",
        sales: "1200",
        image:
            "https://readdy.ai/api/search-image?query=spicy%20hot%20pot%20with%20fresh%20vegetables%20tofu%20and%20noodles%20in%20red%20broth%20on%20clean%20white%20background%20commercial%20food%20photography%20style%20high%20quality&width=300&height=192&seq=food001&orientation=landscape",
    },
    {
        name: "港式叉烧包",
        description: "传统工艺，皮薄馅多",
        price: "18",
        rating: "4.6",
        sales: "850",
        image:
            "https://readdy.ai/api/search-image?query=hong%20kong%20style%20char%20siu%20bao%20steamed%20buns%20with%20barbecue%20pork%20filling%20on%20clean%20white%20background%20commercial%20food%20photography%20style&width=300&height=192&seq=food002&orientation=landscape",
    },
    {
        name: "日式豚骨拉面",
        description: "浓郁汤底，Q弹面条",
        price: "35",
        rating: "4.7",
        sales: "960",
        image:
            "https://readdy.ai/api/search-image?query=japanese%20tonkotsu%20ramen%20with%20rich%20pork%20bone%20broth%20soft%20boiled%20egg%20and%20green%20onions%20on%20clean%20white%20background%20commercial%20food%20photography&width=300&height=192&seq=food003&orientation=landscape",
    },
    {
        name: "意式玛格丽特披萨",
        description: "经典口味，新鲜番茄",
        price: "42",
        rating: "4.5",
        sales: "720",
        image:
            "https://readdy.ai/api/search-image?query=margherita%20pizza%20with%20fresh%20tomatoes%20mozzarella%20cheese%20and%20basil%20leaves%20on%20clean%20white%20background%20commercial%20food%20photography%20style&width=300&height=192&seq=food004&orientation=landscape",
    },
    {
        name: "韩式石锅拌饭",
        description: "营养均衡，口感丰富",
        price: "32",
        rating: "4.4",
        sales: "680",
        image:
            "https://readdy.ai/api/search-image?query=korean%20bibimbap%20in%20stone%20bowl%20with%20mixed%20vegetables%20rice%20and%20fried%20egg%20on%20clean%20white%20background%20commercial%20food%20photography%20style&width=300&height=192&seq=food005&orientation=landscape",
    },
    {
        name: "泰式绿咖喱鸡",
        description: "香辣椰浆，嫩滑鸡肉",
        price: "38",
        rating: "4.6",
        sales: "540",
        image:
            "https://readdy.ai/api/search-image?query=thai%20green%20curry%20chicken%20with%20coconut%20milk%20and%20vegetables%20served%20with%20jasmine%20rice%20on%20clean%20white%20background%20commercial%20food%20photography&width=300&height=192&seq=food006&orientation=landscape",
    },
    {
        name: "美式汉堡套餐",
        description: "厚实牛肉，配薯条可乐",
        price: "45",
        rating: "4.3",
        sales: "890",
        image:
            "https://readdy.ai/api/search-image?query=american%20burger%20meal%20with%20beef%20patty%20lettuce%20tomato%20served%20with%20french%20fries%20and%20cola%20on%20clean%20white%20background%20commercial%20food%20photography&width=300&height=192&seq=food007&orientation=landscape",
    },
    {
        name: "广式点心拼盘",
        description: "精致小食，一次品尝",
        price: "58",
        rating: "4.7",
        sales: "420",
        image:
            "https://readdy.ai/api/search-image?query=cantonese%20dim%20sum%20platter%20with%20various%20steamed%20dumplings%20and%20buns%20arranged%20beautifully%20on%20clean%20white%20background%20commercial%20food%20photography&width=300&height=192&seq=food008&orientation=landscape",
    },
];

</script>

<style scoped></style>