﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class SupplyCenter
    {

        public const String DATA_PATH = "..\\..\\..\\DATA_PreciosCombustibles.csv";




        private List<PetrolStation> petrolStation;

        
        public SupplyCenter()
        {
            PetrolStation = new List<PetrolStation>();

        }



        public void loadData()
        {
            StreamReader sr = new StreamReader(DATA_PATH);
            string line = sr.ReadLine();
            line = sr.ReadLine();
            ///Inicio
            while (line != null)
            {
                String[] elements = line.Split(';');

                String month = elements[0];
                String nameDepartment = elements[1];
                String nameMunicipality = elements[2];
                String tradeName = elements[3];
                string flag = elements[4];
                String addres = elements[5];
                String typeProduct = elements[6];
                String[] values = elements[7].Split(' ');
                double price = Double.Parse(values[1]);


                PetrolStation.Add(new PetrolStation(month, nameDepartment, nameMunicipality, tradeName, flag, addres, typeProduct, price));
                line = sr.ReadLine();
            }///Fin




        }
        /**
        public List<PetrolStation> SearchByStation(string name){
            List<PetrolStation> aux = new List<PetrolStation>();
          
            foreach (PetrolStation value in aux)
            {
                if (value.Flag.Equals(name))
                {
                    aux.Add(value);
                }
            }
            
          return aux;

        }
        **/

        /**
         * This method permited fill by month
         */
        public List<PetrolStation> SearchByMonth(string month){
            List<PetrolStation> aux = new List<PetrolStation>();
          
            foreach (PetrolStation value in aux)
            {
                if (value.Month.Equals(month))
                {
                    aux.Add(value);
                }
            }
            
          return aux;

        }

        
        /**
        * This method permited fill by municipality
        */
        public List<PetrolStation> SearchByMunicipality(string municipality)
        {
            List<PetrolStation> aux = new List<PetrolStation>();

            foreach (PetrolStation value in aux)
            {
                if (value.NameMunicipality.Equals(municipality))
                {
                    aux.Add(value);
                }
            }

            return aux;

        }

        /**
       * This method permited fill by flag
       */
        public List<PetrolStation> SearchByFlag(string flag)
        {
            List<PetrolStation> aux = new List<PetrolStation>();

            foreach (PetrolStation value in aux)
            {
                if (value.Flag.Equals(flag))
                {
                    aux.Add(value);
                }
            }

            return aux;

        }


        /**
    * This method permited fill by product
    */
        public List<PetrolStation> SearcByProduct(string product)
        {
            List<PetrolStation> aux = new List<PetrolStation>();

            foreach (PetrolStation value in aux)
            {
                if (value.TypeProduct.Equals(product))
                {
                    
                    aux.Add(value);
                }
            }

            return aux;

        }



        /**
 * This method permited fill by product
 */
        public List<PetrolStation> SearcByPrice(string price)
        {
            List<PetrolStation> aux = new List<PetrolStation>();

            foreach (PetrolStation value in aux)
            {
                if (value.Price.Equals(price))
                {

                    aux.Add(value);
                }
            }

            return aux;

        }





        public void addPetrolStation(string month, string nameDepartment, string latitude, string longitude, string nameMunicipality, string tradeName, string flag, string addres, string typeProduct, double price) {
           
        }
        public List<PetrolStation> PetrolStation { get => petrolStation; set => petrolStation = value; }


    }

}
