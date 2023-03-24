using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Lightless.Music
{
    /// <summary>
    /// Interaction logic for WaveVisualizer.xaml
    /// </summary>
    public partial class WaveVisualizer : UserControl
    {
        public WaveVisualizer()
        {
            InitializeComponent();

            DataContext = this;


        }

        public Geometry WaverPath
        {
            get { return (Geometry)GetValue(WaverPathProperty); }
            set
            {
                if (updateCompleted)
                {
                    DispatcherOperation update = this.Dispatcher.BeginInvoke(new Action(() =>
                    {

                        SetValue(WaverPathProperty, value);
                    }

                                ), DispatcherPriority.ApplicationIdle);
                    update.Completed += new EventHandler(update_Completed);
                }
            }
        }


        public static readonly DependencyProperty WaverPathProperty = DependencyProperty.Register(
            "WaverPath",
            typeof(Geometry),
            typeof(WaveVisualizer),
            new PropertyMetadata(Geometry.Parse("M 0,0 250,0"), new PropertyChangedCallback(WaverPathChanged))
        );



        //callback
        private static void WaverPathChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            WaveVisualizer m = (WaveVisualizer)obj;
            m.WaverPathChanged(e);
        }

        //callback
        protected virtual void WaverPathChanged(DependencyPropertyChangedEventArgs e)
        {


        }

        public bool fromStream = false;

        private bool updateCompleted = true;


        public void UpdateData(double[] Data)
        {
            //var aaa = new List<double>();
            //double bbb = 0;
            //for (int i = 0; i < 440; i++)
            //{
            //    bbb += 0.004;
            //    aaa.Add(bbb);
            //}

            //Data = aaa.ToArray();
            //var data2 = FFT(Data);
            if (!Data.Any()) return;
            

            if (updateCompleted)
            {

                string PathData = "M ";
                for (int i = 0, j = 0; j <= Data.Length*4-2; i += 1, j+=4)
                {


                    //if (i > 240 || Data[i] == 0)
                    //{

                    //    //if (Data[i] < 0) Data[i] = -Data[i];

                    //    PathData += (j + " " + (-Data[i] * 50));
                    //    break;
                    //}
                    //else
                    //{
                    //    //if (Data[i] < 0) Data[i] = -Data[i];
                    double data = 0;
                    data = Data[i];
                    if (data > 0) data = -data;
                    if (data > 150) data = 150;
                    if (data < -150) data = -150;

                    PathData += (j + " " + 0 + ",");
                    PathData += (j + " " + data + ",");
                    PathData += (j+2 + " " + data + ",");
                    PathData += (j + 2 + " " + 0 + ",");
                    //}
                }
                //PathData.Remove(PathData.Length - 2, 1);
                // PathData += "260,0";
                PathData = PathData.Remove(PathData.Length - 1, 1);
                WaverPath = Geometry.Parse(PathData);


            }
        }
        //int distance2Node = 0;
      
        //private const int samplingFrequency = 44100;
        //int[] chunk_freq = { 800, 1600, 3200, 6400, 12800, 30000 };
        //int[] chunk_freq_jump = { 1, 2, 4, 6, 8, 10, 16 };
        //double lastDelay = 0;
        //private double[] FFT(double[] lastData)
        //{
        //    DateTime chkpoint1 = DateTime.Now;
        //    int actualN = distance2Node + 1;

        //    bool transformed = false;
        //    if (lastData == null || lastData.Length == 0)
        //    {
        //        lastData = new double[2048];
        //    }
        //    else
        //    {
        //        transformed = true;
        //    }
        //    ComplexNumber[] data = new ComplexNumber[2048];
        //    //var runningNode = endingNode;
        //    for (int i = 0; i < N_FFT; i++)
        //    {
        //        data[i] = runningNode.Value;
        //        if (runningNode.PrevNode == null)
        //        {
        //        }
        //        runningNode = runningNode.PrevNode;
        //    }
        //    var result = lastData;
        //    double N2 = result.Length / 2;
        //    double[] finalresult = new double[lastData.Length];
        //    int k = 1, transformedDataIndex = 0;
        //    double value = 0;

        //    double refFeq = 250;

        //    int i_ref = (int)(refFeq * N2 / 22050);
        //    for (int i = 0; i < N2; i += k)
        //    {
        //        value = 0;
        //        //k = i / i_ref;
        //        //k = k == 0 ? 1 : k;
        //        var mappedFreq = i * samplingFrequency / 2 / N2;
        //        for (int l = 0; l < chunk_freq.Length; l++)
        //        {
        //            if (mappedFreq < chunk_freq[l] || l == chunk_freq.Length - 1)
        //            {
        //                k = chunk_freq_jump[l];//chunk_freq[l] / chunk_freq[0];
        //                break;
        //            }
        //        }

        //        for (int j = i; j < i + k && j < N2; j++)
        //        {
        //            value += result[j].Manigtude;
        //        }


        //        value = value * 1;
        //        lastData[transformedDataIndex] -= lastDelay * 0.4;
        //        finalresult[transformedDataIndex] = value;
        //        transformedDataIndex++;
        //    }


        //    if (!transformed)
        //        Array.Resize<double>(ref finalresult, transformedDataIndex);


        //    DateTime chkpoint1_end = DateTime.Now;
        //    lastDelay = chkpoint1_end.Subtract(chkpoint1).TotalMilliseconds;
        //    return finalresult;
        //}
        //public static ComplexNumber[] FFT(ComplexNumber[] data)
        //{
        //    int N = data.Length;
        //    ComplexNumber[] X = new ComplexNumber[N];
        //    ComplexNumber[] e, E, d, D;
        //    if (N == 1)
        //    {
        //        X[0] = data[0];
        //        return X;
        //    }

        //    int k = 0;
        //    e = new ComplexNumber[N / 2];
        //    d = new ComplexNumber[N / 2];
        //    for (k = 0; k < N / 2; k++)
        //    {
        //        e[k] = data[k * 2];
        //        d[k] = data[k * 2 + 1];
        //    }
        //    E = FFT(e);
        //    D = FFT(d);

        //    for (k = 0; k < N / 2; k++)
        //    {
        //        D[k] *= ComplexNumber.FromPolar(1, -2 * Math.PI * k / N);
        //    }

        //    for (k = 0; k < N / 2; k++)
        //    {
        //        X[k] = E[k] + D[k];
        //        X[k + N / 2] = E[k] - D[k];
        //    }
        //    return X;
        //}
        void update_Completed(object sender, EventArgs e)
        {
            updateCompleted = true;
        }
    }
}
