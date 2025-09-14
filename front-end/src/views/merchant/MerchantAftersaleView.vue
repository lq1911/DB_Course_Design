<!-- eslint-disable -->
<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->

<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 顶部导航栏 -->
    <header
      class="fixed top-0 left-0 right-0 bg-white/80 backdrop-blur-md shadow-sm z-50 h-16 border-b border-gray-100">
      <div class="flex items-center justify-between h-full px-6">
        <div class="flex items-center">
          <h1 class="text-xl font-bold text-[#F9771C]">{{ projectName }}</h1>
        </div>
        <div class="flex items-center space-x-4">
          <el-icon class="text-gray-600 text-xl cursor-pointer hover:text-[#F9771C] transition-colors">
            <Bell />
          </el-icon>
          <div class="flex items-center space-x-2">
            <img
              src="https://readdy.ai/api/search-image?query=professional%20restaurant%20owner%20portrait%20with%20friendly%20smile%20wearing%20chef%20uniform%20against%20clean%20white%20background%20modern%20lighting&width=40&height=40&seq=merchant-avatar-001&orientation=squarish"
              alt="商家头像"
              class="w-10 h-10 rounded-full object-cover ring-2 ring-white shadow-sm"
            />
            <span class="text-gray-700 font-medium">{{ merchantInfo.username || '加载中...' }}</span>
          </div>
        </div>
      </div>
    </header>

    <div class="flex pt-16">
      <!-- 左侧导航菜单 -->
      <aside
        class="fixed left-0 top-16 bottom-0 w-52 bg-white/80 backdrop-blur-md shadow-sm overflow-y-auto border-r border-gray-100">
        <nav class="p-4">
          <div class="space-y-2">
            <div v-for="(item, index) in menuItems" :key="index" @click="handleMenuClick(item)" :class="{
                'bg-orange-50 text-[#F9771C] border-r-3 border-[#F9771C]': $route.name === item.routeName,
                'text-gray-700 hover:bg-gray-50/80 hover:text-[#F9771C]': $route.name !== item.routeName
              }" class="flex items-center px-4 py-3 rounded-xl cursor-pointer transition-all duration-200 font-medium">
              <el-icon class="mr-3 text-lg">
                <component :is="item.icon" />
              </el-icon>
              <span>{{ item.label }}</span>
            </div>
          </div>
        </nav>
        <!-- 退出登录区域 -->
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
      <main class="ml-52 flex-1 p-8">
        <!-- 订单售后 -->
        <div v-if="activeMenu === 'aftersale'">
          <h2 class="text-2xl font-bold text-gray-800 mb-6">订单售后</h2>

          <!-- 切换标签 -->
          <div class="bg-white/80 backdrop-blur-md rounded-2xl shadow-lg p-6 mb-8 border border-gray-100">
            <div class="flex space-x-4">
              <button v-for="tab in aftersaleTabs" :key="tab.value" @click="activeAftersaleTab = tab.value" :class="{
                  'bg-gradient-to-r from-[#F9771C] to-orange-500 text-white shadow-lg': activeAftersaleTab === tab.value,
                  'bg-gray-100/80 text-gray-700 hover:bg-gray-200/80 hover:text-[#F9771C]': activeAftersaleTab !== tab.value
                }" class="px-6 py-3 rounded-xl transition-all duration-200 font-medium shadow-sm">
                {{ tab.label }}
              </button>
            </div>
          </div>

          <!-- 处罚记录 -->
          <div v-if="activeAftersaleTab === 'penalties'">
            <div class="bg-white/80 backdrop-blur-md rounded-2xl shadow-lg border border-gray-100 overflow-hidden">
              <div class="p-6 border-b border-gray-100">
                <div class="flex items-center gap-3">
                  <el-input v-model="penaltyFilters.keyword" placeholder="处罚编号/原因关键词" class="modern-input" clearable />
                  <el-button type="warning" class="modern-btn-primary" @click="loadPenalties()">筛选</el-button>
                </div>
              </div>
              <el-table :data="penaltyList" style="width: 100%" @row-click="openPenaltyDetail" class="modern-table">
                <el-table-column prop="id" label="处罚编号" width="150" />
                <el-table-column prop="reason" label="处罚原因" />
                <el-table-column prop="time" label="处罚时间" width="180" />
                <el-table-column label="商家处罚措施">
                  <template #default="scope">
                    {{ punishmentDict[scope.row.merchantAction] || scope.row.merchantAction }}
                  </template>
                </el-table-column>

                <el-table-column label="店铺处罚措施">
                  <template #default="scope">
                    {{ punishmentDict[scope.row.platformAction] || scope.row.platformAction }}
                  </template>
                </el-table-column>
                <el-table-column label="操作" width="160">
                  <template #default="scope">
                    <el-button size="small" class="modern-btn-secondary"
                      @click.stop="openPenaltyDetail(scope.row)">详情</el-button>
                    <el-button size="small" class="modern-btn-primary"
                      @click.stop="openPenaltyAppeal(scope.row)">申诉</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </div>
            <!-- 处罚详情抽屉 -->
            <el-drawer v-model="penaltyDetailVisible" title="处罚详情" size="520px" direction="rtl" class="modern-drawer">
              <div v-if="penaltyDetail" class="p-6">
                <div class="space-y-4">
                  <div class="bg-gray-50 rounded-xl p-4">
                    <div class="space-y-2 text-sm">
                      <div><b class="text-gray-600">处罚编号：</b>{{ penaltyDetail.id }}</div>
                      <div><b class="text-gray-600">处罚时间：</b>{{ penaltyDetail.time }}</div>
                      <div><b class="text-gray-600">处罚原因：</b>{{ penaltyDetail.reason }}</div>
                      <div><b class="text-gray-600">平台措施：</b>{{ punishmentDict[penaltyDetail.platformAction] ||
                        penaltyDetail.platformAction }}</div>
                      <div><b class="text-gray-600">商家措施：</b>{{ punishmentDict[penaltyDetail.merchantAction] ||
                        penaltyDetail.merchantAction }}</div>
                    </div>
                  </div>
                </div>
              </div>
            </el-drawer>
            <!-- 处罚申诉弹窗 -->
            <el-dialog v-model="penaltyAppealVisible" title="处罚申诉" width="460px" class="modern-dialog">
              <div class="space-y-4">
                <div>
                  <el-input v-model="penaltyAppealReason" type="textarea" placeholder="请填写申诉理由" :rows="4"
                    class="modern-textarea" />
                </div>
              </div>
              <template #footer>
                <el-button @click="penaltyAppealVisible = false" class="modern-btn-secondary">取消</el-button>
                <el-button class="modern-btn-primary" :disabled="!penaltyAppealReason"
                  @click="submitPenaltyAppeal">提交申诉</el-button>
              </template>
            </el-dialog>
          </div>

          <!-- 售后申请列表 -->
          <div v-if="activeAftersaleTab === 'aftersale'">
            <div class="bg-white/80 backdrop-blur-md rounded-2xl shadow-lg border border-gray-100 overflow-hidden">
              <div class="p-6 border-b border-gray-100">
                <div class="flex items-center gap-3">
                  <el-select v-model="asFilters.keyword" placeholder="订单号/用户名/电话" class="modern-select">
                    <el-option label="全部" value="" />
                    <el-option label="订单号" value="orderNo" />
                    <el-option label="用户名" value="user.name" />
                    <el-option label="电话" value="user.phone" />
                  </el-select>
                  <el-input v-model="asFilters.keyword" placeholder="订单号/用户名/电话" class="modern-input" clearable />
                  <el-button type="warning" class="modern-btn-primary" @click="loadAfterSales(1)">查询</el-button>
                  <el-button @click="resetAsFilters" class="modern-btn-secondary">重置</el-button>
                </div>
              </div>
              <el-table :data="aftersaleList" style="width: 100%" class="modern-table">
                <el-table-column prop="orderNo" label="订单号" width="200" />
                <el-table-column label="用户" width="120">
                  <template #default="scope">
                    <div class="flex items-center gap-2">
                      <img v-if="scope.row.user?.avatar" :src="scope.row.user.avatar"
                        class="w-8 h-8 rounded-full object-cover border shadow-sm" />
                      <span>{{ scope.row.user?.name }}</span>
                    </div>
                  </template>
                </el-table-column>
                <el-table-column prop="reason" label="申请原因" />
                <el-table-column prop="createdAt" label="申请时间" width="160" />
                <el-table-column label="操作" width="100">
                  <template #default="scope">
                    <el-button size="small" class="modern-btn-primary"
                      @click="openAsDetail(scope.row.id)">详情</el-button>
                  </template>
                </el-table-column>
              </el-table>
              <div class="flex justify-between items-center p-4 border-t border-gray-100">
                <div></div>
                <el-pagination background layout="prev, pager, next" :page-size="asPageSize" :current-page="asPage"
                  :total="asTotal" @current-change="loadAfterSales" :pager-count="5" class="modern-pagination" />
              </div>
            </div>
            <!-- 详情抽屉 -->
            <el-drawer v-model="asDetailVisible" title="售后详情" size="600px" direction="rtl" class="modern-drawer">
              <div v-if="asDetail" class="p-6">
                <div class="space-y-4">
                  <div class="bg-gray-50 rounded-xl p-4">
                    <div class="grid grid-cols-2 gap-4 text-sm">
                      <div><b class="text-gray-600">订单号：</b>{{ asDetail.orderNo }}</div>
                      <div><b class="text-gray-600">申请时间：</b>{{ asDetail.createdAt }}</div>
                      <div class="col-span-2"><b class="text-gray-600">用户：</b>{{ asDetail.user?.name }}（{{
                        asDetail.user?.phone }}）</div>
                    </div>
                  </div>
                  <div class="bg-gray-50 rounded-xl p-4">
                    <b class="text-gray-600 block mb-2">申请原因：</b>
                    <p class="text-sm">{{ asDetail.reason }}</p>
                  </div>
                  <div class="bg-orange-50 rounded-xl p-4 border border-orange-200">
                    <b class="text-gray-600 block mb-3">处理操作：</b>
                    <el-radio-group v-model="decision.action" class="mb-3">
                      <el-radio label="approve" class="modern-radio">同意</el-radio>
                      <el-radio label="reject" class="modern-radio">拒绝</el-radio>
                      <el-radio label="negotiate" class="modern-radio">协商</el-radio>
                    </el-radio-group>
                    <el-input v-model="decision.remark" placeholder="处理意见（必填）" class="modern-input" />
                    <el-button class="modern-btn-primary" :disabled="!decision.action || !decision.remark"
                      @click="submitDecision">提交处理</el-button>
                  </div>
                </div>
              </div>
            </el-drawer>
          </div>

          <!-- 评论查看 -->
          <div v-if="activeAftersaleTab === 'reviews'">
            <div class="bg-white/80 backdrop-blur-md rounded-2xl shadow-lg border border-gray-100 overflow-hidden">
              <div class="p-6 border-b border-gray-100">
                <div class="flex items-center gap-3">
                  <el-input v-model="reviewFilters.keyword" placeholder="内容/订单号" class="modern-input" clearable />
                  <el-button type="warning" @click="fetchReviews(1)" class="modern-btn-primary">筛选</el-button>
                  <el-button @click="resetReviewFilters" class="modern-btn-secondary">重置</el-button>
                </div>
              </div>
              <el-table :data="reviews" style="width: 100%" class="modern-table">
                <el-table-column prop="orderNo" label="订单号" width="200" />
                <el-table-column label="用户" width="120">
                  <template #default="scope">
                    <div class="flex items-center gap-2">
                      <img v-if="scope.row.user?.avatar" :src="scope.row.user.avatar"
                        class="w-8 h-8 rounded-full object-cover border shadow-sm" />
                      <span>{{ scope.row.user?.name }}</span>
                    </div>
                  </template>
                </el-table-column>
                <el-table-column prop="content" label="内容" />
                <el-table-column prop="createdAt" label="时间" width="160" />
                <el-table-column label="操作" width="120">
                  <template #default="scope">
                    <div class="flex items-center gap-1">
                      <el-button size="small" class="modern-btn-primary"
                        @click="openReplyDialog(scope.row)">回复</el-button>
                    </div>
                  </template>
                </el-table-column>
              </el-table>
              <div class="flex justify-between items-center p-4 border-t border-gray-100">
                <div></div>
                <el-pagination background layout="prev, pager, next" :page-size="reviewPageSize"
                  :current-page="reviewPage" :total="reviewTotal" @current-change="fetchReviews" :pager-count="5"
                  class="modern-pagination" />
              </div>
            </div>
            <!-- 回复弹窗 -->
            <el-dialog v-model="replyDialogVisible" title="回复评论" width="520px" class="modern-dialog">
              <div class="space-y-4">
                <div>
                  <span class="font-bold text-gray-700 text-sm">历史聊天记录：</span>
                  <div id="reply-chat-container"
                    class="mt-2 max-h-60 overflow-y-auto rounded-xl border border-gray-100 bg-gray-50 p-3 space-y-2">
                    <div v-if="chatLoading" class="text-center text-gray-500 text-sm py-4">加载中...</div>
                    <template v-else>
                      <div v-if="!chatMessages.length" class="text-center text-gray-400 text-sm py-4">暂无聊天记录</div>
                      <div v-for="(msg, idx) in chatMessages" :key="idx" class="flex"
                        :class="msg.sender === 'merchant' ? 'justify-end' : 'justify-start'">
                        <div :class="[
                          'px-3 py-2 rounded-2xl text-sm shadow-sm max-w-[75%]',
                          msg.sender === 'merchant' ? 'bg-[#FEECDC] text-gray-800 rounded-br-sm' : 'bg-white text-gray-800 rounded-bl-sm border'
                        ]">
                          <div class="whitespace-pre-wrap leading-relaxed">{{ msg.content }}</div>
                          <div class="text-[11px] text-gray-400 mt-1 text-right">{{ msg.time }}</div>
                        </div>
                      </div>
                    </template>
                  </div>
                </div>
                <div>
                  <span class="font-bold text-gray-700 text-sm">常用语：</span>
                  <div class="flex flex-wrap gap-2 mt-2">
                    <el-tag v-for="(phrase, idx) in quickPhrases" :key="idx" size="small" class="modern-tag"
                      @click="insertToReply(phrase)">{{ phrase }}</el-tag>
                  </div>
                </div>
                <div>
                  <span class="font-bold text-gray-700 text-sm">表情：</span>
                  <div class="flex flex-wrap gap-2 mt-2">
                    <span v-for="(emoji, idx) in emojis" :key="emoji" @click="insertToReply(emoji)"
                      class="text-2xl cursor-pointer hover:scale-110 transition-transform">{{ emoji }}</span>
                  </div>
                </div>
                <el-input id="reply-content-textarea" v-model="replyContent" type="textarea" placeholder="回复内容"
                  :rows="4" class="modern-textarea" />
              </div>
              <template #footer>
                <el-button @click="replyDialogVisible = false" class="modern-btn-secondary">取消</el-button>
                <el-button class="modern-btn-primary" @click="submitReply">发送</el-button>
              </template>
            </el-dialog>

          </div>
        </div>

      </main>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { getProjectName } from '@/stores/name';

