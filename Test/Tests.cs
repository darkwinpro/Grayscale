using NUnit.Framework;
using FluentAssertions;


namespace TestGrayscale
{
    [TestFixture]
    public class Tests
    {
        [TestCase(0, 0, 0, 0)]
        [TestCase(255, 100, 100, 151)]
        [TestCase(255, 254, 255, 254)]
        [TestCase(255, 255, 255, 255)]
        [TestCase(100, 100, 100, 100)]
        [TestCase(200, 200, 200, 200)]
        [TestCase(200, 0, 100, 100)]
        public void WhenColorPixelPassed_AndDataIsCorrect_ThenGrayscalePixelAsResult(byte r, byte g, byte b,
            byte expectedResult)
        {
            // Arrange. 
            var red = new byte[1, 1];
            var green = new byte[1, 1];
            var blue = new byte[1, 1];

            red[0, 0] = r;
            green[0, 0] = g;
            blue[0, 0] = b;

            // Act. 
            GrayscaleTask.Program.GrayscaleImage(red, green, blue);

            // Assert. 
            expectedResult.Should().Be(red[0, 0]);
            expectedResult.Should().Be(green[0, 0]);
            expectedResult.Should().Be(blue[0, 0]);
        }

        [Test]
        public void BigImagePassed_AndDataIsCorrect_AllPixelsChanged()
        {
            // Arrange. 
            int width = 100;
            int height = 100;

            var red = new byte[width, height];
            var green = new byte[width, height];
            var blue = new byte[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    red[x, y] = 100;
                    green[x, y] = 0;
                    blue[x, y] = 200;
                }
            }

            var expectedResult = new byte[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    expectedResult[x, y] = 100;
                }
            }

            // Act. 
            GrayscaleTask.Program.GrayscaleImage(red, green, blue);

            // Assert. 
            red.Should().Equal(expectedResult);
            green.Should().Equal(expectedResult);
            blue.Should().Equal(expectedResult);
        }
    }
}