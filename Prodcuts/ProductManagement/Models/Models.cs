using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }


        //navigation property
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

        public int SubCateogryId { get; set; }
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

    }


    public class Sale
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int ModeOfPaymentId { get; set; }

        public ModeOfPayment ModeOfPayment { get; set; }
        public int Quantity { get; set; }
        public string CucstomerName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }

        public DateTime DateOfBilling { get; set; }


    }


    public class ModeOfPayment
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }

}