import { ref, reactive, onMounted, nextTick } from 'vue';
// ▼▼▼ 修改点 1: 在图标导入中加入 SwitchButton ▼▼▼
import { Bell, House, List, Ticket, Warning, User, SwitchButton } from '@element-plus/icons-vue';
import { useRouter, useRoute } from 'vue-router';
// ▼▼▼ 修改点 2: 导入 ElMessageBox 用于确认弹窗 ▼▼▼
import { ElMessage, ElMessageBox } from 'element-plus'; // 假设您可能需要ElMessage, 一并导入

// --- 您已有的 API 导入 (保持不变) ---
import { replyReview, getReviewList, getPenaltyList, getPenaltyDetail, appealPenalty, type Review } from '@/api/merchant_api';
import type { AfterSaleApplication, AfterSaleListParams } from '@/api/merchant_api';
import { getAfterSaleList, getAfterSaleDetail, decideAfterSale } from '@/api/merchant_api';
import { type PenaltyRecord } from '@/api/merchant_api';

// --- 您已有的登出逻辑相关导入 (保持不变) ---
import loginApi from '@/api/login_api';
import { removeToken } from '@/utils/jwt';
// 本地聊天消息类型
interface LocalChatMessage {
  sender: 'user' | 'merchant';
  content: string;
  time: string;
}

