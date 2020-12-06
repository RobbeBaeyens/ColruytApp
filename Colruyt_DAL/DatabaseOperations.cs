using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
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
                    .OrderBy(x => x.email)
                    .ThenBy(x => x.gebruikersnaam)
                    .ThenBy(x => x.wachtwoord);
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
                    .Where(x => x.id == gebruiker.id)
                    .SingleOrDefault();
                entities.Lijst.Remove((Lijst)query.Lijst);
                entities.Login.Remove(query);
                return entities.SaveChanges();
            }
        }


        //_____LIJST_____\\

        //Ophalen lijstjes
        public static List<Lijst> OphalenLijstjes(Login gebruiker)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Lijst
                    .Include(x => x.Login)
                    .Where(x => x.Login.id == gebruiker.id)
                    .OrderBy(x => x.datumAangemaakt);
                return query.ToList();
            }
        }
        
        //Ophalen lijst
        public static Lijst OphalenLijst(int id)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Lijst
                    .Where(x => x.id == id);
                return query.SingleOrDefault();
            }
        }

        //Toevoegen lijstje
        public static int ToevoegenLijstje(Lijst lijstje)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Lijst.Add(lijstje);
                return entities.SaveChanges();
            }
        }

        //Aanpassen lijstje
        public static int AanpassenLijstje(Lijst lijstje)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Entry(lijstje).State = EntityState.Modified;
                return entities.SaveChanges();
            }
        }

        //Verwijderen lijstje
        public static int VerwijderenLijstje(Lijst lijst)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Lijst
                    .Include(x => x.Lijst_Product)
                    .Where(x => x.id == lijst.id)
                    .SingleOrDefault();
                foreach(Lijst_Product product in query.Lijst_Product)
                {
                    entities.Lijst_Product.Remove(product);
                }
                entities.Lijst.Remove(query);
                return entities.SaveChanges();
            }
        }


        //____LIJST_PRODUCT_____\\

        //Ophalen producten uit lijst
        public static List<Lijst_Product> ProductenOphalenOpLijst(Lijst lijst)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Lijst_Product
                    .Where(x => x.lijstId == lijst.id);
                return query.ToList();
            }
        }

        //Toevoegen product in lijst
        public static int ToevoegenProductInLijst(Lijst_Product productInLijst)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Lijst_Product.Add(productInLijst);
                return entities.SaveChanges();
            }
        }

        //Verwijderen product in lijst
        public static int VerwijderenProductInLijst(Lijst_Product product)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Entry(product).State = EntityState.Deleted;
                return entities.SaveChanges();
            }
        }


        //_____PRODUCT_____\\

        //Alle Producten ophalen uit winkel
        public static List<Product> ProductenOphalen()
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Product
                    .OrderBy(x => x.naam);
                return query.ToList();
            }
        }

        //Producten ophalen uit winkel volgens categorie
        public static List<Product> ProductenOphalen(Categorie categorie)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Product
                    .Include(x => x.Categorie)
                    .Where(x => x.categorieId == categorie.id)
                    .OrderBy(x => x.Categorie.id);
                return query.ToList();
            }
        }

        //Product ophalen op id
        public static Product ProductOphalenOpId(int id)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Product
                    .Where(x => x.id == id)
                    .SingleOrDefault();
                return query;
            }
        }

        //Product toevoegen in winkel
        public static int ToevoegenProduct(Product product)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Product.Add(product);
                return entities.SaveChanges();
            }
        }

        //Product aanpassen uit winkel
        public static int AanpassenProduct(Product product)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Entry(product).State = EntityState.Modified;
                return entities.SaveChanges();
            }
        }

        //Product verwijderen uit winkel
        public static int VerwijderenProduct(Product product)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                entities.Entry(product).State = EntityState.Deleted;
                return entities.SaveChanges();
            }
        }


        //_____CATEGORIE_____\\

        //Categorieën ophalen
        public static List<Categorie> OphalenCatogorieën()
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Categorie
                    .OrderBy(x => x.routenummer)
                    .ThenBy(x => x.naam);
                return query.ToList();
            }
        }
    }
}
