using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;
using Xunit;

namespace NetworkUtility.Tests.PingTests;

public class NetworkServiceTests
{
	private readonly NetworkService _networkService;

	public NetworkServiceTests()
	{
		// SUT - System Under Test
		_networkService = new NetworkService();
	}
	
	[Fact]
	public void NetworkService_SendPing_ReturnsPing()
	{
		// Arrange
		
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
		var expected = new NetworkPing
		{
			Domain = "https://www.google.com",
			Ping = 22,
			Port = 123,
			SslEnabled = true
		};
		
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
}