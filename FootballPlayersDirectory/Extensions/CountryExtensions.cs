using FootballPlayersDirectory.Models;

namespace FootballPlayersDirectory.Extensions
{
    public static class CountryExtensions
    {
        static readonly Dictionary<Countries, string> _countryCssClasses = new()
        {
            [Countries.Russia] = "badge  rounded-pill bg-danger",
            [Countries.USA] = "badge rounded-pill bg-warning",
            [Countries.Italy] = "badge rounded-pill bg-success"
        };

        public static string ToCssClass(this Countries country) => _countryCssClasses[country];
    }
}
