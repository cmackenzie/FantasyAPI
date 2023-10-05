namespace FantasyProcessor.Services.Models
{
	public class ChannelMessage<T>
	{
		private readonly string _topic;
		private readonly T _payload;

		public ChannelMessage(string topic, T payload)
		{
			_topic = topic;
			_payload = payload;
		}

		public T Payload
		{
			get { return _payload; }
		}

        public string Topic
        {
            get { return _topic; }
        }
    }
}

