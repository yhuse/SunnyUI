using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Sunny.UI
{
    public class DigitalFont
    {
        private static DigitalFont _instance = null;

        public static DigitalFont Instance
        {
            get
            {
                if (_instance == null) _instance = new DigitalFont();
                return _instance;
            }
        }

        private readonly PrivateFontCollection ImageFont;

        public DigitalFont()
        {
            byte[] buffer = ReadFontFileFromResource("Sunny.UI.Font.sa-digital-number.ttf");
            ImageFont = new PrivateFontCollection();
            var memoryFont = Marshal.AllocCoTaskMem(buffer.Length);
            Marshal.Copy(buffer, 0, memoryFont, buffer.Length);
            ImageFont.AddMemoryFont(memoryFont, buffer.Length);
        }

        public Font GetFont(float size)
        {
            return new Font(ImageFont.Families[0], size, FontStyle.Regular, GraphicsUnit.Point);
        }

        private byte[] ReadFontFileFromResource(string name)
        {
            byte[] buffer = null;
            Stream fontStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
            if (fontStream != null)
            {
                buffer = new byte[fontStream.Length];
                fontStream.Read(buffer, 0, (int)fontStream.Length);
                fontStream.Close();
            }

            return buffer;
        }
    }
}
