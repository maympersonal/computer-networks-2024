using System; // Importa el espacio de nombres System
using System.Windows.Forms; // Importa el espacio de nombres System.Windows.Forms

namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{
    public class Viewer
    {   TextBox _status;
        public Viewer(TextBox status)
        {
            _status = status;
        }
        // Método para mostrar un mensaje en la consola
        public void ShowMessage(string message)
        {
            this._status.AppendText(message);
        }
    }
}