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
              @click="handleMenuClick(item)"
              :class="{
                'bg-orange-50 text-[#F9771C] border-r-3 border-[#F9771C]': $route.name === item.routeName,
                'text-gray-700 hover:bg-gray-50': $route.name !== item.routeName
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
        <!-- 订单中心 -->
        <div v-if="activeMenu === 'orders'">
          <h2 class="text-2xl font-bold text-gray-800 mb-6">订单中心</h2>
          
          <!-- 错误提示区域 -->
          <div v-if="errorMessage" class="mb-4 p-4 bg-red-50 border border-red-200 rounded-lg">
            <div class="flex items-center justify-between">
              <div class="flex items-center">
                <svg class="w-5 h-5 text-red-400 mr-2" fill="currentColor" viewBox="0 0 20 20">
                  <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clip-rule="evenodd"></path>
                </svg>
                <span class="text-red-800">{{ errorMessage }}</span>
              </div>
              <div class="flex items-center space-x-2">
                <button 
                  @click="retryLoad"
                  class="text-red-400 hover:text-red-600 transition-colors px-2 py-1 rounded text-sm"
                >
                  重试
                </button>
                <button 
                  @click="clearError"
                  class="text-red-400 hover:text-red-600 transition-colors"
                >
                  <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
                  </svg>
                </button>
              </div>
            </div>
          </div>
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
                        <div class="bg-white/95 backdrop-blur-xl rounded-3xl p-10 mb-10 shadow-2xl border border-orange-200/20 relative overflow-hidden">
              <!-- 装饰性背景元素 -->
              <div class="absolute top-0 right-0 w-32 h-32 bg-gradient-to-br from-[#F9771C]/10 to-transparent rounded-full -translate-y-16 translate-x-16"></div>
              <div class="absolute bottom-0 left-0 w-24 h-24 bg-gradient-to-tr from-[#F9771C]/5 to-transparent rounded-full translate-y-12 -translate-x-12"></div>
              
              <div class="flex justify-between items-center relative z-10">
                <div class="flex space-x-5">
                  <button
                    v-for="status in orderStatuses"
                    :key="status.value"
                    @click="selectedOrderStatus = status.value"
                    :class="{
                      'bg-[#F9771C] text-white shadow-2xl transform scale-105 ring-4 ring-[#F9771C]/15 backdrop-blur-sm': selectedOrderStatus === status.value,
                      'bg-white/90 text-gray-700 hover:bg-white hover:shadow-xl backdrop-blur-sm border border-gray-200/30': selectedOrderStatus !== status.value
                    }"
                    class="px-10 py-5 rounded-3xl transition-all duration-300 cursor-pointer whitespace-nowrap font-semibold text-sm relative overflow-hidden group"
                  >
                    <!-- 按钮内部装饰 -->
                    <div class="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-700"></div>
                    <span class="relative z-10">{{ status.label }}</span>
                  </button>
                </div>
                <div class="flex items-center space-x-8">
                  <div class="flex items-center space-x-5 bg-white/90 backdrop-blur-xl rounded-3xl px-8 py-4 shadow-xl border border-gray-200/30 relative overflow-hidden">
                    <!-- 装饰性光效 -->
                    <div class="absolute inset-0 bg-gradient-to-r from-[#F9771C]/5 via-transparent to-[#F9771C]/5"></div>
                    <span class="text-sm text-gray-700 font-semibold relative z-10">自动接单</span>
                    <el-switch
                          v-model="autoAcceptOrders"
                         :style="{
                          '--el-switch-on-color': '#F9771C',
                           '--el-switch-off-color': '#E5E7EB'
                         }"
                         class="relative z-10"
                     @change="toggleAutoAcceptOrders"
                      />
                  </div>
                  <button
                    @click="addNewOrder"
                    class="bg-[#F9771C] text-white px-10 py-5 rounded-3xl hover:bg-[#E16A0E] transition-all duration-300 cursor-pointer whitespace-nowrap font-semibold shadow-2xl hover:shadow-3xl hover:scale-105 active:scale-95 relative overflow-hidden group"
                  >
                    <!-- 按钮内部装饰 -->
                    <div class="absolute inset-0 bg-gradient-to-r from-transparent via-white/20 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-700"></div>
                    <span class="relative z-10">模拟新订单</span>
                  </button>
                </div>
              </div>
            </div>
            
            <!-- 订单列表 -->
            <div class="bg-white/95 backdrop-blur-xl rounded-3xl shadow-2xl border border-gray-200/30 overflow-hidden relative">
              <!-- 表格装饰性元素 -->
              <div class="absolute top-0 left-0 w-full h-1 bg-gradient-to-r from-[#F9771C]/20 via-[#F9771C]/10 to-transparent"></div>
              <div class="absolute top-0 right-0 w-16 h-16 bg-gradient-to-bl from-[#F9771C]/5 to-transparent rounded-full -translate-y-8 translate-x-8"></div>
                            
              <el-table 
                :data="filteredOrders" 
                style="width: 100%" 
                class="custom-table relative z-10"
                v-loading="loading.orders"
                element-loading-text="加载订单中..."
                element-loading-spinner="el-icon-loading"
                element-loading-background="rgba(255, 255, 255, 0.8)"
              >
                <el-table-column prop="orderNo" label="订单号" width="180" />
                <el-table-column prop="payTime" label="支付时间" width="180" />
                <el-table-column prop="status" label="订单状态" width="120">
                  <template #default="scope">
                    <span
                      :class="{
                        'bg-orange-100 text-orange-600': scope.row.status === '未接单',
                        'bg-blue-100 text-blue-600': scope.row.status === '已接单',
                        'bg-purple-100 text-purple-600': scope.row.status === '已出餐',
                        'bg-green-100 text-green-600': scope.row.status === '配送中',
                        'bg-gray-100 text-gray-600': scope.row.status === '已送达'
                      }"
                      class="px-3 py-1 rounded-full text-xs font-medium"
                    >
                      {{ scope.row.status }}
                    </span>
                  </template>
                </el-table-column>
                <el-table-column label="菜品信息" min-width="200">
                  <template #default="scope">
                    <div class="space-y-1">
                      <div v-for="(dish, index) in scope.row.dishes" :key="index" class="text-sm">
                        <span>{{ dish.name }} x{{ dish.quantity }}</span>
                      </div>
                    </div>
                  </template>
                </el-table-column>
                <el-table-column prop="remark" label="备注" width="100" />
                <el-table-column label="用户信息" width="150">
                   <template #default="scope">
                     <div>
                       <div class="text-sm font-medium text-gray-900">{{ scope.row.userInfo?.name || '用户' }}</div>
                       <div class="text-xs text-gray-500">{{ scope.row.userInfo?.phone || '暂无电话' }}</div>
                     </div>
                   </template>
                 </el-table-column>
                 <el-table-column label="操作" width="300">
                  <template #default="scope">
                    <div class="flex flex-wrap gap-3">
                      <button
                        v-if="scope.row.status === '未接单'"
                        @click="updateOrderStatus(scope.row, '已接单')"
                        class="bg-[#F9771C] text-white px-4 py-2 rounded-2xl text-sm hover:bg-[#E16A0E] transition-all duration-300 cursor-pointer whitespace-nowrap font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 relative overflow-hidden group"
                      >
                        <div class="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-500"></div>
                        <span class="relative z-10">接单</span>
                      </button>
                      <button
                        v-if="scope.row.status === '已接单'"
                        @click="updateOrderStatus(scope.row, '已出餐')"
                        class="bg-purple-600 text-white px-4 py-2 rounded-2xl text-sm hover:bg-purple-700 transition-all duration-300 cursor-pointer whitespace-nowrap font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 relative overflow-hidden group"
                      >
                        <div class="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-500"></div>
                        <span class="relative z-10">出餐</span>
                      </button>
                      <button
                        v-if="scope.row.status === '已出餐'"
                        @click="publishDeliveryTask(scope.row)"
                        class="bg-green-600 text-white px-4 py-2 rounded-2xl text-sm hover:bg-green-700 transition-all duration-300 cursor-pointer whitespace-nowrap font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 relative overflow-hidden group"
                      >
                        <div class="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-500"></div>
                        <span class="relative z-10">发布配送</span>
                      </button>
                      <span v-if="scope.row.status === '配送中' || scope.row.status === '已送达'" class="text-gray-500 text-sm px-4 py-2.5 bg-gray-100/50 rounded-2xl backdrop-blur-sm">
                        {{ scope.row.status }}
                      </span>
                      
                      <!-- 详细信息和沟通按钮 -->
                      <button
                        @click="showOrderDetails(scope.row)"
                        class="bg-[#F9771C] text-white px-4 py-2 rounded-2xl text-xs hover:bg-[#E16A0E] transition-all duration-300 cursor-pointer font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 relative overflow-hidden group"
                      >
                        <div class="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-500"></div>
                        <span class="relative z-10">详细信息</span>
                      </button>
                      <button
                        @click="showUserChat(scope.row)"
                        class="bg-[#F9771C] text-white px-4 py-2 rounded-2xl text-xs hover:bg-[#E16A0E] transition-all duration-300 cursor-pointer font-semibold shadow-lg hover:shadow-xl hover:scale-105 active:scale-95 relative overflow-hidden group"
                      >
                        <div class="absolute inset-0 bg-gradient-to-r from-transparent via-white/10 to-transparent -translate-x-full group-hover:translate-x-full transition-transform duration-500"></div>
                        <span class="relative z-10">沟通</span>
                      </button>
                    </div>
                  </template>
                </el-table-column>
              </el-table>
            </div>
          </div>
          
          <!-- 菜品管理 -->
          <div v-else-if="activeOrderTab === 'dishes'">
            <div class="bg-white/90 backdrop-blur-md rounded-3xl p-8 mb-8 shadow-2xl border border-orange-200/30">
              <div class="flex justify-between items-center">
                <div class="flex space-x-4">
                  <button
                    v-for="status in dishStatuses"
                    :key="status.value"
                    @click="selectedDishStatus = status.value"
                    :class="{
                      'bg-[#F9771C] text-white shadow-2xl transform scale-105 ring-2 ring-[#F9771C]/20': selectedDishStatus === status.value,
                      'bg-white/80 text-gray-700 hover:bg-white hover:shadow-lg backdrop-blur-sm': selectedDishStatus !== status.value
                    }"
                    class="px-8 py-4 rounded-2xl transition-all duration-300 cursor-pointer whitespace-nowrap font-semibold text-sm"
                  >
                    {{ status.label }}
                  </button>
                </div>
                <button
                  @click="showDishForm = true"
                  class="bg-[#F9771C] text-white px-8 py-4 rounded-2xl hover:bg-[#E16A0E] transition-all duration-300 cursor-pointer whitespace-nowrap font-semibold shadow-2xl hover:shadow-3xl hover:scale-105 active:scale-95"
                >
                  新增菜品
                </button>
              </div>
            </div>
            
            <div class="bg-white/95 backdrop-blur-sm rounded-3xl shadow-2xl border border-gray-200/50 overflow-hidden">
              <el-table 
                :data="filteredDishes" 
                style="width: 100%" 
                class="custom-table"
                v-loading="loading.dishes"
                element-loading-text="加载菜品中..."
                element-loading-spinner="el-icon-loading"
                element-loading-background="rgba(255, 255, 255, 0.8)"
              >
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
                <el-table-column label="操作" width="200">
                  <template #default="scope">
                    <div class="flex space-x-2">
                      <button
                        @click="editDish(scope.row)"
                        class="bg-[#F9771C] hover:bg-[#E16A0E] text-white px-3 py-1 rounded text-sm transition-colors cursor-pointer whitespace-nowrap !rounded-button"
                      >
                        编辑
                      </button>
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
                    </div>
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
                    <div class="space-y-3">
                      <!-- 第一级选择：大菜系 -->
                      <div>
                        <select
                          v-model="selectedMainCategory"
                          @change="onMainCategoryChange"
                          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                        >
                          <option value="">请选择菜系</option>
                          <option value="中国菜系">中国菜系</option>
                          <option value="亚洲菜系">亚洲菜系</option>
                          <option value="欧洲菜系">欧洲菜系</option>
                          <option value="美洲菜系">美洲菜系</option>
                          <option value="非洲菜系">非洲菜系</option>
                          <option value="大洋洲菜系">大洋洲菜系</option>
                          <option value="特色分类">特色分类</option>
                        </select>
                      </div>
                      
                      <!-- 第二级选择：细分菜系 -->
                      <div v-if="selectedMainCategory && subCategories.length > 0">
                        <select
                          v-model="newDish.category"
                          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                        >
                          <option value="">请选择具体分类</option>
                          <option
                            v-for="category in subCategories"
                            :key="category"
                            :value="category"
                          >
                            {{ category }}
                          </option>
                        </select>
                      </div>
                    </div>
                  </div>
                  <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">菜品图片</label>
                    <div 
                      class="border-2 border-dashed border-gray-300 rounded-lg p-6 text-center hover:border-orange-400 transition-colors duration-200 cursor-pointer" 
                      @click="triggerImageUpload"
                      @dragover.prevent
                      @drop.prevent="handleDrop"
                      @dragenter.prevent
                      @dragleave.prevent
                    >
                      <div v-if="!newDish.imagePreview" class="space-y-2">
                        <i class="fas fa-cloud-upload-alt text-3xl text-gray-400"></i>
                        <p class="text-sm text-gray-600">点击或拖拽上传菜品图片</p>
                        <p class="text-xs text-gray-500">支持 JPG、PNG 格式，文件大小不超过 5MB</p>
                      </div>
                      <div v-else class="space-y-2">
                        <div class="relative inline-block">
                          <img :src="newDish.imagePreview" alt="菜品预览" class="w-32 h-32 object-cover rounded-lg" />
                          <button 
                            @click.stop="removeImage" 
                            class="absolute -top-2 -right-2 bg-red-500 text-white rounded-full w-6 h-6 flex items-center justify-center text-xs hover:bg-red-600 transition-colors"
                          >
                            ×
                          </button>
                        </div>
                        <p class="text-sm text-gray-600">点击重新上传</p>
                      </div>
                    </div>
                    <input 
                      ref="imageInput"
                      type="file" 
                      class="hidden" 
                      accept="image/*" 
                      @change="handleImageUpload"
                    />
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

            <!-- 编辑菜品弹窗 -->
            <div v-if="showEditForm" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
              <div class="bg-white rounded-lg p-6 w-[500px]">
                <h3 class="text-lg font-semibold text-gray-800 mb-4">编辑菜品</h3>
                <div class="space-y-4">
                  <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">菜品名称</label>
                    <input
                      v-model="editingDish.name"
                      class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                      placeholder="请输入菜品名称"
                    />
                  </div>
                  <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">价格</label>
                    <input
                      v-model="editingDish.price"
                      type="number"
                      class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                      placeholder="请输入价格"
                    />
                  </div>
                  <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">分类</label>
                    <div class="space-y-3">
                      <!-- 第一级选择：大菜系 -->
                      <div>
                        <select
                          v-model="selectedEditMainCategory"
                          @change="onEditMainCategoryChange"
                          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                        >
                          <option value="">请选择菜系</option>
                          <option value="中国菜系">中国菜系</option>
                          <option value="亚洲菜系">亚洲菜系</option>
                          <option value="欧洲菜系">欧洲菜系</option>
                          <option value="美洲菜系">美洲菜系</option>
                          <option value="非洲菜系">非洲菜系</option>
                          <option value="大洋洲菜系">大洋洲菜系</option>
                          <option value="特色分类">特色分类</option>
                        </select>
                      </div>
                      
                      <!-- 第二级选择：细分菜系 -->
                      <div v-if="selectedEditMainCategory && editSubCategories.length > 0">
                        <select
                          v-model="editingDish.category"
                          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
                        >
                          <option value="">请选择具体分类</option>
                          <option
                            v-for="category in editSubCategories"
                            :key="category"
                            :value="category"
                          >
                            {{ category }}
                          </option>
                        </select>
                      </div>
                    </div>
                  </div>
                  <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">菜品图片</label>
                    <div 
                      class="border-2 border-dashed border-gray-300 rounded-lg p-6 text-center hover:border-orange-400 transition-colors duration-200 cursor-pointer" 
                      @click="triggerEditImageUpload"
                      @dragover.prevent
                      @drop.prevent="handleEditDrop"
                      @dragenter.prevent
                      @dragleave.prevent
                    >
                      <div v-if="!editingDish.imagePreview" class="space-y-2">
                        <img :src="editingDish.image" alt="当前图片" class="w-32 h-32 object-cover rounded-lg mx-auto" />
                        <p class="text-sm text-gray-600">点击或拖拽上传新图片</p>
                        <p class="text-xs text-gray-500">支持 JPG、PNG 格式，文件大小不超过 5MB</p>
                      </div>
                      <div v-else class="space-y-2">
                        <div class="relative inline-block">
                          <img :src="editingDish.imagePreview" alt="菜品预览" class="w-32 h-32 object-cover rounded-lg" />
                          <button 
                            @click.stop="removeEditImage" 
                            class="absolute -top-2 -right-2 bg-red-500 text-white rounded-full w-6 h-6 flex items-center justify-center text-xs hover:bg-red-600 transition-colors"
                          >
                            ×
                          </button>
                        </div>
                        <p class="text-sm text-gray-600">点击重新上传</p>
                      </div>
                    </div>
                    <input 
                      ref="editImageInput"
                      type="file" 
                      class="hidden" 
                      accept="image/*" 
                      @change="handleEditImageUpload"
                    />
                  </div>
                  <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">描述</label>
                    <textarea
                      v-model="editingDish.description"
                      rows="3"
                      class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 text-sm resize-none"
                      placeholder="请输入菜品描述"
                    ></textarea>
                  </div>
                </div>
                <div class="flex justify-end space-x-3 mt-6">
                  <button
                    @click="showEditForm = false"
                    class="px-4 py-2 text-gray-600 border border-gray-300 rounded-md hover:bg-gray-50 transition-colors cursor-pointer whitespace-nowrap !rounded-button"
                  >
                    取消
                  </button>
                  <button
                    @click="updateDish"
                    class="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors cursor-pointer whitespace-nowrap !rounded-button"
                  >
                    保存
                  </button>
                </div>
              </div>
                         </div>
           </div>
         </div>
       </main>
     </div>
     
     <!-- 用户沟通对话框 -->
     <div v-if="showChatDialog" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50" @click="closeEmojiPanel">
       <div class="bg-white rounded-lg w-[600px] h-[500px] flex flex-col overflow-hidden" @click.stop>
         <!-- 对话框头部 -->
         <div class="flex items-center justify-between p-4 border-b border-gray-200 bg-[#FFF8F0]">
           <div class="flex items-center space-x-3">
             <div>
               <div class="font-medium text-gray-900">{{ currentChatUser?.name || '用户' }}</div>
               <div class="text-sm text-gray-500">{{ currentChatUser?.phone || '暂无电话' }}</div>
             </div>
           </div>
           <div class="flex items-center space-x-2">
             <!-- 拨打电话按钮 -->
             <button
               @click="callUser(currentChatUser?.phone)"
               class="text-gray-400 hover:text-[#F9771C] transition-colors"
               title="拨打电话"
             >
               <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                 <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z"></path>
               </svg>
             </button>
             <!-- 关闭按钮 -->
             <button
               @click="closeChatDialog"
               class="text-gray-400 hover:text-gray-600 transition-colors"
             >
               <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                 <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
               </svg>
             </button>
           </div>
         </div>
         
         <!-- 消息列表 -->
         <div class="flex-1 p-4 overflow-y-auto space-y-3" ref="chatMessagesRef">
           <div
             v-for="(message, index) in chatMessages"
             :key="index"
             :class="{
               'flex justify-end items-start space-x-2': message.sender === 'merchant',
               'flex justify-start items-start space-x-2': message.sender === 'user'
             }"
           >
             <!-- 用户头像 -->
             <div v-if="message.sender === 'user'" class="flex-shrink-0">
               <div class="w-8 h-8 bg-blue-500 rounded-full flex items-center justify-center text-white text-sm font-medium">
                 小
               </div>
             </div>
             
             <!-- 消息气泡 -->
             <div
               :class="{
                 'bg-[#F9771C] text-white': message.sender === 'merchant',
                 'bg-gray-100 text-gray-900': message.sender === 'user'
               }"
               class="max-w-xs px-3 py-2 rounded-lg text-sm"
             >
               <div class="mb-1 text-xs opacity-75">{{ message.time }}</div>
               <div>{{ message.content }}</div>
             </div>
             
             <!-- 商家头像 -->
             <div v-if="message.sender === 'merchant'" class="flex-shrink-0">
               <div class="w-8 h-8 bg-[#F9771C] rounded-full flex items-center justify-center text-white text-sm font-medium">
                 商
               </div>
             </div>
           </div>
         </div>
         
         <!-- 输入框 -->
         <div class="p-4 border-t border-gray-200 relative">
           <div class="flex space-x-2">
             <!-- 附件按钮 -->
             <button
               class="px-3 py-2 text-gray-400 hover:text-gray-600 transition-colors cursor-pointer"
               title="添加附件"
             >
               <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                 <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15.172 7l-6.586 6.586a2 2 0 102.828 2.828l6.414-6.586a4 4 0 00-5.656-5.656l-6.415 6.585a6 6 0 108.486 8.486L20.5 13"></path>
               </svg>
             </button>
             
             <!-- 表情按钮 -->
             <button
               @click="toggleEmojiPanel"
               class="px-3 py-2 text-gray-400 hover:text-[#F9771C] transition-colors cursor-pointer"
               title="选择表情"
             >
               <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                 <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M14.828 14.828a4 4 0 01-5.656 0M9 10h.01M15 10h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
               </svg>
             </button>
             
             <!-- 表情面板 -->
             <div v-if="showEmojiPanel" class="absolute bottom-16 left-0 bg-white border border-gray-200 rounded-lg shadow-lg p-2 z-10">
               <div class="grid grid-cols-8 gap-1 max-w-64">
                 <button
                   v-for="emoji in emojiList"
                   :key="emoji"
                   @click="insertEmoji(emoji)"
                   class="w-8 h-8 flex items-center justify-center text-lg hover:bg-gray-100 rounded cursor-pointer"
                   :title="emoji"
                 >
                   {{ emoji }}
                 </button>
               </div>
             </div>
             
             <input
               v-model="newMessage"
               @keyup.enter="sendMessage"
               type="text"
               placeholder="输入消息..."
               class="flex-1 px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm"
             />
             
             <!-- 发送按钮 -->
             <button
               @click="sendMessage"
               class="px-3 py-2 bg-gray-400 text-white rounded-md hover:bg-gray-500 transition-colors cursor-pointer"
             >
               <svg class="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                 <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 19l9 2-9-18-9 18 9-2zm0 0v-8"></path>
               </svg>
             </button>
           </div>
           
           <!-- 常用语按钮 -->
           <div class="px-4 pb-6 pt-4">
             <div class="flex space-x-2">
               <button
                 v-for="phrase in quickPhrases.slice(0, 4)"
                 :key="phrase"
                 @click="insertQuickPhrase(phrase)"
                 class="px-3 py-1 text-xs border border-[#F9771C] text-[#F9771C] rounded hover:bg-[#F9771C] hover:text-white transition-colors cursor-pointer"
               >
                 {{ phrase }}
               </button>
             </div>
           </div>
         </div>
       </div>
     </div>
     
     <!-- 订单详情对话框 -->
     <div v-if="showOrderDetailsDialog" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
       <div class="bg-white rounded-lg w-[600px] max-h-[80vh] flex flex-col overflow-hidden">
         <!-- 对话框头部 -->
         <div class="flex items-center justify-between p-4 border-b border-gray-200">
           <div>
             <div class="text-lg font-medium text-gray-900">订单详细信息</div>
             <div class="text-sm text-gray-500">订单号: {{ selectedOrder?.orderNo }}</div>
           </div>
           <div class="flex items-center space-x-2">
             <span
               :class="{
                 'bg-gray-100 text-gray-600': selectedOrder?.status === '未接单' || selectedOrder?.status === '已送达',
                 'bg-blue-100 text-blue-600': selectedOrder?.status === '已接单' || selectedOrder?.status === '已出餐',
                 'bg-green-100 text-green-600': selectedOrder?.status === '配送中'
               }"
               class="px-2 py-1 rounded-full text-xs font-medium"
             >
               {{ selectedOrder?.status }}
             </span>
             <button
               @click="closeOrderDetailsDialog"
               class="text-gray-400 hover:text-gray-600 transition-colors"
             >
               <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                 <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
               </svg>
             </button>
           </div>
         </div>
         
         <!-- 对话框内容 -->
         <div class="flex-1 p-4 overflow-y-auto space-y-4" v-loading="loading.orders" element-loading-text="加载订单详情中...">
           <!-- 客户信息 -->
           <div>
             <div class="text-sm font-medium text-gray-900 mb-2">客户信息</div>
             <div class="bg-gray-50 rounded-lg p-3">
               <div class="text-sm text-gray-700">客户姓名: {{ selectedOrder?.userInfo?.name }}</div>
               <div class="text-sm text-gray-700">联系电话: {{ selectedOrder?.userInfo?.phone }}</div>
             </div>
           </div>
           
           <!-- 订单信息 -->
           <div>
             <div class="text-sm font-medium text-gray-900 mb-2">订单信息</div>
             <div class="space-y-2">
               <div class="text-sm text-gray-700">下单时间: {{ selectedOrder?.payTime }}</div>
               <div class="text-sm text-gray-700">订单状态: 
                                    <span
                     :class="{
                       'bg-gray-100 text-gray-600': selectedOrder?.status === '未接单' || selectedOrder?.status === '已送达',
                       'bg-blue-100 text-blue-600': selectedOrder?.status === '已接单' || selectedOrder?.status === '已出餐',
                       'bg-green-100 text-green-600': selectedOrder?.status === '配送中'
                     }"
                     class="px-2 py-1 rounded-full text-xs font-medium"
                   >
                   {{ selectedOrder?.status }}
                 </span>
               </div>
               <div class="text-sm text-gray-700">送货地址: {{ selectedOrder?.deliveryAddress || '北京市海淀区中关村大街1号科技大厦A座801室' }}</div>
             </div>
           </div>
           
           <!-- 商品明细 -->
           <div>
             <div class="text-sm font-medium text-gray-900 mb-2">商品明细</div>
             <div class="border border-gray-200 rounded-lg overflow-hidden">
               <table class="w-full text-sm">
                 <thead class="bg-gray-50">
                   <tr>
                     <th class="px-3 py-2 text-left text-gray-700">商品名称</th>
                     <th class="px-3 py-2 text-left text-gray-700">规格</th>
                     <th class="px-3 py-2 text-left text-gray-700">数量</th>
                     <th class="px-3 py-2 text-left text-gray-700">单价</th>
                     <th class="px-3 py-2 text-left text-gray-700">小计</th>
                   </tr>
                 </thead>
                 <tbody>
                   <tr v-for="(dish, index) in selectedOrder?.dishes" :key="index" class="border-t border-gray-200">
                     <td class="px-3 py-2">{{ dish.name }}</td>
                     <td class="px-3 py-2">微辣,加青菜,加粉条</td>
                     <td class="px-3 py-2">{{ dish.quantity }}</td>
                     <td class="px-3 py-2">¥{{ dish.price }}</td>
                     <td class="px-3 py-2">¥{{ dish.price * dish.quantity }}</td>
                   </tr>
                 </tbody>
               </table>
             </div>
           </div>
           
           <!-- 费用明细 -->
           <div>
             <div class="text-sm font-medium text-gray-900 mb-2">费用明细</div>
             <div class="bg-[#FFF8F0] rounded-lg p-3">
               <div class="flex justify-between text-sm mb-1">
                 <span>商品原价:</span>
                 <span>¥{{ selectedOrder?.fees?.originalPrice || selectedOrder?.dishes?.reduce((sum: number, dish: any) => sum + dish.price * dish.quantity, 0) }}</span>
               </div>
               <div class="flex justify-between text-sm mb-1">
                 <span>打包费:</span>
                 <span>¥{{ selectedOrder?.fees?.packingFee || 2 }}</span>
               </div>
               <div class="flex justify-between text-sm mb-1">
                 <span>配送费:</span>
                 <span>¥{{ selectedOrder?.fees?.deliveryFee || 5 }}</span>
               </div>
               <div v-if="selectedOrder?.fees?.couponName" class="flex justify-between text-sm mb-1">
                 <span>使用优惠券:</span>
                 <span class="bg-[#F9771C] text-white px-2 py-1 rounded text-xs">{{ selectedOrder.fees.couponName }}</span>
               </div>
               <div v-if="selectedOrder?.fees?.couponDiscount" class="flex justify-between text-sm mb-1">
                 <span>优惠金额:</span>
                 <span class="text-green-600">-¥{{ selectedOrder.fees.couponDiscount }}</span>
               </div>
               <div class="flex justify-between text-sm font-medium">
                 <span>实付金额:</span>
                 <span class="text-[#F9771C] text-lg">¥{{ selectedOrder?.fees?.finalPrice || 58 }}</span>
               </div>
             </div>
           </div>
         </div>
         
         <!-- 对话框底部 -->
         <div class="p-4 border-t border-gray-200 flex justify-end space-x-2">
           <button
             @click="closeOrderDetailsDialog"
             class="px-4 py-2 border border-gray-300 text-gray-700 rounded-md hover:bg-gray-50 transition-colors"
           >
             关闭
           </button>
           <button
             class="px-4 py-2 bg-gray-800 text-white rounded-md hover:bg-gray-700 transition-colors"
           >
             打印订单
           </button>
         </div>
       </div>
     </div>
   </div>
 </template>

