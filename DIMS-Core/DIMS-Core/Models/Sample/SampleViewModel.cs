using System.ComponentModel.DataAnnotations;

namespace DIMS_Core.Models.Sample
{
    public class SampleViewModel
    {
        public int SampleId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}