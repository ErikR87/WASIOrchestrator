using System.ServiceModel;

namespace WO.Hub.Contract;

[ServiceContract]
public interface IGreeterService
{
	public Task<HelloReply> SayHello(HelloRequest request);

}
