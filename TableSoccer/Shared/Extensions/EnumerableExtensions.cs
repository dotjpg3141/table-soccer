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

		public static void Shuffle<T>(this IList<T> list, Random random)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = random.Next(n + 1);
				(list[k], list[n]) = (list[n], list[k]);
			}
		}
	}
}