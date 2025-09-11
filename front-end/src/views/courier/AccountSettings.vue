<template>
    <div class="min-h-screen bg-gray-50">
        <!-- 顶部导航栏 -->
        <div class="fixed top-0 left-0 right-0 bg-white shadow-sm z-50 px-4 py-3">
            <div class="flex items-center justify-between">
                <div class="flex items-center space-x-3">
                    <el-icon class="cursor-pointer" @click="goBack"><ArrowLeft /></el-icon>
                    <div class="text-lg font-semibold text-gray-900">个人信息</div>
                </div>
            </div>
        </div>
        
        <!-- 主体内容区域 -->
        <div class="pt-16 pb-24">
            <!-- Loading Spinner -->
            <div v-if="isLoading" class="text-center mt-20 text-gray-500">正在加载用户信息...</div>

            <!-- 表单内容 -->
            <div v-else-if="profileData" class="mx-4 mt-6">
                <!-- 头像编辑区域 -->
                <div class="bg-white rounded-lg shadow-sm p-6 mb-6">
                    <div class="text-center">
                        <div class="relative inline-block cursor-pointer" @click="handleAvatarClick">
                            <div class="w-24 h-24 rounded-full bg-gradient-to-r from-blue-400 to-blue-600 flex items-center justify-center mx-auto mb-3 overflow-hidden">
                                <img v-if="avatarPreviewUrl || profileData.avatar" :src="avatarPreviewUrl || profileData.avatar" alt="头像" class="w-full h-full object-cover" />
                                <span v-else class="text-white text-2xl font-medium">{{ profileData.name ? profileData.name.substring(0, 2) : '...' }}</span>
                            </div>
                            <div class="absolute -bottom-1 -right-1 w-8 h-8 bg-blue-500 rounded-full flex items-center justify-center">
                                <el-icon class="text-white text-sm"><Camera /></el-icon>
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
                            <div class="text-xs text-red-500">* 必填</div>
                        </div>
                        <input v-model="profileData.name" type="text" placeholder="请输入姓名" class="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:border-blue-500 bg-white" />
                    </div>

                    <!-- 车辆类型 -->
                    <div class="p-4 border-b border-gray-100">
                        <div class="flex items-center justify-between">
                            <div class="text-sm font-medium text-gray-900 mb-2">车辆类型</div>
                            <div class="text-xs text-red-500">* 必填</div>
                        </div>
                        <input v-model="profileData.vehicleType" type="text" placeholder="请输入车辆类型" class="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:border-blue-500 bg-white" />
                    </div>

                    <!-- 性别 -->
                    <div class="p-4 border-b border-gray-100">
                         <div class="text-sm font-medium text-gray-900 mb-3">性别</div>
                        <div class="flex space-x-4">
                            <label class="flex items-center cursor-pointer">
                                <input v-model="profileData.gender" type="radio" value="男" class="w-4 h-4 text-blue-500 border-gray-300 focus:ring-blue-500" />
                                <span class="ml-2 text-sm text-gray-700">男</span>
                            </label>
                            <label class="flex items-center cursor-pointer">
                                <input v-model="profileData.gender" type="radio" value="女" class="w-4 h-4 text-blue-500 border-gray-300 focus:ring-blue-500" />
                                <span class="ml-2 text-sm text-gray-700">女</span>
                            </label>
                             <label class="flex items-center cursor-pointer">
                                <input v-model="profileData.gender" type="radio" value="保密" class="w-4 h-4 text-blue-500 border-gray-300 focus:ring-blue-500" />
                                <span class="ml-2 text-sm text-gray-700">保密</span>
                            </label>
                        </div>
                    </div>

                    <!-- 生日 -->
                    <div class="p-4 border-b border-gray-100">
                        <div class="text-sm font-medium text-gray-900 mb-2">生日</div>
                        <input v-model="profileData.birthday" type="date" class="w-full px-3 py-2 border border-gray-200 rounded-lg text-sm focus:outline-none focus:border-blue-500 bg-white" />
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
import type { UpdateProfilePayload } from '@/api/api.mock';

import { fetchProfileForEdit, updateUserProfile, uploadAvatarAPI } from '@/api/rider_api';
// --- 响应式变量 ---
const router = useRouter();
const isLoading = ref(true);
const fileInput = ref<HTMLInputElement>();
const avatarPreviewUrl = ref('');
const newAvatarFile = ref<File | null>(null);

const profileData = ref<UpdateProfilePayload>({
    name: '',
    gender: '保密',
    birthday: '',
    avatar: '',
    vehicleType: '电动车'
});

const originalProfileData = ref<UpdateProfilePayload | null>(null);

// --- 生命周期钩子 ---
onMounted(async () => {
    isLoading.value = true;
    try {
        // **【修正点 2】**：调用为编辑页专门创建的 API 函数
        const res = await fetchProfileForEdit(); 
        const fetchedData = res.data;
        
        // 填充表单数据
        profileData.value = {
            name: fetchedData.name || '',
            gender: fetchedData.gender || '保密',
            birthday: fetchedData.birthday ? fetchedData.birthday.split('T')[0] : '',
            avatar: fetchedData.avatar || '',
            vehicleType: fetchedData.vehicleType || '电动车',
        };

        // 创建原始数据的深拷贝，用于比较
        originalProfileData.value = JSON.parse(JSON.stringify(profileData.value));
        
    } catch (error) {
        ElMessage.error('加载用户信息失败');
    } finally {
        isLoading.value = false;
    }
});

// --- 计算属性 ---
const isFormValid = computed(() => {
    if (!profileData.value) return false;
    // 确保姓名和车辆类型都不能为空
    return profileData.value.name?.trim() !== '' && 
           profileData.value.vehicleType?.trim() !== '';
});

const hasChanges = computed(() => {
    if (!originalProfileData.value) return false;
    return JSON.stringify(profileData.value) !== JSON.stringify(originalProfileData.value) || newAvatarFile.value !== null;
});

// --- 事件处理函数 ---
const goBack = () => router.back();

const handleAvatarClick = () => fileInput.value?.click();

const handleFileChange = (event: Event) => {
    const target = event.target as HTMLInputElement;
    const file = target.files?.[0];
    if (file) {
        newAvatarFile.value = file;
        avatarPreviewUrl.value = URL.createObjectURL(file);
    }
};

// **【最终版 handleSave】**



const handleSave = async () => {
    if (!isFormValid.value || !hasChanges.value) return;

    try {
        const payload: UpdateProfilePayload = { ...profileData.value };

        // --- 真实的头像上传逻辑 ---
        if (newAvatarFile.value) {
            ElMessage.info("正在上传新头像...");
            // 1. 调用上传 API
            const uploadRes = await uploadAvatarAPI(newAvatarFile.value);

            // 2. 从响应中获取真实的 URL
            const newAvatarUrl = uploadRes.data.url;

            // 3. 将真实的 URL 赋值给 payload
            payload.avatar = newAvatarUrl;

            ElMessage.success("头像上传成功！");
        }
        // --------------------------

        // 如果生日为空，处理一下
        if (payload.birthday === '') {
            payload.birthday = undefined;
        }

        // 调用更新个人资料的 API
        await updateUserProfile(payload);
        ElMessage.success('个人资料保存成功！');
        router.back();
    } catch (error: any) {
        console.error("保存失败:", error);
        // ... 错误处理逻辑保持不变 ...
        ElMessage.error('保存失败，请重试');
    }
};
</script>

<style scoped>
/* 样式部分保持不变 */
</style>