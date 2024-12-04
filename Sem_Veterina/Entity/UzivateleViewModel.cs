using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class UzivateleViewModel
    {
        public List<UZIVATEL> Uzivatele { get; set; } = new List<UZIVATEL>();
        public UZIVATEL SelectedUzivatel { get; set; }

        public List<ROLE> Role { get; set; } = new List<ROLE>(); // Možný seznam rolí pro výběr
    }
}