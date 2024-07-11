using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;

namespace Harjoitustyö
{
    public class Tuotteet
    {
        public ObservableCollection<Tuote> Tuotes { get; set;}
        public Tuotteet()
        {
            Tuotes = new ObservableCollection<Tuote>();
        }

    }
    public class Tuote
    {
        public string Nimi { get; set; }
        public int Tuote_ID { get; set; }
        public float Hinta { get; set; }
        public Tuote()
        {
            Nimi = string.Empty;
            Tuote_ID = 0;
            Hinta = 0;
        }
      

    }




}
