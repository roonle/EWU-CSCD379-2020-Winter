using System.Collections;


namespace SecretSanta.Business
{
    public class User
    {
        private readonly int _Id;
        private string _FirstName;
        private string _LastName;
        private  ArrayList _Gifts;

        public User(int Id, string FirstName, string LastName, ArrayList gifts)
        {
            this._Id = Id;
            this._FirstName = FirstName;
            this._LastName = LastName;
            this._Gifts = gifts;
        }
    }
}