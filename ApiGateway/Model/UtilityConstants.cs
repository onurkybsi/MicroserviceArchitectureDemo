using System.Text;
using ApiGateway.Services.Auth;

namespace ApiGateway.Model
{
    public static class UtilityConstants
    {
        private static byte[] authSecurityKeyAsBytes;
        public static byte[] AuthSecurityKeyAsBytes
        {
            get => authSecurityKeyAsBytes; set
            {
                if (authSecurityKeyAsBytes == default(byte[]))
                    authSecurityKeyAsBytes = value;   
            }
        }
    }
}