namespace csharp_response_compression.Models
{
    public class FileModel
    {
        public List<string> Files { get; set; }
        public int Count => Files?.Count ?? 0;
    }
}
