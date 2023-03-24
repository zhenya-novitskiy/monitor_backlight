namespace Lightless.Animation
{
    public class TranslateColorContainer
    {
        public Pixel CurrentColor;
        public TransactionDelta TransactionDelta;

        public void SubstructDelta()
        {

            var substructedValue = CurrentColor.R + TransactionDelta.R;
            CurrentColor.R = substructedValue < 0 ? (byte)0 : (byte)substructedValue;

            substructedValue = CurrentColor.G + TransactionDelta.G;
            CurrentColor.G = substructedValue < 0 ? (byte)0 : (byte)substructedValue;

            substructedValue = CurrentColor.B + TransactionDelta.B;
            CurrentColor.B = substructedValue < 0 ? (byte)0 : (byte)substructedValue;
        }
    }
}