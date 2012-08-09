using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Helper;

namespace BaoHien.Common
{
    public class RandomGeneration
    {
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string DefaultRandomString()
        {
           return RandomString(BHConstant.SIZE_OF_CODE,false);
        }
        public static string GetRandomString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public static string GeneratingCode()
        {
            StringBuilder builder = new StringBuilder();
            DateTime nowDate = DateTime.Now;
            builder.Append(BHConstant.PREFIX_FOR_CODE);
            builder.Append(nowDate.Day.ToString()+nowDate.Month.ToString()+nowDate.Year.ToString());

            BaoHienDBDataContext context = BaoHienRepository.GetBaoHienDBDataContext();
            GenerateRandomStringResult randomString = context.GenerateRandomString(BHConstant.SIZE_OF_CODE).FirstOrDefault();
            
            builder.Append(randomString.RandomString);
            return builder.ToString();
        }
    }

}
