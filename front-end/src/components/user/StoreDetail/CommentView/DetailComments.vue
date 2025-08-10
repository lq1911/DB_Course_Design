<template>

</template>

<script setup lang="ts">
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

</script>