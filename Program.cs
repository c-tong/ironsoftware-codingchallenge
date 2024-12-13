// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine(IronSoftware.OldPhonePad("33#"));
Console.WriteLine(IronSoftware.OldPhonePad("227*#"));
Console.WriteLine(IronSoftware.OldPhonePad("4433555 555666#"));
Console.WriteLine(IronSoftware.OldPhonePad("8 88777444666*664#"));
Console.WriteLine(IronSoftware.OldPhonePad("*#"));
Console.WriteLine(IronSoftware.OldPhonePad("222447774447777844426608666 664#"));
Console.WriteLine(IronSoftware.OldPhonePad("3333#"));
Console.WriteLine(IronSoftware.OldPhonePad("777777#"));
Console.WriteLine(IronSoftware.OldPhonePad("2220000008#"));
Console.WriteLine(IronSoftware.OldPhonePad("22211118#"));
Console.WriteLine(IronSoftware.OldPhonePad("222**8*#"));
Console.WriteLine(IronSoftware.OldPhonePad("222**8#"));
public static class IronSoftware
{    
    private static Dictionary<int, Dictionary<int, char>> PadValues = new Dictionary<int, Dictionary<int, char>> 
    {
        { 0, new Dictionary<int, char> { { 1, ' ' } }},
        { 1, new Dictionary<int, char> { { 1, '&' }, { 2, '\'' }, { 3, '(' } } },
        { 2, new Dictionary<int, char> { { 1, 'A' }, { 2, 'B' }, { 3, 'C' } } },
        { 3, new Dictionary<int, char> { { 1, 'D' }, { 2, 'E' }, { 3, 'F' } } },
        { 4, new Dictionary<int, char> { { 1, 'G' }, { 2, 'H' }, { 3, 'I' } } },
        { 5, new Dictionary<int, char> { { 1, 'J' }, { 2, 'K' }, { 3, 'L' } } },
        { 6, new Dictionary<int, char> { { 1, 'M' }, { 2, 'N' }, { 3, 'O' } } },
        { 7, new Dictionary<int, char> { { 1, 'P' }, { 2, 'Q' }, { 3, 'R' }, { 4, 'S' } } },
        { 8, new Dictionary<int, char> { { 1, 'T' }, { 2, 'U' }, { 3, 'V' } } },
        { 9, new Dictionary<int, char> { { 1, 'W' }, { 2, 'X' }, { 3, 'Y' }, { 4, 'Z' } } }
    };

    public static string OldPhonePad(string input) 
    {

        var regex = new Regex("[1-9]");
        if (!regex.IsMatch(input))
        {            
            return "";
        }
        
        var queue = new Queue<char>(input);
        var result = new Stack<char>();
        int occurence = 0;

        while(queue.Count > 0){
            var p = queue.Peek();

            //End of input, we can quit the loop
            if (p == '#')
            {
                break;
            }

            var c = queue.Dequeue();
            occurence++;

            //Delete char from the stack
            if (c == '*' && result.Count > 0)
            {
                result.Pop();
                occurence = 0;
                continue;
            }

            //We expect a new value
            if (c == ' ' || (c == '*' && result.Count == 0))
            {                
                occurence = 0;
                continue;
            }

            if (c != queue.Peek())
            {
                //If we 'input' data too fast, I assume we go back to the original count
                if (occurence > PadValues[(int)char.GetNumericValue(c)].Count()) 
                {
                    if (c == '0')
                    {
                        occurence = 1;
                    }
                    else
                    {
                        occurence -= PadValues[(int)char.GetNumericValue(c)].Count();
                    }
                }
                result.Push(PadValues[(int)char.GetNumericValue(c)][occurence]);
                occurence = 0;
            }
        }

        return string.Join("", result.Reverse());
    }
}
