namespace NextStop.Domain;

/// <summary>
/// Represents geographical coordinates with latitude and longitude values.
/// </summary>
public class Coordinates
{

    public Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    
    public Coordinates() {}
    
    /// <summary>
    /// Gets or sets the latitude of the coordinates.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the coordinates.
    /// </summary>
    public double Longitude { get; set; }

    public override string ToString()
    {
        return $"Latitude: {Latitude}, Longitude: {Longitude}";
    }
}