<template>
  <div class="w-full h-full p-4 bg-gray-100 rounded-lg shadow-md">
    <div class="flex justify-between items-center mb-2">
      <h3 class="text-lg font-semibold text-gray-700">当前位置</h3>
      <span v-if="lastUpdated" class="text-xs text-gray-500">
        {{ lastUpdated }} 更新
      </span>
    </div>
    
    <div v-if="loading" class="flex items-center justify-center h-48 text-gray-500">
      <svg class="animate-spin -ml-1 mr-3 h-5 w-5 text-blue-500" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
        <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
        <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
      </svg>
      正在获取您的实时位置...
    </div>

    <div v-else-if="errorMsg" class="flex items-center justify-center h-48 text-red-500 bg-red-50 p-4 rounded">
      {{ errorMsg }}
    </div>

    <iframe
      v-else
      class="w-full h-48 rounded-lg"
      frameborder="0"
      style="border:0"
      :src="mapUrl"
      allowfullscreen="true"
      aria-hidden="false"
      tabindex="0"
    ></iframe>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
// 导入我们刚刚创建的 API 函数
import { updateCourierLocationAPI } from '@/api/rider_api'; 

const mapUrl = ref('');
const loading = ref(true);
const errorMsg = ref('');
const lastUpdated = ref(''); // 用于显示最后更新时间

let locationUpdateTimer: number | null = null; // 用于存放定时器的ID

// 核心功能函数：获取位置、更新后端、更新地图
async function updateLocation() {
  console.log('开始更新位置...');
  
  if (!navigator.geolocation) {
    errorMsg.value = "您的浏览器不支持地理定位功能。";
    mapUrl.value = "https://maps.google.com/maps?q=Beijing&z=13&output=embed";
    loading.value = false;
    return;
  }

  navigator.geolocation.getCurrentPosition(
    async (position) => {
      // 1. 成功获取到设备位置
      const lat = position.coords.latitude;
      const lng = position.coords.longitude;
      
      // 2. 更新前端地图URL，给用户即时反馈
      mapUrl.value = `https://maps.google.com/maps?q=${lat},${lng}&z=15&output=embed`;
      
      try {
        // 3. 调用 API，将新坐标发送到后端
        await updateCourierLocationAPI(lat, lng);
        console.log('后端位置更新成功！');
        lastUpdated.value = new Date().toLocaleTimeString(); // 更新界面上的时间
      } catch (apiError) {
        console.error('向后端更新位置失败:', apiError);
        errorMsg.value = '无法同步位置到服务器。';
      } finally {
        loading.value = false;
        errorMsg.value = ''; // 成功后清除错误信息
      }
    },
    (geoError) => {
      console.error("定位失败：", geoError);
      errorMsg.value = "无法获取您的位置信息。请检查浏览器权限。";
      mapUrl.value = "https://maps.google.com/maps?q=Shanghai&z=13&output=embed";
      loading.value = false;
    }
  );
}

// onMounted: 组件加载时执行
onMounted(() => {
  // 1. 立即执行一次位置更新
  updateLocation();
  
  // 2. 设置一个定时器，每 30 秒自动更新一次位置
  locationUpdateTimer = setInterval(updateLocation, 30000); // 30000 毫秒 = 30 秒
});

// onUnmounted: 组件被销毁时（例如用户离开页面）执行
onUnmounted(() => {
  // 清除定时器，防止内存泄漏和不必要的 API 调用
  if (locationUpdateTimer) {
    clearInterval(locationUpdateTimer);
    console.log('位置更新定时器已清除。');
  }
});
</script>