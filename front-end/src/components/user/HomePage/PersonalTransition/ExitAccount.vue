<template>
    <transition name="fade">
        <div v-if="props.showExitForm" class="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4">
            <div class="bg-white w-full max-w-sm rounded-lg shadow-xl p-6">
                <div class="text-center">
                    <p class="text-gray-900 font-medium text-lg mb-4">确认退出登录？</p>
                    <div class="flex gap-3 justify-center">
                        <button class="flex-1 border rounded px-4 py-2 hover:bg-gray-50" @click="ExitAccount('/login')">
                            确定
                        </button>
                        <button class="flex-1 bg-red-500 text-white rounded px-4 py-2 hover:bg-red-600" @click="cancel">
                            取消
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </transition>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue'
import { useRouter } from 'vue-router'

import { useUserStore } from '@/stores/user';

const userStore = useUserStore();
const router = useRouter();

const props = defineProps<{
    showExitForm: boolean
}>()

const emit = defineEmits<{
    (e: 'update:showExitForm', value: boolean): void;
}>()

function cancel() {
    emit('update:showExitForm', false);
}

function ExitAccount(path: string) {
    userStore.logout();
    router.push(path);
}

</script>