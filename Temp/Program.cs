using System.Text;
using System.Collections;

//Exerc 1
//var result = Exercise.GetCharacterCount("John Doe");

//Exerc 2
//var input = "4 score and 7 years ago, 8 men had the same PIN code: 6571";
//var result = Exercise.ReplaceDigits(input);

var matrix1 = new Matrix(new int[,]
{
    {1, 2, 3},
    {4, 5, 6}
});

var matrix2 = new Matrix(new int[,]
{
    {7, 8},
    {9, 10},
    {11, 12}
});

var result = MatrixExercise.Multiply(matrix1, matrix2);

Console.WriteLine(result.ToString());

public class Exercise
{
    // TODO: fix this method - remove boxing & unboxing, and return correct result
    public static object GetCharacterCount(string name)
    {
        var result = new Dictionary<char, int>();
        foreach (char c in name.ToLower())
        {
            if (c != ' ')
            {
                if (result.ContainsKey(c))
                    result[c] += 1;
                else
                    result.Add(c, 1);
            }
        }
        return result;
    }

    public static string ReplaceDigits(string sentence)
    {
        string[] words = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        var result = new StringBuilder(sentence.Length);

        for (var i = 0; i < sentence.Length; i++)
        {
            var c = sentence[i];

            if (!Char.IsDigit(c))
                result.Append(c);
            else
            {
                var digit = (int) Char.GetNumericValue(c);
                result.Append(words[digit]);

                if (i < sentence.Length-1 && !Char.IsWhiteSpace(sentence[i+1]))
                    result.Append(' ');
            }
        }

        return result.ToString();
       
    }

}

// TODO: optimize this class
public class Matrix: IEnumerable
{

    private int[] flatArray;
    private int _internalCols;
    private int _internalRows;

    public int Rows { get { return  _internalRows; } }
    public int Columns { get { return _internalCols; } }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return flatArray.GetEnumerator();
    }

    public int this[int row, int col]
    {
        get 
        {
            var index = row > 0 ? row * _internalCols + col: col;
            return flatArray[index];
        }

        set 
        {
            var index = row > 0 ? row * _internalCols + col: col;
            flatArray[index] = value;
        }
    }


    public Matrix(int[,] value)
    {
        _internalRows = value.GetLength(0);
        _internalCols = value.GetLength(1);

        flatArray = new int[value.Length];

        for (int i = 0; i < _internalRows; i++)
        {
            for (int j = 0; j < _internalCols; j++)
            {
                var index = i > 0 ? i * _internalCols + j: j;
                flatArray[index] = value[i, j];
            }
        }
    }
}

public class MatrixExercise
{
    public static Matrix Multiply(Matrix a, Matrix b)
    {
        var result = new Matrix(new int[a.Rows, b.Columns]);
        for (int i = 0; i < result.Rows; i++)
        {
            for (int j = 0; j < result.Columns; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < a.Columns; k++)
                    result[i, j] += (a[i, k] * b[k, j]);
            }
        }
        return result;
    }
}
