using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using TESTtodo;


ToDoList toDoList = new ToDoList();
//ToDo todo = new ToDo();
//List<ToDo> list = toDoList.ToDos;
FileHandle fileHandle = new FileHandle();
int totalToDos = toDoList.ToDos.Count();



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

    bool isInputInt = int.TryParse(userInput, out int input);

    try //Try-catch if input is not int type
    {
        input = Convert.ToInt32(userInput);
    }
    catch (FormatException)
    {
        while (!isInputInt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Gör ditt val genom att välja (1), (2), (3) eller (4): ");
            Console.ResetColor();
            userInput = Console.ReadLine();
            isInputInt = int.TryParse(userInput, out input);
        }
    }

    while (input != 1 && input != 2 && input != 3 && input != 4)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Du kan endast välja (1), (2), (3) eller (4): ");
        Console.ResetColor();
        userInput = Console.ReadLine();
        isInputInt = int.TryParse(userInput, out input);
    }

    switch (input)
    {
        case 1:
            break;
        case 2:
            toDoList.AddToDo(); // Receive task input and add to list
            fileHandle.SaveListToFile(fileHandle.filePath);//Add to text file
            break;
        case 3:
            break;
        case 4:
            break;
    }

    //Display tasks, read from file
    fileHandle.LoadDataFromFile();
    toDoList.PrintList();

    totalToDos = toDoList.ToDos.Count(); //Count nr of tasks
    Console.WriteLine($"Antal uppgifter: {totalToDos}."); //Show nr of tasks

    Console.ReadLine();
}