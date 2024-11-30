using Sem_Veterina;
namespace Sem_Veterina.Entity
{
    public class LogovaniViewModel
    {
        public List<LOGOVANI> Logovani { get; set; } = new List<LOGOVANI>();
        public LOGOVANI SelectedLog { get; set; }
    }

}