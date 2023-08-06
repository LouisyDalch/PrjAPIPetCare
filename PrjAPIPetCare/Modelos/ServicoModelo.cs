using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjAPIPetCare.Modelos
{
    public class ServicoModelo
    {
        public int idServ;
        public DateTime dataIni;
        public DateTime dataFin;
        public int idDono;
        public int idCuidador;
        public int idStatus;
        public string donoNome;
        public string tipoServ;

    }
}