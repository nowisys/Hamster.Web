using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Hamster.Web
{
    public class TypeProvider : ApplicationPart, IApplicationPartTypeProvider
    {
        private List<TypeInfo> types = new List<TypeInfo>();

        public TypeProvider(params TypeInfo[] types)
        {
            this.types.AddRange(types);
        }

        public TypeProvider(IEnumerable<TypeInfo> types)
        {
            this.types.AddRange(types);
        }

        public TypeProvider Add<T>()
        {
            types.Add(typeof(T).GetTypeInfo());
            return this;
        }

        public override string Name => "TypeProvider";
        public IEnumerable<TypeInfo> Types { get { return types; } }
    }
}
