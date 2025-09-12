<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 顶部导航栏 -->
    <header class="fixed top-0 left-0 right-0 bg-white shadow-sm z-50 h-16">
      <div class="flex items-center justify-between h-full px-6">
        <div class="flex items-center">
          <h1 class="text-xl font-bold text-[#F9771C]">FoodDelivery Pro</h1>
        </div>
        <div class="flex items-center space-x-4">
          <el-icon class="text-gray-600 text-xl cursor-pointer">
            <Bell />
          </el-icon>
          <div class="flex items-center space-x-2">
            <img
              src="https://readdy.ai/api/search-image?query=professional%20restaurant%20owner%20portrait%20with%20friendly%20smile%20wearing%20chef%20uniform%20against%20clean%20white%20background%20modern%20lighting&width=40&height=40&seq=merchant-avatar-001&orientation=squarish"
              alt="商家头像" class="w-10 h-10 rounded-full object-cover" />
            <span class="text-gray-700 font-medium">{{ merchantInfo.username || '加载中...' }}</span>
          </div>
        </div>
      </div>
    </header>

    <div class="flex pt-16">
      <!-- 左侧导航菜单 -->
      <aside class="w-48 bg-white shadow-sm overflow-y-auto">
        <nav class="p-3">
          <div class="space-y-2">
            <div v-for="(item, index) in menuItems" :key="index" @click="handleMenuClick(item)" :class="{
                'bg-orange-50 text-[#F9771C] border-r-3 border-[#F9771C]': $route.name === item.routeName,
                'text-gray-700 hover:bg-gray-50': $route.name !== item.routeName
              }"
              class="flex items-center px-4 py-3 rounded-l-lg cursor-pointer transition-colors whitespace-nowrap !rounded-button">
              <el-icon class="mr-3 text-lg">
                <component :is="item.icon" />
              </el-icon>
              <span class="font-medium">{{ item.label }}</span>
            </div>
          </div>
        </nav>
        <div class="p-4 border-t border-gray-100">
          <div @click="handleLogout"
            class="flex items-center px-4 py-3 rounded-lg cursor-pointer transition-colors text-red-500 hover:bg-red-50">
            <el-icon class="mr-3 text-lg">
              <SwitchButton />
            </el-icon>
            <span class="font-medium">退出登录</span>
          </div>
        </div>
      </aside>

      <!-- 主内容区 -->
      <main class="flex-1 p-4 overflow-x-hidden">
        <!-- 配券中心 -->
        <div v-if="activeMenu === 'coupons'">
          <div class="flex justify-between items-center mb-4">
            <h2 class="text-2xl font-bold text-gray-800">配券中心</h2>
            <div class="flex items-center space-x-3">
              <button @click="showCouponForm = true"
                class="bg-[#F9771C] text-white px-6 py-2 rounded-md hover:bg-[#E16A0E] transition-colors cursor-pointer whitespace-nowrap !rounded-button">
                <el-icon class="mr-1">
                  <Plus />
                </el-icon>
                新建优惠券
              </button>
            </div>
          </div>

          <!-- 统计卡片 -->
          <div class="grid grid-cols-1 md:grid-cols-4 gap-2 mb-3">
            <el-card shadow="hover" class="border-l-4 border-[#F9771C]">
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm text-gray-500">总优惠券</p>
                  <p class="text-2xl font-bold">{{ stats.total || 0 }}</p>
                </div>
                <el-icon class="text-[#F9771C] text-2xl">
                  <Collection />
                </el-icon>
              </div>
            </el-card>
            <el-card shadow="hover" class="border-l-4 border-green-500">
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm text-gray-500">有效优惠券</p>
                  <p class="text-2xl font-bold">{{ stats.active || 0 }}</p>
                </div>
                <el-icon class="text-green-500 text-2xl">
                  <Check />
                </el-icon>
              </div>
            </el-card>
            <el-card shadow="hover" class="border-l-4 border-yellow-500">
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm text-gray-500">未开始</p>
                  <p class="text-2xl font-bold">{{ stats.upcoming || 0 }}</p>
                </div>
                <el-icon class="text-yellow-500 text-2xl">
                  <Clock />
                </el-icon>
              </div>
            </el-card>
            <el-card shadow="hover" class="border-l-4 border-gray-500">
              <div class="flex items-center justify-between">
                <div>
                  <p class="text-sm text-gray-500">已过期</p>
                  <p class="text-2xl font-bold">{{ stats.expired || 0 }}</p>
                </div>
                <el-icon class="text-gray-500 text-2xl">
                  <Close />
                </el-icon>
              </div>
            </el-card>
          </div>

          <!-- 优惠券列表 -->
          <el-card class="mb-6">
            <template #header>
              <div class="flex justify-between items-center">
                <span class="text-lg font-semibold">优惠券列表</span>
                <div class="flex items-center space-x-2">
                  <el-button size="small" :disabled="selectedCoupons.length === 0" @click="batchDeleteCoupons"
                    type="danger">
                    <el-icon class="mr-1">
                      <Delete />
                    </el-icon>
                    批量删除
                  </el-button>
                </div>
              </div>
            </template>

            <el-table v-loading="loading" :data="coupons" style="width: 100%" @selection-change="handleSelectionChange"
              :header-cell-style="{ padding: '8px 0' }" :cell-style="{ padding: '8px 0' }">
              <el-table-column type="selection" width="70" align="center" header-align="center" />
              <el-table-column prop="id" label="优惠券ID" width="120" align="center" header-align="center" />
              <el-table-column prop="name" label="优惠券名称" width="120" align="center" header-align="center" />
              <el-table-column label="优惠券类型" width="120" align="center" header-align="center">
                <template #default="scope">
                  <div class="flex justify-center">
                    <el-tag :type="scope.row.type === 'discount' ? 'warning' : 'success'" class="justify-center">
                      {{ scope.row.type === 'discount' ? '折扣券' : '满减券' }}
                    </el-tag>
                  </div>
                </template>
              </el-table-column>
              <el-table-column label="优惠内容" width="100" align="center" header-align="center">
                <template #default="scope">
                  <span class="whitespace-nowrap">
                    {{ scope.row.type === 'discount' ?
                    `${(scope.row.value * 10).toFixed(1)}折` :
                    `满¥${scope.row.minAmount}减¥${scope.row.value}`
                    }}
                  </span>
                </template>
              </el-table-column>
              <el-table-column prop="startTime" label="开始时间" width="230" align="center" header-align="center" />
              <el-table-column prop="endTime" label="结束时间" width="230" align="center" header-align="center" />
              <el-table-column label="状态" width="70" align="center" header-align="center">
                <template #default="scope">
                  <div class="flex justify-center">
                    <el-tag :type="getStatusTagType(scope.row.status)" effect="light">
                      {{ getStatusText(scope.row.status) }}
                    </el-tag>
                  </div>
                </template>
              </el-table-column>
              <el-table-column label="使用情况" width="120" align="center" header-align="center">
                <template #default="scope">
                  <span class="whitespace-nowrap">
                    {{ scope.row.totalQuantity ?
                    `${scope.row.usedQuantity}/${scope.row.totalQuantity}` :
                    `${scope.row.usedQuantity}/∞`
                    }}
                  </span>
                </template>
              </el-table-column>
              <el-table-column label="操作" width="170" fixed="right" align="center" header-align="center">
                <template #default="scope">
                  <div class="flex justify-center space-x-2">
                    <el-button size="small" @click="handleEdit(scope.row)">
                      编辑
                    </el-button>
                    <el-button size="small" type="danger" @click="handleDelete(scope.row)">
                      删除
                    </el-button>
                  </div>
                </template>
              </el-table-column>
            </el-table>

            <div class="flex justify-between items-center mt-4">
              <div class="text-sm text-gray-500">
                共 {{ total }} 条记录
              </div>
              <el-pagination v-model:current-page="currentPage" v-model:page-size="pageSize" :total="total"
                :page-sizes="[10, 20, 50, 100]" layout="sizes, prev, pager, next, jumper" @size-change="fetchCoupons"
                @current-change="fetchCoupons" />
            </div>
          </el-card>

        </div>
      </main>
    </div>

    <!-- 新建/编辑优惠券弹窗 -->
    <el-dialog v-model="showCouponForm" :title="isEditMode ? '编辑优惠券' : '新建优惠券'" width="600px"
      :close-on-click-modal="false">
      <el-form ref="couponFormRef" :model="couponForm" :rules="couponRules" label-width="120px" label-position="left">
        <el-form-item label="优惠券名称" prop="name">
          <el-input v-model="couponForm.name" placeholder="请输入优惠券名称" />
        </el-form-item>

        <el-form-item label="优惠券类型" prop="type">
          <el-radio-group v-model="couponForm.type">
            <el-radio label="fixed">满减券</el-radio>
            <el-radio label="discount">折扣券</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item v-if="couponForm.type === 'fixed'" label="优惠金额" prop="value">
          <el-input-number v-model="couponForm.value" :min="1" :max="1000" :precision="0" controls-position="right">
            <template #prefix>¥</template>
          </el-input-number>
          <span class="ml-2 text-sm text-gray-500">减去的金额</span>
        </el-form-item>

        <el-form-item v-if="couponForm.type === 'fixed'" label="最低消费" prop="minAmount">
          <el-input-number v-model="couponForm.minAmount" :min="couponForm.value + 1" :max="10000" :precision="0"
            controls-position="right">
            <template #prefix>¥</template>
          </el-input-number>
          <span class="ml-2 text-sm text-gray-500">满足此金额才可使用</span>
        </el-form-item>

        <el-form-item v-if="couponForm.type === 'discount'" label="折扣比例" prop="value">
          <el-input-number v-model="couponForm.value" :min="0.1" :max="0.99" :step="0.1" :precision="2"
            controls-position="right" />
          <span class="ml-2 text-sm text-gray-500">例如: 0.8 表示8折</span>
        </el-form-item>

        <el-form-item label="发放数量" prop="totalQuantity">
          <el-input-number v-model="couponForm.totalQuantity" :min="1" :max="100000" :precision="0"
            controls-position="right" />
          <span class="ml-2 text-sm text-gray-500">不填表示不限量</span>
        </el-form-item>

        <el-form-item label="有效期" required>
          <el-col :span="11">
            <el-form-item prop="startTime">
              <el-date-picker v-model="couponForm.startTime" type="datetime" placeholder="开始时间" style="width: 100%"
                :disabled-date="disabledStartDate" />
            </el-form-item>
          </el-col>
          <el-col :span="2" class="text-center">-</el-col>
          <el-col :span="11">
            <el-form-item prop="endTime">
              <el-date-picker v-model="couponForm.endTime" type="datetime" placeholder="结束时间" style="width: 100%"
                :disabled-date="disabledEndDate" />
            </el-form-item>
          </el-col>
        </el-form-item>

        <el-form-item label="优惠券描述" prop="description">
          <el-input v-model="couponForm.description" type="textarea" :rows="3" placeholder="请输入优惠券使用说明" />
        </el-form-item>
      </el-form>

      <template #footer>
        <span class="dialog-footer">
          <el-button @click="showCouponForm = false">取消</el-button>
          <el-button type="primary" @click="submitCouponForm">
            {{ isEditMode ? '保存修改' : '立即创建' }}
          </el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { ElMessage, ElMessageBox, type FormInstance, type FormRules } from 'element-plus';
