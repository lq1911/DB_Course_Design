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
            <div class="min-h-screen bg-gray-50">

                <!-- 顶部导航栏 -->
                <div v-if="userProfile"
                    class="fixed top-0 left-0 right-0 bg-gradient-to-r from-orange-500 to-orange-400 z-50 px-4 py-4">
                    <div class="flex items-center justify-between">
                        <div class="flex items-center space-x-3">
                            <div
                                class="w-10 h-10 bg-white/20 backdrop-blur-sm rounded-xl flex items-center justify-center">
                                <el-icon class="text-white text-xl">
                                    <User />
                                </el-icon>
                            </div>
                            <div>
                                <!-- 这部分数据您已经有了，可以直接用 -->
                                <div class="text-base font-medium text-white">
                                    {{ userProfile.name }}
                                </div>
                                <div class="text-xs text-white/80">ID: {{ userProfile.id }}</div>
                            </div>
                        </div>
                        <div class="flex items-center space-x-5">
                            <div class="text-right">
                                <div class="text-xs text-white/80">今日收入</div>
                                <!-- 修改点: 直接使用计算属性 todayIncome -->
                                <div class="text-base font-semibold text-white">
                                    ¥{{ todayIncome.toFixed(2) }}
                                </div>
                            </div>
                            <div
                                class="w-9 h-9 bg-white/20 backdrop-blur-sm rounded-xl flex items-center justify-center">
                                <el-icon class="text-white text-xl cursor-pointer">
                                    <Bell />
                                </el-icon>
                            </div>
                        </div>
                    </div>
                </div>

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
                            <div class="grid grid-cols-3 gap-4 text-center">

                                <!-- 2. 这是第一个子项: 待取单 -->
                                <div class="bg-orange-50 rounded-2xl p-3">
                                    <div class="text-xl font-semibold text-orange-500 mb-1">
                                        {{ pendingOrderCount }}
                                    </div>
                                    <div class="text-xs text-gray-500">待取单</div>
                                </div>

                                <!-- 3. 这是第二个子项: 配送中 -->
                                <div class="bg-blue-50 rounded-2xl p-3">
                                    <div class="text-xl font-semibold text-blue-500 mb-1">
                                        {{ deliveringOrderCount }}
                                    </div>
                                    <div class="text-xs text-gray-500">配送中</div>
                                </div>

                                <!-- 4. 这是第三个子项: 已送达 -->
                                <div class="bg-green-50 rounded-2xl p-3">
                                    <div class="text-xl font-semibold text-green-500 mb-1">
                                        {{ completedOrderCount }}
                                    </div>
                                    <div class="text-xs text-gray-500">已送达</div>
                                </div>

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
                                <div v-for="tab in orderTabs" :key="tab.key"
                                    class="flex-1 py-3 text-center cursor-pointer"
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
                                <!-- 找到并替换为这个新版本 -->
                                <div v-else v-for="order in filteredOrders" :key="order.id"
                                    class="border rounded-lg p-3 space-y-3">
                                    <div class="flex items-center justify-between">
                                        <div class="text-sm font-medium text-gray-900">#{{ order.id }}</div>
                                        <!-- 修改点: 使用新函数生成状态文本 -->
                                        <div class="text-xs px-2 py-1 rounded-full"
                                            :class="getOrderStatusClass(order.status)">
                                            {{ getOrderStatusText(order.status) }}
                                        </div>
                                    </div>
                                    <div class="text-sm text-gray-900">{{ order.restaurant }}</div>
                                    <div class="text-xs text-gray-500 mb-1">{{ order.address }}</div>
                                    <div class="flex items-center justify-between">
                                        <div class="text-sm font-medium text-orange-500">¥{{ order.fee }}</div>
                                        <!-- 新增点: 根据状态显示操作按钮 -->
                                        <div class="flex space-x-2">
                                            <button v-if="order.status === 'pending'"
                                                @click="handlePickupOrder(order.id)"
                                                class="bg-orange-500 text-white px-4 py-2 text-xs rounded-lg shadow-sm hover:bg-orange-600">
                                                取单
                                            </button>
                                            <button v-if="order.status === 'delivering'"
                                                @click="handleDeliverOrder(order.id)"
                                                class="bg-green-500 text-white px-4 py-2 text-xs rounded-lg shadow-sm hover:bg-green-600">
                                                已送达
                                            </button>
                                        </div>
                                    </div>

                                    <div v-if="order.status === 'pending' || order.status === 'delivering'"
                                        class="mt-3">
                                        <div class="relative">
                                            <img :src="'https://readdy.ai/api/search-image?query=Simple%20delivery%20route%20map%20showing%20pickup%20and%20delivery%20locations%20with%20orange%20markers%2C%20clean%20interface%20design%2C%20mobile%20app%20style%20map%20view%2C%20clear%20navigation%20paths%2C%20professional%20delivery%20service%20aesthetic&width=280&height=160&seq=' + order.id + '&orientation=landscape'"
                                                alt="导航地图" class="w-full h-32 object-cover rounded-lg mb-2" />
                                            <button @click="showNavigation(order)"
                                                class="absolute bottom-3 right-3 bg-orange-500 text-white px-4 py-2 rounded-lg text-sm font-medium shadow-lg !rounded-button flex items-center space-x-2">
                                                <el-icon>
                                                    <Location />
                                                </el-icon>
                                                <span>导航</span>
                                            </button>
                                        </div>
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
                                    <div class="text-sm font-medium text-gray-900">{{ newOrderInfo.restaurantName }}
                                    </div>
                                    <div class="text-xs text-gray-500">{{ newOrderInfo.restaurantAddress }}</div>
                                </div>
                                <div>
                                    <!-- 数据绑定: 顾客名和地址 -->
                                    <div class="text-sm font-medium text-gray-900">送至：{{ newOrderInfo.customerName }}
                                    </div>
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
                                    <div class="text-sm font-semibold text-orange-500">¥{{ newOrderInfo.fee.toFixed(2)
                                    }}
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
            <!-- 导航弹窗 -->
            <div v-if="showNavigationModal"
                class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
                <div class="bg-white rounded-lg w-full max-w-sm">
                    <div class="p-4 border-b flex items-center justify-between">
                        <div class="text-lg font-semibold text-gray-900">
                            导航路线
                        </div>
                        <el-icon class="text-gray-400 cursor-pointer" @click="closeNavigation">
                            <Close />
                        </el-icon>
                    </div>
                    <div class="p-4">
                        <img :src="'https://readdy.ai/api/search-image?query=Detailed%20navigation%20route%20map%20with%20turn%20by%20turn%20directions%2C%20showing%20current%20location%20and%20destination%20with%20clear%20path%20markers%2C%20real%20time%20traffic%20information%2C%20estimated%20arrival%20time%20display%2C%20professional%20navigation%20interface&width=280&height=400&seq=nav001&orientation=portrait'"
                            alt="导航路线" class="w-full h-64 object-cover rounded-lg mb-4" />
                        <div class="space-y-3 mb-4">
                            <div class="flex items-center space-x-2">
                                <div class="w-2 h-2 bg-orange-500 rounded-full"></div>
                                <!-- 数据绑定到 selectedOrder -->
                                <div class="text-sm text-gray-900">
                                    {{ selectedOrder?.restaurant }}
                                </div>
                            </div>
                            <div class="flex items-center space-x-2">
                                <div class="w-2 h-2 bg-green-500 rounded-full"></div>
                                <!-- 数据绑定到 selectedOrder -->
                                <div class="text-sm text-gray-900">
                                    {{ selectedOrder?.address }}
                                </div>
                            </div>
                            <div class="text-sm text-gray-500">
                                预计送达时间：15分钟
                            </div>
                        </div>
                        <button class="w-full bg-orange-500 text-white py-3 rounded-lg font-medium !rounded-button"
                            @click="startNavigation">
                            开始导航
                        </button>
                    </div>
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
    HomeFilled, DocumentCopy, Coin, UserFilled,Close
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
// 新增一个类型别名，让代码更清晰
type OrderStatus = 'pending' | 'delivering' | 'completed';
const userProfile = ref<UserProfile | null>(null);
const locationInfo = ref<any | null>(null);
const orders = ref<Order[]>([]);
const income = ref<number>(0);
const workStatus = ref<{ isOnline: boolean } | null>(null);

