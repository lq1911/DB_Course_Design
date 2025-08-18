<template>
    <transition name="fade">
        <div v-if="props.showAccountForm" class="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
            <div class="bg-white w-full max-w-md rounded-lg shadow-xl p-6 overflow-y-auto max-h-[80vh]">
                <div class="flex justify-between items-center mb-4">
                    <h3 class="font-medium text-gray-900">
                        账户设置
                    </h3>
                    <button class="text-gray-500 hover:text-gray-700" @click="closeForm">
                        <i class="fas fa-times"></i>
                    </button>
                </div>

                <div>
                    <form @submit.prevent="saveAccount" class="space-y-4">
                        <!-- 头像上传 -->
                        <div class="flex flex-col items-center mb-4">
                            <div class="w-20 h-20 rounded-full overflow-hidden bg-gray-200 flex items-center justify-center text-gray-400 text-2xl font-bold mb-2 cursor-pointer hover:ring-2 hover:ring-[#F9771C]"
                                @click="triggerFileInput">
                                <img :src="formData.image" class="w-full h-full object-cover" />
                            </div>
                            <!-- 隐藏文件输入框 -->
                            <input ref="fileInput" type="file" accept="image/*" class="hidden"
                                @change="onAvatarChange" />
                        </div>

                        <!-- 昵称 -->
                        <div>
                            <label class="block text-sm text-gray-700 mb-1">昵称</label>
                            <input v-model="formData.name" type="text" class="w-full border rounded px-2 py-1"
                                required />
                        </div>

                        <!-- 手机号 -->
                        <div>
                            <label class="block text-sm text-gray-700 mb-1">手机号</label>
                            <input v-model="formData.phoneNumber" type="text" class="w-full border rounded px-2 py-1"
                                required />
                        </div>

                        <!-- 操作按钮 -->
                        <div class="flex gap-3 pt-2">
                            <button type="button" class="flex-1 border rounded px-4 py-2 hover:bg-gray-50"
                                @click="closeForm">
                                取消
                            </button>
                            <button type="submit"
                                class="flex-1 bg-[#F9771C] text-white rounded px-4 py-2 hover:bg-[#F9771C]/90">
                                保存
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </transition>
</template>

<script setup lang="ts">
import { reactive, defineProps, defineEmits, ref, watch } from 'vue'

import type { AccountInfo } from '@/api/user_account';

const props = defineProps<{
    showAccountForm: Boolean;
}>()

const emit = defineEmits<{
    (e: 'update:showAccountForm', value: Boolean): void;
    (e: 'update:account', value: AccountInfo): void;
}>()

const formData = reactive<AccountInfo>({
    name: '',
    phoneNumber: 1,
    image: ''
})

watch(
    () => props.showAccountForm,
    (visible) => {
        if (visible) {
            formData.name = accountInfo.name;
            formData.phoneNumber = accountInfo.phoneNumber;
            formData.image = accountInfo.image;
        }
    },
);

// ref 用于触发隐藏的文件输入
const fileInput = ref<HTMLInputElement | null>(null)

function triggerFileInput() {
    fileInput.value?.click()
}

// 关闭弹窗
function closeForm() {
    emit('update:showAccountForm', false)
}

// 保存修改
function saveAccount() {
    accountInfo.name = formData.name;
    accountInfo.phoneNumber = formData.phoneNumber;
    accountInfo.image = formData.image;

    emit('update:account', { ...formData })
    closeForm()
}

// 头像选择
function onAvatarChange(event: Event) {
    const target = event.target as HTMLInputElement
    const file = target.files?.[0]
    if (file) {
        const reader = new FileReader()
        reader.onload = (e) => {
            formData.image = e.target?.result as string
        }
        reader.readAsDataURL(file)
    }
}

// 用户信息, 测试用
const accountInfo = reactive({
    name: "张小明",
    phoneNumber: 1234556,
    image: "https://i.bobopic.com/small/115698491.jpg"
});
</script>
