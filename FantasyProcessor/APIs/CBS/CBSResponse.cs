namespace FantasyProcessor.APIs.CBS
{
    public class CBSResponseBody<T>
    {
        public List<T> players { get; set; }

        public CBSResponseBody()
        {
            players = new List<T>();
        }
    }

	public class CBSResponse<T>
	{
        public CBSResponse()
        {
            body = new CBSResponseBody<T>();
        }

        public CBSResponseBody<T> body { get; set; }
        public int? statusCode { get; set; }
        public string? statusMessage { get; set; }
        public string? uri { get; set; }
        public string? uriAlias { get; set; }
    }
}