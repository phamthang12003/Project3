namespace E_PROJECT_MANAGER.DataTransferObject
{
	public class RequestUngVienFullDetail
	{
		public int? Id { get; set; } = 0;
		public string? fullName { get; set; } = "";
		public string? phoneNumber { get; set; } = "";
		public string? email { get; set; } = "";
		public string? jobSector { get; set; } = "";
		public string? urlFile { get; set; } = "";
		public string? commentMessage { get; set; } = "";

		public int? Age { get; set; } = 0;
		public string? Sex { get; set; } = "";
		public string? Address { get; set; } = "";
	}
}
