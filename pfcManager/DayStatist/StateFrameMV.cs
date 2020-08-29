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
        public double LostWeigt 
        { 
            get 
            {
                //Получение всех весов из статисики
                ObservableCollection<Weight> weights = Weights;

                //Если нет значений, то ничего и не потеряно
                if (weights.Count == 0) 
                    return 0;

                //Ближайший день
                Weight CurrDay = weights[0];
                //Самый удалённый
                Weight LastDay = weights[weights.Count - 1];

                //Возвращение разницы
                return Math.Round((double)(LastDay.Value - CurrDay.Value), 2);
            } 
            set { }
        }

        /// <summary>
        /// Среднее число ккалорий
        /// </summary>
        public double MeanKkal 
        {
            get 
            {
                //Получение всей суммы калорий
                ObservableCollection<SumKkal> kkals = Kkals;

                double summ = 0;
                int count = 0;

                foreach(SumKkal sumKkal in kkals)
                {
                    summ += sumKkal.Summ;
                    count++;
                }

                return Math.Round( summ / count, 2); 
            }
            set { }
        }

        /// <summary>
        /// Получение коллекции информации о весе
        /// </summary>
        public ObservableCollection<Weight> Weights
        {
            get
            {
                if (weightsCollection != null)
                    return weightsCollection;

                weightsCollection = new ObservableCollection<Weight>(WeightStatic.GetWeights(PanelManager.CurrentUserId, 30));

                return weightsCollection;
            }
            set
            {
                weightsCollection = value;
            }
        }

        /// <summary>
        /// Переменная для хранения суммы калорий
        /// </summary>
        ObservableCollection<SumKkal> kkals = null;

        /// <summary>
        /// Статистика калорий за последни 30 дней
        /// </summary>
        public ObservableCollection<SumKkal> Kkals
        {
            get
            {
                if (kkals != null)
                    return kkals;
                kkals = new ObservableCollection<SumKkal>(SumKkal.CallBackDay(30, PanelManager.CurrentUserId));

                return kkals;
            }
            set
            {

            }
        }
    }
}
