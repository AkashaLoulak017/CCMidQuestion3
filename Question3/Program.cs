using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Class to represent a symbol table entry
class SymbolEntry
{
    public string VarName { get; set; }    // Variable name
    public string Type { get; set; }       // Data type
    public string Value { get; set; }      // Assigned value
    public int LineNumber { get; set; }    // Line number where declared
}

class Program
{
    static void Main()
    {
        List<SymbolEntry> symbolTable = new List<SymbolEntry>();  // Symbol table to store valid entries
        int lineNumber = 1;

        Console.WriteLine("Enter lines of code (type 'exit' to stop):");

        while (true)
        {
            Console.Write($"Line {lineNumber}: ");
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "exit")
                break;

            // Regex pattern to match variable declarations (e.g., int x = 5;)
            Match match = Regex.Match(input, @"^(int|float|string|double|var)\s+([a-zA-Z_]\w*)\s*=\s*(.+);$");

            if (match.Success)
            {
                string type = match.Groups[1].Value;
                string varName = match.Groups[2].Value;
                string value = match.Groups[3].Value.Trim();

                // Check if variable name contains a palindrome substring of length ≥ 3
                if (ContainsPalindromeSubstring(varName))
                {
                    // Add to symbol table
                    symbolTable.Add(new SymbolEntry
                    {
                        VarName = varName,
                        Type = type,
                        Value = value,
                        LineNumber = lineNumber
                    });
                    Console.WriteLine($"✅ Inserted: {varName}");
                }
                else
                {
                    Console.WriteLine("❌ Variable name does not contain a palindrome of length ≥ 3. Not inserted.");
                }
            }
            else
            {
                Console.WriteLine("⚠️ Invalid syntax. Try again.");
            }

            lineNumber++;
        }

        // Display the final symbol table
        Console.WriteLine("\nSymbol Table:");
        Console.WriteLine("{0,-15} | {1,-10} | {2,-10} | {3}", "VarName", "Type", "Value", "LineNo");
        Console.WriteLine(new string('-', 60));

        foreach (var entry in symbolTable)
        {
            Console.WriteLine($"{entry.VarName,-15} | {entry.Type,-10} | {entry.Value,-10} | {entry.LineNumber}");
        }
    }

    // Function to check if a string contains any palindrome substring of length ≥ 3
    static bool ContainsPalindromeSubstring(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            for (int len = 3; len <= input.Length - i; len++)
            {
                string substr = input.Substring(i, len);
                if (IsPalindrome(substr))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Helper function to check if a string is a palindrome
    static bool IsPalindrome(string str)
    {
        int left = 0;
        int right = str.Length - 1;
        while (left < right)
        {
            if (str[left] != str[right])
                return false;
            left++;
            right--;
        }
        return true;
    }
}
