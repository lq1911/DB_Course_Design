<template>
  <div class="flex flex-col items-center gap-4 w-full mt-5">
    <StorePoints :commentStatus="commentStatus" :storeInfo="storeInfo" class="w-full max-w-3xl" />
    <DetailComments :commentList="commentList" class="w-full max-w-3xl" />
  </div>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { ref, computed, onMounted, watch } from 'vue'

import type { StoreInfo, CommentList, CommentStatus } from '@/api/user_store_info'
import { getStoreInfo, getCommentStatus, getCommentList } from '@/api/user_store_info'

import StorePoints from '@/components/user/StoreDetail/CommentView/StorePoints.vue'
import DetailComments from '@/components/user/StoreDetail/CommentView/DetailComments.vue'

const route = useRoute();
const storeID = computed(() => route.params.id as string);
const storeInfo = ref<StoreInfo | null>(null);
const commentStatus = ref<CommentStatus | null>(null);
const commentList = ref<CommentList | null>(null);

onMounted(async () => {
  storeInfo.value = await getStoreInfo(storeID.value);
  commentStatus.value = await getCommentStatus(storeID.value);
  commentList.value = await getCommentList(storeID.value);
})

watch(storeID, async (newID, oldID) => {
    if (newID != oldID) {
      storeInfo.value = await getStoreInfo(storeID.value);
      commentStatus.value = await getCommentStatus(storeID.value);
      commentList.value = await getCommentList(storeID.value);
    }
})

</script>