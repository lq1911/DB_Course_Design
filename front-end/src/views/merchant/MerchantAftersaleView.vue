<!-- eslint-disable -->
<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->

<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 顶部导航栏 -->
    <header class="fixed top-0 left-0 right-0 bg-white/80 backdrop-blur-md shadow-sm z-50 h-16 border-b border-gray-100">
      <div class="flex items-center justify-between h-full px-6">
        <div class="flex items-center">
          <h1 class="text-xl font-bold text-[#F9771C]">FoodDelivery Pro</h1>
        </div>
        <div class="flex items-center space-x-4">
          <el-icon class="text-gray-600 text-xl cursor-pointer hover:text-[#F9771C] transition-colors"><Bell /></el-icon>
          <div class="flex items-center space-x-2">
            <img
              src="https://readdy.ai/api/search-image?query=professional%20restaurant%20owner%20portrait%20with%20friendly%20smile%20wearing%20chef%20uniform%20against%20clean%20white%20background%20modern%20lighting&width=40&height=40&seq=merchant-avatar-001&orientation=squarish"
              alt="商家头像"
              class="w-10 h-10 rounded-full object-cover ring-2 ring-white shadow-sm"
            />
            <span class="text-gray-700 font-medium">张老板</span>
          </div>
        </div>
      </div>
    </header>

    <div class="flex pt-16">
      <!-- 左侧导航菜单 -->
      <aside class="fixed left-0 top-16 bottom-0 w-52 bg-white/80 backdrop-blur-md shadow-sm overflow-y-auto border-r border-gray-100">
        <nav class="p-4">
          <div class="space-y-2">
            <div
              v-for="(item, index) in menuItems"
              :key="index"
              @click="handleMenuClick(item)"
              :class="{
                'bg-orange-50 text-[#F9771C] border-r-3 border-[#F9771C]': $route.name === item.routeName,
                'text-gray-700 hover:bg-gray-50/80 hover:text-[#F9771C]': $route.name !== item.routeName
              }"
              class="flex items-center px-4 py-3 rounded-xl cursor-pointer transition-all duration-200 font-medium"
            >
              <el-icon class="mr-3 text-lg">
                <component :is="item.icon" />
              </el-icon>
              <span>{{ item.label }}</span>
            </div>
          </div>
        </nav>
      </aside>

      <!-- 主内容区 -->
      <main class="ml-52 flex-1 p-8">
        <!-- 订单售后 -->
        <div v-if="activeMenu === 'aftersale'">
          <h2 class="text-2xl font-bold text-gray-800 mb-6">订单售后</h2>
          
          <!-- 切换标签 -->
          <div class="bg-white/80 backdrop-blur-md rounded-2xl shadow-lg p-6 mb-8 border border-gray-100">
            <div class="flex space-x-4">
              <button
                v-for="tab in aftersaleTabs"
                :key="tab.value"
                @click="activeAftersaleTab = tab.value"
                :class="{
                  'bg-gradient-to-r from-[#F9771C] to-orange-500 text-white shadow-lg': activeAftersaleTab === tab.value,
                  'bg-gray-100/80 text-gray-700 hover:bg-gray-200/80 hover:text-[#F9771C]': activeAftersaleTab !== tab.value
                }"
                class="px-6 py-3 rounded-xl transition-all duration-200 font-medium shadow-sm"
              >
                {{ tab.label }}
              </button>
            </div>
          </div>

          <!-- 处罚记录 -->
          <div v-if="activeAftersaleTab === 'penalties'">
            <div class="bg-white/80 backdrop-blur-md rounded-2xl shadow-lg border border-gray-100 overflow-hidden">
              <div class="p-6 border-b border-gray-100">
                <div class="flex items-center gap-3">
                  <el-select v-model="penaltyFilters.status" placeholder="申诉状态" class="modern-select">
                    <el-option label="全部" value="" />
                    <el-option label="未申诉" value="未申诉" />
                    <el-option label="申诉中" value="申诉中" />
                    <el-option label="已处理" value="已处理" />
                  </el-select>
                  <el-input v-model="penaltyFilters.keyword" placeholder="处罚编号/原因关键词" class="modern-input" clearable />
                  <el-button type="warning" class="modern-btn-primary" @click="loadPenalties()">筛选</el-button>
                </div>
              </div>
              <el-table :data="penaltyList" style="width: 100%" @row-click="openPenaltyDetail" class="modern-table">
                <el-table-column prop="id" label="处罚编号" width="150" />
                <el-table-column prop="reason" label="处罚原因" />
                <el-table-column prop="time" label="处罚时间" width="180" />
                <el-table-column prop="merchantAction" label="商家处罚措施" />
                <el-table-column prop="platformAction" label="店铺处罚措施" />
                <el-table-column label="状态" width="100">
                  <template #default="scope">
                    <span :class="['px-3 py-1 rounded-full text-xs font-medium',
                      scope.row.status==='未申诉' && 'bg-gray-100 text-gray-600',
                      scope.row.status==='申诉中' && 'bg-orange-100 text-[#F9771C]',
                      scope.row.status==='已处理' && 'bg-green-100 text-green-600']">
                      {{ scope.row.status || '未申诉' }}
                    </span>
                  </template>
                </el-table-column>
                <el-table-column label="操作" width="160">
                  <template #default="scope">
                    <el-button size="small" class="modern-btn-secondary" @click.stop="openPenaltyDetail(scope.row)">详情</el-button>
                    <el-button size="small" class="modern-btn-primary" @click.stop="openPenaltyAppeal(scope.row)">申诉</el-button>
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
                      <div><b class="text-gray-600">处罚金额：</b>{{ penaltyDetail.amount ? '¥'+penaltyDetail.amount : '—' }}</div>
                      <div><b class="text-gray-600">平台措施：</b>{{ penaltyDetail.platformAction }}</div>
                      <div><b class="text-gray-600">商家措施：</b>{{ penaltyDetail.merchantAction }}</div>
                    </div>
                  </div>
                  <div v-if="penaltyDetail.evidenceImages && penaltyDetail.evidenceImages.length" class="bg-gray-50 rounded-xl p-4">
                    <b class="text-gray-600 block mb-2">凭证图片：</b>
                    <div class="flex gap-3">
                      <img v-for="(img, idx) in penaltyDetail.evidenceImages" :key="idx" :src="img" class="w-24 h-24 object-cover rounded-lg shadow-sm" />
                    </div>
                  </div>
                  <div class="bg-gray-50 rounded-xl p-4">
                    <b class="text-gray-600 block mb-2">申诉进度：</b>
                    <el-timeline class="modern-timeline">
                      <el-timeline-item v-for="(item, idx) in penaltyDetail.timeline" :key="idx" :timestamp="item.time" class="modern-timeline-item">
                        <div class="text-sm">{{ item.text }} <span v-if="item.operator" class="text-gray-500">— {{ item.operator }}</span></div>
                      </el-timeline-item>
                    </el-timeline>
                  </div>
                </div>
              </div>
            </el-drawer>
            <!-- 处罚申诉弹窗 -->
            <el-dialog v-model="penaltyAppealVisible" title="处罚申诉" width="460px" class="modern-dialog">
              <div class="space-y-4">
                <div>
                  <el-input v-model="penaltyAppealReason" type="textarea" placeholder="请填写申诉理由" :rows="4" class="modern-textarea" />
                </div>
                <div>
                  <label class="text-gray-600 text-sm font-medium block mb-2">上传申诉材料（最多可多选）</label>
                  <input type="file" multiple accept="image/*" @change="onPenaltyAppealFiles" class="modern-file-input" />
                  <div class="flex gap-3 mt-3 flex-wrap" v-if="penaltyAppealImages.length">
                    <img v-for="(url, i) in penaltyAppealImages" :key="i" :src="url" class="w-20 h-20 object-cover rounded-lg shadow-sm border" />
                  </div>
                </div>
              </div>
              <template #footer>
                <el-button @click="penaltyAppealVisible = false" class="modern-btn-secondary">取消</el-button>
                <el-button class="modern-btn-primary" :disabled="!penaltyAppealReason" @click="submitPenaltyAppeal">提交申诉</el-button>
              </template>
            </el-dialog>
          </div>

          <!-- 售后申请列表 -->
          <div v-if="activeAftersaleTab === 'aftersale'">
            <div class="bg-white/80 backdrop-blur-md rounded-2xl shadow-lg border border-gray-100 overflow-hidden">
              <div class="p-6 border-b border-gray-100">
                <div class="flex items-center gap-3">
                  <el-select v-model="asFilters.type" placeholder="类型" class="modern-select">
                    <el-option label="全部类型" value="all" />
                    <el-option label="退款" value="退款" />
                    <el-option label="退货" value="退货" />
                    <el-option label="投诉" value="投诉" />
                  </el-select>
                  <el-select v-model="asFilters.status" placeholder="状态" class="modern-select">
                    <el-option label="全部状态" value="all" />
                    <el-option label="待处理" value="待处理" />
                    <el-option label="已同意" value="已同意" />
                    <el-option label="已拒绝" value="已拒绝" />
                    <el-option label="协商中" value="协商中" />
                    <el-option label="已完成" value="已完成" />
                  </el-select>
                  <el-input v-model="asFilters.keyword" placeholder="订单号/用户名/电话" class="modern-input" clearable />
                  <el-button type="warning" class="modern-btn-primary" @click="loadAfterSales(1)">查询</el-button>
                  <el-button @click="resetAsFilters" class="modern-btn-secondary">重置</el-button>
                </div>
              </div>
              <el-table :data="aftersaleList" style="width: 100%" class="modern-table">
                <el-table-column prop="orderNo" label="订单号" width="200" />
                <el-table-column prop="type" label="类型" width="80" />
                <el-table-column label="用户" width="120">
                  <template #default="scope">
                    <div class="flex items-center gap-2">
                      <img v-if="scope.row.user?.avatar" :src="scope.row.user.avatar" class="w-8 h-8 rounded-full object-cover border shadow-sm" />
                      <span>{{ scope.row.user?.name }}</span>
                    </div>
                  </template>
                </el-table-column>
                <el-table-column prop="reason" label="申请原因" />
                <el-table-column prop="status" label="状态" width="100" />
                <el-table-column prop="createdAt" label="申请时间" width="160" />
                <el-table-column label="操作" width="100">
                  <template #default="scope">
                    <el-button size="small" class="modern-btn-primary" @click="openAsDetail(scope.row.id)">详情</el-button>
                  </template>
                </el-table-column>
              </el-table>
              <div class="flex justify-between items-center p-4 border-t border-gray-100">
                <div></div>
                <el-pagination
                  background
                  layout="prev, pager, next"
                  :page-size="asPageSize"
                  :current-page="asPage"
                  :total="asTotal"
                  @current-change="loadAfterSales"
                  :pager-count="5"
                  class="modern-pagination"
                />
              </div>
            </div>
            <!-- 详情抽屉 -->
            <el-drawer v-model="asDetailVisible" title="售后详情" size="600px" direction="rtl" class="modern-drawer">
              <div v-if="asDetail" class="p-6">
                <div class="space-y-4">
                  <div class="bg-gray-50 rounded-xl p-4">
                    <div class="grid grid-cols-2 gap-4 text-sm">
                      <div><b class="text-gray-600">订单号：</b>{{ asDetail.orderNo }}</div>
                      <div><b class="text-gray-600">类型：</b>{{ asDetail.type }}</div>
                      <div><b class="text-gray-600">状态：</b>{{ asDetail.status }}</div>
                      <div><b class="text-gray-600">申请时间：</b>{{ asDetail.createdAt }}</div>
                      <div class="col-span-2"><b class="text-gray-600">用户：</b>{{ asDetail.user?.name }}（{{ asDetail.user?.phone }}）</div>
                    </div>
                  </div>
                  <div class="bg-gray-50 rounded-xl p-4">
                    <b class="text-gray-600 block mb-2">申请原因：</b>
                    <p class="text-sm">{{ asDetail.reason }}</p>
                    <p v-if="asDetail.detail" class="text-sm text-gray-500 mt-1">{{ asDetail.detail }}</p>
                  </div>
                  <div v-if="asDetail.evidenceImages && asDetail.evidenceImages.length" class="bg-gray-50 rounded-xl p-4">
                    <b class="text-gray-600 block mb-2">凭证图片：</b>
                    <div class="flex gap-3">
                      <img v-for="(img, idx) in asDetail.evidenceImages" :key="idx" :src="img" class="w-24 h-24 object-cover rounded-lg shadow-sm" />
                    </div>
                  </div>
                  <div class="bg-gray-50 rounded-xl p-4">
                    <b class="text-gray-600 block mb-2">处理进度：</b>
                    <el-timeline class="modern-timeline">
                      <el-timeline-item v-for="(item, idx) in asDetail.timeline" :key="idx" :timestamp="item.time" class="modern-timeline-item">
                        <div class="text-sm">{{ item.text }} <span v-if="item.operator" class="text-gray-500">— {{ item.operator }}</span></div>
                      </el-timeline-item>
                    </el-timeline>
                  </div>
                  <div v-if="asDetail.status === '待处理' || asDetail.status === '协商中'" class="bg-orange-50 rounded-xl p-4 border border-orange-200">
                    <b class="text-gray-600 block mb-3">处理操作：</b>
                    <el-radio-group v-model="decision.action" class="mb-3">
                      <el-radio label="approve" class="modern-radio">同意</el-radio>
                      <el-radio label="reject" class="modern-radio">拒绝</el-radio>
                      <el-radio label="negotiate" class="modern-radio">协商</el-radio>
                    </el-radio-group>
                    <el-input v-model="decision.remark" placeholder="处理意见（必填）" class="mb-3 modern-input" />
                    <el-input v-if="decision.action === 'approve'" v-model.number="decision.refundAmount" type="number" placeholder="退款金额（可选）" class="mb-3 modern-input" />
                    <el-input v-if="decision.action === 'negotiate'" v-model="decision.nextContactAt" type="datetime-local" placeholder="下次联系时间（可选）" class="mb-3 modern-input" />
                    <el-button class="modern-btn-primary" :disabled="!decision.action || !decision.remark" @click="submitDecision">提交处理</el-button>
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
                  <el-select v-model="reviewFilters.rating" placeholder="星级" clearable class="modern-select">
                    <el-option v-for="n in 5" :key="n" :label="n + '星'" :value="n" />
                  </el-select>
                  <el-select v-model="reviewFilters.replied" placeholder="回复状态" clearable class="modern-select">
                    <el-option label="全部" :value="''" />
                    <el-option label="已回复" :value="true" />
                    <el-option label="未回复" :value="false" />
                  </el-select>
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
                      <img v-if="scope.row.user?.avatar" :src="scope.row.user.avatar" class="w-8 h-8 rounded-full object-cover border shadow-sm" />
                      <span>{{ scope.row.user?.name }}</span>
                    </div>
                  </template>
                </el-table-column>
                <el-table-column prop="rating" label="星级" width="120">
                  <template #default="scope">
                    <span class="text-yellow-400 text-lg tracking-wider">{{ '★'.repeat(scope.row.rating) }}{{ '☆'.repeat(5-scope.row.rating) }}</span>
                  </template>
                </el-table-column>
                <el-table-column prop="content" label="内容" />
                <el-table-column prop="createdAt" label="时间" width="160" />
                <el-table-column label="状态" width="90">
                  <template #default="scope">
                    <span v-if="scope.row.reply" class="px-2 py-1 rounded-full text-xs bg-green-100 text-green-600">已回复</span>
                    <span v-else class="px-2 py-1 rounded-full text-xs bg-gray-100 text-gray-600">未回复</span>
                  </template>
                </el-table-column>
                <el-table-column label="操作" width="120">
                  <template #default="scope">
                    <div class="flex items-center gap-1">
                      <el-button size="small" class="modern-btn-primary" @click="openReplyDialog(scope.row)">回复</el-button>
                    </div>
                  </template>
                </el-table-column>
              </el-table>
              <div class="flex justify-between items-center p-4 border-t border-gray-100">
                <div></div>
                <el-pagination
                  background
                  layout="prev, pager, next"
                  :page-size="reviewPageSize"
                  :current-page="reviewPage"
                  :total="reviewTotal"
                  @current-change="fetchReviews"
                  :pager-count="5"
                  class="modern-pagination"
                />
              </div>
            </div>
            <!-- 回复弹窗 -->
            <el-dialog v-model="replyDialogVisible" title="回复评论" width="520px" class="modern-dialog">
              <div class="space-y-4">
                <div>
                  <span class="font-bold text-gray-700 text-sm">历史聊天记录：</span>
                  <div id="reply-chat-container" class="mt-2 max-h-60 overflow-y-auto rounded-xl border border-gray-100 bg-gray-50 p-3 space-y-2">
                    <div v-if="chatLoading" class="text-center text-gray-500 text-sm py-4">加载中...</div>
                    <template v-else>
                      <div v-if="!chatMessages.length" class="text-center text-gray-400 text-sm py-4">暂无聊天记录</div>
                      <div v-for="(msg, idx) in chatMessages" :key="idx" class="flex" :class="msg.sender === 'merchant' ? 'justify-end' : 'justify-start'">
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
                    <el-tag
                      v-for="(phrase, idx) in quickPhrases"
                      :key="idx"
                      size="small"
                      class="modern-tag"
                      @click="insertToReply(phrase)"
                    >{{ phrase }}</el-tag>
                  </div>
                </div>
                <div>
                  <span class="font-bold text-gray-700 text-sm">表情：</span>
                  <div class="flex flex-wrap gap-2 mt-2">
                    <span
                      v-for="(emoji, idx) in emojis"
                      :key="emoji"
                      @click="insertToReply(emoji)"
                      class="text-2xl cursor-pointer hover:scale-110 transition-transform"
                    >{{ emoji }}</span>
                  </div>
                </div>
                <el-input
                  id="reply-content-textarea"
                  v-model="replyContent"
                  type="textarea"
                  placeholder="回复内容"
                  :rows="4"
                  class="modern-textarea"
                />
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
import { ref, reactive, onMounted, nextTick } from 'vue';
import { Bell, House, List, Ticket, Warning, User } from '@element-plus/icons-vue';
import { useRouter, useRoute } from 'vue-router';
import { replyReview, getChatMessages, getReviewList, getPenaltyList, getPenaltyDetail, appealPenalty, type Review } from '@/services/merchant_api';
import type { ChatMessage } from '@/services/merchant_api';
// 1. 引入接口和类型
import type { AfterSaleApplication, AfterSaleListParams } from '@/services/merchant_api';
import { getAfterSaleList, getAfterSaleDetail, decideAfterSale } from '@/services/merchant_api';
import { type PenaltyRecord } from '@/services/merchant_api';