const activeMenu = ref('aftersale');
const router = useRouter();
const $route = useRoute();

const useProjectName = getProjectName();
const projectName = useProjectName.projectName;

const menuItems = [
  { key: 'overview', label: '店铺概况', icon: House, routeName: 'MerchantHome' },
  { key: 'orders', label: '订单中心', icon: List, routeName: 'MerchantOrders' },
  { key: 'coupons', label: '配券中心', icon: Ticket, routeName: 'MerchantCoupons' },
  { key: 'aftersale', label: '订单售后', icon: Warning, routeName: 'MerchantAftersale' },
  { key: 'profile', label: '商家信息', icon: User, routeName: 'MerchantProfile' }
] as const;

import { getMerchantInfo, type MerchantInfo } from '@/api/merchant_api';

const fetchAllData = async () => {
  try {
    console.log('开始获取商家信息...');
    const merchant = await getMerchantInfo();
    console.log('获取到的商家信息:', merchant);
    
    if (merchant) {
      merchantInfo.value = { ...defaultMerchantInfo, ...merchant };
      console.log('设置后的商家信息:', merchantInfo.value);
    }
  } catch (error) {
    console.error('获取商家信息失败:', error);
    merchantInfo.value = { ...defaultMerchantInfo };
  }
};

const defaultMerchantInfo = {
  username: '',
  sellerId: 0
};

