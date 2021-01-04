using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperSocket.Lumina
{
    public class BinHelper
    {
        public static object BytesToStruct(byte[] bytes, Type strcutType)
        {
            if (bytes == null)
            {

            }
            //int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(bytes.Length);
            //Debug.LogError("Type: " + strcutType.ToString() + "---TypeSize:" + size + "----packetSize:" + nSize);
            try
            {
                Marshal.Copy(bytes, 0, buffer, bytes.Length);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        public static List<T> BytesToStructList<T>(byte[] bytes,int? count = null)where T:struct
        {
            var list = new List<T>();
            int size = Marshal.SizeOf(typeof(T));
            if(count == null)
            {
                count = bytes.Length / size;
            }
            for (int i = 0; i < count; i++)
            {
                IntPtr buffer = Marshal.AllocHGlobal(bytes.Length);
                //Debug.LogError("Type: " + strcutType.ToString() + "---TypeSize:" + size + "----packetSize:" + nSize);
                try
                {
                    Marshal.Copy(bytes, i*size, buffer, size);
                    list.Add(Marshal.PtrToStructure<T>(buffer));
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }

            }
            return list;

        }
    }
}
