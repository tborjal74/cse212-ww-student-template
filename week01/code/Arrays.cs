public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Steps Taken 
        double[] multiples = new double[length]; // 1. Initialize the array holding the multiples

        for (int i = 0; i < length; i++) // 2. Use proper for loop to iterate the result of each multiple depending on the length
        {
            multiples[i] = number * (i + 1); // 3. Use the correct formula to calculate the multiples of numbers.
        }

        return multiples; // 4. Return the result to the filled array.

        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // return []; replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    { // Steps
        List<int> newList = new List<int>(); // 1. Initialize the new List

        int cutpoint = data.Count - amount; //2.  Set the variable that will get the numbers to rotate to the right
        List<int> lastPart = data.GetRange(cutpoint, amount); //3. Get the range of the numbers based on the amount to display as the last
        List<int> firstPart = data.GetRange(0, cutpoint); // 4. Get the range of the numbers based on the cutpoint to display as the first

        newList.AddRange(lastPart);// 5. Add the set of last numbers to display in the first
        newList.AddRange(firstPart); // 6. Add the set of first numbers to display the last.

        data.Clear();
        data.AddRange(newList); // 7. Display the whole list of numbers
    }
      // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
}
