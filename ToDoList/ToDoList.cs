namespace TESTtodo
{
    internal class ToDoList
    {
        public List<ToDo> ToDos { get; set; }

        public ToDoList()
        {
            ToDos = new List<ToDo>();
        }

        //Show ToDo list
        public void ShowList()
        {
            Console.WriteLine("--------------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" >>> DIN TO-DO-LISTA <<<");
            Console.ResetColor();
            Console.WriteLine();

            string readText = File.ReadAllText("toDoList.txt");
            Console.WriteLine(readText);
            Console.WriteLine();
            Console.WriteLine("------------------------");
        }

        //Receive task input and add to list
        public void AddToDo()
        {
            while (true)
            {
                Console.WriteLine("--------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" >>> LÄGG TILL EN UPPGIFT <<<");
                Console.ResetColor();
                Console.WriteLine();

                //To-do input
                Console.Write("Uppgift: ");
                string title = Console.ReadLine();

                bool isTitleEmpty = string.IsNullOrWhiteSpace(title);

                if (title == "exit")
                {
                    break;
                }

                while (isTitleEmpty)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ange uppgift: ");
                    Console.ResetColor();
                    title = Console.ReadLine();
                    isTitleEmpty = string.IsNullOrWhiteSpace(title);
                }

                //Project input
                Console.Write("Projekt: ");
                string project = Console.ReadLine();

                bool isProjectEmpty = string.IsNullOrWhiteSpace(project);

                if (project == "exit")
                {
                    break;
                }

                while (isProjectEmpty)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ange projekt: ");
                    Console.ResetColor();
                    project = Console.ReadLine();
                    isProjectEmpty = string.IsNullOrWhiteSpace(project);
                }

                //Deadline input
                Console.Write("Deadline (YY/MM/DD): ");
                string dueDateInput = Console.ReadLine();

                if (dueDateInput.ToLower().Trim() == "exit")
                {
                    break;
                }

                DateTime dueDate;
                bool isDate = DateTime.TryParse(dueDateInput, out dueDate);

                //Try-catch if input is not DateTime type
                try
                {
                    dueDate = Convert.ToDateTime(dueDateInput);
                }
                catch (FormatException)
                {
                    while (!isDate)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Ange ett giltigt datum: ");
                        Console.ResetColor();
                        dueDateInput = Console.ReadLine();
                        isDate = DateTime.TryParse(dueDateInput, out dueDate);
                    }
                }

                // Error handling if date input is today or earlier
                while (dueDate <= DateTime.Now) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ange ett datum längre fram än dagens datum: ");
                    Console.ResetColor();
                    dueDateInput = Console.ReadLine();
                    isDate = DateTime.TryParse(dueDateInput, out dueDate);
                }

                ToDos.Add(new ToDo(title, project, dueDate, false)); //Add to ToDo list
            }
        }

        //Save properties to text file
        public void SaveListToFile(List<ToDo> list, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (ToDo toDo in list)
                {

                    writer.WriteLine("------------------------");
                    writer.WriteLine($" Uppgift: {toDo.Title}");
                    writer.WriteLine($" Deadline: {toDo.DueDate.ToString("yy/MM/dd")}");
                    writer.WriteLine($" Projekt: {toDo.Project}");
                    if (toDo.Done)
                    {
                        writer.WriteLine(" Klart: JA");
                    }
                    else
                    {
                        writer.WriteLine(" Klart: NEJ");
                    }
                }
            }
        }
    }
}