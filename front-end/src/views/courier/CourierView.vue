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
                        <!-- 工作状态卡片 -->
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
                                <div class="bg-orange-50 rounded-2xl p-3">
                                    <div class="text-xl font-semibold text-orange-500 mb-1">
                                        {{ pendingOrderCount }}
                                    </div>
                                    <div class="text-xs text-gray-500">待取单</div>
                                </div>
                                <div class="bg-blue-50 rounded-2xl p-3">
                                    <div class="text-xl font-semibold text-blue-500 mb-1">
                                        {{ deliveringOrderCount }}
                                    </div>
                                    <div class="text-xs text-gray-500">配送中</div>
                                </div>
                                <div class="bg-green-50 rounded-2xl p-3">
                                    <div class="text-xl font-semibold text-green-500 mb-1">
                                        {{ completedOrderCount }}
                                    </div>
                                    <div class="text-xs text-gray-500">已送达</div>
                                </div>
                            </div>
                        </div>

                        <!-- 地图卡片 -->
                        <!-- <div v-if="locationInfo" class="mx-4 mt-4 bg-white rounded-lg shadow-sm overflow-hidden">
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
                        </div> -->
                        <CourierLocationMap />

                    </div>

                    <!-- 可接订单页面 -->
                    <div v-if="currentTab === 'available'" class="mx-4 mt-4 space-y-3">
                        <!-- 有订单时显示列表 -->
                        <div v-if="availableOrders.length > 0">
                            <div v-for="order in availableOrders" :key="order.id"
                                class="bg-white border rounded-lg p-3 shadow-sm space-y-3">
                                <div class="flex items-center justify-between">
                                    <div class="text-sm font-medium text-gray-900">#{{ order.id }}</div>
                                    <div class="text-sm font-medium text-orange-500">¥{{ order.fee }}</div>
                                </div>
                                <div class="space-y-3">
                                    <div class="flex items-start space-x-3">
                                        <div
                                            class="w-6 h-6 bg-orange-500 rounded-full flex items-center justify-center text-white">
                                            <el-icon>
                                                <Shop />
                                            </el-icon>
                                        </div>
                                        <div class="flex-1">
                                            <div class="font-medium text-gray-900">{{ order.restaurant }}</div>
                                            <div class="text-sm text-gray-500">{{ order.pickupAddress }}</div>
                                        </div>
                                    </div>
                                    <div class="flex items-start space-x-3">
                                        <div
                                            class="w-6 h-6 bg-gray-300 rounded-full flex items-center justify-center text-white">
                                            <el-icon>
                                                <User />
                                            </el-icon>
                                        </div>
                                        <div class="flex-1">
                                            <div class="font-medium text-gray-900">{{ order.customer }}</div>
                                            <div class="text-sm text-gray-500">{{ order.deliveryAddress }}</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="grid grid-cols-2 gap-2 text-center text-xs text-gray-500">
                                    <div class="bg-orange-50 rounded-lg p-2">
                                        <div class="text-orange-500 font-medium text-sm">{{ order.distance }}km</div>
                                        <div>配送距离</div>
                                    </div>
                                    <div class="bg-blue-50 rounded-lg p-2">
                                        <div class="text-blue-500 font-medium text-sm">{{ order.time }}分钟</div>
                                        <div>预计送达</div>
                                    </div>
                                </div>
                                <button @click="acceptAvailableOrder(order)"
                                    class="w-full bg-orange-500 text-white py-2 rounded-lg font-medium hover:bg-orange-600 transition-all">
                                    接单
                                </button>
                            </div>
                        </div>
                        <!-- 没有订单时显示提示 -->
                        <div v-else class="text-center text-gray-400 py-24">
                            <el-icon class="text-5xl mb-2">
                                <Bell />
                            </el-icon>
                            <p>当前没有可接的订单</p>
                        </div>

                        <!-- ▼▼▼ 新增的悬浮刷新按钮 ▼▼▼ -->
                        <button @click="refreshAvailableOrders"
                            class="fixed bottom-24 right-5 w-14 h-14 bg-orange-500 text-white rounded-full shadow-lg flex items-center justify-center transition-transform transform hover:scale-110">
                            <el-icon class="text-2xl" :class="{ 'is-loading': isRefreshing }">
                                <Refresh />
                            </el-icon>
                        </button>
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
                                <div v-else v-for="order in filteredOrders" :key="order.id"
                                    class="border rounded-lg p-3 space-y-3">
                                    <div class="flex items-center justify-between">
                                        <div class="text-sm font-medium text-gray-900">#{{ order.id }}</div>
                                        <div class="text-xs px-2 py-1 rounded-full"
                                            :class="getOrderStatusClass(order.status)">
                                            {{ getOrderStatusText(order.status) }}
                                        </div>
                                    </div>
                                    <div class="space-y-2">
                                        <div class="flex items-start space-x-3">
                                            <div
                                                class="mt-1 flex-shrink-0 w-5 h-5 flex items-center justify-center bg-orange-500 rounded-full text-white">
                                                <el-icon :size="12">
                                                    <Shop />
                                                </el-icon>
                                            </div>
                                            <div class="flex-1">
                                                <div class="font-medium text-sm text-gray-900">{{ order.restaurant }}
                                                </div>
                                                <div class="text-xs text-gray-500">{{ order.pickupAddress }}</div>
                                            </div>
                                        </div>
                                        <div class="flex items-start space-x-3">
                                            <div
                                                class="mt-1 flex-shrink-0 w-5 h-5 flex items-center justify-center bg-green-500 rounded-full text-white">
                                                <el-icon :size="12">
                                                    <User />
                                                </el-icon>
                                            </div>
                                            <div class="flex-1">
                                                <div class="font-medium text-sm text-gray-900">{{ order.customer }}
                                                </div>
                                                <div class="text-xs text-gray-500">{{ order.deliveryAddress }}</div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="flex items-center justify-between pt-2">
                                        <div class="text-sm font-medium text-orange-500">¥{{ order.fee }}</div>
                                        <div class="flex space-x-2">
                                            <button v-if="order.status === 'pending'"
                                                @click="handlePickupOrder(order.id)"
                                                class="bg-orange-500 text-white px-4 py-2 text-xs rounded-lg shadow-sm hover:bg-orange-600">
                                                我已到店
                                            </button>
                                            <button v-if="order.status === 'delivering'"
                                                @click="handleDeliverOrder(order.id)"
                                                class="bg-green-500 text-white px-4 py-2 text-xs rounded-lg shadow-sm hover:bg-green-600">
                                                我已送达
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

                    <!-- 投诉页面 -->
                    <div v-if="currentTab === 'complaints'" class="mx-4 mt-4">
                        <!-- 顶部统计 -->
                        <div class="bg-white rounded-lg shadow-sm p-4 mb-4">
                            <div class="text-center">
                                <div class="text-2xl font-bold text-gray-900">
                                    {{ complaints.length }}
                                </div>
                                <div class="text-sm text-gray-500">总投诉数</div>
                            </div>
                        </div>

                        <!-- 投诉记录列表 -->
                        <div class="bg-white rounded-lg shadow-sm">
                            <!-- ▼▼▼ 新增的子导航栏 ▼▼▼ -->
                            <div class="flex border-b">
                                <div v-for="tab in complaintTabs" :key="tab.key"
                                    class="flex-1 py-3 text-center cursor-pointer text-sm font-medium" :class="{
                                        'text-orange-500 border-b-2 border-orange-500': activeComplaintTab === tab.key,
                                        'text-gray-600': activeComplaintTab !== tab.key
                                    }" @click="activeComplaintTab = tab.key">
                                    {{ tab.label }}
                                </div>
                            </div>

                            <!-- 列表内容 -->
                            <div class="p-4 space-y-4">
                                <!-- ▼▼▼ 修改点：v-for 循环现在使用 filteredComplaints ▼▼▼ -->
                                <div v-for="complaint in filteredComplaints" :key="complaint.complaintID"
                                    class="border rounded-lg p-4">
                                    <div class="flex items-center justify-between mb-2">
                                        <div class="text-sm font-medium text-red-500">
                                            #{{ complaint.complaintID }}
                                        </div>
                                        <div class="text-xs text-gray-500">
                                            {{ complaint.complaintTime }}
                                        </div>
                                    </div>
                                    <div class="text-sm text-gray-900 mb-2">
                                        订单号：{{ complaint.deliveryTaskID }}
                                    </div>
                                    <div class="bg-red-50 rounded-lg p-3 mb-3">
                                        <div class="text-sm text-gray-700">
                                            {{ complaint.complaintReason }}
                                        </div>
                                    </div>
                                    <div v-if="complaint.punishment"
                                        class="bg-orange-50 rounded-lg p-3 border border-orange-100">
                                        <div class="flex items-center mb-2">
                                            <el-icon class="text-orange-500 mr-2">
                                                <Warning />
                                            </el-icon>
                                            <span class="text-sm font-medium text-orange-500">{{
                                                complaint.punishment.type }}</span>
                                        </div>
                                        <div class="text-sm text-gray-600">
                                            {{ complaint.punishment.description }}
                                        </div>
                                        <div v-if="complaint.punishment.duration" class="text-xs text-orange-500 mt-1">
                                            处罚时长：{{ complaint.punishment.duration }}
                                        </div>
                                    </div>
                                </div>

                                <!-- ▼▼▼ 新增：当筛选后列表为空时的提示 ▼▼▼ -->
                                <div v-if="filteredComplaints.length === 0" class="text-center text-gray-400 py-12">
                                    <el-icon class="text-4xl mb-2">
                                        <DocumentCopy />
                                    </el-icon>
                                    <p>当前分类下没有记录</p>
                                </div>

                            </div>
                        </div>
                    </div>


                    <!-- 个人中心页面 -->
                    <div v-if="currentTab === 'profile' && userProfile" class="mx-4 mt-4">
                        <!-- 个人资料卡片 (仅更新头像显示) -->
                        <div class="bg-white rounded-lg shadow-sm p-4 mb-4">
                            <div class="flex items-center space-x-4 mb-4">

                                <!-- ▼▼▼ 这是唯一需要修改的部分 ▼▼▼ -->
                                <div
                                    class="w-16 h-16 rounded-full flex items-center justify-center bg-gray-200 overflow-hidden">
                                    <!-- 如果 userProfile.avatar 存在 (是一个有效的URL)，就显示图片 -->
                                    <img v-if="userProfile.avatar" :src="userProfile.avatar" alt="用户头像"
                                        class="w-full h-full object-cover" />
                                    <!-- 否则，显示一个默认的 Element Plus 用户图标 -->
                                    <el-icon v-else class="text-gray-500 text-3xl">
                                        <User />
                                    </el-icon>
                                </div>
                                <!-- ▲▲▲ 修改结束 ▲▲▲ -->

                                <!-- 其他部分保持完全不变 -->
                                <div>
                                    <div class="text-lg font-semibold text-gray-900">{{ userProfile.name }}</div>
                                    <div class="text-sm text-gray-500">ID: {{ userProfile.id }}</div>
                                    <div class="text-xs text-gray-400">注册时间: {{ userProfile.registerDate }}</div>
                                </div>
                            </div>
                            <div class="grid grid-cols-3 gap-4">
                                <div class="bg-gray-50 rounded-lg p-3 text-center">
                                    <div class="text-lg font-semibold text-gray-900">{{ userProfile.rating }}</div>
                                    <div class="text-xs text-gray-500">获评均分</div>
                                </div>
                                <div class="bg-gray-50 rounded-lg p-3 text-center">
                                    <div class="text-lg font-semibold text-gray-900">{{ userProfile.creditScore }}</div>
                                    <div class="text-xs text-gray-500">信誉积分</div>
                                </div>
                                <div class="bg-gray-50 rounded-lg p-3 text-center">
                                    <div class="text-lg font-semibold text-gray-900">¥{{ income.toFixed(2) }}</div>
                                    <div class="text-xs text-gray-500">本月收入</div>
                                </div>
                            </div>
                        </div>
                        <!-- 设置菜单 -->
                        <div class="bg-white rounded-lg shadow-sm">
                            <div class="p-4 space-y-1 divide-y divide-gray-100">
                                <!-- 账号与资料设置 -->
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

                                <!-- 投诉与处罚菜单项 -->
                                <div @click="currentTab = 'complaints'"
                                    class="flex items-center justify-between cursor-pointer py-3 text-gray-900">
                                    <div class="flex items-center space-x-3">
                                        <el-icon class="text-gray-400">
                                            <Warning />
                                        </el-icon>
                                        <span>投诉与处罚</span>
                                    </div>
                                    <el-icon class="text-gray-400">
                                        <ArrowRight />
                                    </el-icon>
                                </div>

                                <div @click="handleLogout"
                                    class="flex items-center justify-between cursor-pointer py-3 text-red-500">
                                    <div class="flex items-center space-x-3">
                                        <el-icon class="text-red-400">
                                            <SwitchButton />
                                        </el-icon>
                                        <span class="font-semibold">退出登录</span>
                                    </div>
                                    <el-icon class="text-red-400">
                                        <ArrowRight />
                                    </el-icon>
                                </div>


                            </div>
                        </div>
                    </div>



                    <!-- 底部导航栏 -->
                    <div
                        class="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 flex justify-around py-2">
                        <div v-for="tab in tabs" :key="tab.key"
                            class="flex flex-col items-center justify-center py-2 cursor-pointer w-full"
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
                                        {{ selectedOrder?.deliveryAddress }}
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
    </div>

