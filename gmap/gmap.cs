﻿using System;
using System.Windows.Forms;
using model;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace gmap
{
    public partial class gmap : Form
    {
        /// <summary>
        /// Relacion con el modelo.
        /// </summary>
        private SupplyCenter supplyCenter;
        private GMarkerGoogle marker;
        private GMapOverlay markerOverlay;
 


        public gmap()
        {
            InitializeComponent();

            supplyCenter = new SupplyCenter();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

      

        private void btSearchStation_Click(object sender, EventArgs e)
        {

        }

        private void btClearStation_Click(object sender, EventArgs e)
        {

        }

        private void btClearFilter_Click(object sender, EventArgs e)
        {
            rbMonth.Enabled = true;
            rbMunicipaly.Enabled = true;
            rbPrice.Enabled = true;
            rbProduct.Enabled = true;
            rbFlag.Enabled = true;

        
           
            gMapC.Overlays.Clear();
            
        }

        private void gmap_Load(object sender, EventArgs e)
        {
            //Elementos de inicio Gmap
            gMapC.DragButton = MouseButtons.Left;
            gMapC.CanDragMap = true;
            gMapC.MapProvider = GMap.NET.MapProviders.OpenCycleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapC.Position = new GMap.NET.PointLatLng(4.570868, -74.2973328);
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapC.MinZoom = 2;
            gMapC.MaxZoom = 18;
            gMapC.Zoom = 6;
            //Fin


            supplyCenter.loadData();



            
    
            int i = 0;
            foreach (var aux in supplyCenter.PetrolStation)
            {

                if (i < 100)
                {
                
                Geocoding(aux.NameDepartment,aux.NameMunicipality,aux,GMarkerGoogleType.green);
                
                    i++;
                }

            }

           
             






        }



        private void AddMarker(PointLatLng pointToAdd, GMarkerGoogleType gMarkerGoogleType,PetrolStation aux)
        {
            var markerOverlay = new GMapOverlay("markers");
            var marker = new GMarkerGoogle(pointToAdd, gMarkerGoogleType);
            markerOverlay.Markers.Add(marker);


            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = String.Format("Mes: " + aux.Month + "\n" + "Departament: " + aux.NameDepartment + "\n" + "Municipio: " + aux.NameMunicipality + "\n" + "Nombre comercial: " + aux.TradeName + "\n" + "Bandera: " + aux.Flag + "\n" + "Direccion: " + aux.Addres + "\n" + "Producto: " + aux.TypeProduct + "\n" + "Precio: " + aux.Price);

            gMapC.Overlays.Add(markerOverlay);

         }

       



        /// <summary>
        /// Permite realizar la geocodificacion es decir, que permite pasar la dirección de un lugar a codigo.
        /// </summary>
        /// <param name="nameDepartament"></param> nombre del departamento donde se encuentra la estación de gasolina.
        /// <param name="municipality"></param> nombre del municipio donde se encuentra la estación  de gasolina.
        /// <param name="aux"></param>  
        private void Geocoding(string nameDepartament, string municipality,PetrolStation aux, GMarkerGoogleType type)
        {
         //geocodificación
            GeoCoderStatusCode statusCode;
       
            var pointLatng = OpenCycleMapProvider.Instance.GetPoint(nameDepartament + " " + municipality, out statusCode);
            
            var lat = pointLatng?.Lat.ToString();
            var lng = pointLatng?.Lng.ToString();
            if (lat != null && lng != null)
            {
            AddMarker(new PointLatLng(Double.Parse(lat), Double.Parse(lng)), type, aux);
            }

        }

        private void rbMonth_CheckedChanged(object sender, EventArgs e)
        {

            cbFilter.Items.Clear();
            foreach (String m in supplyCenter.getMonths())
            {
                cbFilter.Items.Add(m);    
            }

            rbMunicipaly.Enabled=false;
            rbFlag.Enabled=false;
            rbPrice.Enabled=false;
            rbProduct.Enabled=false;
  

        }

        private void rbMunicipaly_CheckedChanged(object sender, EventArgs e)
        {

            cbFilter.Items.Clear();
            foreach (String m in supplyCenter.getMunicipalies())
            {
                cbFilter.Items.Add(m);
            }

            rbMonth.Enabled = false;
            rbFlag.Enabled = false;
            rbPrice.Enabled = false;
            rbProduct.Enabled = false;


        }

        private void rbFlag_CheckedChanged(object sender, EventArgs e)
        {
            cbFilter.Items.Clear();
            foreach (String m in supplyCenter.getFlags())
            {
                cbFilter.Items.Add(m);
            }

            rbMonth.Enabled = false;
            rbMunicipaly.Enabled = false;
            rbPrice.Enabled = false;
            rbProduct.Enabled = false;
        }

        private void rbProduct_CheckedChanged(object sender, EventArgs e)
        {
            cbFilter.Items.Clear();
            foreach (String m in supplyCenter.getProducts())
            {
                cbFilter.Items.Add(m);
            }

            rbMonth.Enabled = false;
            rbFlag.Enabled = false;
            rbPrice.Enabled = false;
            rbMunicipaly.Enabled = false;
        }

        private void rbPrice_CheckedChanged(object sender, EventArgs e)
        {
            cbFilter.Items.Add("Del Mínimo al Promedio ");
            cbFilter.Items.Add("Del Promedio al Máximo ");
            cbFilter.Items.Add("El Máximo ");
            cbFilter.Items.Add("El Mínimo ");

            
            rbMonth.Enabled = false;
            rbFlag.Enabled = false;
            rbProduct.Enabled = false;
            rbMunicipaly.Enabled = false;

        }

        private void btFilter_Click(object sender, EventArgs e)
        {
            gMapC.Overlays.Clear();
            String element = cbFilter.Text;


            if (rbMonth.Enabled)
            {
                var list = supplyCenter.SearchByMonth(element);
                int i = 0;
                foreach (PetrolStation aux in list)
                {
                    if (i < 100)
                    {
                      
                        Geocoding(aux.NameDepartment, aux.NameMunicipality, aux, GMarkerGoogleType.blue);

                        i++;
                    }
                }

            }else if (rbMunicipaly.Enabled)
            {
                var list = supplyCenter.SearchByMunicipality(element);
                int i = 0;
                foreach (PetrolStation aux in list)
                {
                    if (i < 100)
                    {

                        Geocoding(aux.NameDepartment, aux.NameMunicipality, aux, GMarkerGoogleType.yellow);

                        i++;
                    }
                }
            }else if (rbFlag.Enabled)
            {
                var list = supplyCenter.SearchByFlag(element);
                int i = 0;
                foreach (PetrolStation aux in list)
                {
                    if (i < 100)
                    {

                        Geocoding(aux.NameDepartment, aux.NameMunicipality, aux, GMarkerGoogleType.orange);

                        i++;
                    }
                }
            }else if (rbProduct.Enabled)
            {


                var list = supplyCenter.SearcByProduct(element);
                int i = 0;
                foreach (PetrolStation aux in list)
                {
                    if (i < 100)
                    {

                        Geocoding(aux.NameDepartment, aux.NameMunicipality, aux, GMarkerGoogleType.red_small);

                        i++;
                    }
                }
            }else if (rbPrice.Enabled)
            {
                if (cbFilter.Text.Equals("Del Mínimo al Promedio "))
                {
                    var list = supplyCenter.getMinorPrices();
                    int i = 0;
                    foreach (PetrolStation aux in list)
                    {
                        if (i < 100)
                        {

                            Geocoding(aux.NameDepartment, aux.NameMunicipality, aux, GMarkerGoogleType.red_small);

                            i++;
                        }
                    }

                }
                else if (cbFilter.Text.Equals("Del Promedio al Máximo ")) {
                    var list = supplyCenter.getMajorsPrices();
                    int i = 0;
                    foreach (PetrolStation aux in list)
                    {
                        if (i < 100)
                        {

                            Geocoding(aux.NameDepartment, aux.NameMunicipality, aux, GMarkerGoogleType.red_small);

                            i++;
                        }
                    }

                }
                else if (cbFilter.Text.Equals("El Máximo ")) {
                    var value = supplyCenter.getMaxPrice();
                  

                     Geocoding(value.NameDepartment, value.NameMunicipality, value, GMarkerGoogleType.red_dot);

                      

                }
                else if (cbFilter.Text.Equals("El Mínimo "))
                {
                    var value = supplyCenter.getMinPrice();
                    Geocoding(value.NameDepartment, value.NameMunicipality, value, GMarkerGoogleType.red_dot);

                }
            }

        }




    }
}
