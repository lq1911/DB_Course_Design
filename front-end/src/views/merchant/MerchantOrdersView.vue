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
      </main>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue';
import { Bell, House, List, Ticket, Warning, User } from '@element-plus/icons-vue';
import { ElMessage, } from 'element-plus';
import { useRouter, useRoute } from 'vue-router';

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

const autoAcceptOrders = ref(false);


const toggleAutoAcceptOrders = (value: boolean) => {
  ElMessage({
    type: 'success',
    message: `已${value ? '开启' : '关闭'}自动接单`
  });
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


const updateOrderStatus = (order: any, newStatus: string) => {
  order.status = newStatus;
};

const publishDeliveryTask = (order: any) => {
  order.status = '配送中';
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