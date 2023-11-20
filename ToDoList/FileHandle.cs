using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TESTtodo;

namespace TESTtodo
{
    internal class FileHandle
    {
        public FileHandle() { }

        ToDoList list = new ToDoList();

        public string filePath = ".\\textList.txt"; //File path

        //Save list to file Method
        public void SaveListToFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    foreach (ToDo toDo in list.ToDos)
                    {
                        writer.WriteLine($"{toDo.Title},{toDo.Project},{toDo.DueDate.ToString("yy/MM/dd")},{toDo.Done}");
                    }
                }
            }
            //else
            //{
            //    using (StreamWriter writer = new StreamWriter(filePath, true))
            //    {
            //        foreach (ToDo toDo in list.ToDos)
            //        {
            //            writer.WriteLine($"{toDo.Title},{toDo.Project},{toDo.DueDate.ToString("yy/MM/dd")},{toDo.Done}");
            //        }
            //    }
            //}
        }

        //Load list from file Method
        public void LoadDataFromFile()
        {
            if (File.Exists(filePath))
            {
                //list.ToDos.Clear();
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
                        list.ToDos.Add(toDo);
                    }
                }
            }
        }

    }
}
