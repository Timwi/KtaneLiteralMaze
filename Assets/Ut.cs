using System;

namespace LiteralMaze
{
    static class Ut
    {
        public static T[] Insert<T>(this T[] array, int startIndex, T value)
        {
            T[] result = new T[array.Length + 1];
            Array.Copy(array, 0, result, 0, startIndex);
            result[startIndex] = value;
            Array.Copy(array, startIndex, result, startIndex + 1, array.Length - startIndex);
            return result;
        }

        public static T[] Append<T>(this T[] array, T value)
        {
            return Insert(array, array.Length, value);
        }
    }
}
