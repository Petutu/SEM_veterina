using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class PristrojeViewModel
    {
        public List<PRISTROJE> Pristroje { get; set; } = new List<PRISTROJE>();
        public PRISTROJE SelectedPristroj { get; set; }
        public int? KlinikaIdFilter { get; set; } // Pro filtrování podle kliniky
    }
}