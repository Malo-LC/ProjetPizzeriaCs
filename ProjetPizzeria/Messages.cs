using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjetPizzeria
{

    class Commande { }

    class Program
    {
        public static async Task Main(int id)
        {
            Trace.WriteLine($"Preparation de la commande id : {id}");
            await fairePizza("grande");
            await RajouterBoisson("Coca");
            await LivrerCommande("38 rue des Lilas");
            Trace.WriteLine("Fin de cette commande");
        }

        private static async Task<Commande> fairePizza(string size)
        {
            Trace.WriteLine($"Preparation d'une pizza de taille {size}");
            Trace.WriteLine("Rassemblement de tout les ingrédients requis");
            await Task.Delay(5000);
            Trace.WriteLine("Placement de la base");
            await Task.Delay(2000);
            Trace.WriteLine("Rajout des ingrédients");
            await Task.Delay(6000);
            Trace.WriteLine("Cuisson...");
            await Task.Delay(15000);
            Trace.WriteLine("Mise de la pizza en carton");
            await Task.Delay(3000);
            Trace.WriteLine("Pizza Finie !!");
            return new Commande();
        }
        private static async Task<Commande> RajouterBoisson(string boiss)
        {
            Trace.WriteLine($"Rajout du {boiss} dans la commande");
            await Task.Delay(5000);
            Trace.WriteLine("Boisson rajoutée !");
            return new Commande();
        }

        private static async Task<Commande> LivrerCommande(string adresse)
        {
            Trace.WriteLine($"livraison de la commande au {adresse}");
            await Task.Delay(10000);
            Trace.WriteLine("Arrivé à l'adresse indiquée");
            Trace.WriteLine("Règlement en espèce de la commande");
            await Task.Delay(2000);
            Trace.WriteLine("Commande livrée avec succès");
            return new Commande();
        }

    }
}