using Microsoft.EntityFrameworkCore;

namespace TP_A16_ServiceAPI.models
{
    public class Produits
    {
        public int Id { get; set; } 
        public string NomProduit { get; set; }
        public int QuantiteInventaire { get; set; }
        public string FormatProduit { get; set; }
        [Precision(18, 2)]
        public decimal PrixUnitaire { get; set; }
        public string DescriptionProduit { get; set; }
        public string Categorie { get; set; }
        public string Fournisseur { get; set; }
        public string Animal { get; set; }
        public string ImageProduit { get; set; }


    }
}
