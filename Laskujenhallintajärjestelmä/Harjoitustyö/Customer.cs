

using System.Collections.ObjectModel;

namespace Harjoitustyö
{
    public class Asiakas
    {
        public string Nimi { get; set; }
        public int Asiakas_ID { get; set; }

        public string Osoite { get; set; }

        public Asiakas()
        {
            Nimi = string.Empty;
            Osoite = string.Empty;
            Asiakas_ID = 0;
           
        }

    }

    
  

}

