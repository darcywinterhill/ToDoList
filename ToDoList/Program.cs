using TESTtodo;

ToDoList toDoList = new ToDoList();

//Welcome message
Console.WriteLine("Välkommen till din ToDo-lista!");
Console.WriteLine($"Du har {CountToDos()} uppgifter att göra och du har bockat av {CountDoneToDos()} stycken.");

//Run program
toDoList.ChooseOption();

//Count ToDo's saved in list
int CountToDos()
{

    toDoList.LoadDataFromFile();
    return toDoList.ToDos.Count() - CountDoneToDos();
}

int CountDoneToDos()
{

    toDoList.LoadDataFromFile();
    return toDoList.ToDos.Where(t => t.Done == true).Count();
}

Console.ReadLine();
