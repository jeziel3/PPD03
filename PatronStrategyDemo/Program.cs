using System;

namespace PatronStrategyDemo
{
    // 1. Strategy (Interfaz común para los algoritmos)
    public interface IEstrategiaDescuento
    {
        decimal CalcularDescuento(decimal montoOriginal);
    }

    // 2. ConcreteStrategy A (Sin descuento para clientes regulares)
    public class DescuentoRegular : IEstrategiaDescuento
    {
        public decimal CalcularDescuento(decimal montoOriginal)
        {
            Console.WriteLine("Aplicando política: Cliente Regular (0% de descuento).");
            return montoOriginal;
        }
    }

    // 3. ConcreteStrategy B (20% de descuento para clientes VIP)
    public class DescuentoVip : IEstrategiaDescuento
    {
        public decimal CalcularDescuento(decimal montoOriginal)
        {
            Console.WriteLine("Aplicando política: Cliente VIP (20% de descuento).");
            return montoOriginal * 0.80m;
        }
    }

    // 4. Context (Mantiene la referencia a la estrategia y delega la operación)
    public class CarritoDeCompras
    {
        private IEstrategiaDescuento _estrategiaDescuento;

        // Permite inyectar o cambiar la estrategia en tiempo de ejecución
        public void SetEstrategia(IEstrategiaDescuento estrategia)
        {
            _estrategiaDescuento = estrategia;
        }

        public decimal ProcesarPago(decimal monto)
        {
            if (_estrategiaDescuento == null)
            {
                throw new InvalidOperationException("Debe asignar una estrategia de descuento antes de procesar el pago.");
            }

            return _estrategiaDescuento.CalcularDescuento(monto);
        }
    }

    // 5. Programa principal de prueba
    class Program
    {
        static void Main(string[] args)
        {
            var carrito = new CarritoDeCompras();
            decimal montoCompra = 150.00m;

            Console.WriteLine($"Monto inicial de la compra: ${montoCompra}\n");

            // Caso 1: Comportamiento con estrategia regular
            carrito.SetEstrategia(new DescuentoRegular());
            decimal totalRegular = carrito.ProcesarPago(montoCompra);
            Console.WriteLine($"Total a pagar: ${totalRegular}\n-----------------------------------");

            // Caso 2: Se cambia dinámicamente a estrategia VIP en tiempo de ejecución
            carrito.SetEstrategia(new DescuentoVip());
            decimal totalVip = carrito.ProcesarPago(montoCompra);
            Console.WriteLine($"Total a pagar: ${totalVip}\n-----------------------------------");

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}