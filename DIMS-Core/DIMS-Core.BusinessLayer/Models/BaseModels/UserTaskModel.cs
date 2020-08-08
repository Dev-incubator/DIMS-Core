namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class UserTaskModel : BaseDTOModel
    {
        public int? UserTaskId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }

        protected internal override int PKId => UserTaskId.Value;
    }
}