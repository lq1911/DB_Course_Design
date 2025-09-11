<template>
    <div v-if="visible"
        class="fixed border-2 top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-full max-w-md p-6 bg-white rounded-xl shadow-xl z-50">
        <!-- 标题 -->
        <h2 class="text-xl text-left font-semibold text-gray-800 mb-4">提交投诉</h2>

        <!-- 选择目标 -->
        <div class="mb-6">
            <p class="text-sm text-left font-medium text-gray-700 mb-3">选择投诉对象</p>
            <div class="grid grid-cols-2 gap-3">
                <label v-for="role in roles" :key="role.value" :class="{
                    'border-orange-500 bg-orange-50': selectedTarget === role.value,
                    'border-gray-200 hover:border-gray-300': selectedTarget !== role.value
                }" class="flex items-center p-3 border-2 rounded-lg cursor-pointer transition-all duration-200">
                    <input type="radio" :value="role.value" v-model="selectedTarget" class="sr-only" />
                    <i :class="role.icon" class="text-lg mr-3 text-orange-600"></i>
                    <span class="text-sm font-medium text-gray-900">{{ role.label }}</span>
                </label>
            </div>
        </div>

        <!-- 投诉输入 -->
        <div v-if="selectedTarget" class="mb-6">
            <!--店铺投诉-->
            <div v-if="selectedTarget === 'store'" class="mb-4">
                <textarea v-model="storeReport" placeholder="写下您对店铺的投诉..." rows="3"
                    class="w-full rounded-md border border-gray-300 px-3 py-2 text-sm focus:border-orange-500 focus:ring-1 focus:ring-orange-500" />
            </div>

            <!-- 骑手评价 -->
            <div v-if="selectedTarget === 'rider'" class="mb-4">
                <textarea v-model="riderReport" placeholder="写下您对骑手的投诉..." rows="3"
                    class="w-full rounded-md border border-gray-300 px-3 py-2 text-sm focus:border-orange-500 focus:ring-1 focus:ring-orange-500" />
            </div>
        </div>

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

import type { OrderInfo } from "@/api/user_home";
import { postStoreReport, postRiderReport } from "@/api/user_report";

const userStore = useUserStore();
const userId = userStore.getUserID();

const props = defineProps<{
    visible: boolean;
    order: OrderInfo;
}>();

const emit = defineEmits(["close"]);

// 选择对象（单选）
const roles = [
    { value: "store", label: "店铺", icon: "fas fa-store" },
    { value: "rider", label: "骑手", icon: "fas fa-motorcycle" }
];

const selectedTarget = ref<"store" | "rider" | null>(null);

// 输入框内容
const storeReport = ref("");
const riderReport = ref("");

// 错误提示 & 提交状态
const errorMsg = ref("");
const submitting = ref(false);

function close() {
    emit("close");
    // 重置数据
    selectedTarget.value = null;
    storeReport.value = "";
    riderReport.value = "";
    errorMsg.value = "";
}

async function submit() {
    errorMsg.value = "";

    if (!selectedTarget.value) {
        errorMsg.value = "请选择一个投诉对象（店铺或骑手）";
        return;
    }

    try {
        if (selectedTarget.value === "store") {
            if (!storeReport.value.trim()) {
                errorMsg.value = "请填写店铺投诉内容";
                return;
            }

            const storeId = props.order?.storeID;
            const content = storeReport.value.trim();
            await postStoreReport(userId, storeId, content);

        } else if (selectedTarget.value === "rider") {
            if (!riderReport.value.trim()) {
                errorMsg.value = "请填写骑手投诉内容";
                return;
            }
            const content = riderReport.value.trim();
            const orderId = props.order.orderID;
            await postRiderReport(orderId, content);
        }

        submitting.value = true;
        close();
    } finally {
        submitting.value = false;
    }
}
</script>
