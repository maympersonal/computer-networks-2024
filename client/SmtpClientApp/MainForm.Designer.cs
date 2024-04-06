namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{
    partial class MainForm // Define la clase MainForm como parcial
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
            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblAttachments = new System.Windows.Forms.Label();
            this.lblMessages = new System.Windows.Forms.Label();
            this.lblBody = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtAttachments = new System.Windows.Forms.TextBox();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.RichTextBox();
            this.btnAdjuntar = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button(); // Agregado el botón para cerrar la aplicación
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblServerIP.Location = new System.Drawing.Point(30, 55); // Establece la ubicación de la etiqueta Server IP
            this.lblServerIP.Name = "lblServerIP"; // Asigna un nombre a la etiqueta Server IP
            this.lblServerIP.Size = new System.Drawing.Size(54, 13); // Establece el tamaño de la etiqueta Server IP
            this.lblServerIP.TabIndex = 0; // Establece el índice de tabulación de la etiqueta Server IP
            this.lblServerIP.Text = "Server IP:"; // Establece el texto de la etiqueta Server IP
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblPort.Location = new System.Drawing.Point(30, 93); // Establece la ubicación de la etiqueta Port
            this.lblPort.Name = "lblPort"; // Asigna un nombre a la etiqueta Port
            this.lblPort.Size = new System.Drawing.Size(29, 13); // Establece el tamaño de la etiqueta Port
            this.lblPort.TabIndex = 1; // Establece el índice de tabulación de la etiqueta Port
            this.lblPort.Text = "Port:"; // Establece el texto de la etiqueta Port
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblFrom.Location = new System.Drawing.Point(30, 133); // Establece la ubicación de la etiqueta From
            this.lblFrom.Name = "lblFrom"; // Asigna un nombre a la etiqueta From
            this.lblFrom.Size = new System.Drawing.Size(36, 13); // Establece el tamaño de la etiqueta From
            this.lblFrom.TabIndex = 2; // Establece el índice de tabulación de la etiqueta From
            this.lblFrom.Text = "From:"; // Establece el texto de la etiqueta From
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblTo.Location = new System.Drawing.Point(30, 173); // Establece la ubicación de la etiqueta To
            this.lblTo.Name = "lblTo"; // Asigna un nombre a la etiqueta To
            this.lblTo.Size = new System.Drawing.Size(23, 13); // Establece el tamaño de la etiqueta To
            this.lblTo.TabIndex = 3; // Establece el índice de tabulación de la etiqueta To
            this.lblTo.Text = "To:"; // Establece el texto de la etiqueta To
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblSubject.Location = new System.Drawing.Point(30, 213); // Establece la ubicación de la etiqueta Subject
            this.lblSubject.Name = "lblSubject"; // Asigna un nombre a la etiqueta Subject
            this.lblSubject.Size = new System.Drawing.Size(46, 13); // Establece el tamaño de la etiqueta Subject
            this.lblSubject.TabIndex = 4; // Establece el índice de tabulación de la etiqueta Subject
            this.lblSubject.Text = "Subject:"; // Establece el texto de la etiqueta Subject
            // 
            // lblAttachments
            // 
            this.lblAttachments.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblAttachments.Location = new System.Drawing.Point(30, 253); // Establece la ubicación de la etiqueta Attachments
            this.lblAttachments.Name = "lblAttachments"; // Asigna un nombre a la etiqueta Attachments
            this.lblAttachments.Size = new System.Drawing.Size(68, 13); // Establece el tamaño de la etiqueta Attachments
            this.lblAttachments.TabIndex = 5; // Establece el índice de tabulación de la etiqueta Attachments
            this.lblAttachments.Text = "Attachments:"; // Establece el texto de la etiqueta Attachments
            // 
            // lblMessages
            // 
            this.lblMessages.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblMessages.Location = new System.Drawing.Point(30, 433); // Establece la ubicación de la etiqueta Messages
            this.lblMessages.Name = "lblMessages"; // Asigna un nombre a la etiqueta Messages
            this.lblMessages.Size = new System.Drawing.Size(34, 13); // Establece el tamaño de la etiqueta Messages
            this.lblMessages.TabIndex = 6; // Establece el índice de tabulación de la etiqueta Messages
            this.lblMessages.Text = "Messages:"; // Establece el texto de la etiqueta Messages
            // 
            // lblBody
            // 
            this.lblBody.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblBody.Location = new System.Drawing.Point(30, 293); // Establece la ubicación de la etiqueta Body
            this.lblBody.Name = "lblBody"; // Asigna un nombre a la etiqueta Body
            this.lblBody.Size = new System.Drawing.Size(59, 13); // Establece el tamaño de la etiqueta Body
            this.lblBody.TabIndex = 7; // Establece el índice de tabulación de la etiqueta Body
            this.lblBody.Text = "Body:"; // Establece el texto de la etiqueta Body
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(100, 55); // Establece la ubicación del cuadro de texto Server IP
            this.txtServerIP.Name = "txtServerIP"; // Asigna un nombre al cuadro de texto Server IP
            this.txtServerIP.Size = new System.Drawing.Size(500, 20); // Establece el tamaño del cuadro de texto Server IP
            this.txtServerIP.TabIndex = 8; // Establece el índice de tabulación del cuadro de texto Server IP
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(100, 90); // Establece la ubicación del cuadro de texto Port
            this.txtPort.Name = "txtPort"; // Asigna un nombre al cuadro de texto Port
            this.txtPort.Size = new System.Drawing.Size(500, 20); // Establece el tamaño del cuadro de texto Port
            this.txtPort.TabIndex = 9; // Establece el índice de tabulación del cuadro de texto Port
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(100, 130); // Establece la ubicación del cuadro de texto From
            this.txtFrom.Name = "txtFrom"; // Asigna un nombre al cuadro de texto From
            this.txtFrom.Size = new System.Drawing.Size(500, 20); // Establece el tamaño del cuadro de texto From
            this.txtFrom.TabIndex = 10; // Establece el índice de tabulación del cuadro de texto From
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(100, 170); // Establece la ubicación del cuadro de texto To
            this.txtTo.Name = "txtTo"; // Asigna un nombre al cuadro de texto To
            this.txtTo.Size = new System.Drawing.Size(500, 20); // Establece el tamaño del cuadro de texto To
            this.txtTo.TabIndex = 11; // Establece el índice de tabulación del cuadro de texto To
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(100, 210); // Establece la ubicación del cuadro de texto Subject
            this.txtSubject.Name = "txtSubject"; // Asigna un nombre al cuadro de texto Subject
            this.txtSubject.Size = new System.Drawing.Size(500, 20); // Establece el tamaño del cuadro de texto Subject
            this.txtSubject.TabIndex = 12; // Establece el índice de tabulación del cuadro de texto Subject
            // 
            // txtAttachments
            // 
            this.txtAttachments.Location = new System.Drawing.Point(100, 250); // Establece la ubicación del cuadro de texto Attachments
            this.txtAttachments.Name = "txtAttachments"; // Asigna un nombre al cuadro de texto Attachments
            this.txtAttachments.Size = new System.Drawing.Size(400, 20); // Establece el tamaño del cuadro de texto Attachments
            this.txtAttachments.TabIndex = 13; // Establece el índice de tabulación del cuadro de texto Attachments
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(100, 430); // Establece la ubicación del cuadro de texto Messages
            this.txtMessages.Multiline = true; // Establece la propiedad Multiline del cuadro de texto a true
            this.txtMessages.Name = "txtMessages"; // Asigna un nombre al cuadro de texto Messages
            this.txtMessages.Size = new System.Drawing.Size(500, 80); // Establece el tamaño del cuadro de texto Messages
            this.txtMessages.TabIndex = 14; // Establece el índice de tabulación del cuadro de texto Messages
            this.txtMessages.ReadOnly = true; // Establece la propiedad ReadOnly del cuadro de texto a true
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical; // Establece las barras de desplazamiento vertical para el cuadro de texto
            // 
            // txtBody
            // 
            this.txtBody.Location = new System.Drawing.Point(100, 290); // Establece la ubicación del cuadro de texto Body
            this.txtBody.Name = "txtBody"; // Asigna un nombre al cuadro de texto Body
            this.txtBody.Size = new System.Drawing.Size(500, 80); // Establece el tamaño del cuadro de texto Body
            this.txtBody.TabIndex = 15; // Establece el índice de tabulación del cuadro de texto Body
            this.txtBody.Text = ""; // Establece el texto del cuadro de texto Body
            // 
            // btnAdjuntar
            // 
            this.btnAdjuntar.Location = new System.Drawing.Point(525, 250); // Establece la ubicación del botón Adjuntar
            this.btnAdjuntar.Name = "btnAdjuntar"; // Asigna un nombre al botón Adjuntar
            this.btnAdjuntar.Size = new System.Drawing.Size(75, 23); // Establece el tamaño del botón Adjuntar
            this.btnAdjuntar.TabIndex = 16; // Establece el índice de tabulación del botón Adjuntar
            this.btnAdjuntar.Text = "Adjuntar"; // Establece el texto del botón Adjuntar
            this.btnAdjuntar.UseVisualStyleBackColor = true; // Establece la propiedad UseVisualStyleBackColor del botón a true
            this.btnAdjuntar.Click += new System.EventHandler(this.btnAdjuntar_Click); // Agrega un manejador de eventos para el clic en el botón Adjuntar
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(525, 390); // Establece la ubicación del botón Send
            this.btnSend.Name = "btnSend"; // Asigna un nombre al botón Send
            this.btnSend.Size = new System.Drawing.Size(75, 23); // Establece el tamaño del botón Send
            this.btnSend.TabIndex = 17; // Establece el índice de tabulación del botón Send
            this.btnSend.Text = "Send"; // Establece el texto del botón Send
            this.btnSend.UseVisualStyleBackColor = true; // Establece la propiedad UseVisualStyleBackColor del botón a true
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click); // Agrega un manejador de eventos para el clic en el botón Send
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(525, 530); // Establece la ubicación del botón Exit
            this.btnExit.Name = "btnExit"; // Asigna un nombre al botón Exit
            this.btnExit.Size = new System.Drawing.Size(75, 23); // Establece el tamaño del botón Exit
            this.btnExit.TabIndex = 18; // Establece el índice de tabulación del botón Exit
            this.btnExit.Text = "Exit"; // Establece el texto del botón Exit
            this.btnExit.UseVisualStyleBackColor = true; // Establece la propiedad UseVisualStyleBackColor del botón a true
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click); // Agrega un manejador de eventos para el clic en el botón Exit

            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblUsername.Location = new System.Drawing.Point(30, 10); // Establece la ubicación de la etiqueta Username
            this.lblUsername.Name = "lblUsername"; // Asigna un nombre a la etiqueta Username
            this.lblUsername.Size = new System.Drawing.Size(58, 13); // Establece el tamaño de la etiqueta Username
            this.lblUsername.TabIndex = 19; // Establece el índice de tabulación de la etiqueta Username
            this.lblUsername.Text = "Username:"; // Establece el texto de la etiqueta Username
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(100, 10); // Establece la ubicación del cuadro de texto Username
            this.txtUsername.Name = "txtUsername"; // Asigna un nombre al cuadro de texto Username
            this.txtUsername.ReadOnly = true; // Establece la propiedad ReadOnly del cuadro de texto a true
            this.txtUsername.Size = new System.Drawing.Size(200, 20); // Establece el tamaño del cuadro de texto Username
            this.txtUsername.TabIndex = 20; // Establece el índice de tabulación del cuadro de texto Username
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true; // Establece la propiedad AutoSize de la etiqueta a true
            this.lblPassword.Location = new System.Drawing.Point(330, 10); // Establece la ubicación de la etiqueta Password
            this.lblPassword.Name = "lblPassword"; // Asigna un nombre a la etiqueta Password
            this.lblPassword.Size = new System.Drawing.Size(56, 13); // Establece el tamaño de la etiqueta Password
            this.lblPassword.TabIndex = 21; // Establece el índice de tabulación de la etiqueta Password
            this.lblPassword.Text = "Password:"; // Establece el texto de la etiqueta Password
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(400, 10); // Establece la ubicación del cuadro de texto Password
            this.txtPassword.Name = "txtPassword"; // Asigna un nombre al cuadro de texto Password
            this.txtPassword.ReadOnly = true; // Establece la propiedad ReadOnly del cuadro de texto a true
            this.txtPassword.Size = new System.Drawing.Size(200, 20); // Establece el tamaño del cuadro de texto Password
            this.txtPassword.TabIndex = 22; // Establece el índice de tabulación del cuadro de texto Password


            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // Establece el tamaño de la fuente del formulario
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; // Establece el modo de cambio de tamaño del formulario
            this.ClientSize = new System.Drawing.Size(640, 560); // Establece el tamaño del formulario
            this.Controls.Add(this.btnExit); // Agrega el botón Exit al formulario
            this.Controls.Add(this.btnSend); // Agrega el botón Send al formulario
            this.Controls.Add(this.btnAdjuntar); // Agrega el botón Adjuntar al formulario
            this.Controls.Add(this.txtBody); // Agrega el cuadro de texto Body al formulario
            this.Controls.Add(this.txtMessages); // Agrega el cuadro de texto Messages al formulario
            this.Controls.Add(this.txtAttachments); // Agrega el cuadro de texto Attachments al formulario
            this.Controls.Add(this.txtSubject); // Agrega el cuadro de texto Subject al formulario
            this.Controls.Add(this.txtTo); // Agrega el cuadro de texto To al formulario
            this.Controls.Add(this.txtFrom); // Agrega el cuadro de texto From al formulario
            this.Controls.Add(this.txtPort); // Agrega el cuadro de texto Port al formulario
            this.Controls.Add(this.txtServerIP); // Agrega el cuadro de texto Server IP al formulario
            this.Controls.Add(this.lblBody); // Agrega la etiqueta Body al formulario
            this.Controls.Add(this.lblMessages); // Agrega la etiqueta Messages al formulario
            this.Controls.Add(this.lblAttachments); // Agrega la etiqueta Attachments al formulario
            this.Controls.Add(this.lblSubject); // Agrega la etiqueta Subject al formulario
            this.Controls.Add(this.lblTo); // Agrega la etiqueta To al formulario
            this.Controls.Add(this.lblFrom); // Agrega la etiqueta From al formulario
            this.Controls.Add(this.lblPort); // Agrega la etiqueta Port al formulario
            this.Controls.Add(this.lblServerIP); // Agrega la etiqueta Server IP al formulario
            this.Controls.Add(this.lblUsername); // Agrega la etiqueta Username al formulario
            this.Controls.Add(this.txtUsername); // Agrega el cuadro de texto Username al formulario
            this.Controls.Add(this.lblPassword); // Agrega la etiqueta Password al formulario
            this.Controls.Add(this.txtPassword); // Agrega el cuadro de texto Password al formulario
            this.Name = "MainForm"; // Establece el nombre del formulario
            this.Text = "SMTP Client"; // Establece el título del formulario
            this.ResumeLayout(false); // Finaliza el diseño de los controles del formulario
            this.PerformLayout(); // Aplica los cambios de diseño a los controles del formulario
        }

        private System.Windows.Forms.Label lblServerIP; // Declara la etiqueta Server IP
        private System.Windows.Forms.Label lblPort; // Declara la etiqueta Port
        private System.Windows.Forms.Label lblFrom; // Declara la etiqueta From
        private System.Windows.Forms.Label lblTo; // Declara la etiqueta To
        private System.Windows.Forms.Label lblSubject; // Declara la etiqueta Subject
        private System.Windows.Forms.Label lblAttachments; // Declara la etiqueta Attachments
        private System.Windows.Forms.Label lblMessages; // Declara la etiqueta Messages
        private System.Windows.Forms.Label lblBody; // Declara la etiqueta Body
        private System.Windows.Forms.TextBox txtServerIP; // Declara el cuadro de texto Server IP
        private System.Windows.Forms.TextBox txtPort; // Declara el cuadro de texto Port
        private System.Windows.Forms.TextBox txtFrom; // Declara el cuadro de texto From
        private System.Windows.Forms.TextBox txtTo; // Declara el cuadro de texto To
        private System.Windows.Forms.TextBox txtSubject; // Declara el cuadro de texto Subject
        private System.Windows.Forms.TextBox txtAttachments; // Declara el cuadro de texto Attachments
        private System.Windows.Forms.TextBox txtMessages; // Declara el cuadro de texto Messages
        private System.Windows.Forms.RichTextBox txtBody; // Declara el cuadro de texto Body
        private System.Windows.Forms.Button btnAdjuntar; // Declara el botón Adjuntar
        private System.Windows.Forms.Button btnSend; // Declara el botón Send
        private System.Windows.Forms.Button btnExit; // Declara el botón Exit
        private System.Windows.Forms.Label lblUsername; // Declara la etiqueta Username
        private System.Windows.Forms.TextBox txtUsername; // Declara el cuadro de texto Username
        private System.Windows.Forms.Label lblPassword; // Declara la etiqueta Password
        private System.Windows.Forms.TextBox txtPassword; // Declara el cuadro de texto Password
    }
}
