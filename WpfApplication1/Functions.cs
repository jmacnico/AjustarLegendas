using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjustarLegendas
{
    public static class Functions
    {
        public static bool IsNumberInt(this string Texto)
        {
            try
            {
                Convert.ToInt32(Texto);
                return true;
            }
            catch
            {
                return false;
            }

            //foreach (char c in Texto)
            //{
            //    if (!char.IsNumber(c))
            //        return false;
            //}
            //return true;
        }
    }
}
