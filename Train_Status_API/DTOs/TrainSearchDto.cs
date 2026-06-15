namespace Train_Status_API.DTOs
{
    public class TrainSearchDto
    {
        public int TrainId { get; set; }
        public string TrainNumber { get; set; } = null!;
        public string TrainName { get; set; } = null!;
        public string? TrainType { get; set; }

        public string SourceStationCode { get; set; } = null!;
        public string SourceStationName { get; set; } = null!;

        public string DestinationStationCode { get; set; } = null!;
        public string DestinationStationName { get; set; } = null!;

        public bool RunsMon { get; set; }
        public bool RunsTue { get; set; }
        public bool RunsWed { get; set; }
        public bool RunsThu { get; set; }
        public bool RunsFri { get; set; }
        public bool RunsSat { get; set; }
        public bool RunsSun { get; set; }
    }
}