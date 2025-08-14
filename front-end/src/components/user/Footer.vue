<template>
  <footer class="bg-gray-100 text-gray-600 py-10 mt-12 border-t">
    <div class="max-w-screen-xl mx-auto px-4 grid grid-cols-1 md:grid-cols-3 gap-8">
      <div>
        <h3 class="text-lg font-semibold mb-4">关于我们</h3>
        <p class="text-sm leading-relaxed">
          本平台致力于为您提供优质、快速的外卖服务，精选本地热门商家，为您带来丰富的美食体验。
        </p>
      </div>
      <div>
        <h3 class="text-lg font-semibold mb-4">联系我们</h3>
        <ul class="text-sm space-y-2">
          <li>客服邮箱：support@foodexpress.cn</li>
          <li>联系电话：400-123-4567</li>
          <li>地址：上海市嘉定区曹安公路4800号</li>
        </ul>
      </div>
      <div>
        <h3 class="text-lg font-semibold mb-4">关注我们</h3>
        <div class="flex space-x-4 justify-center">
          <a v-for="(icon, index) in icons" :href="icon.url"
            class="flex flex-col items-center hover:text-orange-500" 
            @mouseenter="hoverIndex = index"
            @mouseleave="hoverIndex = -1">
            <img :src="hoverIndex === index ? icon.hoverSrc : icon.normalSrc" class="w-6 h-6 mb-1" />
            <span>{{ icon.name }}</span>
          </a>
        </div>
      </div>
    </div>
    <div class="text-center text-xs text-gray-400 mt-8">
      &copy; 2025 美食外卖平台 {{ name }}&trade;. 保留所有权利。
    </div>
  </footer>
</template>

<script setup lang="ts">
import { ref } from 'vue'

import { getProjectName } from '@/stores/name'

const name = getProjectName().projectName;

//导入图标文件
import twitter from '@/assets/Icons/Twitter.svg'
import twitterHover from '@/assets/Icons/Twitter-hover.svg'
import weibo from '@/assets/Icons/Weibo.svg'
import weiboHover from '@/assets/Icons/Weibo-hover.svg'
import youtube from '@/assets/Icons/Youtube.svg'
import youtubeHover from '@/assets/Icons/Youtube-hover.svg'

const icons = [
  {
    name: '推特',
    normalSrc: twitter,
    hoverSrc: twitterHover,
    url: 'https://x.com/home',
  },
  {
    name: '微博',
    normalSrc: weibo,
    hoverSrc: weiboHover,
    url: 'https://weibo.com/',
  },
  {
    name: '油管',
    normalSrc: youtube,
    hoverSrc: youtubeHover,
    url: 'https://www.youtube.com/',
  },
]

// 存储当前 hover 的索引，初始 -1 表示无 hover
const hoverIndex = ref(-1)
</script>