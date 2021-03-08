using System;
using System.Collections.Generic;
using System.Linq;

namespace Rectangle.Impl
{
	public static class Service
	{
		/// <summary>
		/// See TODO.txt file for task details.
		/// Do not change contracts: input and output arguments, method name and access modifiers
		/// </summary>
		/// <param name="points"></param>
		/// <returns></returns>
		public static Rectangle FindRectangle(List<Point> points)
		{
			if (points == null)
				throw new ArgumentNullException(nameof(points), "List Of points shouldn't be null.");
			if(HasDuplicatePoints(points))
				throw new ArgumentException("List Of points shouldn't contain the same points.");

			if (points.Count < 2)
				throw new ArgumentException("List Of points should contain at least 2 points but it doesn't contain." + points.Count);
            else if(points.Count == 2)
            {
				int Width = DsiatnceBetweenTwoPoints(points[0].X, points[0].Y, points[0].X + 1, points[0].Y);
				int Height = DsiatnceBetweenTwoPoints(points[0].X, points[0].Y, points[0].X, points[0].Y + 1);
				return new Rectangle(points[0].X, points[0].Y, Width, Height);
			}
            else
            {
				(int, int, int, int) rectData = FindRectangleData(points);
				Rectangle rect = new Rectangle(rectData.Item1, rectData.Item2, rectData.Item3, rectData.Item4);
				return rect;
			}
		}

		private static (int, int, int, int) FindRectangleData(List<Point> points)
		{
			var extremums = points.OrderBy(x => x.X);
			Point maxX = extremums.Last();
			Point minX = extremums.First();
			extremums = points.OrderBy(y => y.Y);
			Point maxY = extremums.Last();
			Point minY = extremums.First();

			int X = points.OrderBy(x => x.X).Skip(1).FirstOrDefault().X;
			int Y = maxY.Y;
			int Width = DsiatnceBetweenTwoPoints(X, Y, maxX.X, Y);
			int Height = DsiatnceBetweenTwoPoints(X, Y, X, minY.Y);
			return (X, Y, Width, Height); 
		}

		private static int DsiatnceBetweenTwoPoints(int x1, int y1, int x2, int y2)
        {
			int result = (int)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
			return result;
		}

		public static bool HasDuplicatePoints(List<Point> points)
        {
			int countSamePoints = points.GroupBy(p => new { p.X, p.Y }).Where(g => g.Count() > 1).Count();
			if (countSamePoints > 0)
				return true;
			else
				return false;
        }

	}
}
