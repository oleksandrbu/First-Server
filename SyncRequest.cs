using System.Collections.Generic;
using System.Net;
using System.IO;

namespace First_Server{
    public class SyncRequest : IRequest{
        public List<(string, string)> Request(){
            List<(string, string)> words = new List<(string, string)>();

            for (int i = 0; i < 4; i++){
                words.Add(GetRequest($"{Data.RandomWord(Data.urls)}{Data.links[i]}"));
            }
            
            return words;
        }

        private (string, string) GetRequest(string url){
            (string, string) word = ("", "");
            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using(StreamReader r = new StreamReader(response.GetResponseStream())){
                word.Item1 = r.ReadToEnd();
            }
            string[] values = response.Headers.GetValues("InCamp-Student");
            for(int i = 0; i < values.Length; i++) 
                word.Item2 += values[i];
            
            return word;
        }
    }
}