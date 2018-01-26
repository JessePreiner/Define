using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace define
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ApplicationException("Specify the URI of the resource to retrieve.");
            }

            var results = new Hashtable();
            var uri = ConfigurationManager.AppSettings["api_url"];

            foreach (var word in args)
            {
                var endpoint = string.Format(uri, word);
                var client = new WebClient();
                client.Headers.Add("app_id", ConfigurationManager.AppSettings["app_id"]);
                client.Headers.Add("app_key", ConfigurationManager.AppSettings["app_key"]);
                client.Headers.Add("Content-Type", "application/json");
                try
                {
                    using (Stream data = client.OpenRead(endpoint))
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string s = reader.ReadToEnd();
                            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(s)))
                            {
                                DataContractJsonSerializer des =
                                    new DataContractJsonSerializer(typeof(DictionaryApiResult));
                                DictionaryApiResult apiResult = (DictionaryApiResult) des.ReadObject(ms);

                                results[word] = new ArrayList(apiResult.results
                                    .SelectMany(result => result.lexicalEntries
                                            .SelectMany(lexicalEntry => lexicalEntry.entries
                                                    .SelectMany(entry => entry.senses
                                                            .SelectMany(sense => sense?.definitions ?? new List<string>())
                                                    )
                                            )
                                    ).ToList());
                            }
                        }
                    }
                }
                catch (WebException webException)
                {
                    var response = (HttpWebResponse) webException.Response;
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            results[word] = new ArrayList { "No entry found"};
                            break;
                        case HttpStatusCode.InternalServerError:
                            results[word] = "Unable to contact server";
                            break;
                    }

                }

                catch (Exception generalException)
                {
                    Console.WriteLine("Api call error");
                    Console.WriteLine(generalException.GetBaseException().ToString());
                }

            }

            var i = 1;
            var sortedKeys = new ArrayList(results.Keys);
            sortedKeys.Sort();
            foreach (var key in sortedKeys)
            {
                Console.WriteLine(key);

                foreach (var definition in (ArrayList)results[key])
                {
                    Console.WriteLine($"\t{i}.\t{definition}");
                    i++;
                        
                }
            }
        }


    }
}
