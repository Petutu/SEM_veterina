using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class PersonalViewModel
    {
        public List<PERSONAL> Personal { get; set; } = new List<PERSONAL>();
        public PERSONAL SelectedPersonal { get; set; }
        public int? KlinikaIdFilter { get; set; } // Pro filtrování podle kliniky
    }
}