using System; // Importa el espacio de nombres System, que contiene clases y tipos fundamentales del sistema
using System.Windows.Forms; // Importa el espacio de nombres System.Windows.Forms, que contiene clases para la creación de aplicaciones de Windows Forms

namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{
    static class Program // Define una clase estática Program
    {
        [STAThread] // Atributo que indica que el modelo de subprocesos para la aplicación es de un solo subproceso de apartamento
        static void Main() // Método de entrada principal del programa
        {
            Application.EnableVisualStyles(); // Habilita los estilos visuales de la aplicación
            Application.SetCompatibleTextRenderingDefault(false); // Configura el modo predeterminado de representación de texto compatible con versiones anteriores
            Application.Run(new LoginForm()); // Ejecuta la aplicación y muestra el formulario de inicio de sesión
        }
    }
}