const isLoading = ref(true);
const errorState = ref<string | null>(null);

const currentTab = ref('home');
const activeOrderTab = ref<OrderStatus>('pending');

const showNewOrder = ref(false);
const newOrderInfo = ref<NewOrder | null>(null);
const countdown = ref(30);
let countdownTimer: number | null = null;
const pendingOrderCount = ref(0);
const deliveringOrderCount = ref(0);
const completedOrderCount = ref(0);

const showNavigationModal = ref(false);
const selectedOrder = ref<Order | null>(null); // Order 是您已有的订单接口类型

// --- 静态数据 ---
const tabs = [
    { key: 'home', label: '工作台', icon: HomeFilled },
    { key: 'orders', label: '订单', icon: DocumentCopy },
    { key: 'profile', label: '我的', icon: UserFilled }
];
// 修改 orderTabs 的 label
const orderTabs = [
    { key: 'pending', label: '待取单' },
    { key: 'delivering', label: '配送中' },
    { key: 'completed', label: '已送达' }
];

// --- API 调用逻辑 ---

const todayIncome = computed(() => {
    // 如果月收入还没加载出来，就显示0
    if (!income.value) {
        return 0;
    }
    // 简单模拟：假设今日收入是月收入的 1/25 (可以随便调整)
    const estimatedDaily = income.value / 25;
    // 为了让数字看起来更真实，我们再加一点随机性
    // 比如在估算值的 80% 到 120% 之间随机
    return estimatedDaily * (0.8 + Math.random() * 0.4);
});

