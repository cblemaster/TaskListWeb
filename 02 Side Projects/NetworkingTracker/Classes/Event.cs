using System;
//using System.Collections.Generic;
//using System.Collections.Generic;
//using System.Text;

namespace NetworkingTracker.Classes
{
    class Event
    {

        // methods needed
        // add networking event
        // display networking event as a string
        // display list sorted by date (ascending)
        // edit networking event
        // delete networking event

        // TO DO - create data store; could be access, local sql server, xml file, json file...

        // Properties, using backing variables
        private DateTime date;
        public DateTime Date
        {
            get { return this.date; }

        }
        private string duration; // Duration of event (in minutes); could also be "n/a" or "All Day"
        public string Duration
        {
            get { return this.duration; }
        }
        private string name;    // Event name; could also be "n/a"
        public string Name
        {
            get { return this.name; }
        }
        private string notes;
        public string Notes
        {
            get { return this.notes; }
        }

        // Constructors

        // with parameters
        public Event(DateTime date, string duration, string name, string notes)
        {
            this.date = date;
            this.duration = duration;
            this.name = name;
            this.notes = notes;
        }

        // without parameters
        public Event()
        {

        }


    }
}
