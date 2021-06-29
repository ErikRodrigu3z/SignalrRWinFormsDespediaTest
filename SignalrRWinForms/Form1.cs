using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalrRWinForms
{
    public partial class Form1 : Form
    {
        //GetNewTravels
        private string url = "http://vmi562316.contaboserver.net:9095/travelHub"; 
        private string url2 = "https://localhost:5001/travelHub";  
        //conexion
        HubConnection connection;

       


        public Form1()
        {
            InitializeComponent(); 

            //para evitar error con el certificado SSL
            connection = new HubConnectionBuilder()
            .WithUrl(new Uri(url), options => { 
                var handler = new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                };
                options.HttpMessageHandlerFactory = _ => handler;
            })
            .Build();

            //en caso de desconexion se intentara conectar de nuevo 
            connection.Closed += async (error) =>
            {
                Thread.Sleep(5000);  // cada 5 segundos
                await connection.StartAsync();
            };

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();

                connection.On<string>("GetTravelList", (travel) =>
                {
                    txtJson.Text = travel;
                });

                connection.On<string>("TravelId" + txtTravelId.Text, (travel) =>
                {
                    txtJson.Text = travel;                    
                });



                
            } 
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.Message + " -----> \r\n" + ex.InnerException.Message);
                }

                MessageBox.Show(ex.Message);
            }
        }

       
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.InvokeAsync("GetNewTravels");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.Message + " -----> \r\n" + ex.InnerException.Message);
                }

                MessageBox.Show(ex.Message);
            }
        }



        private async void button2_Click(object sender, EventArgs e)
        {
            //Form2 f2 = new Form2("Nombre: " + typeof(Form1).Name);
            //f2.Show();

            try
            {
                await connection.InvokeAsync("GetTravelById", txtTravelId.Text);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.Message + " -----> \r\n" + ex.InnerException.Message);
                }

                MessageBox.Show(ex.Message);
            }



        }


    }
}
