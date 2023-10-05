using System;
namespace FantasyProcessor.APIs.CBS
{
    public class Icons
    {
        public string? injury { get; set; }
        public string? headline { get; set; }
    }

	public class Player
	{
        public string fullname { get; set; }
        public string elias_id { get; set; }
        public string pro_status { get; set; }
        public string firstname { get; set; }
        public string photo { get; set; }
        public string pro_team { get; set; }
        public string lastname { get; set; }
        public string position { get; set; }
        public string id { get; set; }
        public string jersey { get; set; }
        public int age { get; set; }
        public Icons icons { get; set; }
    }
}