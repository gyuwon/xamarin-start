using System;

namespace FormsCrossPlatform
{
    public class Contact
    {
        public Contact(string name, string email)
        {
            if (null == name)
                throw new ArgumentNullException(nameof(name));
            if (null == email)
                throw new ArgumentNullException(nameof(email));

            Name = name;
            Email = email;
        }

        public string Name { get; }

        public string Email { get; }
    }
}
