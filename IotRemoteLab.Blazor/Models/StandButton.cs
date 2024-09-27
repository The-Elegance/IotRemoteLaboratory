namespace IotRemoteLab.Blazor.Models
{
    public class StandButton
    {
        public long Id { get; set; }
        public PortType PortType { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
