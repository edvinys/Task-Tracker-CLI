using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker_CLI
{
    internal class CommandHandler
    {
        public static void ReadAndAdd(List<Task> tasks)
        {
            Console.WriteLine();
            Console.Write("Name of task - ");
            string name = Console.ReadLine();
            Console.Write("Description of task - ");
            string description = Console.ReadLine();
            Console.WriteLine();

            if (string.Empty == name)
            {
                Console.WriteLine("Error: no name for the task");
                return;
            }

            Task task = new Task(name, description);

            tasks.Add(task);
            Program.SaveInJson(tasks);
        }
        public static void ReadAndRemove(List<Task> tasks)
        {
            Console.WriteLine();
            Console.Write("Name of task to remove - ");
            string taskName = Console.ReadLine();
            Console.WriteLine();


            tasks.RemoveAll(task => task.Name == taskName);

            Console.WriteLine("Removed successfully");
            Console.WriteLine();

            Program.SaveInJson(tasks);
        }
        public static void ReadAndChange(List<Task> tasks)
        {
            Console.WriteLine();
            Console.Write("Name of task to change - ");
            string taskName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("New status: ");
            Console.WriteLine("1 - Finished");
            Console.WriteLine("2 - In progress");
            Console.WriteLine("3 - Not started");
            Console.WriteLine("4 - Return");
            Console.WriteLine();

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            string newStatus = null;

            Thread.Sleep(1000);

            if (keyInfo.Key == ConsoleKey.D1)
            {
                newStatus = "Finished";
                Console.WriteLine();
                Console.WriteLine("Would you like to delete the task?");
                Console.WriteLine("y - yes");
                Console.WriteLine("n - no");
                Console.WriteLine();
                Console.Write("> ");
                ConsoleKeyInfo keyInfo1 = Console.ReadKey();
                Console.WriteLine();

                bool validKey = false;

                while (!validKey)
                {
                    validKey = true;

                    if (keyInfo1.Key == ConsoleKey.Y)
                    {
                        tasks.RemoveAll(task => task.Name == taskName);
                        Console.WriteLine();
                        Console.WriteLine("Deleted successfully");
                        Console.WriteLine();
                        return;
                    }
                    else if (!(keyInfo1.Key == ConsoleKey.N))
                    {
                        Console.WriteLine("Error: wrong key");
                        validKey = false;
                    }

                }
            }
            else if (keyInfo.Key == ConsoleKey.D2)
            {
                newStatus = "In progress";
            }
            else if (keyInfo.Key == ConsoleKey.D3)
            {
                newStatus = "Not Started";
            }
            else if (keyInfo.Key == ConsoleKey.D4)
            {
                return;
            }
            else
            {
                Console.WriteLine("Wrong key, try again");
                return;
            }

            Program.SaveInJson(tasks);
            
            List<Task> needChange = tasks.FindAll(task => task.Name == taskName);

            if (needChange.Count == 0)
            {
                Console.WriteLine("Couldn't quite find the task you were looking for");
                return;
            }

            ChangeTaskStatus(needChange, newStatus);

            Console.WriteLine();
            Console.WriteLine("Changed successfully.");
            Console.WriteLine();

            Program.SaveInJson(tasks);
        }
        private static void ChangeTaskStatus(List<Task> tasks, string newStatus)
        {
            foreach(Task task in tasks)
            {
                task.State = newStatus;
            }
        }
        public static void PrintStatus(List<Task> tasks)
        {
            Console.WriteLine();

            foreach (Task task in tasks)
            {
                Console.WriteLine($"{task.Name} - {task.Description} - {task.State}");
            }

            Console.WriteLine();
        }
    }
}
