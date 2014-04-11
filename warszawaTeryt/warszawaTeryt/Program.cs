using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace warszawaTeryt
{
    internal class Program
    {
        private static int ileBledow = 0;
        private static List<string> uliceSprawdzone = new List<string>();

        private static void Main(string[] args)
        {
            ////http://overpass-turbo.eu
            //string path = "D:\\warszawa";
            //var inputFiles = Directory.EnumerateFiles(path);
            //List<Dzielnica> ulice = new List<Dzielnica>();
            //int ileUlic = 0;
            //foreach (var inputFile in inputFiles)
            //{
            //    XDocument doc;

            //    using (Stream stream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            //    {
            //        doc = XDocument.Load(stream);
            //    }

            //    var streets = doc.Descendants("row");
            //    foreach (var str in streets)
            //    {
            //        var info = str.Descendants("col");
            //        var district = info.First(x => x.Attributes().First(y => y.Name == "name").Value == "NAZWA_GMN");
            //        var name1 = info.First(x => x.Attributes().First(y => y.Name == "name").Value == "NAZWA_1");
            //        var name2 = info.First(x => x.Attributes().First(y => y.Name == "name").Value == "NAZWA_2");
            //        string distName = district.Value;
            //        string strName = string.Format("{0} {1}", name1.Value, name2.Value).Trim();
            //        var u = ulice.FirstOrDefault(x => x.Nazwa == distName);
            //        if (u == null) ulice.Add(new Dzielnica() { Nazwa = distName, Ulice = new List<Ulica>() });
            //        ulice.First(x => x.Nazwa == distName).Ulice.Add(new Ulica() { Nazwa = strName });
            //        ileUlic++;
            //    }
            //}
            //XmlSerializer xs = new XmlSerializer(ulice.GetType());
            //using (Stream stream = new FileStream("D:\\warszawa\\ulice.xml", FileMode.Create, FileAccess.Write))
            //{
            //    xs.Serialize(stream, ulice);
            //}

            //Console.WriteLine("Ulice TERYT: {0}", ileUlic);

            //testBudynkow();

            if (File.Exists("ulice.txt"))
            {
                using (Stream stream = new FileStream("ulice.txt", FileMode.Open, FileAccess.Read))
                using (StreamReader rd = new StreamReader(stream))
                {
                    string read = rd.ReadToEnd();
                    string[] streets = read.Split(new []{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var s in streets)
                    {
                        uliceSprawdzone.Add(s);
                    }
                }
            }

            testUlic();

            Console.ReadLine();
        }

        private static async void testUlic()
        {


            XDocument doc;
            if (File.Exists("osm.xml"))
            {
                using (Stream stream = new FileStream("osm.xml", FileMode.Open, FileAccess.Read))
                {
                    doc = XDocument.Load(stream);
                }
            }
            else
            {
                HttpClient client = new HttpClient();
                string url = "http://overpass-api.de/api/interpreter";
                string data = @"<osm-script output=""xml"" timeout=""250"">
    	                        <union>
                                    <query type=""way"">
                                        <has-kv k=""highway"" v=""""/>
                                        <bbox-query s=""52.07401979734621"" w=""20.672836303710934"" n=""52.395715477302076"" e=""21.56959533691406""/>
                                    </query>
                                </union>
                                <print mode=""body"" />
                                <recurse type=""down""/>
                                <print mode=""skeleton"" order=""quadtile""/>
                            </osm-script>";
                HttpContent content = new StringContent(data);
                var resp = await client.PostAsync(url, content);
                using (Stream stream = new FileStream("osm.xml", FileMode.Create, FileAccess.Write))
                using (StreamReader rd = new StreamReader(await resp.Content.ReadAsStreamAsync()))
                using (StreamWriter wr = new StreamWriter(stream))
                {
                    await wr.WriteAsync(await rd.ReadToEndAsync());
                    doc = XDocument.Parse(await resp.Content.ReadAsStringAsync());
                }
            }

            var streets = doc.Descendants("way")
                .Descendants("tag")
                .Where(x => x.Attribute("k").Value == "highway")
                .Select(x => x.Parent.Descendants("tag").FirstOrDefault(y => y.Attribute("k").Value == "name"))
                .Where(x => x != null)
                .Select(x => x.Attribute("v").Value)
                .Distinct()
                .OrderBy(x => x);

            Console.WriteLine(streets.Count());
            //var streets2 = streets.Take(20);

            //var streets = new string[]{"Inflancka", "Niska", "Karmelicka", "Miła", "Dzika"};

            foreach (var str in streets)
            {
                if (!uliceSprawdzone.Contains(str))
                {
                    Console.WriteLine(str);
                    await testBudynkow(str);
                }
            }
            Console.WriteLine(ileBledow);
        }

        private static async Task testBudynkow(string ulica)
        {
            List<Adres> list = new List<Adres>();

            HttpClient client = new HttpClient();
            string url = "http://overpass-api.de/api/interpreter";
            string data = string.Format(@"<osm-script output=""xml"" timeout=""250"">
    	                        <has-kv k=""addr:city"" v=""Warszawa""/>
                                <has-kv k=""addr:housenumber"" v=""""/>
    	                        <bbox-query s=""52.243489782391705"" w=""20.97538948059082"" n=""52.26358741792289"" e=""21.01203918457031""/>
                                <print mode=""skeleton"" order=""quadtile""/>
                                <union>
                                    <item/>
                                    <recurse type=""up""/>
                                </union>
                                <print mode=""body"" />
                            </osm-script>", ulica);
            HttpContent content = new StringContent(data);
            var resp = await client.PostAsync(url, content);
            string respString = await resp.Content.ReadAsStringAsync();

            XDocument doc = XDocument.Load(await resp.Content.ReadAsStreamAsync());
            var ways = doc.Descendants("way").Descendants("tag").Where(x => x.Attribute("k").Value == "addr:housenumber");
            foreach (var way in ways)
            {
                try
                {
                    var parent = way.Parent;
                    string street = parent.Descendants("tag").First(x => x.Attribute("k").Value == "addr:street").Attribute("v").Value;
                    if (street != ulica) continue;
                    string number = way.Attribute("v").Value;
                    var nodes = parent.Descendants("nd").Select(x => x.Attribute("ref").Value);
                    float lonSum = 0f, latSum = 0f;
                    int sum = 0;
                    foreach (var node in nodes)
                    {
                        try
                        {
                            var n = doc.Descendants("node").FirstOrDefault(x => x.Attribute("id").Value == node);
                            if (n == null) continue;
                            float lon = float.Parse(n.Attribute("lon").Value, CultureInfo.InvariantCulture);
                            float lat = float.Parse(n.Attribute("lat").Value, CultureInfo.InvariantCulture);

                            lonSum += lon;
                            latSum += lat;
                            sum++;
                        }
                        catch (Exception ex)
                        {
                            ileBledow++;
                            Console.WriteLine(ex.Message);
                            continue;

                        }
                    }
                    Console.WriteLine("\t{0} {1} \t\t Lat: {2}\tLon: {3}", street, number, latSum/sum, lonSum/sum);
                    list.Add(new Adres()
                    {
                        Numer = number,
                        Lat = latSum/sum,
                        Lon = lonSum/sum
                    });
                }
                catch (Exception ex)
                {
                    ileBledow++;
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }

            if (list.Count > 0)
            {
                using (var ctx = new FoodSearchEntities())
                {
                    var street = ctx.Streets.FirstOrDefault(x => x.Name == ulica) ?? ctx.Streets.Add(new Street() {Name = ulica});
                    await ctx.SaveChangesAsync();
                    foreach (var l in list)
                    {
                        Adres l1 = l;
                        var exists = ctx.Addresses.FirstOrDefault(x => x.StreetId == street.StreetId && x.Number == l1.Numer);
                        if (exists == null)
                        {
                            ctx.Addresses.Add(new Address()
                            {
                                StreetId = street.StreetId,
                                Number = l.Numer,
                                Lat = l.Lat,
                                Lon = l.Lon
                            });
                            await ctx.SaveChangesAsync();
                        }
                    }
                }
            }

            uliceSprawdzone.Add(ulica);
            using (Stream str = new FileStream("ulice.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(str))
            {
                foreach (var u in uliceSprawdzone)
                {
                    await sw.WriteLineAsync(u);
                }
            }
        }
    }
}
