<!-- The exported code uses Tailwind CSS. Install Tailwind CSS in your dev environment to ensure all styles work. -->
<template>
  <div class="min-h-screen bg-gray-50">
    <!-- 顶部商家信息区 -->
    <div
      class="relative bg-gradient-to-r from-orange-50 to-orange-100 shadow-sm overflow-hidden"
    >
      <button
        class="absolute left-4 top-4 flex items-center text-gray-600 hover:text-gray-900 transition-colors cursor-pointer !rounded-button whitespace-nowrap bg-white/80 backdrop-blur-sm px-4 py-2 text-sm"
      >
        <i class="fas fa-arrow-left mr-2"></i>
        返回
      </button>
      <div class="absolute top-0 right-0 w-1/3 h-full opacity-10">
        <i
          class="fas fa-utensils text-orange-500 text-[300px] transform rotate-12 translate-x-1/4 -translate-y-1/4"
        ></i>
      </div>
      <div class="absolute bottom-0 left-0 w-1/4 h-full opacity-10">
        <i
          class="fas fa-pepper-hot text-orange-500 text-[200px] transform -rotate-12 -translate-x-1/4 translate-y-1/4"
        ></i>
      </div>
      <div class="max-w-7xl mx-auto px-4 py-8 relative">
        <div class="flex items-center space-x-8">
          <div class="relative">
            <img
              :src="storeInfo.image"
              alt="商家头像"
              class="w-28 h-28 rounded-2xl object-cover shadow-lg"
            />
            <div
              class="absolute -bottom-2 -right-2 bg-orange-500 text-white px-3 py-1 rounded-full text-sm font-medium"
            >
              <i class="fas fa-check-circle mr-1"></i>认证商家
            </div>
          </div>
          <div class="flex-1">
            <div class="flex items-center space-x-4 mb-3">
              <h1 class="text-3xl font-bold text-gray-900">
                {{ storeInfo.name }}
              </h1>
              <div
                class="flex items-center px-3 py-1 bg-orange-500 bg-opacity-10 rounded-full"
              >
                <i class="fas fa-crown text-orange-500 mr-2"></i>
                <span class="text-orange-600 font-medium">优质商家</span>
              </div>
            </div>
            <div class="flex items-center space-x-6 text-sm mb-3">
              <div
                class="flex items-center bg-white px-3 py-1.5 rounded-full shadow-sm"
              >
                <i class="fas fa-star text-yellow-400 mr-2"></i>
                <span class="font-medium text-gray-900"
                  >{{ storeInfo.rating }}</span
                >
              </div>
              <div class="flex items-center">
                <i class="fas fa-shopping-bag text-gray-500 mr-2"></i>
                <span>月售 {{ storeInfo.monthlySales }} 单</span>
              </div>
              <div class="flex items-center">
                <i class="fas fa-truck text-gray-500 mr-2"></i>
                <span>配送费 ¥{{ storeInfo.deliveryFee }}</span>
              </div>
              <div class="flex items-center">
                <i class="fas fa-clock text-gray-500 mr-2"></i>
                <span>{{ storeInfo.deliveryTime }}</span>
              </div>
            </div>
            <p class="text-gray-700 max-w-2xl">{{ storeInfo.description }}</p>
          </div>
        </div>
      </div>
    </div>
    <!-- Tab导航栏 -->
    <div class="bg-white border-b">
      <div class="max-w-7xl mx-auto px-4">
        <div class="flex space-x-8">
          <button
            v-for="tab in tabs"
            :key="tab.id"
            @click="activeTab = tab.id"
            :class="{
'border-b-2 border-[#F9771C] text-[#F9771C]': activeTab === tab.id,
'text-gray-600 hover:text-gray-900': activeTab !== tab.id
}"
            class="py-4 px-2 font-medium transition-colors cursor-pointer"
          >
            {{ tab.name }}
          </button>
        </div>
      </div>
    </div>
    <!-- 主内容区域 -->
    <div class="max-w-7xl mx-auto px-4 py-6 relative">
      <!-- 点单页面 -->
      <div v-if="activeTab === 'menu'" class="flex gap-6">
        <!-- 左侧商品分类导航 -->
        <div
          class="w-1/5 bg-white rounded-lg shadow-sm"
          style="box-shadow: 2px 0 4px rgba(0,0,0,0.05)"
        >
          <div class="py-4">
            <div
              v-for="(category, index) in categories"
              :key="category.id"
              @click="activeCategory = category.id"
              :class="{
