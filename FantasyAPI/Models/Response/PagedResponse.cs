using FantasyAPI.Adapters;

namespace FantasyAPI.Models.Response
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> items { get; set; }
        public int count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }

        public static PagedResponse<T> ToPagedResponse<T>(int offset, int take, int count, IEnumerable<T> items)
        {
            return new PagedResponse<T>()
            {
                items = items,
                next = offset + take >= count ? null : String.Format("offset={0}&take={1}", offset + take, take),
                previous = offset == 0 ? null : String.Format("offset={0}&take={1}", Math.Max(0, offset - take), take),
                count = count
            };
        }
    }
}

