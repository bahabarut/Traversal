using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRApiForSql.DAL;
using SignalRApiForSql.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApiForSql.Models
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
            await _hubContext.Clients.All.SendAsync("ReceiveVisitorList", GetVisitorCharList());
        }


        //charta verileri basacak fonksiyonumuz
        public List<VisitorChart> GetVisitorCharList()
        {
            List<VisitorChart> visitorCharts = new List<VisitorChart>();
            //burda sorgu komutu oluşturduk
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select tarih,[1],[2],[3],[4],[5] from (select[City], CityVisitCount,Cast([VisitDate] as Date) as tarih from Visitors) as VisitTable Pivot (Sum(CityVisitCount) for City in([1],[2],[3],[4],[5])) as pivottable order by tarih asc";
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

                            if (DBNull.Value.Equals(reader[x]))
                            {
                                visitorChart.Counts.Add(0);
                            }
                            else
                            {

                                visitorChart.Counts.Add(reader.GetInt32(x));
                            }
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
