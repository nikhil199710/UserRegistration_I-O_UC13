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
                    bool flag1 = true;
                    while (flag1)
                    {
                        Console.WriteLine("Press 0 to display all contacts");
                        Console.WriteLine("Press 1 To search by state ");
                        Console.WriteLine("Press 2 To search by city");
                        Console.WriteLine("Press 3 To Write using Stream Reader ");
                        Console.WriteLine("Press 4 To Read all the contacts from file using stream Reader");
                        Console.WriteLine("Press 5 To Write Contacts to a Csv File ");
                        Console.WriteLine("Press 6 To Read all the contacts from Csv file ");
                        Console.WriteLine("Press 7 to exit");
                        int checkOption = Convert.ToInt32(Console.ReadLine());
                        switch (checkOption)
                        {
                            case 0:
                                multipleAddressBook.display();
                                break;
                            case 1:
                                addressBook1.SearchingByState();

                                break;
                            case 2:
                                addressBook1.SearchingByCity();
                                break;
                            case 3:
                                AddressBook.WriteUsingStreamReader();
                                break;
                            case 4:
                                AddressBook.ReadFromStreamReader();
                                break;
                            case 5:
                                Console.WriteLine("Write name of the address Book you which you want to write");
                                string nameOfAddressBook = Console.ReadLine();
                                if (Program.dictionaryformultiplerecords.ContainsKey(nameOfAddressBook))
                                {
                                    AddressBook.WritingContactsinCsvFile(dictionaryformultiplerecords, nameOfAddressBook);
                                }
                                else
                                {
                                    Console.WriteLine("addressBook Name not Found");
                                }
                                break;
                            case 6:
                                AddressBook.ReadingContactsFromCsvFile();
                                break;
                            case 7:
                                flag1 = false;
                                break;
                        }
                    }
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