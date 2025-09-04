<template>
    <div class="min-h-screen bg-gray-50">
        <!-- 状态一: 加载中 -->
        <div v-if="isLoading" class="flex flex-col justify-center items-center h-screen">
            <p class="text-gray-500">正在拼命加载数据中...</p>
        </div>

        <!-- 状态二: 加载失败 -->
        <div v-else-if="errorState" class="flex flex-col justify-center items-center h-screen space-y-4">
            <el-icon class="text-red-500 text-5xl">
                <CircleCloseFilled />
            </el-icon>
            <p class="text-red-500">{{ errorState }}</p>
            <button @click="loadDashboardData"
                class="bg-orange-500 text-white px-6 py-2 rounded-full shadow-md hover:bg-orange-600 transition-all">
                点击重试
            </button>
        </div>

        <!-- 状态三: 加载成功 (渲染主要内容) -->
        <div v-else>

            <!-- 主要内容区域 -->
            <div class="pt-24 pb-20">
                <!-- 工作台页面 -->
                <div v-if="currentTab === 'home'">
                    <div v-if="workStatus" class="bg-white mx-4 mt-6 rounded-2xl shadow-lg p-5">
                        <div class="flex items-center justify-between mb-5">
                            <div class="flex items-center space-x-3">
                                <div class="text-xl font-semibold text-gray-800">工作状态</div>
                                <div v-if="workStatus.isOnline"
                                    class="px-3 py-1 bg-gradient-to-r from-green-500 to-green-400 text-white text-xs rounded-full shadow-sm">
                                    在线</div>
                                <div v-else
                                    class="px-3 py-1 bg-gradient-to-r from-gray-400 to-gray-500 text-white text-xs rounded-full shadow-sm">
                                    离线</div>
                            </div>
                        </div>
                        <div class="flex items-center justify-center mb-10">
                            <div class="relative">
                                <div class="w-24 h-24 rounded-3xl flex items-center justify-center cursor-pointer shadow-lg transition-all duration-300 transform hover:scale-105"
                                    :class="workStatus.isOnline ? 'bg-gradient-to-br from-orange-500 to-orange-400' : 'bg-gradient-to-br from-gray-400 to-gray-500'"
                                    @click="toggleWorkStatus">
                                    <el-icon class="text-white text-3xl">
                                        <Switch />
                                    </el-icon>
                                </div>
                                <div class="absolute -bottom-8 left-1/2 transform -translate-x-1/2 text-base font-medium"
                                    :class="workStatus.isOnline ? 'text-orange-500' : 'text-gray-500'">
                                    {{ workStatus.isOnline ? '开工中' : '已收工' }}
                                </div>
                            </div>
                        </div>
                        <div class="grid grid-cols-3 gap-6 text-center">

                        </div>
                    </div>

                    <div v-if="locationInfo" class="mx-4 mt-4 bg-white rounded-lg shadow-sm overflow-hidden">
                        <div class="h-64 relative">
                            <img src="https://readdy.ai/api/search-image?query=Urban%20delivery%20map%20interface%20showing%20rider%20location%20with%20orange%20markers%20and%20navigation%20routes%2C%20clean%20modern%20GPS%20interface%20design%2C%20realistic%20mobile%20map%20view%20with%20clear%20street%20layout%2C%20professional%20delivery%20app%20aesthetic%2C%20bright%20daylight%20view&width=343&height=256&seq=map001&orientation=landscape"
                                alt="配送地图" class="w-full h-full object-cover" />
                            <div class="absolute top-4 left-4 bg-white rounded-lg px-3 py-2 shadow-sm">
                                <div class="text-xs text-gray-500">当前位置</div>
                                <div class="text-sm font-medium text-gray-900">{{ locationInfo.area }}</div>
                            </div>
                            <div
                                class="absolute bottom-4 right-4 bg-orange-500 w-10 h-10 rounded-full flex items-center justify-center cursor-pointer !rounded-button">
                                <el-icon class="text-white">
                                    <Location />
                                </el-icon>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- 订单列表页面 -->
                <div v-if="currentTab === 'orders'" class="mx-4 mt-4 order-list-container">
                    <div class="bg-white rounded-lg shadow-sm">
                        <div class="flex border-b">
                            <div v-for="tab in orderTabs" :key="tab.key" class="flex-1 py-3 text-center cursor-pointer"
                                :class="{ 'text-orange-500 border-b-2 border-orange-500': activeOrderTab === tab.key }"
                                @click="activeOrderTab = tab.key">{{ tab.label }}</div>
                        </div>
                        <div class="p-4 space-y-3">
                            <div v-if="filteredOrders.length === 0" class="text-center text-gray-400 py-12">
                                <el-icon class="text-4xl mb-2">
                                    <DocumentCopy />
                                </el-icon>
                                <p>当前分类下没有订单哦</p>
                            </div>
                            <div v-else v-for="order in filteredOrders" :key="order.id" class="border rounded-lg p-3">
                                <div class="flex items-center justify-between mb-2">
                                    <div class="text-sm font-medium text-gray-900">#{{ order.id }}</div>
                                    <div class="text-xs px-2 py-1 rounded-full"
                                        :class="getOrderStatusClass(order.status)">{{ order.statusText }}</div>
                                </div>
                                <div class="text-sm text-gray-900 mb-1">{{ order.restaurant }}</div>
                                <div class="text-xs text-gray-500 mb-2">{{ order.address }}</div>
                                <div class="flex items-center justify-between">
                                    <div class="text-sm font-medium text-orange-500">¥{{ order.fee }}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <!-- 个人中心页面 -->
                <div v-if="currentTab === 'profile' && userProfile" class="mx-4 mt-4">
                    <div class="bg-white rounded-lg shadow-sm p-4 mb-4">
                        <div class="flex items-center space-x-4 mb-4">
                            <div class="w-16 h-16 bg-orange-500 rounded-full flex items-center justify-center">
                                <el-icon class="text-white text-2xl">
                                    <User />
                                </el-icon>
                            </div>
                            <div>
                                <div class="text-lg font-semibold text-gray-900">{{ userProfile.name }}</div>
                                <div class="text-sm text-gray-500">ID: {{ userProfile.id }}</div>
                                <div class="text-xs text-gray-400">注册时间: {{ userProfile.registerDate }}</div>
                            </div>
                        </div>
                        <div class="grid grid-cols-2 gap-4">
                            <div class="bg-gray-50 rounded-lg p-3 text-center">
                                <div class="text-lg font-semibold text-gray-900">{{ userProfile.rating }}</div>
                                <div class="text-xs text-gray-500">获评均分</div>
                            </div>
                            <div class="bg-gray-50 rounded-lg p-3 text-center">
                                <div class="text-lg font-semibold text-gray-900">{{ userProfile.creditScore }}</div>
                                <div class="text-xs text-gray-500">信誉积分</div>
                            </div>
                             <div class="bg-gray-50 rounded-lg p-3 text-center">
                                <div class="text-lg font-semibold text-gray-900">{{ income }}</div>
                                <div class="text-xs text-gray-500">薪资</div>
                            </div>                           
                        </div>
                    </div>


                    <!-- 设置菜单 -->
                    <div class="bg-white rounded-lg shadow-sm">
                        <div class="p-4 space-y-1">
                            <router-link :to="{ name: 'AccountSettings' }"
                                class="flex items-center justify-between cursor-pointer py-3 no-underline text-gray-900">
                                <div class="flex items-center space-x-3">
                                    <el-icon class="text-gray-400">
                                        <Edit />
                                    </el-icon>
                                    <span>账号与资料设置</span>
                                </div>
                                <el-icon class="text-gray-400">
                                    <ArrowRight />
                                </el-icon>
                            </router-link>
                        </div>
                    </div>
                </div>
            </div>

            <!-- 新订单弹窗 -->
            <div v-if="showNewOrder"
                class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
                <div class="bg-white rounded-lg w-full max-w-sm">
                    <div class="p-4 border-b">
                        <div class="flex items-center justify-between">
                            <div class="text-lg font-semibold text-gray-900">新订单</div>
                            <div class="text-sm text-orange-500">{{ countdown }}s</div>
                        </div>
                    </div>

                    <!-- 核心修改: v-if="newOrderInfo" 确保在详情加载后才显示内容 -->
                    <div v-if="newOrderInfo" class="p-4">
                        <div class="mb-4">
                            <!-- 数据绑定: 地图图片 -->
                            <img :src="newOrderInfo.mapImageUrl" alt="订单地图"
                                class="w-full h-32 object-cover rounded-lg" />
                        </div>
                        <div class="space-y-3 mb-4">
                            <div>
                                <!-- 数据绑定: 餐厅名和地址 -->
                                <div class="text-sm font-medium text-gray-900">{{ newOrderInfo.restaurantName }}</div>
                                <div class="text-xs text-gray-500">{{ newOrderInfo.restaurantAddress }}</div>
                            </div>
                            <div>
                                <!-- 数据绑定: 顾客名和地址 -->
                                <div class="text-sm font-medium text-gray-900">送至：{{ newOrderInfo.customerName }}</div>
                                <div class="text-xs text-gray-500">{{ newOrderInfo.customerAddress }}</div>
                            </div>
                        </div>
                        <div class="grid grid-cols-2 gap-4 mb-4 text-center">
                            <div>
                                <!-- 数据绑定: 配送距离 -->
                                <div class="text-sm font-semibold text-gray-900">{{ newOrderInfo.distance }}</div>
                                <div class="text-xs text-gray-500">配送距离</div>
                            </div>
                            <div>
                                <!-- 数据绑定: 配送费 -->
                                <div class="text-sm font-semibold text-orange-500">¥{{ newOrderInfo.fee.toFixed(2) }}
                                </div>
                                <div class="text-xs text-gray-500">配送费</div>
                            </div>
                        </div>
                        <div class="flex space-x-3">
                            <button
                                class="flex-1 bg-gray-200 text-gray-700 py-3 rounded-lg font-medium cursor-pointer !rounded-button"
                                @click="rejectOrder">
                                拒绝
                            </button>
                            <button
                                class="flex-1 bg-orange-500 text-white py-3 rounded-lg font-medium cursor-pointer !rounded-button"
                                @click="acceptOrder">
                                接受
                            </button>
                        </div>
                    </div>

                    <!-- 如果订单详情还在加载中，可以显示一个加载提示 -->
                    <div v-else class="p-4 text-center text-gray-500">
                        正在获取订单详情...
                    </div>
                </div>
            </div>

            <!-- 底部导航栏 -->
            <!-- 底部导航栏 -->
            <div class="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 flex justify-around py-2">
                <div v-for="tab in tabs" :key="tab.key"
                    class="flex flex-col items-center justify-center py-2 cursor-pointer"
                    :class="{ 'text-orange-500': currentTab === tab.key }" @click="currentTab = tab.key">
                    <el-icon class="text-xl mb-1">
                        <component :is="tab.icon" />
                    </el-icon>
                    <span class="text-xs">{{ tab.label }}</span>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted, watch } from 'vue';
