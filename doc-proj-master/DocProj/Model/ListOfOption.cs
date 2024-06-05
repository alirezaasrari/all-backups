namespace DocProj.Model
{
    public class ListOfOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // [JsonIgnore]
        public virtual ICollection<OptionDetail>? OptionDetail { get; set; }
    }
}
