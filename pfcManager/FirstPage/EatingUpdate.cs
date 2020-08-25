using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using pfcManager.Model;
using System.Runtime.CompilerServices;

namespace pfcManager.FirstPage
{
    /// <summary>
    /// Класс еды для обновления 
    /// </summary>
    class EatingUpdate :
        Eating, INotifyPropertyChanged
    {
        public EatingUpdate()
        {
            Id = -1;
        }

        public long IdUpd
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
                OnPropertyChanged("IdUdp");
            }
        }

        public EatingUpdate(Eating eating)
        {
            Id = eating.Id;
            Weight = eating.Weight;
            Idfood = eating.Idfood;
            Idusers = eating.Idusers;
            Datatime = eating.Datatime;
        }

        /// <summary>
        /// Событие, вызываемое при обновлении
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод, через который происходит указание, что элемнет изменил значение
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