const merchantInfo = ref({ ...defaultMerchantInfo });

const handleMenuClick = (menuItem: typeof menuItems[number]) => {
  router.push({ name: menuItem.routeName });
};

// 处罚记录
const penaltyList = ref<PenaltyRecord[]>([]);
const penaltyFilters = reactive<{ keyword: string }>({ keyword: '' });
const penaltyDetailVisible = ref(false);
const penaltyDetail = ref<PenaltyRecord | null>(null);

async function loadPenalties() {
  const params: { keyword?: string } = {};
  if (penaltyFilters.keyword) params.keyword = penaltyFilters.keyword.trim();
  try {
    const list = await getPenaltyList(params);
    penaltyList.value = list || [];
  } catch (error) {
    console.error('加载处罚记录失败:', error);
    penaltyList.value = [];
  }
}

async function openPenaltyDetail(row: PenaltyRecord) {
  try {
    penaltyDetail.value = await getPenaltyDetail(row.id);
    penaltyDetailVisible.value = true;
  } catch (error) {
    console.error('获取处罚详情失败:', error);
  }
}

// 处罚申诉弹窗
const penaltyAppealVisible = ref(false);
const penaltyAppealReason = ref('');
let penaltyAppealTarget: PenaltyRecord | null = null;

function openPenaltyAppeal(row: PenaltyRecord) {
  penaltyAppealTarget = row;
  penaltyAppealReason.value = '';
  penaltyAppealVisible.value = true;
}

