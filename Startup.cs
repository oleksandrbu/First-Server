using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.IO;

namespace First_Server
{
    public class Startup
    {
        string[] who = {"Повар", "Чебурек", "Кек"};
        string[] how = {"влажно", "вздыхая", "задумчиво"};
        string[] does = {"готовит", "прогает", "говорит"};
        string[] what = {"орех", "котлетка", "кот"};
        string[] urls = {"http://d0165c8e5358.ngrok.io", 
        "http://12f1a14e7e50.ngrok.io", 
        "http://4c1449a93861.ngrok.io", 
        "http://7a45a5f78857.ngrok.io", 
        "http://e77fd3b7ed59.ngrok.io",
        "http://a089177a583a.ngrok.io",
        "http://aba617d86eae.ngrok.io",
        "http://26b139b05b0f.ngrok.io",
        "http://ef845d6343d7.ngrok.io",
        "http://5e9e572e07b3.ngrok.io",
        "http://67e5aa89deb6.ngrok.io",
        "http://8a2f59ef9085.ngrok.io"};
        string[] links = {"/who", "/how", "/does", "/what"};

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/who", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(RandomWord(who));
                });
                endpoints.MapGet("/how", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(RandomWord(how));
                });
                endpoints.MapGet("/does", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(RandomWord(does));
                });
                endpoints.MapGet("/what", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(RandomWord(what));
                });
                endpoints.MapGet("/quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync($"{RandomWord(who)} {RandomWord(how)} {RandomWord(does)} {RandomWord(what)}.");
                });
                endpoints.MapGet("/incamp18-quote", async context =>
                {
                    string requestLine = "", requestWords = "";
                    (string, string)[] words = {("who","none"), ("how","none"), ("does","none"), ("what","none")};

                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");
                    for (int i = 0; i < 4; i++)
                    {
                        words[i] = GetRequest($"{RandomWord(urls)}{links[i]}", words[i]);
                        requestWords += $"{words[i].Item1} ";
                        requestLine += $"{words[i].Item1} from {words[i].Item2}<br>";
                    }
                    
                    await context.Response.WriteAsync($"{requestWords}.<br>{requestLine}");
                });
            });
        }

        public string RandomWord(string[] wordList){
            var rand = new Random();
            return wordList[rand.Next(0, wordList.Length)];
        }

        public (string, string) GetRequest(string url, (string, string) word)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using(StreamReader r = new StreamReader(response.GetResponseStream()))
            {
                word.Item1 = r.ReadToEnd();
            }
            for(int i=0; i < response.Headers.Count; i++)
            {
                if (response.Headers.Keys[i] == "InCamp-Student"){
                    word.Item2 = response.Headers[i];
                    break;
                }
            }
            
            return word;
        }
    }
}
