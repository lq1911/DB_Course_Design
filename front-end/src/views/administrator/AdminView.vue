<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->
<template>
    <div class="min-h-screen bg-gray-50">
        <!-- 顶部导航栏 -->
        <!-- 顶部导航栏 (修改后) -->
        <header class="fixed top-0 left-0 right-0 bg-white border-b border-gray-200 z-50">
            <div class="flex items-center justify-between px-6 h-16">
                <!-- Logo 和标题 (这部分没问题) -->
                <div class="flex items-center space-x-4">
                    <div class="flex items-center space-x-3">
                        <div class="w-8 h-8 bg-orange-500 rounded-lg flex items-center justify-center">
                            <i class="fas fa-utensils text-white text-sm"></i>
                        </div>
                        <h1 class="text-xl font-semibold text-gray-800">外卖管理系统</h1>
                    </div>
                </div>

                <!-- 用户信息和登出按钮的区块 -->
                <div class="flex items-center space-x-4">
                    <!-- 【修改】用 v-if 包裹用户信息，防止数据加载完成前出错 -->
                    <div v-if="currentUser" class="flex items-center space-x-3">
                        <img :src="'https://s1.aigei.com/src/img/png/f7/f734d8198b614d0a9356196cd83c6758.png?imageMogr2/auto-orient/thumbnail/!282x282r/gravity/Center/crop/282x282/quality/85/%7CimageView2/2/w/282&e=2051020800&token=P7S2Xpzfz11vAkASLTkfHN7Fw-oOZBecqeJaxypL:FldJin-4wd319skieoNSW_v2zAY='"
                             :alt="currentUser.username + '的头像'"
                            class="w-8 h-8 rounded-full object-cover">
                        <span class="text-sm text-gray-700">{{ currentUser.username }}</span>
                    </div>
                    <!-- 【修改】这是现在唯一的一个登出按钮，并绑定了点击事件 -->
                    <button @click="handleLogout" class="text-gray-500 hover:text-gray-700 cursor-pointer">
                        <i class="fas fa-sign-out-alt text-lg"></i>
                    </button>
                </div>
            </div>
        </header>
        <div class="flex pt-16">
            <!-- 左侧导航菜单 -->
            <aside class="fixed left-0 top-16 bottom-0 w-64 bg-white border-r border-gray-200 overflow-y-auto">
                <nav class="p-4">
                    <ul class="space-y-2">
                        <li>
                            <a href="#"
                                :class="{ 'bg-orange-500 text-white': activeMenu === 'admin', 'text-gray-700 hover:bg-gray-100': activeMenu !== 'admin' }"
                                class="flex items-center space-x-3 px-4 py-3 rounded-lg transition-colors cursor-pointer"
                                @click="activeMenu = 'admin'">
                                <i class="fas fa-user-shield text-lg"></i>
                                <span>管理员信息</span>
                            </a>
                        </li>
                        <li>
                            <a href="#"
                                :class="{ 'bg-orange-500 text-white': activeMenu === 'afterSales', 'text-gray-700 hover:bg-gray-100': activeMenu !== 'afterSales' }"
                                class="flex items-center space-x-3 px-4 py-3 rounded-lg transition-colors cursor-pointer"
                                @click.prevent="activeMenu = 'afterSales'">
                                <i class="fas fa-headset text-lg"></i>
                                <span>售后处理中心</span>
                            </a>
                        </li>
                        <li>
                            <a href="#"
                                :class="{ 'bg-orange-500 text-white': activeMenu === 'complaints', 'text-gray-700 hover:bg-gray-100': activeMenu !== 'complaints' }"
                                class="flex items-center space-x-3 px-4 py-3 rounded-lg transition-colors cursor-pointer"
                                @click.prevent="activeMenu = 'complaints'">
                                <i class="fas fa-exclamation-triangle text-lg"></i>
                                <span>投诉处理中心</span>
                            </a>
                        </li>
                        <li>
                            <a href="#"
                                :class="{ 'bg-orange-500 text-white': activeMenu === 'violations', 'text-gray-700 hover:bg-gray-100': activeMenu !== 'violations' }"
                                class="flex items-center space-x-3 px-4 py-3 rounded-lg transition-colors cursor-pointer"
                                @click="activeMenu = 'violations'">
                                <i class="fas fa-gavel text-lg"></i>
                                <span>违规举报处理</span>
                            </a>
                        </li>
                        <li>
                            <a href="#"
                                :class="{ 'bg-orange-500 text-white': activeMenu === 'reviews', 'text-gray-700 hover:bg-gray-100': activeMenu !== 'reviews' }"
                                class="flex items-center space-x-3 px-4 py-3 rounded-lg transition-colors cursor-pointer"
                                @click="activeMenu = 'reviews'">
                                <i class="fas fa-comment-dots text-lg"></i>
                                <span>评论审核管理</span>
                            </a>
                        </li>
                    </ul>
                </nav>
            </aside>
            <!-- 右侧内容区域 -->
            <main class="flex-1 ml-64 p-6">
                <!-- 面包屑导航 -->
                <div class="mb-6">
                    <nav class="flex items-center space-x-2 text-sm text-gray-600">
                        <span>首页</span>
                        <i class="fas fa-chevron-right text-xs"></i>
                        <span class="text-orange-500">{{ getBreadcrumb() }}</span>
                    </nav>
                </div>
                <!-- 售后处理中心页面 -->
                <div v-if="activeMenu === 'afterSales'">
                    <div class="bg-white rounded-xl shadow-sm border border-gray-100">
                        <div class="p-6 border-b border-gray-100">
                            <div class="flex items-center justify-between mb-4">
                                <h2 class="text-lg font-semibold text-gray-900">售后处理中心</h2>
                            </div>
                            <!-- 搜索和筛选区域 -->
                            <div class="flex items-center space-x-4 mb-6">
                                <div class="flex-1 max-w-md">
                                    <div class="relative">
                                        <input type="text" placeholder="搜索售后申请编号..."
                                            class="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm"
                                            v-model="searchQuery">
                                        <i
                                            class="fas fa-search absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 text-sm"></i>
                                    </div>
                                </div>
                                <div class="flex space-x-2">
                                    <button v-for="status in afterSalesStatuses" :key="status.value"
                                        :class="{ 'bg-orange-500 text-white': selectedAfterSalesStatus === status.value, 'bg-gray-100 text-gray-700 hover:bg-gray-200': selectedAfterSalesStatus !== status.value }"
                                        class="px-4 py-2 rounded-lg text-sm font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap"
                                        @click="selectedAfterSalesStatus = status.value">
                                        {{ status.label }}
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- 售后申请列表 -->
                        <div class="overflow-x-auto">
                            <table class="w-full">
                                <thead class="bg-gray-50">
                                    <tr>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            申请编号</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            订单编号</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            申请时间</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            情况说明</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            处理措施</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredAfterSales" :key="item.applicationId"
                                        class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.applicationId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.orderId }}
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{
                                            item.applicationTime }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{
                                            item.description }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900">{{ item.punishment || '-' }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <button @click="openAfterSaleDetail(item)"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">
                                                <span class="flex items-center">
                                                    <i class="fas fa-eye mr-1"></i>
                                                    查看详情
                                                </span>
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- 分页 -->
                        <div class="px-6 py-4 border-t border-gray-200">
                            <div class="flex items-center justify-between">
                                <div class="text-sm text-gray-700">
                                    显示第 1-10 条，共 {{ afterSalesList.length }} 条记录
                                </div>
                                <div class="flex space-x-2">
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">上一页</button>
                                    <button
                                        class="px-3 py-1 bg-orange-500 text-white rounded text-sm cursor-pointer !rounded-button whitespace-nowrap">1</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">2</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">下一页</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- 投诉处理中心页面 -->
                <div v-if="activeMenu === 'complaints'">
                    <div class="bg-white rounded-xl shadow-sm border border-gray-100">
                        <div class="p-6 border-b border-gray-100">
                            <div class="flex items-center justify-between mb-4">
                                <h2 class="text-lg font-semibold text-gray-900">投诉处理中心</h2>
                            </div>
                            <!-- 搜索和筛选区域 -->
                            <div class="flex items-center space-x-4 mb-6">
                                <div class="flex-1 max-w-md">
                                    <div class="relative">
                                        <input type="text" placeholder="搜索投诉编号..."
                                            class="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm"
                                            v-model="complaintSearchQuery">
                                        <i
                                            class="fas fa-search absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 text-sm"></i>
                                    </div>
                                </div>
                                <div class="flex space-x-2">
                                    <button v-for="status in complaintStatuses" :key="status.value"
                                        :class="{ 'bg-orange-500 text-white': selectedComplaintStatus === status.value, 'bg-gray-100 text-gray-700 hover:bg-gray-200': selectedComplaintStatus !== status.value }"
                                        class="px-4 py-2 rounded-lg text-sm font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap"
                                        @click="selectedComplaintStatus = status.value">
                                        {{ status.label }}
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- 投诉列表 -->
                        <div class="overflow-x-auto">
                            <table class="w-full">
                                <thead class="bg-gray-50">
                                    <tr>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            投诉编号</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            投诉对象</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            投诉内容</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            申请时间</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            处理措施</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredComplaints" :key="item.complaintId"
                                        class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.complaintId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.target }}
                                        </td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{ item.content }}
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{
                                            item.applicationTime }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900">{{ item.punishment || '-' }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <button @click="openComplaintDetail(item)"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">查看详情</button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- 分页 -->
                        <div class="px-6 py-4 border-t border-gray-200">
                            <div class="flex items-center justify-between">
                                <div class="text-sm text-gray-700">
                                    显示第 1-10 条，共 {{ complaintsList.length }} 条记录
                                </div>
                                <div class="flex space-x-2">
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">上一页</button>
                                    <button
                                        class="px-3 py-1 bg-orange-500 text-white rounded text-sm cursor-pointer !rounded-button whitespace-nowrap">1</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">2</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">下一页</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- 店铺违规处理页面 -->
                <div v-if="activeMenu === 'violations'">
                    <div class="bg-white rounded-xl shadow-sm border border-gray-100">
                        <div class="p-6 border-b border-gray-100">
                            <div class="flex items-center justify-between mb-4">
                                <h2 class="text-lg font-semibold text-gray-900">店铺违规处理</h2>
                            </div>
                            <!-- 搜索和筛选区域 -->
                            <div class="flex items-center space-x-4 mb-6">
                                <div class="flex-1 max-w-md">
                                    <div class="relative">
                                        <input type="text" placeholder="搜索处罚编号或店铺名称..."
                                            class="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm"
                                            v-model="violationSearchQuery">
                                        <i
                                            class="fas fa-search absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 text-sm"></i>
                                    </div>
                                </div>
                                <div class="flex space-x-2">
                                    <button v-for="status in violationStatuses" :key="status.value"
                                        :class="{ 'bg-orange-500 text-white': selectedViolationStatus === status.value, 'bg-gray-100 text-gray-700 hover:bg-gray-200': selectedViolationStatus !== status.value }"
                                        class="px-4 py-2 rounded-lg text-sm font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap"
                                        @click="selectedViolationStatus = status.value">
                                        {{ status.label }}
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- 违规处理列表 -->
                        <div class="overflow-x-auto">
                            <table class="w-full">
                                <thead class="bg-gray-50">
                                    <tr>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            处罚编号</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            店铺名称</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            处罚原因</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            商家处罚措施</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            店铺处罚措施</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            处罚时间</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredViolations" :key="item.punishmentId"
                                        class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.punishmentId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.storeName
                                            }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{ item.reason }}
                                        </td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{
                                            punishmentOptions.violations.merchant.find(option => option.value === item.merchantPunishment)?.label || item.merchantPunishment }}
                                        </td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{
                                            punishmentOptions.violations.store.find(option => option.value === item.storePunishment)?.label || item.storePunishment }}
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{
                                            item.punishmentTime }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <button @click="openViolationDetail(item)"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">
                                                <span class="flex items-center">
                                                    <i class="fas fa-eye mr-1"></i>
                                                    查看详情
                                                </span>
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- 分页 -->
                        <div class="px-6 py-4 border-t border-gray-200">
                            <div class="flex items-center justify-between">
                                <div class="text-sm text-gray-700">
                                    显示第 1-10 条，共 {{ violationsList.length }} 条记录
                                </div>
                                <div class="flex space-x-2">
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">上一页</button>
                                    <button
                                        class="px-3 py-1 bg-orange-500 text-white rounded text-sm cursor-pointer !rounded-button whitespace-nowrap">1</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">2</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">下一页</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- 评论审核管理页面 -->
                <div v-if="activeMenu === 'reviews'">
                    <div class="bg-white rounded-xl shadow-sm border border-gray-100">
                        <div class="p-6 border-b border-gray-100">
                            <div class="flex items-center justify-between mb-4">
                                <h2 class="text-lg font-semibold text-gray-900">评论审核管理</h2>
                            </div>
                            <!-- 搜索和筛选区域 -->
                            <div class="flex items-center space-x-4 mb-6">
                                <div class="flex-1 max-w-md">
                                    <div class="relative">
                                        <input type="text" placeholder="搜索评论内容..."
                                            class="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm"
                                            v-model="reviewSearchQuery">
                                        <i
                                            class="fas fa-search absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 text-sm"></i>
                                    </div>
                                </div>
                                <div class="flex space-x-2">
                                    <button v-for="status in reviewStatuses" :key="status.value"
                                        :class="{ 'bg-orange-500 text-white': selectedReviewStatus === status.value, 'bg-gray-100 text-gray-700 hover:bg-gray-200': selectedReviewStatus !== status.value }"
                                        class="px-4 py-2 rounded-lg text-sm font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap"
                                        @click="selectedReviewStatus = status.value">
                                        {{ status.label }}
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- 评论审核列表 -->
                        <div class="overflow-x-auto">
                            <table class="w-full">
                                <thead class="bg-gray-50">
                                    <tr>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            评论ID</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            用户名</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            评论类型 <!-- 【修改】这里由“店铺名称”改为“评论类型” -->
                                        </th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            评论内容</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            评分</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            提交时间</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredReviews" :key="item.reviewId" class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.reviewId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.username
                                            }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{
                                            item.type
                                            }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{ item.content }}
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                                            <div class="flex items-center">
                                                <span class="mr-1">{{ item.rating }}</span>
                                                <div class="flex text-yellow-400">
                                                    <i v-for="star in 5" :key="star"
                                                        :class="star <= item.rating ? 'fas fa-star' : 'far fa-star'"
                                                        class="text-xs"></i>
                                                </div>
                                            </div>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.submitTime
                                            }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <button @click="openReviewDetail(item)"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">
                                                <span class="flex items-center">
                                                    <i class="fas fa-eye mr-1"></i>
                                                    查看详情
                                                </span>
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- 分页 -->
                        <div class="px-6 py-4 border-t border-gray-200">
                            <div class="flex items-center justify-between">
                                <div class="text-sm text-gray-700">
                                    显示第 1-10 条，共 {{ reviewsList.length }} 条记录
                                </div>
                                <div class="flex space-x-2">
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">上一页</button>
                                    <button
                                        class="px-3 py-1 bg-orange-500 text-white rounded text-sm cursor-pointer !rounded-button whitespace-nowrap">1</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">2</button>
                                    <button
                                        class="px-3 py-1 border border-gray-300 rounded text-sm hover:bg-gray-50 cursor-pointer !rounded-button whitespace-nowrap">下一页</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div v-if="activeMenu === 'admin' && currentUser">
                    <div class="bg-white rounded-xl shadow-sm border border-gray-100">
                        <div class="p-6 border-b border-gray-100">
                            <h2 class="text-lg font-semibold text-gray-900">管理员信息管理</h2>
                        </div>
                        <div class="p-6">
                            <div class="max-w-2xl">
                                <!-- 头像和基本信息 -->
                                <div class="flex items-center space-x-6 mb-8">
                                    <img :src="'https://s1.aigei.com/src/img/png/f7/f734d8198b614d0a9356196cd83c6758.png?imageMogr2/auto-orient/thumbnail/!282x282r/gravity/Center/crop/282x282/quality/85/%7CimageView2/2/w/282&e=2051020800&token=P7S2Xpzfz11vAkASLTkfHN7Fw-oOZBecqeJaxypL:FldJin-4wd319skieoNSW_v2zAY='"
                                        class="w-24 h-24 rounded-full object-cover border-4 border-gray-100">
                                    <div>
                                        <h3 class="text-xl font-semibold text-gray-900">{{ currentUser.username }}</h3>
                                        <p class="text-gray-600">{{ currentUser.role }}</p>
                                        <p class="text-sm text-gray-500">注册时间：{{ currentUser.registrationDate }}</p>
                                    </div>
                                </div>

                                <!-- 信息表单 -->
                                <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">管理员ID</label>
                                        <input type="text" :value="currentUser.id" readonly
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">用户名</label>
                                        <input type="text" v-model="currentUser.username"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">手机号</label>
                                        <input type="text" :value="currentUser.phone" readonly
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">电子邮箱</label>
                                        <input type="email" v-model="currentUser.email" readonly
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">性别</label>
                                        <div class="flex space-x-4">
                                            <label class="flex items-center">
                                                <!-- 【修改】将 value="男" 修改为 value="M" -->
                                                <input type="radio" name="gender" value="M" v-model="currentUser.gender"
                                                    class="text-orange-500 focus:ring-orange-500">
                                                <span class="ml-2 text-sm text-gray-700">男</span>
                                            </label>
                                            <label class="flex items-center">
                                                <!-- 【修改】将 value="女" 修改为 value="F" (假设女性的代号是'F') -->
                                                <input type="radio" name="gender" value="F" v-model="currentUser.gender"
                                                    class="text-orange-500 focus:ring-orange-500">
                                                <span class="ml-2 text-sm text-gray-700">女</span>
                                            </label>
                                        </div>
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">姓名</label>
                                        <input type="text" :value="currentUser.realName" readonly
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">生日</label>
                                        <input type="date" v-model="currentUser.birthDate"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">管理对象</label>
                                        <div class="relative">
                                            <!-- 点击的文本框 -->
                                            <div @click="toggleDropdown" 
                                                class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm bg-white cursor-pointer flex justify-between items-center">
                                                <span class="text-gray-700">
                                                    {{ getSelectedText() }}
                                                </span>
                                                <svg class="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 9l-7 7-7-7"></path>
                                                </svg>
                                            </div>
                                            
                                            <!-- 下拉选项 -->
                                            <div v-show="dropdownVisible" 
                                                class="absolute z-10 w-full mt-1 bg-white border border-gray-300 rounded-lg shadow-lg">
                                                <div v-for="option in managementOptions" 
                                                    :key="option"
                                                    @click="toggleOption(option)"
                                                    class="flex items-center px-3 py-2 hover:bg-gray-50 cursor-pointer">
                                                    <input type="checkbox" 
                                                        :checked="isSelected(option)"
                                                        class="text-orange-500 focus:ring-orange-500 mr-2" readonly>
                                                    <span class="text-sm text-gray-700">{{ option }}</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">处理事项获评均分</label>
                                        <div class="flex items-center space-x-2">
                                            <input type="text" :value="currentUser.averageRating" readonly
                                                class="w-20 px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-sm">
                                        </div>
                                    </div>
                                    <div>
                                        <label class="flex items-center">
                                            <input type="checkbox" v-model="currentUser.isPublic"
                                                class="text-orange-500 focus:ring-orange-500">
                                            <span class="ml-2 text-sm text-gray-700">公开个人信息</span>
                                        </label>
                                    </div>
                                </div>

                                <!-- 操作按钮 -->
                                <div class="mt-8 flex space-x-4">
                                    <button @click="handleSaveChanges" :disabled="isSaving"
                                        class="px-6 py-2 bg-orange-500 text-white rounded-lg hover:bg-orange-600 transition-colors cursor-pointer !rounded-button whitespace-nowrap disabled:opacity-50">
                                        {{ isSaving ? '保存中...' : '保存修改' }}
                                    </button>
                                    <button @click="resetForm" :disabled="isSaving"
                                        class="px-6 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition-colors cursor-pointer !rounded-button whitespace-nowrap">
                                        重置
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </main>

        </div>
        <!-- 售后详情弹窗 -->
        <el-dialog v-model="showAfterSaleDetail" title="售后详情" width="800px" class="after-sale-detail-dialog">
            <div v-if="currentAfterSale" class="after-sale-detail">
                <div class="border-b border-gray-200 pb-6 mb-6">
                    <div class="flex items-start justify-between">
                        <div class="flex items-center space-x-4">
                            <div class="w-12 h-12 bg-gray-100 rounded-full flex items-center justify-center">
                                <i class="fas fa-headset text-gray-400"></i>
                            </div>
                            <div>
                                <h3 class="text-lg font-semibold text-gray-900">售后申请编号：{{
                                    currentAfterSale.applicationId }}
                                </h3>
                                <p class="text-sm text-gray-500">订单编号：{{ currentAfterSale.orderId }}</p>
                            </div>
                        </div>
                        <span :class="getStatusClass(currentAfterSale.status)" class="px-3 py-1 rounded-full text-sm">
                            {{ currentAfterSale.status }}
                        </span>
                    </div>
                </div>
                <div class="grid grid-cols-2 gap-6 mb-6">
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">申请时间</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <p class="text-gray-900">{{ currentAfterSale.applicationTime }}</p>
                        </div>
                    </div>
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">售后类型</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <p class="text-gray-900">退款申请</p>
                        </div>
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">问题描述</h4>
                    <div class="p-4 bg-gray-50 rounded-lg">
                        <p class="text-gray-900">{{ currentAfterSale.description }}</p>
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">处理备注</h4>
                    <!-- 【修改】添加 disabled 属性 -->
                    <el-input 
                        v-model="afterSaleNote" 
                        type="textarea" 
                        :rows="3" 
                        placeholder="请输入处理备注" 
                        maxlength="500"
                        show-word-limit 
                        :disabled="currentAfterSale.status !== '待处理'"
                    />
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">处罚措施</h4>
                    <!-- 【修改】添加 disabled 属性 -->
                    <el-select 
                        v-model="selectedPunishment" 
                        class="w-full mb-4" 
                        placeholder="请选择处罚措施"
                        :disabled="currentAfterSale.status !== '待处理'"
                    >
                        <el-option 
                            v-for="option in punishmentOptions.afterSales" 
                            :key="option.value"
                            :label="option.label" 
                            :value="option.value" 
                        />
                    </el-select>
                    <h4 class="text-sm font-medium text-gray-700 mb-2">处罚原因</h4>
                    <!-- 【修改】添加 disabled 属性 -->
                    <el-input 
                        v-model="punishmentReason" 
                        type="textarea" 
                        :rows="3" 
                        placeholder="请输入处罚原因" 
                        maxlength="500"
                        show-word-limit 
                        :disabled="currentAfterSale.status !== '待处理'"
                    />
                </div>
                <div class="flex justify-end space-x-4">
                    <el-button @click="showAfterSaleDetail = false">取消</el-button>
                    <el-button v-if="currentAfterSale.status === '待处理'" type="primary" @click="handleAfterSaleAction">
                        执行处罚
                    </el-button>
                </div>
            </div>
        </el-dialog>
        <!-- 投诉详情弹窗 -->
        <el-dialog v-model="showComplaintDetail" title="投诉详情" width="800px" class="complaint-detail-dialog">
            <div v-if="currentComplaint" class="complaint-detail">
                <div class="border-b border-gray-200 pb-6 mb-6">
                    <div class="flex items-start justify-between">
                        <div class="flex items-center space-x-4">
                            <div class="w-12 h-12 bg-gray-100 rounded-full flex items-center justify-center">
                                <i class="fas fa-user text-gray-400"></i>
                            </div>
                            <div>
                                <h3 class="text-lg font-semibold text-gray-900">投诉编号：{{
                                    currentComplaint.complaintId }}</h3>
                                <p class="text-sm text-gray-500">提交时间：{{ currentComplaint.applicationTime }}</p>
                            </div>
                        </div>
                        <span :class="getStatusClass(currentComplaint.status)" class="px-3 py-1 rounded-full text-sm">
                            {{ currentComplaint.status }}
                        </span>
                    </div>
                </div>
                <div class="grid grid-cols-2 gap-6 mb-6">
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">投诉对象</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <p class="text-gray-900 font-medium">{{ currentComplaint.target }}</p>
                        </div>
                    </div>
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">投诉类型</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <p class="text-gray-900">服务态度问题</p>
                        </div>
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">投诉内容</h4>
                    <div class="p-4 bg-gray-50 rounded-lg">
                        <p class="text-gray-900">{{ currentComplaint.content }}</p>
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">处罚措施</h4>
                    <!-- 【修改】添加 disabled 属性 -->
                    <el-select 
                        v-model="selectedComplaintPunishment" 
                        class="w-full mb-4" 
                        placeholder="请选择处罚措施"
                        :disabled="currentComplaint.status !== '待处理'"
                    >
                        <el-option v-for="option in punishmentOptions.complaints" :key="option.value"
                            :label="option.label" :value="option.value" />
                    </el-select>
                    
                    <h4 class="text-sm font-medium text-gray-700 mb-2">罚款金额</h4>
                    <!-- 【修改】添加 disabled 属性 -->
                    <el-input-number 
                        v-model="complaintFine" 
                        :min="0" 
                        :precision="2" 
                        :step="10"
                        controls-position="right"
                        placeholder="请输入罚款金额" 
                        class="w-full mb-4" 
                        :disabled="currentComplaint.status !== '待处理'"
                    />
                    
                    <h4 class="text-sm font-medium text-gray-700 mb-2">处罚原因</h4>
                    <!-- 【修改】添加 disabled 属性 -->
                    <el-input 
                        v-model="complaintPunishmentReason" 
                        type="textarea" 
                        :rows="3" 
                        placeholder="请输入处罚原因"
                        maxlength="500" 
                        show-word-limit 
                        class="mb-4"
                        :disabled="currentComplaint.status !== '待处理'"
                    />
                    
                    <h4 class="text-sm font-medium text-gray-700 mb-2">处理备注</h4>
                    <!-- 【修改】添加 disabled 属性 -->
                    <el-input 
                        v-model="complaintNote" 
                        type="textarea" 
                        :rows="3" 
                        placeholder="请输入处理备注" 
                        maxlength="500"
                        show-word-limit 
                        :disabled="currentComplaint.status !== '待处理'"
                    />
                </div>
                <div class="flex justify-end space-x-4">
                    <el-button @click="showComplaintDetail = false">取消</el-button>
                    <el-button v-if="currentComplaint.status === '待处理'" type="primary"
                        @click="handleComplaintProcess">执行处罚</el-button>
                </div>
            </div>
        </el-dialog>
        <!-- 违规举报详情弹窗 -->
        <el-dialog v-model="showViolationDetail" title="违规举报详情" width="800px" class="violation-detail-dialog">
            <div v-if="currentViolation" class="violation-detail">
                <div class="border-b border-gray-200 pb-6 mb-6">
                    <div class="flex items-start justify-between">
                        <div class="flex items-center space-x-4">
                            <div class="w-12 h-12 bg-gray-100 rounded-full flex items-center justify-center">
                                <i class="fas fa-store text-gray-400"></i>
                            </div>
                            <div>
                                <h3 class="text-lg font-semibold text-gray-900">{{ currentViolation.storeName }}
                                </h3>
                                <p class="text-sm text-gray-500">处罚编号：{{ currentViolation.punishmentId }}</p>
                            </div>
                        </div>
                        <span :class="getStatusClass(currentViolation.status)" class="px-3 py-1 rounded-full text-sm">
                            {{ currentViolation.status }}
                        </span>
                    </div>
                </div>
                <div class="grid grid-cols-2 gap-6 mb-6">
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">处罚时间</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <p class="text-gray-900">{{ currentViolation.punishmentTime }}</p>
                        </div>
                    </div>
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">违规类型</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <p class="text-gray-900">食品安全</p>
                        </div>
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">违规原因</h4>
                    <div class="p-4 bg-gray-50 rounded-lg">
                        <p class="text-gray-900">{{ currentViolation.reason }}</p>
                    </div>
                </div>
                <div class="grid grid-cols-2 gap-6 mb-6">
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">商家处罚措施</h4>
                        <!-- 【核心修改】将 v-if 替换为 :disabled -->
                        <el-select 
                            v-model="selectedMerchantPunishment"
                            class="w-full" 
                            placeholder="请选择商家处罚措施"
                            :disabled="currentViolation.status !== '待处理'"
                        >
                            <el-option v-for="option in punishmentOptions.violations.merchant" :key="option.value"
                                :label="option.label" :value="option.value" />
                        </el-select>
                        <!-- 【修改】删除原来的 v-else 显示区域，因为 el-select 在禁用时会自动显示已选值 -->
                    </div>
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">店铺处罚措施</h4>
                        <!-- 【核心修改】将 v-if 替换为 :disabled -->
                        <el-select 
                            v-model="selectedStorePunishment"
                            class="w-full" 
                            placeholder="请选择店铺处罚措施"
                            :disabled="currentViolation.status !== '待处理'"
                        >
                            <el-option v-for="option in punishmentOptions.violations.store" :key="option.value"
                                :label="option.label" :value="option.value" />
                        </el-select>
                        <!-- 【修改】删除原来的 v-else 显示区域 -->
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">处理备注</h4>
                    <!-- 【修改】禁用条件改为 status === '已完成' -->
                    <el-input 
                        v-model="violationNote" 
                        type="textarea" 
                        :rows="3" 
                        placeholder="请输入处理备注" 
                        maxlength="500"
                        show-word-limit 
                        :disabled="currentViolation.status === '已完成'"
                    />
                </div>
                <div class="flex justify-end space-x-4">
                    <el-button @click="showViolationDetail = false">取消</el-button>
                    <el-button v-if="currentViolation.status === '待处理'" type="primary"
                        @click="handleViolationAction('process')">开始执行</el-button>
                    <el-button v-if="currentViolation.status === '执行中'" type="success"
                        @click="handleViolationAction('complete')">完成执行</el-button>
                </div>
            </div>
        </el-dialog>
        <!-- 评论详情弹窗 -->
        <el-dialog v-model="showReviewDetail" title="评论详情" width="800px" class="review-detail-dialog">
            <div v-if="currentReview" class="review-detail">
                <!-- 用户和评分信息 (这部分不变) -->
                <div class="border-b border-gray-200 pb-6 mb-6">
                    <div class="flex items-start justify-between">
                        <div class="flex items-center space-x-4">
                            <div class="w-12 h-12 bg-gray-100 rounded-full flex items-center justify-center">
                                <i class="fas fa-user text-gray-400"></i>
                            </div>
                            <div>
                                <h3 class="text-lg font-semibold text-gray-900">{{ currentReview.username }}
                                </h3>
                                <p class="text-sm text-gray-500">{{ currentReview.submitTime }}</p>
                            </div>
                        </div>
                        <div class="flex items-center space-x-1 text-yellow-400">
                            <i v-for="star in 5" :key="star"
                                :class="star <= currentReview.rating ? 'fas fa-star' : 'far fa-star'"></i>
                            <span class="ml-2 text-gray-700">{{ currentReview.rating }} 分</span>
                        </div>
                    </div>
                </div>
                <!-- 店铺和评论信息 (这部分不变) -->
                <div class="grid grid-cols-2 gap-6 mb-6">
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">评论类型</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <p class="text-gray-900 font-medium">{{ currentReview.type }}</p>
                        </div>
                    </div>
                    <div>
                        <h4 class="text-sm font-medium text-gray-700 mb-2">评论状态</h4>
                        <div class="p-4 bg-gray-50 rounded-lg">
                            <span :class="getStatusClass(currentReview.status)"
                                class="inline-block px-3 py-1 rounded-full text-sm">
                                {{ currentReview.status }}
                            </span>
                        </div>
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">评论内容</h4>
                    <div class="p-4 bg-gray-50 rounded-lg">
                        <p class="text-gray-900">{{ currentReview.content }}</p>
                    </div>
                </div>
                <div class="mb-6">
                    <h4 class="text-sm font-medium text-gray-700 mb-2">图片附件</h4>
                    <div class="grid grid-cols-4 gap-4">
                        <div v-for="(image, index) in reviewImages" :key="index" class="relative aspect-square">
                            <img :src="image" alt="评论图片" class="w-full h-full object-cover rounded-lg">
                        </div>
                    </div>
                </div>

                <!-- 【核心修改】将原来的表单替换为新的按钮组 -->
                <div class="flex justify-end space-x-4">
                    <el-button @click="showReviewDetail = false">取消</el-button>
                    <!-- 只在“待处理”状态下显示操作按钮 -->
                    <template v-if="currentReview.status === '待处理'">
                        <el-button type="danger" @click="processReview('reject')">判定违规</el-button>
                        <el-button type="success" @click="processReview('approve')">审核通过</el-button>
                    </template>
                </div>
            </div>
        </el-dialog>
    </div>