<script lang="ts" setup>
import { ref, computed, watch, onMounted } from 'vue';
import { Bell, House, List, Ticket, Warning, User } from '@element-plus/icons-vue';
import { ElMessage, } from 'element-plus';
import { useRouter, useRoute } from 'vue-router';

// 订单详情类型定义
interface OrderDetail {
  orderNo: string;
  payTime: string;
  status: string;
  remark: string;
  userInfo: {
    name: string;
    phone: string;
    avatar?: string;
  };
  dishes: Array<{
    name: string;
    price: number;
    quantity: number;
  }>;
  deliveryAddress: string;
  fees: {
    originalPrice: number;
    packingFee: number;
    deliveryFee: number;
    couponDiscount: number;
    finalPrice: number;
    couponName?: string;
  };
}

import {
  getOrders,
  getOrderDetail,
  updateOrderStatus as updateOrderStatusApi,
  publishDeliveryTask as publishDeliveryTaskApi,
  getAutoAcceptStatus,
  toggleAutoAcceptOrders as toggleAutoAcceptOrdersApi,
  addNewOrder as addNewOrderApi,
  getDishes,
  createDish as createDishApi,
  updateDish as updateDishApi,
  toggleDishStatus as toggleDishStatusApi,
  getDishCategories,
  getChatMessages,
  sendMessage as sendMessageApi,
  callUser as callUserApi,
  uploadDishImage,
  handleApiError
} from '@/services/merchant_api';

