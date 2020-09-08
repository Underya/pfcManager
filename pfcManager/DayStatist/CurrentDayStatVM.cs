using System;
using System.Collections.Generic;
using System.Text;

namespace pfcManager.DayStatist
{
    /// <summary>
    /// View-Model для конкретного дня
    /// </summary>
    class CurrentDayStatVM : FirstPage.FirstPageVM
    {

        public CurrentDayStatVM() : base()
        {

        }

        public CurrentDayStatVM(DateTime day) : base(day)
        {

        }
            

        /// <summary>
        /// Кнопка выхода в историю
        /// </summary>
        public AcceptCommand ExitToHisoric
        {
            get
            {
                return new AcceptCommand(obj =>
                {
                    MainMenuVM.SetHistoryMenu();
                });
            }
            set
            {

            }
        }

    }
}
