using System;
namespace German.Core.CustomResponses
{
	public class Success
	{
		private  bool success;
		private string message;
		public Success(bool success, string message)
		{
			this.success = success;
			this.message = message;
		}
	}
}

