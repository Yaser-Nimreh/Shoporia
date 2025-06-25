namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Category> Categories =>
    [
        Category.Create(new Guid("7fda60f2-3bb4-4e34-bd2c-d7e53d1f1a00"), "Smartphones", "All smartphone devices including iPhones."),
        Category.Create(new Guid("e1bfc871-d774-48f5-894c-98334999e5b4"), "Android Phones", "Phones running the Android operating system."),
        Category.Create(new Guid("a9f3e7be-2a99-4cb7-9f34-7f0ad6e3bb51"), "Laptops", "Portable computers for work and play."),
        Category.Create(new Guid("f2b4c490-118e-4f1c-a5a0-40413b02411f"), "Tablets", "Touchscreen tablet devices."),
        Category.Create(new Guid("d4f23e27-bcc9-4af2-9c1a-172c8a7d63fc"), "Accessories", "Smartphone and tech accessories."),
        Category.Create(new Guid("cf19c674-e7a3-4e49-9e3e-e2ab037267fd"), "Wearables", "Smartwatches and fitness trackers.")
    ];

    public static IEnumerable<Product> Products =>
    [
        // Smartphones
        Product.Create(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), "iPhone X", "Latest iPhone model with FaceID", 500, new Guid("7fda60f2-3bb4-4e34-bd2c-d7e53d1f1a00"), "iphone-x.png"),
        Product.Create(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), "iPhone 13 Pro", "High-end iPhone with triple camera", 950, new Guid("7fda60f2-3bb4-4e34-bd2c-d7e53d1f1a00"), "iphone-13pro.png"),

        // Android Phones
        Product.Create(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"), "Huawei Plus", "Long battery life with great performance", 650, new Guid("e1bfc871-d774-48f5-894c-98334999e5b4"), "huawei-plus.png"),
        Product.Create(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"), "Xiaomi Mi", "Affordable Android phone", 450, new Guid("e1bfc871-d774-48f5-894c-98334999e5b4"), "xiaomi-mi.png"),

        // Laptops
        Product.Create(new Guid("b1326f3f-2b4c-44c8-bf2f-4b0f37d54e70"), "MacBook Air", "Apple's thin and light laptop", 999, new Guid("a9f3e7be-2a99-4cb7-9f34-7f0ad6e3bb51"), "macbook-air.png"),
        Product.Create(new Guid("21358a69-5fc6-46ee-bcb8-2f3a9de94db7"), "Dell XPS 13", "Powerful Windows ultrabook", 1100, new Guid("a9f3e7be-2a99-4cb7-9f34-7f0ad6e3bb51"), "dell-xps.png"),

        // Tablets
        Product.Create(new Guid("94c63b2e-0534-4a1a-a6d2-5102642aa17a"), "iPad Pro", "High-end Apple tablet", 799, new Guid("f2b4c490-118e-4f1c-a5a0-40413b02411f"), "ipad-pro.png"),
        Product.Create(new Guid("bd96fe96-1b4c-44b7-ae4b-1f3977708ff0"), "Samsung Galaxy Tab", "Android tablet with S-Pen support", 650, new Guid("f2b4c490-118e-4f1c-a5a0-40413b02411f"), "galaxy-tab.png")
    ];
}