import { ElMessage, ElLoading } from 'element-plus';
import {
    User, Bell, Switch, Location, CircleCloseFilled,
    HomeFilled, DocumentCopy, Coin, UserFilled
} from '@element-plus/icons-vue';

// ===================================================================
//  数据源切换开关
// ===================================================================
const useMockData = false;

// 动态导入API模块
// 注意: 我们需要确保真实api.ts也导出了同名函数, 即使它们暂时为空
import * as RealAPI from '@/api/rider_api';
import * as MockAPI from '@/api/api.mock';

const api = useMockData ? MockAPI : RealAPI;

// --- 接口定义 ---
interface UserProfile { name: string; id: string; registerDate: string; rating: number; creditScore: number; }
interface Order { id: string; status: string; restaurant: string; address: string; fee: string; time: string; statusText: string; }
interface NewOrder {
    id: string;
    restaurantName: string;
    restaurantAddress: string;
    customerName: string;
    customerAddress: string;
    distance: string;
    fee: number;
    mapImageUrl: string;
}

// --- 状态定义 ---
const userProfile = ref<UserProfile | null>(null);
const locationInfo = ref<any | null>(null);
const orders = ref<Order[]>([]);
const income=ref<number>(0);
const workStatus = ref<{ isOnline: boolean } | null>(null);

