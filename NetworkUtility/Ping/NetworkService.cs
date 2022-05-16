namespace NetworkUtility.Ping;

public class NetworkService
{
	public string SendPing()
	{
		return "Ping Sent";
	}

	public int PingTimeout(int a, int b)
	{
		return a + b;
	}

	public DateTime LastPinged()
	{
		return DateTime.Now;
	}

	public NetworkPing GetPingDetails()
	{
		return new NetworkPing
		{
			Domain = "https://www.google.com",
			Ping = 22,
			Port = 123,
			SslEnabled = true
		};
	}
}

public class NetworkPing
{
	public int Ping { get; set; }
	public string Domain { get; set; }
	public int Port { get; set; }
	public bool SslEnabled { get; set; } = false;
}