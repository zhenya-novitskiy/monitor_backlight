using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using Lightless.Arduino;
using Lightless.Components;
using Lightless.CustomEventArgs;
using Lightless.Music;
using Microsoft.Win32;
using NAudio.Wave;

namespace Lightless
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Converter cvt, cvt2 = null;

        private const int samplingFrequency = 44100;
        private const int numberValues = 1000;
        Node dataList = new Node(new ComplexNumber(0, 0));
        double[] sourceData;
        double[] transformedData;
        WaveIn waveInStream;
        private WaveMixerStream32 mixer;
        private static int fftLength = 256;
        private SampleAggregator sampleAggregator = new SampleAggregator(fftLength);

        //OSD osdPanel = new OSD();

        #region Settings
        public int Mode
        {
            get;
            set;
        }
        public double MasterScaleFFT
        {
            get;
            set;
        }
        public double DropOffScale
        {
            get;
            set;
        }
        public int N_FFT
        {
            get;
            set;
        }
        #endregion

        #region Effect related variables

        Image mainBuffer;
        Graphics gMainBuffer;

        Image tempBuffer;
        Graphics gTempBuffer;

        ColorMatrix colormatrix = new ColorMatrix(new float[][]
                        {
                            new float[]{1, 0, 0, 0, 0},
                            new float[]{0, 1, 0, 0, 0},
                            new float[]{0, 0, 1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                          //  new float[]{0, 0, 0, -0.001f, 1},
                            new float[]{-0.01f, -0.01f, -0.01f, 0, 0}
                          //  new float[]{0, 0, 0, 0, 1}
                        });
        ImageAttributes imgAttribute;

        #endregion 
        private IWaveIn waveIn;

        public MainWindow()
        {
            
            InitializeComponent();
            
            AddButton.Init("../Images/add2.png", "../Images/add1.png");
            RenameButton.Init("../Images/rename2.png", "../Images/rename1.png");
            SaveButton.Init("../Images/save2.png", "../Images/save1.png");
            DeleteButton.Init("../Images/delete2.png", "../Images/delete1.png");

            AddButton.MouseLeftButtonDown += Add_Click;
            RenameButton.MouseLeftButtonDown += Rename_Click;
            SaveButton.MouseLeftButtonDown += Save_Click;
            DeleteButton.MouseLeftButtonDown += Delete_Click;

           // Hide();
            ArduinoCommunication.OnArduinoConnectionStatusChanged += ArduinoCommunicationOnOnArduinoConnectionStatusChanged;

            var themesList = ThemesManager.GetThemesList();
            ThemesList.Load(themesList);
            ThemesList.ThemeSelected += ThemesList_ThemeSelected;

           
            
            AnimationManager.PixelModelCollection = LedSurfaceControl.Leds;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            //AllpyTheme(Configuration.Get<string>(ConfigurationData.CurrentTheme));
            Closing += MainWindow_Closing;

            InitSoundCapture();
        }

        private void InitSoundCapture()
        {

            sampleAggregator.FftCalculated += new EventHandler<FftEventArgs>(FftCalculated);
            sampleAggregator.PerformFFT = true;

            endingNode = dataList;

            waveInStream = new WaveIn();
            waveInStream.NumberOfBuffers = 5;
            waveInStream.BufferMilliseconds = 10;
            waveInStream.WaveFormat = new WaveFormat(samplingFrequency, 1);
            waveInStream.DataAvailable += new EventHandler<WaveInEventArgs>(waveInStream_DataAvailable);
            waveInStream.StartRecording();

            Mode = 1;
            MasterScaleFFT = 1;
            DropOffScale = 2;
            N_FFT = 2048;

            BeatDetector.InitDetector(50);
        }

        int distance2Node = 0;
        Node endingNode;
        int[] chunk_freq = { 800, 1600, 3200, 6400, 12800, 30000 };
        int[] chunk_freq_jump = { 1, 2, 4, 6, 8, 10, 16 };

        private void waveInStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            //if (sourceData == null)
            //    sourceData = new double[e.BytesRecorded / 2];

            //for (int i = 0; i < e.BytesRecorded; i += 2)
            //{
            //    short sampleL = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i + 0]);
            //    //  short sampleR = (short)((e.Buffer[i + 1+2] << 8) | e.Buffer[i + 2]);
            //    double sample32 = (sampleL) / 32722d;
            //    sourceData[i / 2] = sample32;// (double)(e.Buffer[i]) / 255;
            //}

            //if (sourceData == null)
            //    sourceData = new double[e.BytesRecorded / 2];

            //for (int i = 0; i < e.BytesRecorded; i += 2)
            //{
            //    short sampleL = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i + 0]);
            //    //  short sampleR = (short)((e.Buffer[i + 1+2] << 8) | e.Buffer[i + 2]);
            //    double sample32 = (sampleL) / 32722d;
            //    sourceData[i / 2] = sample32;// (double)(e.Buffer[i]) / 255;

            //}

            //for (int i = 0; i < sourceData.Length; i++)
            //{
            //    sampleAggregator.Add((float)sourceData[i]);
            //}


            //byte[] buffer = e.Buffer;
            //int bytesRecorded = e.BytesRecorded;
            //int bufferIncrement = waveIn.WaveFormat.BlockAlign;

            //for (int index = 0; index < bytesRecorded; index += bufferIncrement)
            //{
            //    float sample32 = BitConverter.ToSingle(buffer, index);
            //    sampleAggregator.Add(sample32);
            //}



            //Int32 sample_count = e.BytesRecorded / (waveIn.WaveFormat.BitsPerSample / 8);
            //Single[] data = new Single[sample_count];

            //for (int i = 0; i < sample_count; ++i)
            //{
            //    data[i] = BitConverter.ToSingle(e.Buffer, i * 4);
            //}

            //int j = 0;
            //var Audio_Samples = new Double[sample_count / 2];
            //for (int sample = 0; sample < data.Length; sample += 2)
            //{
            //    Audio_Samples[j] = (Double)data[sample];
            //    Audio_Samples[j] += (Double)data[sample + 1];
            //    ++j;
            //}
            //AppendData(Audio_Samples);

            if (sourceData == null)
                sourceData = new double[e.BytesRecorded / 2];

            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                short sampleL = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i + 0]);
                //  short sampleR = (short)((e.Buffer[i + 1+2] << 8) | e.Buffer[i + 2]);
                double sample32 = (sampleL) / 32722d;
                sourceData[i / 2] = sample32;// (double)(e.Buffer[i]) / 255;
            }

            AppendData(sourceData);
        }

        void FftCalculated(object sender, FftEventArgs e)
        {
            AppendData(e.Result.Select(x=>(double)x.X).ToArray());
        }

        private void AppendData(double[] newData)
        {
            //for (int i = 0, j = 0; j <= 1022; i += 1, j += 2)
            //{


            //    //if (i > 240 || Data[i] == 0)
            //    //{

            //    //    //if (Data[i] < 0) Data[i] = -Data[i];

            //    //    PathData += (j + " " + (-Data[i] * 50));
            //    //    break;
            //    //}
            //    //else
            //    //{
            //    //    //if (Data[i] < 0) Data[i] = -Data[i];
            //    double data2 = 0;
            //    data2 = data[i] * 6000;
            //    if (data2 < 0) data2 = -data2;
            //    if (data2 > 255) data2 = 255;
            //    data[i] = data2;

            //    //}
            //}
         

            int N = 10000;
            //double[] data = new double[N];

            var prevNode = dataList;
            var shiftNode = dataList;


            for (int j = 0; j < newData.Length; j++)
            {
                endingNode.NextNode = new Node(new ComplexNumber(newData[j], 0));
                endingNode.NextNode.PrevNode = endingNode;
                endingNode = endingNode.NextNode;
                if (j == newData.Length - 1)
                    endingNode.isEndPoint = true;
                // data[thresholdCounter] = runningNode.Value;
                distance2Node++;
            }
            if (distance2Node > N)
            {
                for (int j = 0; j < newData.Length; j++)
                {
                    dataList = dataList.NextNode;
                }
                dataList.isStartPoint = true;
                dataList.PrevNode = null;
                distance2Node = distance2Node - newData.Length;
            }

            transformedData = FFT(transformedData);
            if (transformedData.Any())
            {
                WaveVisualizer.UpdateData(transformedData);
            }


            Do(transformedData.ToList());


        }

        private void Do(List<double> data)
        {
            double max = 0;
            if (data.Any())
            {
                max = data.Take(15).Max();
                if (max < 50)
                {
                    max = 0;
                }
            }
            LedSurfaceControl.Leds.Update(() =>
            {
                LedSurfaceControl.Leds.Where(x=> LedFilter.Top1(x) || LedFilter.Top2(x) || LedFilter.Bottom1(x) || LedFilter.Bottom2(x)).ToList().ForEach(x => x.PixelData = new Pixel(x.Index, (byte)max, 0, 0));
            });

            if (data.Any())
            {
                max = data.Skip(30).Take(25).Max();
                if (max < 20) max = 0;
            }
            LedSurfaceControl.Leds.Update(() =>
            {
                LedSurfaceControl.Leds.Where(x => LedFilter.Left1WithCorners(x) || LedFilter.Right2WithCorners(x)).ToList().ForEach(x => x.PixelData = new Pixel(x.Index, 0, (byte)(max), (byte)(max*2)));
            });
        }

        double lastDelay = 0;
        private double[] FFT(double[] lastData)
        {
            DateTime chkpoint1 = DateTime.Now;
            if (dataList == null)
                return null;
            int actualN = distance2Node + 1;

            if (actualN < N_FFT)
                return new double[0];

            bool transformed = false;
            if (lastData == null || lastData.Length == 0)
            {
                lastData = new double[N_FFT];
            }
            else
            {
                transformed = true;
            }
            ComplexNumber[] data = new ComplexNumber[N_FFT];
            var runningNode = endingNode;
            for (int i = 0; i < N_FFT; i++)
            {
                data[i] = runningNode.Value;
                if (runningNode.PrevNode == null)
                {
                }
                runningNode = runningNode.PrevNode;
            }
            var result = FFTProcessor.FFT(data);
            double N2 = result.Length / 2;
            double[] finalresult = new double[lastData.Length];
            int k = 1, transformedDataIndex = 0;
            double value = 0;

            double refFeq = 250;

            int i_ref = (int)(refFeq * N2 / 22050);
            for (int i = 0; i < N2; i += k)
            {
                value = 0;
                //k = i / i_ref;
                //k = k == 0 ? 1 : k;
                var mappedFreq = i * samplingFrequency / 2 / N2;
                for (int l = 0; l < chunk_freq.Length; l++)
                {
                    if (mappedFreq < chunk_freq[l] || l == chunk_freq.Length - 1)
                    {
                        k = chunk_freq_jump[l];//chunk_freq[l] / chunk_freq[0];
                        break;
                    }
                }

                for (int j = i; j < i + k && j < N2; j++)
                {
                    value += result[j].Manigtude;
                }


                value = value * MasterScaleFFT;
                lastData[transformedDataIndex] -= lastDelay * DropOffScale;
                if (Mode == 0)
                    finalresult[transformedDataIndex] = value;
                else
                    finalresult[transformedDataIndex] = value > lastData[transformedDataIndex] ? value : lastData[transformedDataIndex];
                transformedDataIndex++;
            }


            if (!transformed)
                Array.Resize<double>(ref finalresult, transformedDataIndex);


            DateTime chkpoint1_end = DateTime.Now;
            lastDelay = chkpoint1_end.Subtract(chkpoint1).TotalMilliseconds;
            return finalresult;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    AnimationManager.TranslateNow(ThemesManager.LoadTheme(Configuration.Get<string>(ConfigurationData.CurrentTheme)));
                    break;
                case PowerModes.StatusChange:
                    break;
                case PowerModes.Suspend:
                    AnimationManager.TournOff();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.ConsoleDisconnect:
                case SessionSwitchReason.RemoteDisconnect:
                case SessionSwitchReason.SessionLock:
                case SessionSwitchReason.SessionLogoff:
                    AnimationManager.TournOff();
                    break;
                case SessionSwitchReason.ConsoleConnect:
                case SessionSwitchReason.RemoteConnect:
                case SessionSwitchReason.SessionLogon:
                case SessionSwitchReason.SessionUnlock:
                case SessionSwitchReason.SessionRemoteControl:
                    AnimationManager.TranslateNow(ThemesManager.LoadTheme(ThemesManager.GetRandomThemeName()));
                    break;
                default:
                    break;
            }
        }

        private void ThemesList_ThemeSelected(object sender, StringEventArgs e)
        {
            AllpyTheme(e.Value);
            Configuration.Set(ConfigurationData.CurrentTheme, e.Value);
        }

        private void ArduinoCommunicationOnOnArduinoConnectionStatusChanged(object sender, OnArduinoConnectionStatusChangedArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() => { StatusButton.Content = e.ArduinoStatus == ArduinoStatus.Connected ? "True" : "False"; }), DispatcherPriority.Send);
        }

        private void ColorPicker_OnColorChanged(object sender, ColorChangeEventArgs e)
        {
            LedSurfaceControl.Leds.Update(() =>
            {
                LedSurfaceControl.Leds.Where(x => x.Selected).ToList().ForEach(x => x.PixelData = new Pixel(x.Index, e.Color.R, e.Color.G, e.Color.B));
            });
        }

        public void SaveCurrentTheme(string themeName = null)
        {
            var item = (ThemesList.PlayListContainer.SelectedItem as ThemeItem);
            if (item != null || themeName != null)
            {
                ThemesManager.Save(themeName ?? item.ThemeName.Text, LedSurfaceControl.Leds.Select(x => x.PixelData));
            }
        }

        public void AllpyTheme(string name)
        {
            var themeData = ThemesManager.LoadTheme(name);

            AnimationManager.TranslateToColor(themeData);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveCurrentTheme();
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            var item = (ThemesList.PlayListContainer.SelectedItem as ThemeItem);
            if (item != null)
            {
                var dialog = new InputDialog {ResponseText = item.ThemeName.Text};
                if (dialog.ShowDialog() == true)
                {
                    var newName = dialog.ResponseText;
                    ThemesManager.DeleteTheme(item.ThemeName.Text);
                    item.ThemeName.Text = newName;
                    SaveCurrentTheme();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var item = (ThemesList.PlayListContainer.SelectedItem as ThemeItem);
            if (item != null)
            {
                ThemesManager.DeleteTheme(item.ThemeName.Text);
                ThemesList.PlayListContainer.Items.Remove(item);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InputDialog {};
            if (dialog.ShowDialog() == true)
            {
                ThemesList.PlayListContainer.Items.Add(new ThemeItem(ThemesList.PlayListContainer.Items.Count + 1, dialog.ResponseText));
                ThemesList.PlayListContainer.SelectedIndex = ThemesList.PlayListContainer.Items.Count;
                SaveCurrentTheme(dialog.ResponseText);
            }
        }
    }
}
