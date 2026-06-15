namespace Train_Status_API.DTOs
{
    public class RouteSearchResultDto
    {
        public int TrainId { get; set; }
        public string TrainNumber { get; set; } = null!;
        public string TrainName { get; set; } = null!;
        public string? TrainType { get; set; }

        public string FromStationCode { get; set; } = null!;
        public string FromStationName { get; set; } = null!;
        public TimeOnly? DepartureTime { get; set; }
        public int FromDay { get; set; }

        public string ToStationCode { get; set; } = null!;
        public string ToStationName { get; set; } = null!;
        public TimeOnly? ArrivalTime { get; set; }
        public int ToDay { get; set; }

        public int DistanceKm { get; set; }
    }
}