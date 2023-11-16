using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFreamework
{
    public class EfAccountDal : GenericUowRepository<Account>, IAccountDal
    {
        // burdaki base GeneriUowRepository nin içindeki ctor una context gönderiyoruz yani o ctor u kullan gibi
        public EfAccountDal(Context context) : base(context)
        {

        }
    }
}
