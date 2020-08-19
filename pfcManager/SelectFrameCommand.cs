﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace pfcManager
{
    /// <summary>
    /// Команды выбора текушего фрейма в главном меню
    /// </summary>
    class SelectFrameCommand : ICommand
    {
        public SelectFrameCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
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
