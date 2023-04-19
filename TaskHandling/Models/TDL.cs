using System.Collections.ObjectModel;


namespace TaskHandling.Models
{
    class TDL
    {
        public TDL()
        {
            this.Name = "New TDL";
            this.image = "../Assets/TDLIcons/sport";
            tdlCollection = new ObservableCollection<TDL>();
            tasksColletion = new ObservableCollection<Task>();
        }

        public string Name { get; set; }

        public string image { get; set; }

        public ObservableCollection<TDL> tdlCollection { get; set; }
        public ObservableCollection<Task> tasksColletion { get; set; }

    }
}
