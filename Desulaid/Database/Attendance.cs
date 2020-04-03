namespace Desulaid.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendance")]
    public partial class Attendance
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }

        [StringLength(50)]
        public string Reason { get; set; }

        [StringLength(50)]
        public string Date { get; set; }

        [StringLength(50)]
        public string Time { get; set; }
    }
}
