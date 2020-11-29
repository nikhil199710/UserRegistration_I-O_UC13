namespace AddressBookSystem
{/// <summary>
/// Declares variables required in AddressBook
/// </summary>
    public class ContactPerson
    {
        public string firstName;
        public string lastName;
        public string address;
        public string city;
        public string state;
        public int zip;
        public double phoneNo;
        public string email;

        /// <summary>
        /// Parameterised Constructor for Contact Person with below parameters
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="phoneNo">The phone no.</param>
        /// <param name="email">The email.</param>
        public ContactPerson(string firstName, string lastName, string address, string city, string state, int zip, double phoneNo, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNo = phoneNo;
            this.email = email;
        }
    }
}