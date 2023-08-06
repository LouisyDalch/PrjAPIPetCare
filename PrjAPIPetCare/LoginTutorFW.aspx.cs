using Newtonsoft.Json;
using PrjAPIPetCare.BancoDeDados;
using PrjAPIPetCare.Resultados;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrjAPIPetCare
{
    public partial class LoginTutorFW : System.Web.UI.Page
    {

        static Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            string usuario = Request.QueryString["username"];
            string senha = Request.QueryString["password"];

            if (usuario == null || senha == null)
            {
                Response.Write(JsonConvert.SerializeObject(new ServiceResult(false)));
                return;
            }

            var reader = ConexaoBD.Instance.ExecuteReader("exec usp_loginTutores @username, @password",
                new SqlParameter[] {
                    new SqlParameter("@username", usuario),
                    new SqlParameter("@password", senha)
                }
            );

            LoginResult result = new LoginResult();

            if (reader.Read())
            {
                int idDono = reader.GetInt32(0);

                string token = "";

                token += Convert.ToBase64String(Encoding.UTF8.GetBytes(idDono.ToString()));
                token += ".";
                token += Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.ToString()));
                token += ".";
                token += Convert.ToBase64String(Encoding.UTF8.GetBytes(random.Next(int.MaxValue).ToString()));

                reader.Close();

                ConexaoBD.Instance.ExecuteUpdate("INSERT INTO TokenTutor(token, id_dono) VALUES (@token, @id_dono)",
                    new SqlParameter[] {
                        new SqlParameter("@token", token),
                        new SqlParameter("@id_dono", idDono)
                    }
                );

                result.token = token;
            }
            else
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