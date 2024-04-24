using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{

    class SmtpConfigReader
    {
        private string defaultServerIP;
        private string defaultPort;
        private string defaultUserName;
        private string defaultPassword;

        private string defaultFrom;

        public SmtpConfigReader(string filePath)
        {
            ReadConfigurations(filePath);
        }

        public string ServerIP => defaultServerIP;
        public string Port => defaultPort;
        public string UserName => defaultUserName;
        public string Password => defaultPassword;
        public string From => defaultFrom;
        private void ReadConfigurations(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo de configuración no existe.", filePath);
            }

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                // Ignorar comentarios
                if (line.StartsWith("#"))
                    continue;

                var parts = line.Split(';');

                // Verificar si es el servidor por defecto
                if (parts.Last().Trim() == "*")
                {
                    defaultServerIP = parts[0].Trim();
                    defaultPort = parts[1].Trim();
                    defaultUserName = parts[2].Trim();
                    defaultPassword = parts[3].Trim();
                    defaultFrom = parts[4].Trim();
                    break;
                }
            }

            if (defaultServerIP == null)
            {
                throw new Exception("No se encontró un servidor por defecto en el archivo de configuración.");
            }
        }
    }

}

