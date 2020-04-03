namespace Desulaid.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleNam { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Registration { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string GroupId { get; set; }
    }
}
