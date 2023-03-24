using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Lightless.Arduino
{

    public class ArduinoColorData : IArduinoData
    {
        public SendPriority SendPriority { get; set; }
        public byte[] ArduinoData { get; set; }
        public int Id { get; set; }

        public ArduinoColorData(int id, IEnumerable<Pixel> pixelData, SendPriority priority)
        {
            Id = id;
            SendPriority = priority;

            var data = new List<byte> {(byte)Id};

            foreach (var color in pixelData)
            {
                data.Add(color.R);
                data.Add(color.G);
                data.Add(color.B);
            }
            ArduinoData = data.ToArray();

        }
    }
    public interface IArduinoData
    {
        SendPriority SendPriority { get; set; }
        byte[] ArduinoData { get; set; }

        int Id { get; set; }
    }
   
    public enum Module
    {
        LedDiods = 0
    }
    public enum ArduinoStatus
    {
        Connected = 0,
        Disconnected = 1
    }
    public class OnArduinoConnectionStatusChangedArgs : EventArgs
    {
        public ArduinoStatus ArduinoStatus { get; set; }
    }
    public class OnVolumeChangedArgs : EventArgs
    {
        public int VolumeLevel { get; set; }
    }
    public class OnDataSended : EventArgs
    {
        public Module Module { get; set; }
        public byte[] Data { get; set; }
    }
    public enum SendPriority
    {
        Argent,
        Higth,
        Medium,
        Low
    }
}
