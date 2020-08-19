﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pfcManager
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPanel.xaml
    /// </summary>
    public partial class AuthorizationPanel : UserControl
    {
        public AuthorizationPanel()
        {
            InitializeComponent();
            //Устанока View-Model для объекта
            DataContext = new AuthorizationModel();
        }
    }
}