import {
  Bell,
  House,
  List,
  Ticket,
  Warning,
  User,
  Plus,
  Collection,
  Check,
  Clock,
  Close,
  Delete,
  SwitchButton
} from '@element-plus/icons-vue';


import { getMerchantInfo } from '@/api/merchant_api';
import API from '@/api/index';


import loginApi from '@/api/login_api';
import { removeToken } from '@/utils/jwt';

const router = useRouter();
const $route = useRoute();
const couponFormRef = ref<FormInstance>();

// 菜单项
const activeMenu = ref('coupons');
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

// 商家名相关
const defaultMerchantInfo = {
  username: ''
};
const isLoading = ref(true);
const merchantInfo = ref({ ...defaultMerchantInfo });

// 优惠券列表相关
const loading = ref(false);
const coupons = ref<any[]>([]);
const selectedCoupons = ref<any[]>([]);
const currentPage = ref(1);
const pageSize = ref(10);
const total = ref(0);


// 统计数据
const stats = reactive({
  total: 0,
  active: 0,
  expired: 0,
  upcoming: 0,
  totalUsed: 0,
  totalDiscountAmount: 0
});

// 优惠券表单
const showCouponForm = ref(false);
const isEditMode = ref(false);
const currentCouponId = ref('');
const couponForm = reactive({
  name: '',
  type: 'fixed',
  value: 5,
  minAmount: 20,
  totalQuantity: 100,
  startTime: '',
  endTime: '',
  description: ''
});

