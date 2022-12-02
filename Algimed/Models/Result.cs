using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algimed.Models
{
    [Table("Results")]
    public class Result
    {
        [Key]
        public string Cells { get; set; }
        public double? Value { get; set; }

        public override string ToString()
        {
            return this.Cells + " | " + this.Value;
        }
    }
}
