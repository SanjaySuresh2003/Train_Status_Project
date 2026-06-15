using System;
using System.Collections.Generic;

namespace Train_Status_API.Models;

public partial class Train
{
    public int TrainId { get; set; }

    public string TrainNumber { get; set; } = null!;

    public string TrainName { get; set; } = null!;

    public int SourceStationId { get; set; }

    public int DestinationStationId { get; set; }

    public string? TrainType { get; set; }

    public bool RunsMon { get; set; }

    public bool RunsTue { get; set; }

    public bool RunsWed { get; set; }

    public bool RunsThu { get; set; }

    public bool RunsFri { get; set; }

    public bool RunsSat { get; set; }

    public bool RunsSun { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Station DestinationStation { get; set; } = null!;

    public virtual Station SourceStation { get; set; } = null!;

    public virtual ICollection<TrainRoute> TrainRoutes { get; set; } = new List<TrainRoute>();
}
