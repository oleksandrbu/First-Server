using System.Collections.Generic;

namespace First_Server{
    public class SyncRequest : AbstructRequest, IRequest{
        public List<(string, string)> Request(){
            List<(string, string)> words = new List<(string, string)>();

            for (int i = 0; i < 4; i++){
                words.Add(GetRequest($"{Data.RandomWord(Data.urls)}{Data.links[i]}"));
            }
            
            return words;
        }
    }
}