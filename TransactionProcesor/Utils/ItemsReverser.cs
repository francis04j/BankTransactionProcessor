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
    
    private readonly Object _objlock = new();

    public T Reverse<T>(T input) where T : class
    {
        if (input == null)
        {
            throw new ArgumentNullException("input is null");
        }
        
        if (input is string)
        {
            if (string.IsNullOrEmpty(input.ToString()))
            {
                return input;
            }
            
            //reverse string
            return ReverseString(input as string) as T;
                    
        }
    }

    public IList<T> Reverse<T>(IList<T> input)
    {
        if (input is IEnumerable)
        {
            var reversed = new List<T>();
            for (int i = 0; i < input.Count / 2; i++)
            {
                
                reversed.Add(input.ElementAt(i));
            }

            return reversed;
        }
        return input as IList<T>;
    }

    private string ReverseString(string input)
    {
        lock (_objlock)
        {
            var reversed = new StringBuilder(input.Length);
            for (int i = input.Length - 1; i >= 0; i--)
            {
                reversed.Append(input[i]);

            }

            return reversed.ToString();
        }
    }
}