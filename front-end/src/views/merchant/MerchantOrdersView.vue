/* eslint-disable */
<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->

<template>
  <div class="min-h-screen bg-gray-50">
    <header class="fixed top-0 left-0 right-0 bg-white shadow-sm z-50 h-16">
      <div class="flex items-center justify-between h-full px-6">
        <div class="flex items-center">
          <h1 class="text-xl font-bold text-[#F9771C]">FoodDelivery Pro</h1>
        </div>
        <div class="flex items-center space-x-4">
          <el-icon class="text-gray-600 text-xl cursor-pointer"><Bell /></el-icon>
          <div class="flex items-center space-x-2">
            <span class="text-gray-700 font-medium">商家中心</span>
          </div>
        </div>
      </div>
    </header>

    <div class="flex pt-16">
      <aside class="fixed left-0 top-16 bottom-0 w-52 bg-white shadow-sm overflow-y-auto z-50">
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

      <main class="ml-52 flex-1 p-6">
        <h2 class="text-2xl font-bold text-gray-800 mb-6">订单中心</h2>

        <div v-if="errorMessage" class="mb-4 p-4 bg-red-50 border border-red-200 rounded-lg">
          <div class="flex items-center justify-between">
            <div class="flex items-center">
              <svg class="w-5 h-5 text-red-400 mr-2" fill="currentColor" viewBox="0 0 20 20">
                <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clip-rule="evenodd"></path>
              </svg>
              <span class="text-red-800">{{ errorMessage }}</span>
            </div>
            <div class="flex items-center space-x-2">
              <button @click="retryLoad" class="btn-error">重试</button>
              <button @click="clearError" class="btn-icon text-red-400 hover:text-red-600">
                <svg class="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
                </svg>
              </button>
            </div>
          </div>
        </div>

        <div class="bg-white rounded-lg shadow-sm p-4 mb-6">
          <div class="flex space-x-4">
            <button
              v-for="tab in orderTabs"
              :key="tab.value"
              @click="activeOrderTab = tab.value"
              :class="{
                'tab-button active': activeOrderTab === tab.value,
                'tab-button': activeOrderTab !== tab.value
              }"
              class="whitespace-nowrap"
            >
              <span>{{ tab.label }}</span>
            </button>
          </div>
        </div>

        <div v-if="activeOrderTab === 'orders'">
          <div class="bg-white/95 backdrop-blur-xl rounded-3xl shadow-2xl border border-gray-200/30 overflow-hidden relative z-10 transform transition-all duration-300 hover:shadow-3xl hover:scale-[1.01]">
            <div class="absolute top-0 left-0 w-full h-1 bg-gradient-to-r from-[#F9771C] via-[#FF8C42] to-transparent"></div>

            <!-- 工具栏：自动接单开关 -->
            <div class="p-6 flex items-center justify-end gap-4">
              <div class="flex items-center gap-3 bg-gradient-to-r from-orange-50 to-yellow-50 px-6 py-3 rounded-2xl border border-orange-200/50 shadow-lg backdrop-blur-sm">
                <span class="text-sm font-medium text-orange-800">自动接单</span>
                <el-switch
                  v-model="autoAcceptOrders"
                  @change="(v:any)=>onAutoAcceptChange(Boolean(v))"
                  :style="{ '--el-switch-on-color': '#F9771C', '--el-switch-off-color': '#E5E7EB' }"
                />
              </div>
            </div>

            <el-table 
              :data="orders" 
              style="width: 100%" 
              class="custom-table relative z-10"
              v-loading="loading.orders"
              element-loading-text="加载订单中..."
              element-loading-spinner="el-icon-loading"
              element-loading-background="rgba(255, 255, 255, 0.8)"
            >
              <el-table-column prop="orderId" label="订单ID" width="120" align="center" />
              <el-table-column prop="paymentTime" label="支付时间" width="160" />
              <el-table-column prop="customerId" label="客户ID" width="100" align="center" />
              <el-table-column prop="storeId" label="门店ID" width="100" align="center" />
              <el-table-column prop="sellerId" label="商家ID" width="100" align="center" />
              <el-table-column prop="localStatus" label="接单状态" width="120" align="center">
                <template #default="scope">
                  <span
                    :class="{
                      'bg-green-100 text-green-600': scope.row.localStatus === 'accepted',
                      'bg-red-100 text-red-600': scope.row.localStatus === 'rejected',
                      'bg-gray-100 text-gray-600': !scope.row.localStatus
                    }"
                    class="px-3 py-1 rounded-full text-xs font-medium"
                  >
                    {{ scope.row.localStatus === 'accepted' ? '已接单' : scope.row.localStatus === 'rejected' ? '已拒单' : '待处理' }}
                  </span>
                </template>
              </el-table-column>
              <el-table-column prop="remarks" label="备注" min-width="200" />
              <el-table-column label="操作" min-width="520">
                <template #default="scope">
                  <div class="flex flex-wrap items-center gap-2">
                    <button
                      @click="showOrderDetails(scope.row)"
                      class="btn-primary btn-small shrink-0"
                    >
                      详细信息
                    </button>
                    <button
                      v-if="!scope.row.localStatus || scope.row.localStatus === 'rejected'"
                      @click="acceptOrder(scope.row)"
                      class="btn-success btn-small shrink-0"
                    >
                      接单
                    </button>
                    <button
                      v-if="!scope.row.localStatus || scope.row.localStatus === 'accepted'"
                      @click="rejectOrder(scope.row)"
                      class="btn-danger btn-small shrink-0"
                    >
                      拒单
                    </button>
                    <button
                      v-if="!scope.row.deliveryStatus || scope.row.deliveryStatus === 'none'"
                      @click="openPublishDialog(scope.row)"
                      class="btn-info btn-small shrink-0"
                    >
                      发布配送
                    </button>
                    <button
                      v-else-if="scope.row.deliveryStatus === 'published'"
                      @click="openDeliveryInfo(scope.row)"
                      class="btn-warning btn-small shrink-0"
                    >
                      骑手已接单
                    </button>
                    <button
                      v-if="scope.row.deliveryStatus === 'published'"
                      @click="openDeliveryInfo(scope.row)"
                      class="btn-secondary btn-small shrink-0"
                    >
                      骑手信息
                    </button>
                  </div>
                </template>
              </el-table-column>
            </el-table>
          </div>
        </div>

        <div v-else-if="activeOrderTab === 'dishes'">
          <div class="bg-gradient-to-r from-orange-50 to-yellow-50 backdrop-blur-md rounded-3xl p-6 mb-6 shadow-2xl border border-orange-200/30 flex items-center justify-between relative z-10 transform transition-all duration-300 hover:shadow-3xl">
            <div class="text-sm font-medium text-orange-800">管理菜品</div>
            <button @click="showDishForm = true" class="btn-primary btn-medium">
              新增菜品
            </button>
          </div>

          <div class="bg-white/95 backdrop-blur-sm rounded-3xl shadow-2xl border border-gray-200/50 overflow-hidden relative z-10 transform transition-all duration-300 hover:shadow-3xl">
            <el-table 
              :data="dishes" 
              style="width: 100%" 
              class="custom-table"
              v-loading="loading.dishes"
              element-loading-text="加载菜品中..."
              element-loading-spinner="el-icon-loading"
              element-loading-background="rgba(255, 255, 255, 0.8)"
            >
              <el-table-column prop="dishId" label="菜品ID" width="120" />
              <el-table-column prop="dishName" label="菜品名称" width="200" />
              <el-table-column prop="price" label="价格" width="120">
                <template #default="scope">¥{{ scope.row.price }}</template>
              </el-table-column>
              <el-table-column prop="description" label="描述" />
              <el-table-column prop="isSoldOut" label="状态" width="120">
                <template #default="scope">
                  <span :class="{ 'text-gray-500': scope.row.isSoldOut === 0, 'text-green-600': scope.row.isSoldOut === 2 }" class="font-medium">
                    {{ scope.row.isSoldOut === 0 ? '售罄' : '在售' }}
                  </span>
                </template>
              </el-table-column>
              <el-table-column label="操作" width="200">
                <template #default="scope">
                  <div class="flex space-x-2">
                    <button @click="editDish(scope.row)" class="btn-primary btn-small">编辑</button>
                    <button @click="toggleSoldOut(scope.row)" class="btn-small" :class="{ 'btn-danger': scope.row.isSoldOut === 2, 'btn-success': scope.row.isSoldOut === 0 }">
                      {{ scope.row.isSoldOut === 2 ? '设为售罄' : '设为在售' }}
                    </button>
                  </div>
                </template>
              </el-table-column>
            </el-table>
          </div>

          <!-- 新增菜品弹窗 -->
          <div v-if="showDishForm" class="fixed inset-0 bg-black/60 backdrop-blur-sm flex items-center justify-center z-50">
            <div class="bg-white rounded-2xl p-8 w-[500px] shadow-2xl border border-gray-100 transform transition-all duration-300 scale-100">
              <h3 class="text-xl font-bold text-gray-800 mb-6 text-center">新增菜品</h3>
              <div class="space-y-4">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">菜品名称</label>
                  <input v-model="newDish.dishName" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm transition-all duration-200 bg-gray-50 hover:bg-white" placeholder="请输入菜品名称" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">价格</label>
                  <input v-model="newDish.price" type="number" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm transition-all duration-200 bg-gray-50 hover:bg-white" placeholder="请输入价格" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">描述</label>
                  <textarea v-model="newDish.description" rows="3" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm resize-none transition-all duration-200 bg-gray-50 hover:bg-white" placeholder="请输入菜品描述"></textarea>
                </div>
                <div class="flex items-center space-x-2">
                  <input id="newSoldOut" type="checkbox" v-model="newDish.isSoldOut" true-value="0" false-value="2" />
                  <label for="newSoldOut" class="text-sm text-gray-700">售罄</label>
                </div>
              </div>
              <div class="flex justify-end space-x-3 mt-6">
                <button @click="showDishForm = false" class="btn-outline btn-medium">取消</button>
                <button @click="createDishHandler" class="btn-info btn-medium">创建</button>
              </div>
            </div>
          </div>

          <!-- 编辑菜品弹窗 -->
          <div v-if="showEditForm" class="fixed inset-0 bg-black/60 backdrop-blur-sm flex items-center justify-center z-50">
            <div class="bg-white rounded-2xl p-8 w-[500px] shadow-2xl border border-gray-100 transform transition-all duration-300 scale-100">
              <h3 class="text-xl font-bold text-gray-800 mb-6 text-center">编辑菜品</h3>
              <div class="space-y-4">
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">菜品名称</label>
                  <input v-model="editingDish.dishName" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm transition-all duration-200 bg-gray-50 hover:bg-white" placeholder="请输入菜品名称" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">价格</label>
                  <input v-model="editingDish.price" type="number" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm transition-all duration-200 bg-gray-50 hover:bg-white" placeholder="请输入价格" />
                </div>
                <div>
                  <label class="block text-sm font-medium text-gray-700 mb-2">描述</label>
                  <textarea v-model="editingDish.description" rows="3" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm resize-none transition-all duration-200 bg-gray-50 hover:bg-white" placeholder="请输入菜品描述"></textarea>
                </div>
                <div class="flex items-center space-x-2">
                  <input id="editSoldOut" type="checkbox" v-model="editingDish.isSoldOut" true-value="0" false-value="2" />
                  <label for="editSoldOut" class="text-sm text-gray-700">售罄</label>
                </div>
              </div>
              <div class="flex justify-end space-x-4 mt-8">
                <button @click="showEditForm = false" class="btn-outline btn-medium">取消</button>
                <button @click="updateDishHandler" class="btn-info btn-medium">保存</button>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>

    <!-- 订单详情对话框 -->
    <div v-if="showOrderDetailsDialog" class="fixed inset-0 bg-black/60 backdrop-blur-sm flex items-center justify-center z-50">
      <div class="bg-white rounded-2xl w-[720px] max-h-[80vh] flex flex-col overflow-hidden shadow-2xl border border-gray-100 transform transition-all duration-300 scale-100">
        <div class="flex items-center justify-between p-6 border-b border-gray-200 bg-gradient-to-r from-orange-50 to-yellow-50">
          <div>
            <div class="text-xl font-bold text-gray-900">订单详细信息</div>
            <div class="text-sm text-orange-600 font-medium">订单ID: {{ selectedOrder?.orderId }}</div>
          </div>
          <button @click="closeOrderDetailsDialog" class="btn-icon text-gray-400 hover:text-gray-600">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
            </svg>
          </button>
        </div>

        <div class="flex-1 p-4 overflow-y-auto space-y-4" v-loading="loading.orders" element-loading-text="加载订单详情中...">
          <div class="grid grid-cols-2 gap-4">
            <div class="bg-gray-50 rounded-lg p-3">
              <div class="text-sm text-gray-700">支付时间: {{ selectedOrder?.paymentTime }}</div>
              <div class="text-sm text-gray-700">备注: {{ selectedOrder?.remarks || '-' }}</div>
            </div>
            <div class="bg-gray-50 rounded-lg p-3">
              <div class="text-sm text-gray-700">客户ID: {{ selectedOrder?.customerId }}</div>
              <div class="text-sm text-gray-700">门店ID: {{ selectedOrder?.storeId }}，商家ID: {{ selectedOrder?.sellerId }}</div>
            </div>
          </div>

          <!-- 优惠券信息 -->
          <div class="bg-gray-50 rounded-lg p-3">
            <div class="text-sm font-medium text-gray-900 mb-2">优惠券信息</div>
            <div v-if="orderCoupons.length > 0" class="space-y-2">
              <div v-for="coupon in orderCoupons" :key="coupon.couponId" class="flex items-center justify-between p-2 bg-white rounded border">
                <div class="flex-1">
                  <div class="text-sm font-medium text-gray-800">{{ coupon.couponName }}</div>
                  <div class="text-xs text-gray-600">{{ coupon.description }}</div>
                  <div class="text-xs text-gray-500">有效期: {{ coupon.validFrom }} - {{ coupon.validTo }}</div>
                </div>
                <div class="text-right">
                  <div class="text-sm font-bold text-red-600">
                    <span v-if="coupon.discountType === 'percentage'">-{{ coupon.discountValue }}%</span>
                    <span v-else>-¥{{ coupon.discountValue }}</span>
                  </div>
                  <div class="text-xs text-gray-500">已使用</div>
                </div>
              </div>
            </div>
            <div v-else class="text-sm text-gray-500 text-center py-2">
              未使用优惠券
            </div>
          </div>

          <div>
            <div class="text-sm font-medium text-gray-900 mb-2">商品明细（来自购物车条目）</div>
            <div class="border border-gray-200 rounded-lg overflow-hidden">
              <table class="w-full text-sm">
                <thead class="bg-gray-50">
                  <tr>
                    <th class="px-3 py-2 text-left text-gray-700">菜品名称</th>
                    <th class="px-3 py-2 text-left text-gray-700">数量</th>
                    <th class="px-3 py-2 text-left text-gray-700">单价</th>
                    <th class="px-3 py-2 text-left text-gray-700">小计</th>
                  </tr>
                </thead>
                <tbody>
                                     <tr v-for="it in orderItems" :key="it.itemId" class="border-t border-gray-200">
                     <td class="px-3 py-2">{{ it.dish?.dishName || '菜品信息加载中...' }}</td>
                     <td class="px-3 py-2">{{ it.quantity }}</td>
                     <td class="px-3 py-2">¥{{ it.dish?.price || 0 }}</td>
                     <td class="px-3 py-2">¥{{ it.totalPrice }}</td>
                   </tr>
                </tbody>
              </table>
            </div>
            <div class="text-right mt-2 text-sm text-gray-700">总计：¥{{ orderTotal }}</div>
          </div>
        </div>

        <div class="p-4 border-t border-gray-200 flex justify-end">
          <button @click="closeOrderDetailsDialog" class="btn-outline btn-medium">关闭</button>
        </div>
      </div>
    </div>

    <!-- 发布配送任务对话框 -->
    <div v-if="showPublishDialog" class="fixed inset-0 bg-black/60 backdrop-blur-sm flex items-center justify-center z-50">
      <div class="bg-white rounded-2xl w-[520px] p-8 shadow-2xl border border-gray-100 transform transition-all duration-300 scale-100">
        <div class="text-xl font-bold text-gray-800 mb-6 text-center">发布配送任务</div>
        <div class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">预计到店时间</label>
            <input v-model="publishForm.estimatedArrivalTime" type="datetime-local" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm transition-all duration-200 bg-gray-50 hover:bg-white" />
          </div>
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2">预计送达时间</label>
            <input v-model="publishForm.estimatedDeliveryTime" type="datetime-local" class="w-full px-4 py-3 border border-gray-300 rounded-xl focus:outline-none focus:ring-2 focus:ring-[#F9771C] focus:border-[#F9771C] text-sm transition-all duration-200 bg-gray-50 hover:bg-white" />
          </div>
        </div>
        <div class="flex justify-end space-x-4 mt-8">
          <button @click="closePublishDialog" class="btn-outline btn-medium">取消</button>
          <button @click="submitPublish" class="btn-primary btn-medium">发布</button>
        </div>
      </div>
    </div>

    <!-- 配送信息对话框（展示骑手信息） -->
    <div v-if="showDeliveryInfoDialog" class="fixed inset-0 bg-black/60 backdrop-blur-sm flex items-center justify-center z-50">
      <div class="bg-white rounded-2xl w-[640px] p-8 max-h-[80vh] overflow-auto shadow-2xl border border-gray-100 transform transition-all duration-300 scale-100">
        <div class="flex items-center justify-between mb-6">
          <div class="text-xl font-bold text-gray-800">配送与骑手信息</div>
          <button @click="closeDeliveryInfoDialog" class="btn-icon text-gray-400 hover:text-gray-600">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" /></svg>
          </button>
        </div>
        <div v-if="deliveryInfo.deliveryTask" class="mb-3 text-sm text-gray-700">
          <div>任务ID：{{ deliveryInfo.deliveryTask?.taskId }}</div>
          <div>预计到店：{{ deliveryInfo.deliveryTask?.estimatedArrivalTime }}</div>
          <div>预计送达：{{ deliveryInfo.deliveryTask?.estimatedDeliveryTime }}</div>
        </div>
        <div v-if="deliveryInfo.publish" class="mb-3 text-sm text-gray-700">
          <div>发布者商家ID：{{ deliveryInfo.publish?.sellerId }}</div>
          <div>发布时间：{{ deliveryInfo.publish?.publishTime }}</div>
        </div>
        <div v-if="deliveryInfo.accept" class="mb-3 text-sm text-gray-700">
          <div>接单骑手ID：{{ deliveryInfo.accept?.courierId }}</div>
          <div>接单时间：{{ deliveryInfo.accept?.acceptTime }}</div>
        </div>
        <div v-if="deliveryInfo.courier" class="mb-3 text-sm text-gray-700">
          <div>骑手姓名：{{ deliveryInfo.courier?.fullName || '—' }}（ID：{{ deliveryInfo.courier?.userId }}）</div>
          <div>电话：{{ deliveryInfo.courier?.phoneNumber || '—' }}</div>
          <div>车型：{{ deliveryInfo.courier?.vehicleType }}，信誉：{{ deliveryInfo.courier?.reputationPoints }}，总配送：{{ deliveryInfo.courier?.totalDeliveries }}</div>
          <div>平均时长：{{ deliveryInfo.courier?.avgDeliveryTime }} 分，评分：{{ deliveryInfo.courier?.averageRating }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue';
import { Bell, House, List, Ticket, Warning, User } from '@element-plus/icons-vue';
import { ElMessage } from 'element-plus';
import { useRouter, useRoute } from 'vue-router';

import {
  getOrders,
  getCartItems,
  getDishes,
  createDish as createDishApi,
  updateDish as updateDishApi,
  toggleDishSoldOut,
  handleApiError,
  publishDeliveryTaskForOrder,
  getOrderDeliveryInfo,
  acceptOrder as acceptOrderApi,
  rejectOrder as rejectOrderApi,
  getOrderCoupons,
  type OrderDeliveryInfo,
  type FoodOrder,
  type ShoppingCartItem,
  type Dish,
  type OrderCouponInfo
} from '@/api/merchant_api';

// 本地示例数据（与数据库字段对齐）
const localOrdersSample: (FoodOrder & { localStatus?: string; deliveryStatus?: string })[] = [
  { orderId: 1001, paymentTime: '2024-12-01 12:30:00', remarks: '不要辣椒', customerId: 1, cartId: 10, storeId: 101, sellerId: 201, localStatus: 'accepted', deliveryStatus: 'published' },
  { orderId: 1002, paymentTime: '2024-12-01 13:15:00', remarks: '多加米饭', customerId: 2, cartId: 11, storeId: 101, sellerId: 201, localStatus: 'accepted', deliveryStatus: 'published' },
  { orderId: 1003, paymentTime: '2024-12-01 14:00:00', remarks: '打包带走', customerId: 3, cartId: 12, storeId: 102, sellerId: 202, localStatus: 'accepted', deliveryStatus: 'none' }
];

const localDishesSample: Dish[] = [
  { dishId: 1, dishName: '麻婆豆腐', price: 28, description: '特制豆瓣酱，麻辣鲜香', isSoldOut: 2 },
  { dishId: 2, dishName: '宫保鸡丁', price: 32, description: '鸡丁花生，鲜辣爽口', isSoldOut: 2 },
  { dishId: 3, dishName: '水煮鱼', price: 58, description: '草鱼片，辣而不燥', isSoldOut: 0 }
];

const localCartItemsByCartId: Record<number, ShoppingCartItem[]> = {
  10: [
    { itemId: 10001, quantity: 1, totalPrice: 28, dishId: 1, cartId: 10, dish: localDishesSample[0] },
    { itemId: 10002, quantity: 2, totalPrice: 64, dishId: 2, cartId: 10, dish: localDishesSample[1] }
  ],
  11: [
    { itemId: 11001, quantity: 1, totalPrice: 58, dishId: 3, cartId: 11, dish: localDishesSample[2] }
  ],
  12: [
    { itemId: 12001, quantity: 2, totalPrice: 56, dishId: 1, cartId: 12, dish: localDishesSample[0] }
  ]
};

// 本地示例：订单优惠券信息（作为API不可用时的回退）
const localOrderCouponsByOrderId: Record<number, OrderCouponInfo[]> = {
  1001: [
    {
      couponId: 1,
      couponName: '新用户专享券',
      description: '新用户首次下单专享优惠',
      discountType: 'percentage',
      discountValue: 10,
      validFrom: '2024-12-01',
      validTo: '2024-12-31',
      isUsed: true
    }
  ],
  1002: [
    {
      couponId: 2,
      couponName: '满减优惠券',
      description: '满50减10元',
      discountType: 'fixed',
      discountValue: 10,
      validFrom: '2024-12-01',
      validTo: '2024-12-31',
      isUsed: true
    }
  ],
  1003: [] // 第三个订单没有使用优惠券
};

// 本地样例：骑手与配送任务信息（用于无后端时的演示）
const localCourierSamples = [
  {
    userId: 3001,
    courierRegistrationTime: '2024-01-01T00:00:00Z',
    vehicleType: '电动车',
    reputationPoints: 96,
    totalDeliveries: 1280,
    avgDeliveryTime: 28,
    averageRating: 4.8,
    monthlySalary: 6800,
    fullName: '王强',
    phoneNumber: 13800000001,
  },
  {
    userId: 3002,
    courierRegistrationTime: '2024-01-15T00:00:00Z',
    vehicleType: '摩托车',
    reputationPoints: 90,
    totalDeliveries: 980,
    avgDeliveryTime: 31,
    averageRating: 4.6,
    monthlySalary: 6200,
    fullName: '李敏',
    phoneNumber: 13800000002,
  },
];

const localDeliveryInfoByOrderId: Record<number, OrderDeliveryInfo> = {
  // 为前两个订单添加骑手信息（已发布配送的订单）
  1001: {
    deliveryTask: {
      taskId: 5001,
      estimatedArrivalTime: '2024-12-01 12:40:00',
      estimatedDeliveryTime: '2024-12-01 13:00:00',
      customerId: 1,
      storeId: 101,
    },
    publish: {
      sellerId: 201,
      deliveryTaskId: 5001,
      publishTime: '2024-12-01 12:35:00',
    },
    accept: {
      courierId: 3001,
      deliveryTaskId: 5001,
      acceptTime: '2024-12-01 12:36:00',
    },
    courier: localCourierSamples[0],
  },
  1002: {
    deliveryTask: {
      taskId: 5002,
      estimatedArrivalTime: '2024-12-01 13:25:00',
      estimatedDeliveryTime: '2024-12-01 13:45:00',
      customerId: 2,
      storeId: 101,
    },
    publish: {
      sellerId: 201,
      deliveryTaskId: 5002,
      publishTime: '2024-12-01 13:20:00',
    },
    accept: {
      courierId: 3002,
      deliveryTaskId: 5002,
      acceptTime: '2024-12-01 13:21:00',
    },
    courier: localCourierSamples[1],
  },
};

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
const loading = ref({ orders: false, dishes: false });

// 错误处理
const errorMessage = ref('');

const clearError = () => {
  errorMessage.value = '';
};

const retryLoad = async () => {
  errorMessage.value = '';
  await Promise.all([loadOrders(), loadDishes()]);
};

// 初始化数据
onMounted(async () => {
  await Promise.all([loadOrders(), loadDishes()]);
});

// 加载订单数据
const loadOrders = async () => {
  try {
    loading.value.orders = true;
    const apiOrders = await getOrders();
    if (apiOrders && (apiOrders as any).length > 0) {
      // 为API返回的订单添加本地状态字段
      orders.value = (apiOrders as any).map((order: FoodOrder) => ({
        ...order,
        localStatus: 'accepted', // 默认已接单
        deliveryStatus: 'none'   // 默认未发布配送
      }));
    }
  } catch (error) {
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
    const apiDishes = await getDishes();
    if (apiDishes && (apiDishes as any).length > 0) {
      dishes.value = apiDishes as any;
    }
  } catch (error) {
    const errorMsg = handleApiError(error);
    ElMessage.error(errorMsg);
    errorMessage.value = `加载菜品失败: ${errorMsg}`;
  } finally {
    loading.value.dishes = false;
  }
};

// 对齐数据库，移除自动接单与菜品分类逻辑

// const merchantInfo = ref({
//   username: 'zhanglaosan',
// });

const orderTabs = [
  { value: 'orders', label: '订单管理' },
  { value: 'dishes', label: '菜品管理' }
];

const activeOrderTab = ref('orders');
const showDishForm = ref(false);
const showEditForm = ref(false);
const orders = ref<(FoodOrder & { localStatus?: string; deliveryStatus?: string })[]>(localOrdersSample);
// 前端本地接单状态与自动接单开关（仅前端态，数据库未定义订单状态）
const autoAcceptOrders = ref(false);

const showOrderDetailsDialog = ref(false);
const selectedOrder = ref<FoodOrder | null>(null);
const orderItems = ref<ShoppingCartItem[]>([]);
const orderCoupons = ref<OrderCouponInfo[]>([]);
const orderTotal = computed(() => orderItems.value.reduce((sum, it) => sum + Number(it.totalPrice || 0), 0));

const showOrderDetails = async (order: FoodOrder) => {
  try {
    loading.value.orders = true;
    selectedOrder.value = order;
    orderItems.value = await getCartItems(order.cartId);
    // 加载订单优惠券信息
    orderCoupons.value = await getOrderCoupons(order.orderId);
    showOrderDetailsDialog.value = true;
  } catch (error) {
    const errorMsg = handleApiError(error);
    ElMessage.error(errorMsg);
    // 使用本地购物车条目样例作为回退
    orderItems.value = localCartItemsByCartId[order.cartId] ?? [];
    // 使用本地优惠券样例作为回退
    orderCoupons.value = localOrderCouponsByOrderId[order.orderId] ?? [];
    showOrderDetailsDialog.value = true;
  } finally {
    loading.value.orders = false;
  }
};

const closeOrderDetailsDialog = () => {
  showOrderDetailsDialog.value = false;
  selectedOrder.value = null;
  orderItems.value = [];
  orderCoupons.value = [];
};

// 菜品管理
const dishes = ref<Dish[]>(localDishesSample);

const newDish = ref({ dishName: '', price: '', description: '', isSoldOut: 2 as any });
const editingDish = ref({ dishId: 0, dishName: '', price: 0 as any, description: '', isSoldOut: 0 as any });

const createDishHandler = async () => {
  try {
    if (!newDish.value.dishName || newDish.value.price === '' || !newDish.value.description) {
      ElMessage.error('请填写完整的菜品信息');
      return;
    }
    const created = await createDishApi({
      dishName: newDish.value.dishName,
      price: newDish.value.price,
      description: newDish.value.description,
      isSoldOut: Number(newDish.value.isSoldOut) || 0,
    });
    dishes.value.push(created as any);
    showDishForm.value = false;
    newDish.value = { dishName: '', price: '', description: '', isSoldOut: 2 } as any;
    ElMessage.success('菜品创建成功');
  } catch (error) {
    ElMessage.error(handleApiError(error));
  }
};

const editDish = (dish: any) => {
  editingDish.value = { ...dish };
  showEditForm.value = true;
};

const updateDishHandler = async () => {
  try {
    if (!editingDish.value.dishId) return;
    const updated = await updateDishApi(editingDish.value.dishId, {
      dishName: editingDish.value.dishName,
      price: Number(editingDish.value.price),
      description: editingDish.value.description,
      isSoldOut: Number(editingDish.value.isSoldOut) || 0,
    });
    const idx = dishes.value.findIndex(d => d.dishId === updated.dishId);
    if (idx !== -1) dishes.value[idx] = updated as any;
    showEditForm.value = false;
    ElMessage.success('菜品更新成功');
  } catch (error) {
    ElMessage.error(handleApiError(error));
  }
};

const toggleSoldOut = async (dish: any) => {
  try {
    const newValue = dish.isSoldOut === 0 ? 2 : 0;
    await toggleDishSoldOut(dish.dishId, newValue);
    dish.isSoldOut = newValue;
    ElMessage.success('状态更新成功');
  } catch (error) {
    ElMessage.error(handleApiError(error));
  }
};

// 自动接单与接/拒处理（前端本地态）
const onAutoAcceptChange = (val: boolean) => {
  if (val) {
    orders.value.forEach((o: any) => {
      if (!o.localStatus || o.localStatus === 'rejected') {
        o.localStatus = 'accepted';
      }
    });
  }
};

const acceptOrder = async (order: any) => {
  try {
    await acceptOrderApi(order.orderId);
    order.localStatus = 'accepted';
    ElMessage.success('已接单');
  } catch (error) {
    ElMessage.error(handleApiError(error));
  }
};

const rejectOrder = async (order: any) => {
  try {
    await rejectOrderApi(order.orderId);
    order.localStatus = 'rejected';
    ElMessage.success('已拒单');
  } catch (error) {
    ElMessage.error(handleApiError(error));
  }
};

// 发布配送任务
const showPublishDialog = ref(false);
const publishTargetOrder = ref<FoodOrder | null>(null);
const publishForm = ref({ estimatedArrivalTime: '', estimatedDeliveryTime: '' });

const openPublishDialog = (order: FoodOrder) => {
  publishTargetOrder.value = order;
  // 默认时间：当前时间+10/30分钟
  const now = new Date();
  const pad = (n: number) => (n < 10 ? `0${n}` : `${n}`);
  const toLocal = (d: Date) => `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}T${pad(d.getHours())}:${pad(d.getMinutes())}`;
  const eta = new Date(now.getTime() + 10 * 60 * 1000);
  const etd = new Date(now.getTime() + 30 * 60 * 1000);
  publishForm.value.estimatedArrivalTime = toLocal(eta);
  publishForm.value.estimatedDeliveryTime = toLocal(etd);
  showPublishDialog.value = true;
};

const closePublishDialog = () => {
  showPublishDialog.value = false;
  publishTargetOrder.value = null;
};

const submitPublish = async () => {
  if (!publishTargetOrder.value) return;
  try {
    // 优先调用后端
    try {
      await publishDeliveryTaskForOrder(publishTargetOrder.value.orderId, {
        estimatedArrivalTime: publishForm.value.estimatedArrivalTime,
        estimatedDeliveryTime: publishForm.value.estimatedDeliveryTime,
      });
    } catch (_) {
      // 后端不可用时，构造本地配送信息样例
      const orderId = publishTargetOrder.value.orderId;
      const courier = localCourierSamples[(orderId % localCourierSamples.length)];
      const eta = publishForm.value.estimatedArrivalTime;
      const etd = publishForm.value.estimatedDeliveryTime;
      localDeliveryInfoByOrderId[orderId] = {
        deliveryTask: {
          taskId: 5000 + orderId,
          estimatedArrivalTime: eta,
          estimatedDeliveryTime: etd,
          customerId: publishTargetOrder.value.customerId,
          storeId: publishTargetOrder.value.storeId,
        },
        publish: {
          sellerId: publishTargetOrder.value.sellerId,
          deliveryTaskId: 5000 + orderId,
          publishTime: new Date().toISOString(),
        },
        accept: {
          courierId: courier.userId,
          deliveryTaskId: 5000 + orderId,
          acceptTime: new Date().toISOString(),
        },
        courier,
      } as OrderDeliveryInfo;
    }
    
    // 更新订单的配送状态为已发布
    if (publishTargetOrder.value) {
      const orderIndex = orders.value.findIndex(o => o.orderId === publishTargetOrder.value!.orderId);
      if (orderIndex !== -1) {
        orders.value[orderIndex].deliveryStatus = 'published';
      }
    }
    
    ElMessage.success('配送任务已发布');
    showPublishDialog.value = false;
    // 发布后立即加载配送与骑手信息
    await openDeliveryInfo(publishTargetOrder.value);
  } catch (error) {
    ElMessage.error(handleApiError(error));
  }
};

// 配送与骑手信息展示
const showDeliveryInfoDialog = ref(false);
const deliveryInfo = ref<OrderDeliveryInfo>({});

const openDeliveryInfo = async (order: FoodOrder) => {
  try {
    const local = localDeliveryInfoByOrderId[order.orderId];
    if (local) {
      deliveryInfo.value = local;
    } else {
      deliveryInfo.value = await getOrderDeliveryInfo(order.orderId);
    }
    showDeliveryInfoDialog.value = true;
  } catch (error) {
    ElMessage.error(handleApiError(error));
  }
};

const closeDeliveryInfoDialog = () => {
  showDeliveryInfoDialog.value = false;
  deliveryInfo.value = {} as any;
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