using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;


namespace pfcManager
{
    /// <summary>
    /// Комманда для выхода на меню авторизации
    /// </summary>
    class ExitCommand : ICommand
    {
        public ExitCommand(Func<object, bool> canExecute = null) 
        {
            this.execute = Exit;
        }

        /// <summary>
        /// Переход на страницу авторизации
        /// </summary>
        void Exit(object obj)
        {
            PanelManager.GoAuthotizatePanel(true);
        }

        /// <summary>
        /// Действие, выполняемое командой
        /// </summary>
        protected Action<object> execute;

        /// <summary>
        /// Проверка, может ли команда быть выполненой
        /// </summary>
        protected Func<object, bool> canExecute;

        /// <summary>
        /// Добавление события, вызываемого при изменение возможности выполнения
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Проверка, может ли быть команда выполнена
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary>
        /// Выполнение команды
        /// </summary>
        /// <param name="parameter"></param>
        public virtual void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
