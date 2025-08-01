/* eslint-disable */
<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->

<template>
<div class="min-h-screen bg-gray-50">
<!-- 顶部导航栏 -->
<header class="fixed top-0 left-0 right-0 bg-white shadow-sm z-50 h-16">
<div class="flex items-center justify-between h-full px-6">
<div class="flex items-center">
<h1 class="text-xl font-bold text-[#F9771C]">FoodDelivery Pro</h1>
</div>
<div class="flex items-center space-x-4">
<el-icon class="text-gray-600 text-xl cursor-pointer"><Bell /></el-icon>
<div class="flex items-center space-x-2">
<img
src="https://readdy.ai/api/search-image?query=professional%20restaurant%20owner%20portrait%20with%20friendly%20smile%20wearing%20chef%20uniform%20against%20clean%20white%20background%20modern%20lighting&width=40&height=40&seq=merchant-avatar-001&orientation=squarish"
alt="商家头像"
class="w-10 h-10 rounded-full object-cover"
/>
<span class="text-gray-700 font-medium">张老板</span>
</div>
</div>
</div>
</header>
<div class="flex pt-16">
<!-- 左侧导航菜单 -->
<aside class="fixed left-0 top-16 bottom-0 w-52 bg-white shadow-sm overflow-y-auto">
<nav class="p-4">
<div class="space-y-2">
<div
v-for="(item, index) in menuItems"
:key="index"
@click="activeMenu = item.key"
:class="{
'bg-orange-50 text-[#F9771C] border-r-3 border-[#F9771C]': activeMenu === item.key,
'text-gray-700 hover:bg-gray-50': activeMenu !== item.key
}"
class="flex items-center px-4 py-3 rounded-l-lg cursor-pointer transition-colors whitespace-nowrap !rounded-button"
>
<el-icon class="mr-3 text-lg">
<component :is="item.icon" />
</el-icon>
<span class="font-medium">{{ item.label }}</span>
</div>
</div>
</nav>
</aside>
<!-- 主内容区 -->
<main class="ml-52 flex-1 p-6">
<!-- 店铺概况 -->
<div v-if="activeMenu === 'overview'">
<h2 class="text-2xl font-bold text-gray-800 mb-6">店铺概况</h2>
<!-- 数据卡片区 -->
<div class="grid grid-cols-4 gap-6 mb-8">
<div class="bg-white rounded-lg shadow-sm p-6">
<div class="flex items-center justify-between">
<div>
<p class="text-gray-600 text-sm">店铺评分</p>
<p class="text-2xl font-bold text-orange-500">{{ shopInfo.rating }}</p>
</div>
<el-icon class="text-orange-500 text-3xl"><Star /></el-icon>
</div>
</div>
<div class="bg-white rounded-lg shadow-sm p-6">
<div class="flex items-center justify-between">
<div>
<p class="text-gray-600 text-sm">月销量</p>
<p class="text-2xl font-bold text-green-500">{{ shopInfo.monthlySales }}</p>
</div>
<el-icon class="text-green-500 text-3xl"><TrendCharts /></el-icon>
</div>
</div>
<div class="bg-white rounded-lg shadow-sm p-6">
<div class="flex items-center justify-between">
<div>
<p class="text-gray-600 text-sm">信誉积分</p>
<p class="text-2xl font-bold text-blue-500">{{ merchantInfo.creditScore }}</p>
</div>
<el-icon class="text-blue-500 text-3xl"><Medal /></el-icon>
</div>
</div>
<div class="bg-white rounded-lg shadow-sm p-6">
<div class="flex items-center justify-between">
<div>
<p class="text-gray-600 text-sm">营业状态</p>
<p class="text-lg font-bold" :class="isOpen ? 'text-[#F9771C]' : 'text-gray-500'">
{{ isOpen ? '营业中' : '休息中' }}
</p>
</div>
<el-switch
v-model="isOpen"
active-color="#F9771C"
inactive-color="#DCDFE6"
@change="toggleBusinessStatus"
/>
</div>
</div>
</div>
<!-- 店铺基本信息 -->
<div class="bg-white rounded-lg shadow-sm p-6">
<h3 class="text-lg font-semibold text-gray-800 mb-4">店铺基本信息</h3>
<div class="grid grid-cols-2 gap-6">
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">店铺编号</label>
<p class="text-gray-900">{{ shopInfo.id }}</p>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">店铺名称</label>
<p class="text-gray-900">{{ shopInfo.name }}</p>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">建立时间</label>
<p class="text-gray-900">{{ shopInfo.createTime }}</p>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">店铺地址</label>
<input
v-model="shopInfo.address"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
placeholder="请输入店铺地址"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">营业时间</label>
<input
v-model="shopInfo.businessHours"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
placeholder="例：09:00-22:00"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">店铺特色</label>
<input
v-model="shopInfo.feature"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
placeholder="请输入店铺特色（选填）"
/>
</div>
</div>
<div class="mt-6">
<button
@click="saveShopInfo"
:disabled="isSaving"
:class="{
'opacity-75 cursor-not-allowed': isSaving,
'hover:bg-[#E16A0E]': !isSaving
}"
class="bg-[#F9771C] text-white px-6 py-2 rounded-md transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
{{ isSaving ? '保存中...' : '保存信息' }}
</button>
</div>
</div>
</div>
<!-- 订单中心 -->
<div v-if="activeMenu === 'orders'">
<h2 class="text-2xl font-bold text-gray-800 mb-6">订单中心</h2>
<!-- 订单/菜品切换 -->
<div class="bg-white rounded-lg shadow-sm p-4 mb-6">
<div class="flex space-x-4">
<button
v-for="tab in orderTabs"
:key="tab.value"
@click="activeOrderTab = tab.value"
:class="{
'bg-[#F9771C] text-white': activeOrderTab === tab.value,
'bg-gray-100 text-gray-700 hover:bg-gray-200': activeOrderTab !== tab.value
}"
class="px-4 py-2 rounded-md transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
{{ tab.label }}
</button>
</div>
</div>
<div v-if="activeOrderTab === 'orders'">
<div class="bg-white rounded-lg shadow-sm p-4 mb-6">
<div class="flex justify-between items-center">
<div class="flex space-x-4">
<button
v-for="status in orderStatuses"
:key="status.value"
@click="selectedOrderStatus = status.value"
:class="{
'bg-[#F9771C] text-white': selectedOrderStatus === status.value,
'bg-gray-100 text-gray-700 hover:bg-gray-200': selectedOrderStatus !== status.value
}"
class="px-4 py-2 rounded-md transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
{{ status.label }}
</button>
</div>
<div class="flex items-center space-x-2">
<span class="text-gray-600">自动接单</span>
<el-switch
v-model="autoAcceptOrders"
active-color="#F9771C"
inactive-color="#DCDFE6"
@change="toggleAutoAcceptOrders"
/>
</div>
</div>
</div>
<!-- 订单列表 -->
<div class="bg-white rounded-lg shadow-sm">
<el-table :data="filteredOrders" style="width: 100%">
<el-table-column prop="orderNo" label="订单号" width="180" />
<el-table-column prop="payTime" label="支付时间" width="180" />
<el-table-column prop="status" label="订单状态" width="120">
<template #default="scope">
<span
:class="{
'text-orange-500': scope.row.status === '未接单',
'text-blue-500': scope.row.status === '已接单',
'text-purple-500': scope.row.status === '已出餐',
'text-green-500': scope.row.status === '配送中',
'text-gray-500': scope.row.status === '已送达'
}"
class="font-medium"
>
{{ scope.row.status }}
</span>
</template>
</el-table-column>
<el-table-column label="菜品信息" min-width="200">
<template #default="scope">
<div class="space-y-1">
<div v-for="(dish, index) in scope.row.dishes" :key="index" class="flex justify-between text-sm">
<span>{{ dish.name }} x{{ dish.quantity }}</span>
<span class="text-gray-600">¥{{ dish.price * dish.quantity }}</span>
</div>
</div>
</template>
</el-table-column>
<el-table-column prop="remark" label="备注" />
<el-table-column label="操作" width="200">
<template #default="scope">
<button
v-if="scope.row.status === '未接单'"
@click="updateOrderStatus(scope.row, '已接单')"
class="bg-[#F9771C] text-white px-3 py-1 rounded text-sm hover:bg-[#E16A0E] transition-colors mr-2 cursor-pointer whitespace-nowrap !rounded-button"
>
接单
</button>
<button
v-if="scope.row.status === '已接单'"
@click="updateOrderStatus(scope.row, '已出餐')"
class="bg-purple-600 text-white px-3 py-1 rounded text-sm hover:bg-purple-700 transition-colors mr-2 cursor-pointer whitespace-nowrap !rounded-button"
>
出餐
</button>
<button
v-if="scope.row.status === '已出餐'"
@click="publishDeliveryTask(scope.row)"
class="bg-green-600 text-white px-3 py-1 rounded text-sm hover:bg-green-700 transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
发布配送
</button>
</template>
</el-table-column>
</el-table>
</div>
</div>
<!-- 菜品管理 -->
<div v-else-if="activeOrderTab === 'dishes'">
<div class="flex justify-between items-center mb-6">
<div class="flex space-x-4">
<button
v-for="status in dishStatuses"
:key="status.value"
@click="selectedDishStatus = status.value"
:class="{
'bg-blue-600 text-white': selectedDishStatus === status.value,
'bg-gray-100 text-gray-700 hover:bg-gray-200': selectedDishStatus !== status.value
}"
class="px-4 py-2 rounded-md transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
{{ status.label }}
</button>
</div>
<button
@click="showDishForm = true"
class="bg-[#F9771C] text-white px-6 py-2 rounded-md hover:bg-[#E16A0E] transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
新增菜品
</button>
</div>
<div class="bg-white rounded-lg shadow-sm">
<el-table :data="filteredDishes" style="width: 100%">
<el-table-column width="120">
<template #default="scope">
<img
:src="scope.row.image"
:alt="scope.row.name"
class="w-20 h-20 object-cover rounded-lg"
/>
</template>
</el-table-column>
<el-table-column prop="name" label="菜品名称" width="180" />
<el-table-column prop="price" label="价格" width="120">
<template #default="scope">
¥{{ scope.row.price }}
</template>
</el-table-column>
<el-table-column prop="category" label="分类" width="120" />
<el-table-column prop="description" label="描述" />
<el-table-column prop="status" label="状态" width="120">
<template #default="scope">
<span
:class="{
'text-green-500': scope.row.status === '上架中',
'text-gray-500': scope.row.status === '已下架'
}"
class="font-medium"
>
{{ scope.row.status }}
</span>
</template>
</el-table-column>
<el-table-column label="操作" width="120">
<template #default="scope">
<button
@click="toggleDishStatus(scope.row)"
:class="{
'bg-red-600 hover:bg-red-700': scope.row.status === '上架中',
'bg-green-600 hover:bg-green-700': scope.row.status === '已下架'
}"
class="text-white px-3 py-1 rounded text-sm transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
{{ scope.row.status === '上架中' ? '下架' : '上架' }}
</button>
</template>
</el-table-column>
</el-table>
</div>
<!-- 新增菜品弹窗 -->
<div v-if="showDishForm" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
<div class="bg-white rounded-lg p-6 w-[500px]">
<h3 class="text-lg font-semibold text-gray-800 mb-4">新增菜品</h3>
<div class="space-y-4">
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">菜品名称</label>
<input
v-model="newDish.name"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
placeholder="请输入菜品名称"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">价格</label>
<input
v-model="newDish.price"
type="number"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
placeholder="请输入价格"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">分类</label>
<select
v-model="newDish.category"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
>
<option value="川菜">川菜</option>
<option value="粤菜">粤菜</option>
<option value="湘菜">湘菜</option>
<option value="特色小吃">特色小吃</option>
</select>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">描述</label>
<textarea
v-model="newDish.description"
rows="3"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 text-sm resize-none"
placeholder="请输入菜品描述"
></textarea>
</div>
</div>
<div class="flex justify-end space-x-3 mt-6">
<button
@click="showDishForm = false"
class="px-4 py-2 text-gray-600 border border-gray-300 rounded-md hover:bg-gray-50 transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
取消
</button>
<button
@click="createDish"
class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
创建
</button>
</div>
</div>
</div>
</div>
</div>
<!-- 配券中心 -->
<div v-if="activeMenu === 'coupons'">
<div class="flex justify-between items-center mb-6">
<h2 class="text-2xl font-bold text-gray-800">配券中心</h2>
<button
@click="showCouponForm = true"
class="bg-[#F9771C] text-white px-6 py-2 rounded-md hover:bg-[#E16A0E] transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
新建优惠券
</button>
</div>
<!-- 优惠券列表 -->
<div class="bg-white rounded-lg shadow-sm">
<el-table :data="coupons" style="width: 100%">
<el-table-column prop="id" label="优惠券编号" width="180" />
<el-table-column prop="minAmount" label="起用金额" width="120">
<template #default="scope">
¥{{ scope.row.minAmount }}
</template>
</el-table-column>
<el-table-column prop="discountAmount" label="优惠金额" width="120">
<template #default="scope">
¥{{ scope.row.discountAmount }}
</template>
</el-table-column>
<el-table-column prop="startTime" label="开始时间" width="180" />
<el-table-column prop="endTime" label="结束时间" width="180" />
<el-table-column label="状态" width="100">
<template #default="scope">
<span
:class="{
'text-green-500': isValidCoupon(scope.row),
'text-gray-500': !isValidCoupon(scope.row)
}"
class="font-medium"
>
{{ isValidCoupon(scope.row) ? '有效' : '已过期' }}
</span>
</template>
</el-table-column>
</el-table>
</div>
<!-- 新建优惠券弹窗 -->
<div v-if="showCouponForm" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
<div class="bg-white rounded-lg p-6 w-96">
<h3 class="text-lg font-semibold text-gray-800 mb-4">新建优惠券</h3>
<div class="space-y-4">
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">起用金额</label>
<input
v-model="newCoupon.minAmount"
type="number"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
placeholder="请输入起用金额"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">优惠金额</label>
<input
v-model="newCoupon.discountAmount"
type="number"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
placeholder="请输入优惠金额"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">开始时间</label>
<input
v-model="newCoupon.startTime"
type="datetime-local"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">结束时间</label>
<input
v-model="newCoupon.endTime"
type="datetime-local"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
/>
</div>
</div>
<div class="flex justify-end space-x-3 mt-6">
<button
@click="showCouponForm = false"
class="px-4 py-2 text-gray-600 border border-gray-300 rounded-md hover:bg-gray-50 transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
取消
</button>
<button
@click="createCoupon"
class="px-4 py-2 bg-[#F9771C] text-white rounded-md hover:bg-[#E16A0E] transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
创建
</button>
</div>
</div>
</div>
</div>
<!-- 订单售后 -->
<div v-if="activeMenu === 'aftersale'">
<h2 class="text-2xl font-bold text-gray-800 mb-6">订单售后</h2>
<!-- 切换标签 -->
<div class="bg-white rounded-lg shadow-sm p-4 mb-6">
<div class="flex space-x-4">
<button
v-for="tab in aftersaleTabs"
:key="tab.value"
@click="activeAftersaleTab = tab.value"
:class="{
'bg-[#F9771C] text-white': activeAftersaleTab === tab.value,
'bg-gray-100 text-gray-700 hover:bg-gray-200': activeAftersaleTab !== tab.value
}"
class="px-4 py-2 rounded-md transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
{{ tab.label }}
</button>
</div>
</div>
<!-- 处罚记录 -->
<div v-if="activeAftersaleTab === 'penalties'">
<div class="bg-white rounded-lg shadow-sm">
<el-table :data="penalties" style="width: 100%">
<el-table-column prop="id" label="处罚编号" width="150" />
<el-table-column prop="reason" label="处罚原因" />
<el-table-column prop="time" label="处罚时间" width="180" />
<el-table-column prop="merchantAction" label="商家处罚措施" />
<el-table-column prop="platformAction" label="店铺处罚措施" />
</el-table>
</div>
</div>
<!-- 评论查看 -->
<div v-if="activeAftersaleTab === 'reviews'">
<div class="space-y-4">
<div v-for="review in reviews" :key="review.id" class="bg-white rounded-lg shadow-sm p-6">
<div class="flex items-start space-x-4">
<img
:src="review.avatar"
alt="用户头像"
class="w-12 h-12 rounded-full object-cover"
/>
<div class="flex-1">
<div class="flex items-center space-x-2 mb-2">
<span class="font-medium text-gray-800">{{ review.username }}</span>
<div class="flex text-yellow-400">
<el-icon v-for="i in review.rating" :key="i"><Star /></el-icon>
</div>
<span class="text-sm text-gray-500">{{ review.time }}</span>
</div>
<p class="text-gray-700 mb-3">{{ review.content }}</p>
<div v-if="review.reply" class="bg-gray-50 rounded-lg p-3">
<p class="text-sm text-gray-600"><strong>商家回复：</strong>{{ review.reply }}</p>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
<!-- 商家信息 -->
<div v-if="activeMenu === 'profile'">
<h2 class="text-2xl font-bold text-gray-800 mb-6">商家信息</h2>
<div class="bg-white rounded-lg shadow-sm p-6">
<h3 class="text-lg font-semibold text-gray-800 mb-4">基本信息</h3>
<div class="grid grid-cols-2 gap-6">
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">用户名</label>
<input
v-model="merchantInfo.username"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">手机号</label>
<input
v-model="merchantInfo.phone"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">电子邮箱</label>
<input
v-model="merchantInfo.email"
type="email"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">性别</label>
<select
v-model="merchantInfo.gender"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
>
<option value="男">男</option>
<option value="女">女</option>
</select>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">姓名</label>
<input
v-model="merchantInfo.name"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">生日</label>
<input
v-model="merchantInfo.birthday"
type="date"
class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
/>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">注册时间</label>
<p class="text-gray-900 py-2">{{ merchantInfo.registerTime }}</p>
</div>
<div>
<label class="block text-sm font-medium text-gray-700 mb-2">账号状态</label>
<p class="text-green-600 font-medium py-2">{{ merchantInfo.status }}</p>
</div>
</div>
<div class="mt-6">
<button
@click="saveShopInfo"
:disabled="isSaving"
:class="{
'opacity-75 cursor-not-allowed': isSaving,
'hover:bg-[#E16A0E]': !isSaving
}"
class="bg-[#F9771C] text-white px-6 py-2 rounded-md transition-colors cursor-pointer whitespace-nowrap !rounded-button"
>
{{ isSaving ? '保存中...' : '保存信息' }}
</button>
</div>
</div>
</div>
</main>
</div>
</div>
</template>
<script lang="ts" setup>
import { ref, computed } from 'vue';
// import { Bell, Star, TrendCharts, Medal, Shop, House, List, Ticket, Warning, ChatDotRound, User } from '@element-plus/icons-vue';
import { Bell, Star, TrendCharts, Medal,  House, List, Ticket, Warning,  User } from '@element-plus/icons-vue';
const activeMenu = ref('overview');
const menuItems = [
{ key: 'overview', label: '店铺概况', icon: House },
{ key: 'orders', label: '订单中心', icon: List },
{ key: 'coupons', label: '配券中心', icon: Ticket },
{ key: 'aftersale', label: '订单售后', icon: Warning },
{ key: 'profile', label: '商家信息', icon: User }
];
import { ElMessage, ElMessageBox } from 'element-plus';
const isOpen = ref(true);
const autoAcceptOrders = ref(false);
const toggleBusinessStatus = async (value: boolean) => {
try {
await ElMessageBox.confirm(
`确定要${value ? '开启营业' : '暂停营业'}吗？`,
'提示',
{
confirmButtonText: '确定',
cancelButtonText: '取消',
type: 'warning'
}
);
ElMessage({
type: 'success',
message: `已${value ? '开启营业' : '暂停营业'}`
});
} catch {
isOpen.value = !value;
}
};
const toggleAutoAcceptOrders = (value: boolean) => {
ElMessage({
type: 'success',
message: `已${value ? '开启' : '关闭'}自动接单`
});
};
const shopInfo = ref({
id: 'SH20241201001',
name: '张记川菜馆',
createTime: '2024-01-15',
address: '北京市朝阳区三里屯街道工体北路8号',
businessHours: '09:00-22:00',
feature: '正宗川菜，麻辣鲜香',
rating: 4.8,
monthlySales: 1256
});
const isSaving = ref(false);
const saveShopInfo = async () => {
if (isSaving.value) return;
isSaving.value = true;
try {
// Simulate API call
await new Promise(resolve => setTimeout(resolve, 1500));
ElMessage({
message: '店铺信息保存成功',
type: 'success',
});
} catch (error) {
ElMessage.error('保存失败，请重试');
} finally {
isSaving.value = false;
}
};
const merchantInfo = ref({
username: 'zhanglaosan',
phone: '13800138000',
email: 'zhang@example.com',
gender: '男',
name: '张老三',
birthday: '1980-05-15',
registerTime: '2024-01-15 10:30:00',
creditScore: 95,
status: '正常营业'
});
const orderStatuses = [
{ value: 'all', label: '全部订单' },
{ value: '未接单', label: '未接单' },
{ value: '已接单', label: '已接单' },
{ value: '已出餐', label: '已出餐' },
{ value: '配送中', label: '配送中' },
{ value: '已送达', label: '已送达' }
];
const orderTabs = [
{ value: 'orders', label: '订单管理' },
{ value: 'dishes', label: '菜品管理' }
];
const activeOrderTab = ref('orders');
const dishStatuses = [
{ value: 'all', label: '全部' },
{ value: '上架中', label: '上架中' },
{ value: '已下架', label: '已下架' }
];
const selectedDishStatus = ref('all');
const showDishForm = ref(false);
const newDish = ref({
name: '',
price: '',
category: '川菜',
description: '',
status: '上架中'
});
const dishes = ref([
{
id: 1,
name: '麻婆豆腐',
price: 28,
category: '川菜',
description: '使用特制豆瓣酱，口感麻辣鲜香',
status: '上架中',
image: 'https://readdy.ai/api/search-image?query=traditional%20Chinese%20mapo%20tofu%20dish%20with%20rich%20red%20sauce%20and%20minced%20meat%20garnished%20with%20green%20onions%20on%20white%20plate%20professional%20food%20photography&width=80&height=80&seq=dish-001&orientation=squarish'
},
{
id: 2,
name: '宫保鸡丁',
price: 32,
category: '川菜',
description: '选用新鲜鸡肉，搭配花生，口感鲜辣',
status: '上架中',
image: 'https://readdy.ai/api/search-image?query=kung%20pao%20chicken%20with%20peanuts%20and%20dried%20chilies%20in%20dark%20sauce%20Chinese%20cuisine%20professional%20food%20photography&width=80&height=80&seq=dish-002&orientation=squarish'
},
{
id: 3,
name: '水煮鱼',
price: 58,
category: '川菜',
description: '新鲜草鱼，配以特制料汤',
status: '已下架',
image: 'https://readdy.ai/api/search-image?query=Sichuan%20boiled%20fish%20in%20hot%20chili%20oil%20with%20bean%20sprouts%20and%20vegetables%20professional%20food%20photography&width=80&height=80&seq=dish-003&orientation=squarish'
}
]);
const filteredDishes = computed(() => {
if (selectedDishStatus.value === 'all') {
return dishes.value;
}
return dishes.value.filter(dish => dish.status === selectedDishStatus.value);
});
const toggleDishStatus = (dish: any) => {
dish.status = dish.status === '上架中' ? '已下架' : '上架中';
};
const createDish = () => {
const newId = dishes.value.length + 1;
dishes.value.push({
id: newId,
...newDish.value,
image: 'https://readdy.ai/api/search-image?query=delicious%20Chinese%20cuisine%20dish%20beautifully%20plated%20on%20white%20background%20professional%20food%20photography&width=80&height=80&seq=dish-new&orientation=squarish'
});
showDishForm.value = false;
newDish.value = {
name: '',
price: '',
category: '川菜',
description: '',
status: '上架中'
};
};
const selectedOrderStatus = ref('all');
const orders = ref([
{
orderNo: 'ORD20241201001',
payTime: '2024-12-01 12:30:00',
status: '未接单',
remark: '不要辣椒',
dishes: [
{
name: '麻婆豆腐',
price: 28,
quantity: 1
},
{
name: '宫保鸡丁',
price: 32,
quantity: 2
}
]
},
{
orderNo: 'ORD20241201002',
payTime: '2024-12-01 13:15:00',
status: '已接单',
remark: '多加米饭',
dishes: [
{
name: '水煮鱼',
price: 58,
quantity: 1
}
]
},
{
orderNo: 'ORD20241201003',
payTime: '2024-12-01 14:00:00',
status: '已出餐',
remark: '打包带走',
dishes: [
{
name: '麻婆豆腐',
price: 28,
quantity: 2
}
]
}
]);
const filteredOrders = computed(() => {
if (selectedOrderStatus.value === 'all') {
return orders.value;
}
return orders.value.filter(order => order.status === selectedOrderStatus.value);
});
const showCouponForm = ref(false);
const newCoupon = ref({
minAmount: '',
discountAmount: '',
startTime: '',
endTime: ''
});
const coupons = ref([
{
id: '20241201120000',
minAmount: 50,
discountAmount: 10,
startTime: '2024-12-01 00:00:00',
endTime: '2024-12-31 23:59:59'
},
{
id: '20241201130000',
minAmount: 100,
discountAmount: 20,
startTime: '2024-12-01 00:00:00',
endTime: '2024-11-30 23:59:59'
}
]);
const penalties = ref([
{
id: 'PEN20241201001',
reason: '食品安全问题',
time: '2024-11-15 16:30:00',
merchantAction: '整改厨房卫生',
platformAction: '警告处理'
}
]);
const reviews = ref([
{
id: 1,
username: '美食爱好者',
avatar: 'https://readdy.ai/api/search-image?query=happy%20customer%20portrait%20smiling%20face%20with%20clean%20background%20modern%20lighting%20professional%20photo&width=48&height=48&seq=customer-avatar-001&orientation=squarish',
rating: 5,
time: '2024-12-01 14:30:00',
content: '菜品很棒，味道正宗，配送也很及时！',
reply: ''
},
{
id: 2,
username: '吃货小王',
avatar: 'https://readdy.ai/api/search-image?query=young%20customer%20portrait%20friendly%20smile%20with%20clean%20background%20modern%20lighting%20professional%20photo&width=48&height=48&seq=customer-avatar-002&orientation=squarish',
rating: 4,
time: '2024-11-30 19:45:00',
content: '川菜做得不错，就是稍微有点咸了。',
reply: '感谢您的反馈，我们会注意调整口味的。'
}
]);
const aftersaleTabs = [
{ value: 'penalties', label: '处罚记录' },
{ value: 'reviews', label: '评论查看' }
];
const activeAftersaleTab = ref('penalties');
const updateOrderStatus = (order: any, newStatus: string) => {
order.status = newStatus;
};
const publishDeliveryTask = (order: any) => {
order.status = '配送中';
};
const isValidCoupon = (coupon: any) => {
const now = new Date();
const endTime = new Date(coupon.endTime);
return endTime > now;
};
const createCoupon = () => {
const now = new Date();
const couponId = now.getFullYear().toString() +
(now.getMonth() + 1).toString().padStart(2, '0') +
now.getDate().toString().padStart(2, '0') +
now.getHours().toString().padStart(2, '0') +
now.getMinutes().toString().padStart(2, '0') +
now.getSeconds().toString().padStart(2, '0');
coupons.value.push({
id: couponId,
minAmount: Number(newCoupon.value.minAmount),
discountAmount: Number(newCoupon.value.discountAmount),
startTime: newCoupon.value.startTime.replace('T', ' ') + ':00',
endTime: newCoupon.value.endTime.replace('T', ' ') + ':00'
});
showCouponForm.value = false;
newCoupon.value = {
minAmount: '',
discountAmount: '',
startTime: '',
endTime: ''
};
};
</script>
<style scoped>
.\!rounded-button {
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
</style>
