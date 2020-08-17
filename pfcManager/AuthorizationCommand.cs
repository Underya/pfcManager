using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace pfcManager
{
    /// <summary>
    /// Команда авторизации пользоваотеля
    /// </summary>
    class AuthorizationCommand : ICommand
    {
        /// <summary>
        /// Действие, выполняемое командой
        /// </summary>
        private Action<object> execute;

        /// <summary>
        /// Проверка, может ли команда быть выполненой
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Добавление события, вызываемого при изменение возможности выполнения
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Создание комманды с указанными функциями
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        public AuthorizationCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
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
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