'bg-white text-[#F9771C] border-l-4 border-[#F9771C]': activeCategory === category.id,
'bg-[#F8F8F8] text-gray-700 hover:bg-[#F2F2F2]': activeCategory !== category.id
}"
              class="h-15 px-2 flex items-center justify-between cursor-pointer transition-all duration-200 relative"
              :style="index < categories.length - 1 ? 'border-bottom: 1px solid #EEEEEE' : ''"
            >
              <span class="text-base font-medium ml-2"
                >{{ category.name }}</span
              >
              <i
                v-if="activeCategory === category.id"
                class="fas fa-chevron-right text-gray-400 text-xs mr-4"
              ></i>
            </div>
          </div>
        </div>
        <!-- 右侧商品列表 -->
        <div class="flex-1 pl-6">
          <div class="bg-white rounded-lg shadow-sm p-6">
            <h3 class="text-xl font-bold text-gray-900 mb-6">
              {{ getCurrentCategory()?.name }}
            </h3>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              <div
                v-for="item in getCurrentMenuItems()"
                :key="item.id"
                class="bg-white border border-gray-200 rounded-lg overflow-hidden hover:shadow-md transition-shadow"
              >
                <img
                  :src="item.image"
                  alt="商品图片"
                  class="w-full h-48 object-cover object-top"
                />
                <div class="p-4">
                  <h4 class="font-semibold text-gray-900 mb-2">
                    {{ item.name }}
                  </h4>
                  <p class="text-sm text-gray-600 mb-3 line-clamp-2">
                    {{ item.description }}
                  </p>
                  <div class="flex items-center justify-between">
                    <div class="flex items-center space-x-2">
                      <span class="text-lg font-bold text-[#F9771C]"
                        >¥{{ item.price }}</span
                      >
                      <span
                        v-if="item.originalPrice"
                        class="text-sm text-gray-500 line-through"
                        >¥{{ item.originalPrice }}</span
                      >
                    </div>
                    <div class="flex items-center space-x-2">
                      <button
                        @click="decreaseQuantity(item.id)"
                        :disabled="!getItemQuantity(item.id)"
                        class="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 disabled:opacity-50 cursor-pointer !rounded-button whitespace-nowrap"
                      >
                        <i class="fas fa-minus text-xs"></i>
                      </button>
                      <span class="w-8 text-center"
                        >{{ getItemQuantity(item.id) || 0 }}</span
                      >
                      <button
                        @click="increaseQuantity(item.id)"
                        class="w-8 h-8 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer !rounded-button whitespace-nowrap"
                      >
                        <i class="fas fa-plus text-xs"></i>
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- 评价页面 -->
      <div
        v-if="activeTab === 'reviews'"
        class="bg-white rounded-lg shadow-sm p-6"
      >
        <div class="mb-6">
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
                <div
                  v-for="(score, index) in [5, 4, 3, 2, 1]"
                  :key="score"
                  class="flex items-center space-x-2"
                >
                  <span class="text-sm text-gray-600 w-8">{{ score }}星</span>
                  <div class="flex-1 bg-gray-200 rounded-full h-2">
                    <div
                      class="bg-[#F9771C] h-2 rounded-full"
                      :style="{ width: `${reviewStats[index]}%` }"
                    ></div>
                  </div>
                  <span class="text-sm text-gray-600 w-12"
                    >{{ reviewStats[index] }}%</span
                  >
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="space-y-6">
          <div
            v-for="review in reviews"
            :key="review.id"
            class="border-b border-gray-200 pb-6 last:border-b-0"
          >
            <div class="flex items-start space-x-4">
              <img
                :src="review.avatar"
                alt="用户头像"
                class="w-12 h-12 rounded-full object-cover"
              />
              <div class="flex-1">
                <div class="flex items-center space-x-2 mb-2">
                  <span class="font-semibold text-gray-900"
                    >{{ review.username }}</span
                  >
                  <div class="flex items-center">
                    <i
                      v-for="star in 5"
                      :key="star"
                      :class="star <= review.rating ? 'text-yellow-400' : 'text-gray-300'"
                      class="fas fa-star text-sm"
                    ></i>
                  </div>
                  <span class="text-sm text-gray-500">{{ review.date }}</span>
                </div>
                <p class="text-gray-700 mb-3">{{ review.content }}</p>
                <div v-if="review.images.length" class="flex space-x-2">
                  <img
                    v-for="(image, index) in review.images"
                    :key="index"
                    :src="image"
                    alt="评价图片"
                    class="w-20 h-20 object-cover rounded-lg cursor-pointer"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!-- 商家信息页面 -->
      <div
        v-if="activeTab === 'info'"
        class="bg-white rounded-lg shadow-sm p-6"
      >
        <h3 class="text-xl font-bold text-gray-900 mb-6">商家信息</h3>
        <div class="space-y-6">
          <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div class="space-y-4">
              <div class="flex items-center space-x-3">
                <i class="fas fa-store text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">商家名称</div>
                  <div class="text-gray-600">{{ storeInfo.name }}</div>
                </div>
              </div>
              <div class="flex items-center space-x-3">
                <i class="fas fa-map-marker-alt text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">商家地址</div>
                  <div class="text-gray-600">{{ storeInfo.address }}</div>
                </div>
              </div>
              <div class="flex items-center space-x-3">
                <i class="fas fa-phone text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">联系电话</div>
                  <div class="text-gray-600">{{ storeInfo.phone }}</div>
                </div>
              </div>
              <div class="flex items-center space-x-3">
                <i class="fas fa-clock text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">营业时间</div>
                  <div class="text-gray-600">{{ storeInfo.businessHours }}</div>
                </div>
              </div>
            </div>
            <div class="space-y-4">
              <div class="flex items-center space-x-3">
                <i class="fas fa-certificate text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">营业执照</div>
                  <div class="text-gray-600">{{ storeInfo.license }}</div>
                </div>
              </div>
              <div class="flex items-center space-x-3">
                <i class="fas fa-shield-alt text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">食品经营许可证</div>
                  <div class="text-gray-600">{{ storeInfo.foodLicense }}</div>
                </div>
              </div>
              <div class="flex items-center space-x-3">
                <i class="fas fa-truck text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">配送范围</div>
                  <div class="text-gray-600">{{ storeInfo.deliveryRange }}</div>
                </div>
              </div>
              <div class="flex items-center space-x-3">
                <i class="fas fa-money-bill text-[#F9771C] w-5"></i>
                <div>
                  <div class="font-semibold text-gray-900">起送金额</div>
                  <div class="text-gray-600">¥{{ storeInfo.minOrder }}</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- 右侧悬浮购物车 -->
    <div class="fixed right-6 bottom-6 z-50">
      <button
        @click="toggleCart"
        class="bg-[#F9771C] text-white w-14 h-14 rounded-full shadow-lg hover:bg-orange-600 transition-colors flex items-center justify-center cursor-pointer !rounded-button whitespace-nowrap"
      >
        <i class="fas fa-shopping-cart text-lg"></i>
        <span
          v-if="totalItems > 0"
          class="absolute -top-2 -right-2 bg-red-500 text-white text-xs w-6 h-6 rounded-full flex items-center justify-center"
        >
          {{ totalItems }}
        </span>
      </button>
    </div>
    <!-- 购物车侧边栏 -->
    <div
      v-if="showCart"
      class="fixed inset-0 z-50 overflow-hidden"
      @click="showCart = false"
    >
      <div class="absolute inset-0 bg-black bg-opacity-50"></div>
      <div
        class="absolute right-0 top-0 h-full w-96 bg-white shadow-xl transform transition-transform duration-300"
        @click.stop
      >
        <div class="flex flex-col h-full">
          <div class="flex items-center justify-between p-4 border-b">
            <h3 class="text-lg font-semibold text-gray-900">购物车</h3>
            <button
              @click="showCart = false"
              class="text-gray-500 hover:text-gray-700 cursor-pointer"
            >
              <i class="fas fa-times text-lg"></i>
            </button>
          </div>
          <div class="flex-1 overflow-y-auto p-4">
            <div
              v-if="cartItems.length === 0"
              class="text-center text-gray-500 mt-8"
            >
              <i class="fas fa-shopping-cart text-4xl mb-4"></i>
              <p>购物车是空的</p>
            </div>
            <div v-else class="space-y-4">
              <div
                v-for="item in cartItems"
                :key="item.id"
                class="flex items-center space-x-3 bg-gray-50 p-3 rounded-lg"
              >
                <img
                  :src="item.image"
                  alt="商品图片"
                  class="w-12 h-12 object-cover rounded object-top"
                />
                <div class="flex-1">
                  <h4 class="font-medium text-gray-900 text-sm">
                    {{ item.name }}
                  </h4>
                  <p class="text-[#F9771C] font-semibold">¥{{ item.price }}</p>
                </div>
                <div class="flex items-center space-x-2">
                  <button
                    @click="decreaseQuantity(item.id)"
                    class="w-6 h-6 rounded-full bg-gray-200 flex items-center justify-center text-gray-600 hover:bg-gray-300 cursor-pointer !rounded-button whitespace-nowrap"
                  >
                    <i class="fas fa-minus text-xs"></i>
                  </button>
                  <span class="w-6 text-center text-sm"
                    >{{ item.quantity }}</span
                  >
                  <button
                    @click="increaseQuantity(item.id)"
                    class="w-6 h-6 rounded-full bg-[#F9771C] text-white flex items-center justify-center hover:bg-orange-600 cursor-pointer !rounded-button whitespace-nowrap"
                  >
                    <i class="fas fa-plus text-xs"></i>
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div v-if="cartItems.length > 0" class="border-t p-4">
            <div class="flex items-center justify-between mb-4">
              <span class="text-lg font-semibold text-gray-900">总计</span>
              <span class="text-xl font-bold text-[#F9771C]"
                >¥{{ totalPrice.toFixed(2) }}</span
              >
            </div>
            <button
              class="w-full bg-[#F9771C] text-white py-3 rounded-lg font-semibold hover:bg-orange-600 transition-colors cursor-pointer !rounded-button whitespace-nowrap"
            >
              去结算
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from "vue";
const activeTab = ref("menu");
const activeCategory = ref(1);
const showCart = ref(false);
const cart = ref<Record<number, number>>({});
const tabs = [
  { id: "menu", name: "点单" },
  { id: "reviews", name: "评价" },
  { id: "info", name: "商家" },
];
const storeInfo = {
  name: "川香麻辣烫",
  rating: 4.8,
  monthlySales: 1256,
  deliveryFee: 3,
  deliveryTime: "30-45分钟",
  description:
    "正宗川味麻辣烫，精选优质食材，汤底浓郁香醇，让您品味地道川菜风情",
  image:
    "https://readdy.ai/api/search-image?query=modern%20chinese%20restaurant%20storefront%20with%20warm%20lighting%20and%20traditional%20elements%2C%20clean%20professional%20photography%20with%20soft%20natural%20lighting%20and%20minimalist%20background&width=200&height=200&seq=store001&orientation=squarish",
  address: "北京市朝阳区三里屯街道工体北路8号",
  phone: "010-12345678",
  businessHours: "10:00-22:00",
  license: "京朝工商备字第123456号",
  foodLicense: "JY11105010012345",
  deliveryRange: "3公里内",
  minOrder: 20,
};
const categories = [
  { id: 1, name: "招牌推荐" },
  { id: 2, name: "荤菜类" },
  { id: 3, name: "素菜类" },
  { id: 4, name: "丸子类" },
  { id: 5, name: "豆制品" },
  { id: 6, name: "主食类" },
  { id: 7, name: "饮品" },
];
const menuItems = [
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
    originalPrice: 35,
    image:
      "https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape",
  },
  {
    id: 2,
    categoryId: 1,
    name: "川香牛肉面",
    description: "手工拉面配嫩滑牛肉片，汤底鲜美，麻辣适中",
    price: 22,
    image:
      "https://readdy.ai/api/search-image?query=traditional%20chinese%20beef%20noodle%20soup%20with%20tender%20beef%20slices%20and%20hand%20pulled%20noodles%20in%20clear%20savory%20broth%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food002&orientation=landscape",
  },
  {
    id: 3,
    categoryId: 1,
    name: "特色酸辣粉",
    description: "正宗重庆酸辣粉，酸辣开胃，配花生米和榨菜",
    price: 18,
    image:
      "https://readdy.ai/api/search-image?query=authentic%20chinese%20sour%20and%20spicy%20glass%20noodles%20with%20peanuts%20and%20pickled%20vegetables%20in%20tangy%20red%20sauce%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food003&orientation=landscape",
  },
  {
    id: 4,
    categoryId: 2,
    name: "精品牛肉片",
    description: "新鲜牛肉切片，肉质鲜嫩，是麻辣烫的经典搭配",
    price: 12,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20sliced%20raw%20beef%20for%20hot%20pot%20cooking%2C%20thin%20marbled%20meat%20slices%20arranged%20neatly%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food004&orientation=landscape",
  },
  {
    id: 5,
    categoryId: 2,
    name: "嫩滑羊肉卷",
    description: "优质羊肉卷，肥瘦相间，涮煮后鲜美可口",
    price: 15,
    image:
      "https://readdy.ai/api/search-image?query=premium%20lamb%20meat%20rolls%20sliced%20thin%20for%20hot%20pot%2C%20marbled%20texture%20with%20fat%20and%20lean%20meat%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food005&orientation=landscape",
  },
  {
    id: 6,
    categoryId: 2,
    name: "午餐肉",
    description: "经典午餐肉片，方便快捷，老少皆宜",
    price: 8,
    image:
      "https://readdy.ai/api/search-image?query=sliced%20luncheon%20meat%20spam%20for%20hot%20pot%20cooking%2C%20pink%20rectangular%20meat%20slices%20arranged%20on%20plate%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food006&orientation=landscape",
  },
  {
    id: 7,
    categoryId: 3,
    name: "新鲜白菜",
    description: "当季新鲜白菜，清脆爽口，营养丰富",
    price: 4,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20chinese%20cabbage%20leaves%20cut%20for%20hot%20pot%2C%20green%20and%20white%20vegetable%20pieces%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food007&orientation=landscape",
  },
  {
    id: 8,
    categoryId: 3,
    name: "土豆片",
    description: "精选土豆切片，口感软糯，吸汤入味",
    price: 5,
    image:
      "https://readdy.ai/api/search-image?query=sliced%20potatoes%20for%20hot%20pot%20cooking%2C%20thin%20round%20potato%20slices%20arranged%20neatly%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food008&orientation=landscape",
  },
  {
    id: 9,
    categoryId: 3,
    name: "菠菜",
    description: "新鲜菠菜叶，富含维生素，健康美味",
    price: 6,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20spinach%20leaves%20for%20hot%20pot%20cooking%2C%20green%20leafy%20vegetables%20clean%20and%20vibrant%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food009&orientation=landscape",
  },
  {
    id: 10,
    categoryId: 4,
    name: "鱼豆腐",
    description: "Q弹鱼豆腐，口感丰富，老少皆宜",
    price: 8,
    image:
      "https://readdy.ai/api/search-image?query=fish%20tofu%20balls%20for%20hot%20pot%2C%20white%20and%20golden%20colored%20processed%20fish%20balls%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food010&orientation=landscape",
  },
  {
    id: 11,
    categoryId: 4,
    name: "牛肉丸",
    description: "手工制作牛肉丸，弹性十足，肉香浓郁",
    price: 10,
    image:
      "https://readdy.ai/api/search-image?query=handmade%20beef%20meatballs%20for%20hot%20pot%2C%20round%20brown%20meat%20balls%20with%20smooth%20texture%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food011&orientation=landscape",
  },
  {
    id: 12,
    categoryId: 4,
    name: "鱼丸",
    description: "新鲜鱼肉制作，口感爽滑，鲜味十足",
    price: 9,
    image:
      "https://readdy.ai/api/search-image?query=fresh%20fish%20balls%20for%20hot%20pot%20cooking%2C%20white%20round%20fish%20meatballs%20with%20smooth%20surface%2C%20professional%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food012&orientation=landscape",
  },
];
const reviews = [
  {
    id: 1,
    username: "美食达人小王",
    rating: 5,
    date: "2024-01-15",
    content:
      "味道非常正宗，麻辣烫的汤底特别香，配菜新鲜，服务态度也很好，下次还会再来！",
    avatar:
      "https://readdy.ai/api/search-image?query=friendly%20asian%20person%20avatar%20profile%20photo%20with%20warm%20smile%2C%20professional%20headshot%20photography%20with%20clean%20background&width=100&height=100&seq=user001&orientation=squarish",
    images: [
      "https://readdy.ai/api/search-image?query=delicious%20mala%20tang%20hot%20pot%20with%20colorful%20ingredients%20and%20spicy%20red%20broth%20in%20bowl%2C%20appetizing%20food%20photography&width=150&height=150&seq=review001&orientation=squarish",
      "https://readdy.ai/api/search-image?query=various%20hot%20pot%20ingredients%20including%20vegetables%20meat%20and%20tofu%20arranged%20beautifully%2C%20food%20photography&width=150&height=150&seq=review002&orientation=squarish",
    ],
  },
  {
    id: 2,
    username: "吃货小李",
    rating: 4,
    date: "2024-01-12",
    content:
      "整体不错，分量足够，价格合理。就是稍微有点咸，不过还是会推荐给朋友的。",
    avatar:
      "https://readdy.ai/api/search-image?query=happy%20young%20asian%20person%20avatar%20with%20cheerful%20expression%2C%20professional%20portrait%20photography%20with%20neutral%20background&width=100&height=100&seq=user002&orientation=squarish",
    images: [],
  },
  {
    id: 3,
    username: "川菜爱好者",
    rating: 5,
    date: "2024-01-10",
    content:
      "作为一个四川人，这家的麻辣烫真的很地道！辣度刚好，麻味也很正宗，强烈推荐！",
    avatar:
      "https://readdy.ai/api/search-image?query=mature%20asian%20person%20with%20satisfied%20expression%2C%20professional%20headshot%20with%20clean%20background&width=100&height=100&seq=user003&orientation=squarish",
    images: [
      "https://readdy.ai/api/search-image?query=authentic%20sichuan%20style%20mala%20tang%20with%20rich%20red%20spicy%20broth%20and%20various%20ingredients%2C%20traditional%20food%20photography&width=150&height=150&seq=review003&orientation=squarish",
    ],
  },
];
const reviewStats = [65, 25, 8, 2, 0];
const toggleCart = () => {
  showCart.value = !showCart.value;
};
const getCurrentCategory = () => {
  return categories.find((cat) => cat.id === activeCategory.value);
};
const getCurrentMenuItems = () => {
  return menuItems.filter((item) => item.categoryId === activeCategory.value);
};
const getItemQuantity = (itemId: number) => {
  return cart.value[itemId] || 0;
};
const increaseQuantity = (itemId: number) => {
  cart.value[itemId] = (cart.value[itemId] || 0) + 1;
};
const decreaseQuantity = (itemId: number) => {
  if (cart.value[itemId] && cart.value[itemId] > 0) {
    cart.value[itemId]--;
    if (cart.value[itemId] === 0) {
      delete cart.value[itemId];
    }
  }
};
const totalItems = computed(() => {
  return Object.values(cart.value).reduce((sum, quantity) => sum + quantity, 0);
});
const cartItems = computed(() => {
  return Object.entries(cart.value)
    .filter(([, quantity]) => quantity > 0)
    .map(([itemId, quantity]) => {
      const item = menuItems.find((item) => item.id === parseInt(itemId));
      return item ? { ...item, quantity } : null;
    })
    .filter((item) => item !== null);
});
const totalPrice = computed(() => {
  return cartItems.value.reduce((sum, item) => {
    return sum + (item?.price || 0) * (item?.quantity || 0);
  }, 0);
});
</script>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
.\!rounded-button {
  border-radius: 12px !important;
}
.h-15 {
  height: 60px;
}
</style>
