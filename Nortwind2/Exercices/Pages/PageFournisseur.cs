using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2.Pages
{
    class PageFournisseur : MenuPage
    {
        public PageFournisseur() : base("Page Fournisseur")
        {
            Menu.AddOption("1", "Liste des pays ",AfficherPays);

            Menu.AddOption("2", "Fournisseurs d'un pays ",AfficherFournisseursPays);
        }


        private void AfficherPays()
        {
            var pays=Contexte.GetPaysFournisseurs();
            ConsoleTable.From(pays,"Pays").Display("Pays");
        }

        private void AfficherFournisseursPays()
        {
            Console.WriteLine("Taper Pays");
            string pays = Console.ReadLine();
            var fournisseurs= Contexte.GetFournisseurs(pays);
            ConsoleTable.From(fournisseurs).Display("Fournisseurs");
        }

    }
}
