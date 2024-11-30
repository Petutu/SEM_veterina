using Sem_Veterina.Entity;
namespace Sem_Veterina.Entity
{
    public class LecbyViewModel
    {
        public List<LECBY> Lecby { get; set; } = new List<LECBY>();
        public LECBY SelectedLecba { get; set; }
    }
}