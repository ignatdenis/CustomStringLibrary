using System;

namespace StringLibraryProject
{
    internal static class CustomStringLibrary
    {
        // ==========================================
        // Character Functions
        // ==========================================
        public static bool IsAlphaNumeric(char ch)
        {
            return IsAlpha(ch) || IsDigit(ch);
        }

        public static bool IsAlpha(char ch)
        {
            return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z');
        }

        public static bool IsDigit(char ch)
        {
            return ch >= '0' && ch <= '9';
        }

        public static bool IsLower(char ch)
        {
            return ch >= 'a' && ch <= 'z';
        }

        public static bool IsUpper(char ch)
        {
            return ch >= 'A' && ch <= 'Z';
        }

        public static char ToLower(char ch)
        {
            if (IsUpper(ch)) return (char)(ch + 32);
            return ch;
        }

        public static char ToUpper(char ch)
        {
            if (IsLower(ch)) return (char)(ch - 32);
            return ch;
        }

        // ==========================================
        // Basic String Functions
        // ==========================================
        public static int Length(string target)
        {
            if (target == null) return 0;

            int count = 0;
            foreach (char ch in target)
            {
                count++;
            }
            return count;
        }

        public static string Substring(string target, int startIndex)
        {
            int length = Length(target) - startIndex;
            char[] substring = new char[length];

            for (int i = 0; i < length; i++)
            {
                substring[i] = target[startIndex + i];
            }
            return new string(substring);
        }

        public static string Substring(string target, int startIndex, int length)
        {
            char[] substring = new char[length];
            for (int i = 0; i < length; i++)
            {
                substring[i] = target[startIndex + i];
            }
            return new string(substring);
        }

        // ==========================================
        // Copy & Concatenation
        // ==========================================
        public static void Copy(ref string destination, string source)
        {
            destination = source;
        }

        public static void Copy(ref string destination, string source, int count)
        {
            destination = Substring(source, 0, count);
        }

        public static void Concat(ref string destination, string source)
        {
            int destLength = Length(destination);
            int sourceLength = Length(source);
            char[] temp = new char[destLength + sourceLength];

            int k = 0;
            for (int i = 0; i < destLength; i++) temp[k++] = destination[i];
            for (int i = 0; i < sourceLength; i++) temp[k++] = source[i];

            destination = new string(temp);
        }

        public static void Concat(ref string destination, string source, int count)
        {
            int destLength = Length(destination);
            char[] temp = new char[destLength + count];

            int k = 0;
            for (int i = 0; i < destLength; i++) temp[k++] = destination[i];
            for (int i = 0; i < Length(source) && i < count; i++) temp[k++] = source[i];

            destination = new string(temp);
        }

        // ==========================================
        // Searching
        // ==========================================
        public static string FindSubstring(string target, char ch)
        {
            for (int i = 0; i < Length(target); i++)
            {
                if (target[i] == ch) return Substring(target, i);
            }
            return null;
        }

        public static string FindSubstring(string target, string searchString)
        {
            for (int i = 0; i < Length(target); i++)
            {
                bool isFound = true;
                for (int j = 0; j < Length(searchString); j++)
                {
                    if (i + j >= Length(target) || target[i + j] != searchString[j])
                    {
                        isFound = false;
                        break;
                    }
                }
                if (isFound) return Substring(target, i);
            }
            return null;
        }

        public static int IndexOf(string target, char ch)
        {
            for (int i = 0; i < Length(target); i++)
            {
                if (target[i] == ch) return i;
            }
            return -1;
        }

        public static int IndexOf(string target, string searchString)
        {
            for (int i = 0; i < Length(target); i++)
            {
                bool isFound = true;
                for (int j = 0; j < Length(searchString); j++)
                {
                    if (i + j >= Length(target) || target[i + j] != searchString[j])
                    {
                        isFound = false;
                        break;
                    }
                }
                if (isFound) return i;
            }
            return -1;
        }

        // ==========================================
        // Comparison
        // ==========================================
        public static int Compare(string str1, string str2)
        {
            int i = 0, j = 0;
            while (i < Length(str1) && j < Length(str2))
            {
                if (str1[i] < str2[j]) return -1;
                if (str1[i] > str2[j]) return 1;
                i++;
                j++;
            }
            if (i < Length(str1)) return 1;
            if (j < Length(str2)) return -1;
            return 0;
        }

        // ==========================================
        // Splitting
        // ==========================================
        private static int CountWords(string target, string delimiters)
        {
            int wordCount = 0;
            bool inWord = false;

            for (int i = 0; i < Length(target); i++)
            {
                bool isDelimiter = IndexOf(delimiters, target[i]) != -1;

                if (!isDelimiter)
                {
                    if (!inWord)
                    {
                        inWord = true;
                        wordCount++;
                    }
                }
                else
                {
                    inWord = false;
                }
            }
            return wordCount;
        }

        public static string[] Split(string target, string delimiters)
        {
            int start = 0, k = 0, n = Length(target);
            string[] result = new string[CountWords(target, delimiters)];

            for (int i = 0; i <= n; i++)
            {
                bool isEndOrDelimiter = (i == n) || (IndexOf(delimiters, target[i]) != -1);

                if (isEndOrDelimiter)
                {
                    if (i > start)
                    {
                        result[k++] = Substring(target, start, i - start);
                    }
                    start = i + 1;
                }
            }
            return result;
        }

