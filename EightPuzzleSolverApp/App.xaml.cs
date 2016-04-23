using System.Windows;
using GalaSoft.MvvmLight.Threading;

namespace EightPuzzleSolverApp
{
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}
