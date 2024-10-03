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
        
        return input;
    }

    public IList<T> Reverse<T>(IList<T> input)
    {
        if (input is IEnumerable)
        {
           // var reversed = new List<T>();
            for (int i = 0; i < input.Count / 2; i++)
            {
                var temp = input[i];
                input[i] = input[input.Count - i - 1];
                input[input.Count - i - 1] = temp;
            }

            return input;
        }
        return input;
    }

    private string ReverseString(string input)
    {
        lock (_objlock)
        {
            var reversed = new StringBuilder(input);
            for (int i = 0; i < input.Length / 2; i++)
            {
                var temp = reversed[i];
                reversed[i] = reversed[input.Length - i - 1];
                reversed[input.Length - i - 1] = temp;

            }

            return reversed.ToString();
        }
    }
}