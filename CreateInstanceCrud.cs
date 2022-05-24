using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlDatabaseMerdogan
{
    class CreateInstanceCrud
    {

        public static ICrudMethod Create()
        {

            // Burası confiG dosyasıyla değitirilebir,Sql Veritabanıyla hızlı bir şekilde entegre olur

            return new XmlDatabase();

        }

    }
}
