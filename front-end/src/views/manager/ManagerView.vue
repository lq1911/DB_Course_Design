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
                                @click="activeMenu = 'afterSales'">
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
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredAfterSales" :key="item.id" class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.applicationId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.orderId }}
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{
                                            item.applicationTime }}</td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{
                                            item.description }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <a href="https://readdy.ai/home/2f0e3279-0ca6-4f2a-a2fe-7ff07ff7c3dc/ed327a2a-6ee2-40e4-81aa-e76b3616f733"
                                                data-readdy="true"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">查看详情</a>
                                            <button v-if="item.status === '待处理'"
                                                class="text-blue-600 hover:text-blue-900 cursor-pointer !rounded-button whitespace-nowrap">处理</button>
                                            <button
                                                class="text-green-600 hover:text-green-900 cursor-pointer !rounded-button whitespace-nowrap">评估</button>
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
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredComplaints" :key="item.id" class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.complaintId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.target }}
                                        </td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{ item.content }}
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{
                                            item.applicationTime }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <a href="https://readdy.ai/home/2f0e3279-0ca6-4f2a-a2fe-7ff07ff7c3dc/ed327a2a-6ee2-40e4-81aa-e76b3616f733"
                                                data-readdy="true"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">查看详情</a>
                                            <button v-if="item.status === '待处理'"
                                                class="text-blue-600 hover:text-blue-900 cursor-pointer !rounded-button whitespace-nowrap">处理</button>
                                            <button
                                                class="text-green-600 hover:text-green-900 cursor-pointer !rounded-button whitespace-nowrap">评估</button>
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
                <!-- 举报管理页面 -->
                <div v-if="activeMenu === 'reports'">
                    <div class="bg-white rounded-xl shadow-sm border border-gray-100">
                        <div class="p-6 border-b border-gray-100">
                            <div class="flex items-center justify-between mb-4">
                                <h2 class="text-lg font-semibold text-gray-900">举报管理</h2>
                            </div>
                            <!-- 搜索和筛选区域 -->
                            <div class="flex items-center space-x-4 mb-6">
                                <div class="flex-1 max-w-md">
                                    <div class="relative">
                                        <input type="text" placeholder="搜索举报编号..."
                                            class="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm"
                                            v-model="reportSearchQuery">
                                        <i
                                            class="fas fa-search absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 text-sm"></i>
                                    </div>
                                </div>
                                <div class="flex space-x-2">
                                    <button v-for="status in reportStatuses" :key="status.value"
                                        :class="{ 'bg-orange-500 text-white': selectedReportStatus === status.value, 'bg-gray-100 text-gray-700 hover:bg-gray-200': selectedReportStatus !== status.value }"
                                        class="px-4 py-2 rounded-lg text-sm font-medium transition-colors cursor-pointer !rounded-button whitespace-nowrap"
                                        @click="selectedReportStatus = status.value">
                                        {{ status.label }}
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- 举报列表 -->
                        <div class="overflow-x-auto">
                            <table class="w-full">
                                <thead class="bg-gray-50">
                                    <tr>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            举报编号</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            举报对象</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            举报内容</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            期待解决方式</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            申请时间</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredReports" :key="item.id" class="hover:bg-gray-50">
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{{
                                            item.reportId }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ item.target }}
                                        </td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{ item.content }}
                                        </td>
                                        <td class="px-6 py-4 text-sm text-gray-900 max-w-xs truncate">{{
                                            item.expectedSolution }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{
                                            item.applicationTime }}</td>
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <a href="https://readdy.ai/home/2f0e3279-0ca6-4f2a-a2fe-7ff07ff7c3dc/ed327a2a-6ee2-40e4-81aa-e76b3616f733"
                                                data-readdy="true"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">查看详情</a>
                                            <button v-if="item.status === '待处理'"
                                                class="text-blue-600 hover:text-blue-900 cursor-pointer !rounded-button whitespace-nowrap">处理</button>
                                            <button
                                                class="text-green-600 hover:text-green-900 cursor-pointer !rounded-button whitespace-nowrap">评估</button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- 分页 -->
                        <div class="px-6 py-4 border-t border-gray-200">
                            <div class="flex items-center justify-between">
                                <div class="text-sm text-gray-700">
                                    显示第 1-10 条，共 {{ reportsList.length }} 条记录
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
                                    <tr v-for="item in filteredViolations" :key="item.id" class="hover:bg-gray-50">
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
                                            <a href="https://readdy.ai/home/2f0e3279-0ca6-4f2a-a2fe-7ff07ff7c3dc/ed327a2a-6ee2-40e4-81aa-e76b3616f733"
                                                data-readdy="true"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">查看详情</a>
                                            <button
                                                class="text-green-600 hover:text-green-900 cursor-pointer !rounded-button whitespace-nowrap">监督</button>
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
                                            状态</th>
                                        <th
                                            class="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                            操作</th>
                                    </tr>
                                </thead>
                                <tbody class="bg-white divide-y divide-gray-200">
                                    <tr v-for="item in filteredReviews" :key="item.id" class="hover:bg-gray-50">
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
                                        <td class="px-6 py-4 whitespace-nowrap">
                                            <span :class="getStatusClass(item.status)"
                                                class="inline-block px-2 py-1 text-xs rounded-full">
                                                {{ item.status }}
                                            </span>
                                        </td>
                                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium space-x-2">
                                            <a href="https://readdy.ai/home/2f0e3279-0ca6-4f2a-a2fe-7ff07ff7c3dc/ed327a2a-6ee2-40e4-81aa-e76b3616f733"
                                                data-readdy="true"
                                                class="text-orange-600 hover:text-orange-900 cursor-pointer !rounded-button whitespace-nowrap">查看详情</a>
                                            <button v-if="item.status === '待审核'"
                                                class="text-green-600 hover:text-green-900 cursor-pointer !rounded-button whitespace-nowrap">通过</button>
                                            <button v-if="item.status === '待审核'"
                                                class="text-red-600 hover:text-red-900 cursor-pointer !rounded-button whitespace-nowrap">拒绝</button>
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
</template>
<script lang="ts" setup>
import { ref, computed } from 'vue';
// 当前激活的菜单
const activeMenu = ref('admin');
// 搜索查询
const searchQuery = ref('');
const complaintSearchQuery = ref('');
const reportSearchQuery = ref('');
const violationSearchQuery = ref('');
const reviewSearchQuery = ref('');
// 状态筛选
const selectedAfterSalesStatus = ref('all');
const selectedComplaintStatus = ref('all');
const selectedReportStatus = ref('all');
const selectedViolationStatus = ref('all');
const selectedReviewStatus = ref('all');

