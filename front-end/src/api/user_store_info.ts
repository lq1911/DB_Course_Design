import { getData } from '@/api/multiuse_function'

// 店家信息
export interface StoreInfo {
  id: number
  name: string
  image: string
  address: string
  businessHours: string
  rating: number
  monthlySales: number
  description: string
  createTime: string
}

// 外送任务
export interface DeliveryTask {
  id: number
  deliveryTime: number
  deliveryFee: number
}

// 评价
export interface Comment {
  id: number;
  username: string;
  rating: number;
  date: string;
  content: string;
  avatar: string;
  images: string[]
}

// 店铺的评论数组
export interface CommentList {
  comments: Comment[];
}

// 综合评价
export interface CommentStatus {
  status: number[];
}

// 菜品信息
export interface MenuItem {
  id: number;
  categoryId: number;
  name: string;
  description: string;
  price: number;
  image: string;
  isSoldOut: number;
}

export async function getStoreInfo(StoreId: string): Promise<StoreInfo> {
  return getData<StoreInfo>(`/api/user/store/storeInfo`, {
    params: {
      storeId: StoreId
    }
  });
}

export async function getDeliveryTasks(UserId: number) {
    return getData<DeliveryTask[]>("/api/user/deliveryTasks", {
        params: {
            userId: UserId
        }
    });
}

export async function getCommentList(StoreId: string) {
    return getData<CommentList>("/api/user/store/commentList", {
        params: {
            storeId: StoreId
        }
    });
}

export async function getCommentStatus(StoreId: string): Promise<CommentStatus> {
  return getData<CommentStatus>("/api/user/store/commentStatus", {
        params: {
            storeId: StoreId
        }
    });
}

export async function getMenuItem(UserId: number, StoreId: number) {
    return getData<MenuItem[]>("/api/store/dish", {
        params: {
            userId: UserId,
            storeId: StoreId
        }
    });
}

// 以下均为测试代码，完成后删除
export const storeInfo = {
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

export const deliveryTask = {
  id: 1,
  deliveryTime: 23,
  deliveryFee: 6
}

export const commentList = [
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
export const commentStatus = [65, 25, 8, 2, 0];

export const menuItems = [
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
    image:
      "https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape",
    isSoldOut: 0
  },
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
    image:
      "https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape",
  },
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
    image:
      "https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape",
  },
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
    image:
      "https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape",
  },
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
    image:
      "https://readdy.ai/api/search-image?query=delicious%20chinese%20mala%20tang%20hot%20pot%20with%20various%20ingredients%20including%20beef%20slices%20tofu%20vegetables%20and%20noodles%20in%20spicy%20red%20broth%2C%20appetizing%20food%20photography%20with%20clean%20white%20background&width=300&height=240&seq=food001&orientation=landscape",
  },
  {
    id: 1,
    categoryId: 1,
    name: "招牌麻辣烫套餐",
    description:
      "精选牛肉片、鱼豆腐、土豆片、白菜、粉条等丰富配菜，配特制麻辣汤底",
    price: 28,
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