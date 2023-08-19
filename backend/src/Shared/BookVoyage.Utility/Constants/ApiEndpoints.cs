namespace BookVoyage.Utility.Constants;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Categories
    {
        private const string Base = $"{ApiBase}/categories";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Authors
    {
        private const string Base = $"{ApiBase}/authors";
        public const string Create = Base;
        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
    
    public static class Books
    {
        private const string Base = $"{ApiBase}/books";
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
        private const string Base = $"{ApiBase}/auth";
        public const string Login = $"{Base}/login";
    }

    public static class Users
    {
        private const string Base = $"{ApiBase}/users";
        public const string Create = $"{Base}/register";
        public const string Get = Base;
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class ShoppingCart
    {
        private const string Base = $"{ApiBase}/cart";
        public const string UpsertItem = Base;
        public const string Get = $"{Base}/{{id:string}}";
    }
}