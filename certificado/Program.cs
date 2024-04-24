using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto.Operators;

class Program {
  // Este método crea un certificado autofirmado utilizando la biblioteca BouncyCastle.
// Toma un parámetro 'hostname' que se utiliza para establecer el nombre común (CN) del certificado.
// Devuelve un objeto X509Certificate2 que representa el certificado generado.
public static X509Certificate2 CreateSelfSignedCertificate(string hostname)
{
    // Genera un generador de números aleatorios seguro utilizando el algoritmo CryptoAPI.
    CryptoApiRandomGenerator randomGenerator = new CryptoApiRandomGenerator();
    SecureRandom random = new SecureRandom(randomGenerator);

    // Crea un generador de certificados X.509 versión 3.
    X509V3CertificateGenerator certGenerator = new X509V3CertificateGenerator();

    // Establece el nombre del sujeto y del emisor del certificado como el nombre común (CN) proporcionado.
    certGenerator.SetSubjectDN(new X509Name("CN=" + hostname));
    certGenerator.SetIssuerDN(new X509Name("CN=" + hostname));

    // Genera un número de serie aleatorio para el certificado.
    BigInteger serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
    certGenerator.SetSerialNumber(serialNumber);

    // Establece las fechas de validez del certificado desde ahora hasta 10 años después.
    certGenerator.SetNotBefore(DateTime.UtcNow.Date);
    certGenerator.SetNotAfter(DateTime.UtcNow.Date.AddYears(10));

    // Configura los parámetros para la generación de claves RSA de 2048 bits.
    KeyGenerationParameters keyGenerationParameters = new KeyGenerationParameters(random, 2048);

    // Inicializa un generador de pares de claves RSA.
    RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
    keyPairGenerator.Init(keyGenerationParameters);
    AsymmetricCipherKeyPair keyPair = keyPairGenerator.GenerateKeyPair();

    // Establece la clave pública del certificado como la clave pública del par de claves generado.
    certGenerator.SetPublicKey(keyPair.Public);

    // Crea una fábrica de firmas ASN.1 utilizando el algoritmo SHA256WithRSA y la clave privada generada.
    var signatureFactory = new Asn1SignatureFactory("SHA256WithRSA", keyPair.Private, random);

    // Genera el certificado utilizando el generador de certificados y la fábrica de firmas.
    var cert = certGenerator.Generate(signatureFactory);

    // Crea un almacén PKCS#12 para almacenar el certificado y la clave privada.
    Pkcs12Store store = new Pkcs12StoreBuilder().Build();

    // Agrega el certificado al almacén con un alias 'cert'.
    var certificateEntry = new X509CertificateEntry(cert);
    store.SetCertificateEntry("cert", certificateEntry);

    // Agrega la clave privada al almacén con el mismo alias y asocia el certificado con la clave privada.
    store.SetKeyEntry("cert", new AsymmetricKeyEntry(keyPair.Private), new[] { certificateEntry });

    // Guarda el almacén en un flujo de memoria utilizando una contraseña vacía.
    var stream = new MemoryStream();
    store.Save(stream, "".ToCharArray(), random);

    // Crea y devuelve un objeto X509Certificate2 utilizando los datos del flujo de memoria.
    return new X509Certificate2(
        stream.ToArray(), "",
        X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
}

// Método principal que crea un certificado autofirmado y lo guarda como archivos .pfx y .cer.
public static void Main(string[] args)
{
    X509Certificate2 cert = null;
    
    // Obtiene las rutas completas de los archivos de certificado .pfx y .cer.
    string pfxPath = Path.GetFullPath("selfsigned-certificate.pfx");
    string cerPath = Path.GetFullPath("selfsigned-certificate.cer");

    Console.WriteLine($"CREANDO CERTIFICADO:{pfxPath}");

    // Crea un certificado autofirmado utilizando 'localhost' como nombre del certificado.
    cert = CreateSelfSignedCertificate("localhost");

    // Exporta el certificado como archivos .pfx y .cer.
    File.WriteAllBytes(pfxPath, cert.Export(X509ContentType.Pkcs12));
    File.WriteAllBytes(cerPath, cert.Export(X509ContentType.Cert));
    
    Console.WriteLine("CERTIFICADO CREADO");
}

}