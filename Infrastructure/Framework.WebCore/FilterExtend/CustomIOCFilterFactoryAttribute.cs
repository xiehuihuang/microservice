using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.WebCore.FilterExtend
{
    /// <summary>
    /// 基于完成Filter的依赖注入
    /// </summary>
    public class CustomIOCFilterFactoryAttribute : Attribute, IFilterFactory
    {
        private readonly Type _FilterType = null;

        public CustomIOCFilterFactoryAttribute(Type type)
        {
            _FilterType = type;
        }
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            //return (IFilterMetadata)serviceProvider.GetService(typeof(CustomExceptionFilterAttribute));

            return (IFilterMetadata)serviceProvider.GetService(_FilterType);
        }
    }


}
