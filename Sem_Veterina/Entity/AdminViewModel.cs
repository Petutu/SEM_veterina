namespace Sem_Veterina.Entity
{
    public class AdminViewModel
    {
        public List<string> AvailableEntities { get; set; }
        public string SelectedEntity { get; set; }
        public List<KLINIKY> Kliniky { get; set; }
        public List<MAJITELE> Majitele { get; set; }
        public List<ZVIRATA> Zvirata { get; set; }
        public List<PRISTROJE> Pristroje { get; set; }
        public List<ZDRAVOTNIAKCE> ZdravotniAkce { get; set; }
        public List<PERSONAL> Personal { get; set; }
        public List<LEKY> Leky { get; set; }
        public List<LECBY> Lecby { get; set; }
        public List<DIAGNOZY> Diagnozy { get; set; }
        // public KLINIKY SelectedKlinika { get; set; }
    }
}