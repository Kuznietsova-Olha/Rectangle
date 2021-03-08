using System;
using System.Collections.Generic;
using Rectangle.Impl;

namespace Rectangle.Console
{
	class Program
	{
		/// <summary>
		/// Use this method for local debugging only. The implementation should remain in Rectangle.Impl project.
		/// See TODO.txt file for task details.
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			List<Point> listOfPoints = CreateListOfRandomPoints(10);
			DisplayListOfPoints(listOfPoints);

			DisplayRectangleData(Service.FindRectangle(listOfPoints));


			System.Console.ReadKey();
		}

		public static List<Point> CreateListOfRandomPoints(int numberOfPoints)
		{
			List<Point> points = new List<Point>(numberOfPoints);

			Random random = new Random();
			for (int index = 0; index < numberOfPoints; index++)
			{
				Point point = new Point { X = random.Next(-20, 20), Y = random.Next(-20, 20) };
				if (!points.Contains(point))
				{
					points.Add(point);
				}
			}
			return points;
		}

		public static void DisplayListOfPoints(List<Point> points)
		{
			foreach (Point point in points)
			{
				System.Console.WriteLine("({0, 3}, {1, 3})", point.X, point.Y);
			}
			System.Console.WriteLine();
		}

		public static void DisplayRectangleData(Rectangle.Impl.Rectangle rectangle)
		{
			System.Console.WriteLine("Main Point of Rectangle: ({0}, {1}). Width = {2, 3}, Height = {3, 3}", rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
			System.Console.WriteLine();
		}


	}
}
