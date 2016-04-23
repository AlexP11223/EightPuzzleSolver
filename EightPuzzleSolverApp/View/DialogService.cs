using System.Windows;

namespace EightPuzzleSolverApp.View
{
    public class DialogService : IDialogService
    {
        public void ShowError(string text, string title = "")
        {
            MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ShowConfirmation(string text, string title = "")
        {
            return MessageBox.Show(text, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
    }
}
