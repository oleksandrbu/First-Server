using System.Collections.Generic;
using System.Threading.Tasks;

namespace First_Server{
    public class AsyncRequest : AbstructRequest, IRequest{
        public List<(string, string)> Request(){
            return AdditionalTask().Result;
        }
        private async Task<List<(string, string)>> AdditionalTask(){
            List<(string, string)> words = new List<(string, string)>();

            Task<(string, string)> task1, task2, task3, task0;

            task0 = Task.Run(() => GetRequest($"{Data.RandomWord(Data.urls)}{Data.links[0]}"));
            task1 = Task.Run(() => GetRequest($"{Data.RandomWord(Data.urls)}{Data.links[1]}"));
            task2 = Task.Run(() => GetRequest($"{Data.RandomWord(Data.urls)}{Data.links[2]}"));
            task3 = Task.Run(() => GetRequest($"{Data.RandomWord(Data.urls)}{Data.links[3]}"));

            await Task.WhenAll(new[] { task0, task1, task2, task3});
            
            words.Add(((string, string)) task0.Result);
            words.Add(((string, string)) task1.Result);
            words.Add(((string, string)) task2.Result);
            words.Add(((string, string)) task3.Result);

            return words;
        }
    }
}