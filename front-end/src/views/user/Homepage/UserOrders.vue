<template>
    <TopNav />

    <main class="pt-20 min-h-screen">
        <div class="max-w-screen-xl mx-auto px-6 py-8">
            <h1 class="text-3xl font-bold text-gray-800 mb-8 text-left">我的订单</h1>
            <!-- 订单状态标签 -->
            <div class="flex space-x-1 mb-8 bg-white rounded-lg p-2 shadow-sm">
                <button v-for="(status, index) in orderStatuses" :key="index" @click="activeOrderStatus = status.key"
                    :class="{
                        'bg-orange-500 text-white': activeOrderStatus === status.key,
                        'text-gray-600 hover:bg-gray-100': activeOrderStatus !== status.key
                    }"
                    class="px-6 py-2 rounded-lg font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap">
                    {{ status.label }}
                </button>
            </div>
            <!-- 订单列表 -->
            <div class="space-y-4">
                <div v-for="(order, index) in filteredOrders" :key="index"
                    class="bg-white rounded-lg shadow-md p-6 text-left">
                    <div class="flex justify-between items-start mb-4">
                        <div>
                            <h3 class="font-bold text-lg">{{ order.restaurantName }}</h3>
                            <p class="text-gray-600 text-sm">
                                订单号：{{ order.orderNumber }}
                            </p>
                            <p class="text-gray-600 text-sm">
                                下单时间：{{ order.orderTime }}
                            </p>
                        </div>
                        <span :class="{
                            'text-orange-500': order.status === 'pending',
                            'text-blue-500': order.status === 'delivering',
                            'text-green-500': order.status === 'completed',
                            'text-red-500': order.status === 'cancelled'
                        }" class="font-medium">
                            {{ getOrderStatusText(order.status) }}
                        </span>
                    </div>
                    <div class="border-t pt-4">
                        <div class="flex justify-between items-center">
                            <div class="flex items-center space-x-4">
                                <img :src="order.image" :alt="order.restaurantName"
                                    class="w-16 h-16 rounded-lg object-cover object-top" />
                                <div>
                                    <p class="font-medium">{{ order.items }}</p>
                                    <p class="text-gray-600 text-sm">
                                        共 {{ order.itemCount }} 件商品
                                    </p>
                                </div>
                            </div>
                            <div class="text-right">
                                <p class="font-bold text-lg">¥{{ order.totalAmount }}</p>
                                <div class="flex space-x-2 mt-2">
                                    <button v-if="order.status === 'completed'"
                                        class="bg-gray-100 hover:bg-gray-200 text-gray-700 px-4 py-1 rounded text-sm transition-colors cursor-pointer !rounded-button whitespace-nowrap">
                                        再来一单
                                    </button>
                                    <button v-if="order.status === 'completed'"
                                        class="bg-orange-500 hover:bg-orange-600 text-white px-4 py-1 rounded text-sm transition-colors cursor-pointer !rounded-button whitespace-nowrap">
                                        评价
                                    </button>
                                    <button v-if="order.status === 'delivering'"
                                        class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-1 rounded text-sm transition-colors cursor-pointer !rounded-button whitespace-nowrap">
                                        查看物流
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

    <Footer />
</template>

<script lang="ts" setup>
import { ref, computed } from "vue";
import TopNav from '@/components/user/HomePage/TopNav.vue';
import Footer from '@/components/user/Footer.vue';

const activeOrderStatus = ref("all");

// 订单状态
const orderStatuses = [
    { key: "all", label: "全部订单" },
    { key: "pending", label: "待付款" },
    { key: "delivering", label: "配送中" },
    { key: "completed", label: "已完成" },
];
// 订单数据
const orders = [
    {
        orderNumber: "DD202501170001",
        restaurantName: "蜀香麻辣烫",
        orderTime: "2025-01-17 12:30",
        status: "completed",
        items: "招牌麻辣烫 x1, 鸭血豆腐 x1",
        itemCount: 2,
        totalAmount: "35.50",
        image:
            "https://readdy.ai/api/search-image?query=spicy%20hot%20pot%20restaurant%20logo%20and%20food%20display%20on%20clean%20white%20background%20commercial%20style&width=64&height=64&seq=order001&orientation=squarish",
    },
    {
        orderNumber: "DD202501170002",
        restaurantName: "港记茶餐厅",
        orderTime: "2025-01-16 18:45",
        status: "delivering",
        items: "叉烧包 x3, 奶茶 x2",
        itemCount: 5,
        totalAmount: "42.00",
        image:
            "https://readdy.ai/api/search-image?query=hong%20kong%20tea%20restaurant%20logo%20and%20dim%20sum%20display%20on%20clean%20white%20background%20commercial%20style&width=64&height=64&seq=order002&orientation=squarish",
    },
    {
        orderNumber: "DD202501170003",
        restaurantName: "一兰拉面",
        orderTime: "2025-01-15 19:20",
        status: "completed",
        items: "豚骨拉面 x2, 煎饺 x1",
        itemCount: 3,
        totalAmount: "78.00",
        image:
            "https://readdy.ai/api/search-image?query=japanese%20ramen%20restaurant%20logo%20and%20noodle%20bowls%20on%20clean%20white%20background%20commercial%20style&width=64&height=64&seq=order003&orientation=squarish",
    },
    {
        orderNumber: "DD202501170004",
        restaurantName: "必胜客",
        orderTime: "2025-01-14 20:15",
        status: "pending",
        items: "玛格丽特披萨 x1, 意面 x1",
        itemCount: 2,
        totalAmount: "89.00",
        image:
            "https://readdy.ai/api/search-image?query=pizza%20restaurant%20logo%20and%20italian%20food%20display%20on%20clean%20white%20background%20commercial%20style&width=64&height=64&seq=order004&orientation=squarish",
    },
];

// 计算属性
const filteredOrders = computed(() => {
    if (activeOrderStatus.value === "all") {
        return orders;
    }
    return orders.filter((order) => order.status === activeOrderStatus.value);
});

const getOrderStatusText = (status: string) => {
    const statusMap: { [key: string]: string } = {
        pending: "待付款",
        delivering: "配送中",
        completed: "已完成",
        cancelled: "已取消",
    };
    return statusMap[status] || "未知状态";
};

</script>