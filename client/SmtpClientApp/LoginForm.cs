using System;
using System.Windows.Forms;

namespace SmtpClientApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // Inicializar los valores predeterminados de los TextBoxes
            txtUsername.Text = "mi_correo@example.com";
            txtPassword.Text = "mi_contraseña";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Verificar las credenciales (aquí deberías tener tu propia lógica de autenticación)
            if (username == "mi_correo@example.com" && password == "mi_contraseña")
            {
                MainForm mainForm = new MainForm(username,password);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrectos", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Cerrar MainForm
            this.Close();
        }
    }
}