const activeMenu = ref('orders');
const router = useRouter();
const $route = useRoute();

const menuItems = [
  { key: 'overview', label: '店铺概况', icon: House, routeName: 'MerchantHome' },
  { key: 'orders', label: '订单中心', icon: List, routeName: 'MerchantOrders' },
  { key: 'coupons', label: '配券中心', icon: Ticket, routeName: 'MerchantCoupons' },
  { key: 'aftersale', label: '订单售后', icon: Warning, routeName: 'MerchantAftersale' },
  { key: 'profile', label: '商家信息', icon: User, routeName: 'MerchantProfile' }
] as const;

const handleMenuClick = (menuItem: typeof menuItems[number]) => {
  router.push({ name: menuItem.routeName });
};

// 数据加载状态
const loading = ref({
  orders: false,
  dishes: false,
  chat: false,
  autoAccept: false
});

// 错误处理
const errorMessage = ref('');

const clearError = () => {
  errorMessage.value = '';
};

const retryLoad = async () => {
  errorMessage.value = '';
  await Promise.all([
    loadOrders(),
    loadDishes()
  ]);
};

const autoAcceptOrders = ref(false);

// 初始化数据
onMounted(async () => {
  await Promise.all([
    loadOrders(),
    loadDishes(),
    loadAutoAcceptStatus(),
    loadCategories()
  ]);
});

