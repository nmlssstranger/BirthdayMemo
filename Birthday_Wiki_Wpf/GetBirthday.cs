using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Linq;

namespace Birthday_Wiki_Wpf
{
    class GetBirthday
    {
        public static string GetUrl(string address)
        {
            WebClient client = new WebClient { Credentials = CredentialCache.DefaultNetworkCredentials };
            return client.DownloadString(address);
        }


        public static string GetSomeData(HtmlNode elem)
        {
            string outputText = "";
            // проверка на наличие найденных узлов
            if (elem != null)
            {
                outputText += elem.InnerText + "";
            }
            return outputText;
        }

        public static List<string> GetGroupList(HtmlNodeCollection elems)
        {
            List<string> list = new List<string>();
            if (elems != null)
            {
                foreach (HtmlNode HN in elems)
                {
                    list = HN.InnerText.Split('\n').ToList();
                }
            }
            list.RemoveAll(x => x == ""); // сносим все пустые строки, если есть
            if (list.Count > 1)
            {
                list.RemoveRange(1, list.Count - 1);
            }
            else
                if (list.Count == 1)
            {
                List<string> bandList = list[0].Split(',').ToList();
                for (int i = 0; i < bandList.Count; i++)
                {
                    if (bandList[i].First() == ' ')
                    {
                        bandList[i] = bandList[i].Remove(0, 1);
                    }
                }
                bandList.RemoveAll(x => x == " "); // сносим все пустые строки, если есть
                bandList.RemoveRange(1, bandList.Count - 1);
                list = bandList;
            }
            return list;
        }
        public static Person GetArticle(string html) //парсим страницу
        {
            HtmlDocument HD = new HtmlDocument();
            var web = new HtmlWeb
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8,
            };
            HD = web.Load(html);                    //Скачиваем всю HTML страницу

            var bday = HD.DocumentNode.SelectSingleNode("//span[@class='bday']");
            var name = HD.DocumentNode.SelectSingleNode("//h1");
            var groups = HD.DocumentNode.SelectNodes("//td[..//th/span[text() = 'Associated acts']]");
            return new Person(GetSomeData(name), Convert.ToDateTime(GetSomeData(bday)), GetGroupList(groups), 0); // генерируем нового человека
        }

    }
}