const activeMenu = ref('aftersale');
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

// 处罚记录（支持后端 + 本地样例回退）
const penaltyList = ref<PenaltyRecord[]>([]);
const samplePenaltyList: PenaltyRecord[] = [
  {
    id: 'PEN20241201001',
    reason: '食品安全问题',
    time: '2024-11-15 16:30:00',
    merchantAction: '整改厨房卫生',
    platformAction: '警告处理',
    status: '未申诉',
    amount: 500,
    timeline: [
      { time: '2024-11-15 16:30:00', text: '平台下发处罚决定', operator: '平台' }
    ]
  },
  {
    id: 'PEN20241201002',
    reason: '超时配送',
    time: '2024-11-20 10:15:00',
    merchantAction: '加强配送管理',
    platformAction: '扣除信用分',
    status: '申诉中',
    amount: 100,
    timeline: [
      { time: '2024-11-20 10:15:00', text: '平台下发处罚决定', operator: '平台' },
      { time: '2024-11-20 11:00:00', text: '商家发起申诉', operator: '商家A' }
    ]
  }
];
const penaltyFilters = reactive<{ status: '' | '未申诉' | '申诉中' | '已处理'; keyword: string }>({ status: '', keyword: '' });
const penaltyDetailVisible = ref(false);
const penaltyDetail = ref<PenaltyRecord | null>(null);

