using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using  System.Configuration;
using System.Globalization;

namespace EventfullApiClient
{
    class Program
    {
        static void Main(string[] args)
        {


            GetData gD = new GetData();
            gD.GetXmlData();


        }
    }

    class GetData
    {
        public void GetXmlData()
        {
           
           /*
           --------------------------------------------------------------------
            Author :Faruk GÜNGÖR
            e-mail :farcompen71@gmail.com
            Using Eventful api to get events around you in the current month
            ---------------------------------------------------------
           */

            
           
            
            //get api appkey from app.config
            string appKey = ConfigurationManager.AppSettings["appKey"];
           
            //get location from user 
            Console.WriteLine("Enter Name of Your Location (example : istanbul) :");
            string lokasyon = Console.ReadLine();
            
            //counts of event results 
            byte page_size = 20;
          
            //get name of the current month 
            string month = DateTime.Now.ToString("MMMM",CultureInfo.InvariantCulture);
            Console.WriteLine("Please wait .. \n");
            string url = "http://api.eventful.com/rest/events/search?app_key=";
            HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(url+appKey+"&location="+lokasyon+"&date="+month+"&sort_order=date&page_size="+page_size);
            myRequest.Method = "GET";
            myRequest.ContentType = "text/xml;encoding=utf-8";
            WebResponse myResponse;
            myResponse = myRequest.GetResponse();
            XmlDocument myDoc = new XmlDocument();
            using (Stream responseStream = myResponse.GetResponseStream())
            {
                if(responseStream != null)
                    myDoc.Load(responseStream);
                XmlNodeList xmlNode;
                xmlNode = myDoc.GetElementsByTagName("event");
                for (int i = 0; i < xmlNode.Count ; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Event: "+ xmlNode[i].ChildNodes[0]?.InnerText.Trim() +"\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("url: "+ xmlNode[i].ChildNodes[1]?.InnerText.Trim()+"\n");
                   
                    Console.WriteLine("Event Start Time : " + xmlNode[i].ChildNodes[3]?.InnerText.Trim()+"\n");
                   
                    Console.WriteLine("Adress: " + xmlNode[i].ChildNodes[12]?.InnerText.Trim() + xmlNode[i].ChildNodes[13]?.InnerText.Trim()+ xmlNode[i].ChildNodes[14]?.InnerText.Trim()+"\n");
               

                }

                Console.ReadKey();

            }



        }



    }
        }

