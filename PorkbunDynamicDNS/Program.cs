using PorkbunDynamicDNS;
using PorkbunDynamicDNS.Models;

string domain = ConfigurationManager.Get("Domain");
string[] subdomains = ConfigurationManager.Get("Subdomains").Split(',');
Console.WriteLine($"Domain: {domain}\nSubdomains: {string.Join(", ", subdomains)}");

PorkbunAPI porkbunAPI = new PorkbunAPI(ConfigurationManager.Get("API:BaseAddress"),
                                       ConfigurationManager.Get("API:ApiKey"),
                                       ConfigurationManager.Get("API:SecretApiKey"));

string ip = string.Empty;
try
{
    ip = await porkbunAPI.GetIP();
    Console.WriteLine($"IP address: {ip}");
}
catch (Exception ex)
{
    Console.WriteLine("Error when retrieving IP address: " + ex.Message);
    Environment.Exit(-1);
}

IEnumerable<Record>? records = (await porkbunAPI.GetRecords(domain))?.Where(r => r.Type == "A");

foreach (string subdomain in subdomains)
{
    string host = subdomain + "." + domain;
    try
    {
        if (records is not null && records.Any(r => r.Name == host))
        {
            if (records.First(r => r.Name == host).Content != ip)
            {
                await porkbunAPI.EditRecord(domain, subdomain, ip);
                Console.WriteLine($"{host}: DNS record updated.");
            }
            else
            {
                Console.WriteLine($"{host}: Already up-to-date.");
            }
        }
        else
        {
            await porkbunAPI.CreateRecord(domain, subdomain, ip);
            Console.WriteLine($"{host}: DNS record created.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(host + ": " + ex.Message);
    }
}