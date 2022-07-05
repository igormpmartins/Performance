using System;
public class Program
{
    // declare a const string - can we modify it? 
    public const string sentence = "The quick brown fox jumped over the lazy dog";

    static unsafe void Main(string[] args)
    {
        fixed (char* pSentence = sentence)
        {
            // report initial state
            Console.WriteLine($"The sentence is: {sentence}");
            Console.WriteLine($"The address of the sentence is: {(IntPtr)pSentence}");

            // modify the string
            char* p = pSentence;
            for (var i = 0; i < sentence.Length; i++)
            {
                *p = '*';
                p++;
            }

            // report final state
            Console.WriteLine($"The sentence is: {sentence}");
            Console.WriteLine($"The address of the sentence is: {(IntPtr)pSentence}");
        }
    }
}