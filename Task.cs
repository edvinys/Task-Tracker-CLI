using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker_CLI
{
    internal class Task
    {
        public Task(string? name, string? description)
        {
            Name = name;
            Description = description;
            State = "Not started";
        }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? State { get; set; }

    }
}
