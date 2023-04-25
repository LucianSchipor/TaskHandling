using System.Collections.ObjectModel;
using System.ComponentModel;
using TaskHandling.Services;

namespace TaskHandling.Models
{
    public class TDL : INotifyPropertyChanged
    {
        public TDL parent;
        public event PropertyChangedEventHandler PropertyChanged;
        public TDLService tdlservice;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public TDL()
        {
            this._name = "New TDL";
            this._image = "../Assets/TDLIcons/sport";
            _tdlCollection = new ObservableCollection<TDL>();
            _tasksColletion = new ObservableCollection<Task>();
            this.parent = null;
            tdlservice = new TDLService(this);
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (this._name != value)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                if (this._image != value)
                {
                    _image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }


        private ObservableCollection<TDL> _tdlCollection;
        public ObservableCollection<TDL> TdlCollection
        {
            get
            {
                return _tdlCollection;
            }
            set
            {
                if (this._tdlCollection != value)
                {

                    _tdlCollection = value;
                    NotifyPropertyChanged("TdlCollection");
                }
            }
        }

        private ObservableCollection<Task> _tasksColletion;
        public ObservableCollection<Task> TasksCollection
        {
            get
            {
                return _tasksColletion;
            }
            set
            {
                if (value != _tasksColletion)
                {

                    _tasksColletion = value;
                    NotifyPropertyChanged("TasksCollection");
                }
            }
        }
    }
}
