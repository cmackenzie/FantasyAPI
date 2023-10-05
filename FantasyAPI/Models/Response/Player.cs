namespace FantasyAPI.Models.Response
{
    public class Player
    {
        public int id { get; set; }
        public string name_brief { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string position { get; set; }
        public int age { get; set; }
        public int average_position_age_diff { get; set; }
    }
}

