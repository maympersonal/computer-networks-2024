using System; // Importa el espacio de nombres System
using System.Windows.Forms; // Importa el espacio de nombres System.Windows.Forms

namespace SmtpClientApp
{
    public partial class LoginForm : Form // Definición de la clase LoginForm que hereda de Form
    {   private SmtpConfigReader reader = new SmtpConfigReader("config.txt");
        public LoginForm() // Constructor de la clase LoginForm
        {
            InitializeComponent(); // Inicializa los componentes del formulario

            #pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
            this.Click += LoginForm_Click; // Asigna el evento Click del formulario al método LoginForm_Click
            #pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        }

        private void btnOk_Click(object sender, EventArgs e) // Método para manejar el clic en el botón "OK"
        {
            string username = txtUsername.Text; // Obtiene el texto del TextBox txtUsername
            string password = txtPassword.Text; // Obtiene el texto del TextBox txtPassword

           
            string txtServerIP = reader.ServerIP;
            string txtPort = reader.Port;
            string txtFrom  = reader.From;

            ClientSMTP smtpClient = new ClientSMTP( // Crea una instancia de ClientSMTP
                txtServerIP, // Pasa la dirección IP del servidor SMTP
                int.Parse(txtPort), // Pasa el puerto del servidor SMTP
                txtUsername.Text, // Pasa el nombre de usuario
                txtPassword.Text, // Pasa la contraseña
                txtFrom, null // Pasa el remitente y el mensaje
            );

            #pragma warning disable CS8604 // Possible null reference argument.
            int resultCode = smtpClient.CheckUser();
            #pragma warning restore CS8604 // Possible null reference argument.

           // Verificar las credenciales (aquí deberías tener tu propia lógica de autenticación)
            if (resultCode == 0)
            {
                MainForm mainForm = new MainForm(username, password); // Crea una instancia de la clase MainForm
                mainForm.Show(); // Muestra el formulario MainForm
                this.Hide(); // Oculta el formulario actual
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un MessageBox con un mensaje de error
            }
        }

        private void btnExit_Click(object sender, EventArgs e) // Método para manejar el clic en el botón "Salir"
        {
            // Cerrar MainForm
            this.Close(); // Cierra el formulario actual
        }

        private void LoginForm_Click(object sender, EventArgs e) // Método para manejar el clic en el formulario
        {
            // Obtenemos las coordenadas del clic relativas al formulario
            Point relativePoint = this.PointToClient(Cursor.Position);

            // Comprobamos si las coordenadas están dentro de algún control
            bool insideControl = false;
            foreach (Control control in this.Controls)
            {
                if (control.Bounds.Contains(relativePoint))
                {
                    insideControl = true;
                    break;
                }
            }

            // Si no está dentro de ningún control, mostramos el MessageBox
            if (!insideControl)
            {
                            
                txtUsername.Text = reader.UserName;
                txtPassword.Text = reader.Password;

            }
        }
    }
}
