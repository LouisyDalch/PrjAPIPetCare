using PrjAPIPetCare.BancoDeDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using PrjAPIPetCare.Resultados;
using Newtonsoft.Json;
using System.Text;

namespace PrjAPIPetCare
{
    public partial class LoginFW : System.Web.UI.Page
    {
        static Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Request.QueryString["username"];
            string password = Request.QueryString["password"];

            if (username == null || password == null)
            {
                Response.Write(JsonConvert.SerializeObject(new ServiceResult(false)));
                return;
            }

            var reader = ConexaoBD.Instance.ExecuteReader("exec usp_loginCuidador @username, @password",
                new SqlParameter[] { 
                    new SqlParameter("@username", username), 
                    new SqlParameter("@password", password) 
                }
            );

            LoginResult result = new LoginResult();

            if (reader.Read())
            {
                int idCuidador = reader.GetInt32(0);

                string token = "";

                token += Convert.ToBase64String(Encoding.UTF8.GetBytes(idCuidador.ToString()));
                token += ".";
                token += Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
                token += ".";
                token += Convert.ToBase64String(Encoding.UTF8.GetBytes(random.Next(int.MaxValue).ToString()));

                reader.Close();

                ConexaoBD.Instance.ExecuteUpdate("INSERT INTO TokenCuidador(token, id_cuidador) VALUES (@token, @id_cuidador)",
                    new SqlParameter[] {
                        new SqlParameter("@token", token),
                        new SqlParameter("@id_cuidador", idCuidador)
                    }
                );

                result.token = token;
            }else
            {
                reader.Close();

                result.success = false;
                result.token = null;
            }

            string json = JsonConvert.SerializeObject(result);

            Response.Write(json);
        }
    }
}