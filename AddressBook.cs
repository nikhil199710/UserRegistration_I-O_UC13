using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace AddressBookSystem
{
    /// <summary>
    /// Creates an Addressbook 
    ///Performs Operations like adding, displaying , updating , deleting details using a list 
    /// </summary>
    public class AddressBook
    {
        /// <summary>
        /// The address book list for storing details
        /// </summary>
        public List<ContactPerson> addressBookList;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBook"/> class.
        /// Non Parameterised Constructor for AddressBook
        /// </summary>
        public AddressBook()
        {
            addressBookList = new List<ContactPerson>();
        }

        /// <summary>
        /// Adds the details of persons.
        /// UC7
        /// </summary>     
        public void AddDetailsOfPersons(string firstName, string lastName, string address, string city, string state, int zip, double phoneNo, string eMail)
        {
            /// Creates an instance of class Contact Person 
            /// For Adding the details in the list addressbooklist
            ContactPerson contactPerson = new ContactPerson(firstName, lastName, address, city, state, zip, phoneNo, eMail);
            bool checkForDuplicacy = CheckForDuplicacy(firstName, lastName);
            if (checkForDuplicacy == true)
            {
                addressBookList.Add(contactPerson);
                Console.WriteLine("detail succesfully added");
                //ReadingContactsFromCsvFile(addressBookList);
                //WritingContactsFromCsvFile(addressBookList);

                ///Adding details into dictionaryByState with state as key
                if (Program.dictionaryByState.ContainsKey(contactPerson.state))
                {
                    Program.dictionaryByState[contactPerson.state].Add(contactPerson);
                }
                else
                {
                    List<ContactPerson> list = new List<ContactPerson>();
                    list.Add(contactPerson);
                    Program.dictionaryByState.Add(contactPerson.state, list);
                }

                ///Adding details into dictionaryByCity with city as key
                if (Program.dictionaryByCity.ContainsKey(contactPerson.city))
                {
                    Program.dictionaryByCity[contactPerson.city].Add(contactPerson);
                }
                else
                {
                    List<ContactPerson> list = new List<ContactPerson>();
                    list.Add(contactPerson);
                    Program.dictionaryByCity.Add(contactPerson.city, list);
                }
            }
        }

        /// <summary>
        /// Displays the contact person details.
        /// </summary>
        public void DisplayContactPersonDetails()
        {
            ///sorting  using lambda function
            Console.WriteLine(" Press 1 to display by name ");
            Console.WriteLine(" Press 2 to display by city ");
            Console.WriteLine(" Press 3 to display by state ");
            Console.WriteLine(" Press 4 to display by zip ");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("The Details of Contact Number sorted by name ");
                    addressBookList.Sort((ContactPerson1, ContactPerson2) => ContactPerson1.firstName.CompareTo(ContactPerson2.firstName));
                    break;
                case 2:
                    Console.WriteLine("The Details of Contact Number sorted by city ");
                    addressBookList.Sort((ContactPerson1, ContactPerson2) => ContactPerson1.city.CompareTo(ContactPerson2.city));
                    break;
                case 3:
                    Console.WriteLine("The Details of Contact Number sorted by state");
                    addressBookList.Sort((ContactPerson1, ContactPerson2) => ContactPerson1.state.CompareTo(ContactPerson2.state));
                    break;
                case 4:
                    Console.WriteLine("The Details of Contact Number sorted by zip ");
                    addressBookList.Sort((ContactPerson1, ContactPerson2) => ContactPerson1.zip.CompareTo(ContactPerson2.zip));
                    break;
                default:
                    Console.WriteLine("By default sorting is done by name");
                    addressBookList.Sort((ContactPerson1, ContactPerson2) => ContactPerson1.firstName.CompareTo(ContactPerson2.firstName));
                    break;
            }

            foreach (ContactPerson contactPerson in addressBookList)
            {
                Console.WriteLine("firstName : " + contactPerson.firstName + "  last name  :" + contactPerson.lastName + " address : " + contactPerson.address + " city : " + contactPerson.city + " state : " + contactPerson.state + "  zip : " + contactPerson.zip + " phone number : " + contactPerson.phoneNo + "  email :" + contactPerson.email);
            }
        }

        /// <summary>
        /// Updates the contact person details.      
        /// </summary>
        public void UpdateContactPersonDetails(string newFirstName, string newLastName)
        {
            foreach (ContactPerson contactPerson in addressBookList)
            {
                ///checking if the  full name  in the list matches with the full name entered by the user 
                if (newFirstName == contactPerson.firstName && newLastName == contactPerson.lastName)
                {
                    Console.WriteLine("Enter the details to be updated");
                    contactPerson.address = Console.ReadLine();
                    contactPerson.city = Console.ReadLine();
                    contactPerson.state = Console.ReadLine();
                    contactPerson.zip = Convert.ToInt32(Console.ReadLine());
                    contactPerson.phoneNo = Convert.ToDouble(Console.ReadLine());
                    contactPerson.email = Console.ReadLine();
                    Console.WriteLine("details has been updated");
                    Console.WriteLine("the updated list is-");
                    DisplayContactPersonDetails();
                }
            }
        }

        /// <summary>
        /// Deletes the contact person details.
        /// </summary>  
        public void DeleteContactPersonDetails(string fName, string lName)
        {
            foreach (ContactPerson contactPerson in addressBookList)
            {
                if (fName == contactPerson.firstName && lName == contactPerson.lastName)
                {
                    addressBookList.Remove(contactPerson);
                    Console.WriteLine("contact person deleted");
                    Console.WriteLine("updated list is ");
                    DisplayContactPersonDetails();
                }
            }
        }

        /// <summary>
        /// Check for Duplicacy 
        /// UC7
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public bool CheckForDuplicacy(string firstName, string lastName)
        {
            foreach (ContactPerson person in addressBookList)
            {
                if (person.firstName.Equals(firstName) && person.lastName.Equals(lastName))
                {
                    Console.WriteLine("Person with this full name already exists in the contact book");

                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Searching By State()
        /// UC9,UC10
        /// </summary>
        public void SearchingByState()
        {
            ///index used for checking count for a state searched
            int index = 0;
            Console.WriteLine("enter the name of state you wish to search");
            string searchState = Console.ReadLine();
            /// if searchState is not present in the dictionary as key then new list is created and added
            /// otherwise added in the same list 
            if (!Program.dictionaryByState.ContainsKey(searchState))
            {
                Console.WriteLine("no such state records found");
                return;
            }
            foreach (ContactPerson contactPerson in Program.dictionaryByState[searchState])
            {
                index++;
                Console.WriteLine("firstName : " + contactPerson.firstName + "  last name  :" + contactPerson.lastName + " address : " + contactPerson.address + " city : " + contactPerson.city + " state : " + contactPerson.state + "  zip : " + contactPerson.zip + " phone number : " + contactPerson.phoneNo + "  email :" + contactPerson.email);
            }
            Console.WriteLine("The count of {0} is {1}", searchState, index);
        }

        /// <summary>
        /// Searching by city
        /// UC9 ,UC10
        /// </summary>
        public void SearchingByCity()
        {
            ///index used for checking count for a city searched
            int index = 0;
            Console.WriteLine("enter the name of city you wish to search");
            string searchCity = Console.ReadLine();
            /// if searchCity is not present in the dictionary as key then new list is created and added
            /// otherwise added in the same list 

            if (!Program.dictionaryByCity.ContainsKey(searchCity))
            {
                Console.WriteLine("no such city records found");
                return;
            }
            foreach (ContactPerson contactPerson in Program.dictionaryByCity[searchCity])
            {
                index++;
                Console.WriteLine("firstName : " + contactPerson.firstName + "  last name  :" + contactPerson.lastName + " address : " + contactPerson.address + " city : " + contactPerson.city + " state : " + contactPerson.state + "  zip : " + contactPerson.zip + " phone number : " + contactPerson.phoneNo + "  email :" + contactPerson.email);
            }
            Console.WriteLine("The count of {0} is {1}", searchCity, index);
        }

        /// <summary>
        /// Writing using Stream Reader
        /// UC13
        /// </summary>
        public static void WriteUsingStreamReader()
        {
            ///writing all the entries in addressbook to path using stream reader           
            Console.WriteLine("Writing contacts to file");
            ///declaring a path 
            string path = "C:/Users/Administrator/Desktop/FileIOOperation-AddressBook/FileIOoperation-AddressBook/FileIOoperation-AddressBook/Records.txt";
            ///checking if file exists
            if (File.Exists(path))
            {
                ///if it exists stream writer func is called from Io stream
                using (StreamWriter stream = File.AppendText(path))
                {
                    /// for each addressbook stream.writeline will print the addressbookk
                    foreach (KeyValuePair<String, List<ContactPerson>> keyvaluepair in Program.dictionaryformultiplerecords)
                    {
                        string name = keyvaluepair.Key;
                        List<ContactPerson> list = (List<ContactPerson>)keyvaluepair.Value;
                        stream.WriteLine("Address Book Name: " + name);
                        foreach (ContactPerson contactPerson in list)
                        {
                            stream.WriteLine("firstName : " + contactPerson.firstName + "  last name  :" + contactPerson.lastName + " address : " + contactPerson.address + " city : " + contactPerson.city + " state : " + contactPerson.state + "  zip : " + contactPerson.zip + " phone number : " + contactPerson.phoneNo + "  email :" + contactPerson.email);
                        }
                    }
                    Console.WriteLine("Address Book written into the file successfully");
                    stream.Flush();
                }
            }
        }

        /// <summary>
        /// Reading using Stream Reader
        /// UC13
        /// </summary>
        public static void ReadFromStreamReader()
        {
            ///path is defined for reading
            string path = "C:/Users/Administrator/Desktop/FileIOOperation-AddressBook/FileIOoperation-AddressBook/FileIOoperation-AddressBook/Records.txt";
            using (StreamReader sr = File.OpenText(path))
            {
                ///until line is null streamReader instance sr will read till then 
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                ///it is mandatory to close and flush otherwise errors will be thrown
                sr.Close();
            }
        }

        /// <summary>
        /// Reading Contacts From CsvFile
        /// UC14
        /// </summary>
        public static void ReadingContactsFromCsvFile()
        {
            Console.WriteLine("Reading Contacts from csv file ");
            foreach (KeyValuePair<string, List<ContactPerson>> keyValuePair in Program.dictionaryformultiplerecords)
            {
                string path = @"C:\Users\Administrator\Desktop\FileIOOperation-AddressBook\FileIOoperation-AddressBook\FileIOoperation-AddressBook" + keyValuePair.Key + ".csv";
                if (File.Exists(path))
                {
                    var reader = new StreamReader(path);
                    var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    List<ContactPerson> records = csv.GetRecords<ContactPerson>().ToList();
                    foreach (ContactPerson contactPerson in records)
                    {
                        Console.WriteLine("firstName : " + contactPerson.firstName + "  last name  :" + contactPerson.lastName + " address : " + contactPerson.address + " city : " + contactPerson.city + " state : " + contactPerson.state + "  zip : " + contactPerson.zip + " phone number : " + contactPerson.phoneNo + "  email :" + contactPerson.email);

                    }
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Writing Contacts in CsvFile
        /// UC14
        /// </summary>
        /// <param name="dictionaryOfMultipleRecord"></param>
        /// <param name="nameOfBook"></param>
        public static void WritingContactsinCsvFile(Dictionary<string, List<ContactPerson>> dictionaryOfMultipleRecord, String nameOfBook)
        {
            ///creating different files for diff addressbook so path changes accordingly
            foreach (KeyValuePair<string, List<ContactPerson>> keyValuePair in dictionaryOfMultipleRecord)
            {
                string name = keyValuePair.Key;
                ///creating a list 
                List<ContactPerson> list = keyValuePair.Value;
                if (name.Equals(nameOfBook))
                {
                    string path = "C:/Users/Administrator/Desktop/FileIOOperation-AddressBook/FileIOoperation-AddressBook/FileIOoperation-AddressBook" + nameOfBook + ".csv";
                    using (StreamWriter stream = new StreamWriter(path))
                    {
                        ///instatiating csvreader object to read csv file
                        using (CsvWriter writer = new CsvWriter(stream, CultureInfo.InvariantCulture))
                        {
                            ///csv writer will write records in the list to the path
                            writer.WriteRecords(list);
                            /// flush the csv writer
                            writer.Flush();
                        }
                        Console.WriteLine("successfull writing in csv file ");
                        ///closing stream is also mandatory
                        stream.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Writing Contacts As JsonFile
        /// UC15
        /// </summary>
        /// <param name="dictionaryOfMultipleRecord"></param>
        /// <param name="nameOfBook"></param>
        public static void WritingContactsAsJsonFile(Dictionary<string, List<ContactPerson>> dictionaryOfMultipleRecord, String nameOfBook)
        {

            ///creating different files for diff addressbook so path changes accordingly
            foreach (KeyValuePair<string, List<ContactPerson>> keyValuePair in dictionaryOfMultipleRecord)
            {
                string path = "C:/Users/Administrator/Desktop/FileIOOperation-AddressBook/FileIOoperation-AddressBook/FileIOoperation-AddressBook" + nameOfBook + ".json";
                string name = keyValuePair.Key;
                List<ContactPerson> list = keyValuePair.Value;
                if (name.Equals(nameOfBook))
                {
                    ///creating a streamwriter instance
                    StreamWriter stream = new StreamWriter(path);
                    ///serialising it to conver it into json file
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    JsonWriter jsonWriter = new JsonTextWriter(stream);
                    serializer.Serialize(stream, list);
                    stream.Flush();
                    stream.Close();
                    Console.WriteLine("details added successfully in json");
                }
            }
        }

        /// <summary>
        /// Reading Contacts From JsonFile()
        /// UC15
        /// </summary>
        public static void ReadingContactsFromJsonFile()
        {
            Console.WriteLine("Reading Contacts from json files ");
            foreach (KeyValuePair<string, List<ContactPerson>> keyValuePair in Program.dictionaryformultiplerecords)
            {
                //defining path for each address book
                string path = "C:/Users/Administrator/Desktop/FileIOOperation-AddressBook/FileIOoperation-AddressBook/FileIOoperation-AddressBook" + keyValuePair.Key + ".json";
                ///deserializing object from json file, reading files and typecasting to list
                List<ContactPerson> list = JsonConvert.DeserializeObject<List<ContactPerson>>(File.ReadAllText(path));
                foreach (ContactPerson contactPerson in list)
                {
                    Console.WriteLine("firstName : " + contactPerson.firstName + "  last name  :" + contactPerson.lastName + " address : " + contactPerson.address + " city : " + contactPerson.city + " state : " + contactPerson.state + "  zip : " + contactPerson.zip + " phone number : " + contactPerson.phoneNo + "  email :" + contactPerson.email);
                    Console.WriteLine("/n");
                }
            }
        }




    }
}