// 加载订单数据
const loadOrders = async () => {
  try {
    loading.value.orders = true;
    const apiOrders = await getOrders({ 
      status: selectedOrderStatus.value === 'all' ? undefined : selectedOrderStatus.value as any 
    });
    // 合并API数据和本地测试数据，处理类型兼容性
    const mergedOrders = [...orders.value, ...apiOrders.map(order => ({
      ...order,
      userInfo: {
        ...order.userInfo,
        avatar: order.userInfo.avatar || ''
      }
    }))];
    orders.value = mergedOrders as any;
  } catch (error) {
    console.error('加载订单失败:', error);
    const errorMsg = handleApiError(error);
    ElMessage.error(errorMsg);
    errorMessage.value = `加载订单失败: ${errorMsg}`;
  } finally {
    loading.value.orders = false;
  }
};

// 加载菜品数据
const loadDishes = async () => {
  try {
    loading.value.dishes = true;
    const apiDishes = await getDishes({ 
      status: selectedDishStatus.value === 'all' ? undefined : selectedDishStatus.value as any 
    });
    // 合并API数据和本地测试数据
    dishes.value = [...dishes.value, ...apiDishes];
  } catch (error) {
    console.error('加载菜品失败:', error);
    const errorMsg = handleApiError(error);
    ElMessage.error(errorMsg);
    errorMessage.value = `加载菜品失败: ${errorMsg}`;
  } finally {
    loading.value.dishes = false;
  }
};

