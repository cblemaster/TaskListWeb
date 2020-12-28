using NetworkingTracker.Classes;
using System;
using System.Collections.Generic;

namespace NetworkingTracker
{
    class Program
    {
        // create list of events
        private static List<Event> networkingEvents = new List<Event>();

        static void Main(string[] args)
        {
            // add some networking events
            Event e1 = new Event(new DateTime(2020, 9, 27), "90", "MeetUp", "Made 3 new connections");
            Event e2 = new Event(new DateTime(2020, 9, 17), "60", "Tea and Tech", "Featured guest Joe Biden");
            Event e3 = new Event(new DateTime(2020, 9, 23), "n/a", "n/a", "Connected with Paul Reubens on LinkedIn");
            networkingEvents.Add(e1);
            networkingEvents.Add(e2);
            networkingEvents.Add(e3);

            // display networking event
            Console.WriteLine("Date\tDuration\tName\tNotes");
            foreach (Event e in networkingEvents)
            {
                // TODO - fix formatting on this...
                Console.WriteLine(e.Date.ToString("MM/dd/yyyy") + "\t"+ e.Duration + "\t" + e.Name + "\t" + e.Notes);
            }
        }
    }
}
