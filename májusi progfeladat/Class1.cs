using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace májusi_progfeladat
{
    public class user
    {


        private static user instance;
        public string Username { get; set; }
        public string Password { get; set; }

        private user() { }

        public static user Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new user();
                }
                return instance;
            }
        }
        /*public string name { get; set; }
        public string pass { get; set; }


        public static user UserQuery(string nev, string jelszo)
        {

            user felhasz = null;
            felhasz.name = nev;
            felhasz.pass = jelszo;
        }

        //public string Nev { get => name; set => name = value; }
        //public string Jelszo { get => pass; set => pass = value; }*/
    }
}
