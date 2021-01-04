using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperSocket.Lumina.Model
{
    public class func_sig_t
    {
        public int version;
        public byte[] signature;

        public func_sig_t(IByteBuffer buffer)
        {
            version = buffer.packdd();

            var length = buffer.packdd();

            signature = new byte[length];

            buffer.ReadBytes(signature);
        }
        public func_sig_t()
        {
        }
    }
    /// <summary>
    /// list对象去重
    /// </summary>
    public class Compare : IEqualityComparer<func_sig_t>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(func_sig_t x, func_sig_t y)
        {
            if (x == null || y == null)
                return false;
            if (Convert.ToBase64String(x.signature) == Convert.ToBase64String(y.signature))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(func_sig_t obj)
        {
            return obj.GetHashCode();
        }
    }
}
