using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace EstimatedBudget.Helpers
{
    /// <summary>
    /// REPRESENTE LES MODELES DES MESSAGEBOX
    /// </summary>
    public static class DialogService
    {
        public static async Task<MessageDialogResult> ShowMessage(string message1, string message2 = "", MessageDialogStyle dialogStyle = MessageDialogStyle.Affirmative)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            metroWindow.MetroDialogOptions.AffirmativeButtonText = "Valider";
            metroWindow.MetroDialogOptions.NegativeButtonText = "Annuler";
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Inverted;
            return await metroWindow.ShowMessageAsync(message1, message2, dialogStyle,
                metroWindow.MetroDialogOptions);
        }

        public static async Task<MessageDialogResult> ShowMessage2(string title, string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            return await metroWindow.ShowMessageAsync(title, message, dialogStyle,
                metroWindow.MetroDialogOptions);
        }

        //NULL = CANCEL 
        public static async Task<string> ShowInput(string Tilte, string Message, MessageDialogStyle dialogStyle, int LimitMaxLength, string DefautText = "")
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            metroWindow.MetroDialogOptions.NegativeButtonText = "Annuler";
            metroWindow.MetroDialogOptions.AffirmativeButtonText = "Valider";
            metroWindow.MetroDialogOptions.DefaultText = DefautText;

            var result = await metroWindow.ShowInputAsync(Tilte, Message, metroWindow.MetroDialogOptions);

            if (result.Length > LimitMaxLength)
            {
                await ShowMessage2("Nombre de caratcères maximum atteint.", "Vous avez dépassé le nombre de caractères maximum dans la zone de saise. \nLe maximum est de " + LimitMaxLength + " caractères.", MessageDialogStyle.Affirmative);
                return null;
            }

            return result;
        }

        public static async Task<string> ShowInputAnio(string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            //return await metroWindow.ShowInputAsync("Agregar nueva actividad...", "Introduzca la descripción de la actividad");
            var result = await metroWindow.ShowInputAsync("Introduce el año de operación", "Introduzca el año del POA");
            //LoginDialogData result = await metroWindow.ShowLoginAsync("Introduce la nueva actividad", "Introduce descripción");
            int resultInt;
            if (result != null && int.TryParse(result, out resultInt))
            {
                if (result.Length > 4 || resultInt > 3000 || resultInt < 1960)
                {
                    await metroWindow.ShowMessageAsync("El valor introducido no es un año valido.", "Introdujo: " + result);
                    return string.Empty;
                }
                else
                {
                    await metroWindow.ShowMessageAsync("Has introducido", "Introdujo: " + result);
                    return result;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(result))
                {
                    await metroWindow.ShowMessageAsync(
                    "El valor introducido no es válido.", "Introdujo: " + result);
                }
                return string.Empty;
            }


        }
        public static async Task<string> ShowEditActividad(string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            //return await metroWindow.ShowInputAsync("Agregar nueva actividad...", "Introduzca la descripción de la actividad");
            MetroDialogSettings settings = new MetroDialogSettings() { DefaultText = message };
            var result = await metroWindow.ShowInputAsync("Editar actividad...",
                "Introduzca la descripción de la actividad", settings);
            //LoginDialogData result = await metroWindow.ShowLoginAsync("Introduce la nueva actividad", "Introduce descripción");

            if (result == null)
                return message;
            else
            {
                await metroWindow.ShowMessageAsync("Has introducido", result);
                return result;
            }



        }

    }
}
