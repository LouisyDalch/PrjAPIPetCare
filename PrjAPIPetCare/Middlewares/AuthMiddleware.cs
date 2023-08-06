using PrjAPIPetCare.BancoDeDados;
using PrjAPIPetCare.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PrjAPIPetCare.Middlewares
{
    public class AuthMiddleware
    {
        public static CuidadorModelo VerificarTokenDoCuidador(string token)
        {
            try
            {
                SqlDataReader reader = ConexaoBD.Instance.ExecuteReader("EXEC usp_validarTokenCuidador @token",
                new SqlParameter[] {
                    new SqlParameter("@token", token)
                }
            );

                CuidadorModelo model = null;

                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        //não é válido
                    }
                    else
                    {
                        // é válido
                        model = new CuidadorModelo
                        {
                            id_cuidador = reader.GetInt32(0),
                            nome = reader.GetString(1),
                            email = reader.GetString(2),
                            datanasce = reader.GetDateTime(3),
                            telefone = reader.GetString(4),
                            cpf = reader.GetString(5),
                            genero = reader.GetString(6),
                            senha = reader.GetString(7),
                            especializacao = reader.GetString(8),
                            tempoexper = reader.GetString(9)
                        };
                    }
                }

                reader.Close();

                return model;
            }catch(Exception)
            {
                return null;
            }
        }
    }
}