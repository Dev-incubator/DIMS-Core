namespace DIMS_Core.Models
{
    public class DeleteUserViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }

        public DeleteUserViewModel(int UserId, string FullName)
        {
            this.UserId = UserId;
            this.FullName = FullName;
        }
    }
}