</template>
<script lang="ts" setup>
// =================================================================
// 步骤 1: 导入必要的模块
// =================================================================
import { ref, computed, onMounted, readonly } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import { useRouter } from 'vue-router';
import axios from 'axios'; // 导入axios用于真实API请求

// =================================================================
// 步骤 2: 定义数据类型
// =================================================================
interface AdminInfo {
    id: string; // e.g., "ADM001"
    username: string;
    realName: string; // e.g., "张伟"
    role: string; // e.g., "系统管理员"
    registrationDate: string; // e.g., "2023-01-15"
    avatarUrl: string;
    phone: string;
    email: string;
    gender: '男' | '女';
    birthDate: string; // e.g., "1990-05-15"
    managementScope: string;
    averageRating: number;
    isPublic: boolean;
}


interface AfterSaleItem {
    applicationId: string;
    orderId: string;
    applicationTime: string;
    description: string;
    status: '待处理' | '已完成';
    punishment: string;
    punishmentReason?: string;
    processingNote?: string;
}

interface ComplaintItem {
    complaintId: string;
    target: string;
    content: string;
    applicationTime: string;
    status: '待处理' | '已完成';
    punishment: string;
    fine: string;
    punishmentReason?: string;
    processingNote?: string;
}

interface ViolationItem {
    punishmentId: string;
    storeName: string;
    reason: string;
    merchantPunishment: string;
    storePunishment: string;
    punishmentTime: string;
    status: '待处理' | '执行中' | '已完成';
    processingNote?: string;
}

