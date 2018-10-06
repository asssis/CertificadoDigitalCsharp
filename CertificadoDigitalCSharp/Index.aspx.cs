using CertificadoDigitalCSharp.bo;
using CertificadoDigitalCSharp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CertificadoDigitalCSharp
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
     
            try
            {

                HttpContext context = HttpContext.Current;

                byte[] RawData = context.Request.ClientCertificate.Certificate;
                X509Certificate2 xc = new X509Certificate2();
                HttpClientCertificate cert = Request.ClientCertificate;
                if (cert.IsPresent)
                {
                    X509Certificate2 certificado =  new X509Certificate2(RawData);
                    Certificado objCertificado = new CertificadoBO().PegarCertificado(RawData);
                    txtCpfCnpj.Text = objCertificado.eCpfCnpj;
                    txtNome.Text = objCertificado.Nome;
                    txtTipo.Text = objCertificado.TipoCertificado.ToString();

                }
                else
                {
                    txtTipo.Text = "";
                    txtNome.Text = "";
                    txtCpfCnpj.Text = "";

                }
            }
            catch (Exception)
            {
                txtTipo.Text = "";
                txtNome.Text = "";
                txtCpfCnpj.Text = "";

            }
        }
    }
}