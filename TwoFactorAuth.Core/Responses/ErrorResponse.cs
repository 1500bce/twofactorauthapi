using System.Collections.Generic;

namespace TwoFactorAuth.Core.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public ErrorResponse()
        {
            this.ErrorMessages = new List<object>();
        }

        public List<object> ErrorMessages { get; set; }

    }
}
