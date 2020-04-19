using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Data;
using BeyondTheTutor.Models.ZoomModels;

namespace BeyondTheTutor.Controllers
{
    public class ZoomAPIController : Controller
    {
        string apiKey = "";
        public JsonResult Meetings()
        {
            string uri = "https://api.zoom.us/v2/users/R3FaGcfjRda8dVl7FB3Lhg/meetings?page_number=1&page_size=30&type=live";
            Debug.WriteLine(uri);

            string data = SendRequest(uri, apiKey);
            Meetings dictionary = JsonConvert.DeserializeObject<Meetings>(data);

            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Users()
        {
            string uri = "https://api.zoom.us/v2/users?page_number=1&page_size=30&status=active";
            Debug.WriteLine(uri);

            string data = SendRequest(uri, apiKey);
            Users dictionary = JsonConvert.DeserializeObject<Users>(data);

            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }

        private string SendRequest(string uri, string credientials)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.Headers.Add("authorization", "Bearer " + credientials);
            request.Accept = "application/json";

            string jsonString = null;

            using (WebResponse response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                jsonString = reader.ReadToEnd();
                reader.Close();
                stream.Close();
            }
            return jsonString;
        }
    }
}