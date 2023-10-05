namespace FantasyCore
{
	public abstract class AdapterBase<T, U>
	{
		public AdapterBase() { }

        public abstract U Convert(T from);

        public IEnumerable<U> Convert(IEnumerable<T> from)
        {
            return from.Select(f => Convert(f)).ToList();
        }
    }
}