interface ReviewItem {
    reviewId: string;
    username: string;
    type: string;
    content: string;
    rating: number;
    submitTime: string;
    status: '待处理' | '已完成' | '违规';
}

// =================================================================
// 步骤 3: API定义和切换逻辑
// =================================================================

// 3.1 ----------------- 模拟API实现 -----------------
const mockApi = {
    getAfterSalesList: async (): Promise<AfterSaleItem[]> => ([
        { applicationId: 'AS202401001', orderId: 'ORD202401001', applicationTime: '2024-01-15 14:30', description: '商品质量问题，要求退款', status: '待处理', punishment: '-' },
        { applicationId: 'AS202401002', orderId: 'ORD202401002', applicationTime: '2024-01-15 13:45', description: '配送延误，商品已变质', status: '待处理', punishment: '-' },
        { applicationId: 'AS202401003', orderId: 'ORD202401003', applicationTime: '2024-01-15 12:20', description: '商品与描述不符', status: '已完成', punishment: '全额退款' },
    ]),
    getComplaintsList: async (): Promise<ComplaintItem[]> => ([
        { complaintId: 'CP202401001', target: '骑手张三', content: '配送员态度恶劣，服务质量差', applicationTime: '2024-01-15 14:30', status: '待处理', punishment: '-', fine: '' },
        { complaintId: 'CP202401002', target: '商家李记餐厅', content: '商家出餐速度慢，影响配送时效', applicationTime: '2024-01-15 13:45', status: '待处理', punishment: '-', fine: ''},
        { complaintId: 'CP202401003', target: '骑手王五', content: '配送员未按时送达，且态度不好', applicationTime: '2024-01-15 12:20', status: '已完成', punishment: '暂停接单 3 天', fine: '' }
    ]),
    getViolationsList: async (): Promise<ViolationItem[]> => ([
        { punishmentId: 'PUN202401001', storeName: '张记小炒', reason: '食品安全问题，使用过期食材制作食品', merchantPunishment: '-', storePunishment: '-', punishmentTime: '2024-01-15 14:30', status: '待处理' },
        { punishmentId: 'PUN202401002', storeName: '美味餐厅', reason: '虚假宣传，图片与实物不符', merchantPunishment: '罚款500元', storePunishment: '下架违规商品', punishmentTime: '2024-01-15 13:45', status: '已完成' },
        { punishmentId: 'PUN202401003', storeName: '快送外卖', reason: '配送员私自拆开包装', merchantPunishment: '-', storePunishment: '-', punishmentTime: '2024-01-15 12:20', status: '待处理' }
    ]),
    getReviewsList: async (): Promise<ReviewItem[]> => ([
        { reviewId: 'RV202401001', username: '用户张三', type:'普通评论', content: '味道不错，配送也很快，推荐！', rating: 5, submitTime: '2024-01-15 14:30', status: '待处理' },
        { reviewId: 'RV202401002', username: '用户李四', type:'普通评论', content: '包装很好，食物新鲜，服务态度也不错', rating: 4, submitTime: '2024-01-15 13:45', status: '已完成' },
        { reviewId: 'RV202401003', username: '用户王五', type:'普通评论', content: '这家店的食物质量有问题，不建议购买', rating: 1, submitTime: '2024-01-15 12:20', status: '已完成' }
    ]),
    getAdminInfo: async (): Promise<AdminInfo> => ({
        id: 'ADM001',
        username: 'admin_zhang',
        realName: '张伟',
        role: '系统管理员',
        registrationDate: '2023-01-15',
        avatarUrl: 'https://readdy.ai/api/search-image?query=professional%20business%20administrator%20avatar%20portrait%20with%20clean%20white%20background%20modern%20corporate%20headshot%20style%20high%20quality%20detailed%20facial%20features%20confident%20expression&width=120&height=120&seq=admin-profile-001&orientation=squarish',
        phone: '13800138000',
        email: 'admin@fooddelivery.com',
        gender: '男',
        birthDate: '1990-05-15',
        managementScope: '售后处理、投诉管理、评论审核',
        averageRating: 4.8,
        isPublic: true,
    }),
    updateAdminInfo: async (data: AdminInfo) => { console.log('【Mock API】更新管理员信息:', data); return { success: true, data }; },
    updateAfterSale: async (item: AfterSaleItem) => { console.log('【Mock API】更新售后:', item); return { success: true, data: item }; },
    updateComplaint: async (item: ComplaintItem) => { console.log('【Mock API】更新投诉:', item); return { success: true, data: item }; },
    updateViolation: async (item: ViolationItem) => { console.log('【Mock API】更新违规:', item); return { success: true, data: item }; },
    updateReview: async (item: ReviewItem) => { console.log('【Mock API】更新评论:', item); return { success: true, data: item }; },
};

