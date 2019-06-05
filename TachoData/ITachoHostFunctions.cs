using System;
namespace TachoData
{
	/// <summary>
	/// The interface used to get a formatted timestamp string from the Timestamp Service.
	/// </summary>
	public interface IHostTachoFunctions
	{
		string GetTimestamp();
	}
}
