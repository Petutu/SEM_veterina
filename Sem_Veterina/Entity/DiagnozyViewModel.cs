using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class DiagnozyViewModel
    {
        public List<DIAGNOZY> Diagnozy { get; set; } = new List<DIAGNOZY>();
        public DIAGNOZY SelectedDiagnoza { get; set; }
    }
}