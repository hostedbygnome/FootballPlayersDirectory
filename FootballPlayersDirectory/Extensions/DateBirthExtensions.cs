namespace FootballPlayersDirectory.Extensions
{
    public static class DateBirthExtensions
    {
        public static string ToCorrectDate(this string dateBirth)
        {
            return $"{dateBirth.Substring(dateBirth.Length - 2, 2)}." +
                   $"{dateBirth.Substring(dateBirth.Length - 5, 2)}." +
                   $"{dateBirth.Substring(0, 4)}";
        }
    }
}
