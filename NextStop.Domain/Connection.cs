namespace NextStop.Domain;

public class Connection
{
    public DateTime Time { get; set; }
    
    public IList<Route> Routes { get; set; }
    
    public RouteStopPoint? Start { get; set; }
    
    public RouteStopPoint? End { get; set; }
    
    public Connection(DateTime Time, RouteStopPoint? Start, RouteStopPoint? End)
    {
        this.Time = Time;
        this.Routes = new List<Route>();
        this.Start = Start;
        this.End = End;
    }
}