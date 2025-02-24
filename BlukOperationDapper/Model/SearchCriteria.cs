namespace BlukOperationDapper.Model
{
    public class SearchCriteria
    {
        public IEnumerable<int>? Ids { get; set; } = default;
        public IEnumerable<string>? Names { get; set; } = default;

        public bool? isActive { get; set; } = default;
    }
}
