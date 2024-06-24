using FluentValidation.Results;
using System.Text;

namespace PasswordBox.MVVM.Model
{
    internal class ErrorDisplay
    {
        internal static void ShowValidationErrorMessage(List<ValidationFailure> failures)
        {
            StringBuilder errorMsg = new();

            foreach (var error in failures)
            {
                errorMsg.Append(error.ErrorMessage + "\n");
            }

            Messagebox.Show(errorMsg.ToString());
        }
    }
}
