using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VegaStar.Entity
{
	public class UserGroup
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserGroupId { get; set; }
        public string Code { get; set; } = " ";
        public string Description { get; set; } = " ";

        //public ICollection<User>? Users { get; set; }


    }
}

