<template>
    <div class="min-h-screen bg-gray-50">
        <!-- 顶部导航栏 -->
        <div class="fixed top-0 left-0 right-0 bg-white shadow-sm z-50 px-4 py-3">
            <div class="flex items-center justify-between">
                <div class="flex items-center space-x-3">
                    <el-icon class="cursor-pointer" @click="goBack">
                        <ArrowLeft />
                    </el-icon>
                    <div class="text-lg font-semibold text-gray-900">个人信息</div>
                </div>
            </div>
        </div>
        <!-- 主体内容区域 -->
        <div class="pt-16 pb-24">
            <div v-if="userInfo" class="mx-4 mt-6">
                <!-- 头像编辑区域 -->
                <div class="bg-white rounded-lg shadow-sm p-6 mb-6">
                    <div class="text-center">
                        <div class="relative inline-block cursor-pointer" @click="handleAvatarClick">
                            <div
                                class="w-24 h-24 rounded-full bg-gradient-to-r from-blue-400 to-blue-600 flex items-center justify-center mx-auto mb-3 overflow-hidden">
                                <img v-if="avatarUrl" :src="avatarUrl" alt="头像" class="w-full h-full object-cover" />
                                <span v-else class="text-white text-2xl font-medium">JD</span>
                            </div>
                            <div
                                class="absolute -bottom-1 -right-1 w-8 h-8 bg-blue-500 rounded-full flex items-center justify-center">
                                <el-icon class="text-white text-sm">
                                    <Camera />
                                </el-icon>
                            </div>
                        </div>
                        <div class="text-sm text-gray-500 mt-2">点击更换头像</div>
                        <input ref="fileInput" type="file" accept="image/*" class="hidden" @change="handleFileChange" />
                    </div>
                </div>
                <!-- 信息编辑表单 -->
                <div class="bg-white rounded-lg shadow-sm overflow-hidden">
                    <!-- 姓名 -->
                    <div class="p-4 border-b border-gray-100">
                        <div class="flex items-center justify-between">
                            <div class="text-sm font-medium text-gray-900 mb-2">姓名</div>
                            <div class="text-xs text-red-500">*</div>
                        </div>
                        <input v-model="userInfo.name" type="text" placeholder="请输入姓名"
                            class="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:border-blue-500 bg-white"
                            :class="{ 'border-red-300': nameError }" />
                        <div v-if="nameError" class="text-xs text-red-500 mt-1">姓名不能为空</div>
                    </div>
                    <!-- 性别 -->
                    <div class="p-4 border-b border-gray-100">
                        <div class="text-sm font-medium text-gray-900 mb-3">性别</div>
                        <div class="flex space-x-4">
                            <label class="flex items-center cursor-pointer">
                                <input v-model="userInfo.gender" type="radio" value="男"
                                    class="w-4 h-4 text-blue-500 border-gray-300 focus:ring-blue-500" />
                                <span class="ml-2 text-sm text-gray-700">男</span>
                            </label>
                            <label class="flex items-center cursor-pointer">
                                <input v-model="userInfo.gender" type="radio" value="女"
                                    class="w-4 h-4 text-blue-500 border-gray-300 focus:ring-blue-500" />
                                <span class="ml-2 text-sm text-gray-700">女</span>
                            </label>
                            <label class="flex items-center cursor-pointer">
                                <input v-model="userInfo.gender" type="radio" value="保密"
                                    class="w-4 h-4 text-blue-500 border-gray-300 focus:ring-blue-500" />
                                <span class="ml-2 text-sm text-gray-700">保密</span>
                            </label>
                        </div>
                    </div>
                    <!-- 生日 -->
                    <div class="p-4 border-b border-gray-100">
                        <div class="text-sm font-medium text-gray-900 mb-2">生日</div>
                        <input v-model="userInfo.birthday" type="date"
                            class="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:border-blue-500 bg-white" />
                    </div>
                    <!-- 用户ID -->
                    <div class="p-4">
                        <div class="text-sm font-medium text-gray-900 mb-2">用户 ID</div>
                        <div
                            class="w-full px-3 py-2 bg-gray-100 rounded-lg text-sm text-gray-600 border border-gray-200">
                            {{ userInfo.userId }}
                        </div>
                        <div class="text-xs text-gray-400 mt-1">用户 ID 不可修改</div>
                    </div>
                </div>
                <!-- 其他信息 -->
                <div class="bg-white rounded-lg shadow-sm overflow-hidden mt-4">
                    <!-- 注册时间 -->
                    <div class="p-4 border-b border-gray-100">
                        <div class="text-sm font-medium text-gray-900 mb-2">注册时间</div>
                        <div class="text-sm text-gray-600">2023 年 3 月 15 日</div>
                    </div>
                    <!-- 最后登录 -->
                    <div class="p-4">
                        <div class="text-sm font-medium text-gray-900 mb-2">最后登录</div>
                        <div class="text-sm text-gray-600">2024 年 7 月 24 日 14:30</div>
                    </div>
                </div>
            </div>
        </div>
        <!-- 底部保存按钮 -->
        <div class="fixed bottom-0 left-0 right-0 bg-white border-t border-gray-200 p-4">
            <button @click="handleSave" :disabled="!isFormValid || !hasChanges"
                class="w-full py-3 bg-blue-500 text-white text-sm font-medium rounded-lg !rounded-button cursor-pointer transition-colors duration-200"
                :class="{
                    'bg-blue-600 hover:bg-blue-700': isFormValid && hasChanges,
                    'bg-gray-300 cursor-not-allowed': !isFormValid || !hasChanges
                }">
                保存修改
            </button>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue';
