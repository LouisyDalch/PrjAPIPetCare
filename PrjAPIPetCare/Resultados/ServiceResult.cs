using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjAPIPetCare.Resultados
{
    public class ServiceResult
    {
        public bool success = true;

        public ServiceResult(bool success = true)
        {
            this.success = success;
        }


    }
}