// 加载自动接单状态
const loadAutoAcceptStatus = async () => {
  try {
    const status = await getAutoAcceptStatus();
    autoAcceptOrders.value = status.autoAcceptOrders;
  } catch (error) {
    console.error('加载自动接单状态失败:', error);
    // 保持默认值，不显示错误消息
  }
};

// 加载菜品分类
const loadCategories = async () => {
  try {
    const apiCategories = await getDishCategories();
    // 合并API分类和本地分类
    Object.assign(categoryMap, apiCategories);
  } catch (error) {
    console.error('加载菜品分类失败:', error);
    // 保持本地分类，不显示错误消息
  }
};

const toggleAutoAcceptOrders = async (value: string | number | boolean) => {
  const boolValue = Boolean(value);
  
  try {
    loading.value.autoAccept = true;
    await toggleAutoAcceptOrdersApi(boolValue);
    
    // 如果开启自动接单，将所有未接单的订单自动变为已接单
    if (boolValue) {
      orders.value.forEach(order => {
        if (order.status === '未接单') {
          order.status = '已接单';
        }
      });
    }
    
    ElMessage({
      type: 'success',
      message: `已${boolValue ? '开启' : '关闭'}自动接单${boolValue ? '，未接单订单已自动接单' : ''}`
    });
  } catch (error) {
    console.error('切换自动接单状态失败:', error);
    ElMessage.error(handleApiError(error));
    // 恢复原状态
    autoAcceptOrders.value = !boolValue;
  } finally {
    loading.value.autoAccept = false;
  }
};

// const merchantInfo = ref({
//   username: 'zhanglaosan',
// });

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
const showEditForm = ref(false);

// 树状分类相关数据
const selectedMainCategory = ref('');
const subCategories = ref<string[]>([]);

// 编辑相关数据
const selectedEditMainCategory = ref('');
const editSubCategories = ref<string[]>([]);
const editingDish = ref({
  id: 0,
  name: '',
  price: '',
  category: '',
  description: '',
  status: '上架中',
  image: '',
  imagePreview: ''
});

