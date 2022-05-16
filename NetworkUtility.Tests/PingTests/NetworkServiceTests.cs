using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.DNS;
using NetworkUtility.Ping;
using Xunit;

namespace NetworkUtility.Tests.PingTests;

public class NetworkServiceTests
{
	private readonly NetworkService _networkService;
	public readonly IDNS Dns;

	public NetworkServiceTests()
	{
		Dns = A.Fake<IDNS>();
		// SUT - System Under Test
		_networkService = new NetworkService(Dns);
	}


	[Fact]
	public void NetworkService_SendPing_ReturnsPing()
	{
		// Arrange
		A.CallTo(() => Dns.SendDnsRequest()).Returns(true);
		
		// Act
		var result = _networkService.SendPing();

		// Assert
		result
			.Should()
			.NotBeNullOrWhiteSpace();
		
		result
			.Should()
			.Be("Ping Sent");

		result
			.Should()
			.Contain("Sent");
	}

	[Theory]
	[InlineData(1, 2, 3)]
	[InlineData(3, 4, 7)]
	public void NetworkService_SendPing_ReturnsPingTimeout(int a, int b, int expected)
	{
		// Act

		// Arrange
		var result = _networkService.PingTimeout(a, b);

		// Assert
		result
			.Should()
			.Be(expected);
		result
			.Should()
			.BeGreaterOrEqualTo(expected);
		result
			.Should()
			.NotBeInRange(Int32.MinValue, 0, "Ping Should be always positive.");
	}

	[Fact]
	public void NetworkService_LastPinged_ReturnsDate()
	{
		// Arrange
		
		// Act
		var result = _networkService.LastPinged();
		
		// Assert
		result
			.Should()
			.BeAfter(DateTime.Now.AddMinutes(-1));
		result
			.Should()
			.NotBeBefore(15.May(2022));
	}

	[Fact]
	public void NetworkService_SendPing_GetPingDetails()
	{
		// Arrange

		// Act
		var result = _networkService.GetPingDetails();

		// Assert
		result
			.Should()
			.BeOfType<NetworkPing>();
		result.Domain
			.Should()
			.Be("https://www.google.com");
		result.Ping
			.Should()
			.BeLessThan(50);
	}
	
	[Fact]
	public void NetworkService_SendPing_MostRecentPingDetails()
	{
		// Arrange
		var expectedSingle = new NetworkPing
			{Domain = "https://dotnet.microsoft.com/", Ping = 20, Port = 1, SslEnabled = true};

		// Act
		var result = _networkService.MostRecentPingDetails();
		
		// Assert
		result
			.Should()
			.BeOfType<List<NetworkPing>>();
		
		result
			.Should()
			.ContainEquivalentOf(expectedSingle);
		
		result
			.Should()
			.Contain(x => x.SslEnabled == true);

	}
	
}