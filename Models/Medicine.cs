using Microsoft.AspNetCore.Http;

namespace Pharmify.Models
{
    public class Medicine
    {
        public string Id { get; set; }
        public byte[] Photo { get; set; }
        public string Title { get; set; }
        public List<string> Description { get; set; }
        public int Category { get; set; }
    }

}
