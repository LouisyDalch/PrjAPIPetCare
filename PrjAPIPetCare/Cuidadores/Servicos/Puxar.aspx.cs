using Newtonsoft.Json;
using PrjAPIPetCare.Controladeres;
using PrjAPIPetCare.Middlewares;
using PrjAPIPetCare.Modelos;
using PrjAPIPetCare.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrjAPIPetCare.Cuidadores.Servicos
{
    public partial class Puxar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuidadorModelo cuidadorAtual = AuthMiddleware.VerificarTokenDoCuidador(Request.Headers["Authorization"]);
            if(cuidadorAtual == null)
            {
                Response.Write(JsonConvert.SerializeObject(new ServiceResult(false)));
                return;
            }

            ListResult result = CuidadorControl.PuxarServicos(cuidadorAtual.id_cuidador);

            Response.Write(JsonConvert.SerializeObject(result));
        }
    }
}