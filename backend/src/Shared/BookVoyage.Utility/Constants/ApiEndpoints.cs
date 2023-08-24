namespace BookVoyage.Utility.Constants;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class V1
    {
        private const string VersionBase = $"{ApiBase}/v1";
        public static class Categories
        {
            private const string Base = $"{VersionBase}/categories";
            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class Authors
        {
            private const string Base = $"{VersionBase}/authors";
            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }
        
        public static class Books
        {
            private const string Base = $"{VersionBase}/books";
            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
            public const string GetByCategory = $"{Base}/categories/{{id:guid}}";
            public const string GetByAuthor = $"{Base}/authors/{{id:guid}}";
        }

        public static class Auth
        {
            private const string Base = $"{VersionBase}/auth";
            public const string Login = $"{Base}/login";
        }

        public static class Users
        {
            private const string Base = $"{VersionBase}/users";
            public const string Create = $"{Base}/register";
            public const string Get = Base;
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }

        public static class ShoppingCart
        {
            private const string Base = $"{VersionBase}/cart";
            public const string UpsertItem = Base;
            public const string Get = $"{Base}/{{id:guid}}";
        }

        public static class Orders
        {
            private const string Base = $"{VersionBase}/orders";
            public const string Create = Base;
            public const string GetByUserId = $"{Base}/users/{{id:guid}}";
            public const string GetByOrderId = $"{Base}/{{id:guid}}";
            public const string UpdateStatus = $"{Base}/{{id:guid}}";
        }

        public static class Payments
        {
            private const string Base = $"{VersionBase}/payment";
            public const string Create = Base;
        }
        
    }

    
}