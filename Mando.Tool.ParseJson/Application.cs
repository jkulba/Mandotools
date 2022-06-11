using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Mando.Tool.ParseJson
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
            IdentifyNextStep();
        }

        private void IdentifyNextStep()
        {
            string selectedAction = "";

            do
            {
                selectedAction = GetActionChoice();

                Console.WriteLine();

                switch (selectedAction)
                {
                    case "1":
                        string? filename = string.Empty;
                        Console.Write("Enter file name: ");
                        filename = Console.ReadLine();
                        Console.WriteLine(filename);

                        List<Person> inputdata = new List<Person>();

                        using (StreamReader r = new StreamReader(filename!))
                        {
                            string json = r.ReadToEnd();
                            inputdata = JsonSerializer.Deserialize<List<Person>>(json);
                        }

                        List<DataReadyPerson> destination = inputdata.Select(d => new DataReadyPerson
                        {
                            CityOfResidence = d.City,
                            fname = d.Firstname,
                            lname = d.Lastname,
                            DataReadPersonId = d.Id
                        }).ToList();

                        string jsonString = JsonSerializer.Serialize(destination, new JsonSerializerOptions() { WriteIndented = true });
                        using (StreamWriter outputFile = new StreamWriter("dataRead.json"))
                        {
                            outputFile.WriteLine(jsonString);
                        }

                        break;
                    case "2":
                        Console.WriteLine("Option 2");
                        break;
                    case "3":
                        Console.WriteLine("Option 3");
                        break;
                    default:
                        Console.WriteLine("This is an invalid choice. Hit enter and try again.");
                        break;
                }
                Console.WriteLine("Hit return to continue...");
                Console.ReadLine();

            } while (selectedAction != "2");
        }

        private string GetActionChoice()
        {
            string? output = string.Empty;

            Console.Clear();
            Console.WriteLine("Menu Options".ToUpper());
            Console.WriteLine("1 - Load JSON File");
            Console.WriteLine("2 - Exit");
            Console.Write("What would you like to choose: ");
            output = Console.ReadLine();

            return output!;
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string City { get; set; }
    }
    public class DataReadyPerson
    {
        public int DataReadPersonId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string CityOfResidence { get; set; }
    }
}