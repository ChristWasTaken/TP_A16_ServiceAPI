using Microsoft.EntityFrameworkCore;

namespace TP_A16_ServiceAPI.models
{
    public class ProduitsDTO
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

        public static ProduitsDTO ProduitsToDTO(Produits produit) =>
            new ProduitsDTO
            {
                Id = produit.Id,
                NomProduit = produit.NomProduit,
                QuantiteInventaire = produit.QuantiteInventaire,
                FormatProduit = produit.FormatProduit,
                PrixUnitaire = produit.PrixUnitaire,
                DescriptionProduit = produit.DescriptionProduit,
                Categorie = produit.Categorie,
                Fournisseur = produit.Fournisseur,
                Animal = produit.Animal,
                ImageProduit = produit.ImageProduit
            };
    }
}
