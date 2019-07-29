using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDiary.Models
{
    public class DiaryEntry
    {
        //global unique identifier as primary key
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        [Required]
        [StringLength(100)]
        public string Text { get; set; }

        public string UserId { get; set; }

        //no applicationUser class?
        public IdentityUser User { get; set; }
    }
}
