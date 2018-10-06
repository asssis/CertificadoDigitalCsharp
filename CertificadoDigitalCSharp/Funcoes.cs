using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CertificadoDigitalCSharp
{
    public class Funcoes
    {
        public static readonly string EVENTSOURCE = "DEC";
        public static readonly string[] THUMBPRINTS = new[]
        {
            "8EFDCABC93E61E925D4D1DED181A4320A467A139",
            "BC3E5EA55CBF337B48913F107C94026314543D7B",
            "8900EA5E422C0F277B5FAA9298389EFA199D573B",
            "DAB9855C79C2AA58B554B564051FB93183FE75A8",
            "42B22C5C740107BE9BFF55333BEE29BB5D91BF06",
            "705D2B4565C7047A540694A79AF7ABB842BDC161",
            "A9822E6C6933C63C148C2DCAA44A5CF1AAD2C42E",
            // novos em 07/09/2012
            "33FDB3581DC5A8A9F7D25C0C5165F0A96EAAF5E9",
            "583854E594EC5AB4A00580126144843D135CF49D",
            "FA9743AE604E27021E2FAE1198E0233CA6A55EAE",
            // novos em 22/04/2013
            "39F40FD3132BCF73672E04ED4C977FCCA178C245",
            "AF9C7E13EE62CDE76B25AB34B63141B00AF6DE1B",
            // novos em 04/12/2013
            "B914DA394D5B387B56405F9FB61BABCD0FB5E412",
            "665CEB10F660C74FE88DF6B980ABAF7F02131F55",
            // novos em 25/06/2015
            "19810756DF9F39B64A736C249F472492AAA175C7", // ipvaws.intra.fazenda.sp.gov.br
            "‎C87F82A0E137176CF283ADB0E9BE9EB6298D0422", // sefaznet10
            "‎EE5A24AE62A95C346811A48DCC86E43E22B913C6", // siafemhom.intra.fazenda.sp.gov.br
            "‎A1AEBD6A98FF300C9CA989E1DB0CBC9762C2426C", // sintegrasp.sede.fazenda.sp.gov.br
            "‎7901D6F85A8E1F93062EEA008990936E363B85A0", // srvcs28.intra.fazenda.sp.gov.br
            "‎FB082F663D84E1F3AE01153C71EB8CA6089DC2AE",  // webdesenv.intra.fazenda.sp.gov.br
            "‎D9C229E56A8847DB9F5582AB10A59CFEE4573BFB", // webservices.intra.fazenda.sp.gov.br
            "‎76BBF38F448E35B3E7E65E5F151E4686CC19F973", // webservicesdes.intra.fazenda.sp.gov.br
            "‎A020DCB42B30C38A3F0418CE5551DA3CD988C1E1", // webserviceshml2.intra.fazenda.sp.gov.br
            "‎F5BCEBBBCF208D0BBE8F9CE4DA111ACB6BF880D5", // webservicespre.intra.fazenda.sp.gov.br
            "‎1AA2E6C37358E2844A00FE2920E744A5812F6E97", // (TIBCO – SpotFire) srv10241.intra.fazenda.sp.gov.br
            "‎CA9DC4A59C02C26CA8A64C2FFB39AA72E9D9516B", // (TIBCO SPOTFIRE WEB PLAYER) srv11006.intra.fazenda.sp.gov.br
            "‎EA2F3976762AC73BB594D91E4D574BEF6BEC82CA", // (TIBCO SPOTFIRE (DEV) srv10551.intra.fazenda.sp.gov.br
            "‎597F3262F7C3D618949B7878FE718D1CB2A46BBE", // (TIBCO - Iprocess / BW) srv10852.intra.fazenda.sp.gov.br
            "‎E00ACAC7BCAD7FF62F6143ADE653D47B7A6A2CAB", // (Intranet - HML) SRV10364.intra.fazenda.sp.gov.br
            "‎B0D5A89031B82573E93C4A277B7BCC2D64B2E887", // (Desenvolvimento Web) SRV10437.intra.fazenda.sp.gov.br
            "‎ACA522B6F266B5E665D32F443A16A5183894CF2D", // (Testes - APP) SRV10441.intra.fazenda.sp.gov.br
            "‎6A79AEA4AE83C680A0934551E18FE747E7663D42", // (Aplicações) SRV10444.intra.fazenda.sp.gov.br
            "‎CB2BE5A157EA74C828B28E3B8C0CBAAFB1C4F0A5", // (KMS) srv11032.intra.fazenda.sp.gov.br
            "‎4F82B322B7AAAED59865EBACC2E0429B294CBFD2", // (KMS) srv11106.intra.fazenda.sp.gov.br
            "‎217A3EF0BE12364222D43C2251000C069B4C0682", // (SPO Sefaz Identity Aplicacao) srv72207.intra.fazenda.sp.gov.br 
            "‎74EE7BECEADF668FAEE7E85671E8254A51D29BE3", // (SPO Sefaz Identity Aplicacao) srv72208.intra.fazenda.sp.gov.br 
            "‎F55F6CEF1FA26F8AA3A54BF3297B9556AA6FB73D", // (Web Server CAS DESEN) srv72066.intra.fazenda.sp.gov.br
            "‎948ABFE89CAF230D6CDE51FD4B15AAE8F43E5F24", // (Web Server CAS DESEN) srv72067.intra.fazenda.sp.gov.br
            "‎68DAE12A34CDCA8C3A480ACA93ED2F15D47F32BE" // (Report Server 2008 CAS DESEN) srv72069.intra.fazenda.sp.gov.br
        };
        public static bool ValidarCPF(string CPF)
        {
            if (string.IsNullOrEmpty(CPF))
            {
                return false;
            }

            System.Text.RegularExpressions.Regex regEx =
                new System.Text.RegularExpressions.Regex(
                @"^\d{11}|((\d{3}.){2}\d{3}-\d{2})$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                System.Text.RegularExpressions.RegexOptions.Singleline);

            if (!regEx.IsMatch(CPF))
            {
                return false;
            }
            else
            {
                string fmtCpf = CPF.Replace(".", "").Replace("-", "");
                if (fmtCpf.Equals("00000000000"))
                {
                    return false;
                }
                int total = 0;
                int digitoVerificador = 0;
                total += (int.Parse(fmtCpf.Substring(0, 1)) * 10);
                total += (int.Parse(fmtCpf.Substring(1, 1)) * 9);
                total += (int.Parse(fmtCpf.Substring(2, 1)) * 8);
                total += (int.Parse(fmtCpf.Substring(3, 1)) * 7);
                total += (int.Parse(fmtCpf.Substring(4, 1)) * 6);
                total += (int.Parse(fmtCpf.Substring(5, 1)) * 5);
                total += (int.Parse(fmtCpf.Substring(6, 1)) * 4);
                total += (int.Parse(fmtCpf.Substring(7, 1)) * 3);
                total += (int.Parse(fmtCpf.Substring(8, 1)) * 2);
                digitoVerificador = total % 11;
                if (digitoVerificador < 2)
                {
                    digitoVerificador = 0;
                }
                else
                {
                    digitoVerificador = 11 - digitoVerificador;
                }

                if (int.Parse(fmtCpf.Substring(9, 1)) != digitoVerificador)
                {
                    return false;
                }
                else
                {
                    total = 0;
                    digitoVerificador = 0;
                    total += (int.Parse(fmtCpf.Substring(0, 1)) * 11);
                    total += (int.Parse(fmtCpf.Substring(1, 1)) * 10);
                    total += (int.Parse(fmtCpf.Substring(2, 1)) * 9);
                    total += (int.Parse(fmtCpf.Substring(3, 1)) * 8);
                    total += (int.Parse(fmtCpf.Substring(4, 1)) * 7);
                    total += (int.Parse(fmtCpf.Substring(5, 1)) * 6);
                    total += (int.Parse(fmtCpf.Substring(6, 1)) * 5);
                    total += (int.Parse(fmtCpf.Substring(7, 1)) * 4);
                    total += (int.Parse(fmtCpf.Substring(8, 1)) * 3);
                    total += (int.Parse(fmtCpf.Substring(9, 1)) * 2);
                    digitoVerificador = total % 11;
                    if (digitoVerificador < 2)
                    {
                        digitoVerificador = 0;
                    }
                    else
                    {
                        digitoVerificador = 11 - digitoVerificador;
                    }
                    if (int.Parse(fmtCpf.Substring(10, 1)) != digitoVerificador)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
        public static bool ValidarCNPJ(string CNPJ)
        {
            if (string.IsNullOrEmpty(CNPJ))
            {
                return false;
            }

            System.Text.RegularExpressions.Regex regEx =
                new System.Text.RegularExpressions.Regex(
                @"^\d{14}|(\d{2}(.\d{3}){2}/\d{4}-\d{2})$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                System.Text.RegularExpressions.RegexOptions.Singleline);

            if (!regEx.IsMatch(CNPJ))
            {
                return false;
            }
            else
            {
                string fmtCnpj = CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
                if (fmtCnpj.Equals("00000000000000"))
                {
                    return false;
                }
                int total = 0;
                int digitoVerificador = 0;
                total += (int.Parse(fmtCnpj.Substring(0, 1)) * 5);
                total += (int.Parse(fmtCnpj.Substring(1, 1)) * 4);
                total += (int.Parse(fmtCnpj.Substring(2, 1)) * 3);
                total += (int.Parse(fmtCnpj.Substring(3, 1)) * 2);
                total += (int.Parse(fmtCnpj.Substring(4, 1)) * 9);
                total += (int.Parse(fmtCnpj.Substring(5, 1)) * 8);
                total += (int.Parse(fmtCnpj.Substring(6, 1)) * 7);
                total += (int.Parse(fmtCnpj.Substring(7, 1)) * 6);
                total += (int.Parse(fmtCnpj.Substring(8, 1)) * 5);
                total += (int.Parse(fmtCnpj.Substring(9, 1)) * 4);
                total += (int.Parse(fmtCnpj.Substring(10, 1)) * 3);
                total += (int.Parse(fmtCnpj.Substring(11, 1)) * 2);
                digitoVerificador = total % 11;
                if (digitoVerificador < 2)
                {
                    digitoVerificador = 0;
                }
                else
                {
                    digitoVerificador = 11 - digitoVerificador;
                }

                if (int.Parse(fmtCnpj.Substring(12, 1)) != digitoVerificador)
                {
                    return false;
                }
                else
                {
                    total = 0;
                    digitoVerificador = 0;
                    total += (int.Parse(fmtCnpj.Substring(0, 1)) * 6);
                    total += (int.Parse(fmtCnpj.Substring(1, 1)) * 5);
                    total += (int.Parse(fmtCnpj.Substring(2, 1)) * 4);
                    total += (int.Parse(fmtCnpj.Substring(3, 1)) * 3);
                    total += (int.Parse(fmtCnpj.Substring(4, 1)) * 2);
                    total += (int.Parse(fmtCnpj.Substring(5, 1)) * 9);
                    total += (int.Parse(fmtCnpj.Substring(6, 1)) * 8);
                    total += (int.Parse(fmtCnpj.Substring(7, 1)) * 7);
                    total += (int.Parse(fmtCnpj.Substring(8, 1)) * 6);
                    total += (int.Parse(fmtCnpj.Substring(9, 1)) * 5);
                    total += (int.Parse(fmtCnpj.Substring(10, 1)) * 4);
                    total += (int.Parse(fmtCnpj.Substring(11, 1)) * 3);
                    total += (int.Parse(fmtCnpj.Substring(12, 1)) * 2);
                    digitoVerificador = total % 11;
                    if (digitoVerificador < 2)
                    {
                        digitoVerificador = 0;
                    }
                    else
                    {
                        digitoVerificador = 11 - digitoVerificador;
                    }
                    if (int.Parse(fmtCnpj.Substring(13, 1)) != digitoVerificador)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

    }
}