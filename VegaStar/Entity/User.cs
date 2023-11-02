using System;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

namespace VegaStar.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Login { get; set; } = " ";
        public string Password { get; set; } = " ";
        public DateTime CreatedDate { get; set; }

        // Внешний ключ для UserGroup
        [JsonIgnore]
        public int UserGroupId { get; set; }

        public required UserGroup UserGroup { get; set; } 

        // Внешний ключ для UserState
        [JsonIgnore]
        public int UserStateId { get; set; }
 
        public UserState? UserState { get; set; }
	}
}

