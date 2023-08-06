using PrjAPIPetCare.BancoDeDados;
using PrjAPIPetCare.Modelos;
using PrjAPIPetCare.Resultados;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrjAPIPetCare.Controladeres
{
    public class CuidadorControl
    {
        public static ListResult PuxarServicos(int idCuidador)
        {
            ListResult result = new ListResult();

            SqlDataReader reader = ConexaoBD.Instance.ExecuteReader("exec usp_servSolicCuidador @cuidador", new SqlParameter[] { new SqlParameter("@cuidador", idCuidador) });

            while(reader.Read())
            {
                ServicoModelo modelo = new ServicoModelo();

                modelo.idServ = reader.GetInt32(0);
                modelo.dataIni = reader.GetDateTime(1);
                modelo.dataFin = reader.GetDateTime(2);
                modelo.idDono = reader.GetInt32(3);
                modelo.idCuidador = reader.GetInt32(4);
                modelo.idStatus = reader.GetInt32(5);
                modelo.donoNome = reader.GetString(6);
                modelo.tipoServ = reader.GetString(7);

                result.Resultados.Add(modelo);
            }

            reader.Close(); 


            return result;
        }
    }
}