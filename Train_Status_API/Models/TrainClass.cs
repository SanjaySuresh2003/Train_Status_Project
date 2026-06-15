using System;
using System.Collections.Generic;

namespace Train_Status_API.Models;

public partial class TrainClass
{
    public int ClassId { get; set; }

    public string ClassCode { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
