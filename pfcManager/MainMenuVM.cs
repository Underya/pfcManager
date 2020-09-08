using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using pfcManager.Model;
using pfcManager.FirstPage;
using pfcManager.DayStatist;
using pfcManager.FoodList;

namespace pfcManager
{
    /// <summary>
    /// Model-View для главного меню приложения
    /// </summary>
    class MainMenuVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Указание на профиль текушего пользователя 
        /// </summary>
        UserSave userSave = null;

        /// <summary>
        /// Текущий выбранный элемент управления
        /// </summary>
        UserControl selectFrame = null;

        /// <summary>
        /// Текущий выбранный кадр
        /// </summary>
        public UserControl SelectFrame
        {
            get { return selectFrame; }
            set { selectFrame = value; OnPropertyChanged("SelectFrame"); }
        }

        /// <summary>
        /// Последнее активное меню пользователя
        /// </summary>
        public static MainMenuVM ActivMainMenu = null;

        /// <summary>
        /// Модель для управления панелью
        /// </summary>
        public MainMenuVM()
        {
            userSave = PanelManager.CurrentUser;
            //По умолчанию открывается главная страница
            selectFrame = new firstPage();
            //Сохранение ссылки
            ActivMainMenu = this;
        }

        /// <summary>
        /// Создание команды для выхода из профиля
        /// </summary>
        public ExitCommand exitCommand { get { return new ExitCommand(); } }

        /// <summary>
        /// Имя текущего пользователя
        /// </summary>
        public string UserName
        {
            get
            {
                return userSave.Lastname + " " + userSave.Firstname + " " + userSave.Midname;
            }
        }

        /// <summary>
        /// Команда для выбора первой страницы
        /// </summary>
        SelectFrameCommand selectFirstCommand = null;

        /// <summary>
        /// Выбор первой страницы
        /// </summary>
        public SelectFrameCommand  SelectFirstFrame
        { get 
            {
                return selectFirstCommand ??
                    (new SelectFrameCommand(SetFirstFrame));
            } 
        }

        /// <summary>
        /// Комманда для установления меню списка еды
        /// </summary>
        SelectFrameCommand selectedFoodList = null;

        /// <summary>
        /// Метод для получения комманда выбора еды
        /// </summary>
        public SelectFrameCommand SelectFoodList 
        {
            get
            {
                return selectedFoodList ??
                    (selectedFoodList = new SelectFrameCommand(SetFoodList));
            }
            set 
            {
                selectedFoodList = value;
            }
        }

        /// <summary>
        /// Установка первой страницы
        /// </summary>
        /// <param name="obj"></param>
        void SetFirstFrame(object obj)
        {
            //Если установлена, то можно ничего не делать
            if (SelectFrame is firstPage)
                return;
            //Установка страницы
            SelectFrame = new firstPage();
        }

        /// <summary>
        /// Установка страницы со списком еды
        /// </summary>
        /// <param name="obj"></param>
        void SetFoodList(object obj)
        {
            //Если текущий фрейм совпдает - то ничего не делать
            if (SelectFrame is FoodListFrame)
                return;
            SelectFrame = new FoodListFrame();
        }

        /// <summary>
        /// Команда для выбора страницы с текущим днём
        /// </summary>
        SelectFrameCommand selectDayFrame = null;

        /// <summary>
        /// Обработка комманды для выбора страницы с днём
        /// </summary>
        public SelectFrameCommand SelectDayFrame
        {
            get
            {
                return selectDayFrame ??
                    (new SelectFrameCommand(SetDayFrame));
            }
        }

        /// <summary>
        /// Установка страницы с инфой о дне
        /// </summary>
        /// <param name="obj"></param>
        void SetDayFrame(object obj)
        {
            //Если текущий фрейм совпдает - то ничего не делать
            if (SelectFrame is DayStateFrame)
                return;
            SelectFrame = new DayStateFrame();
        }

        /// <summary>
        /// Событие, вызываемое пр иобновлении
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

        /// <summary>
        /// Открытие фрейма со статистикой за указнный день
        /// </summary>
        /// <param name="day"></param>
        public static void SetCurrentDayStatistick(DateTime day)
        {
            ActivMainMenu.SelectFrame = new CurrentDayStatistick(day);
        }
    }
}
