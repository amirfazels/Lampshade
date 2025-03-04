namespace CommentManagement.Application.Contracts.Comment
{
    public class AddComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public long OwnerRecordId { get; private set; }
        public int Type { get; private set; }
        public long ParentId { get; private set; }
    }
}