async function submitPenaltyAppeal() {
  if (!penaltyAppealTarget || !penaltyAppealReason.value) return;
  try {
    await appealPenalty(penaltyAppealTarget.id, penaltyAppealReason.value);
  } finally {
    penaltyAppealVisible.value = false;
    await loadPenalties();
  }
}

// 评论管理
const reviews = ref<Review[]>([]);
const reviewPage = ref(1);
const reviewPageSize = ref(10);
const reviewTotal = ref(0);
const reviewFilters = reactive({
  keyword: ''
});

async function fetchReviews(page = 1) {
  reviewPage.value = page;
  
  // 检查是否有商家ID
  console.log('当前商家信息:', merchantInfo.value);
  console.log('商家ID:', merchantInfo.value.sellerId);
  
  if (!merchantInfo.value.sellerId) {
    console.warn('商家ID未获取到，无法加载评论');
    reviews.value = [];
    reviewTotal.value = 0;
    return;
  }
  
  try {
    const params = {
      page: reviewPage.value,
      pageSize: reviewPageSize.value,
      keyword: reviewFilters.keyword || undefined,
      sellerId: merchantInfo.value.sellerId
    };
    
    console.log('发送评论请求参数:', params);
    const res = await getReviewList(params);
    console.log('评论API响应:', res);
    
    reviews.value = res.list || [];
    reviewTotal.value = res.total || 0;
  } catch (error) {
    console.error('获取评论列表失败:', error);
    reviews.value = [];
    reviewTotal.value = 0;
  }
}

function resetReviewFilters() {
  reviewFilters.keyword = '';
  fetchReviews(1);
}
// 回复
const replyDialogVisible = ref(false);
const replyContent = ref('');
const replyReviewId = ref<number | null>(null);
const currentReview = ref<Review | null>(null);
const chatMessages = ref<LocalChatMessage[]>([]);
const chatLoading = ref(false);
const activeChatOrderNo = ref<string | null>(null);

