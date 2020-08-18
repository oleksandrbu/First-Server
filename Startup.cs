using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace First_Server
{
    public class Startup
    {
        public bool FLAG = false;
        public string[] who = {"Повар", "Чебурек", "Кек"};
        public string[] how = {"влажно", "вздыхая", "задумчиво"};
        public string[] does = {"готовит", "прогает", "говорит"};
        public string[] what = {"орех", "котлетка", "кот"};

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/who", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(Data.RandomWord(who));
                });
                endpoints.MapGet("/how", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(Data.RandomWord(how));
                });
                endpoints.MapGet("/does", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(Data.RandomWord(does));
                });
                endpoints.MapGet("/what", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync(Data.RandomWord(what));
                });
                endpoints.MapGet("/quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    await context.Response.WriteAsync($"{Data.RandomWord(who)} {Data.RandomWord(how)} {Data.RandomWord(does)} {Data.RandomWord(what)}.");
                });
                endpoints.MapGet("/incamp18-quote", async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.Headers.Add("InCamp-Student", "Ichigen");

                    IRequest request;
                    if (FLAG){
                        request = new AsyncRequest();
                    } else {
                        request = new SyncRequest();
                    }   
                    DateTime start = DateTime.Now;
                    List<(string, string)> words = request.Request();
                    TimeSpan timeItTook = DateTime.Now - start;
                                     
                    string requestLine = "", requestWords = "";
                    for (int i = 0; i < 4; i++)
                    {
                        requestWords += $"{words[i].Item1} ";
                        requestLine += $"{words[i].Item1} from {words[i].Item2}<br>";
                    }
                    
                    await context.Response.WriteAsync($"{requestWords}.<br>{requestLine}<br>For:{timeItTook.Milliseconds}ms");
                });
                endpoints.MapGet("/tests", async context =>
                {
                    FLAG = !FLAG;
                    if (FLAG){
                        await context.Response.WriteAsync("Async enable.");
                    } else {
                        await context.Response.WriteAsync("Async disable.");
                    }                    
                });
            });
        }
    }
}
