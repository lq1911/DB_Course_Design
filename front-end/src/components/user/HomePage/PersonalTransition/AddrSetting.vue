<template>
    <transition name="fade">
        <div v-if="props.showAddressForm" class="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
            <div class="bg-white w-full max-w-md rounded-lg shadow-xl p-6">
                <div class="flex justify-between items-center mb-4">
                    <h3 class="font-medium text-gray-900">
                        编辑地址
                    </h3>
                    <button class="text-gray-500 hover:text-gray-700" @click="closeForm">
                        <i class="fas fa-times"></i>
                    </button>
                </div>

                <div>
                    <form @submit.prevent="saveAddress" class="space-y-4">
                        <div class="grid grid-cols-2 gap-3">
                            <div>
                                <label class="block text-sm text-gray-700 mb-1">收货人</label>
                                <input v-model="formData.name" type="text" class="w-full border rounded px-2 py-1"
                                    required />
                            </div>
                            <div>
                                <label class="block text-sm text-gray-700 mb-1">手机号</label>
                                <input v-model="formData.phoneNumber" type="text"
                                    class="w-full border rounded px-2 py-1" required />
                            </div>
                        </div>

                        <div>
                            <label class="block text-sm text-gray-700 mb-1">详细地址</label>
                            <input v-model="formData.address" type="text" class="w-full border rounded px-2 py-1"
                                required />
                        </div>

                        <!--表单按钮-->
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
import { defineProps, defineEmits, reactive, watch } from 'vue'

import type { Address } from '@/api/user_address';
import { saveAddressInfo } from '@/api/user_address';

const props = defineProps<{
    showAddressForm: Boolean;
}>();

const emit = defineEmits<{
    (e: "update:showAddressForm", value: Boolean): void;
    (e: "update:address", value: Address): void;
}>();

// 待添加用户编号
const userID = 0;

const formData = reactive<Address>({
    id: userID,
    name: '',
    phoneNumber: 1,
    address: ''
});

watch(
    () => props.showAddressForm,
    (visible) => {
        if (visible) {
            formData.name = addrInfo.name;
            formData.phoneNumber = addrInfo.phoneNumber;
            formData.address = addrInfo.address;
        }
    },
);

// 关闭弹窗
function closeForm() {
    emit("update:showAddressForm", false);
}

// 保存地址
async function saveAddress() {
    try {
        const result = await saveAddressInfo(formData)
        if (result.success) {
            addrInfo.name = formData.name;
            addrInfo.phoneNumber = formData.phoneNumber;
            addrInfo.address = formData.address;

            emit("update:address", { ...formData });
            closeForm();
        } else {
            alert(result.message || '保存失败')
        }
    } catch (err) {
        console.error(err)
        alert('更新地址信息时出错')
    }
}

// 用户信息, 测试用
const addrInfo = reactive({
    name: "张小明",
    phoneNumber: 1234556,
    address: "同济大学"
});

</script>