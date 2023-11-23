using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
 
class Result
{
    public class Data
    {
        public string Barcode { get; set; }
        public int Price { get; set; }
        public double Discount { get; set; }
        public string Category { get; set; }
        public string Item { get; set; }
    }
 
    public class Response
    {
        public List<Data> Data { get; set; }
    }

    public class CategoryResponse
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public static CategoryResponse getHighestPriceItemByCategory(string category)
    {
        if (String.IsNullOrWhiteSpace(category))
        {
            return new CategoryResponse()
            {
                Name = "Category can't be empty",
                Price = -1
            };
        }
        
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = "https://jsonmock.hackerrank.com/api/inventory";

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = response.Content.ReadAsStringAsync().Result;

                    var responseData = JsonConvert.DeserializeObject<Response>(jsonData);

                    if (responseData.Data.Count() < 1)
                    {
                        return new CategoryResponse()
                        {
                            Name = "There are no items",
                            Price = (int)response.StatusCode
                        };
                    }

                    List<Data> categoryItems =
                        responseData.Data.Where(p => p.Category != null && p.Category == category).ToList();

                    if (categoryItems.Count() < 1)
                    {
                        return new CategoryResponse()
                        {
                            Name = "There are no items in this category",
                            Price = (int)response.StatusCode
                        };
                    }

                    int maxPrice = categoryItems.Select(p => p.Price).Max();
                    return categoryItems
                        .Where(p => p.Price == maxPrice)
                        .Select(p => new CategoryResponse()
                        {
                            Price = p.Price,
                            Name = p.Item
                        })
                        .First();
                }
                
                return new CategoryResponse()
                {
                    Name = "Error",
                    Price = (int)response.StatusCode
                };
            }
            catch (Exception e)
            {
                return new CategoryResponse()
                {
                    Name = e.Message,
                    Price = -1
                };
            }
        }
    }
    
    /*
     * Complete the 'getDiscountedPrice' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER barcode as parameter.
     * API URL: https://jsonmock.hackerrank.com/api/inventory?barcode=<barcode>
     */
 
    public static int getDiscountedPrice(int barcode)
    {
        using(HttpClient client = new HttpClient())
        {
            try
            {
                string url = "https://jsonmock.hackerrank.com/api/inventory?barcode=" + barcode.ToString();
                       
                HttpResponseMessage response = client.GetAsync(url).Result;
               
                if(response.IsSuccessStatusCode)
                {
                    string jsonData = response.Content.ReadAsStringAsync().Result;
 
                    var responseData = JsonConvert.DeserializeObject<Response>(jsonData);
                   
                    if(responseData.Data.Count()  < 1)
                        return -1;
                   
                    var barcodeData = responseData.Data.First();
                   
                    if(barcodeData.Price == null || barcodeData.Discount == null)
                        return -1;
                   
                    int discountedPrice = (int)Math.Round((barcodeData.Price - ((barcodeData.Discount / 100) * barcodeData.Price)));
                   
                    return discountedPrice;
                }
               
                return -1;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
    }
    
    
 
}
 
class Solution
{
    public static void Main(string[] args)
    {
 
        string category = Console.ReadLine().Trim();
        var result = Result.getHighestPriceItemByCategory(category);
        Console.WriteLine(result.Name);
        Console.WriteLine(result.Price);
    }
}