using BackEnd.Models;
using System.Reflection;

namespace BackEnd
{
    public class UserModelInspector
    {
        public static void InspectUserModel()
        {
            Console.WriteLine("=== User模型属性检查 ===");

            Type userType = typeof(User);

            Console.WriteLine($"模型类型: {userType.Name}");
            Console.WriteLine($"命名空间: {userType.Namespace}");
            Console.WriteLine();

            // 获取所有公共属性
            PropertyInfo[] properties = userType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            Console.WriteLine($"属性总数: {properties.Length}");
            Console.WriteLine();

            Console.WriteLine("=== 所有属性列表 ===");
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo prop = properties[i];
                Console.WriteLine($"{i + 1:D2}. 属性名: {prop.Name}");
                Console.WriteLine($"    类型: {prop.PropertyType.Name}");
                Console.WriteLine($"    可读: {prop.CanRead}");
                Console.WriteLine($"    可写: {prop.CanWrite}");

                // 检查是否有DatabaseGenerated特性
                var dbGeneratedAttr = prop.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute>();
                if (dbGeneratedAttr != null)
                {
                    Console.WriteLine($"    数据库生成: {dbGeneratedAttr.DatabaseGeneratedOption}");
                }

                // 检查是否有Required特性
                var requiredAttr = prop.GetCustomAttribute<System.ComponentModel.DataAnnotations.RequiredAttribute>();
                if (requiredAttr != null)
                {
                    Console.WriteLine($"    必需字段: 是");
                }

                // 检查是否有MaxLength特性
                var maxLengthAttr = prop.GetCustomAttribute<System.ComponentModel.DataAnnotations.MaxLengthAttribute>();
                if (maxLengthAttr != null)
                {
                    Console.WriteLine($"    最大长度: {maxLengthAttr.Length}");
                }

                // 检查是否有Column特性
                var columnAttr = prop.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.ColumnAttribute>();
                if (columnAttr != null)
                {
                    Console.WriteLine($"    列名: {columnAttr.Name}");
                }

                Console.WriteLine();
            }

            // 检查是否有AdministratorUserID属性
            PropertyInfo adminUserIDProp = userType.GetProperty("AdministratorUserID");
            if (adminUserIDProp != null)
            {
                Console.WriteLine("⚠️  发现AdministratorUserID属性!");
                Console.WriteLine($"    类型: {adminUserIDProp.PropertyType.Name}");
                Console.WriteLine($"    可读: {adminUserIDProp.CanRead}");
                Console.WriteLine($"    可写: {adminUserIDProp.CanWrite}");
            }
            else
            {
                Console.WriteLine("✅ 确认：User模型中没有AdministratorUserID属性");
            }

            Console.WriteLine();
            Console.WriteLine("=== 导航属性检查 ===");

            // 检查导航属性
            string[] navigationProperties = { "Customer", "Courier", "Administrator", "Seller" };
            foreach (string navProp in navigationProperties)
            {
                PropertyInfo navProperty = userType.GetProperty(navProp);
                if (navProperty != null)
                {
                    Console.WriteLine($"✅ {navProp}: {navProperty.PropertyType.Name}");
                }
                else
                {
                    Console.WriteLine($"❌ {navProp}: 未找到");
                }
            }
        }

        public static void Main(string[] args)
        {
            InspectUserModel();
        }
    }
}
