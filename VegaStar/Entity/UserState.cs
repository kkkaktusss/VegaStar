using System;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace VegaStar.Entity
{
	public class UserState
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserStateId { get; set; }
        public string Code { get; set; } = " ";
        public string Description { get; set; } = " ";

    }
}

