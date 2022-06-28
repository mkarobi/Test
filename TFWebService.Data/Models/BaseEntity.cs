using System;
using System.ComponentModel.DataAnnotations;

namespace TFWebService.Data.Models
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public DateTime UpdateTime { get; set; }
    }
}
