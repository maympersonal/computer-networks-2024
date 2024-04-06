partial class MainForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null; // Variable para almacenar los componentes del diseñador

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) // Método para liberar recursos utilizados por el formulario
    {
        if (disposing && (components != null)) // Verificar si se deben liberar recursos administrados y si hay componentes para liberar
        {
            components.Dispose(); // Liberar los componentes del diseñador
        }
        base.Dispose(disposing); // Llamar al método base para realizar la limpieza adicional
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        // Inicializar los componentes de la interfaz de usuario generados por el diseñador
        this.txtServerName = new System.Windows.Forms.TextBox(); // Crear un nuevo cuadro de texto para el nombre del servidor
        this.txtServerIP = new System.Windows.Forms.TextBox(); // Crear un nuevo cuadro de texto para la dirección IP del servidor
        this.txtPort = new System.Windows.Forms.TextBox(); // Crear un nuevo cuadro de texto para el puerto del servidor
        this.btnStart = new System.Windows.Forms.Button(); // Crear un nuevo botón para iniciar el servidor
        this.btnStop = new System.Windows.Forms.Button(); // Crear un nuevo botón para detener el servidor
        this.txtStatus = new System.Windows.Forms.TextBox(); // Crear un nuevo cuadro de texto para mostrar el estado del servidor
        this.lblServerName = new System.Windows.Forms.Label(); // Crear una nueva etiqueta para el nombre del servidor
        this.lblServerIP = new System.Windows.Forms.Label(); // Crear una nueva etiqueta para la dirección IP del servidor
        this.lblPort = new System.Windows.Forms.Label(); // Crear una nueva etiqueta para el puerto del servidor
        this.SuspendLayout(); // Suspender el diseño de la interfaz de usuario para realizar cambios

        // Configurar las propiedades de cada control en el formulario

        // 
        // txtServerName
        // 
        // Configurar las propiedades del cuadro de texto para el nombre del servidor
                // Configurar las propiedades del cuadro de texto para el nombre del servidor
        this.txtServerName.Location = new System.Drawing.Point(120, 20); // Establecer la posición del cuadro de texto en el formulario
        this.txtServerName.Name = "txtServerName"; // Asignar un nombre al cuadro de texto
        this.txtServerName.Size = new System.Drawing.Size(250, 20); // Establecer el tamaño del cuadro de texto
        this.txtServerName.TabIndex = 0; // Establecer el índice de tabulación del cuadro de texto
        this.txtServerName.Text = "MySMTPServer"; // Establecer el texto predeterminado del cuadro de texto

        // 
        // txtServerIP
        // 
        // Configurar las propiedades del cuadro de texto para la dirección IP del servidor
        this.txtServerIP.Location = new System.Drawing.Point(120, 50); // Establecer la posición del cuadro de texto en el formulario
        this.txtServerIP.Name = "txtServerIP"; // Asignar un nombre al cuadro de texto
        this.txtServerIP.Size = new System.Drawing.Size(250, 20); // Establecer el tamaño del cuadro de texto
        this.txtServerIP.TabIndex = 1; // Establecer el índice de tabulación del cuadro de texto
        this.txtServerIP.Text = "127.0.0.1"; // Establecer el texto predeterminado del cuadro de texto

        // 
        // txtPort
        // 
        // Configurar las propiedades del cuadro de texto para el puerto del servidor
        this.txtPort.Location = new System.Drawing.Point(120, 80); // Establecer la posición del cuadro de texto en el formulario
        this.txtPort.Name = "txtPort"; // Asignar un nombre al cuadro de texto
        this.txtPort.Size = new System.Drawing.Size(100, 20); // Establecer el tamaño del cuadro de texto
        this.txtPort.TabIndex = 2; // Establecer el índice de tabulación del cuadro de texto
        this.txtPort.Text = "25"; // Establecer el texto predeterminado del cuadro de texto

        // 
        // btnStart
        // 
        // Configurar las propiedades del botón para iniciar el servidor
        this.btnStart.Location = new System.Drawing.Point(20, 120); // Establecer la posición del botón en el formulario
        this.btnStart.Name = "btnStart"; // Asignar un nombre al botón
        this.btnStart.Size = new System.Drawing.Size(80, 30); // Establecer el tamaño del botón
        this.btnStart.TabIndex = 3; // Establecer el índice de tabulación del botón
        this.btnStart.Text = "Start"; // Establecer el texto del botón
        this.btnStart.UseVisualStyleBackColor = true; // Establecer si el botón mostrará un estilo predeterminado o personalizado
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click); // Asociar un manejador de eventos al hacer clic en el botón

        // 
        // btnStop
        // 
        // Configurar las propiedades del botón para detener el servidor
        this.btnStop.Location = new System.Drawing.Point(120, 120); // Establecer la posición del botón en el formulario
        this.btnStop.Name = "btnStop"; // Asignar un nombre al botón
        this.btnStop.Size = new System.Drawing.Size(80, 30); // Establecer el tamaño del botón
        this.btnStop.TabIndex = 4; // Establecer el índice de tabulación del botón
        this.btnStop.Text = "Stop"; // Establecer el texto del botón
        this.btnStop.UseVisualStyleBackColor = true; // Establecer si el botón mostrará un estilo predeterminado o personalizado
        this.btnStop.Click += new System.EventHandler(this.btnStop_Click); // Asociar un manejador de eventos al hacer clic en el botón

        // 
        // txtStatus
        // 
        // Configurar las propiedades del cuadro de texto para mostrar el estado del servidor
        this.txtStatus.Location = new System.Drawing.Point(20, 170); // Establecer la posición del cuadro de texto en el formulario
        this.txtStatus.Multiline = true; // Permitir múltiples líneas de texto en el cuadro de texto
        this.txtStatus.Name = "txtStatus"; // Asignar un nombre al cuadro de texto
        this.txtStatus.ReadOnly = true; // Establecer el cuadro de texto como solo lectura
        this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical; // Habilitar la barra de desplazamiento vertical en el cuadro de texto
        this.txtStatus.Size = new System.Drawing.Size(350, 100); // Establecer el tamaño del cuadro de texto
        this.txtStatus.TabIndex = 5; // Establecer el índice de tabulación del cuadro de texto

        // 
        // lblServerName
        // 
        // Configurar las propiedades de la etiqueta para el nombre del servidor
        this.lblServerName.AutoSize = true; // Permitir que la etiqueta ajuste automáticamente su tamaño
        this.lblServerName.Location = new System.Drawing.Point(20, 23); // Establecer la posición de la etiqueta en el formulario
        this.lblServerName.Name = "lblServerName"; // Asignar un nombre a la etiqueta
        this.lblServerName.Size = new System.Drawing.Size(67, 13); // Establecer el tamaño de la etiqueta
        this.lblServerName.TabIndex = 6; // Establecer el índice de tabulación de la etiqueta
        this.lblServerName.Text = "Server Name"; // Establecer el texto de la etiqueta

        // 
        // lblServerIP
        // 
        // Configurar las propiedades de la etiqueta para la dirección IP del servidor
        this.lblServerIP.AutoSize = true; // Permitir que la etiqueta ajuste automáticamente su tamaño
        this.lblServerIP.Location = new System.Drawing.Point(20, 53); // Establecer la posición de la etiqueta en el formulario
        this.lblServerIP.Name = "lblServerIP"; // Asignar un nombre a la etiqueta
        this.lblServerIP.Size = new System.Drawing.Size(53, 13); // Establecer el tamaño de la etiqueta
        this.lblServerIP.TabIndex = 7; // Establecer el índice de tabulación de la etiqueta
        this.lblServerIP.Text = "Server IP"; // Establecer el texto de la etiqueta

        // 
        // lblPort
        // 
        // Configurar las propiedades de la etiqueta para el puerto del servidor
        this.lblPort.AutoSize = true; // Permitir que la etiqueta ajuste automáticamente su tamaño
        this.lblPort.Location = new System.Drawing.Point(20, 83); // Establecer la posición de la etiqueta en el formulario
        this.lblPort.Name = "lblPort"; // Asignar un nombre a la etiqueta
        this.lblPort.Size = new System.Drawing.Size(26, 13); // Establecer el tamaño de la etiqueta
        this.lblPort.TabIndex = 8; // Establecer el índice de tabulación de la etiqueta
        this.lblPort.Text = "Port"; // Establecer el texto de la etiqueta


                // 
        // MainForm
        // 
        // Configurar las propiedades del formulario principal
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // Establecer el tamaño de fuente automático basado en DPI
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; // Permitir que el formulario ajuste automáticamente su tamaño de fuente
        this.ClientSize = new System.Drawing.Size(400, 300); // Establecer el tamaño del formulario
        this.Controls.Add(this.lblPort); // Agregar la etiqueta del puerto al formulario
        this.Controls.Add(this.lblServerIP); // Agregar la etiqueta de la dirección IP del servidor al formulario
        this.Controls.Add(this.lblServerName); // Agregar la etiqueta del nombre del servidor al formulario
        this.Controls.Add(this.txtStatus); // Agregar el cuadro de texto de estado al formulario
        this.Controls.Add(this.btnStop); // Agregar el botón de detener al formulario
        this.Controls.Add(this.btnStart); // Agregar el botón de iniciar al formulario
        this.Controls.Add(this.txtPort); // Agregar el cuadro de texto del puerto al formulario
        this.Controls.Add(this.txtServerIP); // Agregar el cuadro de texto de la dirección IP del servidor al formulario
        this.Controls.Add(this.txtServerName); // Agregar el cuadro de texto del nombre del servidor al formulario
        this.Name = "MainForm"; // Establecer el nombre del formulario
        this.Text = "SMTP Server"; // Establecer el texto del título del formulario
        this.ResumeLayout(false); // Reanudar el diseño de la interfaz de usuario después de realizar cambios
        this.PerformLayout(); // Reanudar el diseño de la interfaz de usuario después de realizar cambios
    }

    #endregion
    private System.Windows.Forms.TextBox txtServerName; // Declaración de un cuadro de texto para el nombre del servidor
    private System.Windows.Forms.TextBox txtServerIP; // Declaración de un cuadro de texto para la dirección IP del servidor
    private System.Windows.Forms.TextBox txtPort; // Declaración de un cuadro de texto para el puerto del servidor
    private System.Windows.Forms.Button btnStart; // Declaración de un botón para iniciar el servidor
    private System.Windows.Forms.Button btnStop; // Declaración de un botón para detener el servidor
    private System.Windows.Forms.TextBox txtStatus; // Declaración de un cuadro de texto para mostrar el estado del servidor
    private System.Windows.Forms.Label lblServerName; // Declaración de una etiqueta para el nombre del servidor
    private System.Windows.Forms.Label lblServerIP; // Declaración de una etiqueta para la dirección IP del servidor
    private System.Windows.Forms.Label lblPort; // Declaración de una etiqueta para el puerto del servidor

}
