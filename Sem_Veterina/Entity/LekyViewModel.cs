using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class LekyViewModel
    {
        public List<LEKY> Leky { get; set; } = new List<LEKY>();
        public LEKY SelectedLek { get; set; }
    }
}