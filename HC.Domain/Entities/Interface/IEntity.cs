using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Domain.Entities.Interface
{
    public interface IEntity<T> //To be able to change the ID type whenever we want
    {
        T ID { get; set; }  
    }
}
