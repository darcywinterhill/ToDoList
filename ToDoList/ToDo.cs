namespace TESTtodo
{
    internal class ToDo
    {
        public ToDo(string title, string project, DateTime dueDate, bool done)
        {
            Title = title;
            Project = project;
            DueDate = dueDate;
            Done = done;
        }

        public List<ToDo> Tasks { get; set; }
        public ToDo()
        {
            Tasks = new List<ToDo>();
        }

        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Project { get; set; }
        public bool Done { get; set; }


        //string Print()
        //{
        //    return todo.Title + todo.DueDate.ToString("yyyy/MM/dd") + todo.Project;
        //}

    }
}