using System;

namespace First_Server{
    public class Data{
        public static string[] urls = {"http://f40d021fd09e.ngrok.io"};
        public static string[] links = {"/who", "/how", "/does", "/what"};

        public static string RandomWord(string[] wordList){
            var rand = new Random();
            return wordList[rand.Next(0, wordList.Length)];
        }
    }
}