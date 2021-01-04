using SuperSocket.Lumina.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina
{
    public class CacheHelper
    {
        public static Dictionary<string, func_info_t> Data { get; set; } = new Dictionary<string, func_info_t>();
        public static bool Add(func_md_t data)
        {
            var signature = Convert.ToBase64String(data.Signature.signature); 
            //Console.WriteLine(data.Metadata.func_name +"  "+signature);

            var metadata = Data.GetValueOrDefault(signature);

            if (metadata == null)
            {
                func_info_t info = new func_info_t();
                info.MetaData = data.Metadata;
                info.Popularity = 0;
                Data.Add(signature, info);
            }
            else
            {
                if (metadata.MetaData.func_name == data.Metadata.func_name && Convert.ToBase64String(metadata.MetaData.serialized_data) == Convert.ToBase64String(data.Metadata.serialized_data))
                {
                    return false;
                }


                func_info_t info = new func_info_t();
                info.MetaData = data.Metadata;
                info.Popularity = metadata.Popularity;

                Data[signature] = info;
                //popularity ++;
            }
            return true;

        }
        public static func_info_t Get(func_sig_t sig)
        {
            var signature =Convert.ToBase64String(sig.signature);
            //Console.WriteLine(signature);
            var data1 = Data.GetValueOrDefault(signature);
            return data1;

        }
    }
}