// 3.2 ----------------- 真实API实现 -----------------
const apiClient = axios.create({ baseURL: 'http://localhost:5250/api', timeout: 5000 }); // 根据你的后端地址修改 baseURL

apiClient.interceptors.request.use(
    (config) => {
        // 1. 从 localStorage 中获取 Token
        const token = localStorage.getItem('authToken');

        // 2. 如果 Token 存在，则将其添加到请求头中
        if (token) {
            // 这里的 'Bearer ' 是一个标准的格式，后端会需要它来正确解析Token
            config.headers.Authorization = `Bearer ${token}`;
        }

        // 3. 返回配置好的请求对象，让请求继续发送
        return config;
    },
    (error) => {
        // 对请求错误做些什么
        return Promise.reject(error);
    }
);






// 3.2 ----------------- 真实API实现 (修改后) -----------------

const realApi = {
    // --- 获取列表 (GET请求) - 这部分保持不变，因为它们返回的是数组 ---
    getAfterSalesList: () => apiClient.get<AfterSaleItem[]>('/admin/after-sales/mine').then(res => res.data),
    getComplaintsList: () => apiClient.get<ComplaintItem[]>('/admin/delivery-complaints/mine').then(res => res.data),
    getViolationsList: () => apiClient.get<ViolationItem[]>('/admin/violation-penalties/mine').then(res => res.data),
    getReviewsList: () => apiClient.get<ReviewItem[]>('/admin/review-comments/mine').then(res => res.data),
    getAdminInfo: () => apiClient.get<AdminInfo>('/admin/info').then(res => res.data),

    // --- 更新数据 (PUT请求) ---
    // 【核心修改】将所有更新函数的返回值包装成 { success: boolean, data: T } 的格式

    updateAfterSale: async (item: AfterSaleItem) => {
        try {
            const response = await apiClient.put<AfterSaleItem>(`/admin/after-sales/update`, item);
            return { success: true, data: response.data }; // 成功时，包装成礼盒返回
        } catch (error) {
            console.error('更新售后失败:', error);
            // 失败时，也返回一个礼盒，但 success 为 false，data 可以是原始数据或 null
            return { success: false, data: item };
        }
    },

    updateComplaint: async (item: ComplaintItem) => {
        try {
            const response = await apiClient.put<ComplaintItem>(`/admin/delivery-complaints/update`, item);
            return { success: true, data: response.data };
        } catch (error) {
            console.error('更新投诉失败:', error);
            return { success: false, data: item };
        }
    },

    updateViolation: async (item: ViolationItem) => {
        try {
            const response = await apiClient.put<ViolationItem>(`/admin/violation-penalties/update`, item);
            return { success: true, data: response.data };
        } catch (error) {
            console.error('更新违规失败:', error);
            return { success: false, data: item };
        }
    },

    updateReview: async (item: ReviewItem) => {
        try {
            const response = await apiClient.put<ReviewItem>(`/admin/review-comments/update`, item);
            return { success: true, data: response.data };
        } catch (error) {
            console.error('更新评论失败:', error);
            return { success: false, data: item };
        }
    },

    updateAdminInfo: async (data: AdminInfo) => {
        try {
            const response = await apiClient.put<AdminInfo>('/admin/info', data);
            return { success: true, data: response.data };
        } catch (error) {
            console.error('更新管理员信息失败:', error);
            return { success: false, data: data };
        }
    },
};

