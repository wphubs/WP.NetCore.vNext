using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WP.Shared.Application.Contracts
{
    public abstract class AbstractAppService 
    {
        protected ResponseResult DefaultResult() => new();

        protected ResponseResult<TValue> AppSrvResult<TValue>([NotNull] TValue value) => new(value);

        protected ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null) => new(statusCode, detail, title, instance, type);

    }
}
