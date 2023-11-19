using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRApi.DAL;
using SignalRApi.Hubs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Models
{
    public class VisitorService
    {
        /*
        - her bir gün 5 saniyede tamamlanacak
        - her günde 5 farkllı şehre random değer gelecek
        - Totalda 10 ardışık günün değeri tabloya eklenecek
        - işlemler toplam 50 saniye sürecek 
        - her saniyede tablonun son hali gözükecek
        - toplamda 50 satır kayıt eklenmiş olacak
        - bu işlemler postmanda gerçekleşecek
         */
        private readonly Context _context;
        private readonly IHubContext<VisitorHub> _hubContext;

        public VisitorService(Context context, IHubContext<VisitorHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Visitor> GetList()
        {
            return _context.Visitors.AsQueryable();
        }

        public async Task SaveVisitor(Visitor visitor)
        {
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("", "Buraya veri gelecek");
        }


        //charta verileri basacak fonksiyonumuz
        public List<VisitorChart> GetVisitorCharList()
        {
            List<VisitorChart> visitorCharts = new List<VisitorChart>();
            //burda sorgu komutu oluşturduk
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "Select * From crosstab( 'Select VisitDate,City,CityVisitCount From Visitors Order By 1,2 ') As ct(VisitDate TimeStamp,City1 int,City2 int,City3 int,City4 int,City5 int);";
                //sorgu türü ya procedur yada query olarak query belirledik text
                command.CommandType = System.Data.CommandType.Text;
                //baglantıyı aç
                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    //komut okunduğu sürece 
                    while (reader.Read())
                    {
                        VisitorChart visitorChart = new VisitorChart();
                        visitorChart.VisitDate = reader.GetDateTime(0);
                        //her bir şehir için tarihleri alıyoruz
                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            visitorChart.Counts.Add(reader.GetInt32(x));
                        });
                        visitorCharts.Add(visitorChart);
                    }
                }
                _context.Database.CloseConnection();
                return visitorCharts;
            }
        }
    }
}
