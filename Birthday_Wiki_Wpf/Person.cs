using System;
using System.Collections.Generic;
using System.Linq;

namespace Birthday_Wiki_Wpf
{
    [Serializable]
    public class Person
    {
        public int id;
        public string name;
        public DateTime birthDate;
        public List<string> groupList;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public List<string> GroupList
        {
            get { return groupList; }
            set { groupList = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public Person()
        {
            name = "";
            birthDate = new DateTime();
            groupList = new List<string>();
            id = 0;
        }
        public Person(string n, DateTime d, List<string> l, int i)
        {
            name = n;
            birthDate = d;
            groupList = l;
            id = i;
        }
        public Person(string n, DateTime d, int i)
        {
            name = n;
            birthDate = d;
            groupList = null;
            id = i;
        }

        public Person(Person p)
        {
            name = p.name;
            birthDate = p.birthDate;
            groupList = p.groupList;
            id = p.id;
        }

        // временный вывод
        public string FormString()
        {
            int length = groupList.Count;
            string s = name + " ";
            foreach (string g in groupList)
            {
                if (groupList.Count == 1)
                    s += "(" + g + ")";
                else
                {
                    if (g == groupList.First())
                        s += "(" + g + ", ";
                    else if (g == groupList.Last())
                        s += g + ")";
                    else s += g + ", ";
                }
            }
            s += " - " + birthDate.Date.ToString("d");
            return s;
        }
    }
}
