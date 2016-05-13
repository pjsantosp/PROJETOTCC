using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SISPTD.Models
{
    public enum Situacao
    {
        Em_Tramitação = 0,
        Autorizado = 1,
        Não_Autorizado = 2,
        Pendente = 3,
        Agendado = 4,

    }
}