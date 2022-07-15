using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemCustomerData.Entities
{
    public class CustomerMaster
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]

        public string? Customer_FName { get; set; }

        public string? Customer_LName { get; set; }

        public int Customer_Phone { get; set; }

        [Required]
        public string? Customer_Email { get; set; }

    }
}
