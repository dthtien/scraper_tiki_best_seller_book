using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace Scraper_ED_day1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scraping....");

            TinTrinhLibrary.WebClient client = new TinTrinhLibrary.WebClient();

            StreamWriter file = new StreamWriter(@"D:\study\ECDESI\products.txt");
            for (int t = 1; t <= 4; t++) // get multiple page
            {

                string html_source = client.Get($"https://tiki.vn/bestsellers/sach-truyen-tieng-viet/c316?p={t}", "https://tiki.vn/", "");
                int i = 0;

                /// Get Product div
                MatchCollection products = Regex.Matches(html_source, "class=\"infomation\">(.*?)fa-caret-right", RegexOptions.Singleline);
                //end Get
                foreach (Match product in products)
                {
                    string string_product = product.Groups[1].Value.Trim();
                    /// Remove htlm tag
                    MatchCollection products_detail = Regex.Matches(string_product, "[^>]+?(?=<)", RegexOptions.Singleline); //
                                                                                                                             /// write data to products.txt
                    foreach (Match product_detail in products_detail)
                        if (product_detail.Value.Trim() != "")
                        {
                            i++;
                            /// Set category 
                            if (i == 1)
                            {
                                string p = "Name: " + product_detail.Value.Trim();
                                file.WriteLine(p);
                            }
                            if (i == 2)
                            {
                                string p = "Author: " + product_detail.Value.Trim();
                                file.WriteLine(p);
                            }
                            if (i == 5)
                            {
                                string p = "Price: " + product_detail.Value.Trim();
                                file.WriteLine(p);
                            }
                            if (i == 8)
                            {
                                string p = "Decription: " + product_detail.Value.Trim();
                                file.WriteLine(p);
                            }
                            if (i == 9)
                            {
                                string p = "end!\n\n------------------------------------";
                                file.WriteLine(p);
                            }
                            file.Flush();
                            ///end Set catgory
                            ///
                        }
                    i = 0;

                }
            }
            //end write
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Ok");
            Console.ReadLine();

        }
    }
}