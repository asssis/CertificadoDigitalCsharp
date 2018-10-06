using CertificadoDigitalCSharp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace CertificadoDigitalCSharp.bo
{
    public class CertificadoBO
    {
        public Certificado AutenticarUsuario(byte[] RawData)
        {
            Certificado obj = PegarCertificado(RawData);
            if (obj.Proposito != PropositoUso.Valido)
                return null;
            if (obj.Validade != Validade.Valido)
                return null;
            if (obj.Revogacao != Revogacao.Valido)
                return null;
            if (obj.Caminho != CaminhoCertificado.Valido)
                return null;
            return obj;

        }
        public Certificado PegarCertificado(byte[] RawData)
        {
            Certificado obj = new Certificado();
            obj.TipoCertificado = RecuperarTipoCertificado(RawData);
            if (obj.TipoCertificado == TipoCertificado.eCPF)
                obj.eCpfCnpj = RecuperarCPF(RawData);
            else
                obj.eCpfCnpj = RecuperarCNPJ(RawData);
            obj.Nome = RecuperarNome(RawData);
            obj.Validade = VerificarValidade(RawData);
            obj.Revogacao = VerificarRevogacao(RawData);
            obj.Caminho = ValidarCaminhoDeCertificacao(RawData);
            obj.Dominio = RecuperarWinLogon(RawData);
            obj.ChavePublica = RecuperarChavePublica(RawData);
            obj.Certificado = RawData;
            return obj;
        }
        private TipoCertificado RecuperarTipoCertificado(byte[] RawData)
        {
            System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                       new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);

            // OID "2.5.29.17" - OtherName
            X509Extension extension = certificado.Extensions["2.5.29.17"];
            if (extension != null)
            {
                string otherName = extension.Format(false);
                if (!string.IsNullOrEmpty(otherName) && (otherName.Contains("2.16.76.1.3.1")))
                    return TipoCertificado.eCPF;
                else if (!string.IsNullOrEmpty(otherName) && (otherName.Contains("2.16.76.1.3.3")))
                    return TipoCertificado.eCNPJ;
            }

            return TipoCertificado.Null;
        }
        private string RecuperarCPF(byte[] RawData)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                       new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);


                // OID "2.5.29.17" - OtherName
                X509Extension extension = certificado.Extensions["2.5.29.17"];
                if (extension != null)
                {
                    string otherName = extension.Format(false);
                    if (!string.IsNullOrEmpty(otherName) &&
                        (otherName.Contains("2.16.76.1.3.1") ||
                        otherName.Contains("2.16.76.1.3.4")))
                    {
                        int offset;
                        if (otherName.Contains("2.16.76.1.3.1"))
                        {
                            offset = otherName.IndexOf("2.16.76.1.3.1=");
                        }
                        else
                        {
                            offset = otherName.IndexOf("2.16.76.1.3.4=");
                        }
                        offset += 14;
                        int endPos = otherName.IndexOf(",", offset);
                        string cpfEncValue;
                        if (endPos == -1)
                        {
                            cpfEncValue = otherName.Substring(offset);
                        }
                        else
                        {
                            cpfEncValue = otherName.Substring(offset, endPos - offset);
                        }
                        List<byte> byteArrayCpf = new List<byte>();
                        string[] tokens = cpfEncValue.Split(' ');
                        for (int i = 10; i < 21; i++)
                        {
                            byte value = byte.Parse(tokens[i], System.Globalization.NumberStyles.HexNumber);
                            // Valores em hexadecimal / decimal para os caracteres
                            // -: 2d / 45
                            // .: 2e / 46
                            // /: 2f / 47
                            // 0-9: 30-39 / 48-57
                            if ((value >= 48) &&
                                (value <= 57))
                            {
                                byteArrayCpf.Add(value);
                            }
                        }
                        string valor = ASCIIEncoding.ASCII.GetString(byteArrayCpf.ToArray());
                        if (byteArrayCpf.Count == 11 && Funcoes.ValidarCPF(valor))
                        {
                            string cpf = ASCIIEncoding.ASCII.GetString(byteArrayCpf.ToArray());
                            return cpf;
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        private string RecuperarCNPJ(byte[] RawData)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                        new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);

                // OID "2.5.29.17" - OtherName
                X509Extension extension = certificado.Extensions["2.5.29.17"];
                if (extension != null)
                {
                    string otherName = extension.Format(false);
                    if (!string.IsNullOrEmpty(otherName) &&
                        (otherName.Contains("2.16.76.1.3.3")))
                    {
                        int offset = otherName.IndexOf("2.16.76.1.3.3=");
                        offset += 14;
                        int endPos = otherName.IndexOf(",", offset);
                        string cnpjEncValue = otherName.Substring(offset, endPos - offset);
                        List<byte> byteArrayCnpj = new List<byte>();
                        foreach (string token in cnpjEncValue.Split(' '))
                        {
                            byte value = byte.Parse(token, System.Globalization.NumberStyles.HexNumber);
                            // Valores em hexadecimal / decimal para os caracteres
                            // -: 2d / 45
                            // .: 2e / 46
                            // /: 2f / 47
                            // 0-9: 30-39 / 48-57
                            if ((value >= 48) &&
                                (value <= 57))
                            {
                                byteArrayCnpj.Add(value);
                            }
                        }
                        string valor = ASCIIEncoding.ASCII.GetString(byteArrayCnpj.ToArray());
                        if (byteArrayCnpj.Count == 14 && Funcoes.ValidarCNPJ(valor))
                        {
                            return ASCIIEncoding.ASCII.GetString(byteArrayCnpj.ToArray());
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }

            catch (Exception ex)
            {
                return "";
            }

        }
        private Revogacao VerificarRevogacao(byte[] RawData)
        {
            bool revogado = false;
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                            new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);

                X509Chain chain = new X509Chain();

                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(1000);
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                chain.ChainPolicy.VerificationTime = DateTime.Now;

                chain.Build(certificado);


                foreach (X509ChainElement element in chain.ChainElements)
                {
                    if (chain.ChainStatus.Length > 1)
                    {
                        for (int index = 0; index < element.ChainElementStatus.Length; index++)
                        {
                            revogado = true;
                        }
                    }
                }

                if (revogado)
                    return Revogacao.Revogado;
                else
                    return Revogacao.Valido;
            }
            catch (Exception)
            {
                return Revogacao.Null;
            }

        }
        private Validade VerificarValidade(byte[] RawData)
        {

            bool valido = false;

            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                         new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);

                X509Chain ch = new X509Chain();
                ch.Build(certificado);

                DateTime inicioValidade = DateTime.Parse(certificado.GetEffectiveDateString());
                DateTime terminoValidade = DateTime.Parse(certificado.GetExpirationDateString());

                valido = inicioValidade <= DateTime.Now && terminoValidade >= DateTime.Now;

                if (!valido)
                    return Validade.Expirado;
                else
                    return Validade.Valido;

            }

            catch (Exception)
            {
                return Validade.Null;
            }

        }
        private string RecuperarChavePublica(byte[] RawData)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                     new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);
                return certificado.ToString();
            }
            catch
            {
                return "";
            }
        }
        private ChavePrivada VerificaExistenciaChavePrivada(byte[] RawData)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                           new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);
                bool possuiChavePrivada = certificado.HasPrivateKey;
                if (possuiChavePrivada)
                    return ChavePrivada.Sim;
                else
                    return ChavePrivada.Nao;

            }
            catch (Exception ex)
            {
                return ChavePrivada.Null;
            }
        }
        private PropositoUso ValidarPropositoDeUso(byte[] RawData)
        {
            bool valido = false;

            try
            {
                string keyUsage = string.Empty;

                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                           new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);


                X509Chain ch = new X509Chain();
                ch.Build(certificado);

                X509Certificate2Collection collection = new X509Certificate2Collection();
                collection.Add(certificado);


                for (int i = 0; i < collection.Count; i++)
                {
                    foreach (X509Extension extension in collection[i].Extensions)
                    {

                        if (extension.Oid.FriendlyName == "Key Usage")
                        {
                            X509KeyUsageExtension ext = (X509KeyUsageExtension)extension;
                            keyUsage = ext.KeyUsages.ToString();
                            if (keyUsage.Contains("DigitalSignature") && keyUsage.Contains("NonRepudiation"))
                                valido = true;
                        }
                    }
                }

                if (!valido)
                    return PropositoUso.Diverso;
                else
                    return PropositoUso.Valido;


            }
            catch (Exception)
            {
                return PropositoUso.Null;
            }
        }
        private string RecuperarWinLogon(byte[] RawData)
        {
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                     new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);


                string upn = certificado.GetNameInfo(X509NameType.UpnName, false).Trim();
                int pos = upn.IndexOf("@");
                string dominio = upn.Substring(pos + 1);
                return dominio;
            }

            catch (Exception)
            {
                return "";
            }

        }
        private string RecuperarNome(byte[] RawData)
        {
            string nm;
            int pos;
            string nome;

            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                        new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);

                string[] tokens = certificado.SubjectName.Name.Split(',');
                foreach (string token in tokens)
                {
                    if (token.Contains("CN="))
                    {
                        nm = token.Replace("CN=", "");
                        pos = (nm.IndexOf(":") > 0 ? nm.IndexOf(":") : nm.Length);
                        nome = nm.Substring(0, pos);

                    }
                }

                nm = certificado.SubjectName.Name.Split(',')[0].Replace("CN=", "");
                pos = (nm.IndexOf(":") > 0 ? nm.IndexOf(":") : nm.Length);
                nome = nm.Substring(0, pos);
                return nome;
            }
            catch (Exception)
            {
                return "";
            }
        }
        private CaminhoCertificado ValidarCaminhoDeCertificacao(byte[] RawData)
        {
            bool valido = false;

            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificado =
                         new System.Security.Cryptography.X509Certificates.X509Certificate2(RawData);

                X509Chain cadeiaCertificacao = new X509Chain();

                cadeiaCertificacao.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                cadeiaCertificacao.ChainPolicy.RevocationFlag = X509RevocationFlag.EndCertificateOnly;
                cadeiaCertificacao.Build(certificado);
                X509Certificate2 certificadoRaiz =
                    cadeiaCertificacao.ChainElements[(cadeiaCertificacao.ChainElements.Count - 1)].Certificate;

                foreach (var thumbprint in Funcoes.THUMBPRINTS)
                {
                    if (certificadoRaiz.Thumbprint.Equals(thumbprint))
                    {
                        valido = true;
                        break;
                    }
                }

                if (!valido)
                    return CaminhoCertificado.Invalido;
                else
                    return CaminhoCertificado.Valido;
            }
            catch (Exception)
            {
                return CaminhoCertificado.Null;
            }
        }
    }
}