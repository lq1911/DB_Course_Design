<template>
    <div class="min-h-screen bg-gradient-to-br from-orange-50 to-orange-100 flex items-center justify-center py-8">
        <div class="w-full max-w-6xl bg-white rounded-2xl shadow-2xl overflow-hidden my-8">
            <div class="flex min-h-[800px]">
                <!-- 左侧品牌展示区 -->
                <div
                    class="hidden lg:flex lg:w-1/2 bg-gradient-to-br from-orange-500 to-orange-600 relative overflow-hidden">
                    <div class="absolute inset-0 bg-black bg-opacity-20"></div>
                    <img src="https://readdy.ai/api/search-image?query=modern%20food%20delivery%20service%20illustration%20with%20smartphone%20app%20interface%20showing%20delicious%20meals%20and%20delivery%20person%20on%20motorcycle%20in%20vibrant%20colors%20with%20clean%20minimalist%20background%20design%20professional%20business%20style&width=600&height=700&seq=brand001&orientation=portrait"
                        alt="外卖配送服务" class="w-full h-full object-cover object-top" />
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
                            <!-- 基础信息 -->
                            <div class="grid grid-cols-2 gap-4">
                                <div>
                                    <label class="block text-sm font-medium text-gray-700 mb-1">昵称</label>
                                    <input type="text" v-model="registerForm.nickname" placeholder="请输入昵称"
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
                                    <div class="grid grid-cols-2 gap-4">
                                        <div>
                                            <label class="block text-sm font-medium text-gray-700 mb-1">预期月薪</label>
                                            <input type="number" v-model="riderInfo.monthlySalary" placeholder="请输入预期月薪"
                                                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                        </div>
                                        <div>
                                            <label class="block text-sm font-medium text-gray-700 mb-1">预计配送时间</label>
                                            <input type="text" v-model="riderInfo.avgDeliveryTime" placeholder="例如：30分钟"
                                                class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                        </div>
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
                                    <h3 class="text-lg font-medium text-gray-900 mb-4">店铺信息</h3>
                                    <div>
                                        <label class="block text-sm font-medium text-gray-700 mb-1">店铺名称</label>
                                        <input type="text" v-model="storeInfo.name" placeholder="请输入店铺名称" maxlength="50"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
                                    </div>
                                    <div class="mt-4">
                                        <label class="block text-sm font-medium text-gray-700 mb-1">营业时间</label>
                                        <input type="text" v-model="storeInfo.businessHours"
                                            placeholder="请输入营业时间，例如：周一至周五 9:00-22:00"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
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
                                                <span :class="{ 'text-gray-400': !merchantInfo.category }">
                                                    {{ merchantInfo.category || '请选择经营类别' }}
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
                                        <textarea v-model="merchantInfo.address" placeholder="请输入详细店铺地址" rows="3"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm resize-none"></textarea>
                                    </div>
                                    <div class="mt-4">
                                        <label class="block text-sm font-medium text-gray-700 mb-1">联系电话</label>
                                        <input type="tel" v-model="merchantInfo.contactPhone" placeholder="请输入店铺联系电话"
                                            class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-orange-500 focus:border-orange-500 text-sm" />
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
                                <div class="flex items-center">
                                    <input type="checkbox" v-model="agreeTerms"
                                        class="rounded border-gray-300 text-orange-600 focus:ring-orange-500">
                                    <span class="ml-2 text-sm text-gray-600">
                                        我已阅读并同意
                                        <a href="#"
                                            class="text-orange-600 hover:text-orange-500 cursor-pointer">用户协议</a>
                                        和
                                        <a href="#"
                                            class="text-orange-600 hover:text-orange-500 cursor-pointer">隐私政策</a>
                                    </span>
                                </div>
                                <button type="submit" :disabled="isLoading || !agreeTerms"
                                    class="w-full bg-orange-600 text-white py-3 px-4 rounded-lg hover:bg-orange-700 focus:ring-2 focus:ring-orange-500 focus:ring-offset-2 font-medium transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed cursor-pointer whitespace-nowrap rounded-lg">
                                    <i v-if="isLoading" class="fas fa-spinner fa-spin mr-2"></i>
                                    {{ isLoading ? '注册中...' : '注册账号' }}
                                </button>
                            </div>
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
    password: '', // 限制10字符
    confirmPassword: '',
    phone: '', // 限制11位数字
    email: '', // 限制30字符
    gender: '', // 2字符
    realName: '', // 限制6字符
    avatarUrl: '',
    birthday: '',
    isPublic: 0, // 0不公开，1公开，2仅好友
    verificationCode: ''
});
// 骑手信息
const riderInfo = reactive({
    vehicleType: '', // 配送车型 20字符
    reputationScore: 0, // 信誉积分
    totalDeliveries: 0, // 送单总量
    avgDeliveryTime: '', // 平均配送时间
    rating: 0, // 获评均分
    monthlySalary: 0 // 月薪
});
// 管理员信息
const adminInfo = reactive({
    managementObject: '', // 管理对象 50字符
    handledItems: '', // 处理事项
    avgRating: 0 // 处理事项获平均分
});
// 店铺信息
const storeInfo = reactive({
    name: '', // 50字符
    address: '', // 100字符
    businessHours: '', // 营业时间
    establishmentDate: '', // 店铺建立时间
    businessLicense: null
});
// 商家信息
const merchantInfo = reactive({
    storeName: '',
    category: '',
    address: '',
    contactPhone: '',
    businessLicense: null
});
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
    merchantInfo.category = category;
    showCategoryDropdown.value = false;
};
// 处理登录
const handleLogin = async () => {
    if (!loginForm.account || !loginForm.password) {
        alert('请填写完整的登录信息');
        return;
    }
    isLoading.value = true;
    // 模拟登录请求
    setTimeout(() => {
        isLoading.value = false;
        alert(`${getRoleLabel(selectedRole.value)}登录成功！`);
    }, 2000);
};
// 处理注册
const handleRegister = async () => {
    if (!validateRegisterForm()) {
        return;
    }
    isLoading.value = true;
    // 模拟注册请求
    setTimeout(() => {
        isLoading.value = false;
        alert(`${getRoleLabel(selectedRole.value)}注册成功！`);
    }, 2000);
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
    if (registerForm.realName && registerForm.realName.length > 6) {
        alert('姓名不能超过6个字符');
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
// 获取角色标签
const getRoleLabel = (value: string) => {
    const role = roles.find(r => r.value === value);
    return role ? role.label : '';
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
        if (!riderInfo.avgDeliveryTime) {
            alert('请输入预计配送时间');
            return false;
        }
        if (!riderInfo.monthlySalary || riderInfo.monthlySalary <= 0) {
            alert('请输入有效的预期月薪');
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