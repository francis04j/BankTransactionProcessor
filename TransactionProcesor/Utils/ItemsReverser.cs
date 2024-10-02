using System.Collections;
using System.Text;

namespace TransactionProcesor.Utils;

public class ItemsReverser
{
    /*
     *Things to consider
     *
     * empty or null string
     * Types that do inherit spec
     *
     * how to multithread
     * performant
     * low-level
     * 
     */
    public T Reverse<T>(T input) where T : class, IEnumerable
    {
        if (input is string)
        {
            if (string.IsNullOrEmpty(input.ToString()))
            {
                return input;
            }
            
            //reverse string
            var reversed = new StringBuilder(input.ToString().Length);
            for (int i = input.ToString().Length - 1; i >= 0;  i--)
            {
                reversed.Append(input.ToString()[i]);
                
            }
            return reversed.ToString() as T;
                    
        }

        if (input == null)
        {
            throw new ArgumentNullException("input is null");
        }

        if (input is IEnumerable)
        {
            
            var ienuminput = input as IEnumerable;
            if (ienuminput.Count() > 0)
            {
                
            }
            var reversed = new List<T>();
            for (int i = input.Count(); i >= 0; i--)
            {
                reversed.Add(input.ElementAt(i));
            }

            return reversed as T;
        }
        return "dlroW olleH" as T;
    }
}