async function loadPenalties() {
  const params: { status?: '未申诉' | '申诉中' | '已处理'; keyword?: string } = {};
  if (penaltyFilters.status) params.status = penaltyFilters.status;
  if (penaltyFilters.keyword) params.keyword = penaltyFilters.keyword.trim();
  try {
    const list = await getPenaltyList(params);
    if (Array.isArray(list) && list.length > 0) {
      penaltyList.value = list;
      return;
    }
    usePenaltySampleFallback();
  } catch {
    usePenaltySampleFallback();
  }
}

function usePenaltySampleFallback() {
  let list = samplePenaltyList.slice();
  if (penaltyFilters.status) list = list.filter(p => p.status === penaltyFilters.status);
  if (penaltyFilters.keyword) {
    const kw = penaltyFilters.keyword.trim();
    list = list.filter(p => p.id.includes(kw) || p.reason.includes(kw));
  }
  penaltyList.value = list;
}

async function openPenaltyDetail(row: PenaltyRecord) {
  try {
    penaltyDetail.value = await getPenaltyDetail(row.id);
  } catch {
    penaltyDetail.value = row;
  }
  penaltyDetailVisible.value = true;
}

// 处罚申诉弹窗
const penaltyAppealVisible = ref(false);
const penaltyAppealReason = ref('');
const penaltyAppealImages = ref<string[]>([]);
let penaltyAppealTarget: PenaltyRecord | null = null;

