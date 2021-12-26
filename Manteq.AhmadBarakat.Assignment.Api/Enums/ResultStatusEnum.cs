using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Enums
{
    public enum ResultStatusEnum
    {
        [Description("Success")]
        Success,
        [Description("Server Error")]
        ServerError,
        [Description("BadRequest")]
        BadRequest
    }
}
