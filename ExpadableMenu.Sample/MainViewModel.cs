using ExpadableMenu.Sample.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ExpadableMenu.Sample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties

        public ObservableCollection<ExpandableMenuItem> ExpandableMenuItems
        {
            get { return expandableMenuItems = expandableMenuItems ?? new ObservableCollection<ExpandableMenuItem>(); }
        }
        private ObservableCollection<ExpandableMenuItem> expandableMenuItems;

        public ExpandableMenuItem SelectedExpandableMenuItem
        {
            get { return selectedExpandableMenuItem; }
            set
            {
                if (selectedExpandableMenuItem != value)
                {
                    selectedExpandableMenuItem = value;
                    RaisePropertyChanged("SelectedExpandableMenuItem");
                }
            }
        }
        private ExpandableMenuItem selectedExpandableMenuItem;

        #endregion


        public MainViewModel()
        {
            ExpandableMenuItems.Add(new ExpandableMenuItem { Header = "Mail", Content = "Main content" });
            ExpandableMenuItems.Add(new ExpandableMenuItem { Header = "Calendar", Content = "Calendar content" });
            ExpandableMenuItems.Add(new ExpandableMenuItem { Header = "Contacts", Content = "Contacts content" });
            ExpandableMenuItems.Add(new ExpandableMenuItem { Header = "Schedule", Content = "Schedule content" });
        }












        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
