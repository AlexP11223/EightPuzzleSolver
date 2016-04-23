namespace EightPuzzleSolverApp.View
{
    public interface IDialogService
    {
        void ShowError(string text, string title = "");

        bool ShowConfirmation(string text, string title = "");
    }
}
