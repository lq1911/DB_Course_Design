<template>
  <el-dialog
    v-model="visible"
    :title="title"
    width="520px"
    :modal="false"
  >
    <div class="space-y-4">
      <!-- 聊天记录 -->
      <div>
        <span class="font-bold text-gray-700 text-sm">历史聊天记录：</span>
        <div
          id="reply-chat-container"
          class="mt-2 max-h-60 overflow-y-auto rounded-xl border border-gray-100 bg-gray-50 p-3 space-y-2"
        >
          <div v-if="chatLoading" class="text-center text-gray-500 text-sm py-4">
            加载中...
          </div>
          <template v-else>
            <div
              v-if="!chatMessages.length"
              class="text-center text-gray-400 text-sm py-4"
            >
              暂无聊天记录
            </div>
            <div
              v-for="(msg, idx) in chatMessages"
              :key="idx"
              class="flex"
              :class="msg.sender === identity ? 'justify-end' : 'justify-start'"
            >
              <div
                :class="[
                  'px-3 py-2 rounded-2xl text-sm shadow-sm max-w-[75%]',
                  msg.sender === identity
                    ? 'bg-[#FEECDC] text-gray-800 rounded-br-sm'
                    : 'bg-white text-gray-800 rounded-bl-sm border'
                ]"
              >
                <div class="whitespace-pre-wrap leading-relaxed">
                  {{ msg.content }}
                </div>
                <div class="text-[11px] text-gray-400 mt-1 text-right">
                  {{ msg.time }}
                </div>
              </div>
            </div>
          </template>
        </div>
      </div>

      <!-- 表情 -->
      <div>
        <div class="flex flex-wrap gap-2 mt-2">
          <span
            v-for="emoji in emojis"
            :key="emoji"
            @click="insertToReply(emoji)"
            class="text-2xl cursor-pointer hover:scale-110 transition-transform"
          >
            {{ emoji }}
          </span>
        </div>
      </div>

      <!-- 输入框 -->
      <el-input
        v-model="replyContent"
        type="textarea"
        placeholder="回复内容"
        :rows="4"
        class="modern-textarea"
      />
    </div>

    <template #footer>
      <el-button @click="close" class="modern-btn-secondary">取消</el-button>
      <el-button class="modern-btn-primary" @click="submitReply">发送</el-button>
    </template>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits, watch } from "vue";

interface Message {
  sender: string;
  content: string;
  time: string;
}

const props = defineProps<{
  modelValue: boolean;
  title: string;        // 弹窗标题
  identity: "merchant" | "user" | "rider"; // 当前用户身份
  chatMessages: Message[];
  emojis: string[];
  chatLoading?: boolean;
}>();

const emit = defineEmits<{
  (e: "update:modelValue", val: boolean): void;
  (e: "submit", content: string): void;
}>();

const visible = ref(props.modelValue);
const replyContent = ref("");

watch(
  () => props.modelValue,
  (val) => (visible.value = val)
);

watch(visible, (val) => emit("update:modelValue", val));

function insertToReply(content: string) {
  replyContent.value += content;
}

function close() {
  visible.value = false;
}

function submitReply() {
  if (!replyContent.value.trim()) return;
  emit("submit", replyContent.value.trim());
  replyContent.value = "";
}
</script>
