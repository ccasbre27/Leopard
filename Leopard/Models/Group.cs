using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Leopard.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        private string _name;

        [DisplayName("Nombre")]
        [Required]
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); ; }
        }


        public string Description { get; set; }

       
        private string _whatsAppURL;

        [DisplayName("WhatsApp")]
        [Required]
        [RegularExpression(@"^https://chat.whatsapp.com/.+", ErrorMessage = "Url no válida")]
        public string WhatsAppURL
        {
            get { return _whatsAppURL; }
            set { _whatsAppURL = value.Trim(); }
        }

        [DisplayName("Código")]
        [Required]
        public int Code { get; set; }

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