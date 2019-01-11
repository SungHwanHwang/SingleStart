using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SingleProject
{
    public class UserInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string userId = string.Empty;
        private string password = string.Empty;
        private string userName = string.Empty;
        private string email = string.Empty;
        private string userBirth;
        private int userValue;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string UserId
        {
            get 
            {
                return userId;
            }
            set
            {
                this.userId = value;
                OnPropertyChanged("userId");                
            }
        }

        public string Passwrod
        {
            get
            {
                return password;
            }
            set
            {
                this.password = value;
                OnPropertyChanged("passwrod");
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                this.userName = value;
                OnPropertyChanged("userName");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged("email");
            }
        }

        public string UserBirth
        {
            get
            {
                return userBirth;
            }
            set
            {
                this.userBirth = value;
                OnPropertyChanged("userBirth");
            }
        }

        public int UserValue
        {
            get
            {
                return userValue;
            }
            set
            {
                this.userValue = value;
                OnPropertyChanged("userValue");
            }
        }


    }
}
