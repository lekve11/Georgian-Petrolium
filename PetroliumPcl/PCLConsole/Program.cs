﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PetroliumPcl;
using PetroliumPcl.Petrolium;

namespace PCLConsole
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<PetrolCompany> petroliums = PetrolCompanyContext.GetPetroliums();

            foreach (var item in petroliums)
            {
                Console.WriteLine("----" + item.Name + "----");
                foreach (var f in item.Fuels)
                {
                    Console.WriteLine(String.Format("{0} {1} {2}  -> {3}", f.NameEn, f.NameRu, f.NameGe, f.Price));
                }
            }

            
            Console.ReadLine();
        }
    }
}
