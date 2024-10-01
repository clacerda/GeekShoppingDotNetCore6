public class ShipmentRequest
{
    public From from { get; set; }
    public To to { get; set; }
    public Package package { get; set; }
}

public class From
{
    public string postal_code { get; set; }
}

public class To
{
    public string postal_code { get; set; }
}

public class Package
{
    public decimal height { get; set; }
    public decimal width { get; set; }
    public decimal length { get; set; }
    public decimal weight { get; set; }
}
