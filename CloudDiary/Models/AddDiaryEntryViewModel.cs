using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudDiary.Models
{
    public class AddDiaryEntryViewModel
    {
        [Required]
        [StringLength(100)]
        [DisplayName("New Entry")]
        public string Text { get; set; }
    }
}