// 3.3 ----------------- API切换器 -----------------
const useMock = false; // 切换为 true 使用模拟API，false 使用真实API
const api = useMock ? mockApi : realApi;


// =================================================================
// 步骤 4: 组件状态和逻辑
// =================================================================

// 4.1 ----------------- 组件状态定义 -----------------
const activeMenu = ref('admin');

// 搜索和筛选的状态
const searchQuery = ref('');
const complaintSearchQuery = ref('');
const violationSearchQuery = ref('');
const reviewSearchQuery = ref('');
const selectedAfterSalesStatus = ref('all');
const selectedComplaintStatus = ref('all');
const selectedViolationStatus = ref('all');
const selectedReviewStatus = ref('all');

// 数据列表 (初始化为空数组，由API填充)
const afterSalesList = ref<AfterSaleItem[]>([]);
const complaintsList = ref<ComplaintItem[]>([]);
const violationsList = ref<ViolationItem[]>([]);
const reviewsList = ref<ReviewItem[]>([]);

// 弹窗和当前选中项的状态
const showAfterSaleDetail = ref(false);
const currentAfterSale = ref<AfterSaleItem | null>(null);
const afterSaleNote = ref('');
const showComplaintDetail = ref(false);
const currentComplaint = ref<ComplaintItem | null>(null);
const complaintNote = ref('');
const showViolationDetail = ref(false);
const currentViolation = ref<ViolationItem | null>(null);
const violationNote = ref('');
const showReviewDetail = ref(false);
const currentReview = ref<ReviewItem | null>(null);
const reviewImages = ref([
    'https://readdy.ai/api/search-image?query=delicious%20chinese%20food%20dish%20on%20white%20plate%20with%20garnish%20professional%20food%20photography%20with%20natural%20lighting%20high%20end%20restaurant%20presentation&width=200&height=200&seq=review-img-001&orientation=squarish',
    'https://readdy.ai/api/search-image?query=gourmet%20asian%20cuisine%20plated%20elegantly%20with%20side%20dishes%20professional%20food%20photography%20natural%20lighting%20restaurant%20quality%20presentation&width=200&height=200&seq=review-img-002&orientation=squarish',
    'https://readdy.ai/api/search-image?query=authentic%20chinese%20restaurant%20interior%20modern%20design%20with%20traditional%20elements%20professional%20photography%20warm%20lighting&width=200&height=200&seq=review-img-003&orientation=squarish'
]);
// 处罚措施选项的状态
const selectedPunishment = ref('');
const punishmentReason = ref('');
const selectedComplaintPunishment = ref('');
const complaintPunishmentReason = ref('');
const selectedMerchantPunishment = ref('');
const selectedStorePunishment = ref('');
const complaintFine = ref<number | undefined>(undefined);

