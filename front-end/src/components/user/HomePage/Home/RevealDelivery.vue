<template>
  <div
    v-if="visible"
    class="fixed border-2 top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2 w-full max-w-2xl p-6 bg-white rounded-xl shadow-xl z-50"
  >
    <!-- 关闭按钮 -->
    <button
      class="absolute top-2 right-2 text-gray-500 hover:text-black"
      @click="close"
    >
      ✖
    </button>

    <!-- 加载中提示 -->
    <div v-if="loading" class="text-center text-gray-500">正在获取当前位置...</div>

    <!-- Google Map 嵌入 -->
    <iframe
      v-else
      class="w-full h-[60vh] rounded-lg"
      frameborder="0"
      style="border:0"
      :src="mapUrl"
    ></iframe>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, defineProps, defineEmits } from "vue";

const props = defineProps<{ visible: boolean }>();
const emit = defineEmits(["close"]);

const mapUrl = ref("");
const loading = ref(true);

function close() {
  emit("close");
}

// 监听弹窗打开，获取位置
watch(
  () => props.visible,
  (val) => {
    if (val) {
      loading.value = true;
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            const lat = position.coords.latitude;
            const lng = position.coords.longitude;
            mapUrl.value = `https://maps.google.com/maps?q=${lat},${lng}&z=15&output=embed`;
            loading.value = false;
          },
          (error) => {
            console.error("定位失败：", error);
            // 定位失败时默认到同济大学嘉定校区
            mapUrl.value =
              "https://maps.google.com/maps?q=31.281553,121.213517&z=15&output=embed";
            loading.value = false;
          }
        );
      } else {
        console.error("浏览器不支持定位");
        // 浏览器不支持定位时默认到同济大学嘉定校区
        mapUrl.value =
          "https://maps.google.com/maps?q=31.281553,121.213517&z=15&output=embed";
        loading.value = false;
      }
    }
  }
);

</script>
