using System;
using System.Collections.Generic;

namespace System.Linq
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<(int index, T item)> Enumerate<T>(this IEnumerable<T> source)
		{
			int index = 0;
			foreach (var item in source)
			{
				yield return (index++, item);
			}
		}
	}
}