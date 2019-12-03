using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows;

namespace Birthday_Wiki_Wpf
{
    class DeserializePerson
    {
        public static PersonCollection Deserialize(string path)
        {
            PersonCollection personCollection = new PersonCollection();
            XmlSerializer formatter = new XmlSerializer(typeof(PersonCollection));
            // десериализация, извлекаем список персон и выводим
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (new System.IO.FileInfo(path).Length != 0)
                {
                    try
                    {
                        personCollection = (PersonCollection)formatter.Deserialize(fs);
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Что-то пошло не так!\n\n" + ex.ToString(),
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            return personCollection;
        }
    }
}
