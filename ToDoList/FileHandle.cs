namespace ToDoListProject
{
    internal class FileHandle
    {
        public string filePath = "textList.txt";

        public FileHandle(string filePath)
        {
            this.filePath = filePath;
        }

        //Save list to file
        public void SaveListToFile(string filePath, ToDoList toDoList)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (ToDo toDo in toDoList.ToDos)
                {
                    writer.WriteLine(   $"{toDo.Title}," +
                                        $"{toDo.Project}," +
                                        $"{toDo.DueDate.ToString("yy/MM/dd")}," +
                                        $"{toDo.Done}");
                }
            }
        }

        //Load list from file
        public void LoadDataFromFile(ToDoList toDoList)
        {
            if (File.Exists(filePath))
            {
                toDoList.ToDos.Clear();
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
                        toDoList.ToDos.Add(toDo);
                    }
                }
            }
        }
    }
}
