namespace BackEnd.Models.Enums
{
    // 用户身份
    public enum UserIdentity
    {
        Customer = 0,
        Courier = 1,
        Administrator = 2,
        Seller = 3
    }

    // 消费者会员
    public enum MembershipStatus
    {
        NotMember = 0,
        Member = 1
    }

    // 用户是否公开信息
    public enum ProfilePrivacyLevel
    {
        Private = 0,      // 不公开
        Public = 1,       // 公开  
        FriendsOnly = 2   // 仅好友
    }

    // 菜品是否售罄
    public enum DishIsSoldOut
    {
        IsSoldOut = 0,  // 售罄
        IsNotSoldOut = 2  // 未售罄
    }

    // 优惠券状态
    public enum CouponState
    {
        Unused = 0,
        Used = 1,
        Expired = 2
    }

    // 店铺状态
    public enum StoreState
    {
        IsOperation = 0,  // 营业
        Closing = 1,  // 休息
        Banned = 2  // 封禁
    }

    // 店铺种类
    public enum StoreCategory
    {
        DeliciousFood = 0,
        SuperMarket = 1,
        GreenGrocery = 2
    }

    // 商家状态
    public enum SellerState
    {
        Normal = 0,
        Banned = 1
    }

    public enum FoodOrderState
    {
        Pending = 0,
        Preparing = 1,
        Completed = 2
    }

    public enum CommentType
    {
        Comment = 0,
        Store = 1,
        FoodOrder = 2
    }

    public enum CourierIsOnline
    {
        Online = 0,
        Offline = 1
    }
}
