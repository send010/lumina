using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket.Lumina.Model;
using SuperSocket.ProtoBase;

namespace SuperSocket.Lumina
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = SuperSocketHostBuilder.Create<RPC, MyPackageFilter>(args)
                    .UsePackageHandler(async (s, p) =>
                    {
                        Console.WriteLine($"Code = {p.Code}");
                        var rpc = new RPC();
                        if (p.Code == TypeEnum.RPC_HELO)
                        {
                            var bytes1 = new List<Byte> { 0, 0, 0, 0, 0xa };
                            await s.SendAsync(bytes1.ToArray());
                            //var md = new RPC_HELO(p.Data.ToArray());
                            //check 
                            return;
                        }
                        else if (p.Code == TypeEnum.PULL_MD)
                        {
                            var md = new PULL_MD(p.Data.ToArray());
                            PULL_MD_RESULT result = new PULL_MD_RESULT();
                            //md 没问题就从数据库里面获取 获取之后返回
                            foreach (var item in md.funcInfos)
                            {
                                //从数据库里面取
                                var data =CacheHelper.Get(item);
                                if (data != null && result.Result.FirstOrDefault(p => p.MetaData.func_name == data.MetaData.func_name) ==null)
                                {
                                    result.Found.Add(0);
                                    result.Result.Add(data);
                                }
                                else
                                {
                                    result.Found.Add(1);
                                    //result.Result.Add(new func_info_t());
                                    //result.Found.Add(0);
                                }
                            }
                            rpc.Data = result.GetBytes().ToList();
                            rpc.Code = TypeEnum.PULL_MD_RESULT;
                            await s.SendAsync(rpc.GetBytes());
                        }
                        else if (p.Code == TypeEnum.PUSH_MD)
                        {
                            var md = new PUSH_MD(p.Data.ToArray());
                            var result = new PUSH_MD_RESULT();
                            foreach (var item in md.funcInfos)
                            {
                                //存储
                                result.ResultsFlags.Add(CacheHelper.Add(item)?1:0);
                            }
                            rpc.Data = result.GetBytes().ToList();
                            rpc.Code = TypeEnum.PUSH_MD_RESULT;
                            await s.SendAsync(rpc.GetBytes());
                        }
                    })
                    .ConfigureSuperSocket(options =>
                    {
                        options.Name = "Echo Server";



                        //byte[] data = File.ReadAllBytes("lumen.p12");
                        options.AddListener(new ListenOptions
                        {
                            Ip = "Any",
                            Port = 4443,
                            Security = SslProtocols.Tls12,
                            CertificateOptions = new CertificateOptions()
                            {
                                FilePath = "client.pfx",
                                //ClientCertificateRequired = false,

                                //Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate(data, "1164185642"),
                                //Password = "1164185642",
                                //RemoteCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback((OnRemoteCertificateValidationCallback))
                            }
                        }
                        ) ; 
                    })
                    .ConfigureLogging((hostCtx, loggingBuilder) =>
                    {
                        loggingBuilder.AddConsole();
                    })
                    .Build();

             host.RunAsync();

            Console.ReadLine();
        }

        private static bool OnRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