// 分类数据映射
const categoryMap = {
  '中国菜系': ['川菜', '粤菜', '湘菜', '鲁菜', '苏菜', '浙菜', '闽菜', '徽菜', '东北菜', '西北菜', '新疆菜', '云南菜', '贵州菜', '广西菜', '海南菜', '台湾菜', '香港菜', '澳门菜'],
  '亚洲菜系': ['日本料理', '韩国料理', '泰国菜', '越南菜', '新加坡菜', '马来西亚菜', '印度菜', '巴基斯坦菜', '斯里兰卡菜', '尼泊尔菜', '孟加拉菜', '菲律宾菜', '印度尼西亚菜', '缅甸菜', '老挝菜', '柬埔寨菜', '蒙古菜', '哈萨克斯坦菜', '乌兹别克斯坦菜', '吉尔吉斯斯坦菜', '塔吉克斯坦菜', '土库曼斯坦菜', '阿富汗菜', '伊朗菜', '伊拉克菜', '叙利亚菜', '黎巴嫩菜', '约旦菜', '以色列菜', '巴勒斯坦菜', '沙特阿拉伯菜', '阿联酋菜', '卡塔尔菜', '科威特菜', '巴林菜', '阿曼菜', '也门菜', '土耳其菜', '格鲁吉亚菜', '亚美尼亚菜', '阿塞拜疆菜', '塞浦路斯菜'],
  '欧洲菜系': ['法国菜', '意大利菜', '西班牙菜', '德国菜', '英国菜', '爱尔兰菜', '荷兰菜', '比利时菜', '瑞士菜', '奥地利菜', '匈牙利菜', '捷克菜', '斯洛伐克菜', '波兰菜', '立陶宛菜', '拉脱维亚菜', '爱沙尼亚菜', '芬兰菜', '瑞典菜', '挪威菜', '丹麦菜', '冰岛菜', '俄罗斯菜', '乌克兰菜', '白俄罗斯菜', '摩尔多瓦菜', '罗马尼亚菜', '保加利亚菜', '塞尔维亚菜', '克罗地亚菜', '斯洛文尼亚菜', '波斯尼亚菜', '黑山菜', '北马其顿菜', '阿尔巴尼亚菜', '希腊菜', '马耳他菜', '葡萄牙菜'],
  '美洲菜系': ['美国菜', '加拿大菜', '墨西哥菜', '古巴菜', '牙买加菜', '海地菜', '多米尼加菜', '波多黎各菜', '巴哈马菜', '巴巴多斯菜', '特立尼达和多巴哥菜', '巴西菜', '阿根廷菜', '智利菜', '秘鲁菜', '哥伦比亚菜', '委内瑞拉菜', '厄瓜多尔菜', '玻利维亚菜', '巴拉圭菜', '乌拉圭菜', '圭亚那菜', '苏里南菜', '法属圭亚那菜'],
  '非洲菜系': ['埃及菜', '利比亚菜', '突尼斯菜', '阿尔及利亚菜', '摩洛哥菜', '苏丹菜', '南苏丹菜', '埃塞俄比亚菜', '厄立特里亚菜', '吉布提菜', '索马里菜', '肯尼亚菜', '乌干达菜', '坦桑尼亚菜', '卢旺达菜', '布隆迪菜', '刚果民主共和国菜', '刚果共和国菜', '加蓬菜', '赤道几内亚菜', '圣多美和普林西比菜', '喀麦隆菜', '中非共和国菜', '乍得菜', '尼日尔菜', '尼日利亚菜', '贝宁菜', '多哥菜', '加纳菜', '科特迪瓦菜', '利比里亚菜', '塞拉利昂菜', '几内亚菜', '几内亚比绍菜', '塞内加尔菜', '冈比亚菜', '毛里塔尼亚菜', '马里菜', '布基纳法索菜', '佛得角菜', '安哥拉菜', '赞比亚菜', '津巴布韦菜', '马拉维菜', '莫桑比克菜', '博茨瓦纳菜', '纳米比亚菜', '南非菜', '莱索托菜', '斯威士兰菜', '马达加斯加菜', '毛里求斯菜', '塞舌尔菜', '科摩罗菜'],
  '大洋洲菜系': ['澳大利亚菜', '新西兰菜', '巴布亚新几内亚菜', '斐济菜', '所罗门群岛菜', '瓦努阿图菜', '新喀里多尼亚菜', '萨摩亚菜', '汤加菜', '基里巴斯菜', '图瓦卢菜', '瑙鲁菜', '帕劳菜', '密克罗尼西亚菜', '马绍尔群岛菜'],
  '特色分类': ['特色小吃', '甜品', '饮品', '汤品', '沙拉', '烧烤', '火锅', '素食', '清真', '有机', '无麸质', '低卡路里', '高蛋白', '儿童餐', '老人餐', '孕妇餐', '病号餐', '节日特色', '季节限定', '招牌菜', '新品推荐', '限时特价']
};

// 处理主分类变化
const onMainCategoryChange = () => {
  newDish.value.category = '';
  if (selectedMainCategory.value && categoryMap[selectedMainCategory.value as keyof typeof categoryMap]) {
    subCategories.value = categoryMap[selectedMainCategory.value as keyof typeof categoryMap];
  } else {
    subCategories.value = [];
  }
};

