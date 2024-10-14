namespace RMRBDClient.Models
{
    public class Province
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        //public List<string> NameExtension { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set;}
        public bool CanUpdateCOD { get; set; }
        public int status { get; set; }
    }
}
