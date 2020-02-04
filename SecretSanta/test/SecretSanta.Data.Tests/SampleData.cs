
namespace SecretSanta.Data.Tests
{
    static public class SampleData
    {
        public const string Inigo = "Inigo";
        public const string Montoya = "Montoya";
        

        public const string Princess = "Princess";
        public const string Buttercup = "Buttercup";
        

        static public User CreateInigoMontoya() => new User(Inigo, Montoya);
        static public User CreatePrincessButtercup() => new User(Princess, Buttercup);
    }
}