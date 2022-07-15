using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemCustomerData.Entities
{
    public class ItemMaster
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public string? Item_Name { get; set; }
        public int Item_Rate { get; set; }

        public int Item_Qty { get; set; }

    }
}
