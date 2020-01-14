using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class Gift
    {
        private int _Id;
        private string _Title;
        private string _Description;
        private string _Url;
        private User _User;

#pragma warning disable CA1054 // Uri parameters should not be strings
        public Gift(int Id, string Title, string Description, string Url, User User)
#pragma warning restore CA1054 // Uri parameters should not be strings comment: Allowing to be to spec of the assigment
        {
            this._Id = Id;
            this._Title = Title;
            this._Description = Description;
            this._Url = Url;
            this._User = User;
        }

        public int Id { get; }

        public string Title { get; set; }
    }
}
