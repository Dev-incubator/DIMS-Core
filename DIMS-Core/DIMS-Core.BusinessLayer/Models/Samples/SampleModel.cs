using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.BusinessLayer.Models.Samples
{
    public class SampleModel
    {
        public int SampleId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}