async function loadChatHistory() {
  chatLoading.value = true;
  try {
    // 模拟聊天记录，实际项目中应该调用后端API
    const synthetic: LocalChatMessage[] = [];
    if (currentReview.value) {
      // 用户最开始的评论
      synthetic.push({ sender: 'user', content: currentReview.value.content, time: currentReview.value.createdAt });
    }
    chatMessages.value = [...synthetic];
  } catch (err) {
    const fallback: LocalChatMessage[] = [];
    if (currentReview.value) {
      fallback.push({ sender: 'user', content: currentReview.value.content, time: currentReview.value.createdAt });
    }
    chatMessages.value = fallback;
  } finally {
    chatLoading.value = false;
    nextTick(() => {
      const el = document.getElementById('reply-chat-container');
      if (el) el.scrollTop = el.scrollHeight;
    });
  }
}

function openReplyDialog(review: Review) {
  replyReviewId.value = review.id;
  currentReview.value = review;
  replyContent.value = '';
  replyDialogVisible.value = true;
  activeChatOrderNo.value = review.orderNo;
  loadChatHistory();
}

async function submitReply() {
  if (!replyReviewId.value || !replyContent.value) return;
  await replyReview(replyReviewId.value, replyContent.value);
  // 发送成功后，追加到聊天记录中并滚动到底部
  const newMsg: LocalChatMessage = { sender: 'merchant', content: replyContent.value, time: new Date().toLocaleString() };
  chatMessages.value.push(newMsg);
  replyContent.value = '';
  nextTick(() => {
    const el = document.getElementById('reply-chat-container');
    if (el) el.scrollTop = el.scrollHeight;
  });
}
onMounted(async () => {
  await fetchAllData();
  fetchReviews();
  loadPenalties();
  loadAfterSales(1);
});

const aftersaleTabs = [
  { value: 'penalties', label: '处罚记录' },
  { value: 'reviews', label: '评论查看' },
  { value: 'aftersale', label: '售后申请' }
];

const activeAftersaleTab = ref('penalties');

// 常用语和表情
const quickPhrases = [
  '感谢您的反馈！',
  '欢迎再次光临！',
  '我们会尽快改进',
  '祝您生活愉快！',
  '很抱歉给您带来不便'
];
const emojis = [
  '😀','😂','🥰','😎','🤔','😱','😴','🤗','😤','😇','😜','😅','😆','😏','😬','😳','😢','😭','😡','😋',
  '👍','🙏','��','🎉','🌟','🍽️','🍔','🍟','🍕','🍜','🍣','🍦','🍰','🥤','🥟','🥗','🥩','🥚','🥛'
];

function insertToReply(text: string) {
  // 插入到光标处
  const textarea = document.getElementById('reply-content-textarea') as HTMLTextAreaElement | null;
  if (textarea) {
    const start = textarea.selectionStart;
    const end = textarea.selectionEnd;
    const value = replyContent.value;
    replyContent.value = value.slice(0, start) + text + value.slice(end);
    // 重新聚焦并设置光标
    nextTick(() => {
      textarea.focus();
      textarea.selectionStart = textarea.selectionEnd = start + text.length;
    });
  } else {
    replyContent.value += text;
  }
}

// 2. 售后申请相关数据与方法
const aftersaleList = ref<AfterSaleApplication[]>([]);
const asPage = ref(1);
const asPageSize = ref(10);
const asTotal = ref(0);
const asFilters = reactive({
  keyword: ''
});


async function loadAfterSales(page = 1) {
  asPage.value = page;
  const params: AfterSaleListParams = {
    page: asPage.value,
    pageSize: asPageSize.value,
    keyword: asFilters.keyword || undefined
  };
  try {
    const res = await getAfterSaleList(params);
    aftersaleList.value = res.list || [];
    asTotal.value = res.total || 0;
  } catch (error) {
    console.error('获取售后申请列表失败:', error);
    aftersaleList.value = [];
    asTotal.value = 0;
  }
}

function resetAsFilters() {
  asFilters.keyword = '';
  loadAfterSales(1);
}

// 详情与处理
const asDetailVisible = ref(false);
const asDetail = ref<AfterSaleApplication | null>(null);

async function openAsDetail(id: number) {
  asDetailVisible.value = true;
  clearDecision();
  try {
    const detail = await getAfterSaleDetail(id);
    asDetail.value = detail;
  } catch (error) {
    console.error('获取售后申请详情失败:', error);
    asDetail.value = null;
  }
}

