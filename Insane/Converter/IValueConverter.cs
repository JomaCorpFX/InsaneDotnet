using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insane.Converter
{
    public interface IValueConverter<TValue>
    {
        public TValue Convert(TValue value) ;
        public TValue Deconvert(TValue value) ;
    }
}
