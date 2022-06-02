using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace Mando.Tool.ParseXml
{
    public class Application : IApplication
    {
        public readonly ILogger<Application> _logger;

        public Application(ILogger<Application> logger)
        {
            _logger = logger;
        }
        public void Run()
        {
            Console.WriteLine("Parse Xml data.xml");

            // List<Person> inputdata = new List<Person>();
            People? people = null;

            XmlSerializer serializer = new XmlSerializer(typeof(People));
            using (StreamReader reader = new StreamReader("data.xml"))
            {
                people = (People)(serializer.Deserialize(reader));
            }
            foreach (Person person in people.person)
            {
                Console.WriteLine(person.Firstname);
            }


        }

    }

    [XmlRoot("people")]
    public class People
    {
        [XmlElement("person")]
        public Person[]? person { get; set; }
    }

    [Serializable]
    public class Person
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("firstname")]
        public string? Firstname { get; set; }
        [XmlElement("lastname")]
        public string? Lastname { get; set; }
        [XmlElement("city")]
        public string? City { get; set; }
    }
    public class DataReadyPerson
    {
        public int DataReadPersonId { get; set; }
        public string? fname { get; set; }
        public string? lname { get; set; }
        public string? CityOfResidence { get; set; }
    }
}