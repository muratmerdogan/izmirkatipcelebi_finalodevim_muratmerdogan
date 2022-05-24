using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XmlDatabaseMerdogan
{
    class XmlDatabase : ICrudMethod
    {
        public bool ConnectAndCreate()
        {
            try
            {
                if (!File.Exists("PersonDatabase.xml"))
                {
                    XmlTextWriter xmlCreate = new XmlTextWriter("PersonDatabase.xml", null);
                    xmlCreate.WriteStartDocument();
                    xmlCreate.WriteComment("İzmir Katip Çelebi-Murat Merdoğan");
                    xmlCreate.WriteStartElement("Persons");
                    xmlCreate.WriteEndDocument();
                    xmlCreate.Close();

                }
                return true;
            }
            catch
            {
                return false;

            }
        }
        public bool Add(Person person)
        {
          if(ConnectAndCreate())
          {
              XmlDocument doc = new XmlDocument();
              doc.Load("PersonDatabase.xml");
              XmlElement root = doc.DocumentElement;
              XmlNodeList kayitlar = root.SelectNodes("/Persons/Person");
              XmlElement UserElement = doc.CreateElement("Person");




          

              XmlElement PersonId = doc.CreateElement("PersonId");
              PersonId.InnerText = (GetList().Max(e => e.PersonId) +1 ).ToString();
              UserElement.AppendChild(PersonId);

     
              XmlElement name = doc.CreateElement("Name");
              name.InnerText = person.Name;
              UserElement.AppendChild(name);

              XmlElement SurName = doc.CreateElement("SurName"); 
              SurName.InnerText = person.Surname;
              UserElement.AppendChild(SurName);
              doc.DocumentElement.AppendChild(UserElement);


            



              XmlElement WorkStartDate = doc.CreateElement("WorkStartDate");
              WorkStartDate.InnerText = person.WorkStartDate.ToString();
              UserElement.AppendChild(WorkStartDate);
              doc.DocumentElement.AppendChild(UserElement);


              XmlElement Salary = doc.CreateElement("Salary");
              Salary.InnerText = person.Salary.ToString();
              UserElement.AppendChild(Salary);
              doc.DocumentElement.AppendChild(UserElement);



              XmlElement Age = doc.CreateElement("Age");
              Age.InnerText = person.Age.ToString();
              UserElement.AppendChild(Age);
              doc.DocumentElement.AppendChild(UserElement);

              XmlTextWriter xmlInsert = new XmlTextWriter("PersonDatabase.xml", null);
              xmlInsert.Formatting = Formatting.Indented;
              doc.WriteContentTo(xmlInsert);
              xmlInsert.Close();
              return true;
          }
          else
          {
              return false;
          }
        }
        public bool Delete(int personId)
        {
            try
            {
                XDocument XMLDoc = XDocument.Load("PersonDatabase.xml");
                XElement elment = (from xml1 in XMLDoc.Descendants("Person")
                                   select xml1).Where(e=>e.Element("PersonId").Value==personId.ToString()).FirstOrDefault();

               
                elment.Remove();
                XMLDoc.Save("PersonDatabase.xml");
                return true;
            }
            catch
            {
                return false;

            }
        }
        public bool Update(Person person)
        {
            return false;

        }
        public List<Person> GetList()
        {
            XDocument xDoc = XDocument.Load(@"PersonDatabase.xml");
            List<Person> list = (from element in xDoc.Descendants("Person")
                                 select new Person() {
                                     PersonId = Convert.ToInt32(element.Element("PersonId").Value),
                                     Name = element.Element("Name").Value,
                                     Surname = element.Element("SurName").Value, 
                                     Age = Convert.ToInt32(element.Element("Age").Value),
                                     Salary = Convert.ToDecimal(element.Element("Salary").Value), 
                                     WorkStartDate = Convert.ToDateTime(element.Element("WorkStartDate").Value
                                     
                                     ) }).ToList();
            return list;
        }
    }
}
