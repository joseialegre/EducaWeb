namespace WebApplication1.Models
{
    public class Novedad
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string Publisher { get; set; }

        public DateTime Date { get; set; }
        public Novedad()
        {

        }
    }
}
