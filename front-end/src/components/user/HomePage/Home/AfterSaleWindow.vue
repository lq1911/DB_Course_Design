<template>
    <div v-if="visible"
        class="fixed border-2 top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-full max-w-md p-6 bg-white rounded-xl shadow-xl z-50">
        <!-- 标题 -->
        <h2 class="text-xl text-left font-semibold text-gray-800 mb-4">提交售后</h2>

        <!-- 投诉输入 -->
        <textarea v-model="afterSaleReport" placeholder="写下您的售后诉求..." rows="3"
            class="w-full rounded-md border border-gray-300 px-3 py-2 text-sm focus:border-orange-500 focus:ring-1 focus:ring-orange-500" />

        <!-- 错误提示 -->
        <p v-if="errorMsg" class="text-sm text-red-500 mb-4">{{ errorMsg }}</p>

        <!-- 按钮 -->
        <div class="flex justify-end gap-3">
            <button class="px-4 py-2 rounded-lg border border-gray-300 text-gray-700 text-sm hover:bg-gray-100"
                @click="close">
                取消
            </button>
            <button
                class="px-4 py-2 rounded-lg bg-orange-500 text-white text-sm hover:bg-orange-600 disabled:opacity-50"
                :disabled="submitting" @click="submit">
                {{ submitting ? "提交中..." : "提交" }}
            </button>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits } from "vue";
import { useUserStore } from "@/stores/user";

import { postAfterSaleApplication } from "@/api/user_home";
import type { OrderInfo } from "@/api/user_home";

const userStore = useUserStore();
const userId = userStore.getUserID();

const props = defineProps<{
    visible: boolean;
    order: OrderInfo;
}>();

const emit = defineEmits(["close"]);

// 输入框内容
const afterSaleReport = ref("");

// 错误提示 & 提交状态
const errorMsg = ref("");
const submitting = ref(false);

function close() {
    emit("close");
    // 重置数据
    afterSaleReport.value = "";
    errorMsg.value = "";
}

async function submit() {
    errorMsg.value = "";

    try {
        if (!afterSaleReport.value) {
            errorMsg.value = '请输入售后内容'
        }

        await postAfterSaleApplication(userId, props.order.orderID, afterSaleReport.value);

        submitting.value = true;
        close();
    } finally {
        submitting.value = false;
    }
}
</script>
