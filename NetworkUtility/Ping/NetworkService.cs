using NetworkUtility.DNS;

namespace NetworkUtility.Ping;

public class NetworkService
{
	private readonly IDNS _dns;

	public NetworkService(IDNS dns)
	{
		_dns = dns;
	}
	
	public string SendPing()
	{
		var dnsResult = _dns.SendDnsRequest();
		if (dnsResult)
			return "Ping Sent";
		return "Ping Failed to send.";
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

	public List<NetworkPing> MostRecentPingDetails()
	{
		var res = new List<NetworkPing>()
		{
			new() { Domain = "https://dotnet.microsoft.com/", Ping = 20, Port = 1, SslEnabled = true },
			new() { Domain = "https://dotnet.microsoft.com/en-us/apps/aspnet", Ping = 30, Port = 5001, SslEnabled = true },
			new() { Domain = "https://dotnet.microsoft.com/en-us/apps/maui", Ping = 62, Port = 8162, SslEnabled = true }
		};
		return res;
	}
}

public class NetworkPing
{
	public int Ping { get; set; }
	public string Domain { get; set; }
	public int Port { get; set; }
	public bool SslEnabled { get; set; } = false;
}