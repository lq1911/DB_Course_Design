<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 主要内容区域 -->
    <div class="container mx-auto px-4 py-6">
      <div class="grid grid-cols-1 lg:grid-cols-10 gap-6">
        <!-- 左侧菜品展示区 (70%) -->
        <div class="lg:col-span-7 bg-white rounded-lg shadow-sm">
          <!-- 顶部标题栏 -->
          <div class="flex items-center justify-between p-6 border-b border-gray-100">
            <div class="flex items-center gap-3">
              <h2 class="text-xl font-semibold text-gray-800">已选菜品</h2>
              <span class="bg-orange-100 text-orange-600 px-2 py-1 rounded-full text-sm font-medium">
                {{ selectedItems.length }}道菜
              </span>
            </div>
            <button 
              @click="clearAll"
              class="text-gray-500 hover:text-orange-500 transition-colors text-sm"
            >
              清空
            </button>
          </div>

          <!-- 菜品列表区域 -->
          <div class="p-6">
            <div class="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-4 max-h-96 overflow-y-auto custom-scrollbar">
              <div 
                v-for="item in selectedItems" 
                :key="item.id"
                class="bg-gray-50 rounded-lg p-4 hover:shadow-md transition-all duration-200 hover:bg-gray-100"
              >
                <!-- 菜品图片 -->
                <div class="aspect-square bg-gray-200 rounded-lg mb-3 overflow-hidden">
                  <img 
                    :src="item.image" 
                    :alt="item.name"
                    class="w-full h-full object-cover transition-transform duration-200 hover:scale-105"
                  />
                </div>
                
                <!-- 菜品信息 -->
                <div class="space-y-2">
                  <h3 class="font-medium text-gray-800 line-clamp-2 text-sm">{{ item.name }}</h3>
                  <p class="text-orange-500 font-semibold">¥{{ item.price }}</p>
                  
                  <!-- 数量调整器 -->
                  <div class="flex items-center justify-between">
                    <div class="flex items-center gap-2">
                      <button 
                        @click="decreaseQuantity(item.id)"
                        class="w-7 h-7 rounded-full border border-gray-300 flex items-center justify-center hover:border-orange-500 hover:text-orange-500 transition-colors"
                      >
                        -
                      </button>
                      <span class="w-8 text-center font-medium">{{ item.quantity }}</span>
                      <button 
                        @click="increaseQuantity(item.id)"
                        class="w-7 h-7 rounded-full bg-orange-500 text-white flex items-center justify-center hover:bg-orange-600 transition-colors"
                      >
                        +
                      </button>
                    </div>
                    <span class="text-sm font-semibold text-gray-800">
                      ¥{{ (item.price * item.quantity).toFixed(2) }}
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- 右侧订单信息区 (30%) -->
        <div class="lg:col-span-3 space-y-4">
          <!-- 收货地址模块 -->
          <div class="bg-white rounded-lg shadow-sm p-4">
            <div class="flex items-start gap-3">
              <div class="text-orange-500 mt-1">
                <svg class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20">
                  <path fill-rule="evenodd" d="M5.05 4.05a7 7 0 119.9 9.9L10 18.9l-4.95-4.95a7 7 0 010-9.9zM10 11a2 2 0 100-4 2 2 0 000 4z" clip-rule="evenodd" />
                </svg>
              </div>
              <div class="flex-1">
                <div class="flex items-center justify-between mb-1">
                  <span class="font-medium text-gray-800">{{ address.name }}</span>
                  <button 
                    @click="editAddress"
                    class="text-orange-500 text-sm hover:text-orange-600"
                  >
                    编辑
                  </button>
                </div>
                <p class="text-sm text-gray-600 leading-relaxed">{{ address.detail }}</p>
                <p class="text-sm text-gray-500 mt-1">{{ address.phone }}</p>
              </div>
            </div>
          </div>

          <!-- 优惠信息区域 -->
          <div class="bg-white rounded-lg shadow-sm p-4">
            <div class="flex items-center justify-between mb-3">
              <span class="font-medium text-gray-800">优惠券</span>
              <span class="text-sm text-gray-500">{{ availableCoupons }}张可用</span>
            </div>
            <button 
              @click="selectCoupon"
              class="w-full flex items-center justify-between p-3 border border-gray-200 rounded-lg hover:border-orange-500 transition-colors"
            >
              <span class="text-sm text-gray-600">
                {{ selectedCoupon ? `已选择：${selectedCoupon.name}` : '选择优惠券' }}
              </span>
              <span class="text-orange-500 text-sm">
                {{ selectedCoupon ? `-¥${selectedCoupon.amount}` : `最高省¥${maxDiscount}` }}
              </span>
            </button>
          </div>

          <!-- 支付方式选择 -->
          <div class="bg-white rounded-lg shadow-sm p-4">
            <h3 class="font-medium text-gray-800 mb-3">支付方式</h3>
            <div class="space-y-2">
              <label 
                v-for="method in paymentMethods" 
                :key="method.id"
                class="flex items-center gap-3 p-2 rounded-lg hover:bg-gray-50 cursor-pointer"
              >
                <input 
                  type="radio" 
                  :value="method.id" 
                  v-model="selectedPayment"
                  class="text-orange-500 focus:ring-orange-500"
                />
                <img :src="method.icon" :alt="method.name" class="w-6 h-6" />
                <span class="text-sm text-gray-700">{{ method.name }}</span>
              </label>
            </div>
          </div>

          <!-- 费用明细区 -->
          <div class="bg-white rounded-lg shadow-sm p-4">
            <h3 class="font-medium text-gray-800 mb-3">费用明细</h3>
            <div class="space-y-2 text-sm">
              <div class="flex justify-between">
                <span class="text-gray-600">商品总价</span>
                <span class="text-gray-800">¥{{ subtotal.toFixed(2) }}</span>
              </div>
              <div class="flex justify-between">
                <span class="text-gray-600">配送费</span>
                <span class="text-gray-800">¥{{ deliveryFee.toFixed(2) }}</span>
              </div>
              <div v-if="selectedCoupon" class="flex justify-between">
                <span class="text-gray-600">优惠金额</span>
                <span class="text-orange-500">-¥{{ selectedCoupon.amount.toFixed(2) }}</span>
              </div>
              <div class="border-t border-gray-100 pt-2 mt-2">
                <div class="flex justify-between items-center">
                  <span class="font-medium text-gray-800">实付金额</span>
                  <span class="font-bold text-lg text-gray-900">¥{{ finalAmount.toFixed(2) }}</span>
                </div>
              </div>
            </div>
          </div>

          <!-- 底部支付按钮 -->
          <button 
            @click="submitOrder"
            class="w-full bg-orange-500 hover:bg-orange-600 text-white font-medium py-4 rounded-lg transition-colors shadow-sm"
          >
            立即支付 ¥{{ finalAmount.toFixed(2) }}
          </button>
        </div>
      </div>
    </div>

    <!-- 优惠券选择弹窗 -->
    <div v-if="showCouponModal" class="fixed inset-0 bg-black bg-opacity-50 flex items-end lg:items-center justify-center z-50">
      <div class="bg-white w-full lg:w-96 lg:rounded-lg p-6 max-h-96 overflow-y-auto">
        <div class="flex items-center justify-between mb-4">
          <h3 class="font-medium text-gray-800">选择优惠券</h3>
          <button @click="showCouponModal = false" class="text-gray-500 hover:text-gray-700">
            <svg class="w-5 h-5" fill="currentColor" viewBox="0 0 20 20">
              <path fill-rule="evenodd" d="M4.293 4.293a1 1 0 011.414 0L10 8.586l4.293-4.293a1 1 0 111.414 1.414L11.414 10l4.293 4.293a1 1 0 01-1.414 1.414L10 11.414l-4.293 4.293a1 1 0 01-1.414-1.414L8.586 10 4.293 5.707a1 1 0 010-1.414z" clip-rule="evenodd" />
            </svg>
          </button>
        </div>
        <div class="space-y-3">
          <div 
            v-for="coupon in coupons" 
            :key="coupon.id"
            @click="applyCoupon(coupon)"
            class="p-3 border border-gray-200 rounded-lg cursor-pointer hover:border-orange-500 transition-colors"
            :class="{ 'border-orange-500 bg-orange-50': selectedCoupon?.id === coupon.id }"
          >
            <div class="flex justify-between items-center">
              <div>
                <p class="font-medium text-gray-800">{{ coupon.name }}</p>
                <p class="text-sm text-gray-500">{{ coupon.description }}</p>
              </div>
              <span class="text-orange-500 font-semibold">-¥{{ coupon.amount }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'

// 响应式数据
const selectedItems = ref([
  {
    id: 1,
    name: '宫保鸡丁',
    price: 28.00,
    quantity: 2,
    image: '/placeholder.svg?height=120&width=120'
  },
  {
    id: 2,
    name: '麻婆豆腐',
    price: 18.00,
    quantity: 1,
    image: '/placeholder.svg?height=120&width=120'
  },
  {
    id: 3,
    name: '红烧肉',
    price: 35.00,
    quantity: 1,
    image: '/placeholder.svg?height=120&width=120'
  },
  {
    id: 4,
    name: '蒜蓉西兰花',
    price: 15.00,
    quantity: 2,
    image: '/placeholder.svg?height=120&width=120'
  }
])

const address = ref({
  name: '张三',
  phone: '138****8888',
  detail: '北京市朝阳区三里屯街道工体北路8号院1号楼2单元301室'
})

const selectedPayment = ref('wechat')
const paymentMethods = ref([
  { id: 'wechat', name: '微信支付', icon: '/placeholder.svg?height=24&width=24' },
  { id: 'alipay', name: '支付宝', icon: '/placeholder.svg?height=24&width=24' },
  { id: 'unionpay', name: '银联支付', icon: '/placeholder.svg?height=24&width=24' }
])

const deliveryFee = ref(5.00)
const availableCoupons = ref(3)
const maxDiscount = ref(15)

const selectedCoupon = ref(null)
const showCouponModal = ref(false)

const coupons = ref([
  { id: 1, name: '满100减15', description: '满100元可用', amount: 15 },
  { id: 2, name: '满50减8', description: '满50元可用', amount: 8 },
  { id: 3, name: '新用户专享', description: '首单立减10元', amount: 10 }
])

// 计算属性
const subtotal = computed(() => {
  return selectedItems.value.reduce((sum, item) => sum + (item.price * item.quantity), 0)
})

const finalAmount = computed(() => {
  let total = subtotal.value + deliveryFee.value
  if (selectedCoupon.value) {
    total -= selectedCoupon.value.amount
  }
  return Math.max(total, 0)
})

// 方法
const increaseQuantity = (id) => {
  const item = selectedItems.value.find(item => item.id === id)
  if (item) {
    item.quantity++
  }
}

const decreaseQuantity = (id) => {
  const item = selectedItems.value.find(item => item.id === id)
  if (item && item.quantity > 1) {
    item.quantity--
  } else if (item && item.quantity === 1) {
    selectedItems.value = selectedItems.value.filter(item => item.id !== id)
  }
}

const clearAll = () => {
  if (confirm('确定要清空所有菜品吗？')) {
    selectedItems.value = []
  }
}

const editAddress = () => {
  alert('编辑地址功能')
}

const selectCoupon = () => {
  showCouponModal.value = true
}

const applyCoupon = (coupon) => {
  selectedCoupon.value = coupon
  showCouponModal.value = false
}

const submitOrder = () => {
  if (selectedItems.value.length === 0) {
    alert('请先选择菜品')
    return
  }
  alert(`订单提交成功！支付金额：¥${finalAmount.value.toFixed(2)}`)
}
</script>

<style scoped>

</style>