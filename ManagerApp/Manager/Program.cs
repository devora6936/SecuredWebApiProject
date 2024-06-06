using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String connectionString = "Data Source = SRV2\\PUPILS; Initial Catalog = Market; Integrated Security = True; Encrypt = False";

            CategoryAccess da = new CategoryAccess();
            //da.fillCategories(connectionString);
            //da.readCategory(connectionString);

            ProductAccess productAccess = new ProductAccess();
            productAccess.fillProducts(connectionString);
            //productAccess.readProducts(connectionString);
            //da.fillDataSet(connectionString);
        }
    }
}
