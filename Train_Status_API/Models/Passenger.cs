using System;
using System.Collections.Generic;

namespace Train_Status_API.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string Pnr { get; set; } = null!;

    public string PassengerName { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string? SeatNumber { get; set; }

    public string? CoachNumber { get; set; }

    public string PassengerStatus { get; set; } = null!;

    public virtual Booking PnrNavigation { get; set; } = null!;
}
