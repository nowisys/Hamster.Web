using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Hamster.Web
{
    public class ControllerProvider : ControllerFeatureProvider
    {
        private List<TypeInfo> types = new List<TypeInfo>();

        public ControllerProvider(params Type[] types)
            : this(from t in types select t.GetTypeInfo())
        {
        }

        public ControllerProvider(IEnumerable<TypeInfo> types)
        {
            this.types.AddRange(types);
        }

        public ControllerProvider Add<T>()
        {
            types.Add(typeof(T).GetTypeInfo());
            return this;
        }

        protected override bool IsController(TypeInfo typeInfo)
        {
            if (types.Count == 0) {
                // if nothing is configured, we use the default
                return base.IsController(typeInfo);
            }

            foreach (var type in this.types) {
                if (typeInfo.Equals(type)) {
                    return true;
                }
            }

            return false;
        }
    }
}