// 表单验证规则
const couponRules = reactive<FormRules>({
  name: [
    { required: true, message: '请输入优惠券名称', trigger: 'blur' },
    { max: 20, message: '长度不能超过20个字符', trigger: 'blur' }
  ],
  type: [
    { required: true, message: '请选择优惠券类型', trigger: 'change' }
  ],
  value: [
    { required: true, message: '请输入优惠金额或折扣', trigger: 'blur' },
    { 
      validator: (rule, value, callback) => {
        if (couponForm.type === 'fixed' && value <= 0) {
          callback(new Error('优惠金额必须大于0'));
        } else if (couponForm.type === 'discount' && (value <= 0 || value >= 1)) {
          callback(new Error('折扣比例必须在0-1之间'));
        } else {
          callback();
        }
      },
      trigger: 'blur'
    }
  ],
  minAmount: [
    { 
      required: true, 
      message: '请输入最低消费金额', 
      trigger: 'blur',
      validator: (rule, value, callback) => {
        if (couponForm.type === 'fixed' && value <= couponForm.value) {
          callback(new Error('最低消费必须大于优惠金额'));
        } else {
          callback();
        }
      }
    }
  ],
  startTime: [
    { required: true, message: '请选择开始时间', trigger: 'change' }
  ],
  endTime: [
    { required: true, message: '请选择结束时间', trigger: 'change' },
    {
      validator: (rule, value, callback) => {
        if (couponForm.startTime && new Date(value) <= new Date(couponForm.startTime)) {
          callback(new Error('结束时间必须晚于开始时间'));
        } else {
          callback();
        }
      },
      trigger: 'change'
    }
  ]
});

