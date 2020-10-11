using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GravityFormsAdapter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var res = MessageBox.Show("Exception occurred: " + e.Exception.Message + "\n" + e.Exception.StackTrace + "\n\nIf this persists please contact support. Attempt to continue?", "Exception occurred", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (res == MessageBoxResult.Yes)
            {
                e.Handled = true;
                return;
            }

        }
    }
}
