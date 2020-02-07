
namespace SecretSanta.Data.Tests
{
    static public class SampleData
    {
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        

        public const string Princess = "Princess";
        public const string Buttercup = "Buttercup";

        public const string SantaGroup = "SantaGroup";

        public const string BatmanGift = "Batman";
        public const string BatmanUrl = "www.Batman.com";
        public const string BatmanDescription = "Plushy";


        static public User CreateInigoMontoya() => new User(Inigo, Montoya);
        static public User CreatePrincessButtercup() => new User(Princess, Buttercup);

        static public Group CreateSantaGroup() => new Group(SantaGroup);

        static public Gift CreateBatmanGift() => new Gift(BatmanGift, BatmanUrl, BatmanDescription, CreateInigoMontoya());
    }
}