const router = useRouter(); // 【新增】获取 router 实例

// 【新增】创建一个响应式变量来存储当前管理员的信息
const currentUser = ref<AdminInfo | null>(null);
// 【新增】创建一个变量来备份初始数据，用于“重置”功能
let originalAdminInfo: AdminInfo | null = null;
// 【新增】一个加载状态，提升用户体验
const isSaving = ref(false);


// 静态数据
const commonStatuses = [{ label: '全部', value: 'all' }, { label: '待处理', value: '待处理' }, { label: '已完成', value: '已完成' }];
const afterSalesStatuses = commonStatuses;
const complaintStatuses = commonStatuses;
const violationStatuses = [{ label: '全部', value: 'all' }, { label: '待处理', value: '待处理' }, { label: '执行中', value: '执行中' }, { label: '已完成', value: '已完成' }];
const reviewStatuses = commonStatuses;
const punishmentOptions = { afterSales: [{ label: '全额退款', value: 'full_refund' }, { label: '部分退款', value: 'partial_refund' }, { label: '重新配送', value: 'redelivery' }, { label: '商家道歉', value: 'apology' }, { label: '赔偿用户', value: 'compensation' }], complaints: [{ label: '警告处分', value: 'warning' }, { label: '暂停接单3天', value: 'suspend_3days' }, { label: '暂停接单7天', value: 'suspend_7days' }, { label: '罚款处理', value: 'fine' }, { label: '终止合作', value: 'terminate' }], violations: { merchant: [{ label: '口头警告', value: 'verbal_warning' }, { label: '书面警告', value: 'written_warning' }, { label: '罚款500元', value: 'fine_500' }, { label: '罚款1000元', value: 'fine_1000' }], store: [{ label: '限期整改', value: 'correction' }, { label: '暂停营业3天', value: 'suspend_3days' }, { label: '暂停营业7天', value: 'suspend_7days' }, { label: '永久下架', value: 'permanent_removal' }] }, reviews: [{ label: '通过审核', value: 'approve' }, { label: '删除评论', value: 'delete' }, { label: '禁止评论7天', value: 'ban_7days' }, { label: '禁止评论30天', value: 'ban_30days' }, { label: '永久禁言', value: 'permanent_ban' }] };

// 4.2 ----------------- 数据获取 -----------------
onMounted(async () => {
    console.log(`API 模式: ${useMock ? '模拟 (Mock)' : '真实 (Real)'}`);
    try {
        // 【修改】使用 Promise.all 并行加载所有数据，包括管理员信息
        const [
            adminInfo,
            afterSales,
            complaints,
            violations,
            reviews
        ] = await Promise.all([
            api.getAdminInfo(), // <--- 调用获取管理员信息的 API
            api.getAfterSalesList(),
            api.getComplaintsList(),
            api.getViolationsList(),
            api.getReviewsList(),
        ]);

        // 【新增】填充管理员信息数据模型
        currentUser.value = adminInfo;
        originalAdminInfo = JSON.parse(JSON.stringify(adminInfo)); // 深拷贝备份，用于重置

        // 填充其他列表数据
        afterSalesList.value = afterSales;
        complaintsList.value = complaints;
        violationsList.value = violations;
        reviewsList.value = reviews;
    } catch (error) {
        ElMessage.error('数据加载失败，请检查网络或联系管理员');
        console.error('数据加载失败:', error);
    }
});

