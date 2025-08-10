import { getData } from '@/api/multiuse_function'

export interface StoreInfo{
    id: string
    name: string
    image: string
    address: string
    businessHours: string
    rating: number
    monthlySales: number
    description: string
    createTime: string
}

export interface DeliveryTask{
    id: string
    deliveryTime: number
    deliveryFee: number
}



export async function getStoreInfo(id: string): Promise<StoreInfo> {
    return getData<StoreInfo>(id);
}

export async function getDeliveryTask(id: string): Promise<DeliveryTask>{
    return getData<DeliveryTask>(id);
}

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