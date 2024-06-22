using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace LearnHub.Core.Interfaces
{
    public interface IBaseEntity
    {
    
    [NotMapped]
    public string TableName { get; set; } 
    }
}