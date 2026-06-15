using System;
using System.Collections.Generic;

namespace Train_Status_API.Models;

public partial class Booking
{
    public string Pnr { get; set; } = null!;

    public int TrainId { get; set; }

    public DateOnly JourneyDate { get; set; }

    public int SourceStationId { get; set; }

    public int DestinationStationId { get; set; }

    public int ClassId { get; set; }

    public DateTime BookingDate { get; set; }

    public string BookingStatus { get; set; } = null!;

    public virtual TrainClass Class { get; set; } = null!;

    public virtual Station DestinationStation { get; set; } = null!;

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    public virtual Station SourceStation { get; set; } = null!;

    public virtual Train Train { get; set; } = null!;
}
