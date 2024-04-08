using System; // Importa el espacio de nombres System
using System.Windows.Forms; // Importa el espacio de nombres System.Windows.Forms
using System.IO; // Importa el espacio de nombres System.IO
using System.Net; // Importa el espacio de nombres System.Net
using System.Net.Sockets; // Importa el espacio de nombres System.Net.Sockets
using System.Text; // Importa el espacio de nombres System.Text
using System.Threading; // Importa el espacio de nombres System.Threading
using System.Collections.Generic; // Importa el espacio de nombres System.Collections.Generic
using System.Linq; // Importa el espacio de nombres System.Linq
using System.Net.Mail; // Importa el espacio de nombres System.Net.Mail
using System.Text.RegularExpressions; // Importa el espacio de nombres System.Text.RegularExpressions
using System.CodeDom; // Importa el espacio de nombres System.CodeDom

namespace SmtpClientApp // Define el espacio de nombres SmtpClientApp
{
    public class SendMailReturnCodes // Define una clase pública SendMailReturnCodes
    {
        public const int Success = 0; // Define una constante pública Success con valor 0
        public const int ConnectionError = 1; // Define una constante pública ConnectionError con valor 1
        public const int AuthenticationError = 2; // Define una constante pública AuthenticationError con valor 2
        public const int SenderAddressError = 3; // Define una constante pública SenderAddressError con valor 3
        public const int RecipientAddressError = 4; // Define una constante pública RecipientAddressError con valor 4
        public const int DataTransmissionError = 5; // Define una constante pública DataTransmissionError con valor 5
        public const int MessageBodyError = 6; // Define una constante pública MessageBodyError con valor 6
        public const int QuitCommandError = 7; // Define una constante pública QuitCommandError con valor 7
        public const int GenerateMimeEmailError = 8; // Define una constante pública GenerateMimeEmailError con valor 8
    }

    public class ClientSMTP // Define una clase pública ClientSMTP
    {
        private string smtpServer; // Declara un campo privado smtpServer de tipo string
        private int port; // Declara un campo privado port de tipo int
        private string username; // Declara un campo privado username de tipo string
        private string password; // Declara un campo privado password de tipo string
        private string fromAddress; // Declara un campo privado fromAddress de tipo string
        private TextBox status; // Declara un campo privado status de tipo TextBox

        public ClientSMTP(string smtpServer, int port, string username, string password, string fromAddress, TextBox status) // Define un constructor público para la clase ClientSMTP
        {
            this.smtpServer = smtpServer; // Asigna el valor del parámetro smtpServer al campo smtpServer
            this.port = port; // Asigna el valor del parámetro port al campo port
            this.username = username; // Asigna el valor del parámetro username al campo username
            this.password = password; // Asigna el valor del parámetro password al campo password
            this.fromAddress = fromAddress; // Asigna el valor del parámetro fromAddress al campo fromAddress
            this.status = status; // Asigna el valor del parámetro status al campo status
        }