function openPenaltyAppeal(row: PenaltyRecord) {
  penaltyAppealTarget = row;
  penaltyAppealReason.value = '';
  penaltyAppealImages.value = [];
  penaltyAppealVisible.value = true;
}
function onPenaltyAppealFiles(ev: Event) {
  const input = ev.target as HTMLInputElement;
  if (!input.files) return;
  // 本地模拟：使用 object URL 预览
  for (const f of Array.from(input.files)) {
    const url = URL.createObjectURL(f);
    penaltyAppealImages.value.push(url);
  }
  (ev.target as HTMLInputElement).value = '';
}
async function submitPenaltyAppeal() {
  if (!penaltyAppealTarget || !penaltyAppealReason.value) return;
  try {
    await appealPenalty(penaltyAppealTarget.id, penaltyAppealReason.value, penaltyAppealImages.value);
  } finally {
    penaltyAppealVisible.value = false;
    await loadPenalties();
  }
}

// 评论管理增强
const allReviews: Review[] = [
  {
    id: 1,
    orderNo: 'ORD20240601001',
    user: { name: '美食达人', phone: '13800000001', avatar: 'https://randomuser.me/api/portraits/men/32.jpg' },
    rating: 5,
    content: '菜品新鲜美味，配送很快，五星好评！',
    images: ['https://images.unsplash.com/photo-1504674900247-0877df9cc836?w=80'],
    createdAt: '2024-06-01 12:30:00',
    reply: { content: '商家回复：感谢您的支持，欢迎再次光临！' }
  },
  {
    id: 2,
    orderNo: 'ORD20240601002',
    user: { name: '吃货小王', phone: '13800000002', avatar: 'https://randomuser.me/api/portraits/women/44.jpg' },
    rating: 3,
    content: '味道一般，分量偏少。',
    createdAt: '2024-06-01 13:10:00'
  },
  {
    id: 3,
    orderNo: 'ORD20240601003',
    user: { name: '匿名用户', phone: '13800000003' },
    rating: 1,
    content: '送餐太慢了，菜都凉了。',
    createdAt: '2024-06-01 14:00:00'
  }
];
const reviews = ref<Review[]>([]);
const reviewPage = ref(1);
const reviewPageSize = ref(10);
const reviewTotal = ref(3);
const reviewFilters = reactive({
  rating: undefined as number | undefined,
  replied: '' as boolean | string,
  keyword: ''
});