// 获取优惠券列表
const fetchCoupons = async () => {
  try {
    loading.value = true;
    const response = await API.get('/api/merchant/coupons', {
      params: {
        page: currentPage.value,
        pageSize: pageSize.value,
      }
    });
    
    console.log('优惠券API响应:', response.data);
    
    // 检查响应数据结构
    if (response.data && response.data.data && response.data.data.list) {
      coupons.value = response.data.data.list.map((item: any) => ({
        ...item,
        status: getCouponStatus(item)
      }));
      total.value = response.data.data.total;
    } else {
      console.warn('优惠券数据格式不正确:', response.data);
      coupons.value = [];
      total.value = 0;
    }
    
    console.log('处理后的优惠券数据:', coupons.value);
  } catch (error) {
    console.error('获取优惠券列表失败:', error);
    ElMessage.error('获取优惠券列表失败');
    coupons.value = [];
    total.value = 0;
  } finally {
    loading.value = false;
  }
};

// 获取统计数据
const fetchStats = async () => {
  try {
    const response = await API.get('/api/merchant/coupons/stats');
    Object.assign(stats, response.data.data);
  } catch (error) {
    console.error('获取统计数据失败', error);
  }
};

// 判断优惠券状态
const getCouponStatus = (coupon: any) => {
  const now = new Date();
  const start = new Date(coupon.startTime);
  const end = new Date(coupon.endTime);
  
  if (now < start) return 'upcoming';
  if (now > end) return 'expired';
  return 'active';
};

// 状态标签样式
const getStatusTagType = (status: string) => {
  switch (status) {
    case 'active': return 'success';
    case 'expired': return 'info';
    case 'upcoming': return 'warning';
    default: return 'primary';
  }
};

// 状态文本
const getStatusText = (status: string) => {
  switch (status) {
    case 'active': return '有效';
    case 'expired': return '已过期';
    case 'upcoming': return '未开始';
    default: return status;
  }
};

// 处理选择变化
const handleSelectionChange = (val: any[]) => {
  console.log('选择变化:', val);
  selectedCoupons.value = val;
  console.log('当前选中的优惠券数量:', selectedCoupons.value.length);
};

// 编辑优惠券
const handleEdit = (row: any) => {
  isEditMode.value = true;
  currentCouponId.value = row.id;
  Object.assign(couponForm, {
    name: row.name,
    type: row.type,
    value: row.value,
    minAmount: row.minAmount,
    totalQuantity: row.totalQuantity,
    startTime: row.startTime,
    endTime: row.endTime,
    description: row.description
  });
  showCouponForm.value = true;
};

// 提交表单
const submitCouponForm = async () => {
  try {
    await couponFormRef.value?.validate();
    
    const payload = {
      ...couponForm,
      startTime: new Date(couponForm.startTime).toISOString(),
      endTime: new Date(couponForm.endTime).toISOString()
    };
    
    if (isEditMode.value) {
      await API.put(`/api/merchant/coupons/${currentCouponId.value}`, payload);
      ElMessage.success('优惠券更新成功');
    } else {
      await API.post('/api/merchant/coupons', payload);
      ElMessage.success('优惠券创建成功');
    }
    
    showCouponForm.value = false;
    fetchCoupons();
    fetchStats();
  } catch (error) {
    console.error('表单提交失败', error);
  }
};

