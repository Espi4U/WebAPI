using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FamilyFinance.Models
{
    public class Field : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _name = _name.TrimStart();
                OnPropertyChanged("Name");

            }
        }

        private bool _isNotValid;
        public bool IsNotValid
        {
            get => _isNotValid;
            set
            {
                _isNotValid = value;
                OnPropertyChanged("IsNotValid");
            }
        }

        private string _notValidMessageError;
        public string NotValidMessageError
        {
            get => _notValidMessageError;
            set
            {
                _notValidMessageError = value;
                OnPropertyChanged("NotValidMessageError");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
