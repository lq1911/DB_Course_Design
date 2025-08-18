<template>

    <div class="min-h-screen bg-gradient-to-br from-orange-50 to-orange-100 flex items-center justify-center py-8">
        <div class="w-full max-w-6xl bg-white rounded-2xl shadow-2xl overflow-hidden my-8">
            <div class="flex min-h-[800px]">


                <!-- 左侧品牌展示区 -->
                <div
                    class="hidden lg:flex lg:w-1/2 bg-gradient-to-br from-orange-500 to-orange-600 relative overflow-hidden">
                    <div class="absolute inset-0 bg-black bg-opacity-20"></div>
                    <img src="@/assets/food-delivery-login.jpg" alt="外卖配送服务"
                        class="w-full h-full object-cover object-top" />
                    <div class="absolute inset-0 flex flex-col justify-center items-center text-white p-12 z-10">
                        <div class="text-center">
                            <h1 class="text-4xl font-bold mb-4">美食外卖平台</h1>
                            <p class="text-xl opacity-90 mb-8">连接美食与生活，让每一餐都充满惊喜</p>
                            <div class="flex items-center justify-center space-x-8">
                                <div class="text-center">
                                    <i class="fas fa-store text-3xl mb-2"></i>
                                    <p class="text-sm">优质商家</p>
                                </div>
                                <div class="text-center">
                                    <i class="fas fa-motorcycle text-3xl mb-2"></i>
                                    <p class="text-sm">快速配送</p>
                                </div>
                                <div class="text-center">
                                    <i class="fas fa-heart text-3xl mb-2"></i>
                                    <p class="text-sm">贴心服务</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <!-- 右侧登录注册区 -->
                <div class="w-full lg:w-1/2 p-8 lg:p-12 flex flex-col">
                    <div class="max-w-md mx-auto w-full overflow-y-auto">

                        <!-- Logo -->
                        <div class="text-center mb-8">
                            <div
                                class="inline-flex items-center justify-center w-16 h-16 bg-orange-100 rounded-full mb-4">
                                <i class="fas fa-utensils text-2xl text-orange-600"></i>
                            </div>
                            <h2 class="text-3xl font-bold text-gray-900">欢迎使用</h2>
                            <p class="text-gray-600 mt-2">请选择您的身份并完成登录或注册</p>
                        </div>


                        <!-- Tab 切换 -->
                        <div class="flex bg-gray-100 rounded-lg p-1 mb-6">
                            <button @click="activeTab = 'login'"
                                :class="{ 'bg-white shadow-sm text-orange-600': activeTab === 'login', 'text-gray-600': activeTab !== 'login' }"
                                class="flex-1 py-2 px-4 rounded-md text-sm font-medium transition-all duration-200 cursor-pointer whitespace-nowrap rounded-lg">
                                登录
                            </button>
                            <button @click="activeTab = 'register'"
                                :class="{ 'bg-white shadow-sm text-orange-600': activeTab === 'register', 'text-gray-600': activeTab !== 'register' }"
                                class="flex-1 py-2 px-4 rounded-md text-sm font-medium transition-all duration-200 cursor-pointer whitespace-nowrap rounded-lg">
                                注册
                            </button>
                        </div>



                        <!-- 角色选择 -->
                        <div class="mb-6">
                            <p class="text-sm font-medium text-gray-700 mb-3">选择您的身份</p>
                            <div class="grid grid-cols-2 gap-3">
                                <label v-for="role in roles" :key="role.value"
                                    :class="{ 'border-orange-500 bg-orange-50': selectedRole === role.value, 'border-gray-200 hover:border-gray-300': selectedRole !== role.value }"
                                    class="flex items-center p-3 border-2 rounded-lg cursor-pointer transition-all duration-200">
                                    <input type="radio" :value="role.value" v-model="selectedRole" class="sr-only" />
                                    <i :class="role.icon" class="text-lg mr-3 text-orange-600"></i>
                                    <span class="text-sm font-medium text-gray-900">{{ role.label }}</span>
                                </label>
                            </div>
                        </div>


                        <!-- 登录表单 -->
                        <form v-if="activeTab === 'login'" @submit.prevent="handleLogin" class="space-y-4">
                            <div>

                                <label class="block text-sm font-medium text-gray-700 mb-1">手机号 / 邮箱</label>

                                <div class="relative">
                                    <input type="text" v-model="loginForm.account" placeholder="请输入手机号或邮箱"
                                        class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />

                                    <i
                                        class="fas fa-user absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400"></i>
                                </div>
                            </div>
                            <div>
                                <label class="block text-sm font-medium text-gray-700 mb-1">密码</label>
                                <div class="relative">
                                    <input :type="showPassword ? 'text' : 'password'" v-model="loginForm.password"
                                        placeholder="请输入密码"
                                        class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm pr-10" />
                                    <button type="button" @click="showPassword = !showPassword"
                                        class="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600 cursor-pointer">
                                        <i :class="showPassword ? 'fas fa-eye-slash' : 'fas fa-eye'"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="flex items-center justify-between">
                                <label class="flex items-center">
                                    <input type="checkbox" v-model="rememberPassword"
                                        class="rounded border-gray-300 text-orange-600 focus:ring-orange-500">
                                    <span class="ml-2 text-sm text-gray-600">记住密码</span>
                                </label>
                                <a href="#"
                                    class="text-sm text-orange-600 hover:text-orange-500 cursor-pointer">忘记密码？</a>
                            </div>
                            <button type="submit" :disabled="isLoading"
                                class="w-full bg-orange-600 text-white py-3 px-4 rounded-lg hover:bg-orange-700 focus:ring-2 focus:ring-orange-500 focus:ring-offset-2 font-medium transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed cursor-pointer whitespace-nowrap rounded-lg">
                                <i v-if="isLoading" class="fas fa-spinner fa-spin mr-2"></i>
                                {{ isLoading ? '登录中...' : '登录' }}
                            </button>
                        </form>



                        <!-- 注册表单 -->
                        <form v-if="activeTab === 'register'" @submit.prevent="handleRegister" class="space-y-4">


                            <!-- 注册-基础信息 -->

                            <div class="grid grid-cols-2 gap-4">
                                <div>
                                    <label class="block text-sm font-medium text-gray-700 mb-1">昵称</label>
                                    <input type="text" v-model="registerForm.nickname" placeholder="请输入昵称"
                                        class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                </div>
                                <!-- 新增：真实姓名输入框 (仅当非消费者时显示) -->
                                <div v-if="selectedRole !== 'consumer'">
                                    <label class="block text-sm font-medium text-gray-700 mb-1">真实姓名</label>
                                    <input type="text" v-model="registerForm.realName" placeholder="请输入真实姓名"
                                        class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                </div>
                                <div>
                                    <label class="block text-sm font-medium text-gray-700 mb-1">性别</label>
                                    <select v-model="registerForm.gender"
                                        class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm appearance-none bg-white cursor-pointer">
                                        <option value="">请选择</option>
                                        <option value="male">男</option>
                                        <option value="female">女</option>
                                    </select>
                                </div>
                            </div>

                            <div>
                                <label class="block text-sm font-medium text-gray-700 mb-1">手机号码</label>
                                <div class="flex space-x-2">
                                    <input type="tel" v-model="registerForm.phone" placeholder="请输入手机号码"
                                        class="flex-1 px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                    <button type="button" @click="sendVerificationCode" :disabled="codeCountdown > 0"
                                        class="px-4 py-3 bg-gray-100 text-gray-700 rounded-lg hover:bg-gray-200 transition-colors duration-200 text-sm font-medium cursor-pointer whitespace-nowrap rounded-lg">
                                        {{ codeCountdown > 0 ? `${codeCountdown}s` : '获取验证码' }}
                                    </button>
                                </div>
                            </div>
                            <div>
                                <label class="block text-sm font-medium text-gray-700 mb-1">验证码</label>
                                <input type="text" v-model="registerForm.verificationCode" placeholder="请输入验证码"
                                    class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                            </div>
                            <div>
                                <label class="block text-sm font-medium text-gray-700 mb-1">邮箱</label>
                                <input type="email" v-model="registerForm.email" placeholder="请输入邮箱地址"
                                    class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                            </div>
                            <div class="grid grid-cols-2 gap-4">
                                <div>
                                    <label class="block text-sm font-medium text-gray-700 mb-1">设置密码</label>
                                    <div class="relative">
                                        <input :type="showRegisterPassword ? 'text' : 'password'"
                                            v-model="registerForm.password" placeholder="请设置密码"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm pr-10" />
                                        <button type="button" @click="showRegisterPassword = !showRegisterPassword"
                                            class="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 hover:text-gray-600 cursor-pointer">
                                            <i :class="showRegisterPassword ? 'fas fa-eye-slash' : 'fas fa-eye'"></i>
                                        </button>
                                    </div>
                                </div>
                                <div>
                                    <label class="block text-sm font-medium text-gray-700 mb-1">确认密码</label>
                                    <input type="password" v-model="registerForm.confirmPassword" placeholder="请确认密码"
                                        class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                </div>
                            </div>
                            <div>
                                <label class="block text-sm font-medium text-gray-700 mb-1">生日</label>
                                <input type="date" v-model="registerForm.birthday"
                                    class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                            </div>


                            <!-- 角色特殊信息 -->
                            <div v-if="selectedRole !== 'consumer'" class="border-t pt-4 mt-6">
                                <h3 class="text-lg font-medium text-gray-900 mb-4">{{ getRoleSpecificTitle() }}</h3>


                                <!-- 骑手特殊信息 -->
                                <div v-if="selectedRole === 'rider'" class="space-y-4">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">配送车型</label>
                                        <select v-model="riderInfo.vehicleType"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                            <option value="">请选择配送车型</option>
                                            <option value="electric_bike">电动自行车</option>
                                            <option value="motorcycle">摩托车</option>
                                            <option value="car">小型汽车</option>
                                        </select>
                                    </div>
                                </div>

                                <!-- 管理员特殊信息 -->
                                <div v-if="selectedRole === 'admin'" class="space-y-4">
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">管理对象</label>
                                        <select v-model="adminInfo.managementObject"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm">
                                            <option value="">请选择管理对象</option>
                                            <option value="riders">骑手管理</option>
                                            <option value="comments">评论审核</option>
                                            <option value="reports">举报处理</option>
                                            <option value="aftersales">售后服务</option>
                                        </select>
                                    </div>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">处理事项说明</label>
                                        <textarea v-model="adminInfo.handledItems" placeholder="请简要说明您将负责处理的主要事项"
                                            rows="3"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm resize-none"></textarea>
                                    </div>
                                </div>


                                <!-- 商家特殊信息 -->
                                <div v-if="selectedRole === 'merchant'">

                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">店铺名称</label>
                                        <input type="text" v-model="storeInfo.name" placeholder="请输入店铺名称" maxlength="50"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                    </div>
                                    <!-- 修改：将营业时间拆分为开店和关店时间 -->
                                    <div class="grid grid-cols-2 gap-4 mt-4">
                                        <div>
                                            <label class="block text-sm font-medium text-gray-700 mb-1">开店时间</label>
                                            <input type="time" v-model="storeInfo.openingTime"
                                                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                        </div>
                                        <div>
                                            <label class="block text-sm font-medium text-gray-700 mb-1">关店时间</label>
                                            <input type="time" v-model="storeInfo.closingTime"
                                                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                        </div>
                                    </div>
                                    <div class="mt-4">
                                        <label class="block text-sm font-medium text-gray-700 mb-1">店铺建立时间</label>
                                        <input type="date" v-model="storeInfo.establishmentDate"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                    </div>
                                    <div class="mt-4">
                                        <label class="block text-sm font-medium text-gray-700 mb-1">经营类别</label>
                                        <div class="relative">
                                            <button type="button" @click="showCategoryDropdown = !showCategoryDropdown"
                                                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm text-left bg-white cursor-pointer flex items-center justify-between">
                                                <span :class="{ 'text-gray-400': !storeInfo.category }">
                                                    {{ storeInfo.category || '请选择经营类别' }}
                                                </span>
                                                <i class="fas fa-chevron-down text-gray-400"></i>
                                            </button>
                                            <div v-if="showCategoryDropdown"
                                                class="absolute z-10 w-full mt-1 bg-white border border-gray-300 rounded-lg shadow-lg">
                                                <div v-for="category in categories" :key="category"
                                                    @click="selectCategory(category)"
                                                    class="px-4 py-2 hover:bg-gray-100 cursor-pointer text-sm">
                                                    {{ category }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mt-4">
                                        <label class="block text-sm font-medium text-gray-700 mb-1">店铺地址</label>
                                        <textarea v-model="storeInfo.address" placeholder="请输入详细店铺地址" rows="3"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm resize-none">
                                        </textarea>
                                    </div>
                                    <div class="mt-4">
                                        <label class="block text-sm font-medium text-gray-700 mb-1">营业执照</label>
                                        <div
                                            class="border-2 border-dashed border-gray-300 rounded-lg p-6 text-center hover:border-orange-400 transition-colors duration-200">
                                            <i class="fas fa-cloud-upload-alt text-3xl text-gray-400 mb-2"></i>
                                            <p class="text-sm text-gray-600">点击或拖拽上传营业执照</p>
                                            <p class="text-xs text-gray-500 mt-1">支持 JPG、PNG 格式，文件大小不超过 5MB</p>
                                            <input type="file" class="hidden" accept="image/*" />
                                        </div>
                                    </div>
                                </div>



                            </div>
                            <!-- 协议 -->
                            <div class="flex items-center">
                                <input type="checkbox" v-model="agreeTerms"
                                    class="rounded border-gray-300 text-orange-600 focus:ring-orange-500">
                                <span class="ml-2 text-sm text-gray-600">
                                    我已阅读并同意
                                    <span @click="showModal('agreement')"
                                        class="text-orange-600 hover:text-orange-500 cursor-pointer underline">用户协议</span>
                                    和
                                    <span @click="showModal('privacy')"
                                        class="text-orange-600 hover:text-orange-500 cursor-pointer underline">隐私政策</span>
                                </span>
                            </div>

                            <!-- 协议弹窗内容 -->
                            <div v-if="isModalVisible"
                                class="fixed inset-0 bg-black bg-opacity-60 flex items-center justify-center z-50 p-4">
                                <!-- 点击背景遮罩可以关闭弹窗 -->
                                <div @click.self="closeModal" class="absolute inset-0"></div>

                                <!-- 弹窗内容区域 -->
                                <div
                                    class="relative w-full max-w-2xl bg-white rounded-lg shadow-xl flex flex-col max-h-[90vh]">

                                    <!-- 弹窗头部 -->
                                    <div class="flex justify-between items-center p-4 border-b">
                                        <h2 class="text-xl font-semibold text-gray-800">{{ modalTitle }}</h2>
                                        <button @click="closeModal" class="text-gray-400 hover:text-gray-600">
                                            <i class="fas fa-times text-2xl"></i>
                                        </button>
                                    </div>

                                    <!-- 弹窗正文 (可滚动) -->
                                    <div class="p-6 overflow-y-auto" v-html="modalContent">
                                    </div>

                                    <!-- 弹窗底部 -->
                                    <div class="flex justify-end p-4 bg-gray-50 border-t rounded-b-lg">
                                        <button @click="closeModal"
                                            class="bg-orange-600 text-white px-6 py-2 rounded-lg hover:bg-orange-700 transition-colors duration-200">
                                            我已阅读并关闭
                                        </button>
                                    </div>

                                </div>
                            </div>

                            <button type="submit" :disabled="isLoading || !agreeTerms"
                                class="w-full bg-orange-600 text-white py-3 px-4 rounded-lg hover:bg-orange-700 focus:ring-2 focus:ring-orange-500 focus:ring-offset-2 font-medium transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed cursor-pointer whitespace-nowrap rounded-lg">
                                <i v-if="isLoading" class="fas fa-spinner fa-spin mr-2"></i>
                                {{ isLoading ? '注册中...' : '注册账号' }}
                            </button>
                        </form>


                        <!-- 第三方登录 -->
                        <div class="mt-6 pt-6 border-t border-gray-200">
                            <p class="text-center text-sm text-gray-600 mb-4">或使用第三方账号登录</p>
                            <div class="flex justify-center space-x-4">
                                <button
                                    class="flex items-center justify-center w-10 h-10 bg-red-500 text-white rounded-full hover:bg-red-600 transition-colors duration-200 cursor-pointer rounded-lg">
                                    <i class="fab fa-google"></i>
                                </button>
                                <button
                                    class="flex items-center justify-center w-10 h-10 bg-blue-600 text-white rounded-full hover:bg-blue-700 transition-colors duration-200 cursor-pointer rounded-lg">
                                    <i class="fab fa-facebook-f"></i>
                                </button>
                                <button
                                    class="flex items-center justify-center w-10 h-10 bg-green-500 text-white rounded-full hover:bg-green-600 transition-colors duration-200 cursor-pointer rounded-lg">
                                    <i class="fab fa-weixin"></i>
                                </button>
                            </div>
                        </div>

                        <!-- 底部链接 -->
                        <div class="mt-6 text-center text-sm text-gray-600">
                            <a href="#" class="hover:text-orange-600 cursor-pointer">返回首页</a>
                            <span class="mx-2">|</span>
                            <a href="#" class="hover:text-orange-600 cursor-pointer">联系客服</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script lang="ts" setup>
import { ref, reactive } from 'vue';
import api from '@/api/api'; // 导入我们的 API 服务
import axios from 'axios';

// 响应式数据
const activeTab = ref('login');
const selectedRole = ref('consumer');
const showPassword = ref(false);
const showRegisterPassword = ref(false);
const rememberPassword = ref(false);
const agreeTerms = ref(false);
const isLoading = ref(false);
const codeCountdown = ref(0);
const showCategoryDropdown = ref(false);
const isModalVisible = ref(false);//弹窗显示的状态
const modalTitle = ref('');//弹窗的标题
const modalContent = ref('');//弹窗的内容 
// 角色选项
const roles = [
    { value: 'admin', label: '管理员', icon: 'fas fa-user-shield' },
    { value: 'merchant', label: '商家', icon: 'fas fa-store' },
    { value: 'rider', label: '骑手', icon: 'fas fa-motorcycle' },
    { value: 'consumer', label: '消费者', icon: 'fas fa-user' }
];
// 经营类别
const categories = [
    '中式快餐', '西式快餐', '日韩料理', '甜品饮品',
    '火锅烧烤', '地方小吃', '健康轻食', '咖啡茶饮'
];
// 登录表单
const loginForm = reactive({
    account: '',
    password: ''
});
// 注册表单
const registerForm = reactive({
    nickname: '', // 限制15字符
    realName: '', // 限制10字符
    password: '', // 限制10字符
    confirmPassword: '',
    phone: '', // 限制11位数字
    email: '', // 限制30字符
    gender: '', // 2字符
    avatarUrl: '',
    birthday: '',
    isPublic: 0, // 0不公开，1公开，2仅好友
    verificationCode: ''
});
// 骑手信息
const riderInfo = reactive({
    vehicleType: '', // 配送车型 20字符
});
// 管理员信息
const adminInfo = reactive({
    managementObject: '', // 管理对象 50字符
    handledItems: '', // 处理事项
});
// 店铺信息
const storeInfo = reactive({
    name: '', // 50字符
    address: '', // 100字符
    openingTime: '', // <-- 新增
    closingTime: '', // <-- 新增
    businessHours: '', // 营业时间
    establishmentDate: '', // 店铺建立时间
    businessLicense: null,
    category: ''// 经营类别
});



// 存储不同协议的文本内容
const policies = {
    agreement: {
        title: '美食外卖平台用户服务协议',
        content: `
      <div class="space-y-6 text-gray-700 text-sm">
        <p class="text-xs text-gray-500">最后更新日期：2025年7月21日</p>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">一、 特别提示</h2>
          <p>欢迎您使用美食外卖平台！为了保障您的合法权益，请您务必审慎阅读、充分理解本协议各条款内容，特别是免除或者限制责任的条款、法律适用和争议解决条款。当您按照注册页面提示填写信息、阅读并同意本协议且完成全部注册程序后，即表示您已充分阅读、理解并接受本协议的全部内容，并与本平台达成一致，成为本平台“用户”。</p>
        </section>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">二、 账户注册与使用</h2>
          <p class="mb-2"><strong>2.1 用户资格：</strong>您确认，在您完成注册程序或以其他本平台允许的方式实际使用服务时，您应当是具备完全民事权利能力和完全民事行为能力的自然人、法人或其他组织。</p>
          <p class="mb-2"><strong>2.2 账户安全：</strong>您将对使用该账户及密码进行的一切操作及言论负完全的责任，您同意：</p>
          <ul class="list-disc list-inside space-y-1 pl-4">
            <li>妥善保管您的账户信息和密码。</li>
            <li>如发现任何非法使用用户帐号或存在安全漏洞的情况，应立即通知本平台。</li>
            <li>确保您在每个上网时段结束时，以正确步骤离开网站。本平台不能也不会对因您未能遵守本款规定而发生的任何损失或损毁负责。</li>
          </ul>
        </section>
        
        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">三、 用户行为规范</h2>
          <p class="mb-2">您承诺，在使用本平台服务的过程中，将遵守以下规定：</p>
          <ol class="list-decimal list-inside space-y-1 pl-4">
              <li>不发布任何骚扰、中伤、辱骂、恐吓、庸俗淫秽或其他任何非法的信息资料。</li>
              <li>不发布任何涉嫌侵犯他人知识产权或其他合法权益的信息。</li>
              <li>不进行虚假交易、恶意评价等扰乱平台正常交易秩序的行为。</li>
              <li>遵守所有适用的法律法规、本协议及平台发布的各项规则。</li>
          </ol>
        </section>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">四、 免责声明</h2>
          <p>由于商家原因（如菜品缺货、信息错误等）或不可抗力因素（如网络中断、自然灾害等）导致的服务中断或延迟，本平台不承担任何责任，但会尽力协助您与商家解决问题。</p>
        </section>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">五、 协议的变更与终止</h2>
          <p>本平台有权根据需要不时地制订、修改本协议及/或各类规则，并以网站公示的方式进行公告，不再单独通知予您。变更后的协议和规则一经在网站公布后，立即自动生效。如您不同意相关变更，应当立即停止使用本平台服务。</p>
        </section>
      </div>
    `
    },
    privacy: {
        title: '美食外卖平台隐私政策',
        content: `
      <div class="space-y-6 text-gray-700 text-sm">
        <p class="text-xs text-gray-500">生效日期：2025年7月21日</p>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">引言</h2>
          <p>我们深知个人信息对您的重要性，并会尽全力保护您的个人信息安全可靠。我们致力于维持您对我们的信任，恪守以下原则，保护您的个人信息：权责一致原则、目的明确原则、选择同意原则、最少够用原则、确保安全原则、主体参与原则、公开透明原则等。同时，我们承诺，我们将按业界成熟的安全标准，采取相应的安全保护措施来保护您的个人信息。</p>
        </section>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">一、 我们如何收集和使用您的个人信息</h2>
          <p class="mb-3">为了向您提供服务，我们会遵循合法、正当、必要的原则，收集和使用您的个人信息。以下是详细说明：</p>
          <div class="overflow-x-auto">
            <table class="w-full border-collapse border border-gray-300">
              <thead class="bg-gray-100">
                <tr>
                  <th class="border border-gray-300 p-2 text-left font-semibold">服务场景</th>
                  <th class="border border-gray-300 p-2 text-left font-semibold">收集的个人信息类型</th>
                  <th class="border border-gray-300 p-2 text-left font-semibold">目的</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td class="border border-gray-300 p-2">账号注册/登录</td>
                  <td class="border border-gray-300 p-2">手机号码、密码、昵称</td>
                  <td class="border border-gray-300 p-2">创建和管理您的账户</td>
                </tr>
                <tr>
                  <td class="border border-gray-300 p-2">下单与配送</td>
                  <td class="border border-gray-300 p-2">收货地址、联系人、联系电话、订单详情</td>
                  <td class="border border-gray-300 p-2">完成商品配送和售后服务</td>
                </tr>
                <tr>
                  <td class="border border-gray-300 p-2">支付结算</td>
                  <td class="border border-gray-300 p-2">支付信息、交易记录</td>
                  <td class="border border-gray-300 p-2">处理您的付款请求</td>
                </tr>
                <tr>
                  <td class="border border-gray-300 p-2">客服与售后</td>
                  <td class="border border-gray-300 p-2">您的联系方式、与我们的沟通记录</td>
                  <td class="border border-gray-300 p-2">响应您的咨询、请求和投诉</td>
                </tr>
              </tbody>
            </table>
          </div>
        </section>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">二、 我们如何共享、转让、公开披露您的个人信息</h2>
          <p class="mb-2"><strong>共享：</strong>我们不会与任何公司、组织和个人共享您的个人信息，但以下情况除外：</p>
          <ul class="list-disc list-inside space-y-1 pl-4">
            <li>获得您的明确同意后。</li>
            <li>为实现交易目的，与商家、骑手共享必要的订单信息。</li>
            <li>在法律法规要求或强制性的政府要求或司法裁定下。</li>
          </ul>
          <p class="mt-2">我们不会将您的个人信息转让给任何公司、组织和个人，但涉及合并、收购或破产清算时，如涉及到个人信息转让，我们会要求新的持有您个人信息的公司、组织继续受此隐私政策的约束。</p>
        </section>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">三、 您管理个人信息的权利</h2>
          <p>您有权访问、更正、删除您的个人信息，以及撤回已同意的授权。您可以通过“我的”-“账户设置”等功能页面进行操作，或通过联系客服来行使您的权利。</p>
        </section>

        <section>
          <h2 class="text-lg font-bold text-gray-900 mb-3">四、 联系我们</h2>
          <p>如果您对本隐私政策有任何疑问、意见或建议，可通过客服邮箱 <strong class="text-orange-600">service@yourdomain.com</strong> 与我们联系。</p>
        </section>
      </div>
    `
    }
};

// 显示弹窗的方法
const showModal = (type: keyof typeof policies) => {
    if (policies[type]) {
        modalTitle.value = policies[type].title;
        modalContent.value = policies[type].content;
        isModalVisible.value = true;
    }
};

// 关闭弹窗的方法
const closeModal = () => {
    isModalVisible.value = false;
};



// 发送验证码
const sendVerificationCode = () => {
    if (!registerForm.phone) {
        alert('请先输入手机号码');
        return;
    }
    // 模拟发送验证码
    codeCountdown.value = 60;
    const timer = setInterval(() => {
        codeCountdown.value--;
        if (codeCountdown.value <= 0) {
            clearInterval(timer);
        }
    }, 1000);
    alert('验证码已发送到您的手机');
};


// 选择经营类别
const selectCategory = (category: string) => {
    storeInfo.category = category;
    showCategoryDropdown.value = false;
};


// 处理登录
const handleLogin = async () => {
    if (!loginForm.account || !loginForm.password) {
        alert('请填写完整的登录信息');
        return;
    }
    isLoading.value = true;

    try {
        // 1. 发送登录请求
        const response = await api.login({
            account: loginForm.account,
            password: loginForm.password,
            role: selectedRole.value // 同时告诉后端以哪个角色身份登录
        });

        // 2. 处理成功响应
        // 登录成功后，后端通常会返回一个 token
        const { token, user, message } = response.data;

        // 2.1 将 token 存储起来（例如，在浏览器的 localStorage 中）
        // 这样用户刷新页面或访问其他页面时，我们能知道他已经登录了
        localStorage.setItem('authToken', token);

        alert(message || `${user.nickname}，欢迎回来！`);

        // 2.2 登录成功后跳转到主页或其他页面
        // router.push('/dashboard'); // (需要先引入 vue-router 的 useRouter)

    } catch (error) {
        // 3. 处理错误响应
        if (axios.isAxiosError(error))
            alert(error.response?.data?.error || '登录失败，账号或密码错误。');
        else {
            alert("发生未知错误，请稍后再试。")
        }

    } finally {
        // 4. 结束加载状态
        isLoading.value = false;
    }
};


// 处理注册
const handleRegister = async () => {
    // ... 省略验证代码，但您可以暂时移除对 realName 和角色信息的验证 ...
    isLoading.value = true;

    // 1. 【核心变化】只准备基础账号信息
    // 注意看，我们从 registerForm 中剔除了 realName
    const { realName, ...baseRegisterForm } = registerForm; 

    const registrationData = {
        ...baseRegisterForm,
        role: selectedRole.value,
    };

    try {
        // 2. 第一步：只发送基础信息进行注册
        const registerResponse = await api.register(registrationData);
        alert(registerResponse.data.message || '账号创建成功！现在请登录以完善资料。');

        // 3. 注册成功，自动切换到登录 Tab，让用户登录
        activeTab.value = 'login';
        // 自动填充刚注册的账号，提升体验
        loginForm.account = registerForm.phone || registerForm.email;
        loginForm.password = ''; // 清空密码框让用户输入

        // 【重要】到此，账号创建完成。
        // 真实姓名、店铺信息等，应该在用户【登录后】再提交。
        // 你可以在登录后跳转的页面（比如用户中心）提供一个表单，
        // 让用户填写 realName, storeInfo 等，然后调用 api.updateUserProfile() 等新接口。

    } catch (error) {
        if (axios.isAxiosError(error)) {
            alert(error.response?.data?.error || '注册失败，服务器返回错误。');
        } else {
            console.error('An unexpected error occurred:', error);
            alert('发生未知错误，请稍后再试。');
        }
    } finally {
        isLoading.value = false;
    }
};

// 验证注册表单
const validateRegisterForm = () => {
    // 基础验证
    if (!validateRoleSpecificInfo()) {
        return false;
    }
    if (!registerForm.nickname || registerForm.nickname.length > 15) {
        alert('请输入昵称(不超过15个字符)');
        return false;
    }
    if (selectedRole.value !== 'consumer' && !registerForm.realName) {
        alert('请输入您的真实姓名');
        return false;
    }
    if (!registerForm.phone || !/^\d{11}$/.test(registerForm.phone)) {
        alert('请输入正确的11位手机号码');
        return false;
    }
    if (!registerForm.verificationCode) {
        alert('请输入验证码');
        return false;
    }
    if (!registerForm.email || registerForm.email.length > 30) {
        alert('请输入正确的邮箱地址(不超过30个字符)');
        return false;
    }
    if (!registerForm.password || registerForm.password.length > 10) {
        alert('请设置密码(不超过10个字符)');
        return false;
    }
    if (registerForm.password !== registerForm.confirmPassword) {
        alert('两次输入的密码不一致');
        return false;
    }

    // 角色特定验证
    if (selectedRole.value === 'merchant') {
        if (!storeInfo.name || storeInfo.name.length > 50) {
            alert('请输入店铺名称(不超过50个字符)');
            return false;
        }
        if (!storeInfo.address || storeInfo.address.length > 100) {
            alert('请输入店铺地址(不超过100个字符)');
            return false;
        }
        if (!storeInfo.businessHours) {
            alert('请输入营业时间');
            return false;
        }
        if (!storeInfo.openingTime) {
            alert('请选择开店时间');
            return false;
        }
        if (!storeInfo.closingTime) {
            alert('请选择关店时间');
            return false;
        }

        if (!storeInfo.establishmentDate) {
            alert('请设置店铺建立时间');
            return false;
        }
        if (!storeInfo.establishmentDate) {
            alert('请设置店铺建立时间');
            return false;
        }
    } else if (selectedRole.value === 'rider') {
        if (!riderInfo.vehicleType || riderInfo.vehicleType.length > 20) {
            alert('请选择配送车型(不超过20个字符)');
            return false;
        }
    } else if (selectedRole.value === 'admin') {
        if (!adminInfo.managementObject || adminInfo.managementObject.length > 50) {
            alert('请输入管理对象(不超过50个字符)');
            return false;
        }
    }
    return true;
};


const getRoleSpecificTitle = () => {
    switch (selectedRole.value) {
        case 'merchant':
            return '店铺信息';
        case 'rider':
            return '骑手信息';
        case 'admin':
            return '管理员信息';
        default:
            return '';
    }
};


// 验证角色特定信息
const validateRoleSpecificInfo = () => {
    if (selectedRole.value === 'rider') {
        if (!riderInfo.vehicleType) {
            alert('请选择配送车型');
            return false;
        }
    } else if (selectedRole.value === 'admin') {
        if (!adminInfo.managementObject) {
            alert('请选择管理对象');
            return false;
        }
        if (!adminInfo.handledItems) {
            alert('请填写处理事项说明');
            return false;
        }
    }
    return true;
};
</script>



<style scoped>
/* 自定义样式 */
.login-container {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
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

/* 自定义选择框样式 */
select {
    background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'%3e%3cpath stroke='%236b7280' stroke-linecap='round' stroke-linejoin='round' stroke-width='1.5' d='m6 8 4 4 4-4'/%3e%3c/svg%3e");
    background-position: right 0.5rem center;
    background-repeat: no-repeat;
    background-size: 1.5em 1.5em;
    padding-right: 2.5rem;
}
</style>