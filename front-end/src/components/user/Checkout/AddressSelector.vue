<template>
  <div>
    <!-- 地址卡片 -->
    <div class="bg-white shadow-lg rounded-lg border-0">
      <div class="flex items-center justify-between px-4 py-3 border-b">
        <div class="flex items-center gap-2 text-[#F9771C]">
          <i class="fas fa-map-pin"></i>
          收货地址
        </div>
      </div>
      <div class="p-4">
        <div v-if="selectedAddress" class="flex items-start justify-between">
          <div class="flex-1">
            <div class="flex items-center gap-2 mb-1">
              <span class="font-semibold text-gray-900">{{ selectedAddress.name }}</span>
              <span class="text-sm text-gray-600">{{ selectedAddress.phone }}</span>
            </div>
            <p class="text-sm text-gray-600 leading-relaxed">{{ selectedAddress.address }}</p>
          </div>
          <button
            class="text-gray-400 hover:text-[#F9771C]"
            @click="openForm(selectedAddress)"
          >
            <i class="fas fa-edit"></i>
          </button>
        </div>
      </div>
    </div>

    <!-- 弹窗表单 -->
    <transition name="fade">
      <div
        v-if="showForm"
        class="fixed inset-0 bg-black/50 z-50 flex items-center justify-center p-4"
      >
        <div class="bg-white w-full max-w-md rounded-lg shadow-xl p-6">
          <div class="flex justify-between items-center mb-4">
            <h3 class="font-medium text-gray-900">
              编辑地址
            </h3>
            <button class="text-gray-500 hover:text-gray-700" @click="closeForm">
              <i class="fas fa-times"></i>
            </button>
          </div>

          <div>
            <form @submit.prevent="saveAddress" class="space-y-4">
              <div class="grid grid-cols-2 gap-3">
                <div>
                  <label class="block text-sm text-gray-700 mb-1">收货人</label>
                  <input
                    v-model="formData.name"
                    type="text"
                    class="w-full border rounded px-2 py-1"
                    required
                  />
                </div>
                <div>
                  <label class="block text-sm text-gray-700 mb-1">手机号</label>
                  <input
                    v-model="formData.phone"
                    type="text"
                    class="w-full border rounded px-2 py-1"
                    required
                  />
                </div>
              </div>

              <div>
                <label class="block text-sm text-gray-700 mb-1">详细地址</label>
                <input
                  v-model="formData.address"
                  type="text"
                  class="w-full border rounded px-2 py-1"
                  required
                />
              </div>

              <!--表单按钮-->
              <div class="flex gap-3 pt-2">
                <button
                  type="button"
                  class="flex-1 border rounded px-4 py-2 hover:bg-gray-50"
                  @click="closeForm"
                >
                  取消
                </button>
                <button
                  type="submit"
                  class="flex-1 bg-[#F9771C] text-white rounded px-4 py-2 hover:bg-[#F9771C]/90"
                >
                  保存
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';

interface Address {
  id: number;
  name: string;
  phone: string;
  address: string;
  is_default?: boolean;
}

const addresses = ref<Address[]>([]);
const selectedAddress = ref<Address | null>(null);

const showForm = ref(false);
const editingAddress = ref<Address | null>(null);

const formData = reactive({
  id: 0,
  name: '',
  phone: '',
  address: '',
  is_default: false
});

// 模拟获取地址列表
onMounted(() => {
  addresses.value = [
    { id: 1, name: '张三', phone: '13888888888', address: '北京市朝阳区三里屯', is_default: true },
    { id: 2, name: '李四', phone: '13999999999', address: '上海市浦东新区'}
  ];
  selectedAddress.value = addresses.value.find(a => a.is_default) || addresses.value[0];
});

function openForm(addr?: Address) {
  showForm.value = true;
  editingAddress.value = addr || null;
  if (addr) {
    Object.assign(formData, addr);
  } else {
    Object.assign(formData, { id: Date.now(), name: '', phone: '', address: '', is_default: true });
  }
}

function closeForm() {
  showForm.value = false;
  editingAddress.value = null;
}

function saveAddress() {
  if (editingAddress.value) {
    const index = addresses.value.findIndex(a => a.id === editingAddress.value!.id);
    if (index !== -1) addresses.value[index] = { ...formData };
  } else {
    addresses.value.push({ ...formData });
  }
  selectedAddress.value = { ...formData };
  closeForm();
}

</script>