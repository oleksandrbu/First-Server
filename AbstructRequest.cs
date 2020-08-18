using System.Net;
using System.IO;
using System;

namespace First_Server{
    public abstract class AbstructRequest{
        public (string, string) GetRequest(string url){
            (string, string) word = ("", "");
            
            try{
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using(StreamReader r = new StreamReader(response.GetResponseStream())){
                    word.Item1 = r.ReadToEnd();
                }
                string[] values = response.Headers.GetValues("InCamp-Student");
                for(int i = 0; i < values.Length; i++) 
                    word.Item2 += values[i];
            
                response.Close();
            }
            catch (WebException ex){
                Console.Error.WriteLine("{0}", ex);
            }
            
            return word;
        }
    }
}