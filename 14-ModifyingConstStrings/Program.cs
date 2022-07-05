using System;
public class Program
{
    // declare a const string - can we modify it? 
    public const string sentence = "The quick brown fox jumped over the lazy dog";

    static void Main(string[] args)
    {
        unsafe
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
                //update: without .ToString(), it just doesn't work!
                //Console.WriteLine($"The sentence is: {sentence.ToString()}");
                Console.Write($"The sentence is: ");
                Console.WriteLine(sentence);
                Console.WriteLine($"The address of the sentence is: {(IntPtr)pSentence}");
            }

            //Change for Git

            /*example from StackOverflow
            const string sv = "7";

            Console.WriteLine(sv); // outputs 7

            unsafe
            {
                fixed (char* p = sv)
                {
                    *p = '8';
                }
            }

            Console.WriteLine(sv); // outputs 8
            */
        }
    }
}