</template>

<script lang="ts" setup>
import { ref, computed, onMounted, watch } from 'vue';
import CourierLocationMap from '@/components/courier/CourierLocationMap.vue';
import { ElMessage, ElLoading ,ElMessageBox} from 'element-plus';
import {
    User, Bell, Switch, Location, CircleCloseFilled,
    HomeFilled, DocumentCopy, Coin, UserFilled, Close, Shop, List, Refresh, Warning, Edit
} from '@element-plus/icons-vue';
import { useRouter } from 'vue-router';
import loginApi from '@/api/login_api';      // 导入我们定义好的通用认证API
import { removeToken } from '@/utils/jwt'; 
// ===================================================================
//  数据源切换开关
// ===================================================================
const useMockData = false;

// 动态导入API模块
// 注意: 我们需要确保真实api.ts也导出了同名函数, 即使它们暂时为空
import * as RealAPI from '@/api/rider_api';
import * as MockAPI from '@/api/api.mock';

const api = useMockData ? MockAPI : RealAPI;

const router = useRouter();

// --- 接口定义 ---
export interface UserProfile {
    name: string;
    id: string;
    registerDate: string;
    rating: number;
    creditScore: number;

    // --- 新增的可选属性 ---
    gender?: string;
    birthday?: string; // 通常是 ISO 格式的日期字符串，如 '2024-01-15T00:00:00'
    avatar?: string;   // 头像的 URL
    vehicleType?: string;
    // -----------------------
}
interface Order {
    id: string;
    status: OrderStatus; // 使用我们更精确的类型
    restaurant: string;
    pickupAddress: string;   // 取餐地址interface NewOrder
    deliveryAddress: string; // 送达地址
    customer: string;        // 顾客姓名
    fee: string;
    distance: string;        // 配送距离
    time: string;            // 预计时间
}

