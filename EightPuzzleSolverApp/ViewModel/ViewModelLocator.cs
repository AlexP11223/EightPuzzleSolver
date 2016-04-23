using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using EightPuzzleSolverApp.Model;
using EightPuzzleSolverApp.View;

namespace EightPuzzleSolverApp.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IPuzzleSolverService, PuzzleSolverService>();
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<MainViewModel>();
        }
        
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
        }
    }
}