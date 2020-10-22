using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;


namespace Colruyt_DAL
{
    public class DatabaseOperations
    {
        //_____LOGIN_____\\

        //Ophalen lijst gebruikers (login's)
        public static List<Login> OphalenGebruikers()
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Login
                    .OrderBy(x => x.Voornaam)
                    .ThenBy(x => x.Naam);
                return query.ToList();
            }
        }

        //Toevoegen gebruiker
        public static int ToevoegenGebruiker(Login gebruiker)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Login.Add(gebruiker);
                return entities.SaveChanges();
            }
        }

        //Aanpassen bestaande gebruiker
        public static int AanpassenGebruiker(Login gebruiker)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Entry(gebruiker).State = EntityState.Modified;
                return entities.SaveChanges();
            }
        }

        //Verwijderen gebruiker
        public static int VerwijderenGebruiker(Login gebruiker)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Login
                    .Include(x => x.Lijst)
                    .Where(x => x.Login_ID == gebruiker.Login_ID)
                    .SingleOrDefault();
                entities.Lijst.Remove((Lijst)query.Lijst);
                entities.Login.Remove(query);
                return entities.SaveChanges();
            }
        }

        //_____LIJST_____\\

        //Ophalen 
    }
}
