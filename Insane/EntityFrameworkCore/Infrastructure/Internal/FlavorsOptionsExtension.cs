using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore.Infrastructure.Internal
{
    public class FlavorsOptionsExtension : RelationalOptionsExtension
    {
        public override DbContextOptionsExtensionInfo Info => new FlavorsDbContextOptionsExtensionInfo(this);

        public override void ApplyServices(IServiceCollection services)
        {
            
        }

        protected override RelationalOptionsExtension Clone()
        {
            return this;
        }
    }
}
