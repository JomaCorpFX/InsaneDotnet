using Insane.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.EntityFrameworkCore.Infrastructure
{
    public class FlavorsDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<FlavorsDbContextOptionsBuilder, FlavorsOptionsExtension>
    {

        public FlavorsDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
        {

        }


        public FlavorsOptionsExtension? GetOptionsExtension()
        {
            return (FlavorsOptionsExtension?)OptionsBuilder.Options.Extensions.FirstOrDefault(e => e.Info.GetType().Equals(typeof(FlavorsDbContextOptionsExtensionInfo)));
        }
    }
}