function clearDecision() {
  decision.action = '';
  decision.remark = '';
}

const decision = reactive<{
  action: string,
  remark: string
}>({
  action: '',
  remark: ''
});

async function submitDecision() {
  if (!asDetail.value || !decision.action) return;
  
  try {
    await decideAfterSale(asDetail.value.id, decision.action as any, {
      remark: decision.remark
    });
    await loadAfterSales(asPage.value);
    asDetail.value = await getAfterSaleDetail(asDetail.value.id);
    clearDecision();
  } catch (error) {
    console.error('处理售后申请失败:', error);
  }
}

const punishmentDict: Record<string, string> = {
  verbal_warning: '口头警告',
  written_warning: '书面警告',
  fine_500: '罚款500元',
  fine_1000: '罚款1000元',
  correction: '限期整改',
  suspend_3days: '暂停营业3天',
  suspend_7days: '暂停营业7天',
  permanent_removal: '永久下架',
};


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
/* 苹果风格设计 */
.modern-select :deep(.el-input__wrapper) {
  border-radius: 12px;
  border: 1px solid #e5e7eb;
  background: rgba(255, 255, 255, 0.8);
  backdrop-filter: blur(10px);
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.modern-input :deep(.el-input__wrapper) {
  border-radius: 12px;
  border: 1px solid #e5e7eb;
  background: rgba(255, 255, 255, 0.8);
  backdrop-filter: blur(10px);
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.modern-textarea :deep(.el-textarea__inner) {
  border-radius: 12px;
  border: 1px solid #e5e7eb;
  background: rgba(255, 255, 255, 0.8);
  backdrop-filter: blur(10px);
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
}

.modern-btn-primary {
  background: linear-gradient(135deg, #F9771C 0%, #ff8c42 100%);
  border: none;
  border-radius: 12px;
  color: white;
  font-weight: 500;
  box-shadow: 0 4px 12px rgba(249, 119, 28, 0.3);
  transition: all 0.3s ease;
}

.modern-btn-primary:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(249, 119, 28, 0.4);
}

.modern-btn-secondary {
  background: rgba(255, 255, 255, 0.8);
  border: 1px solid #F9771C;
  border-radius: 12px;
  color: #F9771C;
  font-weight: 500;
  backdrop-filter: blur(10px);
  transition: all 0.3s ease;
}

.modern-btn-secondary:hover {
  background: rgba(249, 119, 28, 0.1);
  transform: translateY(-1px);
}

.modern-table :deep(.el-table__header) {
  background: rgba(249, 119, 28, 0.05);
}

.modern-table :deep(.el-table__row:hover) {
  background: rgba(249, 119, 28, 0.02);
}

.modern-tag {
  background: rgba(249, 119, 28, 0.1);
  border: 1px solid rgba(249, 119, 28, 0.2);
  color: #F9771C;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.2s ease;
}

.modern-tag:hover {
  background: rgba(249, 119, 28, 0.2);
  transform: scale(1.05);
}

.modern-radio :deep(.el-radio__input.is-checked .el-radio__inner) {
  background: #F9771C;
  border-color: #F9771C;
}

.modern-pagination :deep(.el-pager li.is-active) {
  background: #F9771C;
  color: white;
}

.modern-drawer :deep(.el-drawer__header) {
  background: rgba(249, 119, 28, 0.05);
  border-bottom: 1px solid #e5e7eb;
}

.modern-dialog :deep(.el-dialog__header) {
  background: rgba(249, 119, 28, 0.05);
  border-bottom: 1px solid #e5e7eb;
}

.modern-timeline :deep(.el-timeline-item__node) {
  background: #F9771C;
}

.modern-timeline :deep(.el-timeline-item__tail) {
  border-left-color: rgba(249, 119, 28, 0.2);
}

.modern-file-input {
  border: 2px dashed #e5e7eb;
  border-radius: 12px;
  padding: 20px;
  text-align: center;
  background: rgba(255, 255, 255, 0.5);
  cursor: pointer;
  transition: all 0.3s ease;
}

.modern-file-input:hover {
  border-color: #F9771C;
  background: rgba(249, 119, 28, 0.05);
}

.\!rounded-button {
  border-radius: 12px;
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