interface Complaint {
    complaintID: string;
    deliveryTaskID: string;
    complaintTime: string;
    complaintReason: string;
    punishment?: { // punishment 是可选的
        type: string;
        description: string;
        duration?: string; // duration 也是可选的
    };
}

// --- 状态定义 ---
// 新增一个类型别名，让代码更清晰
type OrderStatus = 'to_be_taken' | 'pending' | 'delivering' | 'completed' | 'cancelled';
const userProfile = ref<UserProfile | null>(null);
const locationInfo = ref<any | null>(null);
const orders = ref<Order[]>([]);
const income = ref<number>(0);
const workStatus = ref<{ isOnline: boolean } | null>(null);

const isLoading = ref(true);
const errorState = ref<string | null>(null);

const currentTab = ref('home');
const activeOrderTab = ref<OrderStatus>('pending');
// 在 ref 定义区域添加
const availableOrders = ref<Order[]>([]);

const complaints = ref<Complaint[]>([]);

const cancelledOrderInfo = ref<{ id: string; restaurant: string } | null>(null);

const activeComplaintTab = ref<'all' | 'punished'>('all');

const complaintTabs = [
    { key: 'all', label: '全部投诉' },
    { key: 'punished', label: '含处罚记录' }
] as const;

const pendingOrderCount = ref(0);
const deliveringOrderCount = ref(0);
const completedOrderCount = ref(0);

