using System; // Importa el espacio de nombres System
using System.Windows.Forms; // Importa el espacio de nombres System.Windows.Forms

namespace SmtpClientApp
{
    public partial class LoginForm : Form // Definición de la clase LoginForm que hereda de Form
    {
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

            // Verificar las credenciales (aquí deberías tener tu propia lógica de autenticación)
            if (username == "mi_correo@example.com" && password == "mi_contraseña")
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
                // Inicializar los valores predeterminados de los TextBoxes
                txtUsername.Text = "mi_correo@example.com";
                txtPassword.Text = "mi_contraseña";
            }
        }
    }
}