const isLoading = ref(true);
const errorState = ref<string | null>(null);

const currentTab = ref('home');
const activeOrderTab = ref('pending');

const showNewOrder = ref(false);
const newOrderInfo = ref<NewOrder | null>(null);
const countdown = ref(30);
let countdownTimer: number | null = null;

// --- 静态数据 ---
const tabs = [
    { key: 'home', label: '工作台', icon: HomeFilled },
    { key: 'orders', label: '订单', icon: DocumentCopy },
    { key: 'profile', label: '我的', icon: UserFilled }
];
const orderTabs = [
    { key: 'pending', label: '待处理' },
    { key: 'delivering', label: '配送中' },
    { key: 'completed', label: '已完成' }
];

// --- API 调用逻辑 ---
const loadDashboardData = async () => {
    isLoading.value = true;
    errorState.value = null;
    const loadingInstance = ElLoading.service({ fullscreen: true, text: '加载中...' });
    try {
        const [
            profileRes,
            statusRes,
            incomeRes,
            ordersRes,
            locationRes
        ] = (await Promise.all([
            api.fetchUserProfile(),
            api.fetchWorkStatus(),
            api.fetchIncomeData(),
            api.fetchOrders('pending'),
            api.fetchLocationInfo()
        ])) as [{ data: any }, { data: any }, { data: any }, { data: any }, { data: any }];

        // 赋值
        userProfile.value = profileRes.data;
        workStatus.value = statusRes.data;
        income.value = incomeRes.data;
        orders.value = ordersRes.data;
        locationInfo.value = locationRes.data; // <-- 赋值给 locationInfo

    } catch (error) {
        console.error("加载数据失败:", error);
        errorState.value = "数据加载失败，请检查网络或联系管理员。";
        ElMessage.error(errorState.value);
    } finally {
        isLoading.value = false;
        loadingInstance.close();
    }
};



