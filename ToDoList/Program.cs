using TESTtodo;

ToDoList toDoList = new ToDoList();

//Welcome message
Console.WriteLine("Välkommen till din ToDo-lista!");
Console.WriteLine($"Du har {countToDos()} uppgifter att göra och du har bockat av y stycken.");

//Run program
toDoList.ChooseOption();

//Count ToDo's saved in list
int countToDos()
{
    toDoList.LoadDataFromFile();
    return toDoList.ToDos.Count();
}

Console.ReadLine();