const newDish = ref({
  name: '',
  price: '',
  category: '',
  description: '',
  status: '上架中',
  imagePreview: ''
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

const toggleDishStatus = async (dish: any) => {
  try {
    const newStatus = dish.status === '上架中' ? '已下架' : '上架中';
    await toggleDishStatusApi(dish.id, newStatus);
    dish.status = newStatus;
    ElMessage.success(`菜品已${newStatus === '上架中' ? '上架' : '下架'}`);
  } catch (error) {
    console.error('切换菜品状态失败:', error);
    ElMessage.error(handleApiError(error));
  }
};

const imageInput = ref<HTMLInputElement>();

const triggerImageUpload = () => {
  imageInput.value?.click();
};

const handleImageUpload = (event: Event) => {
  const target = event.target as HTMLInputElement;
  const file = target.files?.[0];
  
  if (file) {
    processImageFile(file);
  }
};

const handleDrop = (event: DragEvent) => {
  const files = event.dataTransfer?.files;
  if (files && files.length > 0) {
    const file = files[0];
    processImageFile(file);
  }
};

const processImageFile = (file: File) => {
  // 检查文件大小（5MB限制）
  if (file.size > 5 * 1024 * 1024) {
    ElMessage.error('文件大小不能超过5MB');
    return;
  }
  
  // 检查文件类型
  if (!file.type.startsWith('image/')) {
    ElMessage.error('请选择图片文件');
    return;
  }
  
  // 创建预览URL
  const reader = new FileReader();
  reader.onload = (e) => {
    newDish.value.imagePreview = e.target?.result as string;
  };
  reader.readAsDataURL(file);
};

const removeImage = () => {
  newDish.value.imagePreview = '';
  if (imageInput.value) {
    imageInput.value.value = '';
  }
};

// 编辑相关函数
const editDish = (dish: any) => {
  editingDish.value = {
    id: dish.id,
    name: dish.name,
    price: dish.price.toString(),
    category: dish.category,
    description: dish.description,
    status: dish.status,
    image: dish.image,
    imagePreview: ''
  };
  
  // 根据当前分类设置主分类
  for (const [mainCategory, subCats] of Object.entries(categoryMap)) {
    if (subCats.includes(dish.category)) {
      selectedEditMainCategory.value = mainCategory;
      editSubCategories.value = subCats;
      break;
    }
  }
  
  showEditForm.value = true;
};

const onEditMainCategoryChange = () => {
  editingDish.value.category = '';
  if (selectedEditMainCategory.value && categoryMap[selectedEditMainCategory.value as keyof typeof categoryMap]) {
    editSubCategories.value = categoryMap[selectedEditMainCategory.value as keyof typeof categoryMap];
  } else {
    editSubCategories.value = [];
  }
};

const editImageInput = ref<HTMLInputElement>();

const triggerEditImageUpload = () => {
  editImageInput.value?.click();
};

const handleEditImageUpload = (event: Event) => {
  const target = event.target as HTMLInputElement;
  const file = target.files?.[0];
  
  if (file) {
    processEditImageFile(file);
  }
};

const handleEditDrop = (event: DragEvent) => {
  const files = event.dataTransfer?.files;
  if (files && files.length > 0) {
    const file = files[0];
    processEditImageFile(file);
  }
};

const processEditImageFile = (file: File) => {
  // 检查文件大小（5MB限制）
  if (file.size > 5 * 1024 * 1024) {
    ElMessage.error('文件大小不能超过5MB');
    return;
  }
  
  // 检查文件类型
  if (!file.type.startsWith('image/')) {
    ElMessage.error('请选择图片文件');
    return;
  }
  
  // 创建预览URL
  const reader = new FileReader();
  reader.onload = (e) => {
    editingDish.value.imagePreview = e.target?.result as string;
  };
  reader.readAsDataURL(file);
};

const removeEditImage = () => {
  editingDish.value.imagePreview = '';
  if (editImageInput.value) {
    editImageInput.value.value = '';
  }
};

const updateDish = async () => {
  try {
    // 验证必填字段
    if (!editingDish.value.name || !editingDish.value.price || !editingDish.value.category) {
      ElMessage.error('请填写完整的菜品信息');
      return;
    }

    // 如果有新的图片预览，先上传图片
    let imageUrl = editingDish.value.image;
    if (editingDish.value.imagePreview && editingDish.value.imagePreview.startsWith('data:')) {
      // 将base64转换为File对象
      const response = await fetch(editingDish.value.imagePreview);
      const blob = await response.blob();
      const file = new File([blob], 'dish-image.jpg', { type: 'image/jpeg' });
      const uploadResult = await uploadDishImage(file);
      imageUrl = uploadResult.imageUrl;
    }

    const updateData = {
      name: editingDish.value.name,
      price: Number(editingDish.value.price),
      category: editingDish.value.category,
      description: editingDish.value.description,
      image: imageUrl
    };

    await updateDishApi(editingDish.value.id, updateData);
    
    // 更新本地数据
    const dishIndex = dishes.value.findIndex(dish => dish.id === editingDish.value.id);
    if (dishIndex !== -1) {
      dishes.value[dishIndex] = {
        ...dishes.value[dishIndex],
        ...updateData
      };
    }
    
    showEditForm.value = false;
    editingDish.value = {
      id: 0,
      name: '',
      price: '',
      category: '',
      description: '',
      status: '上架中',
      image: '',
      imagePreview: ''
    };
    // 重置编辑分类选择
    selectedEditMainCategory.value = '';
    editSubCategories.value = [];
    
    ElMessage.success('菜品更新成功');
  } catch (error) {
    console.error('更新菜品失败:', error);
    ElMessage.error(handleApiError(error));
  }
};

const createDish = async () => {
  try {
    // 验证必填字段
    if (!newDish.value.name || !newDish.value.price || !newDish.value.category) {
      ElMessage.error('请填写完整的菜品信息');
      return;
    }

    // 如果有图片预览，先上传图片
    let imageUrl = newDish.value.imagePreview;
    if (newDish.value.imagePreview && newDish.value.imagePreview.startsWith('data:')) {
      // 将base64转换为File对象
      const response = await fetch(newDish.value.imagePreview);
      const blob = await response.blob();
      const file = new File([blob], 'dish-image.jpg', { type: 'image/jpeg' });
      const uploadResult = await uploadDishImage(file);
      imageUrl = uploadResult.imageUrl;
    }

    const newDishData = {
      name: newDish.value.name,
      price: newDish.value.price,
      category: newDish.value.category,
      description: newDish.value.description,
      imagePreview: imageUrl
    };

    const createdDish = await createDishApi(newDishData);
    dishes.value.push(createdDish);
    
    showDishForm.value = false;
    newDish.value = {
      name: '',
      price: '',
      category: '',
      description: '',
      status: '上架中',
      imagePreview: ''
    };
    // 重置分类选择
    selectedMainCategory.value = '';
    subCategories.value = [];
    
    ElMessage.success('菜品创建成功');
  } catch (error) {
    console.error('创建菜品失败:', error);
    ElMessage.error(handleApiError(error));
  }
};

const selectedOrderStatus = ref('all');

// 聊天相关变量
const showChatDialog = ref(false);
const currentChatUser = ref<any>(null);
const chatMessages = ref<any[]>([]);
const newMessage = ref('');
const chatMessagesRef = ref<HTMLDivElement>();

// 表情相关变量
const showEmojiPanel = ref(false);
const emojiList = [
  '😀', '😃', '😄', '😁', '😆', '😅', '😂', '🤣',
  '😊', '😇', '🙂', '🙃', '😉', '😌', '😍', '🥰',
  '😘', '😗', '😙', '😚', '😋', '😛', '😝', '😜',
  '🤪', '🤨', '🧐', '🤓', '😎', '🤩', '🥳', '😏',
  '😒', '😞', '😔', '😟', '😕', '🙁', '☹️', '😣',
  '😖', '😫', '😩', '🥺', '😢', '😭', '😤', '😠',
  '😡', '🤬', '🤯', '😳', '🥵', '🥶', '😱', '😨',
  '😰', '😥', '😓', '🤗', '🤔', '🤭', '🤫', '🤥',
  '😶', '😐', '😑', '😯', '😦', '😧', '😮', '😲',
  '🥱', '😴', '🤤', '😪', '😵', '🤐', '🥴', '🤢',
  '🤮', '🤧', '😷', '🤒', '🤕', '🤑', '🤠', '💩',
  '👍', '👎', '👌', '✌️', '🤞', '🤟', '🤘', '🤙',
  '👈', '👉', '👆', '🖕', '👇', '☝️', '👋', '🤚',
  '🖐️', '✋', '🖖', '👌', '🤌', '🤏', '✌️', '🤞',
  '🤟', '🤘', '🤙', '👈', '👉', '👆', '🖕', '👇'
];

// 常用语数组
const quickPhrases = [
  '订单已接收，正在为您准备',
  '亲，正在制作中，请耐心等待',
  '您的订单已完成，请及时取餐',
  '感谢您的订单，我们会尽快为您准备',
  '如有任何问题，请随时联系我们',
  '您的订单正在配送中，请保持电话畅通',
  '抱歉让您久等了，我们会加快速度',
  '您的订单已出餐，请到店取餐',
  '配送员已出发，请注意接听电话',
  '感谢您的耐心等待，祝您用餐愉快'
];

const orders = ref([
  {
    orderNo: 'ORD20241201001',
    payTime: '2024-12-01 12:30:00',
    status: '未接单',
    remark: '不要辣椒',
    userInfo: {
      name: '张三',
      phone: '13800138001',
      avatar: 'https://readdy.ai/api/search-image?query=young%20Chinese%20man%20portrait%20with%20friendly%20smile%20professional%20headshot&width=32&height=32&seq=user-001&orientation=squarish'
    },
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
    userInfo: {
      name: '李四',
      phone: '13900139002',
      avatar: 'https://readdy.ai/api/search-image?query=young%20Chinese%20woman%20portrait%20with%20friendly%20smile%20professional%20headshot&width=32&height=32&seq=user-002&orientation=squarish'
    },
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
    userInfo: {
      name: '王五',
      phone: '13700137003',
      avatar: 'https://readdy.ai/api/search-image?query=middle%20aged%20Chinese%20man%20portrait%20with%20friendly%20smile%20professional%20headshot&width=32&height=32&seq=user-003&orientation=squarish'
    },
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


const updateOrderStatus = async (order: any, newStatus: string) => {
  try {
    await updateOrderStatusApi(order.orderNo, newStatus as any);
    order.status = newStatus;
    ElMessage.success(`订单状态已更新为${newStatus}`);
  } catch (error) {
    console.error('更新订单状态失败:', error);
    ElMessage.error(handleApiError(error));
  }
};

const publishDeliveryTask = async (order: any) => {
  try {
    await publishDeliveryTaskApi(order.orderNo);
    order.status = '配送中';
    ElMessage.success('配送任务已发布');
  } catch (error) {
    console.error('发布配送任务失败:', error);
    ElMessage.error(handleApiError(error));
  }
};

// 监听自动接单状态，当开启时自动处理新订单
watch(autoAcceptOrders, (newValue) => {
  if (newValue) {
    // 自动接单开启时，将所有未接单订单变为已接单
    orders.value.forEach(order => {
      if (order.status === '未接单') {
        order.status = '已接单';
      }
    });
  }
});

// 监听订单状态筛选变化
watch(selectedOrderStatus, async (newStatus) => {
  if (newStatus) {
    await loadOrders();
  }
});

// 监听菜品状态筛选变化
watch(selectedDishStatus, async (newStatus) => {
  if (newStatus) {
    await loadDishes();
  }
});

// 聊天相关函数
const showUserChat = async (order: any) => {
  currentChatUser.value = order.userInfo;
  showChatDialog.value = true;
  
  try {
    loading.value.chat = true;
    // 从API获取聊天记录
    const apiMessages = await getChatMessages(order.orderNo);
    chatMessages.value = apiMessages;
    
    // 如果没有聊天记录，添加初始消息
    if (chatMessages.value.length === 0) {
      chatMessages.value = [
        {
          sender: 'user',
          content: `您好，我是${order.userInfo.name}，我的订单号是${order.orderNo}`,
          time: new Date().toLocaleTimeString()
        },
        {
          sender: 'merchant',
          content: '您好！感谢您的订单，我们会尽快为您准备。',
          time: new Date().toLocaleTimeString()
        }
      ];
    }
  } catch (error) {
    console.error('加载聊天记录失败:', error);
    ElMessage.error(handleApiError(error));
    // 使用默认聊天记录
    chatMessages.value = [
      {
        sender: 'user',
        content: `您好，我是${order.userInfo.name}，我的订单号是${order.orderNo}`,
        time: new Date().toLocaleTimeString()
      },
      {
        sender: 'merchant',
        content: '您好！感谢您的订单，我们会尽快为您准备。',
        time: new Date().toLocaleTimeString()
      }
    ];
  } finally {
    loading.value.chat = false;
  }
  
  // 滚动到底部
  setTimeout(() => {
    if (chatMessagesRef.value) {
      chatMessagesRef.value.scrollTop = chatMessagesRef.value.scrollHeight;
    }
  }, 100);
};

const closeChatDialog = () => {
  showChatDialog.value = false;
  currentChatUser.value = null;
  chatMessages.value = [];
  newMessage.value = '';
};

const sendMessage = async () => {
  if (!newMessage.value.trim()) return;
  
  try {
    // 获取当前订单号（从聊天用户信息中推断）
    const currentOrder = orders.value.find(order => 
      order.userInfo.name === currentChatUser.value?.name
    );
    
    if (!currentOrder) {
      ElMessage.error('无法找到对应订单');
      return;
    }
    
    // 发送消息到API
    const sentMessage = await sendMessageApi(currentOrder.orderNo, newMessage.value);
    
    // 添加到本地聊天记录
    chatMessages.value.push(sentMessage);
    
    newMessage.value = '';
    
    // 滚动到底部
    setTimeout(() => {
      if (chatMessagesRef.value) {
        chatMessagesRef.value.scrollTop = chatMessagesRef.value.scrollHeight;
      }
    }, 100);
  } catch (error) {
    console.error('发送消息失败:', error);
    ElMessage.error(handleApiError(error));
  }
};

const callUser = async (phone: string) => {
  try {
    await callUserApi(phone);
    ElMessage.success(`正在拨打 ${phone}`);
  } catch (error) {
    console.error('拨打电话失败:', error);
    ElMessage.error(handleApiError(error));
  }
};

// 订单详情相关变量
const showOrderDetailsDialog = ref(false);
const selectedOrder = ref<OrderDetail | null>(null);

// 订单详情相关函数
const showOrderDetails = async (order: any) => {
  try {
    loading.value.orders = true;
    // 从API获取完整的订单详情
    const orderDetail = await getOrderDetail(order.orderNo);
    selectedOrder.value = orderDetail;
    showOrderDetailsDialog.value = true;
  } catch (error) {
    console.error('获取订单详情失败:', error);
    const errorMsg = handleApiError(error);
    ElMessage.error(errorMsg);
    errorMessage.value = `获取订单详情失败: ${errorMsg}`;
    // 如果API失败，使用本地订单数据并添加默认费用信息
    const localOrderWithFees = {
      ...order,
      deliveryAddress: '北京市海淀区中关村大街1号科技大厦A座801室',
      fees: {
        originalPrice: order.dishes.reduce((sum: number, dish: any) => sum + dish.price * dish.quantity, 0),
        packingFee: 2,
        deliveryFee: 5,
        couponDiscount: 17,
        finalPrice: order.dishes.reduce((sum: number, dish: any) => sum + dish.price * dish.quantity, 0) + 2 + 5 - 17,
        couponName: '新用户优惠券'
      }
    };
    selectedOrder.value = localOrderWithFees as any;
    showOrderDetailsDialog.value = true;
  } finally {
    loading.value.orders = false;
  }
};

const closeOrderDetailsDialog = () => {
  showOrderDetailsDialog.value = false;
  selectedOrder.value = null;
};

// 表情相关函数
const toggleEmojiPanel = () => {
  showEmojiPanel.value = !showEmojiPanel.value;
};

const insertEmoji = (emoji: string) => {
  newMessage.value += emoji;
  showEmojiPanel.value = false;
};

const insertQuickPhrase = (phrase: string) => {
  newMessage.value = phrase;
  showEmojiPanel.value = false;
};

// 点击外部关闭表情面板
const closeEmojiPanel = () => {
  showEmojiPanel.value = false;
};

// 添加新订单功能（用于测试自动接单）
const addNewOrder = async () => {
  try {
    const newOrder = await addNewOrderApi();
    // 处理类型兼容性
    const processedOrder = {
      ...newOrder,
      userInfo: {
        ...newOrder.userInfo,
        avatar: newOrder.userInfo.avatar || ''
      }
    };
    orders.value.unshift(processedOrder as any);
    
    // 如果自动接单开启，立即将新订单变为已接单
    if (autoAcceptOrders.value) {
      processedOrder.status = '已接单';
      ElMessage.success('新订单已自动接单');
    } else {
      ElMessage.success('新订单已添加');
    }
  } catch (error) {
    console.error('添加新订单失败:', error);
    ElMessage.error(handleApiError(error));
  }
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

/* 自定义表格样式 - 苹果风格 */
.custom-table :deep(.el-table) {
  background: transparent !important;
  border: none !important;
  position: relative !important;
  z-index: 10 !important;
}

.custom-table :deep(.el-table__header) {
  background: rgba(255, 255, 255, 0.8) !important;
  backdrop-filter: blur(10px) !important;
  border-bottom: 1px solid rgba(249, 119, 28, 0.1) !important;
  position: relative !important;
  z-index: 10 !important;
}

.custom-table :deep(.el-table__header th) {
  background: transparent !important;
  border: none !important;
  color: #374151 !important;
  font-weight: 600 !important;
  font-size: 0.875rem !important;
  padding: 1rem 0.75rem !important;
}
.custom-table :deep(.el-table__body tr) {
  background: rgba(255, 255, 255, 0.6) !important;
  backdrop-filter: blur(8px) !important;
  border: none !important;
  transition: all 0.2s ease !important;
  position: relative !important;
  z-index: 10 !important;
}

.custom-table :deep(.el-table__body tr:hover) {
  background: rgba(255, 255, 255, 0.8) !important;
  backdrop-filter: blur(12px) !important;
  transform: translateY(-1px) !important;
  box-shadow: 0 4px 12px rgba(249, 119, 28, 0.1) !important;
}

.custom-table :deep(.el-table__body td) {
  border: none !important;
  padding: 1rem 0.75rem !important;
  color: #374151 !important;
  background: transparent !important;
}

/* 状态标签优化 */
.custom-table :deep(.px-3.py-1.rounded-full) {
  backdrop-filter: blur(8px) !important;
  border: 1px solid rgba(255, 255, 255, 0.2) !important;
}
</style>