namespace Dneprokos.UI.Base.Client.WebDriverCore.WebDriverOptions.Common
{
    /// <summary>
    /// Screen size. Used to configure the browser window size.
    /// </summary>
    public class ScreenResolutionOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenResolutionOptions"/> class with default width and height.
        /// </summary>
        public ScreenResolutionOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenResolutionOptions"/> class with the specified width and height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ScreenResolutionOptions(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Width of the browser window. Default value is 1920.
        /// </summary>
        public int Width { get; set; } = 1920; // Default width set to 1920

        /// <summary>
        /// Height of the browser window. Default value is 1080.
        /// </summary>
        public int Height { get; set; } = 1080; // Default height set to 1080

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{Width}x{Height}";
    }
}
