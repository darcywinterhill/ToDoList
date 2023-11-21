namespace TESTtodo
{
    public class ToDoList
    {
        public List<ToDo> ToDos { get; set; }

        public ToDoList()
        {
            ToDos = new List<ToDo>();
        }

        //File path for text file
        public string filePath = "textList.txt";

        //Show options for ToDo list
        public void ShowOptions()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Gör ditt val -->");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" (1) Visa ToDo-listan (sorterad på deadline eller projekt).");
            Console.WriteLine(" (2) Lägg till en uppgift till listan.");
            Console.WriteLine(" (3) Redigera en uppgift (uppdatera, markera som klar, ta bort).");
            Console.WriteLine(" (4) Spara och avsluta.");
        }

        //Choose what to do with ToDo list
        public void ChooseOption()
        {
            while (true)
            {
                ShowOptions();

                Console.WriteLine();
                Console.Write("Mitt val: ");
                string userInput = Console.ReadLine();

                bool isInputInt = int.TryParse(userInput, out int input);

                try //Try-catch if input is not int type
                {
                    input = Convert.ToInt32(userInput);
                }
                catch (FormatException)
                {
                    while (!isInputInt) //Error message if input is not an int
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Gör ditt val genom att välja (1), (2), (3) eller (4): ");
                        Console.ResetColor();
                        userInput = Console.ReadLine();
                        isInputInt = int.TryParse(userInput, out input);
                    }
                }

                while (input != 1 && input != 2 && input != 3 && input != 4) //Error message if input is not 1, 2, 3 or 4
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Du kan endast välja (1), (2), (3) eller (4): ");
                    Console.ResetColor();
                    userInput = Console.ReadLine();
                    isInputInt = int.TryParse(userInput, out input);
                }

                switch (input)
                {
                    case 1: //
                        LoadDataFromFile(); //Load saved list
                        PrintList(); //Display saved list
                        break;
                    case 2:
                        AddToDo(); //Add ToDo to list
                        break;
                    case 3:
                        EditToDo();
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(" >>> HEJDÅ! <<<");
                        Console.ResetColor();
                        System.Environment.Exit(1); //Exit application
                        break;
                }
            }
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

                try
                {
                    ToDos.Add(new ToDo(title, project, dueDate, false));
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Uppgiften registrerades inte.");
                    Console.ResetColor();
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(" >> Välj (4) för att spara uppgiften till din ToDo-lista," +
                    " eller (2) för att fortsätta lägga till uppgifter: ");//Add to ToDo list
                Console.ResetColor();
                string addInput = Console.ReadLine();

                bool isInputInt = int.TryParse(addInput, out int input);

                try //Try-catch if input is not int type
                {
                    input = Convert.ToInt32(addInput);
                }
                catch (FormatException)
                {
                    while (!isInputInt) //Error message if input is not an int
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Välj (4) för att spara och (2) för att fortsätta lägga till uppgifter: ");
                        Console.ResetColor();
                        addInput = Console.ReadLine();
                        isInputInt = int.TryParse(addInput, out input);
                    }
                }

                while (input != 4 && input != 2) //Error message if input is not 4 or 2
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Du kan endast välja (4) eller (2): ");
                    Console.ResetColor();
                    addInput = Console.ReadLine();
                    isInputInt = int.TryParse(addInput, out input);
                }

                if (input == 4) //Save ToDo/s to file
                {
                    try
                    {
                        SaveListToFile(filePath);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Uppgiften sparades till ToDo-listan!");
                        Console.ResetColor();
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Uppgiften kunde inte sparas till ToDo-listan.");
                        Console.ResetColor();
                    }
                    finally
                    {
                        ChooseOption();
                    }
                }

                else if (input == 2) //Continue adding ToDos
                {
                    AddToDo();
                }

            }
        }

        //Save list to file
        public void SaveListToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (ToDo toDo in ToDos)
                {
                    writer.WriteLine($"{toDo.Title},{toDo.Project},{toDo.DueDate.ToString("yy/MM/dd")},{toDo.Done}");

                }
            }
        }

        //Load list from file
        public void LoadDataFromFile()
        {
            if (File.Exists(filePath))
            {
                ToDos.Clear();
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        string title = parts[0];
                        string project = parts[1];
                        DateTime dueDate = DateTime.Parse(parts[2]);
                        bool done = bool.Parse(parts[3]);

                        ToDo toDo = new ToDo(title, project, dueDate, done);
                        //{
                        //    Done = done
                        //};
                        ToDos.Add(toDo);
                    }
                }
            }
        }

        public void EditToDo()
        {
            Console.WriteLine();

            int id = 1;
            foreach (ToDo todo in ToDos)
            {
                Console.WriteLine($"Id: {id}");
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
                id++;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" >> Vilken uppgift vill du uppdatera? Ange Id: ");
            Console.ResetColor();
            int idToEdit = int.Parse(Console.ReadLine());

            ToDo toDoToEdit = ToDos[idToEdit - 1];

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" >> Vill du uppdatera (1) 'Uppgift', (2) 'Projekt', (3) 'Deadline' " +
                                "eller (4) om uppgiften är klar eller inte? ");
            Console.ResetColor();
            int propertyChoice = int.Parse(Console.ReadLine());
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" >> Uppdatera din uppgift: ");
            Console.ResetColor();
            string newValue = Console.ReadLine();

            switch (propertyChoice)
            {
                case 1:
                    toDoToEdit.Title = newValue;
                    break;
                case 2:
                    toDoToEdit.Project = newValue;
                    break;
                case 3:
                    toDoToEdit.DueDate = DateTime.Parse(newValue);
                    break;
                case 4:
                    toDoToEdit.Done = bool.Parse(newValue);
                    break;
                default:
                    Console.WriteLine("Ogiltig input.");
                    break;
            }
            SaveListToFile(filePath);
        }

        //Print list Method
        public void PrintList()
        {
            LoadDataFromFile();

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
                    Console.Write("Ange (1) för lista sorterad på deadline och (2) för lista sorterad på projekt: ");
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

                int id = 1;
                foreach (ToDo todo in ToDos)
                {
                    Console.WriteLine($"Id: {id}");
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
                    id++;
                }
            }
        }
    }
}