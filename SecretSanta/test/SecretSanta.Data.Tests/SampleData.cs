

namespace SecretSanta.Data.Tests
{
    public static class SampleData
    {
        public const string DarkKnight = "DarkKnight ";
        public const string DarkDescription = "Plushy";
        public const string DarkUrl = "www.Darkness.com";

        public const string Disney = "Disney";
        public const string DisneyDescription = "Movie";
        public const string DisneyUrl = "www.Disney.com";


        public static Gift CreateDarkKnightGift() => new Gift(DarkKnight, DarkDescription, DarkUrl, CreateMikeUser());
        public static Gift CreateDisneyGift() => new Gift(Disney, DisneyDescription, DisneyUrl, CreateHaleyUser());

        public const string Mike = "Mike";
        public const string Le = "Le";

        public const string Haley = "Haley";
        public const string Song = "Song";

        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        public const string InigoAlt = "InoMontoya";

        public const string Reese = "Reese";
        public const string Cup = "Cup";
        public const string ReeseAlt = "pbuttercup";
        public static User CreateMikeUser() => new User(Mike, Le);
        public static User CreateHaleyUser() => new User(Haley, Song);

        public static User CreateInigoUser() => new User(Inigo, Montoya);

        public const string Gifters = "Gifters";

        public static Group CreateGiftersGroup() => new Group(Gifters);

    }
}
