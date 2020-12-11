using System;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;

namespace Infrastructure.Framework.Grpc
{
    public static class GrpcCallerService
    {
        public static async Task<ServiceCallResult<TResponse>> CallService<TResponse>(Func<Task<TResponse>> call) where TResponse : IMessage<TResponse>
        {
            var serviceCallResult = new ServiceCallResult<TResponse>();

            try
            {
                var responseFromService = await call();

                // TO-DO: Burası servisten isSuccess false döndüğündede true gitmiş oluyor. Ve 200 dönüyoruz. 
                // Burda responseFromService TResponse oldugu için servisten dönen isSuccess i handle etmek mümkün değil.
                // Bu konuda bi düşünelim ServiceCallResult objesi değişmeli mi bilemiyorum ?
                // Burda servisi çağırırken oluşucak hata ile servis içi hata ayrıştırılmalı  hata kodları yazılmalı !;
                serviceCallResult.IsSuccess = true;
                serviceCallResult.ServiceResponse = responseFromService;

                return serviceCallResult;
            }
            catch (RpcException rex)
            {
                // TO-DO: Mesaj olaylarıı böyle hard coded olmamalı
                serviceCallResult.Message = string.Format("Error calling via grpc: {0} - {1}", rex.Status, rex.Message);

                return serviceCallResult;
            }
            catch (Exception ex)
            {
                serviceCallResult.Message = string.Format("Error occured: {0}", ex.Message);

                return serviceCallResult;
            }
        }
    }
}