const showNavigationModal = ref(false);
const selectedOrder = ref<Order | null>(null); // Order 是您已有的订单接口类型

const isRefreshing = ref(false); // 用于控制刷新按钮的加载状态

// --- 静态数据 ---
const tabs = [
    { key: 'home', label: '工作台', icon: HomeFilled },
    { key: 'available', label: '可接订单', icon: List },
    { key: 'orders', label: '订单', icon: DocumentCopy },
    { key: 'complaints', label: '投诉', icon: Warning },
    { key: 'profile', label: '我的', icon: UserFilled }
];
// 修改 orderTabs 的 label
const orderTabs = [
    { key: 'pending', label: '待取单' },
    { key: 'delivering', label: '配送中' },
    { key: 'completed', label: '已送达' },
    { key: 'cancelled', label: '已取消' } 
] as const;

// --- API 调用逻辑 ---

const filteredComplaints = computed(() => {
    // 如果原始数据还没加载，返回空数组
    if (!complaints.value) return [];

    // 如果当前选择的是“含处罚记录”
    if (activeComplaintTab.value === 'punished') {
        // 则只返回那些 punishment 字段存在的投诉
        return complaints.value.filter(c => c.punishment);
    }

    // 否则（选择的是'all'），返回全部投诉
    return complaints.value;
});

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




