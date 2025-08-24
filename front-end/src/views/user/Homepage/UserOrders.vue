<template>
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

            <div v-if="showLoading" class="flex justify-center items-center h-64">
                <i class="fas fa-spinner fa-spin text-3xl text-[#F9771C]"></i>
            </div>

            <!-- 订单列表 -->
            <div v-else class="space-y-4">
                <div v-for="order in filteredOrders" :key="order.orderID"
                    class="bg-white rounded-lg shadow-md p-6 text-left">

                    <!-- 顶部商家信息 -->
                    <div class="flex justify-between items-start mb-4">
                        <div class="flex items-center space-x-4">
                            <img :src="order.storeImage" :alt="order.storeName"
                                class="w-16 h-16 rounded-lg object-cover object-top" />
                            <div>
                                <h3 class="font-bold text-lg">{{ order.storeName }}</h3>
                                <p class="text-gray-600 text-sm">订单号：{{ order.orderID }}</p>
                                <p class="text-gray-600 text-sm">下单时间：{{ order.paymentTime }}</p>
                            </div>
                        </div>
                        <span :class="{
                            'text-orange-500': order.orderStatus === 0,
                            'text-green-500': order.orderStatus === 1,
                        }" class="font-medium">
                            {{ getOrderStatusText(order.orderStatus) }}
                        </span>
                    </div>

                    <!-- 菜品展示 -->
                    <div class="border-t pt-4">
                        <div class="flex justify-between items-center">
                            <div class="flex space-x-2">
                                <img v-for="(dish, idx) in order.dishImage" :key="idx" :src="dish" alt="菜品"
                                    class="w-12 h-12 rounded-lg object-cover" />
                            </div>

                            <!-- 右侧总金额和操作 -->
                            <div class="text-right">
                                <p class="font-bold text-lg">¥{{ order.totalAmount }}</p>
                                <div class="flex space-x-2 mt-2">
                                    <!-- 配送中 -->
                                    <div v-if="order.orderStatus === 0" class="flex items-center justify-center gap-2">
                                        <button
                                            class="bg-orange-500 hover:bg-orange-600 text-white w-8 h-8 rounded-full text-sm transition-colors cursor-pointer"
                                            title="联系商家">
                                            <i class="fas fa-store"></i>
                                        </button>
                                        <button
                                            class="bg-orange-500 hover:bg-orange-600 text-white w-8 h-8 rounded-full text-sm transition-colors cursor-pointer"
                                            title="联系骑手">
                                            <i class="fas fa-motorcycle"></i>
                                        </button>
                                        <button
                                            class="bg-blue-500 hover:bg-blue-600 text-white px-4 py-1 rounded text-sm transition-colors cursor-pointer whitespace-nowrap">
                                            查看物流
                                        </button>
                                    </div>

                                    <!-- 已完成 -->
                                    <div v-if="order.orderStatus === 1">
                                        <button
                                            class="bg-orange-500 hover:bg-orange-600 text-white px-4 py-1 rounded text-sm transition-colors cursor-pointer whitespace-nowrap">
                                            评价
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </main>
</template>

<script lang="ts" setup>
import { ref, onMounted, computed } from "vue";

import type { OrderInfo } from "@/api/user_home";
import { getOrderInfo } from "@/api/user_home";

const orders = ref<OrderInfo[]>([]);
const activeOrderStatus = ref("all"); // 默认显示全部订单
const showLoading = ref(true);
const orderStatuses = [
    { key: "all", label: "全部订单" },
    { key: "delivering", label: "配送中" },
    { key: "completed", label: "已完成" },
];

onMounted(() => {
    fetchOrders();
});

const getOrderStatusText = (statusNum: number) => {
    const map: Record<number, string> = {
        0: "配送中",
        1: "已完成",
    };
    return map[statusNum] || "未知状态";
};

const fetchOrders = async () => {
    try {
        // 待添加读取用户ID逻辑
        const userID = 0;
        // const userID = getUserId();
        const res: OrderInfo[] = await getOrderInfo(userID); // 返回 OrderInfo[]
        orders.value = res;

        showLoading.value = false;
    } catch (err) {
        alert('获取订单失败');
        console.error("获取订单失败:", err);
    }
};

const filteredOrders = computed(() => {
    if (activeOrderStatus.value === "all") {
        return orders.value;
    } else {
        const statusMap: Record<string, number> = {
            delivering: 0,
            completed: 1,
        };
        const statusNum = statusMap[activeOrderStatus.value];
        return orders.value.filter(order => order.orderStatus === statusNum);
    }
});

</script>