async function fetchReviews(page = 1) {
  reviewPage.value = page;
  try {
    const params: any = {
      page: reviewPage.value,
      pageSize: reviewPageSize.value,
      rating: reviewFilters.rating || undefined,
      replied: reviewFilters.replied === '' ? undefined : !!reviewFilters.replied,
      keyword: reviewFilters.keyword || undefined
    };
    const res = await getReviewList(params);
    reviews.value = res.list;
    reviewTotal.value = res.total;
  } catch {
    // 后端不可用时退回到本地过滤
    let filtered = allReviews.slice();
    if (reviewFilters.rating) {
      filtered = filtered.filter(r => r.rating === reviewFilters.rating);
    }
    if (reviewFilters.replied !== null && reviewFilters.replied !== '') {
      filtered = filtered.filter(r => reviewFilters.replied ? !!r.reply : !r.reply);
    }
    if (reviewFilters.keyword) {
      const kw = reviewFilters.keyword.trim();
      filtered = filtered.filter(r =>
        r.content.includes(kw) ||
        r.orderNo.includes(kw) ||
        (r.user?.name && r.user.name.includes(kw))
      );
    }
    reviewTotal.value = filtered.length;
    reviews.value = filtered.slice((reviewPage.value - 1) * reviewPageSize.value, reviewPage.value * reviewPageSize.value);
  }
}
function resetReviewFilters() {
  reviewFilters.rating = undefined;
  reviewFilters.replied = '';
  reviewFilters.keyword = '';
  fetchReviews(1);
}
// 回复
const replyDialogVisible = ref(false);
const replyContent = ref('');
const replyReviewId = ref<number | null>(null);
const currentReview = ref<Review | null>(null);
const chatMessages = ref<ChatMessage[]>([]);
const chatLoading = ref(false);
const activeChatOrderNo = ref<string | null>(null);

