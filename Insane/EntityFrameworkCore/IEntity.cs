using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore
{
    public interface IEntity
    {
        public string UniqueId { get; set; }
    }
}
