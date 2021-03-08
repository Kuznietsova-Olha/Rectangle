using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rectangle.Impl;


namespace Rectangle.Tests
{
	[TestFixture]
	public class Tests
	{
		private List<Point> points;

		[SetUp]
		public void Setup()
		{
			points = new List<Point>();
		}

		private static IEnumerable<TestCaseData> FindRectangleTestCases
		{
			get
			{
				yield return new TestCaseData(new List<Point> { new Point(-10, 5), new Point(-9, 5), new Point(-9, 9),
					new Point(9, -10), new Point(-4, 3), new Point(8, 1), new Point(3, 1), new Point(2, 6), new Point(4, -4),
					new Point(-3, 3) }, new Rectangle.Impl.Rectangle(-9, 9, 18, 19));
				yield return new TestCaseData(new List<Point> { new Point(-5, 0), new Point(-10, -6), new Point(-6, 4),
					new Point(-4, 4), new Point(-9, -9), new Point(7, -5), new Point(-9, 4), new Point(7, 1), new Point(2, 1),
					new Point(9, -3) }, new Rectangle.Impl.Rectangle(-9, 4, 18, 13));
				yield return new TestCaseData(new List<Point> { new Point(-3, 8), new Point(-13, 16), new Point(4, 19),
					new Point(4, 8), new Point(10, 11), new Point(7, 16), new Point(-4, 19), new Point(3, 9), new Point(11, 6),
					new Point(15, 3), new Point(13, 7), new Point(-1, -12), new Point(6, -3), new Point(-16, -17),
					new Point(19, 5)}, new Rectangle.Impl.Rectangle(-13, 19, 32, 36));
				yield return new TestCaseData(new List<Point> { new Point(3, 7), new Point(15, 0), new Point(-11, 17),
					new Point(8, 13), new Point(25, 22), new Point(0, -6), new Point(29, 27), new Point(0, 10), new Point(-18, -1),
					new Point(5, -14), new Point(16, -6), new Point(5, -6), new Point(-13, -10),
					new Point(29, 8), new Point(20, 24) }, new Rectangle.Impl.Rectangle(-13, 27, 42, 41));
				yield return new TestCaseData(new List<Point> { new Point(-7, -3), new Point(-25, -21), new Point(-2, 27),
					new Point(-28, -25), new Point(2, -30), new Point(2, -5), new Point(13, 28), new Point(-11, 27),
					new Point(-20, 17), new Point(5, 27), new Point(-25, 0), new Point(-24, -1), new Point(-1, 29),
					new Point(-14, -27), new Point(10, 17), new Point(-27, -29), new Point(2, 11), new Point(2, 1),
					new Point(-6, -11), new Point(-22, 20) }, new Rectangle.Impl.Rectangle(-27, 29, 40, 59));
				yield return new TestCaseData(new List<Point> { new Point(1, 1), new Point(5, 3) }, new Rectangle.Impl.Rectangle(1, 1, 1, 1));
				yield return new TestCaseData(new List<Point> { new Point(-2, 3), new Point(-1, 1) }, new Rectangle.Impl.Rectangle(-2, 3, 1, 1));
			}
		}

		private static IEnumerable<TestCaseData> DuplicatePointsTestCases
		{
			get
			{
				yield return new TestCaseData(new List<Point> { new Point(-10, 5), new Point(-9, 5), new Point(-9, 9),
					new Point(9, -10), new Point(-4, 3), new Point(8, 1), new Point(-10, 5), new Point(2, 6), new Point(4, -4),
					new Point(-3, 3) });
				yield return new TestCaseData(new List<Point> { new Point(-5, 0), new Point(-10, -6), new Point(-6, 4),
					new Point(-4, 4), new Point(-9, -9), new Point(7, -5), new Point(-9, 4), new Point(7, 1), new Point(2, 1),
					new Point(-5, 0) });
				yield return new TestCaseData(new List<Point> { new Point(-3, 8), new Point(-13, 16), new Point(4, 19),
					new Point(4, 8), new Point(13, 7), new Point(7, 16), new Point(-4, 19), new Point(3, 9), new Point(11, 6),
					new Point(15, 3), new Point(13, 7), new Point(-1, -12), new Point(6, -3), new Point(-16, -17),
					new Point(19, 5)});
				yield return new TestCaseData(new List<Point> { new Point(-3, 8), new Point(-4, 19), new Point(4, 19),
					new Point(4, 8), new Point(4, 11), new Point(7, 16), new Point(-4, 19) });
				yield return new TestCaseData(new List<Point> { new Point(-3, 8), new Point(-3, 8), new Point(4, 15),
					new Point(6, 8), new Point(4, 1), new Point(11, 5), new Point(-4, 7) });
			}
		}

		private static IEnumerable<TestCaseData> AmountOfPointsTestCases
		{
			get
            {
				yield return new TestCaseData(new List<Point> { new Point(-10, 5) });
				yield return new TestCaseData(new List<Point> { new Point(-5, 0) });
				yield return new TestCaseData(new List<Point> { new Point() });
			}
		}

		[TestCaseSource("FindRectangleTestCases")]
		public void FindRectangle_GetPointsFindRectangle_NewRectangle(List<Point> points, Rectangle.Impl.Rectangle expectedResult)
		{
			// Act 
			var actualResult = Service.FindRectangle(points);
			// Assert
			Assert.True(AssertIsAsExpected(expectedResult, actualResult), "FindRectangle method works incorrectly. Result is not as expected.");
		}

		private bool AssertIsAsExpected(Rectangle.Impl.Rectangle expected, Rectangle.Impl.Rectangle actual)
		{
			if (expected.X == actual.X && expected.Y == actual.Y &&
				expected.Width == actual.Width && expected.Height == actual.Height)
				return true;
			return false;
		}

		[Test]
		public void FindRectangle_NullParameter_ThrowArgumentNullException()
		{
			// Arrange
			var expectedEx = typeof(ArgumentNullException);
			// Act
			var actEx = Assert.Catch(() => Service.FindRectangle(null));
			// Assert
			Assert.AreEqual(expectedEx, actEx.GetType(),
				message: "FindRectangle method throw ArgumentNullException if parameter is null.");
		}

		[TestCaseSource("DuplicatePointsTestCases")]
		public void FindRectangle_ContainsDuplicatePoints_ThrowArgumentException(List<Point> points)
        {
			// Arrange
			var expectedEx = typeof(ArgumentException);
			//Act
			var actEx = Assert.Catch(() => Service.FindRectangle(points));
			//Arrange
			Assert.AreEqual(expectedEx, actEx.GetType(), message: "List Of points shouldn't contain the same points.");
		}
		
		[TestCaseSource("AmountOfPointsTestCases")]
		public void FindRectangle_ContainsAtleastTwoPoints_ThrowArgumentException(List<Point> points)
        {
			// Arrange
			var expectedEx = typeof(ArgumentException);
			//Act
			var actEx = Assert.Catch(() => Service.FindRectangle(points));
            // Assert
            Assert.AreEqual(expectedEx, actEx.GetType(),
                message: "FindRectangle method throw ArgumentException if parameter contains less than two points.");
        }

 
    }
}