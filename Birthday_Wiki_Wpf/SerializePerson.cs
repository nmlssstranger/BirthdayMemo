using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;


namespace Birthday_Wiki_Wpf
{
    class SerializePerson
    {
        public static void Serialize(ObservableCollection<Person> personList)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Person>));
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, personList);
            }
        }
    }
}
