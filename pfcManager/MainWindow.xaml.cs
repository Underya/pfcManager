using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

//dotnet ef dbcontext scaffold "Host=localhost;Database=pfcManager;Username=postgress;Password=1" Npgsql.EntityFrameworkCore.PostgreSQL
//Scaffold-DbContext "Host=localhost;Database=pfcManager;Username=postgress;Password=1" Npgsql.EntityFrameworkCore.PostgreSQL