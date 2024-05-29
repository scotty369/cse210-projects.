using System;

namespace EventPlanning
{
    // Address class to encapsulate address details
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State}, {ZipCode}";
        }
    }

    // Base Event class
    public abstract class Event
    {
        private string title;
        private string description;
        private DateTime date;
        private string time;
        private Address address;

        public Event(string title, string description, DateTime date, string time, Address address)
        {
            this.title = title;
            this.description = description;
            this.date = date;
            this.time = time;
            this.address = address;
        }

        public string GetStandardDetails()
        {
            return $"Title: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time}\nAddress: {address}";
        }

        public abstract string GetFullDetails();

        public string GetShortDescription()
        {
            return $"{GetType().Name}: {title} on {date.ToShortDateString()}";
        }
    }

    // Lecture class derived from Event
    public class Lecture : Event
    {
        private string speaker;
        private int capacity;

        public Lecture(string title, string description, DateTime date, string time, Address address, string speaker, int capacity)
            : base(title, description, date, time, address)
        {
            this.speaker = speaker;
            this.capacity = capacity;
        }

        public override string GetFullDetails()
        {
            return $"{GetStandardDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
        }
    }

    // Reception class derived from Event
    public class Reception : Event
    {
        private string rsvpEmail;

        public Reception(string title, string description, DateTime date, string time, Address address, string rsvpEmail)
            : base(title, description, date, time, address)
        {
            this.rsvpEmail = rsvpEmail;
        }

        public override string GetFullDetails()
        {
            return $"{GetStandardDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
        }
    }

    // OutdoorGathering class derived from Event
    public class OutdoorGathering : Event
    {
        private string weatherForecast;

        public OutdoorGathering(string title, string description, DateTime date, string time, Address address, string weatherForecast)
            : base(title, description, date, time, address)
        {
            this.weatherForecast = weatherForecast;
        }

        public override string GetFullDetails()
        {
            return $"{GetStandardDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Address lectureAddress = new Address("123 Main St", "Springfield", "IL", "62701");
            Lecture lecture = new Lecture("Science Lecture", "A lecture on quantum physics", new DateTime(2023, 7, 20), "10:00 AM", lectureAddress, "Dr. John Smith", 100);

            Address receptionAddress = new Address("456 Elm St", "Springfield", "IL", "62701");
            Reception reception = new Reception("Company Reception", "Annual company gathering", new DateTime(2023, 8, 15), "6:00 PM", receptionAddress, "rsvp@company.com");

            Address outdoorAddress = new Address("789 Oak St", "Springfield", "IL", "62701");
            OutdoorGathering outdoorGathering = new OutdoorGathering("Summer Picnic", "Outdoor family picnic", new DateTime(2023, 9, 10), "1:00 PM", outdoorAddress, "Sunny with a chance of rain");

            Event[] events = { lecture, reception, outdoorGathering };

            foreach (Event ev in events)
            {
                Console.WriteLine(ev.GetStandardDetails());
                Console.WriteLine();
                Console.WriteLine(ev.GetFullDetails());
                Console.WriteLine();
                Console.WriteLine(ev.GetShortDescription());
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