import { ArrowLeft, Camera } from '@element-plus/icons-vue';
import { useRouter } from 'vue-router';
import { ElMessage } from 'element-plus';   

// 使用组件内的API切换开关
const useMockData = false; 
import * as RealAPI from '@/api/rider_api';
import * as MockAPI from '@/api/api.mock';
const api = useMockData ? MockAPI : RealAPI;
const fileInput = ref<HTMLInputElement>();
const avatarUrl = ref('');
const nameError = ref(false);
const userInfo = ref<any | null>(null);
const router = useRouter();
// originalInfo 用于保存从API获取的原始数据，用于重置表单
const originalInfo = ref<any | null>(null);

const isLoading = ref(true);

// 在组件加载时，从API获取数据，并同时填充 userInfo 和 originalInfo
onMounted(async () => {
    isLoading.value = true;
    try {
        const res = await api.fetchUserProfile() as { data: any };
        // 使用深拷贝，确保两个ref是独立的对象
        userInfo.value = JSON.parse(JSON.stringify(res.data));
        originalInfo.value = JSON.parse(JSON.stringify(res.data));
    } catch (error) {
        ElMessage.error('加载用户信息失败');
    } finally {
        isLoading.value = false;
    }
});


const isFormValid = computed(() => {
    if(!userInfo.value) return false;
    // 检查姓名是否为空
    return userInfo.value.name.trim() !== '';
});
const hasChanges = computed(() => {
        if (!userInfo.value || !originalInfo.value) {
        return false;
    }
    return (
        userInfo.value.name !== originalInfo.value.name ||
        userInfo.value.gender !== originalInfo.value.gender ||
        userInfo.value.birthday !== originalInfo.value.birthday ||
        avatarUrl.value !== ''
    );
});
const handleAvatarClick = () => {
    fileInput.value?.click();
};
const handleFileChange = (event: Event) => {
    const target = event.target as HTMLInputElement;
    const file = target.files?.[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = (e) => {
            avatarUrl.value = e.target?.result as string;
        };
        reader.readAsDataURL(file);
    }
};
const goBack = () => {
    // 返回上一页
    router.back();
};
const handleSave = async () => {
    if (!userInfo.value) return;

    // 可以在这里比较 userInfo 和 originalInfo，如果没有变化则不提交
    if (JSON.stringify(userInfo.value) === JSON.stringify(originalInfo.value)) {
        ElMessage.warning('信息未作任何修改');
        return;
    }

    try {
        // 将编辑后的 userInfo.value 提交给API
        await api.updateUserProfile(userInfo.value);
        ElMessage.success('保存成功！');
        router.back();
    } catch (error) {
        ElMessage.error('保存失败，请重试');
    }
};
</script>
<style scoped>
.\!rounded-button {
    border-radius: 8px !important;
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

/* 自定义单选按钮样式 */
input[type="radio"] {
    appearance: none;
    -webkit-appearance: none;
    width: 16px;
    height: 16px;
    border: 2px solid #d1d5db;
    border-radius: 50%;
    background-color: white;
    position: relative;
    cursor: pointer;
}

input[type="radio"]:checked {
    border-color: #3b82f6;
    background-color: #3b82f6;
}

input[type="radio"]:checked::after {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 6px;
    height: 6px;
    border-radius: 50%;
    background-color: white;
}

/* 自定义日期输入框样式 */
input[type="date"] {
    position: relative;
}

input[type="date"]::-webkit-calendar-picker-indicator {
    position: absolute;
    right: 8px;
    cursor: pointer;
}

/* 自定义滚动条 */
::-webkit-scrollbar {
    width: 4px;
}

::-webkit-scrollbar-track {
    background: #f1f1f1;
}

::-webkit-scrollbar-thumb {
    background: #3b82f6;
    border-radius: 2px;
}

::-webkit-scrollbar-thumb:hover {
    background: #2563eb;
}
</style>