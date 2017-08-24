using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leopard.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string WhatsAppURL { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedByEmail { get; set; }

        public bool Approved { get; set; }

        public bool IsActive { get; set; }

        public Group()
        {
            CreatedDate = DateTime.Today;
            Approved = false;
            IsActive = true;
        }
    }
}