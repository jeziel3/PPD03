using System;

namespace BuilderVehiculosDemo
{
    // 1. Product (El objeto complejo resultante)
    public class Vehiculo
    {
        public string Motor { get; set; } = string.Empty;
        public string Transmision { get; set; } = string.Empty;
        public string ColorPintura { get; set; } = string.Empty;
        public bool TieneTechoCorredizo { get; set; }

        public void MostrarDetalles()
        {
            Console.WriteLine($"[Vehículo Ensamblado Exitosamente]\n- Motor: {Motor}\n- Transmisión: {Transmision}\n- Color: {ColorPintura}\n- Techo Corredizo: {(TieneTechoCorredizo ? "Sí" : "No")}\n");
        }
    }

    // 2. Builder (Interfaz con los pasos de construcción)
    public interface IVehiculoBuilder
    {
        void SetMotor(string motor);
        void SetTransmision(string transmision);
        void SetColor(string color);
        void SetTechoCorredizo(bool estado);
        Vehiculo GetVehiculo();
    }

    // 3. ConcreteBuilder (Construye un vehículo de tipo Deportivo)
    public class AutoDeportivoBuilder : IVehiculoBuilder
    {
        private Vehiculo _vehiculo = new Vehiculo();

        public void SetMotor(string motor) => _vehiculo.Motor = $"{motor} (Alto Rendimiento)";
        public void SetTransmision(string transmision) => _vehiculo.Transmision = $"{transmision} Deportiva Secuencial";
        public void SetColor(string color) => _vehiculo.ColorPintura = $"{color} Metálico Racing";
        public void SetTechoCorredizo(bool estado) => _vehiculo.TieneTechoCorredizo = estado;

        public Vehiculo GetVehiculo()
        {
            Vehiculo productoTerminado = _vehiculo;
            _vehiculo = new Vehiculo(); // Reinicia el builder para una nueva instancia
            return productoTerminado;
        }
    }

    // 4. Director (Orquesta una receta fija de construcción)
    public class Director
    {
        public void ConstruirAutoDeportivoCompleto(IVehiculoBuilder builder)
        {
            builder.SetMotor("V8 Bi-Turbo");
            builder.SetTransmision("Automática de 8 velocidades");
            builder.SetColor("Rojo Ferrari");
            builder.SetTechoCorredizo(true);
        }
    }

    // 5. Programa Principal (Punto de entrada de consola)
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DEMOSTRACIÓN PATRÓN BUILDER ===\n");

            IVehiculoBuilder builder = new AutoDeportivoBuilder();
            Director director = new Director();

            // El director guía el proceso de creación paso a paso
            director.ConstruirAutoDeportivoCompleto(builder);
            Vehiculo miAuto = builder.GetVehiculo();

            // Imprime los detalles del vehículo en la consola
            miAuto.MostrarDetalles();

            Console.WriteLine("Presione ENTER para salir...");
            Console.ReadLine();
        }
    }
}