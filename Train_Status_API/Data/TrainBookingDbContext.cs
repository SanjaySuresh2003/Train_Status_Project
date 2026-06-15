using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Train_Status_API.Models;

namespace Train_Status_API.Data;

public partial class TrainBookingDbContext : DbContext
{
    public TrainBookingDbContext()
    {
    }

    public TrainBookingDbContext(DbContextOptions<TrainBookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<TrainClass> TrainClasses { get; set; }

    public virtual DbSet<TrainRoute> TrainRoutes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=TrainBookingDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Pnr).HasName("PK__Bookings__C5773DD29B32C286");

            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Confirmed");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.DestinationStationId).HasColumnName("DestinationStationID");
            entity.Property(e => e.SourceStationId).HasColumnName("SourceStationID");
            entity.Property(e => e.TrainId).HasColumnName("TrainID");

            entity.HasOne(d => d.Class).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Class");

            entity.HasOne(d => d.DestinationStation).WithMany(p => p.BookingDestinationStations)
                .HasForeignKey(d => d.DestinationStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_DestStation");

            entity.HasOne(d => d.SourceStation).WithMany(p => p.BookingSourceStations)
                .HasForeignKey(d => d.SourceStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_SourceStation");

            entity.HasOne(d => d.Train).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Train");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F90E793CA3F");

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.CoachNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PassengerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PassengerStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Confirmed");
            entity.Property(e => e.Pnr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PNR");
            entity.Property(e => e.SeatNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.PnrNavigation).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.Pnr)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Passengers_Booking");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__E0D8A6DDD3790AAF");

            entity.HasIndex(e => e.StationCode, "UQ__Stations__D38856183CF82E88").IsUnique();

            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StationCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.StationName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.TrainId).HasName("PK__Trains__8ED2725A1CCC3175");

            entity.HasIndex(e => e.TrainNumber, "UQ__Trains__10C2CD2F9E3D89B5").IsUnique();

            entity.Property(e => e.TrainId).HasColumnName("TrainID");
            entity.Property(e => e.DestinationStationId).HasColumnName("DestinationStationID");
            entity.Property(e => e.SourceStationId).HasColumnName("SourceStationID");
            entity.Property(e => e.TrainName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TrainNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TrainType)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.DestinationStation).WithMany(p => p.TrainDestinationStations)
                .HasForeignKey(d => d.DestinationStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trains_DestStation");

            entity.HasOne(d => d.SourceStation).WithMany(p => p.TrainSourceStations)
                .HasForeignKey(d => d.SourceStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trains_SourceStation");
        });

        modelBuilder.Entity<TrainClass>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__TrainCla__CB1927A02DE7AD9B");

            entity.HasIndex(e => e.ClassCode, "UQ__TrainCla__2ECD4A55DEC54343").IsUnique();

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrainRoute>(entity =>
        {
            entity.HasKey(e => e.RouteId).HasName("PK__TrainRou__80979AADFFA86D25");

            entity.Property(e => e.RouteId).HasColumnName("RouteID");
            entity.Property(e => e.DayNumber).HasDefaultValue(1);
            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.TrainId).HasColumnName("TrainID");

            entity.HasOne(d => d.Station).WithMany(p => p.TrainRoutes)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainRoutes_Station");

            entity.HasOne(d => d.Train).WithMany(p => p.TrainRoutes)
                .HasForeignKey(d => d.TrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainRoutes_Train");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
