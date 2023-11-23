using ToDoListProject;

ToDoList toDoList = new ToDoList();
FileHandle fileHandle = new FileHandle("textList.txt");

//Welcome message
Console.WriteLine("Välkommen till din ToDo-lista!");
Console.WriteLine($"Du har {NotDoneToDos()} uppgifter att göra och du har bockat av {DoneToDos()} stycken.");

//Run program
toDoList.ChooseOption();

//Count ToDo's not done in list
int NotDoneToDos()
{
    fileHandle.LoadDataFromFile(toDoList);
    return toDoList.ToDos.Count() - DoneToDos();
}

//Count ToDo's done in list
int DoneToDos()
{
    fileHandle.LoadDataFromFile(toDoList);
    return toDoList.ToDos.Where(t => t.Done == true).Count();
}

Console.ReadLine();
