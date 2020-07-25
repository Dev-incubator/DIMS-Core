namespace DIMS_Core.BusinessLayer.Models.BaseModels
{
    public class DirectionModel : BaseDTOModel
    {
        public int DirectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        protected internal override int PKId => DirectionId;
    }
}