/** 刷新当前标签页的订单列表 */
const refreshOrderList = async () => {
    const loadingInstance = ElLoading.service({ target: '.order-list-container', text: '刷新中...' });
    try {
        const res = await api.fetchOrders(activeOrderTab.value) as { data: Order[] };
        orders.value = res.data;
    } catch (error) {
        ElMessage.error("订单列表刷新失败");
    } finally {
        loadingInstance.close();
    }
};

/** 处理“取单”操作 */
const handlePickupOrder = async (orderId: string) => {
    try {
        await api.pickupOrderAPI(orderId);
        ElMessage.success('取单成功！订单已移至“配送中”');
        await refreshOrderList(); // 操作成功后刷新列表
    } catch (error) {
        ElMessage.error("取单操作失败，请重试");
    }
};

/** 处理“已送达”操作 */
const handleDeliverOrder = async (orderId: string) => {
    try {
        await api.deliverOrderAPI(orderId);
        ElMessage.success('操作成功！订单已完成');
        await refreshOrderList(); // 操作成功后刷新列表
    } catch (error) {
        ElMessage.error("确认送达操作失败，请重试");
    }
};



// 新增导航相关函数
const showNavigation = (order: Order) => {
    selectedOrder.value = order; // 将点击的订单信息存起来
    showNavigationModal.value = true; // 打开弹窗
};


const closeNavigation = () => {
    showNavigationModal.value = false;
    selectedOrder.value = null; // 最好在关闭时清空，是个好习惯
};


const startNavigation = () => {
    // 这里可以放跳转到地图APP的逻辑
    // 目前，我们先给一个提示并关闭弹窗
    ElMessage.success('正在为您规划路线...');
    closeNavigation();
};

const loadDashboardData = async () => {
    isLoading.value = true;
    errorState.value = null;
    const loadingInstance = ElLoading.service({ fullscreen: true, text: '加载中...' });
    try {
        // ▼▼▼ 修改 Promise.all，让它获取所有状态的订单 ▼▼▼
        const [
            profileRes,
            statusRes,
            incomeRes,
            pendingOrdersRes,     // 获取待取单列表
            deliveringOrdersRes,  // 获取配送中列表
            completedOrdersRes,   // 获取已送达列表
            locationRes
        ] = (await Promise.all([
            api.fetchUserProfile(),
            api.fetchWorkStatus(),
            api.fetchIncomeData(),
            api.fetchOrders('pending'),     // API 调用 1
            api.fetchOrders('delivering'),  // API 调用 2
            api.fetchOrders('completed'),   // API 调用 3
            api.fetchLocationInfo()
        ])) as [{ data: any }, { data: any }, { data: any }, { data: any[] }, { data: any[] }, { data: any[] }, { data: any }];
        // ▲▲▲ 修改结束 ▲▲▲

        // --- 赋值 ---
        userProfile.value = profileRes.data;
        workStatus.value = statusRes.data;
        // income.value = incomeRes.data; 
        income.value = incomeRes.data;
        locationInfo.value = locationRes.data;

        // ▼▼▼ 新增：为我们新创建的 count ref 赋值 ▼▼▼
        pendingOrderCount.value = pendingOrdersRes.data.length;
        deliveringOrderCount.value = deliveringOrdersRes.data.length;
        completedOrderCount.value = completedOrdersRes.data.length;
        // ▲▲▲ 新增结束 ▲▲▲

        // 保持原有逻辑：页面首次加载时，订单列表默认显示“待取单”的内容
        orders.value = pendingOrdersRes.data;

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


function setupWebSocketListener() {
    // 这里的URL需要后端提供
    const socket = new WebSocket('ws://localhost:5200/notifications');

    // 当连接成功建立时
    socket.onopen = () => {
        console.log('WebSocket连接已建立，等待新订单推送...');
    };

    // 当从服务器接收到消息时
    socket.onmessage = (event) => {
        try {
            // 服务器推送的消息通常是JSON字符串，需要解析
            const notification = JSON.parse(event.data);

            // 假设服务器推送的数据格式是 { type: 'NEW_ORDER', notificationId: 'xyz-123' }
            if (notification && notification.type === 'NEW_ORDER' && notification.notificationId) {
                console.log('收到新订单推送:', notification);
                // 直接调用您已经写好的函数来处理这个推送！
                handleNewOrderPush({ notificationId: notification.notificationId });
            }
        } catch (error) {
            console.error('处理WebSocket消息失败:', error);
        }
    };

    // 处理连接错误
    socket.onerror = (error) => {
        console.error('WebSocket 错误:', error);
    };
}



onMounted(() => {
    loadDashboardData();

    // 如果不是用模拟数据，就启动WebSocket监听器
    if (!useMockData) {
        setupWebSocketListener();
    }

    // 保留模拟数据时的测试逻辑
    if (useMockData) {
        setTimeout(() => {
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
// 找到这个 watch
watch(activeOrderTab, async (newStatus) => {
    // 将其内部逻辑替换为下面这一行
    await refreshOrderList();
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

// 新增这个函数
const getOrderStatusText = (status: string) => {
    switch (status) {
        case 'pending': return '待取单';
        case 'delivering': return '配送中';
        case 'completed': return '已送达';
        default: return '未知状态';
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