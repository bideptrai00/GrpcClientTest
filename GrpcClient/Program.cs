using Google.Protobuf;
using Grpc.Core;
using Grpc.Net.Client;
using Mface;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;

using System.Net.Http;

//// Load the certificate
//var cert = new X509Certificate2("C:/Users/lvhieu/Downloads/GrpcClient-master/GrpcClient-master/GrpcClient/Certificate/new_test_ca.crt");

//// Create a handler for the HttpClient to provide the certificate
//var handler = new HttpClientHandler();
//handler.ClientCertificates.Add(cert);

var httpClientHandler = new HttpClientHandler();
// Return `true` to allow certificates that are untrusted/invalid
httpClientHandler.ServerCertificateCustomValidationCallback =
    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
//var httpClient = new HttpClient(httpClientHandler);

var grpcChannel = GrpcChannel.ForAddress("https://testmfaceapi.misa.vn:9900", new GrpcChannelOptions
{
    HttpHandler = httpClientHandler
});

//var grpcChannel = GrpcChannel.ForAddress("http://10.1.36.81:9000", new GrpcChannelOptions
//{
//    HttpHandler = httpClientHandler
//});

var mfaceClient = new MFace.MFaceClient(grpcChannel);

var mfaceCompaniesReply = mfaceClient.ListCompanies(new ListCompaniesRequest
{
    Prefix = ""
});



var mfaceReply = mfaceClient.RecognizeURL(new RecognizeURLRequest
{
    RequestId = Guid.NewGuid().ToString(),
    CompanyId = "nhat_test",
    ClientId = Guid.NewGuid().ToString(),
    MediaUrl = "http://35.201.160.249:9001/mface-misa/test-data/test2.jpg?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=TWBZxO7YJ32u8VsCuMUVv4aRNi4Pqgeu%2F20240415%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240415T040740Z&X-Amz-Expires=1200&X-Amz-SignedHeaders=host&X-Amz-Signature=4ac7ba9fdc53a2a6f96a1ca54f13d678b0276350369a8c5b5fee21c88d6ab788"
});

Console.WriteLine(mfaceCompaniesReply.ToString());
Console.WriteLine(mfaceReply.ToString());
Console.ReadKey();