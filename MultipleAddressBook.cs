using System;
using System.Collections.Generic;

namespace AddressBookSystem
{
    /// <summary>
    /// Creates a Multiple AddressBook
    /// </summary>
    public class MultipleAddressBook
    {
        /// <summary>
        /// The address book dictionary to accepts Multiple Address Book
        /// </summary>
        public Dictionary<string, AddressBook> addressBookDictionary;
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipleAddressBook"/> class.
        /// Non Parameterised Constructor
        /// </summary>
        public MultipleAddressBook()
        {
            addressBookDictionary = new Dictionary<string, AddressBook>();
        }

        /// <summary>
        /// Adds the multiple address book using dictionary.
        /// </summary>      
        public void AddMultipleAddressBook(string name, AddressBook addressB)
        {
            addressBookDictionary.Add(name, addressB);
        }

        /// <summary>
        /// Displays the specified address book using the name given for AddressBook
        /// </summary>
        /// <param name="name">The name.</param>
        public void display()
        {
            foreach (KeyValuePair<string, AddressBook> element in addressBookDictionary)
            {
                Console.WriteLine("The Name of the address book is : " + element.Key);
                Console.WriteLine("The contact details are :");
                element.Value.DisplayContactPersonDetails();
            }
        }
    }
}