using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leopard.Models
{
    public class GroupViewModel
    {
        public Group Grupo { get; set; }

        public IEnumerable <Group> Grupos { get; set; }
    }
}