        public int SendMail(string[] toAddresses, string subject, string body, string[] attachmentPaths) // Define un método público SendMail que devuelve un entero y toma varios parámetros
        {
            foreach (string toAddress in toAddresses) // Itera sobre las direcciones de correo electrónico de los destinatarios
                if (!IsValidEmail(toAddress)) // Si una dirección de correo electrónico no es válida
                    return SendMailReturnCodes.RecipientAddressError; // Devuelve un código de error de dirección de destinatario

            try // Intenta realizar las siguientes operaciones
            {
                string mimeEmail = GenerateMimeEmail(fromAddress, toAddresses, subject, body, attachmentPaths); // Genera el correo electrónico MIME

                using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) // Crea un cliente Socket para la comunicación TCP
                {
                    client.Connect(smtpServer, port); // Conéctate al servidor SMTP
                    using (var stream = new NetworkStream(client)) // Crea un flujo de red
                    using (var writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true }) // Crea un escritor para escribir en el flujo de red
                    using (var reader = new StreamReader(stream, Encoding.ASCII)) // Crea un lector para leer del flujo de red
                    {
                        Console.WriteLine("aaaaa");
                        SendCommand(writer, $"HELO {smtpServer}"); // Envía el comando HELO al servidor SMTP
<<<<<<< Updated upstream
                        if (!CheckResponse(reader, "250")){
                            Console.WriteLine("aaaa"); // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.ConnectionError;} // Devuelve un código de error de conexión

=======
                        if (!CheckResponse(reader, "220")) // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.ConnectionError; // Devuelve un código de error de conexión
                        if (!CheckResponse(reader, "250")) // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.ConnectionError; // Devuelve un código de error de conexión
                        
                        
                        //SendCommand(writer, $"{smtpServer}"); // Envía el comando HELO al servidor SMTP
                        //if (!CheckResponse(reader, "250")) // Comprueba la respuesta del servidor
                         //   return SendMailReturnCodes.ConnectionError; // Devuelve un código de error de conexión
                        //if (!CheckResponse(reader, "250")) // Comprueba la respuesta del servidor
                            //return SendMailReturnCodes.ConnectionError; // Devuelve un código de error de conexión
                        //SendCommand(writer, "AUTH"); // Envía el comando AUTH LOGIN al servidor SMTP
                        //ShowResponse(reader);
>>>>>>> Stashed changes
                        SendCommand(writer, "AUTH LOGIN"); // Envía el comando AUTH LOGIN al servidor SMTP
                        if (!CheckResponse(reader, "334")) // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.AuthenticationError; // Devuelve un código de error de autenticación
                        SendCommand(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(username))); // Envía el nombre de usuario codificado en base64 al servidor SMTP
                        if (!CheckResponse(reader, "334")) // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.AuthenticationError; // Devuelve un código de error de autenticación
                        SendCommand(writer, Convert.ToBase64String(Encoding.UTF8.GetBytes(password))); // Envía la contraseña codificada en base64 al servidor SMTP
                        if (!CheckResponse(reader, "235")) // Comprueba la respuesta del servidor
                           return SendMailReturnCodes.AuthenticationError; // Devuelve un código de error de autenticación

                        SendCommand(writer, $"MAIL FROM:<{fromAddress}>"); // Envía el comando MAIL FROM al servidor SMTP
                        if (!CheckResponse(reader, "250")) // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.SenderAddressError; // Devuelve un código de error de dirección del remitente

                        foreach (string toAddress in toAddresses) // Itera sobre las direcciones de correo electrónico de los destinatarios
                        {
                            SendCommand(writer, $"RCPT TO:<{toAddress}>"); // Envía el comando RCPT TO al servidor SMTP
                            if (!CheckResponse(reader, "250")) // Comprueba la respuesta del servidor
                              return SendMailReturnCodes.RecipientAddressError; // Devuelve un código de error de dirección de destinatario
                        }

                        SendCommand(writer, "DATA"); // Envía el comando DATA al servidor SMTP
                        if (!CheckResponse(reader, "354")) // Comprueba la respuesta del servidor
                           return SendMailReturnCodes.DataTransmissionError; // Devuelve un código de error de transmisión de datos

                        SendCommand(writer, mimeEmail); // Envía el correo electrónico MIME al servidor SMTP
                        SendCommand(writer, "."); // Envía el comando "." al servidor SMTP
                        if (!CheckResponse(reader, "250")) // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.MessageBodyError; // Devuelve un código de error de cuerpo del mensaje

                        SendCommand(writer, "QUIT"); // Envía el comando QUIT al servidor SMTP
                        if (!CheckResponse(reader, "221")) // Comprueba la respuesta del servidor
                            return SendMailReturnCodes.QuitCommandError; // Devuelve un código de error de comando QUIT
                    }
                    return SendMailReturnCodes.Success; // Devuelve un código de éxito
                }
            }
            catch (Exception ex) // Captura cualquier excepción
            {
                Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}"); // Muestra un mensaje de error en la consola
                return SendMailReturnCodes.GenerateMimeEmailError; // Devuelve un código de error de generación de correo electrónico MIME
            }
        }

        private void UpdateStatus(string format, params object[] args) // Define un método privado UpdateStatus que actualiza el estado del cliente SMTP
        {
            string msg = string.Format(format, args) + Environment.NewLine; // Formatea el mensaje con los argumentos y agrega un salto de línea
            this.status.AppendText(msg); // Agrega el mensaje al cuadro de texto de estado
        }

        private void SendCommand(StreamWriter writer, string? line = null) // Define un método privado SendCommand para enviar comandos al servidor SMTP
        {
            if (line != null) // Si se proporciona una línea de comando
            {
                writer.WriteLine(line); // Escribe la línea en el flujo de escritura
                UpdateStatus($"SEND: {line}"); // Actualiza el estado con el comando enviado
            }
            else // Si no se proporciona una línea de comando
            {
                writer.WriteLine(); // Escribe una línea en blanco en el flujo de escritura
                UpdateStatus($"SEND:"); // Actualiza el estado indicando un envío vacío
            }
        }

        private void ShowResponse(StreamReader reader) // Define un método privado CheckResponse para verificar las respuestas del servidor SMTP
        {
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string response = reader.ReadLine(); // Lee la respuesta del servidor
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            UpdateStatus($"RESPONSE: {response}"); // Actualiza el estado con la respuesta recibida
        }
        private bool CheckResponse(StreamReader reader, string expectedCode) // Define un método privado CheckResponse para verificar las respuestas del servidor SMTP
        {
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            string response = reader.ReadLine(); // Lee la respuesta del servidor
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            UpdateStatus($"RESPONSE: {response}"); // Actualiza el estado con la respuesta recibida
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            return response.StartsWith(expectedCode); // Devuelve verdadero si la respuesta comienza con el código esperado
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        private bool IsValidEmail(string email) // Define un método privado IsValidEmail para validar direcciones de correo electrónico
        {
            try // Intenta realizar las siguientes operaciones
            {
                // Verifica el formato de la dirección de correo electrónico utilizando una expresión regular
                var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                return emailRegex.IsMatch(email); // Devuelve verdadero si la dirección de correo electrónico coincide con el patrón
            }
            catch (FormatException) // Captura una excepción de formato
            {
                return false; // Devuelve falso si hay un error de formato
            }
        }

        public static string GenerateMimeEmail(string from, string[] to, string subject, string body, string[]? attachments = null) // Define un método público estático para generar un correo electrónico MIME
        {
            // Boundary para separar las partes MIME
            string boundary = Guid.NewGuid().ToString().Replace("-", "");

            StringBuilder mimeBuilder = new StringBuilder(); // Crea un constructor de cadenas para construir el correo electrónico MIME

            // Encabezado MIME del correo electrónico
            mimeBuilder.AppendLine($"From: {from}"); // Agrega el encabezado "From" al correo electrónico MIME
            mimeBuilder.AppendLine($"To: {string.Join(",", to)}"); // Agrega el encabezado "To" al correo electrónico MIME
            mimeBuilder.AppendLine($"Subject: {subject}"); // Agrega el encabezado "Subject" al correo electrónico MIME
            mimeBuilder.AppendLine("MIME-Version: 1.0"); // Agrega el encabezado "MIME-Version" al correo electrónico MIME
            mimeBuilder.AppendLine($"Content-Type: multipart/mixed; boundary={boundary}"); // Agrega el encabezado "Content-Type" al correo electrónico MIME
            mimeBuilder.AppendLine(); // Agrega una línea en blanco al correo electrónico MIME

            // Parte de texto del correo electrónico
            mimeBuilder.AppendLine($"--{boundary}"); // Agrega el delimitador de inicio de la parte de texto al correo electrónico MIME
            mimeBuilder.AppendLine("Content-Type: text/plain; charset=utf-8"); // Agrega el encabezado "Content-Type" al correo electrónico MIME
            mimeBuilder.AppendLine("Content-Transfer-Encoding: 7bit"); // Agrega el encabezado "Content-Transfer-Encoding" al correo electrónico MIME
            mimeBuilder.AppendLine(); // Agrega una línea en blanco al correo electrónico MIME
            mimeBuilder.AppendLine(body); // Agrega el cuerpo del mensaje al correo electrónico MIME
            mimeBuilder.AppendLine(); // Agrega una línea en blanco al correo electrónico MIME

            // Adjuntos
            if (attachments != null) // Si hay archivos adjuntos
            {
                foreach (string attachmentPath in attachments) // Itera sobre los archivos adjuntos
                {
                    mimeBuilder.AppendLine($"--{boundary}"); // Agrega el delimitador de inicio de un archivo adjunto al correo electrónico MIME
                    mimeBuilder.AppendLine($"Content-Type: application/octet-stream; name=\"{Path.GetFileName(attachmentPath)}\""); // Agrega el encabezado "Content-Type" al correo electrónico MIME
                    mimeBuilder.AppendLine("Content-Transfer-Encoding: base64"); // Agrega el encabezado "Content-Transfer-Encoding" al correo electrónico MIME
                    mimeBuilder.AppendLine($"Content-Disposition: attachment; filename=\"{Path.GetFileName(attachmentPath)}\""); // Agrega el encabezado "Content-Disposition" al correo electrónico MIME
                    mimeBuilder.AppendLine(); // Agrega una línea en blanco al correo electrónico MIME

                    // Lee el archivo adjunto y lo codifica en base64
                    byte[] attachmentBytes = File.ReadAllBytes(attachmentPath); // Lee el archivo adjunto como un arreglo de bytes
                    string base64Attachment = Convert.ToBase64String(attachmentBytes); // Codifica el archivo adjunto en base64
                    mimeBuilder.AppendLine(base64Attachment); // Agrega el archivo adjunto codificado al correo electrónico MIME
                    mimeBuilder.AppendLine(); // Agrega una línea en blanco al correo electrónico MIME
                }
            }

            // Final del correo electrónico MIME
            mimeBuilder.AppendLine($"--{boundary}--"); // Agrega el delimitador de final de todas las partes MIME al correo electrónico MIME

            return mimeBuilder.ToString(); // Devuelve el correo electrónico MIME como una cadena
        }
    }
}
