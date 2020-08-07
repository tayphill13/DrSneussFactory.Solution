using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Models
{
  public class Machine
  {
    public Machine()
    {
      this.Engineers = new HashSet<EngineerMachine>();
    }
    public int MachineId  {get;set;}
    public string Name {get; set;}
    public string Product {get;set;}
    public virtual ICollection<EngineerMachine> Engineers {get; set;}
  }
    
}