using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Application.Extensions
{
    public static class SessionHelper
    {
        //Set
        public static void SetProductJson(this ISession session, string key,object value) //Sipariş işlemlerini yaparken Core yapısında session bulunmadığı için kendimiz oluşturmalıyız. Bu yüzden burada bir session oluşturuyorum.
        {
            session.SetString(key,JsonConvert.SerializeObject(value)); //Session'a atarken objeyi Json'a dönüştür.
        }

        //Get
        public static T GetProductJson<T>(this ISession session,string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value); //Oluşturulan session'dan ürünü alırken tekrar T tipine dönüştür.
        }
    }
}
