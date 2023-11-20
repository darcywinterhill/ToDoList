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

        public ToDo() { }

        public string Title { get; set; }
        public string Project { get; set; }
        public DateTime DueDate { get; set; }
        public bool Done { get; set; }

    }
}