using System.Drawing;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Parcial_1;
using System.Collections.Generic;

/* 
 PARCIAL 2.

Nos llamaron desde un restaurante para que creemos una aplicación que les ayude a gestionar los pedidos realizados por cada mesa,
para llevar un mejor control.

En su menú cuentan con 4 platos con los siguientes precios:
1)	Pizza - $400 (tarda 20 minutos en salir)
2)	Hamburguesa con papas - $250 (tarda 30 minutos en salir)
3)	Ensalada - $200 (tarda 5 minutos en salir)
4)	Docena de empanadas - $800 (tarda 25 minutos en salir)

Y cuentan con las siguientes bebidas:
1)	Agua - $80
2)	Cerveza de litro - $150
3)	Vino tinto - $250
4)	Coca cola de litro - $200
5)	Café - $100

Así mismo, cuentan con 6 mesas reconocidas por números del 1 al 6.

El programa lo usarán los mozos, por lo que deberán poder consultar qué mesas figuran libres,
adicionarle productos a la cuenta de la mesa de turno o liberar una mesa.

En caso de liberar una mesa, deberá imprimirse en pantalla la cuenta con el monto total a cobrar y disponibilizar
la mesa para que otro comensal la ocupe.

Cuando se adicione algún pedido a una mesa, deberá informarse cuánto va a tardar en entregar dicho pedido
(tomando como referencia el producto con mayor demora). */

int opcion = 0 ;
string respuesta;

var listaMenu = GetMenu();
var listMesas = GetMesas();

Console.WriteLine("CONTROL DE PEDIDOS. \n");
Console.WriteLine("** Elija una opcion:");
try
{
    do
    {
        Console.WriteLine("01_ Reservar mesa. \n02_ Sumar pedido.\n03_ Cerrar mesa.\n04_ Ver estado de las mesas.\n05_ Ver listado del menu.");

        opcion = int.Parse(Console.ReadLine());

        switch (opcion)
        {
            case 1:
                reservarMesa(listMesas);
                break;

            case 2:
                SumarPedido(listaMenu, listMesas);
                break;

            case 3:
                CerrarMesa(listMesas);
                break;

            case 4:
                VerEstadoMesas(listMesas);
                break;

            case 5:
                ListaMenu(listaMenu);
                break;
            default: Console.WriteLine("No es una opcion valida.");
                    break ;
        }
        Console.WriteLine("¿Desea seguir operando? (y/n)");
        respuesta = Console.ReadLine();
    } while (respuesta == "y");

}

catch (FormatException fex) 
{
    Console.WriteLine("Error de formato.");
}
catch (Exception errorGeneral)
{
    Console.WriteLine("error no identificado");
}

finally
{
    Console.WriteLine("esto es el finally. Listo");
}




static List<Menu> GetMenu()
{
    return new List<Menu>()
    {
        new Menu(1, "Pizza", 400, 20),
        new Menu(2, "Hamburguesa con papas", 250, 30),
        new Menu(3, "Ensalada", 200, 5),
        new Menu(4, "Docena de empanadas", 800, 25),
        new Menu(5, "Agua", 80, 0),
        new Menu(6, "Cerveza de litro", 150, 0),
        new Menu(7, "Vino tinto", 250, 0),
        new Menu(7, "Coca cola de litro", 200, 0),
        new Menu(9, "Cafe", 100, 0)
    };
}

static List<Mesa> GetMesas()
{
    return new List<Mesa>()
    {
        new Mesa(1, "Vacia", 0, 0, new List<string>() ),
        new Mesa(2, "Vacia", 0, 0, new List<string>() ),
        new Mesa(3, "Vacia", 0, 0, new List<string>() ),
        new Mesa(4, "Vacia", 0, 0, new List<string>() ),
        new Mesa(5, "Vacia", 0, 0, new List<string>() ),
        new Mesa(6, "Vacia", 0, 0, new List<string>() )
    };                    
}

static void reservarMesa(List<Mesa> listMesas)
{
    foreach (var mesas in listMesas)
        Console.WriteLine($"Mesa {mesas.Id}: {mesas.Disponibilidad}");

    Console.WriteLine("Seleccione una mesa:");

    int selectmesa2 = (int.Parse(Console.ReadLine())) - 1;
    listMesas[selectmesa2].Disponibilidad = "Ocupada";
}

static void SumarPedido(List<Menu> listaMenu, List<Mesa> listMesas)
{
    Console.WriteLine("Agregar pedido a la mesa: ");
    foreach (var mesas in listMesas)
        Console.WriteLine($"Mesa {mesas.Id}: {mesas.Disponibilidad}");

    int mesa = (int.Parse(Console.ReadLine())) - 1;
    if (mesa <= 6)
    {
        Console.WriteLine("¿Cuantos pedidos agregara a la mesa?");
        int cantidad = int.Parse(Console.ReadLine());

        foreach (var menu in listaMenu)
            Console.WriteLine($"{menu.IdM} : {menu.Name} // Valor : ${menu.Price} ");
        Console.WriteLine("Ingrese los numeros indicados: ");
        for (int i = 0; i < cantidad; i++)
        {
            var orden = (int.Parse(Console.ReadLine())) - 1;
            listMesas[mesa].CuentaTotal += listaMenu[orden].Price;
            listMesas[mesa].Time += listaMenu[orden].Time;
            listMesas[mesa].Pedido.Add(listaMenu[orden].Name);
        }
    }
    else
    {
        Console.WriteLine("Esta mesa no existe.");
    }

    Console.WriteLine($"Mesa: {listMesas[mesa].Id} \nTiempo: {listMesas[mesa].Time} minutos \nCuenta: ${listMesas[mesa].CuentaTotal} \nLista pedidos:");
    listMesas[mesa].Pedido.ForEach(x => Console.WriteLine(x));

    /* foreach (var pedido in listMesas[mesa].Pedido)
         Console.WriteLine(pedido);*/
}

static void CerrarMesa(List<Mesa> listMesas)
{
    int selecMesa;
    Console.WriteLine("¿Que mesa va a cerrar?");
    selecMesa = (int.Parse(Console.ReadLine()) - 1);
    Console.WriteLine($"La cuenta total de esta mesa es: ${listMesas[selecMesa].CuentaTotal}");
    listMesas[selecMesa].Disponibilidad = "Vacia";
    listMesas[selecMesa].Time = 0;
    listMesas[selecMesa].CuentaTotal = 0;
    foreach (var item in listMesas)
    {
        listMesas[selecMesa].Pedido.Clear();
    }
}

static void VerEstadoMesas(List<Mesa> listMesas)
{
    foreach (var mesas in listMesas)
    {
        Console.WriteLine($"Mesa n° {mesas.Id}: {mesas.Disponibilidad} / Tiempo de espera: {mesas.Time} minutos / Cuenta total: ${mesas.CuentaTotal} / Lista de pedidos:");

        foreach (var pedido in listMesas[(mesas.Id) - 1].Pedido)
            Console.WriteLine(pedido);
    }
}

static void ListaMenu(List<Menu> listaMenu)
{
    foreach (var menu in listaMenu)
    {
        Console.WriteLine($"Id Producto: {menu.IdM} / Nombre: {menu.Name} / Precio: ${menu.Price} / Tiempo: {menu.Time} minutos");
    }
}