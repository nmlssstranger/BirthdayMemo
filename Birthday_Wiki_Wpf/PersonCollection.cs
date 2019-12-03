using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Birthday_Wiki_Wpf
{
    [Serializable]
    public class PersonCollection // перепилить под наследование списка List<string>
    {
        public PersonCollection()
        {
            Collection = new List<Person>();
        }
        public PersonCollection(PersonCollection pc)
        {
            Collection = pc.Collection;
        }
        [XmlArray("Collection"), XmlArrayItem("Person")]
        public List<Person> Collection { get; set; }

        // вывод всего списка в текстовом виде
        //public List<string> FormTextList()
        //{
        //    List<string> formedList = new List<string>();
        //    foreach (Person p in Collection)
        //    {
        //        formedList.Add(p.FormString());
        //    }
        //    return formedList;
        //}

        // перемещаем прошедших именинников в конец списка
        public void ReplacePastToEnd()
        {
            int pCount = Collection.Count;
            int i = 0, j = 0;
            while ((i < pCount) && (j < pCount))
            {
                if (Collection[i].BirthDate.DayOfYear < DateTime.Now.DayOfYear)
                {
                    Person moving = Collection[i];
                    Collection.RemoveAt(i);
                    Collection.Add(moving);
                    j++;
                }
                else
                {
                    i++;
                    j++;
                }
            }
        }

        public Person GetPersonById(int id)
        {
            return Collection.Find(x => (x.id == id));
        }
    }
}
