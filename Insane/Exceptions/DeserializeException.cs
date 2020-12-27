using System;
using System.Collections.Generic;
using System.Text;

namespace Insane.Exceptions
{
    public class DeserializeException: SystemException
    {
        private readonly Type type;

        public DeserializeException(Type type)
        {
            this.type = type;
        }

        public override string Message => $"Invalid Json for type \"{type.Name}\"";
    }
}
