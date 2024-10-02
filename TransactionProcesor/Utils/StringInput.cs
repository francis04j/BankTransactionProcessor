using System.Collections;

namespace TransactionProcesor.Utils;

public class StringInput : IEnumerable<string>
{
    readonly string _input;

    public StringInput(string input)
    {
        _input = input;
    }

    public StringInput(IEnumerable<string> input)
    {
        _input = string.Join(',', input);
    }
    
    
    public IEnumerator<string> GetEnumerator()
    {
        foreach (var c in _input)
        {
            yield return c.ToString();
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static implicit operator StringInput(string input)
    {
        return new StringInput(input);
    }
}