using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI
{
    public class SuperHero
    {
        [Key]
        public int SHID { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public int Edad { get; set; }
    }
}
