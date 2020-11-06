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
                    .OrderBy(x => x.Gebruikersnaam);
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

        //Ophalen lijstje
        public static List<Lijst> OphalenLijstje(Login gebruiker)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Lijst
                    .OrderBy(x => x.DatumAangemaakt)
                    .Include(x => x.Login)
                    .Where(x => x.Login_ID == gebruiker.Login_ID);
                return query.ToList();
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
        public static int VerwijderenLijstje(Lijst lijstje)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Lijst
                    .Include(x => x.Lijst_Product)
                    .Where(x => x.Lijst_ID == lijstje.Lijst_ID)
                    .SingleOrDefault();
                return entities.SaveChanges();
            }
        }


        //____LIJST_PRODUCT_____\\

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
        public static int VerwijderenProductInLijst(Lijst_Product productInLijst)
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Lijst_Product
                    .Where(x => x.Lijst_Product_ID == productInLijst.Lijst_Product_ID)
                    .SingleOrDefault();
                return entities.SaveChanges();
            }
        }

        
        //_____PRODUCT_____\\

        //Producten ophalen uit winkel
        public static List<Product> ProductenOphalen()
        {
            using (BoodschappenLijstjeEntities entities = new BoodschappenLijstjeEntities())
            {
                var query = entities.Product
                    .OrderBy(x => x.Categorie);
                return query.ToList();
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
                var query = entities.Product
                    .Where(x => x.Product_ID == product.Product_ID)
                    .SingleOrDefault();
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
                    .OrderBy(x => x.Routenummer)
                    .ThenBy(x => x.Naam);
                return query.ToList();
            }
        }
    }
}
