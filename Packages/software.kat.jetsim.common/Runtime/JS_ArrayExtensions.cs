
/*
MIT License

Copyright (c) 2022 Varneon

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using JetBrains.Annotations;

namespace KatSoftware.JetSim.Common.Runtime.Extensions
{
    public static class JS_ArrayExtensions
    {
        /// <summary>
        /// Adds an object to the end of the array
        /// <para>
        /// Based on: <see href="https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-6.0">List&lt;T&gt;.Add(T)</see>
        /// </para>
        /// </summary>
        /// <returns>Modified T[]</returns>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">Source T[] to modify.</param>
        /// <param name="item">The object to be added to the end of the T[].</param>
        [PublicAPI]
        public static T[] Add<T>(this T[] array, T item)
        {
            int length = array.Length;

            var newArray = new T[length + 1];

            array.CopyTo(newArray, 0);

            newArray.SetValue(item, length);

            return newArray;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the array
        /// <para>
        /// Based on: <see href="https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.remove?view=net-6.0">List&lt;T&gt;.Remove(T)</see>
        /// </para>
        /// </summary>
        /// <returns>Modified T[]</returns>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">Source T[] to modify.</param>
        /// <param name="item">The object to remove from the T[].</param>
        [PublicAPI]
        public static T[] Remove<T>(this T[] array, T item)
        {
            int index = System.Array.IndexOf(array, item);

            return index == -1 ? array : array.RemoveAt(index);
        }

        /// <summary>
        /// Removes the element at the specified index of the array
        /// <para>
        /// Based on: <see href="https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.removeat?view=net-6.0">List&lt;T&gt;.RemoveAt(Int32)</see>
        /// </para>
        /// </summary>
        /// <returns>Modified T[]</returns>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">Source T[] to modify.</param>
        /// <param name="index">The zero-based index of the element to remove.</param>
        [PublicAPI]
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            int length = array.Length;

            if (index >= length || index < 0)
            {
                return array;
            }

            int maxIndex = length - 1;

            var newArray = new T[maxIndex];

            if (index == 0)
            {
                System.Array.Copy(array, 1, newArray, 0, maxIndex);
            }
            else if (index == maxIndex)
            {
                System.Array.Copy(array, 0, newArray, 0, maxIndex);
            }
            else
            {
                System.Array.Copy(array, 0, newArray, 0, index);
                System.Array.Copy(array, index + 1, newArray, index, maxIndex - index);
            }

            return newArray;
        }

        /// <summary>
        /// Adds an object to the end of the array while ensuring that duplicates are not added
        /// </summary>
        /// <returns>Modified T[]</returns>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">Source T[] to modify.</param>
        /// <param name="item">The object to be added to the end of the T[].</param>
        [PublicAPI]
        public static T[] AddUnique<T>(this T[] array, T item) => array.Contains(item) ? array : array.Add(item);

        [PublicAPI]
        public static bool Contains<T>(this T[] array, T item) => System.Array.IndexOf(array, item) > -1;
    }
}
