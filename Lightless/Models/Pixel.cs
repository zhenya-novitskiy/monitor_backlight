namespace Lightless
{
    public class Pixel 
    {
        public Pixel()
        {
        }

        public Pixel(int position, byte r, byte g, byte b)
        {
            Position = position;
            R = r;
            G = g;
            B = b;
        }

        public int Position;
        public byte R;
        public byte G;
        public byte B;
    }
}