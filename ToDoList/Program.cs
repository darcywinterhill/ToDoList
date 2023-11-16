using TESTtodo;


ToDoList toDoList = new ToDoList();
ToDo todo = new ToDo();
List<ToDo> list = toDoList.ToDos;
int totalToDos = list.Count(); //Count nr of tasks


void ShowOptions()
{
    Console.WriteLine("Välkommen till din ToDo-lista!");
    Console.WriteLine($"Du har {totalToDos} uppgifter att göra och du har bockat av y stycken.");
    Console.WriteLine();

    Console.WriteLine("Gör ditt val -->");
    Console.WriteLine(" (1) Visa ToDo-listan (sorterad på deadline eller projekt).");
    Console.WriteLine(" (2) Lägg till en uppgift till listan.");
    Console.WriteLine(" (3) Redigera en uppgift (uppdatera, markera som klar, ta bort).");
    Console.WriteLine(" (4) Spara och avsluta.");
}



while (true)
{
    ShowOptions();

    Console.WriteLine();
    Console.Write("Mitt val: ");
    string userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":

            break;

        case "2":
            toDoList.AddToDo(); // Receive task input and add to list
            toDoList.SaveListToFile(list, "ToDoList.txt"); //Add to text file
            break;
        case "3":
            break;
        case "4":
            break;

    }

    //Display tasks, read from file
    toDoList.ShowList();

    totalToDos = list.Count(); //Count nr of tasks
    Console.WriteLine($"Antal uppgifter: {totalToDos}."); //Show nr of tasks

    Console.ReadLine();
}