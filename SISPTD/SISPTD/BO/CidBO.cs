using SISPTD.Models;

namespace SISPTD.BO
{
    public class CidBO:CrudComumEntity<Cid, long>
    {
        public CidBO(dbSISPTD contexto)
            :base(contexto)
        {

        }


    }
}