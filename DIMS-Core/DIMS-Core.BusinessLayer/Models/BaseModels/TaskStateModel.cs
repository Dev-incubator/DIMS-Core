namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class TaskStateModel : BaseDTOModel
    {
        public int? StateId { get; set; }
        public string StateName { get; set; }

        protected internal override int PKId => StateId.Value;
    }
}