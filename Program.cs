using System.Text.Json;


namespace Task_Tracker_CLI
{
    internal class Program
    {
        public const string filePathJson = "tasks.json";

        public static List<Task> tasks = new List<Task>();

        public const string commandList = "-----Task Tracker------\n"
          + "add - to add a a new task\n"
          + "remove - to remove a task\n"
           + "change - to change a tasks status\n"
            + "status - check the status of your tasks\n"
            + "help - get command list\n"
            + "exit - to shutdown\n";
        static void Main(string[] args)
        {
            if (File.Exists(filePathJson))
            {
                string json = File.ReadAllText(filePathJson);
                tasks = JsonSerializer.Deserialize<List<Task>>(json);
            }

            Console.WriteLine(commandList);

            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine().ToLower();

                string[] parts = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = parts[0];

                switch (input)
                {
                    case "add":
                        CommandHandler.ReadAndAdd(tasks);
                        break;
                    case "remove":
                        CommandHandler.ReadAndRemove(tasks);
                        break;
                    case "change":
                        CommandHandler.ReadAndChange(tasks);
                        break;
                    case "status":
                        CommandHandler.PrintStatus(tasks);
                        break;
                    case "help":
                        Console.WriteLine(commandList);
                        break;

                    default:
                        Console.WriteLine("Unrecognized command?");
                        break;
                }

                if (input == "exit")
                {
                    break;
                }
            }
        }

        public static void SaveInJson(List<Task> tasks)
        {
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText(filePathJson, json);
        }
    }
}