using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using pfcManager.Model;

namespace pfcManager.DayStatist
{

    /// <summary>
    /// View-Model для дневной статистики
    /// </summary>
    class StateFrameMV : 
        PropertyChanges
    {
        /// <summary>
        /// Коллекция информации о весе
        /// </summary>
        ObservableCollection<Weight> weightsCollection = null;

        /// <summary>
        /// Потерянный вес за указанный промежуток
        /// </summary>
        public float LostWeigt 
        { 
            get { return -4.0F; } 
            set { }
        }

        /// <summary>
        /// Получение коллекции информации о весе
        /// </summary>
        public ObservableCollection<Weight> Weights
        {
            get
            {
                return new ObservableCollection<Weight>(WeightStatic.GetWeights(PanelManager.CurrentUserId, 30));
            }
            set
            {
                weightsCollection = value;
            }
        }

        /// <summary>
        /// Статистика калорий за последни 30 дней
        /// </summary>
        public ObservableCollection<SumKkal> Kkals
        {
            get
            {
                return new ObservableCollection<SumKkal>(SumKkal.CallBackDay(30, PanelManager.CurrentUserId));
            }
            set
            {

            }
        }
    }
}
