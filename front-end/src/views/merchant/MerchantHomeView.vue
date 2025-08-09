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
            <!-- 商家姓名：默认“加载中” -->
            <span class="text-gray-700 font-medium">{{ merchantInfo.username || '加载中...' }}</span>
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
                  <p class="text-2xl font-bold text-blue-500">{{ shopInfo.creditScore }}</p>
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
                <div class="flex items-center">
                  <input
                    v-model="shopInfo.address"
                    :disabled="!isEditingAddress"
                    :class="{
                      'w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm': true,
                      'bg-gray-100 cursor-not-allowed': !isEditingAddress,
                      'bg-white': isEditingAddress
                    }"
                    placeholder="请输入店铺地址"
                  />
                  <button
                    @click="toggleEdit('address')"
                    class="ml-2 text-[#F9771C] hover:text-[#E16A0E] transition-colors"
                  >
                    <el-icon v-if="!isEditingAddress"><Edit /></el-icon>
                    <el-icon v-else><Check /></el-icon>
                  </button>
                </div>
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">营业时间</label>
                <div class="flex items-center">
                  <input
                    v-model="shopInfo.businessHours"
                    :disabled="!isEditingHours"
                    :class="{
                      'w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm': true,
                      'bg-gray-100 cursor-not-allowed': !isEditingHours,
                      'bg-white': isEditingHours
                    }"
                    placeholder="例：09:00-22:00"
                  />
                  <button
                    @click="toggleEdit('hours')"
                    class="ml-2 text-[#F9771C] hover:text-[#E16A0E] transition-colors"
                  >
                    <el-icon v-if="!isEditingHours"><Edit /></el-icon>
                    <el-icon v-else><Check /></el-icon>
                  </button>
                </div>
              </div>

              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2">店铺特色</label>
                <div class="flex items-center">
                  <input
                    v-model="shopInfo.feature"
                    :disabled="!isEditingFeature"
                    :class="{
                      'w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-[#F9771C] text-sm': true,
                      'bg-gray-100 cursor-not-allowed': !isEditingFeature,
                      'bg-white': isEditingFeature
                    }"
                    placeholder="请输入店铺特色（选填）"
                  />
                  <button
                    @click="toggleEdit('feature')"
                    class="ml-2 text-[#F9771C] hover:text-[#E16A0E] transition-colors"
                  >
                    <el-icon v-if="!isEditingFeature"><Edit /></el-icon>
                    <el-icon v-else><Check /></el-icon>
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
import { ref, onMounted } from 'vue';
import { Bell, Star, TrendCharts, Medal, House, List, Ticket, Warning, User } from '@element-plus/icons-vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import { useRouter, useRoute } from 'vue-router';
import { 
  getShopOverview, 
  getShopInfo, 
  getMerchantInfo, 
  toggleBusinessStatus as apiToggleBusinessStatus,
  updateShopField
} from '@/services/merchant_api';

// 默认空值
const defaultShopInfo = {
  id: '***',
  name: '***',
  createTime: '***',
  address: '',
  businessHours: '',
  feature: '',
  rating: 0,
  monthlySales: 0,
  creditScore: 0
};

const defaultMerchantInfo = {
  username: ''
};

// 响应式数据
const activeMenu = ref('overview');
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

const isOpen = ref(false);
const shopInfo = ref({ ...defaultShopInfo });
const merchantInfo = ref({ ...defaultMerchantInfo });
const isLoading = ref(true);

// 响应式状态
const isEditingAddress = ref(false);
const isEditingHours = ref(false);
const isEditingFeature = ref(false);

// 保存方法
const saveField = async (field: string) => {
  try {
    isLoading.value = true;
    
    let value;
    switch(field) {
      case 'address':
        value = shopInfo.value.address;
        break;
      case 'hours':
        value = shopInfo.value.businessHours;
        break;
      case 'feature':
        value = shopInfo.value.feature;
        break;
    }

    // 调用API保存
    await updateShopField(field, value as string);
    
    ElMessage.success(`${getFieldLabel(field)}更新成功`);
  } catch (error) {
    ElMessage.error(`${getFieldLabel(field)}更新失败`);
    throw error; // 抛出错误以便回滚编辑状态
  } finally {
    isLoading.value = false;
  }
};

// 获取字段显示名称
const getFieldLabel = (field: string) => {
  const labels: Record<string, string> = {
    address: '店铺地址',
    hours: '营业时间',
    feature: '店铺特色'
  };
  return labels[field] || field;
};

// 修改后的切换编辑方法
const toggleEdit = async (field: string) => {
  switch(field) {
    case 'address':
      if (isEditingAddress.value) {
        try {
          await saveField('address');
        } catch {
          return; // 保存失败时保持编辑状态
        }
      }
      isEditingAddress.value = !isEditingAddress.value;
      break;
      
    case 'hours':
      if (isEditingHours.value) {
        try {
          await saveField('hours');
        } catch {
          return;
        }
      }
      isEditingHours.value = !isEditingHours.value;
      break;
      
    case 'feature':
      if (isEditingFeature.value) {
        try {
          await saveField('feature');
        } catch {
          return;
        }
      }
      isEditingFeature.value = !isEditingFeature.value;
      break;
  }
};

// 获取所有数据
const fetchAllData = async () => {
  try {
    isLoading.value = true;
    
    // 并行请求所有数据
    const [overview, shop, merchant] = await Promise.all([
      getShopOverview(),
      getShopInfo(),
      getMerchantInfo()
    ]);
    
    // 更新店铺概览数据
    if (overview.data) {
      shopInfo.value.rating = overview.data.rating || 0;
      shopInfo.value.monthlySales = overview.data.monthlySales || 0;
      shopInfo.value.creditScore = overview.data.creditScore || 0;
      isOpen.value = overview.data.isOpen || false;
    }
    
    // 更新店铺信息
    if (shop.data) {
      const { id, name, createTime, address, businessHours, feature } = shop.data;
      shopInfo.value = {
        ...shopInfo.value, // 保留现有值（包括可能已更新的rating等）
        id: id || defaultShopInfo.id,
        name: name || defaultShopInfo.name,
        createTime: createTime || defaultShopInfo.createTime,
        address: address || defaultShopInfo.address,
        businessHours: businessHours || defaultShopInfo.businessHours,
        feature: feature || defaultShopInfo.feature
      };
    }
    
    // 更新商家信息
    if (merchant.data) {
      merchantInfo.value = { ...defaultMerchantInfo, ...merchant.data };
    }
    
  } catch (error) {
    ElMessage.error('获取数据失败，请稍后重试');
    // 使用默认空值
    shopInfo.value = { ...defaultShopInfo };
    merchantInfo.value = { ...defaultMerchantInfo };
  } finally {
    isLoading.value = false;
  }
};

// 切换营业状态
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
    
    // 调用API更新状态
    await apiToggleBusinessStatus(value);
    
    isOpen.value = value;
    ElMessage({
      type: 'success',
      message: `已${value ? '开启营业' : '暂停营业'}`
    });
  } catch (error) {
    if (error !== 'cancel') {
      ElMessage.error('操作失败，请重试');
    }
    isOpen.value = !value;
  }
};

// 组件挂载时获取数据
onMounted(() => {
  fetchAllData();
});
</script>