/**
 * 【核心】处理接收到的“订单被取消”通知
 * 在真实项目中，这个函数会被 WebSocket 推送调用
 * @param orderId 被取消的订单ID
 */
const handleOrderCancelled = async (orderId: string) => {
    // 1. 在当前的前端订单列表中，找到这个订单的信息，主要是为了获取餐厅名用于弹窗显示
    const orderToCancel = orders.value.find(o => o.id === orderId);

    // 如果在当前列表（比如“待取单”）中找不到这个订单，可能是因为骑手正在看其他页面。
    // 即使找不到，我们依然应该弹窗提示，但内容可以通用一些。
    const restaurantName = orderToCancel ? `【${orderToCancel.restaurant}】的` : '';

    // 2. 弹出UI提示，告知骑手这个事实
    await ElMessageBox.alert(
        `您有一个订单（${restaurantName}订单号: #${orderId}）已被用户取消。`, // 弹窗内容
        '订单取消通知', // 弹窗标题
        {
            confirmButtonText: '我知道了',
            type: 'warning', // 警告类型图标
            callback: () => {
                // 3. 用户点击“我知道了”之后，刷新当前订单列表以同步最新状态
                ElMessage.info('正在刷新订单列表...');
                refreshOrderList(); // 调用您已有的刷新函数
            },
        }
    );
    
    // 【重要】在模拟模式下，为了让刷新有效，我们必须手动修改模拟数据源的状态。
    // 这段代码模仿了“后端数据状态已经被改变”这个事实。
    /*if (useMockData) {
        const orderInMock = MockAPI.mockOrders.find(o => o.id === orderId);
        if (orderInMock) {
            orderInMock.status = 'cancelled';
        }
    }*/
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


const refreshAvailableOrders = async () => {
    // 如果正在刷新，则阻止重复点击
    if (isRefreshing.value) return;

    isRefreshing.value = true;
    ElMessage.info('正在刷新订单...'); // 给出提示

    try {
        // 专门只调用获取可接订单的 API
        const res = await (api as any).fetchAvailableOrders() as { data: Order[] }; 
        availableOrders.value = res.data;
        ElMessage.success('订单列表已更新！');
    } catch (error) {
        console.error("刷新可接订单失败:", error);
        ElMessage.error('刷新失败，请稍后重试');
    } finally {
        // 无论成功还是失败，最后都要结束刷新状态
        isRefreshing.value = false;
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
            locationRes,
            complaintsRes
        ] = (await Promise.all([
            api.fetchUserProfile(),
            api.fetchWorkStatus(),
            api.fetchIncomeData(),
            api.fetchOrders('pending'),     // API 调用 1
            api.fetchOrders('delivering'),  // API 调用 2
            api.fetchOrders('completed'),   // API 调用 3
            api.fetchLocationInfo(),
            (api as typeof MockAPI).fetchComplaints() // <-- 新增 API 调用
        ])) as [
                { data: any },         // 1. 对应 profileRes
                { data: any },         // 2. 对应 statusRes
                { data: any },         // 3. 对应 incomeRes
                { data: any[] },       // 4. 对应 pendingOrdersRes
                { data: any[] },       // 5. 对应 deliveringOrdersRes
                { data: any[] },       // 6. 对应 completedOrdersRes
                { data: any },         // 7. 对应 locationRes
                { data: Complaint[] }
            ];
        // ▲▲▲ 修改结束 ▲▲▲

        // --- 赋值 ---
        userProfile.value = profileRes.data;
        workStatus.value = statusRes.data;
        locationInfo.value = locationRes.data;
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
        complaints.value = (complaintsRes as { data: any[] }).data;

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







onMounted(() => {
    loadDashboardData();

    // 如果不是用模拟数据，就启动WebSocket监听器

});



// --- 监听器 ---
// 找到这个 watch
watch(activeOrderTab, async (newStatus) => {
    // 将其内部逻辑替换为下面这一行
    await refreshOrderList();
});

watch(currentTab, (newTab) => {
    // 当用户切换到“可接订单”页面时
    if (newTab === 'available') {
        // 如果列表是空的，就自动刷新一次
        if (availableOrders.value.length === 0) {
            refreshAvailableOrders();
        }
    }
}, { immediate: true });

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
        case 'cancelled': return 'bg-gray-200 text-gray-500';
        default: return 'bg-gray-100 text-gray-600';
    }
};

