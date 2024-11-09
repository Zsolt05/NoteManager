namespace NoteManager.Client.Components.Toast
{
    public class ToastService
    {
        public event Action<string, string> OnShow;

        public void ShowError(string message)
        {
            OnShow?.Invoke(message, "error");
        }

        public void ShowSuccess(string message)
        {
            OnShow?.Invoke(message, "success");
        }
    }

}
