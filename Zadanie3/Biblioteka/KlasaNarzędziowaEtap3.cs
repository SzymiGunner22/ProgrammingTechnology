﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;


namespace Biblioteka
{
    public class KlasaNarzędziowaEtap3
    {
        public static List<Product> GetProductsByName(string name)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            List<Product> zapytanie =
                (from p in db.Products
                 where SqlMethods.Like(p.Name, "%" + name + "%")
                 select p).ToList();

            return zapytanie;
        }

        public static List<Product> GetProductsByVendorName(string vendorName)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            List<Product> zapytanie = (from p in db.Products
                                       join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                       join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                       where SqlMethods.Like(v.Name, vendorName)
                                       select p).ToList();
            return zapytanie;
        }

        public static List<string> GetProductNamesByVendorName(string vendorName)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            List<string> zapytanie = (from p in db.Products
                                      join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                                      join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                                      where SqlMethods.Like(v.Name, vendorName)
                                      select p.Name).ToList();

            return zapytanie;
        }
        public static string GetProductVendorByProductName(string productName)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var zapytanie = (from p in db.Products
                             join pv in db.ProductVendors on p.ProductID equals pv.ProductID
                             join v in db.Vendors on pv.BusinessEntityID equals v.BusinessEntityID
                             where SqlMethods.Like(p.Name, productName)
                             select v.Name).FirstOrDefault();

            return zapytanie.ToString();
        }

        public static List<Product> GetProductsWithNRecentReviews(int howManyReviews)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            List<Product> zapytanie = (from pr in db.ProductReviews
                                       join p in db.Products on pr.ProductID equals p.ProductID
                                       select p).Take(howManyReviews).ToList<Product>();

            return zapytanie;
        }
        public static List<Product> GetNRecentlyReviewedProducts(int howManyProducts)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            List<Product> zapytanie = (from p in db.Products
                                       join pr in db.ProductReviews on p.ProductID equals pr.ProductID
                                       orderby pr.ReviewDate
                                       select p).Take(howManyProducts).ToList<Product>();

            return zapytanie;
        }
        public static List<Product> GetNProductsFromCategory(string categoryName, int n)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            List<Product> zapytanie = (from p in db.Products
                                       join s in db.ProductSubcategories on p.ProductSubcategoryID equals s.ProductSubcategoryID
                                       join c in db.ProductCategories on s.ProductCategoryID equals c.ProductCategoryID
                                       where c.Name == categoryName
                                       select p).Take(n).ToList();

            return zapytanie;
        }
        public static double GetTotalStandardCostByCategory(ProductCategory category)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var zapytanie = (from p in db.Products
                             join s in db.ProductSubcategories on p.ProductSubcategoryID equals s.ProductSubcategoryID
                             join c in db.ProductCategories on s.ProductCategoryID equals c.ProductCategoryID
                             where c.Equals(category)
                             select p);
            return zapytanie.Sum(p => System.Convert.ToDouble(p.ListPrice));
        }


    }
}
