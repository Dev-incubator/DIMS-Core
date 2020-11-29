namespace DIMS_Core.Models.Route
{
    public class BottomButtonsViewModel
    {
        public IdWithName Id { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public IdWithName BackId { get; set; }
        public string BackAction { get; set; }
        public string BackController { get; set; }
    }
}
