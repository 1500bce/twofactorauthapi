namespace TwoFactorAuth.Core.Responses
{
    public class SuccessResponse : BaseResponse
    {
        public SuccessResponse()
        {
            base.Success = true;
        }
    }
}
