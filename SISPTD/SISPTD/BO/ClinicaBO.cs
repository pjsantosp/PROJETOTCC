using SISPTD.Models;

namespace SISPTD.BO
{
    public class ClinicaBO: CrudComumEntity<Clinica, long>
    {
        public ClinicaBO(dbSISPTD contexto)
            :base(contexto)
        {

        }
      
    }
}