using System.Reflection;
using System.Threading.Tasks;

namespace TESTtodo
{
    internal class ToDoList
    {
        public List<ToDo> ToDos { get; set; }

        public ToDoList()
        {
            ToDos = new List<ToDo>();
        }

        //Receive task input and add to list Method
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

                if (title == "4")
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

                if (project == "4")
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

                if (dueDateInput.ToLower().Trim() == "4")
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

        //Print list Method
        public void PrintList()
        {
            FileHandle fileHandler = new FileHandle();

            Console.WriteLine();
            Console.Write("Vill du se listan sorterad på (1) deadline eller (2) projekt? "); //Sort by project or due date
            string showListInput = Console.ReadLine();

            bool isInt = int.TryParse(showListInput, out int intInput);

            //Error handling for wrong type input
            while (!isInt) //If not an int
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ange en siffra för att se en sorterad lista - " +
                            "(1) för sorterad på deadline och (2) för sorterad på projekt: ");
                Console.ResetColor();
                showListInput = Console.ReadLine();
                isInt = int.TryParse(showListInput, out intInput);
            }

            if (isInt)
            {
                while (intInput != 1 && intInput != 2) //If input is not 1 or 2
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ange (1) för lsita sorterad på deadline och (2) för lista sorerad på projekt: ");
                    Console.ResetColor();
                    showListInput = Console.ReadLine();
                    isInt = int.TryParse(showListInput, out intInput);
                }

                Console.WriteLine();
                Console.WriteLine("--------------------------------");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" >>> DIN TO-DO-LISTA <<<");
                Console.ResetColor();
                Console.WriteLine();

                if (showListInput == "1")
                {
                    ToDos = ToDos.OrderBy(t => t.DueDate).ToList();
                }
                else if (showListInput == "2")
                {
                    ToDos = ToDos.OrderBy(t => t.Project).ToList();
                }

                foreach (ToDo todo in ToDos)
                {
                    Console.WriteLine($"Uppgift: {todo.Title}");
                    Console.WriteLine($"Projekt: {todo.Project}");
                    Console.WriteLine($"Deadline: {todo.DueDate.ToString("yy/MM/dd")}");
                    if (todo.Done == false)
                    {
                        Console.WriteLine($"Klart: NEJ");
                    }
                    else if (todo.Done == true)
                    {
                        Console.WriteLine($"Klart: JA");
                    }
                    Console.WriteLine("---");
                }
            }
            
        }
    }
}