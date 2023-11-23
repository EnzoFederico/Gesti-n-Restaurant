using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_1
{
    public class Mesa
    {
        public int Id { get; set; }
        public string Disponibilidad { get; set; }
        public int Time { get ; set; }
        public int CuentaTotal { get; set; }
        public List<string> Pedido { get; set; }

        public Mesa(int id, string disponibilidad, int time, int cuentaTotal, List<string> pedido) 
        {
            Id = id;
            Disponibilidad = disponibilidad;
            Time = time;
            CuentaTotal = cuentaTotal;
            Pedido = pedido;
        }

        public string MesaDisponible()
        {
            return $"Disponibilidad: {Disponibilidad}";
        }

        public void Reservar()
        {

        }

        public void tiempoEspera(List<Mesa> listMesas,int x, int orden, List<Menu> listaMenu )
        {
            listMesas[x].Time += listaMenu[orden].Time;
        }
    }
}