        // ==========================================
        // Modifications (Insert, Delete, Replace)
        // ==========================================
        public static void Delete(ref string target, int position)
        {
            char[] result = new char[Length(target) - 1];
            for (int i = 0, j = 0; i < Length(target); i++)
            {
                if (i != position - 1) result[j++] = target[i];
            }
            target = new string(result);
        }

        public static void Delete(ref string target, int startPos, int endPos)
        {
            char[] result = new char[Length(target) - (endPos - startPos + 1)];
            for (int i = 0, j = 0; i < Length(target); i++)
            {
                if (i < startPos - 1 || i > endPos - 1) result[j++] = target[i];
            }
            target = new string(result);
        }

        public static void DeleteSubstring(ref string target, string toRemove)
        {
            int pos = IndexOf(target, toRemove);
            int removeLength = Length(toRemove);

            while (pos != -1)
            {
                Delete(ref target, pos + 1, pos + removeLength);
                pos = IndexOf(target, toRemove);
            }
        }

        public static void Insert(ref string target, string toInsert, int position)
        {
            string left = Substring(target, 0, position - 1);
            string right = Substring(target, position - 1, Length(target) - (position - 1));

            Concat(ref left, toInsert);
            Concat(ref left, right);
            target = new string(left);
        }
        // ==========================================
        // Others
        // ==========================================
        public static void Set(ref string target, char ch, int count)
        {
            char[] result = new char[Length(target)];
            int i;
            for (i = 0; i < Length(target) && count > 0; i++, count--)
            {
                result[i] = ch;
            }
            for (; i < Length(target); i++)
            {
                result[i] = target[i];
            }
            target = new string(result);
        }

        public static void Replace(ref string target, string oldValue, string newValue)
        {
            int pos = IndexOf(target, oldValue);
            int oldLength = Length(oldValue);

            while (pos != -1)
            {
                Delete(ref target, pos + 1, pos + oldLength);
                Insert(ref target, newValue, pos + 1);
                pos = IndexOf(target, oldValue);
            }
        }

        public static void ToUpper(ref string target)
        {
            char[] result = new char[Length(target)];
            for (int i = 0; i < Length(target); i++)
            {
                result[i] = ToUpper(target[i]);
            }
            target = new string(result);
        }

        public static void ToLower(ref string target)
        {
            char[] result = new char[Length(target)];
            for (int i = 0; i < Length(target); i++)
            {
                result[i] = ToLower(target[i]);
            }
            target = new string(result);
        }

        public static bool IsPalindrome(string target)
        {
            int left = 0, right = Length(target) - 1;
            while (left < right)
            {
                if (target[left] != target[right]) return false;
                left++;
                right--;
            }
            return true;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            string s1 = "Hello";
            string s2 = s1;

            Console.WriteLine("\nTest Functii Caractere\n");
            Console.WriteLine(CustomStringLibrary.IsAlpha('H'));
            Console.WriteLine(CustomStringLibrary.IsDigit('5'));
            Console.WriteLine(CustomStringLibrary.ToUpper('h'));

            Console.WriteLine("\nTest Substring\n");
            Console.WriteLine(CustomStringLibrary.Substring(s1, 1));
            Console.WriteLine(CustomStringLibrary.Substring(s1, 1, 3));

            Console.WriteLine("\nTest Copiere\n");
            string copyStr = "";
            CustomStringLibrary.Copy(ref copyStr, s1);
            Console.WriteLine(copyStr);

            Console.WriteLine("\nTest Concatenare\n");
            CustomStringLibrary.Concat(ref s1, " World");
            Console.WriteLine(s1);
            CustomStringLibrary.Concat(ref s1, " Again", 3);
            Console.WriteLine(s1);

            Console.WriteLine("\nTest Cautare\n");
            Console.WriteLine(CustomStringLibrary.FindSubstring(s1, 'W'));
            Console.WriteLine(CustomStringLibrary.IndexOf(s1, 'A'));

            Console.WriteLine("\nTest Comparere\n");
            Console.WriteLine(CustomStringLibrary.Compare(s1, "Hello World Ag"));
            Console.WriteLine(CustomStringLibrary.Compare(s1, "Hello"));

            Console.WriteLine("\nTest Split\n");
            string[] split = CustomStringLibrary.Split(s1, " ");
            foreach (string cuv in split)
            {
                if (cuv != null)
                {
                    Console.WriteLine(cuv);
                }
            }

            Console.WriteLine("\nTest Stergeri\n");
            CustomStringLibrary.Delete(ref s1, 5);
            Console.WriteLine(s1);

            CustomStringLibrary.Delete(ref s1, 5, 10);
            Console.WriteLine(s1);

            CustomStringLibrary.DeleteSubstring(ref s1, "ll");
            Console.WriteLine(s1);

            Console.WriteLine("\nTest Inserari\n");
            CustomStringLibrary.Insert(ref s1, "l", 3);
            Console.WriteLine(s1);

            CustomStringLibrary.Insert(ref s1, "123", 3);
            Console.WriteLine(s1);

            Console.WriteLine("\nTest Inlocuire\n");
            CustomStringLibrary.Set(ref s1, 'x', 3);
            Console.WriteLine(s1);

            CustomStringLibrary.Replace(ref s1, "x", "y");
            Console.WriteLine(s1);

            CustomStringLibrary.Replace(ref s1, "yy", "o");
            Console.WriteLine(s1);

            Console.WriteLine("\nTest Upper/Lower\n");
            CustomStringLibrary.ToUpper(ref s1);
            Console.WriteLine(s1);
            CustomStringLibrary.ToLower(ref s1);
            Console.WriteLine(s1);

            Console.ReadLine();
        }
    }
}