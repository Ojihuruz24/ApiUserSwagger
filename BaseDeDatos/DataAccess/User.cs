using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BaseDeDatos.DataAccess
{
    [Table("USUARIO")]
    public class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("IDENTIFICACION")]
        public int Identity { get; set; }

        [Required]
        [StringLength(50)]
        [Column("NOMBRE")]
        public string FirtName { get; set; }

        [Required]
        [StringLength(50)]
        [Column("APELLIDOS")]
        public string LastName { get; set; }

        [Required]
        [Column("CELULAR")]
        public int Cel { get; set; }

        [Required]
        [Column("CORREO")]
        public string Email { get; set; }

        [Required]
        [Column("DIRECCION")]
        public string Direction { get; set; }

    }
}