async function loadChatHistory(orderNo: string) {
  chatLoading.value = true;
  try {
    const apiMessages = await getChatMessages(orderNo);
    const synthetic: ChatMessage[] = [];
    if (currentReview.value) {
      // 用户最开始的评论
      synthetic.push({ sender: 'user', content: currentReview.value.content, time: currentReview.value.createdAt });
      // 如果已有商家回复，也一起展示
      if (currentReview.value.reply?.content) {
        synthetic.push({ sender: 'merchant', content: currentReview.value.reply.content, time: currentReview.value.createdAt });
      }
    }
    chatMessages.value = [...synthetic, ...apiMessages];
  } catch (err) {
    const fallback: ChatMessage[] = [];
    if (currentReview.value) {
      fallback.push({ sender: 'user', content: currentReview.value.content, time: currentReview.value.createdAt });
      if (currentReview.value.reply?.content) {
        fallback.push({ sender: 'merchant', content: currentReview.value.reply.content, time: currentReview.value.createdAt });
      }
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
  loadChatHistory(review.orderNo);
}

async function submitReply() {
  if (!replyReviewId.value || !replyContent.value) return;
  await replyReview(replyReviewId.value, replyContent.value);
  // 发送成功后，追加到聊天记录中并滚动到底部
  const newMsg: ChatMessage = { sender: 'merchant', content: replyContent.value, time: new Date().toLocaleString() };
  chatMessages.value.push(newMsg);
  // 更新本地评论的回复状态（用于“已回复”标记）
  const target = allReviews.find(r => r.id === replyReviewId.value);
  if (target) {
    target.reply = { content: replyContent.value };
  }
  fetchReviews(reviewPage.value);
  replyContent.value = '';
  nextTick(() => {
    const el = document.getElementById('reply-chat-container');
    if (el) el.scrollTop = el.scrollHeight;
  });
}
onMounted(() => {
  fetchReviews();
  loadPenalties();
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
  '👍','🙏','👏','🎉','🌟','🍽️','🍔','🍟','🍕','🍜','🍣','🍦','🍰','🥤','🥟','🥗','🥩','🥚','🥛'
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
  type: 'all' as 'all' | '退款' | '退货' | '投诉',
  status: 'all' as 'all' | '待处理' | '已同意' | '已拒绝' | '协商中' | '已完成',
  keyword: ''
});

// 本地参考用样例数据（后端无数据或联调前展示）
const sampleAfterSaleList: AfterSaleApplication[] = [
  {
    id: 101,
    orderNo: 'ORD20240602001',
    type: '退款',
    user: { name: '赵六', phone: '13800000006', avatar: 'https://randomuser.me/api/portraits/men/12.jpg' },
    reason: '口味不合适，申请退款',
    detail: '口味偏咸，孩子不太能接受',
    status: '待处理',
    refundAmount: 28.8,
    evidenceImages: ['https://images.unsplash.com/photo-1550547660-d9450f859349?w=240'],
    createdAt: '2024-06-02 11:20:00',
    timeline: [
      { time: '2024-06-02 11:20:00', text: '用户提交退款申请', operator: '赵六' }
    ]
  },
  {
    id: 102,
    orderNo: 'ORD20240602002',
    type: '退货',
    user: { name: '钱七', phone: '13800000007', avatar: 'https://randomuser.me/api/portraits/women/52.jpg' },
    reason: '打包盒破损，汤洒出',
    detail: '收到时汤汁外漏，需退货处理',
    status: '协商中',
    refundAmount: 18.5,
    evidenceImages: ['https://images.unsplash.com/photo-1541696432-82c6da8ce7bf?w=240'],
    createdAt: '2024-06-02 12:05:00',
    timeline: [
      { time: '2024-06-02 12:05:00', text: '用户提交退货申请', operator: '钱七' },
      { time: '2024-06-02 12:20:00', text: '商家发起协商', operator: '商家A' }
    ]
  },
  {
    id: 103,
    orderNo: 'ORD20240602003',
    type: '投诉',
    user: { name: '孙二', phone: '13800000008', avatar: 'https://randomuser.me/api/portraits/men/45.jpg' },
    reason: '配送态度不佳',
    status: '已同意',
    createdAt: '2024-06-02 12:18:00',
    timeline: [
      { time: '2024-06-02 12:18:00', text: '用户提交投诉', operator: '孙二' },
      { time: '2024-06-02 12:30:00', text: '商家同意投诉并反馈平台', operator: '商家A' }
    ]
  },
  {
    id: 104,
    orderNo: 'ORD20240602004',
    type: '退款',
    user: { name: '周九', phone: '13800000009', avatar: 'https://randomuser.me/api/portraits/women/68.jpg' },
    reason: '餐品分量不足，申请部分退款',
    status: '已拒绝',
    refundAmount: 5,
    createdAt: '2024-06-02 12:40:00',
    timeline: [
      { time: '2024-06-02 12:40:00', text: '用户申请部分退款', operator: '周九' },
      { time: '2024-06-02 12:55:00', text: '商家拒绝申请', operator: '商家A' }
    ]
  },
  {
    id: 105,
    orderNo: 'ORD20240602005',
    type: '退货',
    user: { name: '吴十', phone: '13800000010', avatar: 'https://randomuser.me/api/portraits/men/28.jpg' },
    reason: '送错餐品，申请退货退款',
    detail: '点了牛肉饭送成鸡肉饭',
    status: '已完成',
    refundAmount: 32,
    evidenceImages: ['https://images.unsplash.com/photo-1498579150354-977475b7ea0b?w=240'],
    createdAt: '2024-06-02 13:05:00',
    timeline: [
      { time: '2024-06-02 13:05:00', text: '用户提交退货申请', operator: '吴十' },
      { time: '2024-06-02 13:30:00', text: '商家处理完成', operator: '商家A' }
    ]
  }
];

async function loadAfterSales(page = 1) {
  asPage.value = page;
  const params: AfterSaleListParams = {
    page: asPage.value,
    pageSize: asPageSize.value,
    type: asFilters.type === 'all' ? undefined : asFilters.type,
    status: asFilters.status === 'all' ? undefined : asFilters.status,
    keyword: asFilters.keyword || undefined
  };
  try {
    const res = await getAfterSaleList(params);
    if (Array.isArray(res.list) && res.list.length > 0) {
      aftersaleList.value = res.list;
      asTotal.value = res.total;
      return;
    }
    // 若后端返回空列表，则使用本地样例做占位
    useSampleFallback();
  } catch {
    // 后端不可用时使用本地样例
    useSampleFallback();
  }
}

function useSampleFallback() {
  let filtered = sampleAfterSaleList.slice();
  if (asFilters.type !== 'all') filtered = filtered.filter(a => a.type === asFilters.type);
  if (asFilters.status !== 'all') filtered = filtered.filter(a => a.status === asFilters.status);
  if (asFilters.keyword) {
    const kw = asFilters.keyword.trim();
    filtered = filtered.filter(a =>
      a.orderNo.includes(kw) ||
      (a.user?.name && a.user.name.includes(kw)) ||
      (a.user?.phone && a.user.phone.includes(kw))
    );
  }
  asTotal.value = filtered.length;
  aftersaleList.value = filtered.slice((asPage.value - 1) * asPageSize.value, asPage.value * asPageSize.value);
}
function resetAsFilters() {
  asFilters.type = 'all';
  asFilters.status = 'all';
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
  } catch (e) {
    // 后端不可用或无此记录时，回退到本地样例
    asDetail.value = sampleAfterSaleList.find(a => a.id === id) || null;
  }
}
function clearDecision() {
  decision.action = '';
  decision.remark = '';
  decision.refundAmount = undefined;
  decision.nextContactAt = undefined;
}
const decision = reactive<{
  action: string,
  remark: string,
  refundAmount?: number,
  nextContactAt?: string
}>({
  action: '',
  remark: ''
});
async function submitDecision() {
  if (!asDetail.value || !decision.action) return;
  const isSample = sampleAfterSaleList.some(a => a.id === asDetail.value?.id);
  if (isSample) {
    // 本地样例模拟流程
    const target = sampleAfterSaleList.find(a => a.id === asDetail.value!.id);
    if (target) {
      if (decision.action === 'approve') {
        target.status = '已同意';
        target.timeline = target.timeline || [];
        target.timeline.push({ time: new Date().toLocaleString(), text: '商家同意申请', operator: '商家A' });
        if (typeof decision.refundAmount === 'number') target.refundAmount = decision.refundAmount;
      } else if (decision.action === 'reject') {
        target.status = '已拒绝';
        target.timeline = target.timeline || [];
        target.timeline.push({ time: new Date().toLocaleString(), text: '商家拒绝申请', operator: '商家A' });
      } else if (decision.action === 'negotiate') {
        target.status = '协商中';
        target.timeline = target.timeline || [];
        target.timeline.push({ time: new Date().toLocaleString(), text: '商家发起协商', operator: '商家A' });
      }
    }
    // 以本地样例刷新展示
    useSampleFallback();
    asDetail.value = target ? { ...target } : null;
    clearDecision();
    return;
  }

  // 正常后端流程
  await decideAfterSale(asDetail.value.id, decision.action as any, {
    remark: decision.remark,
    refundAmount: decision.refundAmount,
    nextContactAt: decision.nextContactAt
  });
  await loadAfterSales(asPage.value);
  asDetail.value = await getAfterSaleDetail(asDetail.value.id);
  clearDecision();
}
onMounted(() => {
  loadAfterSales(1);
});

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