﻿namespace IotRemoteLab.Blazor.Models
{
    public class StandLed
    {
        public long Id { get; set; }
        public PortType PortType { get; set; }
        public bool IsEnable { get; set; }
        public string Name { get; set; }
    }
}