// 删除优惠券
const handleDelete = async (row: any) => {
  try {
    await ElMessageBox.confirm(
      `确定要删除优惠券 "${row.name}" 吗?`,
      '删除确认',
      {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }
    );
    
    await API.delete(`/api/merchant/coupons/${row.id}`);
    ElMessage.success('优惠券删除成功');
    fetchCoupons();
    fetchStats();
  } catch (error) {
    console.error('删除失败', error);
  }
};

// 批量删除
const batchDeleteCoupons = async () => {
  try {
    const ids = selectedCoupons.value.map(item => item.id);
    await ElMessageBox.confirm(
      `确定要删除选中的 ${ids.length} 张优惠券吗?`,
      '批量删除确认',
      {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }
    );
    
    await API.delete('/api/merchant/coupons/batch', { data: { ids } });
    ElMessage.success(`成功删除 ${ids.length} 张优惠券`);
    selectedCoupons.value = [];
    fetchCoupons();
    fetchStats();
  } catch (error) {
    console.error('批量删除失败', error);
  }
};

// 日期选择器禁用逻辑
const disabledStartDate = (time: Date) => {
  return time.getTime() < Date.now() - 86400000;
};

const disabledEndDate = (time: Date) => {
  return time.getTime() < new Date(couponForm.startTime).getTime();
};

// 初始化数据
const initData = async () => {

  // 添加默认测试优惠券
  coupons.value = [{
    id: 'TEST20240001',
    name: '新用户专享券',
    type: 'fixed',
    value: 10,
    minAmount: 50,
    startTime: new Date().toISOString(),
    endTime: new Date(Date.now() + 30 * 24 * 60 * 60 * 1000).toISOString(), // 30天后过期
    totalQuantity: 1000,
    usedQuantity: 0,
    description: '新用户专享优惠券，满50减10',
    status: 'active'
  }];

  await Promise.all([fetchCoupons(), fetchStats()]);
};


// 获取所有数据
const fetchAllData = async () => {
  try {
    isLoading.value = true;
    
    // 并行请求所有数据
    const [merchant] = await Promise.all([
      getMerchantInfo()
    ]);
    
    // 更新商家信息
    if (merchant) {
      merchantInfo.value = { ...defaultMerchantInfo, ...merchant };
    }
    
  } catch (error) {
    ElMessage.error('获取数据失败，请稍后重试');
    // 使用默认空值
    merchantInfo.value = { ...defaultMerchantInfo };
  } finally {
    isLoading.value = false;
  }
};

onMounted(() => {
  initData();
  fetchAllData();
});


async function handleLogout() {
  try {
    // 1. 弹出确认框
    await ElMessageBox.confirm(
      '您确定要退出当前商家账号吗？', // 提示信息可以针对商家进行微调
      '退出登录',
      {
        confirmButtonText: '确定退出',
        cancelButtonText: '取消',
        type: 'warning',
      }
    );

    // 2. 调用后端登出接口
    await loginApi.logout();

    // 3. 核心：清除本地登录状态
    removeToken();

    ElMessage.success('您已成功退出登录');

    // 4. 重定向到登录页面
    router.replace('/login'); // 确保 '/login' 是你的登录页路由

  } catch (error: any) {
    if (error === 'cancel') {
      ElMessage.info('已取消退出操作');
    } else {
      console.error('登出时发生错误:', error);
      ElMessage.warning('与服务器通信失败，但已在本地强制退出');
      removeToken();
      router.replace('/login');
    }
  }
}
</script>

<style scoped>
.\!rounded-button {
  border-radius: 8px;
}

.border-l-4 {
  border-left-width: 4px;
}

:deep(.el-card__header) {
  padding: 12px 20px;
  border-bottom: 1px solid #ffffff;
}

:deep(.el-table .cell) {
  padding-left: 8px;
  padding-right: 8px;
}

.min-h-screen {
  overflow-x: hidden;
}

:deep(.el-table .cell) {
  padding-left: 6px;
  padding-right: 6px;
  white-space: nowrap;
}

:deep(.el-table th.el-table__cell) {
  padding: 8px 0;
}

:deep(.el-card__body) {
  padding: 12px;
}

/* 确保表头和内容单元格对齐 */
:deep(.el-table th.el-table__cell) {
  background-color: #ffffff;
}
:deep(.el-table .cell) {
  line-height: 1.5;
  padding: 0 8px;
}
:deep(.el-table td.el-table__cell) {
  border-bottom: none;
}
</style>