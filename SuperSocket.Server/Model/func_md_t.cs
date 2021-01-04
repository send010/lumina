using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class func_md_t
    {
        public func_md_t(IByteBuffer buffer)
        {
            Metadata = new func_metadata(buffer);
            Signature = new func_sig_t(buffer);
        }

        public func_metadata Metadata { get; set; }
        public func_sig_t Signature { get; set; }
    }
}
