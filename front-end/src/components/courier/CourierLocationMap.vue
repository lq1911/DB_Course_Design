<template>
  <div class="w-full h-full p-4 bg-gray-100 rounded-lg shadow-md">
    <div class="flex justify-between items-center mb-2">
      <h3 class="text-lg font-semibold text-gray-700">当前位置</h3>
      <span v-if="lastUpdated" class="text-xs text-gray-500">
        {{ lastUpdated }} 更新
      </span>
    </div>
    
    <!-- 加载中 -->
    <div v-if="loading" class="flex items-center justify-center h-48 text-gray-500">
      <svg
        class="animate-spin -ml-1 mr-3 h-5 w-5 text-blue-500"
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
      >
        <circle
          class="opacity-25"
          cx="12"
          cy="12"
          r="10"
          stroke="currentColor"
          stroke-width="4"
        ></circle>
        <path
          class="opacity-75"
          fill="currentColor"
          d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
        ></path>
      </svg>
      正在获取您的实时位置...
    </div>
    
    <!-- 地图 -->
    <iframe
      v-else
      class="w-full h-72 rounded-lg"
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
import { ref, onMounted, onUnmounted } from 'vue'
import { updateCourierLocationAPI } from '@/api/rider_api'

// 默认定位：同济大学嘉定校区
const DEFAULT_LAT = 31.281553
const DEFAULT_LNG = 121.213517

const mapUrl = ref('')
const loading = ref(true)
const errorMsg = ref('')
const lastUpdated = ref('')

let locationUpdateTimer: number | null = null

async function updateLocation() {
  console.log('开始更新位置...')

  if (!navigator.geolocation) {
    errorMsg.value = '您的浏览器不支持地理定位功能。'
    mapUrl.value = `https://maps.google.com/maps?q=${DEFAULT_LAT},${DEFAULT_LNG}&z=15&output=embed`
    loading.value = false
    return
  }

  navigator.geolocation.getCurrentPosition(
    async (position) => {
      const lat = position.coords.latitude
      const lng = position.coords.longitude

      // 更新地图
      mapUrl.value = `https://maps.google.com/maps?q=${lat},${lng}&z=15&output=embed`

      try {
        await updateCourierLocationAPI(lat, lng)
        console.log('后端位置更新成功！')
        lastUpdated.value = new Date().toLocaleTimeString()
        errorMsg.value = ''
      } catch (apiError) {
        console.error('向后端更新位置失败:', apiError)
        errorMsg.value = '无法同步位置到服务器。'
      } finally {
        loading.value = false
      }
    },
    (geoError) => {
      console.error('定位失败：', geoError)
      errorMsg.value = '定位失败，已为您展示同济大学嘉定校区。'
      // fallback
      mapUrl.value = `https://maps.google.com/maps?q=${DEFAULT_LAT},${DEFAULT_LNG}&z=15&output=embed`
      loading.value = false
    }
  )
}

onMounted(() => {
  updateLocation()
  locationUpdateTimer = setInterval(updateLocation, 30000) // 每 30 秒更新一次
})

onUnmounted(() => {
  if (locationUpdateTimer) {
    clearInterval(locationUpdateTimer)
    console.log('位置更新定时器已清除。')
  }
})
</script>
