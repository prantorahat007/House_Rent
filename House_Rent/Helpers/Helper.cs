using House_Rent.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
namespace House_Rent.Helpers
{
    public class Helper
    {
        static Random random = new Random();


        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}