// 4.3 ----------------- 计算属性和工具函数 (不变) -----------------
const filteredAfterSales = computed(() =>
    afterSalesList.value.filter(item =>
        (selectedAfterSalesStatus.value === 'all' || item.status === selectedAfterSalesStatus.value) &&
        ((item.applicationId?.toLowerCase() || '').includes((searchQuery.value?.toLowerCase() || '')) ||
            (item.orderId?.toLowerCase() || '').includes((searchQuery.value?.toLowerCase() || '')))
    )
);

const filteredComplaints = computed(() =>
    complaintsList.value.filter(item =>
        (selectedComplaintStatus.value === 'all' || item.status === selectedComplaintStatus.value) &&
        ((item.complaintId?.toLowerCase() || '').includes((complaintSearchQuery.value?.toLowerCase() || '')) ||
            (item.target?.toLowerCase() || '').includes((complaintSearchQuery.value?.toLowerCase() || '')))
    )
);

const filteredViolations = computed(() =>
    violationsList.value.filter(item =>
        (selectedViolationStatus.value === 'all' || item.status === selectedViolationStatus.value) &&
        ((item.punishmentId?.toLowerCase() || '').includes((violationSearchQuery.value?.toLowerCase() || '')) ||
            (item.storeName?.toLowerCase() || '').includes((violationSearchQuery.value?.toLowerCase() || '')))
    )
);

const filteredReviews = computed(() =>
    reviewsList.value.filter(item =>
        (selectedReviewStatus.value === 'all' || item.status === selectedReviewStatus.value) &&
        ((item.content?.toLowerCase() || '').includes((reviewSearchQuery.value?.toLowerCase() || '')) ||
            (item.username?.toLowerCase() || '').includes((reviewSearchQuery.value?.toLowerCase() || '')))
    )
);

const getBreadcrumb = () => ({ admin: '管理员信息', afterSales: '售后处理中心', complaints: '投诉处理中心', violations: '违规举报处理', reviews: '评论审核管理' })[activeMenu.value] || '控制台';
const getStatusClass = (status: string) => ({ '待处理': 'bg-yellow-100 text-yellow-800', '已完成': 'bg-green-100 text-green-800', '执行中': 'bg-blue-100 text-blue-800' })[status] || 'bg-gray-100 text-gray-800';

const openAfterSaleDetail = (item: AfterSaleItem) => {
    currentAfterSale.value = { ...item };
    
    if (item.status === '待处理') {
        // 待处理：清空输入框，准备填写
        afterSaleNote.value = '';
        selectedPunishment.value = '';
        punishmentReason.value = '';
    } else {
        // 已完成：填充已保存的数据，供查看
        afterSaleNote.value = item.processingNote || '';
        selectedPunishment.value = item.punishment || '';
        punishmentReason.value = item.punishmentReason || '';
    }
    
    showAfterSaleDetail.value = true;
};

const openComplaintDetail = (item: ComplaintItem) => {
    currentComplaint.value = { ...item };
    
    if (item.status === '待处理') {
        // 待处理：清空输入框，准备填写
        complaintNote.value = '';
        selectedComplaintPunishment.value = '';
        complaintPunishmentReason.value = '';
        complaintFine.value = 0.00; 
    } else {
        // 已完成：填充已保存的数据，供查看
        complaintNote.value = item.processingNote || '';
        selectedComplaintPunishment.value = item.punishment || '';
        complaintPunishmentReason.value = item.punishmentReason || '';
        complaintFine.value = item.fine ? Number(item.fine) : 0.00;
    }
    
    showComplaintDetail.value = true;
};

const openViolationDetail = (item: ViolationItem) => {
    currentViolation.value = { ...item };
    
    if (item.status === '待处理') {
        // 待处理：清空输入框，准备填写
        selectedMerchantPunishment.value = '';
        selectedStorePunishment.value = '';
        violationNote.value = '';
        selectedMerchantPunishment.value = '';
        selectedStorePunishment.value = '';
    } else {
        // 已完成：填充已保存的数据，供查看
        console.log('原始数据:', item);
        selectedMerchantPunishment.value = item.merchantPunishment === '-' ? '' : item.merchantPunishment;
        selectedStorePunishment.value = item.storePunishment === '-' ? '' : item.storePunishment;
        violationNote.value = item.processingNote || '';
        selectedMerchantPunishment.value = item.merchantPunishment === '-' ? '' : item.merchantPunishment;
        selectedStorePunishment.value = item.storePunishment === '-' ? '' : item.storePunishment;
    }
    
    showViolationDetail.value = true;
};


const openReviewDetail = (item: ReviewItem) => { currentReview.value = { ...item }; showReviewDetail.value = true; };

// 下拉框相关的数据和方法
const dropdownVisible = ref(false)
const managementOptions = ['售后处理', '配送投诉', '商家举报', '评论审核']

const toggleDropdown = () => {
  dropdownVisible.value = !dropdownVisible.value
}

const getSelectedText = () => {
  if (!currentUser.value?.managementScope) return '请选择管理对象'
  
  // 将用户的管理范围按分隔符拆分
  const userSelections = currentUser.value.managementScope.split('、')
  
  // 只保留在当前选项列表中的项目
  const validSelections = userSelections.filter(selection => 
    managementOptions.includes(selection)
  )
  
  // 如果没有有效选项，显示默认文本
  if (validSelections.length === 0) return '请选择管理对象'
  
  // 返回有效的选项，用顿号连接
  return validSelections.join('、')
}

const isSelected = (option: string) => {
  if (!currentUser.value?.managementScope) return false
  return currentUser.value.managementScope.split('、').includes(option)
}

const toggleOption = (option: string) => {
  if (!currentUser.value) return
  
  let selected = currentUser.value.managementScope ? currentUser.value.managementScope.split('、') : []
  const index = selected.indexOf(option)
  
  if (index > -1) {
    selected.splice(index, 1)
  } else {
    selected.push(option)
  }
  
  currentUser.value.managementScope = selected.join('、')
}

// 4.4 ----------------- 修改数据处理函数 (全部完成) -----------------

const handleAfterSaleAction = async () => {
    if (!afterSaleNote.value.trim() || !selectedPunishment.value || !punishmentReason.value.trim()) {
        return ElMessage.warning('请填写完整的处理信息和处罚原因');
    }
    if (!currentAfterSale.value) return;

    try {
        await ElMessageBox.confirm('确定要执行选定的处罚措施吗？', '确认操作', { type: 'warning' });

        const punishmentLabel = punishmentOptions.afterSales.find(o => o.value === selectedPunishment.value)?.label || selectedPunishment.value;
        const updatedItem: AfterSaleItem = {
            ...currentAfterSale.value,
            status: '已完成',
            punishment: punishmentLabel,
            punishmentReason: punishmentReason.value,
            processingNote: afterSaleNote.value
        };

        // 【核心修改】接收API的返回结果
        const response = await api.updateAfterSale(updatedItem);

        // 【核心修改】检查返回结果的 success 字段
        if (response.success) {
            // 只有在后端确认成功后，才更新前端的UI
            const index = afterSalesList.value.findIndex(item => item.applicationId === updatedItem.applicationId);
            if (index !== -1) {
                // 使用从后端返回的最新数据来更新列表，这是最佳实践
                afterSalesList.value[index] = { ...updatedItem, ...response.data };
            }
            ElMessage.success('处理完成，处罚措施已执行');
            showAfterSaleDetail.value = false;
        } else {
            // 如果后端返回失败，则提示用户
            ElMessage.error('操作失败，数据未能成功保存到服务器');
        }

    } catch (error) {
        // 这个 catch 现在主要捕获 ElMessageBox 的 "cancel" 行为
        if (error !== 'cancel') {
            console.error("处理售后时发生未知错误:", error);
            ElMessage.error('操作失败');
        } else {
            ElMessage.info('操作已取消');
        }
    }
};


