  <template>
      <div class="mt-6 mb-6 border-b-2">
        <h3 class="text-xl font-bold text-gray-900 mb-4">用户评价</h3>
        <div class="flex items-center space-x-6 mb-6">
          <div class="text-center">
            <div class="text-3xl font-bold text-[#F9771C] mb-1">
              {{ storeInfo.rating }}
            </div>
            <div class="text-sm text-gray-600">综合评分</div>
          </div>
          <div class="flex-1">
            <div class="space-y-2">
              <div v-for="(score, index) in [5, 4, 3, 2, 1]" :key="score" class="flex items-center space-x-2">
                <span class="text-sm text-gray-600 w-8">{{ score }}星</span>
                <div class="flex-1 bg-gray-200 rounded-full h-2">
                  <div class="bg-[#F9771C] h-2 rounded-full" :style="{ width: total > 0 ? `${(props.commentStatus.status[index] / total) * 100}%` : '0%', }"></div>
                </div>
                <span class="text-sm text-gray-600 w-12">{{  total > 0 ? ((props.commentStatus.status[index] / total) * 100).toFixed(1) : 0 }}%</span>
              </div>
            </div>
          </div>
        </div>
      </div>
  </template>

  <script setup lang="ts">
  import { defineProps,computed} from 'vue'

  import type { CommentStatus } from '@/api/user_store_info'
  import type { StoreInfo } from '@/api/user_store_info'

  const props = defineProps<{
      commentStatus: CommentStatus;
      storeInfo: StoreInfo;
  }>();
  // 计算总数
  const total = computed(() =>
    props.commentStatus.status.reduce((sum, v) => sum + v, 0)
  )
  </script>