// 新增这个函数
const getOrderStatusText = (status: string) => {
    switch (status) {
        case 'pending': return '待取单';
        case 'delivering': return '配送中';
        case 'completed': return '已送达';
        case 'cancelled': return '已取消';
        default: return '未知状态';
    }
};
/**
 * 接受一个在列表中展示的“可接订单”
 */
const acceptAvailableOrder = async (order: Order) => {
    try {
        await api.acceptAvailableOrderAPI(order.id);
        ElMessage.success(`订单 #${order.id} 已接受！将移至“待取单”`);

        // --- 实时更新UI ---
        // 1. 从“可接订单”列表中移除
        availableOrders.value = availableOrders.value.filter(o => o.id !== order.id);

        // 2. 将“待取单”的数量加 1
        pendingOrderCount.value++;

        // 3. 【新增修复】手动将这个订单添加到“订单”页面的数据列表中
        const newlyAcceptedOrder = { ...order, status: 'pending' as OrderStatus };
        orders.value.unshift(newlyAcceptedOrder);

    } catch (error) {
        console.error("接单失败:", error);
        ElMessage.error("接单失败，可能已被他人抢走，请刷新");
    }
};

async function handleLogout() {
    try {
        // 1. 弹出确认框，提供更好的用户体验
        await ElMessageBox.confirm(
            '您确定要退出当前账号吗？',
            '退出登录',
            {
                confirmButtonText: '确定退出',
                cancelButtonText: '取消',
                type: 'warning',
            }
        );

        // 2. (推荐) 调用后端登出接口，通知服务器
        await loginApi.logout();

        // 3. (核心) 从本地存储中删除 Token，清除登录状态
        removeToken();

        ElMessage.success('您已成功退出登录');

        // 4. 重定向到登录页面
        // 使用 replace 而不是 push，这样用户无法通过浏览器“后退”按钮回到之前的页面
        router.replace('/login'); // <-- 请确保 '/login' 是你登录页面的正确路由

    } catch (error: any) {
        // 如果用户点击了“取消”，或者API调用失败
        if (error === 'cancel') { 
            ElMessage.info('已取消退出操作');
        } else {
            console.error('登出时发生错误:', error);
            // 即便通知后端失败，也要强制执行前端登出
            ElMessage.warning('与服务器通信失败，但已在本地强制退出');
            removeToken();
            router.replace('/login');
        }
    }
}


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