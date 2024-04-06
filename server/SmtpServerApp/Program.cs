namespace SmtpServerApp
{
    // Clase estática que contiene el método principal de entrada del programa
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread] // Atributo que indica que el modelo de subprocesamiento del entorno es de subprocesamiento de apartado único
        static void Main()
        {
            // Para personalizar la configuración de la aplicación, como establecer configuraciones de DPI alto o fuente predeterminada,
            // consulte https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize(); // Inicializar la configuración de la aplicación
            Application.Run(new MainForm()); // Ejecutar la aplicación creando una instancia de MainForm
        }
    }
}
