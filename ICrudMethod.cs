using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlDatabaseMerdogan
{
    interface ICrudMethod
    {
          bool Add(Person person);
          bool Delete(int personId);
          bool Update(Person person);
          List<Person> GetList();
    }
}
