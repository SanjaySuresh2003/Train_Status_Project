using System;
using System.Collections.Generic;

namespace Train_Status_API.Models;

public partial class TrainRoute
{
    public int RouteId { get; set; }

    public int TrainId { get; set; }

    public int StationId { get; set; }

    public int SequenceNumber { get; set; }

    public TimeOnly? ArrivalTime { get; set; }

    public TimeOnly? DepartureTime { get; set; }

    public int DayNumber { get; set; }

    public int DistanceFromSource { get; set; }

    public virtual Station Station { get; set; } = null!;

    public virtual Train Train { get; set; } = null!;
}