/**
 * 【新增】处理保存修改的函数
 */
/**
 * 【新增】处理保存修改的函数
 */
const handleSaveChanges = async () => {
    if (!currentUser.value) return;

    isSaving.value = true;
    try {
        const response = await api.updateAdminInfo(currentUser.value);

        if (response.success) {
            // 【核心修改】使用 Object.assign 来更新现有响应式对象的属性
            // 这样做可以更稳定地触发视图更新
            Object.assign(currentUser.value, response.data);

            originalAdminInfo = JSON.parse(JSON.stringify(response.data));

            ElMessage.success('信息更新成功！');
            console.log('管理员信息已更新:', response.data);
        } else {
            ElMessage.error('信息更新失败，未能成功保存到服务器');
            console.error('信息更新失败:', response);
        }

    } catch (error) {
        console.error('更新失败:', error);
        ElMessage.error('信息更新失败，请稍后再试。');
    } finally {
        isSaving.value = false;
    }
};

/**
 * 【新增】重置表单的函数
 */
const resetForm = () => {
    if (originalAdminInfo) {
        currentUser.value = JSON.parse(JSON.stringify(originalAdminInfo)); // 从备份恢复
        ElMessage.info('表单已重置');
    }
};

/**
 * 【新增】处理登出的函数
 */
const handleLogout = () => {
    // 弹出确认框，防止误触
    ElMessageBox.confirm('您确定要退出登录吗？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
    }).then(() => {
        // 1. 清除本地存储的所有认证信息
        localStorage.removeItem('authToken');
        localStorage.removeItem('userInfo'); // 如果你存了用户信息，也要一并清除

        // 2. 显示成功提示
        ElMessage.success('您已成功退出登录');

        // 3. 跳转回登录页面
        router.push('/login'); // 假设你的登录页路由是 '/login'
    }).catch(() => {
        // 用户点击了取消，不做任何事
        ElMessage.info('已取消退出');
    });
};


const handleComplaintProcess = async () => {
    if (!complaintNote.value.trim() || !selectedComplaintPunishment.value || !complaintPunishmentReason.value.trim()) {
        return ElMessage.warning('请填写完整的处理信息和处罚原因');
    }
    if (!currentComplaint.value) return;

    try {
        await ElMessageBox.confirm('确定要处理该投诉吗？', '确认操作', { type: 'warning' });

        const punishmentLabel = punishmentOptions.complaints.find(o => o.value === selectedComplaintPunishment.value)?.label || selectedComplaintPunishment.value;
        const updatedItem: ComplaintItem = {
            ...currentComplaint.value,
            status: '已完成',
            punishment: punishmentLabel,
            punishmentReason: complaintPunishmentReason.value,
            processingNote: complaintNote.value,
            fine: complaintFine.value !== undefined ? String(complaintFine.value) : ''
        };

        // 【核心修改】接收API的返回结果
        const response = await api.updateComplaint(updatedItem);

        // 【核心修改】检查返回结果的 success 字段
        if (response.success) {
            // 只有在后端确认成功后，才更新前端的UI
            const index = complaintsList.value.findIndex(item => item.complaintId === updatedItem.complaintId);
            if (index !== -1) {
                // 使用从后端返回的最新数据来更新列表
                complaintsList.value[index] = { ...updatedItem, ...response.data };
            }
            ElMessage.success('投诉已处理完成');
            showComplaintDetail.value = false;
        } else {
            // 如果后端返回失败，则提示用户
            ElMessage.error('操作失败，数据未能成功保存到服务器');
        }

    } catch (error) {
        // 这个 catch 主要捕获 ElMessageBox 的 "cancel" 行为
        if (error !== 'cancel') {
            console.error("处理投诉时发生未知错误:", error);
            ElMessage.error('操作失败');
        } else {
            ElMessage.info('操作已取消');
        }
    }
};

const handleViolationAction = async (action: 'process' | 'complete') => {
    if (!currentViolation.value) return;
    if (action === 'process' && (!selectedMerchantPunishment.value || !selectedStorePunishment.value)) {
        return ElMessage.warning('请选择商家和店铺的处罚措施');
    }
    if (!violationNote.value.trim()) {
        return ElMessage.warning('请填写处理备注');
    }

    try {
        await ElMessageBox.confirm(`确定要${action === 'process' ? '开始执行' : '完成执行'}该处罚吗？`, '确认操作', { type: 'warning' });

        let updatedItem: ViolationItem;
        if (action === 'process') {
            updatedItem = {
                ...currentViolation.value,
                status: '执行中',
                merchantPunishment: selectedMerchantPunishment.value,
                storePunishment: selectedStorePunishment.value,
                processingNote: violationNote.value
            };
        } else { // action === 'complete'
            updatedItem = {
                ...currentViolation.value,
                status: '已完成',
                processingNote: violationNote.value
            };
        }

        // 【核心修改】接收API的返回结果
        const response = await api.updateViolation(updatedItem);

        // 【核心修改】检查返回结果的 success 字段
        if (response.success) {
            // 只有在后端确认成功后，才更新前端的UI
            const index = violationsList.value.findIndex(item => item.punishmentId === updatedItem.punishmentId);
            if (index !== -1) {
                // 使用从后端返回的最新数据来更新列表
                violationsList.value[index] = { ...updatedItem, ...response.data };
                console.log('更新后的数据:', violationsList.value[index]);
            }
            ElMessage.success(`处罚已${action === 'process' ? '开始执行' : '执行完成'}`);
            showViolationDetail.value = false;
        } else {
            // 如果后端返回失败，则提示用户
            ElMessage.error('操作失败，数据未能成功保存到服务器');
        }

    } catch (error) {
        if (error !== 'cancel') {
            console.error("处理违规时发生未知错误:", error);
            ElMessage.error('操作失败');
        } else {
            ElMessage.info('操作已取消');
        }
    }
};

const processReview = async (decision: 'approve' | 'reject') => {
    if (!currentReview.value) return;

    // 根据决定设置提示文字和最终的处罚说明
    const newStatus = decision === 'approve' ? '已完成' : '违规';
    const actionText = decision === 'approve' ? '审核通过' : '判定违规'

    try {
        // 弹出确认框
        await ElMessageBox.confirm(`确定要将此评论标记为“${actionText}”吗？`, '确认操作', { type: 'warning' });

        // 准备要发送到后端的数据
        const updatedItem: ReviewItem = {
            ...currentReview.value,
            status: newStatus
        };

        // 调用API
        const response = await api.updateReview(updatedItem);

        // 处理API返回结果
        if (response.success) {
            const index = reviewsList.value.findIndex(item => item.reviewId === updatedItem.reviewId);
            if (index !== -1) {
                reviewsList.value[index] = { ...updatedItem, ...response.data };
            }
            ElMessage.success(`操作成功，评论已${actionText}`);
            showReviewDetail.value = false; // 关闭弹窗
        } else {
            ElMessage.error('操作失败，数据未能成功保存到服务器');
        }

    } catch (error) {
        if (error !== 'cancel') {
            console.error(`处理评论(${actionText})时发生未知错误:`, error);
            ElMessage.error('操作失败');
        } else {
            ElMessage.info('操作已取消');
        }
    }
};

</script>
<style scoped>
.\!rounded-button {
    border-radius: 0.5rem;
}

/* 隐藏数字输入框的箭头 */
input[type="number"]::-webkit-outer-spin-button,
input[type="number"]::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
}

input[type="number"] {
    -moz-appearance: textfield;
}

/* 滚动条样式 */
::-webkit-scrollbar {
    width: 6px;
    height: 6px;
}

::-webkit-scrollbar-track {
    background: #f1f1f1;
    border-radius: 3px;
}

::-webkit-scrollbar-thumb {
    background: #c1c1c1;
    border-radius: 3px;
}

::-webkit-scrollbar-thumb:hover {
    background: #a8a8a8;
}
</style>