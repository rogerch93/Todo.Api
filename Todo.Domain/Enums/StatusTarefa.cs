using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Enums
{
    public enum StatusTarefa
    {
        [System.ComponentModel.Description("Pendente")]
        Pendente = 1,

        [System.ComponentModel.Description("Em Andamento")]
        EmAndamento = 2,

        [System.ComponentModel.Description("Concluído")]
        Concluido = 3
    }
}
