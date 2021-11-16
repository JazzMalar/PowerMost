using System;
using PowerMost;
using PowerMost.APIWrapper;

namespace TestMost
{
    class Program
    {
        public string username { get; set; }

        static void Main(string[] args)
        {

            MMApiWrapper api = new MMApiWrapper("https://no-company.cloud.mattermost.com/api/v4/", "hx9qtko5d3rdt8yhq3zk963qkr");
            
            api.GetUsers(); 
        }

        public Program()
        {
            Console.WriteLine(username); 
        }

    }
}