const toggleWorkStatus = async () => {
    if (!workStatus.value) return;

    const newStatus = !workStatus.value.isOnline;

    try {
        await api.toggleWorkStatusAPI(newStatus);
        workStatus.value = {
            ...workStatus.value, // 先用展开运算符(...)复制旧对象的所有属性
            isOnline: newStatus, // 然后用新值覆盖 isOnline 属性
        };

        ElMessage.success(`状态已切换为: ${workStatus.value.isOnline ? '开工' : '已收工'}`);

    } catch (error) {
        console.error("状态切换失败:", error);
        ElMessage.error("状态切换失败，请重试");
    }
};
const acceptOrder = async () => {
    if (!newOrderInfo.value) return;
    try {
        await api.acceptOrderAPI(newOrderInfo.value.id);
        closeOrderModal(); // 使用新的关闭函数
        ElMessage.success('订单已接受！');
        const res = await api.fetchOrders(activeOrderTab.value) as { data: any[] };
        orders.value = res.data;
    } catch (error) {
        ElMessage.error('接受订单失败');
    }
};

const rejectOrder = async () => {
    if (!newOrderInfo.value) return;
    try {
        await api.rejectOrderAPI(newOrderInfo.value.id);
        closeOrderModal(); // 使用新的关闭函数
        ElMessage.info('已拒绝该订单');
    } catch (error) {
        ElMessage.error('操作失败');
    }
};

onMounted(() => {
    loadDashboardData();
    // 模拟收到新订单推送通知
    if (useMockData) {
        setTimeout(() => {
            // 调用新的处理函数，并传递一个模拟的通知ID
            handleNewOrderPush({ notificationId: 'mock-notification-123' });
        }, 5000);
    }
});

// 函数1: 处理新订单推送的函数
const handleNewOrderPush = async (notification: { notificationId: string }) => {
    showNewOrder.value = true;
    newOrderInfo.value = null; // 先清空旧数据，弹窗会显示“加载中”

    try {
        // 调用API获取订单详情
        const res = await api.fetchNewOrder(notification.notificationId) as { data: NewOrder };
        newOrderInfo.value = res.data;

        // 成功获取数据后，开始倒计时
        startCountdown();

    } catch (error) {
        ElMessage.error('获取新订单详情失败');
        showNewOrder.value = false; // 获取失败则直接关闭弹窗
    }
};

// 函数2: 倒计时逻辑
const startCountdown = () => {
    countdown.value = 30; // 每次都重置为30秒
    if (countdownTimer) clearInterval(countdownTimer); // 清除可能存在的旧计时器

    countdownTimer = setInterval(() => {
        countdown.value--;
        if (countdown.value <= 0) {
            clearInterval(countdownTimer!);
            showNewOrder.value = false; // 时间到了自动关闭弹窗
        }
    }, 1000);
};

// 函数3: 统一的关闭弹窗逻辑
const closeOrderModal = () => {
    showNewOrder.value = false;
    if (countdownTimer) clearInterval(countdownTimer); // 关闭弹窗时，必须停止倒计时
};

// --- 监听器 ---
watch(activeOrderTab, async (newStatus) => {
    const loadingInstance = ElLoading.service({ target: '.order-list-container', text: '加载中...' });
    try {
        const res = await api.fetchOrders(newStatus) as { data: any[] };
        orders.value = res.data;
    } catch (error) {
        ElMessage.error("订单列表加载失败");
    } finally {
        loadingInstance.close();
    }
});

// --- 计算属性和工具函数 ---
const filteredOrders = computed(() => {
    if (!orders.value) return [];
    return orders.value.filter(order => order.status === activeOrderTab.value);
});

const getOrderStatusClass = (status: string) => {
    switch (status) {
        case 'pending': return 'bg-orange-100 text-orange-600';
        case 'delivering': return 'bg-blue-100 text-blue-600';
        case 'completed': return 'bg-green-100 text-green-600';
        default: return 'bg-gray-100 text-gray-600';
    }
};
</script>

<style scoped>
.rounded-button {
    border-radius: 8px;
}

input[type="number"]::-webkit-outer-spin-button,
input[type="number"]::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
}

input[type="number"] {
    -moz-appearance: textfield;
}

::-webkit-scrollbar {
    width: 4px;
}

::-webkit-scrollbar-track {
    background: #f1f1f1;
}

::-webkit-scrollbar-thumb {
    background: #F9771C;
    border-radius: 2px;
}

::-webkit-scrollbar-thumb:hover {
    background: #e6691a;
}

.order-list-container {
    position: relative;
}

/* 用于ElLoading定位 */
</style>