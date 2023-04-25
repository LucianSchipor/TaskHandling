using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Serialization;
using TaskHandling.Models;
using TaskHandling.ViewModels;
using TaskHandling.Views;

namespace TaskHandling.Services
{
    public class TDLService
    {
        private TDL tdl;
        public string currentFileName;
        public TDLService(TDL tdl) : this()
        {
            this.tdl = tdl;
        }
        public TDLService()
        {
            currentFileName = "Unknown";
        }
        public void AddTask(Task task)
        {
            tdl.TasksCollection.Add(task);
        }
        public void AddTDL(TDL newTDL)
        {
            tdl.TdlCollection.Add(newTDL);
            tdl.NotifyPropertyChanged(nameof(tdl.TdlCollection));
        }

        public void RemoveTask(Task task)
        {
            tdl.TasksCollection.Remove(task);
        }

        public void RemoveTDL(TDL tDL)
        {
            tdl.TdlCollection.Remove(tDL);
        }

        private void SwapTDLs(TDL first, TDL second)
        {
            var aux = new TDL();
            aux = first;
            first = second;
            second = aux;
        }

        public void MoveTDL(string mode)
        {
            if (mode == "up")
            {
                var index = this.tdl.parent.TdlCollection.IndexOf(this.tdl);
                if (index != 0)
                    SwapTDLs(this.tdl.parent.TdlCollection[index], this.tdl.parent.TdlCollection[index - 1]);
                else
                    MessageBox.Show("This TDL is the first.");
            }
        }

        public void SaveToFile(object param)
        {

            var fileName = currentFileName;
            XmlSerializer serializer = new XmlSerializer(typeof(TDL));

            if (currentFileName != "Unknown")
            {

                using (FileStream stream = new FileStream(currentFileName, FileMode.Create))
                {
                    serializer.Serialize(stream, tdl);
                }
            }
            else
            {
                MessageBox.Show("Nu ai selectat nicio baza de date.");
            }
        }
    }
}
