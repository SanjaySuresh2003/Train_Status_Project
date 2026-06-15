using System;
using System.Collections.Generic;

namespace Train_Status_API.Models;

public partial class Station
{
    public int StationId { get; set; }

    public string StationCode { get; set; } = null!;

    public string StationName { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? State { get; set; }

    public virtual ICollection<Booking> BookingDestinationStations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingSourceStations { get; set; } = new List<Booking>();

    public virtual ICollection<Train> TrainDestinationStations { get; set; } = new List<Train>();

    public virtual ICollection<TrainRoute> TrainRoutes { get; set; } = new List<TrainRoute>();

    public virtual ICollection<Train> TrainSourceStations { get; set; } = new List<Train>();
}
