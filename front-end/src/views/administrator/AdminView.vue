<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->
<template>
    <div class="min-h-screen bg-gray-50">
        <!-- 顶部导航栏 -->
        <header class="fixed top-0 left-0 right-0 bg-white border-b border-gray-200 z-50">
            <div class="flex items-center justify-between px-6 h-16">
                <div class="flex items-center space-x-4">
                    <div class="flex items-center space-x-3">
                        <div class="w-8 h-8 bg-orange-500 rounded-lg flex items-center justify-center">
                            <i class="fas fa-utensils text-white text-sm"></i>
                        </div>
                        <h1 class="text-xl font-semibold text-gray-800">外卖管理系统</h1>
                    </div>
                </div>
                <div class="flex items-center space-x-4">
                    <div class="flex items-center space-x-3">
                        <img src="https://readdy.ai/api/search-image?query=professional%20business%20administrator%20avatar%20portrait%20with%20clean%20white%20background%20modern%20corporate%20headshot%20style%20high%20quality%20detailed%20facial%20features&width=40&height=40&seq=admin-avatar-001&orientation=squarish"
                            alt="管理员头像" class="w-8 h-8 rounded-full object-cover">
                        <span class="text-sm text-gray-700">张管理员</span>
                    </div>
                    <button class="text-gray-500 hover:text-gray-700 cursor-pointer">
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
                                    <tr v-for="item in filteredAfterSales" :key="item.applicationId" class="hover:bg-gray-50">
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
                                    <tr v-for="item in filteredComplaints" :key="item.complaintId" class="hover:bg-gray-50">
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
                                    <tr v-for="item in filteredViolations" :key="item.punishmentId" class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.punishmentId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.storeName
                                            }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{ item.reason }}
                                        </td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{
                                            item.merchantPunishment }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{
                                            item.storePunishment }}</td>
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
                                            店铺名称</th>
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
                                    <tr v-for="item in filteredReviews" :key="item.reviewId" class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.reviewId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.username
                                            }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.storeName
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
                                        <td class="px-6 py-4 text-sm text-gray-900">{{ item.punishment || '-' }}</td>
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
                <!-- 管理员信息页面 -->
                <div v-if="activeMenu === 'admin'">
                    <div class="bg-white rounded-xl shadow-sm border border-gray-100">
                        <div class="p-6 border-b border-gray-100">
                            <h2 class="text-lg font-semibold text-gray-900">管理员信息管理</h2>
                        </div>
                        <div class="p-6">
                            <div class="max-w-2xl">
                                <div class="flex items-center space-x-6 mb-8">
                                    <img src="https://readdy.ai/api/search-image?query=professional%20business%20administrator%20avatar%20portrait%20with%20clean%20white%20background%20modern%20corporate%20headshot%20style%20high%20quality%20detailed%20facial%20features%20confident%20expression&width=120&height=120&seq=admin-profile-001&orientation=squarish"
                                        alt="管理员头像"
                                        class="w-24 h-24 rounded-full object-cover border-4 border-gray-100">
                                    <div>
                                        <h3 class="text-xl font-semibold text-gray-900">张管理员</h3>
                                        <p class="text-gray-600">系统管理员</p>
                                        <p class="text-sm text-gray-500">注册时间：2023-01-15</p>
                                    </div>
                                </div>
                                <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">管理员ID</label>
                                        <input type="text" value="ADM001" readonly
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">用户名</label>
                                        <input type="text" value="admin_zhang"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">手机号</label>
                                        <input type="text" value="13800138000"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">电子邮箱</label>
                                        <input type="email" value="admin@fooddelivery.com"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">性别</label>
                                        <div class="flex space-x-4">
                                            <label class="flex items-center">
                                                <input type="radio" name="gender" value="男" checked
                                                    class="text-orange-500 focus:ring-orange-500">
                                                <span class="ml-2 text-sm text-gray-700">男</span>
                                            </label>
                                            <label class="flex items-center">
                                                <input type="radio" name="gender" value="女"
                                                    class="text-orange-500 focus:ring-orange-500">
                                                <span class="ml-2 text-sm text-gray-700">女</span>
                                            </label>
                                        </div>
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">姓名</label>
                                        <input type="text" value="张伟"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">生日</label>
                                        <input type="date" value="1990-05-15"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">管理对象</label>
                                        <input type="text" value="售后处理、投诉管理、评论审核"
                                            class="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-2">处理事项获评均分</label>
                                        <div class="flex items-center space-x-2">
                                            <input type="text" value="4.8" readonly
                                                class="w-20 px-3 py-2 border border-gray-300 rounded-lg bg-gray-50 text-sm">
                                            <div class="flex text-yellow-400">
                                                <i class="fas fa-star text-sm"></i>
                                                <i class="fas fa-star text-sm"></i>
                                                <i class="fas fa-star text-sm"></i>
                                                <i class="fas fa-star text-sm"></i>
                                                <i class="fas fa-star text-sm"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <label class="flex items-center">
                                            <input type="checkbox" checked
                                                class="text-orange-500 focus:ring-orange-500">
                                            <span class="ml-2 text-sm text-gray-700">公开个人信息</span>
                                        </label>
                                    </div>
                                </div>
                                <div class="mt-8 flex space-x-4">
                                    <button
                                        class="px-6 py-2 bg-orange-500 text-white rounded-lg hover:bg-orange-600 transition-colors cursor-pointer !rounded-button whitespace-nowrap">保存修改</button>
                                    <button
                                        class="px-6 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50 transition-colors cursor-pointer !rounded-button whitespace-nowrap">重置</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>
        </div>
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
                            <h3 class="text-lg font-semibold text-gray-900">售后申请编号：{{ currentAfterSale.applicationId }}
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
                <el-input v-model="afterSaleNote" type="textarea" :rows="3" placeholder="请输入处理备注" maxlength="500"
                    show-word-limit />
            </div>
            <div class="mb-6">
                <h4 class="text-sm font-medium text-gray-700 mb-2">处罚措施</h4>
                <el-select v-model="selectedPunishment" class="w-full mb-4" placeholder="请选择处罚措施">
                    <el-option v-for="option in punishmentOptions.afterSales" :key="option.value" :label="option.label"
                        :value="option.value" />
                </el-select>
                <h4 class="text-sm font-medium text-gray-700 mb-2">处罚原因</h4>
                <el-input v-model="punishmentReason" type="textarea" :rows="3" placeholder="请输入处罚原因" maxlength="500"
                    show-word-limit />
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
                            <h3 class="text-lg font-semibold text-gray-900">投诉编号：{{ currentComplaint.complaintId }}</h3>
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
                <el-select v-model="selectedComplaintPunishment" class="w-full mb-4" placeholder="请选择处罚措施">
                    <el-option v-for="option in punishmentOptions.complaints" :key="option.value" :label="option.label"
                        :value="option.value" />
                </el-select>
                <h4 class="text-sm font-medium text-gray-700 mb-2">处罚原因</h4>
                <el-input v-model="complaintPunishmentReason" type="textarea" :rows="3" placeholder="请输入处罚原因"
                    maxlength="500" show-word-limit class="mb-4" />
                <h4 class="text-sm font-medium text-gray-700 mb-2">处理备注</h4>
                <el-input v-model="complaintNote" type="textarea" :rows="3" placeholder="请输入处理备注" maxlength="500"
                    show-word-limit />
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
                            <h3 class="text-lg font-semibold text-gray-900">{{ currentViolation.storeName }}</h3>
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
                    <el-select v-if="currentViolation?.status === '待处理'" v-model="selectedMerchantPunishment"
                        class="w-full" placeholder="请选择商家处罚措施">
                        <el-option v-for="option in punishmentOptions.violations.merchant" :key="option.value"
                            :label="option.label" :value="option.value" />
                    </el-select>
                    <div v-else class="p-4 bg-gray-50 rounded-lg">
                        <p class="text-gray-900">{{ currentViolation.merchantPunishment }}</p>
                    </div>
                </div>
                <div>
                    <h4 class="text-sm font-medium text-gray-700 mb-2">店铺处罚措施</h4>
                    <el-select v-if="currentViolation?.status === '待处理'" v-model="selectedStorePunishment"
                        class="w-full" placeholder="请选择店铺处罚措施">
                        <el-option v-for="option in punishmentOptions.violations.store" :key="option.value"
                            :label="option.label" :value="option.value" />
                    </el-select>
                    <div v-else class="p-4 bg-gray-50 rounded-lg">
                        <p class="text-gray-900">{{ currentViolation.storePunishment }}</p>
                    </div>
                </div>
            </div>
            <div class="mb-6">
                <h4 class="text-sm font-medium text-gray-700 mb-2">处理备注</h4>
                <el-input v-model="violationNote" type="textarea" :rows="3" placeholder="请输入处理备注" maxlength="500"
                    show-word-limit />
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
            <div class="border-b border-gray-200 pb-6 mb-6">
                <div class="flex items-start justify-between">
                    <div class="flex items-center space-x-4">
                        <div class="w-12 h-12 bg-gray-100 rounded-full flex items-center justify-center">
                            <i class="fas fa-user text-gray-400"></i>
                        </div>
                        <div>
                            <h3 class="text-lg font-semibold text-gray-900">{{ currentReview.username }}</h3>
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
            <div class="grid grid-cols-2 gap-6 mb-6">
                <div>
                    <h4 class="text-sm font-medium text-gray-700 mb-2">店铺信息</h4>
                    <div class="p-4 bg-gray-50 rounded-lg">
                        <p class="text-gray-900 font-medium">{{ currentReview.storeName }}</p>
                        <p class="text-sm text-gray-600 mt-1">订单编号：{{ currentReview.orderId || 'ORD' +
                            currentReview.reviewId }}</p>
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
            <div class="mb-6">
                <h4 class="text-sm font-medium text-gray-700 mb-2">处理方式</h4>
                <el-select v-model="selectedReviewPunishment" class="w-full mb-4" placeholder="请选择处理方式">
                    <el-option v-for="option in punishmentOptions.reviews" :key="option.value" :label="option.label"
                        :value="option.value" />
                </el-select>
                <h4 class="text-sm font-medium text-gray-700 mb-2">处理原因</h4>
                <el-input v-model="reviewPunishmentReason" type="textarea" :rows="3" placeholder="请输入处理原因"
                    maxlength="500" show-word-limit class="mb-4" />
                <h4 class="text-sm font-medium text-gray-700 mb-2">审核备注</h4>
                <el-input v-model="reviewNote" type="textarea" :rows="3" placeholder="请输入审核备注" maxlength="500"
                    show-word-limit />
            </div>
            <div class="flex justify-end space-x-4">
                <el-button @click="showReviewDetail = false">取消</el-button>
                <el-button v-if="currentReview.status === '待处理'" type="primary"
                    @click="handleReviewAction">执行处理</el-button>
            </div>
        </div>
    </el-dialog>
