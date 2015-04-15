using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace warszawaTeryt
{
    internal class Program
    {
        #region Stare

        //        private static int ileBledow = 0;
        //        private static List<string> uliceSprawdzone = new List<string>();
        //        private static int dodano = 0;

        //        private static void Main(string[] args)
        //        {
        //            ////http://overpass-turbo.eu
        //            //string path = "D:\\warszawa";
        //            //var inputFiles = Directory.EnumerateFiles(path);
        //            //List<Dzielnica> ulice = new List<Dzielnica>();
        //            //int ileUlic = 0;
        //            //foreach (var inputFile in inputFiles)
        //            //{
        //            //    XDocument doc;

        //            //    using (Stream stream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
        //            //    {
        //            //        doc = XDocument.Load(stream);
        //            //    }

        //            //    var streets = doc.Descendants("row");
        //            //    foreach (var str in streets)
        //            //    {
        //            //        var info = str.Descendants("col");
        //            //        var district = info.First(x => x.Attributes().First(y => y.Name == "name").Value == "NAZWA_GMN");
        //            //        var name1 = info.First(x => x.Attributes().First(y => y.Name == "name").Value == "NAZWA_1");
        //            //        var name2 = info.First(x => x.Attributes().First(y => y.Name == "name").Value == "NAZWA_2");
        //            //        string distName = district.Value;
        //            //        string strName = string.Format("{0} {1}", name1.Value, name2.Value).Trim();
        //            //        var u = ulice.FirstOrDefault(x => x.Nazwa == distName);
        //            //        if (u == null) ulice.Add(new Dzielnica() { Nazwa = distName, Ulice = new List<Ulica>() });
        //            //        ulice.First(x => x.Nazwa == distName).Ulice.Add(new Ulica() { Nazwa = strName });
        //            //        ileUlic++;
        //            //    }
        //            //}
        //            //XmlSerializer xs = new XmlSerializer(ulice.GetType());
        //            //using (Stream stream = new FileStream("D:\\warszawa\\ulice.xml", FileMode.Create, FileAccess.Write))
        //            //{
        //            //    xs.Serialize(stream, ulice);
        //            //}

        //            //Console.WriteLine("Ulice TERYT: {0}", ileUlic);

        //            //testBudynkow();

        //            using (var ctx = new FoodSearchEntities())
        //            {
        //                ctx.Database.CreateIfNotExists();
        //            }

        //            if (File.Exists("ulice.txt"))
        //            {
        //                using (Stream stream = new FileStream("ulice.txt", FileMode.Open, FileAccess.Read))
        //                using (StreamReader rd = new StreamReader(stream))
        //                {
        //                    string read = rd.ReadToEnd();
        //                    string[] streets = read.Split(new []{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
        //                    foreach (var s in streets)
        //                    {
        //                        uliceSprawdzone.Add(s);
        //                    }
        //                }
        //            }

        //            testUlic();

        //            Console.ReadLine();
        //        }

        //        private static async void testUlic()
        //        {


        //            XDocument doc;

        //            if (File.Exists("osm.xml"))
        //            {
        //                using (Stream stream = new FileStream("osm.xml", FileMode.Open, FileAccess.Read))
        //                {
        //                    doc = XDocument.Load(stream);
        //                }
        //            }
        //            else
        //            {
        //                HttpClient client = new HttpClient();
        //                string url = "http://overpass-api.de/api/interpreter";
        //                string data = @"<osm-script output=""xml"" timeout=""250"">
        //    	                        <union>
        //                                    <query type=""way"">
        //                                        <has-kv k=""highway"" v=""""/>
        //                                        <bbox-query n=""52.259"" s=""50.060"" e=""21.020"" w=""19.959""/>
        //                                    </query>
        //                                </union>
        //                                <print mode=""body"" />
        //                            </osm-script>";
        //                HttpContent content = new StringContent(data);
        //                var resp = await client.PostAsync(url, content);
        //                using (Stream stream = new FileStream("osm.xml", FileMode.Create, FileAccess.Write))
        //                using (StreamReader rd = new StreamReader(await resp.Content.ReadAsStreamAsync()))
        //                using (StreamWriter wr = new StreamWriter(stream))
        //                {
        //                    await wr.WriteAsync(await rd.ReadToEndAsync());
        //                    doc = XDocument.Parse(await resp.Content.ReadAsStringAsync());
        //                }
        //            }
        //            if (!Directory.Exists("Ulice")) Directory.CreateDirectory("Ulice");

        //            var streets = doc.Descendants("way")
        //                .Descendants("tag")
        //                .Where(x => x.Attribute("k").Value == "highway")
        //                .Select(x => x.Parent.Descendants("tag").FirstOrDefault(y => y.Attribute("k").Value == "name"))
        //                .Where(x => x != null)
        //                .Select(x => x.Attribute("v").Value)
        //                .Distinct()
        //                .OrderBy(x => x)
        //                .ToList();

        //            Console.WriteLine(streets.Count);



        //            //var streets2 = streets.Take(20);

        //            //var streets = new string[]{"Inflancka", "Niska", "Karmelicka", "Miła", "Dzika"};

        //            foreach (var str in streets)
        //            {
        //                if (!uliceSprawdzone.Contains(str))
        //                {
        //                    Console.WriteLine(str);
        //                    try
        //                    {
        //                        if (dodano > 500) return;
        //                        bool res = await testBudynkow(str);
        //                        if (res) dodano++;
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ileBledow++;
        //                        Console.WriteLine(ex.Message);
        //                        continue;
        //                    }

        //                }
        //            }
        //            Console.WriteLine(ileBledow);
        //        }

        //        private static async Task<bool> testBudynkow(string ulica)
        //        {
        //            bool result = false;
        //            List<Adres> list = new List<Adres>();
        //            XDocument doc;
        //            if (File.Exists(string.Format("Ulice\\{0}.xml", ulica)))
        //            {
        //                using (Stream stream = new FileStream(string.Format("Ulice\\{0}.xml", ulica), FileMode.Open, FileAccess.Read))
        //                {
        //                    doc = XDocument.Load(stream);
        //                }
        //            }
        //            else
        //            {
        //                HttpClient client = new HttpClient();
        //                string url = "http://overpass-api.de/api/interpreter";
        //                string data = string.Format(@"  <osm-script output=""xml"" timeout=""250"">  	
        //                                                <union>
        //                                                    <query type=""way"">
        //                                                        <has-kv k=""addr:street"" v=""{0}""/>
        //                                                        <bbox-query n=""52.34205"" s=""52.11325"" e=""21.21941"" w=""20.81738""/>
        //                                                    </query>
        //                                                </union>
        //                                                <print mode=""body"" order=""quadtile""/>
        //                                                <union>
        //                                                    <item/>
        //                                                    <recurse type=""down""/>
        //                                                </union>
        //                                                <print mode=""body""/>
        //                                                </osm-script>", ulica);
        //                HttpContent content = new StringContent(data);
        //                var resp = await client.PostAsync(url, content);

        //                using (Stream stream = new FileStream(string.Format("Ulice\\{0}.xml", ulica), FileMode.Create, FileAccess.Write))
        //                using (StreamReader rd = new StreamReader(await resp.Content.ReadAsStreamAsync()))
        //                using (StreamWriter wr = new StreamWriter(stream))
        //                {
        //                    await wr.WriteAsync(await rd.ReadToEndAsync());
        //                    doc = XDocument.Parse(await resp.Content.ReadAsStringAsync());
        //                }
        //            }

        //            var ways = doc.Descendants("way").Descendants("tag").Where(x => x.Attribute("k").Value == "addr:housenumber");
        //            foreach (var way in ways)
        //            {
        //                try
        //                {
        //                    var parent = way.Parent;
        //                    string street = parent.Descendants("tag").FirstOrDefault(x => x.Attribute("k").Value == "addr:street").Attribute("v").Value;
        //                    if (street != ulica) continue;
        //                    string number = way.Attribute("v").Value;
        //                    var nd = parent.Descendants("nd").Select(x => x.Attribute("ref").Value);
        //                    float lonSum = 0f, latSum = 0f;
        //                    int sum = 0;
        //                    foreach (var node in nd)
        //                    {
        //                        try
        //                        {
        //                            var n = doc.Descendants("node").FirstOrDefault(x => x.Attribute("id").Value == node);
        //                            if (n == null) continue;
        //                            float lon = float.Parse(n.Attribute("lon").Value, CultureInfo.InvariantCulture);
        //                            float lat = float.Parse(n.Attribute("lat").Value, CultureInfo.InvariantCulture);

        //                            lonSum += lon;
        //                            latSum += lat;
        //                            sum++;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            ileBledow++;
        //                            Console.WriteLine(ex.Message);
        //                            continue;

        //                        }
        //                    }
        //                    Adres a = new Adres()
        //                    {
        //                        Numer = number,
        //                        Lat = latSum / sum,
        //                        Lon = lonSum / sum
        //                    };
        //                    var dup = list.FirstOrDefault(x => x.Numer == a.Numer);
        //                    if (dup != null)
        //                    {
        //                        float wawLat = 52.229593f;
        //                        float wawLon = 21.011880f;
        //                        float diffLatA = Math.Abs(a.Lat - wawLat);
        //                        float diffLonA = Math.Abs(a.Lon - wawLon);
        //                        float diffLatB = Math.Abs(dup.Lat - wawLat);
        //                        float diffLonB = Math.Abs(dup.Lon - wawLon);

        //                        if (diffLatA < diffLatB || diffLonA < diffLonB)
        //                        {
        //                            list.Remove(dup);
        //                        }
        //                        else if (diffLatA > diffLatB || diffLonA > diffLonB)
        //                        {
        //                            continue;
        //                        }
        //                        else if (diffLatA == diffLatB && diffLonA == diffLonB)
        //                        {
        //                            continue;
        //                        }
        //                    }
        //                    list.Add(a);
        //                }
        //                catch (Exception ex)
        //                {
        //                    ileBledow++;
        //                    Console.WriteLine(ex.Message);
        //                    continue;
        //                }
        //            }

        //            if (list.Count > 0)
        //            {
        //                foreach (var l in list.OrderBy(x => x.Numer))
        //                {
        //                    Console.WriteLine("\t{0} {1} \t\t Lat: {2}\tLon: {3}", ulica, l.Numer, l.Lat, l.Lon);
        //                }

        //                using (var ctx = new FoodSearchEntities())
        //                {
        //                    var street = ctx.Streets.FirstOrDefault(x => x.Name == ulica) ?? ctx.Streets.Add(new Street() {Name = ulica});
        //                    await ctx.SaveChangesAsync();
        //                    foreach (var l in list)
        //                    {
        //                        Adres l1 = l;
        //                        var exists = ctx.Addresses.FirstOrDefault(x => x.StreetId == street.StreetId && x.Number == l1.Numer);
        //                        if (exists == null)
        //                        {
        //                            ctx.Addresses.Add(new Address()
        //                            {
        //                                StreetId = street.StreetId,
        //                                Number = l.Numer,
        //                                Lat = l.Lat,
        //                                Lon = l.Lon
        //                            });
        //                            await ctx.SaveChangesAsync();
        //                        }
        //                    }
        //                }

        //                result = true;
        //            }

        //            uliceSprawdzone.Add(ulica);
        //            using (Stream str = new FileStream("ulice.txt", FileMode.Create, FileAccess.Write))
        //            using (StreamWriter sw = new StreamWriter(str))
        //            {
        //                foreach (var u in uliceSprawdzone)
        //                {
        //                    await sw.WriteLineAsync(u);
        //                }
        //            }

        //            return result;
        //        }

        #endregion

        private static void Main(string[] args)
        {
            //wczytajBudynki();
            //wczytajPolozenie();

            string api = "https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyCkrbSly-hxJRNBEpN5hFvMgDpEK-ZskVo&sensor=false&address={0}";
            string adr = HttpUtility.UrlEncode("Lewartowskiego 17,Warszawa", Encoding.UTF8);
            string url = string.Format(api, adr);
            WebClient web = new WebClient();
            web.Encoding = Encoding.UTF8;
            string xml = web.DownloadString(url);


            Console.ReadLine();
        }

        private static void wczytajPolozenie()
        {
            var url = new ApiKey[]
            {
                new ApiKey() {Id = 1, Url = "https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyCkrbSly-hxJRNBEpN5hFvMgDpEK-ZskVo&sensor=false&address={0}"},
                new ApiKey() {Id = 2, Url = "https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyCl9bsOsLUzHSeAn8X_qw_bo9HSNQf7vug&sensor=false&address={0}"},
                new ApiKey() {Id = 3, Url = "https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyAsxsNSHegAso72DBHnxr3mdIoR6_WbcTk&sensor=false&address={0}"},
                new ApiKey() {Id = 4, Url = "https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyCeb5gH7GrTOjxXFmTbvvzaMPJZaqI--PM&sensor=false&address={0}"}
            };
            int ile = 0;
            WebClient client = new WebClient();
            using (var ctx = new FoodSearchEntities())
            {
                int[] conf;
                using (StreamReader rd = new StreamReader("conf.txt", Encoding.GetEncoding("windows-1250")))
                {
                     conf = rd.ReadToEnd().Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }
                foreach (var u in (from c in conf from u in url where u.Id == c select u.Url))
                {
                    var adresy = ctx.Addresses.Where(x => x.Lat == null).OrderBy(x => x.AddressId).Skip(conf[0]).Take(conf[1]);
                    foreach (var a in adresy)
                    {
                        try
                        {
                            string adr = HttpUtility.UrlEncode(a.Street.Name + " " + a.Number + ",Warszawa", Encoding.UTF8);
                            string xml = client.DownloadString(string.Format(u, adr));
                            XDocument doc = XDocument.Parse(xml);
                            var loc = doc.Descendants("GeocodeResponse").Descendants("result").Descendants("geometry").Descendants("location").First();
                            float lat = float.Parse(loc.Descendants("lat").First().Value, CultureInfo.InvariantCulture);
                            float lon = float.Parse(loc.Descendants("lng").First().Value, CultureInfo.InvariantCulture);
                            a.Lat = lat;
                            a.Lon = lon;
                            ctx.SaveChanges();

                            Console.WriteLine("{0}. {1} {2}: lat={3} lon={4}", (++ile).ToString("0000"), a.Street.Name, a.Number, a.Lat, a.Lon);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            a.Lat = 0f;
                            a.Lon = 0f;
                            ctx.SaveChanges();
                            Console.WriteLine("{0}. {1} {2}: lat={3} lon={4}", ile.ToString("0000"), a.Street.Name, a.Number, a.Lat, a.Lon);
                            continue;
                        }

                    }
                }
            }
        }

        private static void wczytajBudynki()
        {
            string[] lines;
            using (Stream stream = new FileStream("Włochy.csv", FileMode.Open, FileAccess.Read))
            using (StreamReader rd = new StreamReader(stream, Encoding.GetEncoding("windows-1250")))
            {
                lines = rd.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }

            Console.WriteLine(lines.Take(10).Aggregate((a, b) => a += b + "\n"));

            var ulice = lines
                .Select(x => x.Split(';'))
                .Select(x => new
                {
                    Ulica = x[4],
                    Numer = x[2]
                });

            using (var ctx = new FoodSearchEntities())
            {
                int distId = ctx.Districts.First(x => x.DistrictId == 16).DistrictId;
                foreach (var u in ulice)
                {
                    try
                    {
                        Street street = ctx.Streets.FirstOrDefault(x => x.Name == u.Ulica) ?? ctx.Streets.Add(new Street() { Name = u.Ulica });
                        ctx.SaveChanges();
                        Address a = ctx.Addresses.FirstOrDefault(x => x.DistrictId == distId && x.StreetId == street.StreetId && x.Number == u.Numer);
                        if (a == null)
                        {
                            ctx.Addresses.Add(new Address()
                            {
                                DistrictId = distId,
                                StreetId = street.StreetId,
                                Number = u.Numer
                            });
                            ctx.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    finally
                    {
                        Console.WriteLine("{0} {1}", u.Ulica, u.Numer);
                    }
                }
            }
        }
    }
}
