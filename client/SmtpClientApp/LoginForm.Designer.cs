namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{
    partial class LoginForm // Define la clase LoginForm como parcial
    {
        private System.ComponentModel.IContainer components = null; // Declara un contenedor de componentes

        protected override void Dispose(bool disposing) // Define un método para liberar recursos no administrados
        {
            if (disposing && (components != null)) // Si disposing es verdadero y hay componentes
            {
                components.Dispose(); // Libera los recursos del contenedor de componentes
            }
            base.Dispose(disposing); // Llama al método base para realizar la limpieza adicional
        }

        private void InitializeComponent() // Define un método para inicializar los componentes del formulario
        {
            // Inicializa los controles del formulario
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout(); // Inicia el diseño del formulario
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(135, 50); // Establece la ubicación del cuadro de texto de nombre de usuario
            this.txtUsername.Name = "txtUsername"; // Asigna un nombre al cuadro de texto de nombre de usuario
            this.txtUsername.Size = new System.Drawing.Size(200, 20); // Establece el tamaño del cuadro de texto de nombre de usuario
            this.txtUsername.TabIndex = 0; // Establece el índice de tabulación del cuadro de texto de nombre de usuario
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(135, 90); // Establece la ubicación del cuadro de texto de contraseña
            this.txtPassword.Name = "txtPassword"; // Asigna un nombre al cuadro de texto de contraseña
            this.txtPassword.Size = new System.Drawing.Size(200, 20); // Establece el tamaño del cuadro de texto de contraseña
            this.txtPassword.TabIndex = 1; // Establece el índice de tabulación del cuadro de texto de contraseña
            this.txtPassword.UseSystemPasswordChar = true; // Oculta los caracteres de contraseña
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblUsername.Location = new System.Drawing.Point(50, 53); // Establece la ubicación de la etiqueta de nombre de usuario
            this.lblUsername.Name = "lblUsername"; // Asigna un nombre a la etiqueta de nombre de usuario
            this.lblUsername.Size = new System.Drawing.Size(55, 13); // Establece el tamaño de la etiqueta de nombre de usuario
            this.lblUsername.TabIndex = 2; // Establece el índice de tabulación de la etiqueta de nombre de usuario
            this.lblUsername.Text = "Username"; // Establece el texto de la etiqueta de nombre de usuario
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblPassword.Location = new System.Drawing.Point(50, 93); // Establece la ubicación de la etiqueta de contraseña
            this.lblPassword.Name = "lblPassword"; // Asigna un nombre a la etiqueta de contraseña
            this.lblPassword.Size = new System.Drawing.Size(53, 13); // Establece el tamaño de la etiqueta de contraseña
            this.lblPassword.TabIndex = 3; // Establece el índice de tabulación de la etiqueta de contraseña
            this.lblPassword.Text = "Password"; // Establece el texto de la etiqueta de contraseña
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(100, 150); // Establece la ubicación del botón "OK"
            this.btnOk.Name = "btnOk"; // Asigna un nombre al botón "OK"
            this.btnOk.Size = new System.Drawing.Size(75, 23); // Establece el tamaño del botón "OK"
            this.btnOk.TabIndex = 4; // Establece el índice de tabulación del botón "OK"
            this.btnOk.Text = "OK"; // Establece el texto del botón "OK"
            this.btnOk.UseVisualStyleBackColor = true; // Establece la apariencia del botón "OK" como predeterminada
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click); // Asigna un manejador de eventos al hacer clic en el botón "OK"
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(200, 150); // Establece la ubicación del botón "Exit"
            this.btnExit.Name = "btnExit"; // Asigna un nombre al botón "Exit"
            this.btnExit.Size = new System.Drawing.Size(75, 23); // Establece el tamaño del botón "Exit"
            this.btnExit.TabIndex = 18; // Establece el índice de tabulación del botón "Exit"
            this.btnExit.Text = "Exit"; // Establece el texto del botón "Exit"
            this.btnExit.UseVisualStyleBackColor = true; // Establece la apariencia del botón "Exit" como predeterminada
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click); // Asigna un manejador de eventos al hacer clic en el botón "Exit"
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // Establece el tamaño de la fuente
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; // Establece el modo de ajuste de la fuente
            this.ClientSize = new System.Drawing.Size(384, 211); // Establece el tamaño del formulario
            this.Controls.Add(this.btnOk); // Agrega el botón "OK" al formulario
            this.Controls.Add(this.btnExit); // Agrega el botón "Exit" al formulario
            this.Controls.Add(this.lblPassword); // Agrega la etiqueta de contraseña al formulario
            this.Controls.Add(this.lblUsername); // Agrega la etiqueta de nombre de usuario al formulario
            this.Controls.Add(this.txtPassword); // Agrega el cuadro de texto de contraseña al formulario
            this.Controls.Add(this.txtUsername); // Agrega el cuadro de texto de nombre de usuario al formulario
            this.Name = "LoginForm"; // Establece el nombre del formulario
            this.Text = "Login"; // Establece el título del formulario
            this.ResumeLayout(false); // Finaliza el diseño del formulario
            this.PerformLayout(); // Aplica los cambios de diseño al formulario
        }

        private System.Windows.Forms.TextBox txtUsername; // Declara un cuadro de texto para el nombre de usuario
        private System.Windows.Forms.TextBox txtPassword; // Declara un cuadro de texto para la contraseña
        private System.Windows.Forms.Label lblUsername; // Declara una etiqueta para el nombre de usuario
        private System.Windows.Forms.Label lblPassword; // Declara una etiqueta para la contraseña
        private System.Windows.Forms.Button btnOk; // Declara un botón "OK"
        private System.Windows.Forms.Button btnExit; // Declara un botón "Exit"
    }
}
