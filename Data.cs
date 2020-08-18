using System;

namespace First_Server{
    public class Data{
        public static string[] urls = {
            "http://feb2ec000271.ngrok.io",
            "http://5b341e7ae688.ngrok.io",
            "http://74334191d2b3.ngrok.io",
            "http://14add9edba41.ngrok.io",
            "http://9220ec0c3226.ngrok.io"
        };
        public static string[] links = {"/who", "/how", "/does", "/what"};

        public static string RandomWord(string[] wordList){
            var rand = new Random();
            return wordList[rand.Next(0, wordList.Length)];
        }
    }
}