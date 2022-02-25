using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PageList
    {
        public List<Client> Clients { get; set; }
        public List<Client> OffsetClients => Clients.Skip(CurrentPage * CountInPage).Take(CountInPage).ToList();
        public int CountInPage { get; set; } = 20;
        public int CurrentPage { get; set; } = 0;
        public bool IsFirstPage => CurrentPage != 0;
        public bool IsLastPage => Clients.Count - ((CurrentPage + 2) * CountInPage) > -CountInPage;

        /*public PageList(List<Client> clients)
        {
            Clients = clients;
            Clients.ForEach(client =>
            {
                client.ClientService = client.ClientService.OrderByDescending(c => c.StartTime).ToList();
            });
        }*/
        public PageList(List<Client> clients) => Clients = clients;
    }
}