// 售后申请数据
const afterSalesList = ref([
    {
        id: 1,
        applicationId: 'AS202401001',
        orderId: 'ORD202401001',
        applicationTime: '2024-01-15 14:30',
        description: '商品质量问题，要求退款',
        status: '待处理'
    },
    {
        id: 2,
        applicationId: 'AS202401002',
        orderId: 'ORD202401002',
        applicationTime: '2024-01-15 13:45',
        description: '配送延误，商品已变质',
        status: '处理中'
    },
    {
        id: 3,
        applicationId: 'AS202401003',
        orderId: 'ORD202401003',
        applicationTime: '2024-01-15 12:20',
        description: '商品与描述不符',
        status: '已完成'
    },
    {
        id: 4,
        applicationId: 'AS202401004',
        orderId: 'ORD202401004',
        applicationTime: '2024-01-15 11:10',
        description: '包装破损，商品洒漏',
        status: '待处理'
    }
]);
// 投诉数据
const complaintsList = ref([
    {
        id: 1,
        complaintId: 'CP202401001',
        target: '骑手张三',
        content: '配送员态度恶劣，服务质量差',
        applicationTime: '2024-01-15 14:30',
        status: '待处理'
    },
    {
        id: 2,
        complaintId: 'CP202401002',
        target: '商家李记餐厅',
        content: '商家出餐速度慢，影响配送时效',
        applicationTime: '2024-01-15 13:45',
        status: '处理中'
    },
    {
        id: 3,
        complaintId: 'CP202401003',
        target: '骑手王五',
        content: '配送员未按时送达，且态度不好',
        applicationTime: '2024-01-15 12:20',
        status: '已完成'
    }
]);
// 违规举报处理数据
const violationsList = ref([
    {
        id: 1,
        punishmentId: 'PUN202401001',
        storeName: '张记小炒',
        reason: '食品安全问题，使用过期食材制作食品',
        merchantPunishment: '警告并罚款1000元',
        storePunishment: '暂停营业3天',
        punishmentTime: '2024-01-15 14:30',
        status: '执行中'
    },
    {
        id: 2,
        punishmentId: 'PUN202401002',
        storeName: '美味餐厅',
        reason: '虚假宣传，图片与实物不符',
        merchantPunishment: '罚款500元',
        storePunishment: '下架违规商品',
        punishmentTime: '2024-01-15 13:45',
        status: '已完成'
    },
    {
        id: 3,
        punishmentId: 'PUN202401003',
        storeName: '快送外卖',
        reason: '配送员私自拆开包装',
        merchantPunishment: '配送员停职处理',
        storePunishment: '加强配送员管理',
        punishmentTime: '2024-01-15 12:20',
        status: '待处理'
    }
]);
// 评论审核数据
const reviewsList = ref([
    {
        id: 1,
        reviewId: 'RV202401001',
        username: '用户张三',
        storeName: '美味餐厅',
        content: '味道不错，配送也很快，推荐！',
        rating: 5,
        submitTime: '2024-01-15 14:30',
        status: '待审核'
    },
    {
        id: 2,
        reviewId: 'RV202401002',
        username: '用户李四',
        storeName: '快餐店',
        content: '包装很好，食物新鲜，服务态度也不错',
        rating: 4,
        submitTime: '2024-01-15 13:45',
        status: '已通过'
    },
    {
        id: 3,
        reviewId: 'RV202401003',
        username: '用户王五',
        storeName: '小吃摊',
        content: '这家店的食物质量有问题，不建议购买',
        rating: 1,
        submitTime: '2024-01-15 12:20',
        status: '已拒绝'
    }
]);
// 状态选项
const afterSalesStatuses = [
    { label: '全部', value: 'all' },
    { label: '待处理', value: '待处理' },
    { label: '处理中', value: '处理中' },
    { label: '已完成', value: '已完成' }
];
const complaintStatuses = [
    { label: '全部', value: 'all' },
    { label: '待处理', value: '待处理' },
    { label: '处理中', value: '处理中' },
    { label: '已完成', value: '已完成' }
];
const reportStatuses = [
    { label: '全部', value: 'all' },
    { label: '待处理', value: '待处理' },
    { label: '处理中', value: '处理中' },
    { label: '已完成', value: '已完成' }
];
const violationStatuses = [
    { label: '全部', value: 'all' },
    { label: '执行中', value: '执行中' },
    { label: '已完成', value: '已完成' },
    { label: '已撤销', value: '已撤销' }
];
const reviewStatuses = [
    { label: '全部', value: 'all' },
    { label: '待审核', value: '待审核' },
    { label: '已通过', value: '已通过' },
    { label: '已拒绝', value: '已拒绝' }
];
// 计算属性 - 筛选数据
const filteredAfterSales = computed(() => {
    let filtered = afterSalesList.value;
    if (selectedAfterSalesStatus.value !== 'all') {
        filtered = filtered.filter(item => item.status === selectedAfterSalesStatus.value);
    }
    if (searchQuery.value) {
        filtered = filtered.filter(item =>
            item.applicationId.toLowerCase().includes(searchQuery.value.toLowerCase()) ||
            item.orderId.toLowerCase().includes(searchQuery.value.toLowerCase())
        );
    }
    return filtered;
});
const filteredComplaints = computed(() => {
    let filtered = complaintsList.value;
    if (selectedComplaintStatus.value !== 'all') {
        filtered = filtered.filter(item => item.status === selectedComplaintStatus.value);
    }
    if (complaintSearchQuery.value) {
        filtered = filtered.filter(item =>
            item.complaintId.toLowerCase().includes(complaintSearchQuery.value.toLowerCase()) ||
            item.target.toLowerCase().includes(complaintSearchQuery.value.toLowerCase())
        );
    }
    return filtered;
});
const filteredReports = computed(() => {
    let filtered = reportsList.value;
    if (selectedReportStatus.value !== 'all') {
        filtered = filtered.filter(item => item.status === selectedReportStatus.value);
    }
    if (reportSearchQuery.value) {
        filtered = filtered.filter(item =>
            item.reportId.toLowerCase().includes(reportSearchQuery.value.toLowerCase()) ||
            item.target.toLowerCase().includes(reportSearchQuery.value.toLowerCase())
        );
    }
    return filtered;
});
const filteredViolations = computed(() => {
    let filtered = violationsList.value;
    if (selectedViolationStatus.value !== 'all') {
        filtered = filtered.filter(item => item.status === selectedViolationStatus.value);
    }
    if (violationSearchQuery.value) {
        filtered = filtered.filter(item =>
            item.punishmentId.toLowerCase().includes(violationSearchQuery.value.toLowerCase()) ||
            item.storeName.toLowerCase().includes(violationSearchQuery.value.toLowerCase())
        );
    }
    return filtered;
});
const filteredReviews = computed(() => {
    let filtered = reviewsList.value;
    if (selectedReviewStatus.value !== 'all') {
        filtered = filtered.filter(item => item.status === selectedReviewStatus.value);
    }
    if (reviewSearchQuery.value) {
        filtered = filtered.filter(item =>
            item.content.toLowerCase().includes(reviewSearchQuery.value.toLowerCase()) ||
            item.username.toLowerCase().includes(reviewSearchQuery.value.toLowerCase())
        );
    }
    return filtered;
});
// 工具函数
const getBreadcrumb = () => {
    const breadcrumbs = {
        admin: '管理员信息',
        afterSales: '售后处理中心',
        complaints: '投诉处理中心',
        violations: '违规举报处理',
        reviews: '评论审核管理'
    };
    return breadcrumbs[activeMenu.value] || '控制台';
};
const getStatusClass = (status: string) => {
    const statusClasses = {
        '待处理': 'bg-yellow-100 text-yellow-800',
        '处理中': 'bg-blue-100 text-blue-800',
        '已完成': 'bg-green-100 text-green-800',
        '已处理': 'bg-green-100 text-green-800',
        '执行中': 'bg-blue-100 text-blue-800',
        '已撤销': 'bg-gray-100 text-gray-800',
        '待审核': 'bg-yellow-100 text-yellow-800',
        '已通过': 'bg-green-100 text-green-800',
        '已拒绝': 'bg-red-100 text-red-800'
    };
    return statusClasses[status] || 'bg-gray-100 text-gray-800';
};
const getRecordIcon = (type: string) => {
    const iconClasses = {
        afterSales: 'bg-blue-100',
        complaint: 'bg-red-100',
        report: 'bg-yellow-100',
        review: 'bg-green-100'
    };
    return iconClasses[type] || 'bg-gray-100';
};
const getRecordIconClass = (type: string) => {
    const iconClasses = {
        afterSales: 'fas fa-headset text-blue-600',
        complaint: 'fas fa-exclamation-triangle text-red-600',
        report: 'fas fa-flag text-yellow-600',
        review: 'fas fa-comment-dots text-green-600'
    };
    return iconClasses[type] || 'fas fa-circle text-gray-600';
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