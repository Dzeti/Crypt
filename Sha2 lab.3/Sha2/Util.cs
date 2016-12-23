using System.Collections.ObjectModel;
using System.Text;

namespace Sha2
{
    public static class Util
    {
        public static string ArrayToString(byte[] arr)
        {
            StringBuilder s = new StringBuilder(arr.Length * 2);
            for (int i = 0; i < arr.Length; ++i)
            {
                s.AppendFormat("{0:x2}", arr[i]);
            }

            return s.ToString();
        }
    }
}
