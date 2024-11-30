using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class ZvirataViewModel
    {
        public List<ZVIRATA> Zvirata { get; set; } = new List<ZVIRATA>();
        public ZVIRATA SelectedZvire { get; set; }
    }
}