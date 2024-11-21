using Newtonsoft.Json.Serialization;

namespace PokemonReviewApp
{
    public static class StringExtensions
    {
        public static string? ToSnakeCase(this string? str) => str is null
            ? null
            : new DefaultContractResolver() { NamingStrategy = new SnakeCaseNamingStrategy() }.GetResolvedPropertyName(str);
    }
}
