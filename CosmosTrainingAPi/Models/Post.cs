using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeMVCApplication.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int userID { get; set; }
    }
}
