using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<SubCategory> SubCategories { get; set; }
    }

    public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductModel> ProductModels { get; set; }

    }

    public class ProductModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public List<Product> Products { get; set; }

    }

    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductModelId { get; set; }
        public ProductModel ProductModel { get; set; }
        public List<Sale> ProductSales { get; set; }

        public float UnitPrice { get; set; }

    }


    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ModeOfPaymentId { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }

        [Range(1,10, ErrorMessage ="Enter only numaric value (1-10). ")]
        public int Quantity { get; set; }
        public string CucstomerName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }

        public DateTime DateOfBilling { get; set; }

        [NotMapped]
        public int CategoryId { get; set; }

        [NotMapped]
        public int SubCategoryId { get; set; }

        [NotMapped]
        public int ProductModelId { get; set; }
    }


    public class ModeOfPayment
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

}
