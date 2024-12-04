using Oracle.ManagedDataAccess.Client;

namespace Sem_Veterina.Entity
{
    public class DIAGNOZY
    {
        public int ID_DIAGNÓZA { get; set; }
        public string NÁZEV { get; set; } // VARCHAR(20)
        public string POPIS { get; set; } // VARCHAR(40)

        // Cizi klice a relace
        public int ID_ZVÍŘE{ get; set; }
        public ZVIRATA ZVÍŘE { get; set; }

        public int ID_PERSONÁL { get; set; }
        //V databázi se to jmenuje Id_PRESONÁL
        public PERSONAL PERSONÁL { get; set; }

        public ICollection<LECBY> LÉČBY { get; set; }
        public int ID_MAJITEL { get;  set; }
        public MAJITELE MAJITEL { get; set; }
        public int ID_KLINIKA { get; set; }
        public KLINIKY KLINIKA { get; set; }
        //public LECBY LÉČBA { get; set; }
    }
}
