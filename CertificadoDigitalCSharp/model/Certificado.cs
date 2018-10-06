using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CertificadoDigitalCSharp.model
{
    public class Certificado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string eCpfCnpj { get; set; }
        public string Dominio { get; set; }
        public string ChavePublica { get; set; }
        public TipoCertificado TipoCertificado { get; set; }
        public Validade Validade { get; set; }
        public CaminhoCertificado Caminho { get; set; }
        public Revogacao Revogacao { get; set; }
        public byte[] Certificado { get; set; }
        public PropositoUso Proposito { get; set; }
    }
    public enum TipoCertificado : int { Null = 0, eCPF = 1, eCNPJ = 2 }
    public enum Validade : int { Null = 0, Valido = 1, Expirado = 2 }
    public enum Revogacao : int { Null = 0, Valido = 1, Revogado = 2 }
    public enum ChavePrivada : int { Null = 0, Sim = 1, Nao = 2 }
    public enum PropositoUso : int { Null = 0, Valido = 1, Diverso = 2 }
    public enum CaminhoCertificado : int { Null = 0, Valido = 1, Invalido = 2 }
}