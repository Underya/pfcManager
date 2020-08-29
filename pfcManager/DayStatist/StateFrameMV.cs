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
        /// Получение коллекции информации о весе
        /// </summary>
        public ObservableCollection<Weight> Weights
        {
            get
            {
                List<Weight> weightsList = null;
                using(ModelContext mc = new ModelContext())
                {
                    weightsList = mc.Weight.Where(o => o.Idusers == PanelManager.CurrentUserId).OrderByDescending(o => o.Datatime).ToList();
                }
                weightsCollection = new ObservableCollection<Weight>(weightsList);
                return weightsCollection;
            }
            set
            {
                weightsCollection = value;
            }
        }
    }
}
