namespace ToDoListProject
{
    public class ToDoList
    {
        public List<ToDo> ToDos { get; set; }

        private FileHandle fileHandle;

        public ToDoList()
        {
            ToDos = new List<ToDo>();
            fileHandle = new FileHandle(filePath);
        }

        //File path for text file
        public string filePath = "textList.txt";

        /* ** LIST OF METHODS **
         * ShowOptions
         * ExitApp
         * DisplayErrorMessage
         * DisplaySuccessMessage
         * HandleListIsEmpty
         * ShowToDoAndId
         * ChooseOption
         * AddToDo
         * EditToDo
         * PrintList */

        //Show options for ToDo list
        public void ShowOptions()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Gör ditt val -->");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(" (1) Visa ToDo-listan (sorterad på deadline, projekt eller klar/inte klar).");
            Console.WriteLine(" (2) Lägg till en uppgift till listan.");
            Console.WriteLine(" (3) Redigera en uppgift (uppdatera eller ta bort).");
            Console.WriteLine(" (4) Spara och avsluta.");
        }

        public static void ExitApp()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" >>> HEJDÅ! <<<");
            Console.ResetColor();
            System.Environment.Exit(1);
        }

        //General error message
        public static void DisplayErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ResetColor();
        }

        //General success message
        public static void DisplaySuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ResetColor();
        }

        //List is empty message
        public void HandleListIsEmpty()
        {
            int listLength = ToDos.Count();

            while (listLength == 0)
            {
                Console.WriteLine();
                DisplayErrorMessage("Din ToDo-lista är tom.");
                Console.WriteLine();
                ChooseOption();
            }
        }

        //Displays the ToDos and adds an Id 
        public void ShowToDoAndId()
        {
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
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("---");
                Console.ResetColor();
                id++;
            }
        }

        //Choose what to do in ToDo list
        public void ChooseOption()
        {
            while (true)
            {
                ShowOptions();

                Console.WriteLine();
                Console.Write("Mitt val: ");
                string optionInput = Console.ReadLine();

                bool isOptionInputInt = int.TryParse(optionInput, out int optionInputInt);

                try //Try-catch if input is not int type
                {
                    optionInputInt = Convert.ToInt32(optionInput);
                }
                catch (FormatException)
                {
                    while (!isOptionInputInt) //Error message if input is not an int
                    {
                        DisplayErrorMessage("Gör ditt val genom att välja (1), (2), (3) eller (4): ");
                        optionInput = Console.ReadLine();
                        isOptionInputInt = int.TryParse(optionInput, out optionInputInt);
                    }
                }

                while (optionInputInt < 1 || optionInputInt > 4) //Error message if input is not 1 - 4
                {
                    DisplayErrorMessage("Du kan endast välja (1), (2), (3) eller (4): ");
                    optionInput = Console.ReadLine();
                    isOptionInputInt = int.TryParse(optionInput, out optionInputInt);
                }

                switch (optionInputInt)
                {
                    case 1: //
                        HandleListIsEmpty();
                        PrintList(); //Display saved list
                        break;
                    case 2:
                        AddToDo(); //Add ToDo to list
                        break;
                    case 3:
                        HandleListIsEmpty();
                        EditToDo(); //Edit ToDo and save to list
                        break;
                    case 4:
                        ExitApp(); //Exit application
                        break;
                }
            }
        }

        //Receive ToDo input and add to list
        public void AddToDo()
        {
            while (true)
            {
                Console.WriteLine("--------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" >>> LÄGG TILL EN UPPGIFT <<<");
                Console.ResetColor();
                Console.WriteLine();

                //Task input
                Console.Write("Uppgift: ");
                string title = Console.ReadLine();

                bool isTitleInputEmpty = string.IsNullOrWhiteSpace(title);

                while (isTitleInputEmpty)
                {
                    DisplayErrorMessage("Ange uppgift: ");
                    title = Console.ReadLine();
                    isTitleInputEmpty = string.IsNullOrWhiteSpace(title);
                }

                //Project input
                Console.Write("Projekt: ");
                string project = Console.ReadLine();

                bool isProjectInputEmpty = string.IsNullOrWhiteSpace(project);

                while (isProjectInputEmpty)
                {
                    DisplayErrorMessage("Ange projekt: ");
                    project = Console.ReadLine();
                    isProjectInputEmpty = string.IsNullOrWhiteSpace(project);
                }

                //Deadline input
                Console.Write("Deadline (YY/MM/DD): ");
                string dueDateInput = Console.ReadLine();

                DateTime dueDate;
                bool isDueDateDate = DateTime.TryParse(dueDateInput, out dueDate);

                //Try-catch if input is not DateTime type
                try
                {
                    dueDate = Convert.ToDateTime(dueDateInput);
                }
                catch (FormatException)
                {
                    while (!isDueDateDate)
                    {
                        DisplayErrorMessage("Ange ett giltigt datum: ");
                        dueDateInput = Console.ReadLine();
                        isDueDateDate = DateTime.TryParse(dueDateInput, out dueDate);
                    }
                }

                // Error handling if date input is today or earlier
                while (dueDate <= DateTime.Now)
                {
                    DisplayErrorMessage("Ange ett datum längre fram än dagens datum: ");
                    dueDateInput = Console.ReadLine();
                    isDueDateDate = DateTime.TryParse(dueDateInput, out dueDate);
                }

                //Try-catch to add ToDo to list
                try
                {
                    ToDos.Add(new ToDo(title, project, dueDate, false));
                }
                catch (Exception)
                {
                    DisplayErrorMessage("Uppgiften registrerades inte.");
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow; //Add to ToDo list or continue adding ToDos
                Console.Write(" >> Välj (1) för att spara uppgiften till din ToDo-lista," +
                                " eller (2) för att fortsätta lägga till uppgifter: ");
                Console.ResetColor();
                string saveInput = Console.ReadLine();

                bool isSaveInputInt = int.TryParse(saveInput, out int saveInputInt);

                //Try-catch if input is not int type
                try
                {
                    saveInputInt = Convert.ToInt32(saveInput);
                }
                catch (FormatException)
                {
                    while (!isSaveInputInt)
                    {
                        DisplayErrorMessage("Välj (1) för att spara och (2) för att fortsätta lägga till uppgifter: ");
                        saveInput = Console.ReadLine();
                        isSaveInputInt = int.TryParse(saveInput, out saveInputInt);
                    }
                }

                //Error handling if input is not 4 or 2
                while (saveInputInt < 1 && saveInputInt > 2)
                {
                    DisplayErrorMessage("Du kan endast välja (1) eller (2): ");
                    saveInput = Console.ReadLine();
                    isSaveInputInt = int.TryParse(saveInput, out saveInputInt);
                }

                //Save ToDo/s to file
                if (saveInputInt == 1)
                {
                    try
                    {
                        Console.WriteLine();
                        fileHandle.SaveListToFile(filePath, this);
                        DisplaySuccessMessage("Uppgiften/uppgifterna sparades till ToDo-listan!");
                        Console.WriteLine();
                    }
                    catch (Exception)
                    {
                        DisplayErrorMessage("Uppgiften kunde inte sparas till ToDo-listan.");
                    }
                    finally
                    {
                        ChooseOption();
                    }
                }

                //Continue adding ToDos
                else if (saveInputInt == 2)
                {
                    AddToDo();
                }
            }
        }

        //Edit or delete ToDo
        public void EditToDo()
        {
            Console.WriteLine();
            ShowToDoAndId();

            //ToDo Id input
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" >> Vilken uppgift vill du uppdatera? Ange Id: ");
            Console.ResetColor();
            string idInput = Console.ReadLine();

            bool isIdInputInt = int.TryParse(idInput, out int idToEdit);

            //Try-catch if input is not int type
            try
            {
                idToEdit = Convert.ToInt32(idInput);

            }
            catch (FormatException)
            {
                while (!isIdInputInt) //Error message if input is not an int
                {
                    DisplayErrorMessage("Ange Id (siffra) på den uppgift du vill uppdatera: ");
                    idInput = Console.ReadLine();
                    isIdInputInt = int.TryParse(idInput, out idToEdit);
                }
            }

            //Error handling if Id doesn't exist in list
            int listLength = ToDos.Count();

            while (listLength < idToEdit)
            {
                DisplayErrorMessage("Det finns inte någon uppgift med det Id't. Ange ett annat Id: ");
                idInput = Console.ReadLine();
                isIdInputInt = int.TryParse(idInput, out idToEdit);
            }


            ToDo toDoToEdit = ToDos[idToEdit - 1]; //The ToDo to edit

            //Input for what field to update/delete
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" >> Uppdatera - ");
            Console.WriteLine(" (1) Uppgift");
            Console.WriteLine(" (2) Projekt");
            Console.WriteLine(" (3) Deadline");
            Console.WriteLine(" (4) Om uppgiften är klar eller inte. Uppdatera med JA (klar) och NEJ (inte klar).");
            Console.WriteLine(" (5) Radera uppgiften.");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Gör ditt val: ");

            string propertyInput = Console.ReadLine();

            bool isPropertyInputInt = int.TryParse(propertyInput, out int propertyInputInt);

            //Try-catch if input is not int type
            try
            {
                propertyInputInt = Convert.ToInt32(propertyInput);
            }
            catch (FormatException)
            {
                while (!isPropertyInputInt) //Error message if input is not an int
                {
                    DisplayErrorMessage("Ange (1), (2), (3), (4) eller (5) beroende på vad du vill göra: ");
                    propertyInput = Console.ReadLine();
                    isPropertyInputInt = int.TryParse(propertyInput, out propertyInputInt);
                }
            }

            if (isPropertyInputInt)
            {
                while (propertyInputInt < 1 || propertyInputInt > 5)
                {
                    DisplayErrorMessage("Ange (1), (2), (3), (4) eller (5) beroende på vad du vill göra: ");
                    propertyInput = Console.ReadLine();
                    isPropertyInputInt = int.TryParse(propertyInput, out propertyInputInt);
                }

                if (propertyInputInt == 5) //Remove ToDo
                {
                    ToDos.Remove(toDoToEdit);
                    Console.WriteLine();
                    DisplaySuccessMessage("Uppgiften raderades!");
                    Console.WriteLine();
                    fileHandle.SaveListToFile(filePath, this); //Save to file
                }
                else //Update ToDo
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(" >> Uppdatera din uppgift: ");
                    Console.ResetColor();
                    string newValue = Console.ReadLine();

                    bool isNewValueInputEmpty = string.IsNullOrWhiteSpace(newValue);

                    while (isNewValueInputEmpty) //Error message if input is empty
                    {
                        DisplayErrorMessage("Fyll i något för att uppdatera din uppgift: ");
                        newValue = Console.ReadLine();
                        isNewValueInputEmpty = string.IsNullOrWhiteSpace(newValue);
                    }

                    switch (propertyInputInt)
                    {
                        case 1:
                            toDoToEdit.Title = newValue;
                            break;
                        case 2:
                            toDoToEdit.Project = newValue;
                            break;
                        case 3:
                            DateTime newValueDate;
                            bool isNewValueDate = DateTime.TryParse(newValue, out newValueDate);

                            while (!isNewValueDate)
                            {
                                DisplayErrorMessage("Ange ett giltigt datum: ");
                                newValue = Console.ReadLine();
                                isNewValueDate = DateTime.TryParse(newValue, out newValueDate);
                            }

                            if (isNewValueDate)
                            {
                                while (newValueDate <= DateTime.Now) // Error handling if date input is today or earlier
                                {
                                    DisplayErrorMessage("Ange ett datum längre fram än dagens datum: ");
                                    newValue = Console.ReadLine();
                                    isNewValueDate = DateTime.TryParse(newValue, out newValueDate);
                                }

                                toDoToEdit.DueDate = newValueDate;
                            }
                            break;
                        case 4:
                            while (newValue.ToUpper() != "JA" && newValue.ToUpper() != "NEJ")
                            {   // Error handling if input is not Yes or No
                                DisplayErrorMessage("Ange JA (klar) eller NEJ (inte klar): ");
                                newValue = Console.ReadLine();
                            }

                            if (newValue.ToUpper() == "JA")
                            {
                                toDoToEdit.Done = true;
                            }
                            else if (newValue.ToUpper() == "NEJ")
                            {
                                toDoToEdit.Done = false;
                            }
                            break;
                        default:
                            DisplayErrorMessage("Ogiltig input.");
                            break;
                    }

                    //Display "update success" message along with the updated ToDo
                    Console.WriteLine();
                    DisplaySuccessMessage("Din uppgift uppdaterades!");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Uppgift: {toDoToEdit.Title}");
                    Console.WriteLine($"Projekt: {toDoToEdit.Project}");
                    Console.WriteLine($"Deadline: {toDoToEdit.DueDate.ToString("yy/MM/dd")}");
                    if (toDoToEdit.Done)
                    {
                        Console.WriteLine("Klart: JA");
                    }
                    else if (!toDoToEdit.Done)
                    {
                        Console.WriteLine("Klart: NEJ");
                    }
                    fileHandle.SaveListToFile(filePath, this); //Save to file
                }
            }
        }

        //Print list
        public void PrintList()
        {
            fileHandle.LoadDataFromFile(this);

            //Get input if the list should be sorted by project or due date
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" >> Visa lista sorterad på (1) deadline, (2) projekt eller (3) inte klar/klar: ");
            Console.ResetColor();
            string showListInput = Console.ReadLine();

            bool isShowListInputInt = int.TryParse(showListInput, out int showListInputInt);

            //Error handling for wrong type input
            while (!isShowListInputInt) //If not an int
            {
                DisplayErrorMessage("Ange en siffra för att se en sorterad lista - " +
                                    "(1) för deadline (2) för projekt och (3) för inte klar/klar: ");
                showListInput = Console.ReadLine();
                isShowListInputInt = int.TryParse(showListInput, out showListInputInt);
            }

            if (isShowListInputInt)
            {
                while (showListInputInt < 1 || showListInputInt > 3) //If input is not 1, 2 or 3
                {
                    DisplayErrorMessage("Välj mellan (1) deadline, (2) projekt eller (3) inte klar/klar: ");
                    showListInput = Console.ReadLine();
                    isShowListInputInt = int.TryParse(showListInput, out showListInputInt);
                }

                Console.WriteLine();
                Console.WriteLine("--------------------------------");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" >>> DIN TO-DO-LISTA <<<");
                Console.ResetColor();
                Console.WriteLine();

                if (showListInput == "1")
                {
                    ToDos = ToDos.OrderBy(t => t.DueDate).ToList(); //Sort bye due date
                }
                else if (showListInput == "2")
                {
                    ToDos = ToDos.OrderBy(t => t.Project).ToList(); //Sort by project
                }
                else if (showListInput == "3")
                {
                    ToDos = ToDos.OrderBy(t => t.Done).ToList(); //Sort by done/not done
                }
                ShowToDoAndId();
            }
        }
    }
}