</template>
<script lang="ts" setup>
// =================================================================
// 步骤 1: 导入必要的模块
// =================================================================
import { ref, computed, onMounted } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import axios from 'axios'; // 导入axios用于真实API请求

// =================================================================
// 步骤 2: 定义数据类型
// =================================================================
interface AdminInfo {
  id: string; // e.g., "ADM001"
  username: string;
  name: string; // The display name, e.g., "张管理员"
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
  storeName: string;
  content: string;
  rating: number;
  submitTime: string;
  status: '待处理' | '已完成';
  punishment: string;
  punishmentReason?: string;
  processingNote?: string;
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
    { complaintId: 'CP202401001', target: '骑手张三', content: '配送员态度恶劣，服务质量差', applicationTime: '2024-01-15 14:30', status: '待处理', punishment: '-' },
    { complaintId: 'CP202401002', target: '商家李记餐厅', content: '商家出餐速度慢，影响配送时效', applicationTime: '2024-01-15 13:45', status: '待处理', punishment: '-' },
    { complaintId: 'CP202401003', target: '骑手王五', content: '配送员未按时送达，且态度不好', applicationTime: '2024-01-15 12:20', status: '已完成', punishment: '暂停接单 3 天' }
  ]),
  getViolationsList: async (): Promise<ViolationItem[]> => ([
    { punishmentId: 'PUN202401001', storeName: '张记小炒', reason: '食品安全问题，使用过期食材制作食品', merchantPunishment: '-', storePunishment: '-', punishmentTime: '2024-01-15 14:30', status: '待处理' },
    { punishmentId: 'PUN202401002', storeName: '美味餐厅', reason: '虚假宣传，图片与实物不符', merchantPunishment: '罚款500元', storePunishment: '下架违规商品', punishmentTime: '2024-01-15 13:45', status: '已完成' },
    { punishmentId: 'PUN202401003', storeName: '快送外卖', reason: '配送员私自拆开包装', merchantPunishment: '-', storePunishment: '-', punishmentTime: '2024-01-15 12:20', status: '待处理' }
  ]),
  getReviewsList: async (): Promise<ReviewItem[]> => ([
    { reviewId: 'RV202401001', username: '用户张三', storeName: '美味餐厅', content: '味道不错，配送也很快，推荐！', rating: 5, submitTime: '2024-01-15 14:30', status: '待处理', punishment: '-' },
    { reviewId: 'RV202401002', username: '用户李四', storeName: '快餐店', content: '包装很好，食物新鲜，服务态度也不错', rating: 4, submitTime: '2024-01-15 13:45', status: '已完成', punishment: '评论已通过' },
    { reviewId: 'RV202401003', username: '用户王五', storeName: '小吃摊', content: '这家店的食物质量有问题，不建议购买', rating: 1, submitTime: '2024-01-15 12:20', status: '已完成', punishment: '评论已拒绝' }
  ]),
    getAdminInfo: async (): Promise<AdminInfo> => ({
    id: 'ADM001',
    username: 'admin_zhang',
    name: '张管理员',
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
  updateAdminInfo: async (data: AdminInfo) => {console.log('【Mock API】更新管理员信息:', data);return { success: true, data };},
  updateAfterSale: async (item: AfterSaleItem) => { console.log('【Mock API】更新售后:', item); return { success: true, data: item }; },
  updateComplaint: async (item: ComplaintItem) => { console.log('【Mock API】更新投诉:', item); return { success: true, data: item }; },
  updateViolation: async (item: ViolationItem) => { console.log('【Mock API】更新违规:', item); return { success: true, data: item }; },
  updateReview: async (item: ReviewItem) => { console.log('【Mock API】更新评论:', item); return { success: true, data: item }; },
};

// 3.2 ----------------- 真实API实现 -----------------
const apiClient = axios.create({ baseURL: '/api/v1', timeout: 5000 }); // 根据你的后端地址修改 baseURL

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






const realApi = {
    getAfterSalesList: () => apiClient.get<AfterSaleItem[]>('/after-sales').then(res => res.data),
    getComplaintsList: () => apiClient.get<ComplaintItem[]>('/complaints').then(res => res.data),
    getViolationsList: () => apiClient.get<ViolationItem[]>('/violations').then(res => res.data),
    getReviewsList: () => apiClient.get<ReviewItem[]>('/reviews').then(res => res.data),
    getAdminInfo: () => apiClient.get<AdminInfo>('/admin/info').then(res => res.data),
    updateAfterSale: (item: AfterSaleItem) => apiClient.put(`/after-sales/${item.applicationId}`, item).then(res => res.data),
    updateComplaint: (item: ComplaintItem) => apiClient.put(`/complaints/${item.complaintId}`, item).then(res => res.data),
    updateViolation: (item: ViolationItem) => apiClient.put(`/violations/${item.punishmentId}`, item).then(res => res.data),
    updateReview: (item: ReviewItem) => apiClient.put(`/reviews/${item.reviewId}`, item).then(res => res.data),
    updateAdminInfo: (data: AdminInfo) => apiClient.put<AdminInfo>('/admin/info', data).then(res => res.data),
};

// 3.3 ----------------- API切换器 -----------------
const useMock = true; // 切换为 true 使用模拟API，false 使用真实API
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
const reviewNote = ref('');
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
const selectedReviewPunishment = ref('');
const reviewPunishmentReason = ref('');

// 静态数据
const commonStatuses = [ { label: '全部', value: 'all' }, { label: '待处理', value: '待处理' }, { label: '已完成', value: '已完成' } ];
const afterSalesStatuses = commonStatuses;
const complaintStatuses = commonStatuses;
const violationStatuses = [ { label: '全部', value: 'all' }, { label: '待处理', value: '待处理' }, { label: '执行中', value: '执行中' }, { label: '已完成', value: '已完成' } ];
const reviewStatuses = commonStatuses;
const punishmentOptions = { afterSales: [ { label: '全额退款', value: 'full_refund' }, { label: '部分退款', value: 'partial_refund' }, { label: '重新配送', value: 'redelivery' }, { label: '商家道歉', value: 'apology' }, { label: '赔偿用户', value: 'compensation' } ], complaints: [ { label: '警告处分', value: 'warning' }, { label: '暂停接单3天', value: 'suspend_3days' }, { label: '暂停接单7天', value: 'suspend_7days' }, { label: '罚款处理', value: 'fine' }, { label: '终止合作', value: 'terminate' } ], violations: { merchant: [ { label: '口头警告', value: 'verbal_warning' }, { label: '书面警告', value: 'written_warning' }, { label: '罚款500元', value: 'fine_500' }, { label: '罚款1000元', value: 'fine_1000' } ], store: [ { label: '限期整改', value: 'correction' }, { label: '暂停营业3天', value: 'suspend_3days' }, { label: '暂停营业7天', value: 'suspend_7days' }, { label: '永久下架', value: 'permanent_removal' } ] }, reviews: [ { label: '通过审核', value: 'approve' }, { label: '删除评论', value: 'delete' }, { label: '禁止评论7天', value: 'ban_7days' }, { label: '禁止评论30天', value: 'ban_30days' }, { label: '永久禁言', value: 'permanent_ban' } ] };

// 4.2 ----------------- 数据获取 -----------------
onMounted(async () => {
    console.log(`API 模式: ${useMock ? '模拟 (Mock)' : '真实 (Real)'}`);
    try {
        const [afterSales, complaints, violations, reviews] = await Promise.all([
            api.getAfterSalesList(),
            api.getComplaintsList(),
            api.getViolationsList(),
            api.getReviewsList(),
        ]);
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
const filteredAfterSales = computed(() => afterSalesList.value.filter(item => (selectedAfterSalesStatus.value === 'all' || item.status === selectedAfterSalesStatus.value) && (item.applicationId.toLowerCase().includes(searchQuery.value.toLowerCase()) || item.orderId.toLowerCase().includes(searchQuery.value.toLowerCase()))));
const filteredComplaints = computed(() => complaintsList.value.filter(item => (selectedComplaintStatus.value === 'all' || item.status === selectedComplaintStatus.value) && (item.complaintId.toLowerCase().includes(complaintSearchQuery.value.toLowerCase()) || item.target.toLowerCase().includes(complaintSearchQuery.value.toLowerCase()))));
const filteredViolations = computed(() => violationsList.value.filter(item => (selectedViolationStatus.value === 'all' || item.status === selectedViolationStatus.value) && (item.punishmentId.toLowerCase().includes(violationSearchQuery.value.toLowerCase()) || item.storeName.toLowerCase().includes(violationSearchQuery.value.toLowerCase()))));
const filteredReviews = computed(() => reviewsList.value.filter(item => (selectedReviewStatus.value === 'all' || item.status === selectedReviewStatus.value) && (item.content.toLowerCase().includes(reviewSearchQuery.value.toLowerCase()) || item.username.toLowerCase().includes(reviewSearchQuery.value.toLowerCase()))));

const getBreadcrumb = () => ({ admin: '管理员信息', afterSales: '售后处理中心', complaints: '投诉处理中心', violations: '违规举报处理', reviews: '评论审核管理' })[activeMenu.value] || '控制台';
const getStatusClass = (status: string) => ({ '待处理': 'bg-yellow-100 text-yellow-800', '已完成': 'bg-green-100 text-green-800', '执行中': 'bg-blue-100 text-blue-800' })[status] || 'bg-gray-100 text-gray-800';

const openAfterSaleDetail = (item: AfterSaleItem) => { currentAfterSale.value = { ...item }; afterSaleNote.value = ''; selectedPunishment.value = ''; punishmentReason.value = ''; showAfterSaleDetail.value = true; };
const openComplaintDetail = (item: ComplaintItem) => { currentComplaint.value = { ...item }; complaintNote.value = ''; selectedComplaintPunishment.value = ''; complaintPunishmentReason.value = ''; showComplaintDetail.value = true; };
const openViolationDetail = (item: ViolationItem) => { currentViolation.value = { ...item }; violationNote.value = ''; selectedMerchantPunishment.value = item.merchantPunishment === '-' ? '' : item.merchantPunishment; selectedStorePunishment.value = item.storePunishment === '-' ? '' : item.storePunishment; showViolationDetail.value = true; };
const openReviewDetail = (item: ReviewItem) => { currentReview.value = { ...item }; reviewNote.value = ''; selectedReviewPunishment.value = ''; reviewPunishmentReason.value = ''; showReviewDetail.value = true; };

// 4.4 ----------------- 修改数据处理函数 (全部完成) -----------------
const handleAfterSaleAction = async () => {
    if (!afterSaleNote.value.trim() || !selectedPunishment.value || !punishmentReason.value.trim()) return ElMessage.warning('请填写完整的处理信息和处罚原因');
    if (!currentAfterSale.value) return;
    try {
        await ElMessageBox.confirm('确定要执行选定的处罚措施吗？', '确认操作', { type: 'warning' });
        const punishmentLabel = punishmentOptions.afterSales.find(o => o.value === selectedPunishment.value)?.label || selectedPunishment.value;
        const updatedItem: AfterSaleItem = { ...currentAfterSale.value, status: '已完成', punishment: punishmentLabel, punishmentReason: punishmentReason.value, processingNote: afterSaleNote.value };
        await api.updateAfterSale(updatedItem);
        const index = afterSalesList.value.findIndex(item => item.applicationId === updatedItem.applicationId);

        if (index !== -1) afterSalesList.value[index] = updatedItem;
        ElMessage.success('处理完成，处罚措施已执行');
        showAfterSaleDetail.value = false;
    } catch (error) {
        if (error !== 'cancel') ElMessage.error('操作失败');
    }
};

const handleComplaintProcess = async () => {
    if (!complaintNote.value.trim() || !selectedComplaintPunishment.value || !complaintPunishmentReason.value.trim()) return ElMessage.warning('请填写完整的处理信息和处罚原因');
    if (!currentComplaint.value) return;
    try {
        await ElMessageBox.confirm('确定要处理该投诉吗？', '确认操作', { type: 'warning' });
        const punishmentLabel = punishmentOptions.complaints.find(o => o.value === selectedComplaintPunishment.value)?.label || selectedComplaintPunishment.value;
        const updatedItem: ComplaintItem = { ...currentComplaint.value, status: '已完成', punishment: punishmentLabel, punishmentReason: complaintPunishmentReason.value, processingNote: complaintNote.value };
        await api.updateComplaint(updatedItem);

        const index = complaintsList.value.findIndex(item => item.complaintId === updatedItem.complaintId);

        if (index !== -1) complaintsList.value[index] = updatedItem;
        ElMessage.success('投诉已处理完成');
        showComplaintDetail.value = false;
    } catch (error) {
        if (error !== 'cancel') ElMessage.error('操作失败');
    }
};

const handleViolationAction = async (action: 'process' | 'complete') => {
    if (!currentViolation.value) return;
    if (action === 'process' && (!selectedMerchantPunishment.value || !selectedStorePunishment.value)) return ElMessage.warning('请选择商家和店铺的处罚措施');
    if (!violationNote.value.trim()) return ElMessage.warning('请填写处理备注');
    try {
        await ElMessageBox.confirm(`确定要${action === 'process' ? '开始执行' : '完成执行'}该处罚吗？`, '确认操作', { type: 'warning' });
        let updatedItem: ViolationItem;
        if (action === 'process') {
            updatedItem = { ...currentViolation.value, status: '执行中', merchantPunishment: selectedMerchantPunishment.value, storePunishment: selectedStorePunishment.value, processingNote: violationNote.value };
        } else {
            updatedItem = { ...currentViolation.value, status: '已完成', processingNote: violationNote.value };
        }
        await api.updateViolation(updatedItem);

        const index = violationsList.value.findIndex(item => item.punishmentId === updatedItem.punishmentId);

        if (index !== -1) violationsList.value[index] = updatedItem;
        ElMessage.success(`处罚已${action === 'process' ? '开始执行' : '执行完成'}`);
        showViolationDetail.value = false;
    } catch (error) {
        if (error !== 'cancel') ElMessage.error('操作失败');
    }
};

const handleReviewAction = async () => {
    if (!reviewNote.value.trim() || !selectedReviewPunishment.value || !reviewPunishmentReason.value.trim()) return ElMessage.warning('请填写完整的处理信息和处理原因');
    if (!currentReview.value) return;
    try {
        await ElMessageBox.confirm('确定要执行选定的处理措施吗？', '确认操作', { type: 'warning' });
        const punishmentLabel = punishmentOptions.reviews.find(o => o.value === selectedReviewPunishment.value)?.label || selectedReviewPunishment.value;
        const updatedItem: ReviewItem = { ...currentReview.value, status: '已完成', punishment: punishmentLabel, punishmentReason: reviewPunishmentReason.value, processingNote: reviewNote.value };
        await api.updateReview(updatedItem);

        const index = reviewsList.value.findIndex(item => item.reviewId === updatedItem.reviewId);

        if (index !== -1) reviewsList.value[index] = updatedItem;
        ElMessage.success('评论处理完成');
        showReviewDetail.value = false;
    } catch (error) {
        if (error !== 'cancel') ElMessage.error('操作失败');
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