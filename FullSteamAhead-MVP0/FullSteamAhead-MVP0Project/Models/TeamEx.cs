using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FullSteamAheadMVP0Project.Models
{
    public class TeamEx : Team, INotifyPropertyChanged
    {

        public new event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _ImageFilePath;
        public string ImageFilePath
        {
            get
            {
                return _ImageFilePath;
            }
            set
            {
                _ImageFilePath = value;
                NotifyPropertyChanged();
            }

        }
    }
}
