using System;
using System.Collections.Generic;

namespace AddressBookSystem
{/// <summary>
/// Entry Point of the program 
/// </summary>
    class Program
    {
        public static Dictionary<string, List<ContactPerson>> dictionaryByState = new Dictionary<string, List<ContactPerson>>();
        public static Dictionary<string, List<ContactPerson>> dictionaryByCity = new Dictionary<string, List<ContactPerson>>();
        public static Dictionary<string, List<ContactPerson>> dictionaryformultiplerecords = new Dictionary<string, List<ContactPerson>>();


        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Multiple Address Book ");
            AddressBookOptions();
        }
        public static void AddressBookOptions()
        {
            try
            {
                /// creating instance of Multiple Address Book
                MultipleAddressBook multipleAddressBook = new MultipleAddressBook();
                AddressBook addressBook1 = new AddressBook();

                //Accepting details for name of addressbook and contact details in addressBook
                //While loop will break if the user enters empty string 
                while (true)
                {
                    Console.WriteLine("enter the name of  new Addressbook");
                    string name = Console.ReadLine();
                    if (name == "") break;//while loop will break when the if statement is true 
                    AddressBook addressBook = new AddressBook();
                    bool flag = true;
                    while (flag)
                    {
                        Console.WriteLine("Please enter your firstname");
                        string firstName = Console.ReadLine();
                        if (firstName == "")
                        {
                            break;
                            //while loop will break when the if statement is true 
                        }
                        Console.WriteLine("Please enter your lastname");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Please enter your Address");
                        string address = Console.ReadLine();
                        Console.WriteLine("Please enter your city");
                        string city = Console.ReadLine();
                        Console.WriteLine("Please enter your state");
                        string state = Console.ReadLine();
                        Console.WriteLine("Please enter your zip");
                        int zip = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please enter your phone no");
                        double phoneNo = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Please enter your email");
                        string email = Console.ReadLine();
                        addressBook.AddDetailsOfPersons(firstName, lastName, address, city, state, zip, phoneNo, email);
                    }
                    /// calling function to display contact details
                    addressBook.DisplayContactPersonDetails();
                    Console.WriteLine("Press Y to sort by some other detail");
                    char choice = Convert.ToChar(Console.ReadLine());
                    if (choice == 'Y')
                    {
                        addressBook.DisplayContactPersonDetails();
                    }

                    Console.WriteLine("enter an option ");
                    Console.WriteLine("1. To update the contact details");
                    Console.WriteLine("2. To delete the contact details");
                    int option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("enter the first name & last name of the person to be updated");
                            string newFirstName = Console.ReadLine();
                            string newLastName = Console.ReadLine();
                            addressBook1.UpdateContactPersonDetails(newFirstName, newLastName);
                            break;
                        case 2:
                            Console.WriteLine("enter the first name & last name of the person contact to be delete  ");
                            string fName = Console.ReadLine();
                            string lName = Console.ReadLine();
                            addressBook.DeleteContactPersonDetails(fName, lName);
                            break;
                        default:
                            break;

                    }
                    /// adding details in multiple address book using name enter and AddressBook instance created             
                    multipleAddressBook.AddMultipleAddressBook(name, addressBook);
                    dictionaryformultiplerecords.Add(name, addressBook.addressBookList);

                }
                /// display multiple AddressBook using name of addressbook
                multipleAddressBook.display();
                bool repeatState = true;
                while (repeatState)
                {
                    Console.WriteLine("Press Y to search by state");
                    char stateSearchCheck = Convert.ToChar(Console.ReadLine());
                    if (stateSearchCheck == 'Y')
                    {
                        addressBook1.SearchingByState();
                    }
                    Console.WriteLine("If you wish to enter search again Press Y");
                    char repeatCheck = Convert.ToChar(Console.ReadLine());
                    if (repeatCheck != 'Y')
                        repeatState = false;
                }
                bool repeatCity = true;
                while (repeatCity)
                {
                    Console.WriteLine("Press Y to get contacts by city");
                    char cityCheck = Convert.ToChar(Console.ReadLine());

                    if (cityCheck == 'Y')
                    {
                        addressBook1.SearchingByCity();
                    }
                    Console.WriteLine("If you wish to enter search again Press Y");
                    char repeatCheck = Convert.ToChar(Console.ReadLine());
                    if (repeatCheck != 'Y')
                        repeatCity = false;
                }
                Console.WriteLine("To read all the contacts from streamWriter Press Y otherwise Press N");
                char streamReadCheck = Convert.ToChar(Console.ReadLine());

                if (streamReadCheck == 'Y')
                {
                    AddressBook.ReadFromStreamReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(("Invalid entry"));
                Console.WriteLine(ex.Message);
                AddressBookOptions();
            }
        }

    }
}