namespace KampüsSepeti.DTO
{
    public class AnnouncementDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LocationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CommentCount { get; set; }
    }
} 